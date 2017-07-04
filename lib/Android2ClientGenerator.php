<?php
class Android2ClientGenerator extends Java2ClientGenerator
{
	function __construct($xmlPath, Zend_Config $config, $sourcePath = "android2")
	{
		$this->_baseClientPath = "KalturaClient/" . $this->_baseClientPath;
		parent::__construct($xmlPath, $config, $sourcePath);
	}
	
	protected function normalizeSlashes($path)
	{
		return str_replace('/', DIRECTORY_SEPARATOR, $path);
	}

	protected function addFiles($sourcePath, $destPath)
	{
		$sourcePath = realpath($sourcePath);
		$destPath = $this->normalizeSlashes($destPath);
		$this->addSourceFiles($sourcePath, $sourcePath . DIRECTORY_SEPARATOR, $destPath);
	}
	
	public function generate() 
	{
		$this->addFiles("sources/newjava/src", "KalturaClient/src/");
		$this->addFiles("sources/newjava/src/test", "KalturaClientTester/src/main/");
		$this->addFiles("sources/newjava/src/OttTest", "KalturaOttClientTester/src/main/");

		parent::generate();
	}

	protected function addFile($fileName, $fileContents, $addLicense = true)
	{
		$excludePaths = array(
		    "DemoApplication",
			"KalturaClientTester",
			"KalturaClient/src/test",
			"KalturaClient/src/main/java/Kaltura.java",
			"KalturaClient/src/main/java/com/kaltura/client/deprecated",
			"KalturaClient/src/main/java/com/kaltura/client/KalturaLoggerLog4j.java",
		);

		foreach($excludePaths as $excludePath)
		{
			if($this->beginsWith($fileName, $excludePath))
				return;
		}

		$fileContents = str_replace(
				'String clientTag = "java:@DATE@"', 
				'String clientTag = "android:@DATE@"', 
				$fileContents);

		parent::addFile($fileName, $fileContents, $addLicense);
	}
}
