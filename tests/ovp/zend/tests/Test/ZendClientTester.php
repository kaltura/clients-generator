<?php

class ZendClientTester
{
	const UPLOAD_VIDEO_FILENAME = 'DemoVideo.flv';
	const UPLOAD_IMAGE_FILENAME = 'DemoImage.jpg';
	const ENTRY_NAME = 'Media entry uploaded from Zend Framework client library';
	
	/**
	 * @var Kaltura_Client_Client
	 */
	protected $_client;
	
	/**
	 * @var int
	 */
	protected $_partnerId;

	/**
	 *
	 * @var array
	 */
	protected $_responseHeaders;
	
	public function __construct(Kaltura_Client_Client $client, $partnerId)
	{
		$this->_client = $client;
		$this->_partnerId = $partnerId;
	}
	
	public function run()
	{
		$methods = get_class_methods($this);
		foreach($methods as $method)
		{
			if (strpos($method, 'test') === 0)
			{
				try 
				{
					// use the client logger interface to log
					$this->_client->getConfig()->getLogger()->log('Running '.$method);
					$this->$method();
				}
				catch(Exception $ex)
				{
					
					$this->_client->getConfig()->getLogger()->log($method . ' failed with error: ' . $ex->getMessage());
					return;
				}
			}
		}
		echo "\nFinished running client library tests\n";
	}
	
	public function testSyncFlow()
	{
	    // add upload token
	    $uploadToken = new Kaltura_Client_Type_UploadToken();
	    $uploadToken->fileName = self::UPLOAD_VIDEO_FILENAME;
	    $uploadToken = $this->_client->uploadToken->add($uploadToken);
	    $this->assertTrue(strlen($uploadToken->id) > 0);
	    $this->assertEqual($uploadToken->fileName, self::UPLOAD_VIDEO_FILENAME);
	    $this->assertEqual($uploadToken->status, Kaltura_Client_Enum_UploadTokenStatus::PENDING);
	    $this->assertEqual($uploadToken->partnerId, $this->_partnerId);
	    $this->assertEqual($uploadToken->fileSize, null);
	    
	    // add media entry
	    $entry = new Kaltura_Client_Type_MediaEntry();
	    $entry->name = self::ENTRY_NAME;
	    $entry->mediaType = Kaltura_Client_Enum_MediaType::VIDEO;
	    $entry = $this->_client->media->add($entry);
	    $this->assertTrue(strlen($entry->id) > 0);
	    $this->assertTrue($entry->status === Kaltura_Client_Enum_EntryStatus::NO_CONTENT);
	    $this->assertTrue($entry->name === self::ENTRY_NAME);
	    $this->assertTrue($entry->partnerId === $this->_partnerId);
	    
	    // add uploaded token as resource
	    $resource = new Kaltura_Client_Type_UploadedFileTokenResource();
	    $resource->token = $uploadToken->id;
	    $entry = $this->_client->media->addContent($entry->id, $resource);
	    $this->assertTrue($entry->status === Kaltura_Client_Enum_EntryStatus::IMPORT);
	    
	    // upload file using the upload token
	    $uploadFilePath = dirname(__FILE__) . '/../resources/' . self::UPLOAD_VIDEO_FILENAME;
	    $uploadToken = $this->_client->uploadToken->upload($uploadToken->id, $uploadFilePath);
	    $this->assertTrue($uploadToken->status === Kaltura_Client_Enum_UploadTokenStatus::CLOSED);
	    
	    // get flavor by entry
	    $flavorArray = $this->_client->flavorAsset->getByEntryId($entry->id);
	    $this->assertTrue(count($flavorArray) > 0);
	    $foundSource = false;
	    foreach($flavorArray as $flavor)
	    {
		    if ($flavor->flavorParamsId !== 0)
			    continue;
			    
		    $this->assertTrue($flavor->isOriginal);
		    $this->assertTrue($flavor->entryId === $entry->id);
		    $foundSource = true;
	    }
	    $this->assertTrue($foundSource);
	    
	    // count media entries
	    $mediaFilter = new Kaltura_Client_Type_MediaEntryFilter();
	    $mediaFilter->idEqual = $entry->id;
	    $mediaFilter->statusNotEqual = Kaltura_Client_Enum_EntryStatus::DELETED;
	    $entryCount = $this->_client->media->count($mediaFilter);
	    $this->assertTrue($entryCount == 1);
	    
	    // delete media entry
	    $this->_client->media->delete($entry->id);
	    
	    sleep(5); // wait for the status to update
    	
    	// count media entries again
	    $entryCount = $this->_client->media->count($mediaFilter);
	    $this->assertTrue($entryCount == 0);
	}
	
