<?php

define('CONFIG_FILE', 'config.ini');

require_once(dirname(__FILE__).'/TestsAutoloader.php');
TestsAutoloader::register();

require_once(dirname(__FILE__).'/SampleLoggerImplementation.php');
require_once(dirname(__FILE__).'/Test/ZendClientTester.php');

$testerConfig = parse_ini_file(dirname(__FILE__).'/'.CONFIG_FILE);

// init kaltura configuration
$config = new Kaltura_Client_Configuration();
$config->serviceUrl = $testerConfig['serviceUrl'];
$config->curlTimeout = 120;
$config->setLogger(new SampleLoggerImplementation());

// init kaltura client
$client = new Kaltura_Client_Client($config);

// check connectivity
try
{
	$client->system->ping();
}
catch (Kaltura_Client_Exception $ex)
{
	$config->getLogger()->log('Ping failed with api error: '.$ex->getMessage());
	die;
}
catch (Kaltura_Client_ClientException $ex)
{
	$config->getLogger()->log('Ping failed with client error: '.$ex->getMessage());
	die;
}

$options = getopt('', array('persistConnection'));

if(array_key_exists("persistConnection",$options)){
	// run the tester with persistent connection
	$client->setPersistConnection(true);
}

// run the tester
$tester = new ZendClientTester($client, intval($testerConfig['partnerId']));
$tester->run($testerConfig);