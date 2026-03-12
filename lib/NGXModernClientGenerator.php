<?php

/**
 * NGX Modern Client Generator - generates Angular 19+ compatible client library
 * 
 * This generator creates a modern Angular client that is compatible with:
 * - Angular 19.2.19+ (security patched version)
 * - Node.js 18+ (including 20.x, 22.x, 24.x)
 * - RxJS 7.x
 * - TypeScript 5.5+
 * 
 * Usage:
 *   php exec.php ngxModern [output_path]
 * 
 * For the legacy Angular 6.x client, use:
 *   php exec.php ngx [output_path]
 */
class NGXModernClientGenerator extends TypescriptClientGenerator
{
	function __construct($xmlPath, Zend_Config $config)
	{
		parent::__construct($xmlPath, $config, "ngx-modern", "projects/kaltura-ngx-client/src/lib/api");
	}
}
