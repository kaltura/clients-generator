<?php
class AndroidClientGenerator extends JavaClientGenerator
{
	function __construct($xmlPath, Zend_Config $config, $sourcePath = "sources/android")
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
		$this->addFiles("sources/java/src", "KalturaClient/src/");
		$this->addFiles("sources/java/src/test", "KalturaClientTester/src/main/");

		parent::generate();
	}

	protected function addFile($fileName, $fileContents, $addLicense = true)
	{
		$excludePaths = array(
			"KalturaClient/src/test",
			"KalturaClient/src/main/java/Kaltura.java",
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
