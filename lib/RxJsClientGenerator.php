<?php

class RxJSClientGenerator extends TypescriptClientGenerator
{
	function __construct($xmlPath, Zend_Config $config)
	{
		parent::__construct($xmlPath, $config, "rxjs", "src/api");
	}
}
