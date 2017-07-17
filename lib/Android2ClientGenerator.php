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
		$this->addFiles("sources/java2/src", "KalturaClient/src/");

		parent::generate();
	}

	protected function addFile($fileName, $fileContents, $addLicense = true)
	{
		$excludePaths = array(
			"KalturaClient/src/main/java/com/kaltura/client/LoggerLog4j.java",
		);

		foreach($excludePaths as $excludePath)
		{
			if($this->beginsWith($fileName, $excludePath))
				return;
		}

		parent::addFile($fileName, $fileContents, $addLicense);
	}
}
