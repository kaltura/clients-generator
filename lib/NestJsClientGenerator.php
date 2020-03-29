<?php

class NestJsClientGenerator extends TypescriptClientGenerator
{
	function __construct($xmlPath, Zend_Config $config)
	{
		parent::__construct($xmlPath, $config, "nestjs", "src/api");
	}
}
