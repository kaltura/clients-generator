<?php

require_once (__DIR__ . '/typescript/GeneratedFileData.php');
require_once (__DIR__ . '/typescript/KalturaServerMetadata.php');
require_once (__DIR__ . '/typescript/ClassesGenerator.php');
require_once (__DIR__ . '/typescript/EnumsGenerator.php');


class NGXClientGenerator extends TypescriptClientGenerator
{
	protected $_baseClientPath = "src/api";

	function __construct($xmlPath, Zend_Config $config)
	{
		parent::__construct($xmlPath, $config,"ngx");
	}
}