	public function testReturnedArrayObjectUsingPlaylistExecute()
	{
	    // add image entry
	    $imageEntry = $this->addImageEntry();
	    sleep(5); // wait for the status to update
	    
	    // execute playlist from filters
	    $playlistFilter = new Kaltura_Client_Type_MediaEntryFilterForPlaylist();
	    $playlistFilter->tagsLike = $imageEntry->tags;
	    $filterArray = array();
	    $filterArray[] = $playlistFilter;
	    $playlistExecute = $this->_client->playlist->executeFromFilters($filterArray, 10);
	    $this->assertEqual(count($playlistExecute), 1);
	    $firstPlaylistEntry = $playlistExecute[0];
	    $this->assertEqual($firstPlaylistEntry->id, $imageEntry->id);
	    
	    $this->_client->media->delete($imageEntry->id);
	}

	public function testServeUrl()
	{
		$imageEntry = $this->addImageEntry();

		$newThumbAsset = $this->_client->thumbAsset->addFromUrl($imageEntry->id, $imageEntry->thumbnailUrl);

		$thumbAssetFilter = new Kaltura_Client_Type_ThumbAssetFilter();
		$thumbAssetFilter->entryIdEqual = $imageEntry->id;

		$thumbAssets = $this->_client->thumbAsset->listAction($thumbAssetFilter);

		// check we have assets
		$this->assertTrue(!(count($thumbAssets->objects) == 0));

		$asset = $thumbAssets->objects[0];

		$serveUrl = $this->_client->thumbAsset->serve($asset->id);

		// fecth using CURL and test headers
		$res = $this->doCurl($serveUrl);

		$this->assertEqual($res[1], 200);

		$contentType = $this->getResponseContentType();
		$this->assertEqual(strtolower($contentType), 'image/jpeg');

		// delete media entry
		$this->_client->media->delete($imageEntry->id);
	}
	
	public function testMultiRequest()
	{
		$this->_client->startMultiRequest();
		$entry = new Kaltura_Client_Type_BaseEntry();
		$entry->name = "test entry 1";
		$entry->tags = "test1";
		$entryAddResult = $this->_client->baseEntry->add($entry);
		$this->assertEqual((string)$entryAddResult, '{1:result}');
		$this->assertEqual((int)$entryAddResult->value, 1);
		$this->assertEqual((string)$entryAddResult->creatorId, '{1:result:creatorId}');
		$user = new Kaltura_Client_Type_User();
		$user->id = "test1".rand(0, 10000000);
		$user->type = Kaltura_Client_Enum_UserType::USER;
		$userAddResult = $this->_client->user->add($user);
		$this->assertEqual((string)$userAddResult, '{2:result}');
		$this->assertEqual((int)$userAddResult->value, 2);
		$this->assertEqual((string)$userAddResult->id, '{2:result:id}');

		$badUser = new Kaltura_Client_Type_User();
		$badUser->id = "  test  1".rand(0, 10000000); // spaces in user ID not allowed, expected error
		$badUser->type = Kaltura_Client_Enum_UserType::USER;
		$badUserAddResult = $this->_client->user->add($badUser);
		$this->assertEqual((string)$badUserAddResult, '{3:result}');
		$this->assertEqual((int)$badUserAddResult->value, 3);

		$results = $this->_client->doMultiRequest();
		$this->assertTrue($results[0]->name === $entry->name);
		$this->assertTrue($results[1]->id === $user->id);
		$this->assertTrue($results[2] instanceof Kaltura_Client_Exception);
	}

	public function testResponseProfile() {
		$entry = $this->addImageEntry();

		$filter = new Kaltura_Client_Type_ThumbAssetFilter();
		$userFilter = new Kaltura_Client_Type_UserFilter();

		$resourceMapping = new Kaltura_Client_Type_ResponseProfileMapping();
		$resourceMapping->filterProperty = 'entryIdEqual';
		$resourceMapping->parentProperty = 'id';
        
		$userResourceMapping = new Kaltura_Client_Type_ResponseProfileMapping();
		$userResourceMapping->filterProperty = 'idEqual';
		$userResourceMapping->parentProperty = 'userId';

		$thumbListResponseProfile = new Kaltura_Client_Type_ResponseProfile();
		$thumbListResponseProfile->name = "thumbsOfEntry";
		$thumbListResponseProfile->filter = $filter;
		$thumbListResponseProfile->mappings = array($resourceMapping);

		$userResponseProfile = new Kaltura_Client_Type_ResponseProfile();
		$userResponseProfile->name = "entryOwner";
		$userResponseProfile->filter = $userFilter;
		$userResponseProfile->mappings = [$userResourceMapping];

		$responseProfile = new Kaltura_Client_Type_ResponseProfile();
		$responseProfile->name = 'entry';
		$responseProfile->relatedProfiles = [
			$thumbListResponseProfile,
			$userResponseProfile
		];

		$this->_client->setResponseProfile($responseProfile);
		$result = $this->_client->media->get($entry->id);
		$this->assertTrue(count($result->relatedObjects) > 0);
		$this->assertTrue(isset($result->relatedObjects['thumbsOfEntry']));
		$this->assertTrue(isset($result->relatedObjects['entryOwner']));
		$this->assertTrue($result->relatedObjects['entryOwner']->objects[0]->id === $entry->userId);
	}

