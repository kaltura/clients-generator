<?php

class NodeTypescriptClientGenerator extends TypescriptClientGenerator
{
	function __construct($xmlPath, Zend_Config $config)
	{
		parent::__construct($xmlPath, $config, "node-typescript", "src/api");
	}
}
