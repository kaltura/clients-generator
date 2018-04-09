<?php

class NGXClientGenerator extends TypescriptClientGenerator
{
	protected $_baseClientPath = "src/api";

	function __construct($xmlPath, Zend_Config $config)
	{
		parent::__construct($xmlPath, $config,"ngx");
	}
}