	public function testResponseProfileUnNamed() {
		$entry = $this->addImageEntry();

		$filter = new Kaltura_Client_Type_ThumbAssetFilter();
		$userFilter = new Kaltura_Client_Type_UserFilter();

		$resourceMapping = new Kaltura_Client_Type_ResponseProfileMapping();
		$resourceMapping->filterProperty = 'entryIdEqual';
		$resourceMapping->parentProperty = 'id';
        
		$userResourceMapping = new Kaltura_Client_Type_ResponseProfileMapping();
		$userResourceMapping->filterProperty = 'idEqual';
		$userResourceMapping->parentProperty = 'userId';

		$thumbListResponseProfile = new Kaltura_Client_Type_ResponseProfile();
		$thumbListResponseProfile->filter = $filter;
		$thumbListResponseProfile->mappings = array($resourceMapping);

		$userResponseProfile = new Kaltura_Client_Type_ResponseProfile();
		$userResponseProfile->filter = $userFilter;
		$userResponseProfile->mappings = [$userResourceMapping];

		$responseProfile = new Kaltura_Client_Type_ResponseProfile();
		$responseProfile->relatedProfiles = [
			$userResponseProfile,
			$thumbListResponseProfile,
		];

		$this->_client->setResponseProfile($responseProfile);
		$result = $this->_client->media->get($entry->id);
		$this->assertTrue(count($result->relatedObjects) > 0);
		$this->assertTrue(isset($result->relatedObjects[0]));
		$this->assertTrue(isset($result->relatedObjects[1]));
		$this->assertTrue($result->relatedObjects[0]->objects[0]->id === $entry->userId);
	}

	public function testMultiLingualObject() {
		$this->_client->setLanguage('multi');
		$entry = new Kaltura_Client_Type_BaseEntry();
		$entry->description = "test multiling";
		$entry->tags = "testmulti";
		$nameEn = new Kaltura_Client_Type_MultiLingualString();
		$nameEn->language = 'EN';
		$nameEn->value = "Test Entry";
		$nameEs = new Kaltura_Client_Type_MultiLingualString();
		$nameEs->language = 'ES';
		$nameEs->value = "Entrada de prueba";
		$entry->multiLingual_name = [
			$nameEn,
			$nameEs,
		];
		
		$newEntry = $this->_client->baseEntry->add($entry, Kaltura_Client_Enum_EntryType::MEDIA_CLIP);

		$this->assertTrue(empty($newEntry->name));
		$this->assertTrue(is_array($newEntry->multiLingual_name));
		$this->assertTrue(empty($newEntry->description));
		$this->assertTrue(is_array($newEntry->multiLingual_description));
		foreach($newEntry->multiLingual_name as $multiLangName) {
			if($multiLangName->language == 'EN') {
				$this->assertEqual($multiLangName->value , $nameEn->value);
			}
			if($multiLangName->language == 'ES') {
				$this->assertEqual($multiLangName->value , $nameEs->value);
			}
		}
		
		$this->_client->setLanguage(null);
		$entryNotMultiLang = $this->_client->baseEntry->get($newEntry->id);
		$this->assertTrue(!is_array($entryNotMultiLang->name));
		$this->assertTrue(empty($entryNotMultiLang->multiLingual_name));

		$this->_client->setLanguage('ES');

		$entryNotMultiLang = $this->_client->baseEntry->get($newEntry->id);
		$this->assertTrue(!is_array($entryNotMultiLang->name));
		$this->assertEqual($entryNotMultiLang->name, $nameEs->value);
		$this->assertTrue(empty($entryNotMultiLang->multiLingual_name));

		$this->_client->setLanguage(null);
	}

	public function testAccessControlProfile()
	{
		$accessControlProfileName = '__test_access_control_profile_' . rand(0, 10000);
		$accessControlProfile = new Kaltura_Client_Type_AccessControlProfile();
		$accessControlProfile->name = $accessControlProfileName;
		$accessControlProfile->isDefault = Kaltura_Client_Enum_NullableBoolean::FALSE_VALUE;

		$code = '1';
		$message = 'Test Rule';
		$rule = new Kaltura_Client_Type_Rule();
		$rule->code = $code;
		$rule->message = $message;

		$accessControlProfile->rules = [$rule];

		$accessControlProfile = $this->_client->accessControlProfile->add($accessControlProfile);

		$this->assertEqual($accessControlProfile->name, $accessControlProfileName);
		$this->assertEqual($accessControlProfile->rules[0]->code, $code);
		$this->assertEqual($accessControlProfile->rules[0]->message, $message);

		$this->_client->accessControlProfile->delete($accessControlProfile->id);
	}

	public function addImageEntry()
	{
		$entry = new Kaltura_Client_Type_MediaEntry();
		$entry->name = self::ENTRY_NAME;
		$entry->mediaType = Kaltura_Client_Enum_MediaType::IMAGE;
		$entry->tags = uniqid('test_');
		$entry = $this->_client->media->add($entry);

		$uploadToken = new Kaltura_Client_Type_UploadToken();
		$uploadToken->fileName = self::UPLOAD_IMAGE_FILENAME;
		$uploadToken = $this->_client->uploadToken->add($uploadToken);

		$uploadFilePath = dirname(__FILE__) . '/../resources/' . self::UPLOAD_IMAGE_FILENAME;
		$uploadToken = $this->_client->uploadToken->upload($uploadToken->id, $uploadFilePath);

		$resource = new Kaltura_Client_Type_UploadedFileTokenResource();
		$resource->token = $uploadToken->id;
		$entry = $this->_client->media->addContent($entry->id, $resource);

		return $entry;
	}

	protected function assertTrue($v)
	{
		if ($v !== true)
		{
			$backtrace = debug_backtrace();
			$msg = 'Assert failed on line: ' . $backtrace[0]['line'];
			throw new Exception($msg);
		}
	}

	protected function assertEqual($actual, $expected)
	{
		if ($actual !== $expected)
		{
			$backtrace = debug_backtrace();
			$msg = sprintf(
				"Assert failed on line: {$backtrace[0]['line']}, expecting [%s] of type [%s], actual is [%s] of type [%s]",
				$expected,
				gettype($expected),
				$actual,
				gettype($actual));
			throw new Exception($msg);
		}
	}

	/**
	 * Curl HTTP POST Request
	 *
	 * @param string $url
	 * @param array $params
	 * @return array of result and error
	 */
	private function doCurl($url, $params = array(), $files = array()) {
		$this->_responseHeaders = array();
		$ch = curl_init();
		curl_setopt($ch, CURLOPT_URL, $url);
		curl_setopt($ch, CURLOPT_POST, 1);
		if (count($files) > 0) {
			foreach ($files as &$file) {
				// The usage of the @filename API for file uploading is
				// deprecated since PHP 5.5. CURLFile must be used instead.
				if (PHP_VERSION_ID >= 50500) {
					$file = new \CURLFile($file);
				} else {
					$file = "@" . $file; // let curl know its a file
				}
			}
			curl_setopt($ch, CURLOPT_POSTFIELDS, array_merge($params, $files));
		} else {
			$opt = http_build_query($params, '', "&");
			curl_setopt($ch, CURLOPT_POSTFIELDS, $opt);
		}
		curl_setopt($ch, CURLOPT_ENCODING, 'gzip,deflate');
		curl_setopt($ch, CURLOPT_RETURNTRANSFER, true);
		if (count($files) > 0)
			curl_setopt($ch, CURLOPT_TIMEOUT, 0);

		curl_setopt($ch, CURLOPT_SSL_VERIFYPEER, false);
		curl_setopt($ch, CURLOPT_SSL_VERIFYHOST, 2);

		// Save response headers
		curl_setopt($ch, CURLOPT_HEADERFUNCTION, array($this, 'readHeader'));

		$result = curl_exec($ch);
		$curlError = curl_error($ch);
		$curlErrorCode = curl_getinfo($ch, CURLINFO_HTTP_CODE);
		curl_close($ch);
		return array($result, $curlErrorCode, $curlError);
	}

	/* Store response headers into array */

	public function readHeader($ch, $string) {
		array_push($this->_responseHeaders, $string);
		return strlen($string);
	}

	private function getResponseContentType() {
		foreach ($this->_responseHeaders as $header) {
			$pair = explode(':', $header);
			if (isset($pair[0]) && strtolower($pair[0]) == 'content-type' && isset($pair[1])) {
				return trim($pair[1]);
			}
		}

		return null;
	}

}
