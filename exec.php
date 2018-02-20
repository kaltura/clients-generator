<?php 
/**
 * Client Libraries Generator - generate all the clients specified in the given config.ini according to Kaltura API reflection.
 * 
 * Create your client generator by -
 *      1 Choose a unique name to identify your generator (you can see which names are already in use in file 'config/generator.all.ini').
 * 		2 Copy one of the existing client generators (*ClientGenerator.php) and prefix it with your generator identifier.
 * 		2.1 In the second line, update the class name to match the generator identifier.
 * 		2.2 Edit the classes/functions rules according to the target client language.
 *      3. In this file, find where we 'require_once' all the generators and add the one you just created.
 * 		3 Create a new folder under 'sources', use the generator name as the folder name.
 *      4. Open file 'config/generator.all.ini' and add your generator name in a new line.
 * 		4. Open file 'config/generator.ini and update the following:
 *      4.1 Find line '########### Generators configuration ###########'
 *      4.2 Add the following under that line (replace 'XXXXX' with your generator name):
 * 			[XXXXX : public]
 *			generator = XXXXXClientGenerator
 *
 * To run the generator, use command line to run: php exec.php [arguments]
 * 
 * Optional arguments:
 * 		1. Generate Single - The first argument dictates that a single generator should be run instead of all defined in the config.ini
 * 							To use this argument, pass a valid id of one of the available generator classes.
 * 
 * config.ini file paramteres:
 * 		1. The client name is defined by block brackets (ini object) [client_name]
 * 		2. under the [clientname], define the generator parameters, each in new line, as following:
 * 			a. generator - The class name of the client library generator to use
 * 			b. exclude - whether to exclude any specific API services from the client library (ignored if include defined)
 * 			c. include - whether to include any specific API services from the client library (overrides exclude)
 * 			d. plugins - whether to include any specific API services from plugins
 * 			e. additional - whether to include any additional objects not directly defined through API services
 * 			f. internal - whether to show this client in the Client Libraries UI in the testme console, or not. 
 * 							note that setting schemaxml, will also make the client internal
 * 			g. nopackage - whether to generate a tar.gz package from the client library folder 
 * 			h. nofolder - there will not be client folder, the client files will be in output folder (if it's a single file like XML schema) 
 * 			i. ignore - whether to ignore any objects although defined through API services by inheritance
 * 			j. schemaxml - if empty, will introspect the code and create the schema XML, 
 * 							otherwise this should be a url to download schema XML from. Setting this will make the client internal
 * 
 * Notes:
 * 		* Kaltura API ignores only un-sent parameters. Thus, if you would like a parameter value to be left unchanged
 * 			or in classes that contain read-only parameters, make sure to NOT send any un-changed parameters in your HTTP requests.
 * 			A common issue with this, is with languages like Java and ActionScript where Boolean variables can't be set to null, 
 * 			thus it is uknown if the variable was modified before constructing the HTTP request to the server. If this is the case with your language, 
 * 			either create a Nullable Boolean type, or keep a map of changed parameters, then only send the variables in that map.
 */
error_reporting(E_ALL);
date_default_timezone_set('America/New_York');
//the name of the output folder for the generators -
chdir(__DIR__);
set_include_path(get_include_path() . PATH_SEPARATOR . __DIR__ . '/lib/infra');

require_once(__DIR__ . "/lib/infra/Zend/Config/Ini.php");
require_once(__DIR__ . "/lib/infra/KalturaLog.php");

require_once(__DIR__ . "/lib/ClientGeneratorFromXml.php");
require_once(__DIR__ . "/lib/AjaxClientGenerator.php");
require_once(__DIR__ . "/lib/JavaClientGenerator.php");
require_once(__DIR__ . "/lib/Java2ClientGenerator.php");
require_once(__DIR__ . "/lib/AndroidClientGenerator.php");
require_once(__DIR__ . "/lib/Android2ClientGenerator.php");
require_once(__DIR__ . "/lib/BpmnClientGenerator.php");
require_once(__DIR__ . "/lib/CliClientGenerator.php");
require_once(__DIR__ . "/lib/CSharpClientGenerator.php");
require_once(__DIR__ . "/lib/CSharp2ClientGenerator.php");
require_once(__DIR__ . "/lib/ErlangClientGenerator.php");
require_once(__DIR__ . "/lib/JsClientGenerator.php");
require_once(__DIR__ . "/lib/TypescriptClientGenerator.php");
require_once(__DIR__ . "/lib/NGXClientGenerator.php");
require_once(__DIR__ . "/lib/NodeClientGenerator.php");
require_once(__DIR__ . "/lib/Node2ClientGenerator.php");
require_once(__DIR__ . "/lib/ObjCClientGenerator.php");
require_once(__DIR__ . "/lib/Php4ClientGenerator.php");
require_once(__DIR__ . "/lib/Php53ClientGenerator.php");
require_once(__DIR__ . "/lib/Php5ClientGenerator.php");
require_once(__DIR__ . "/lib/PhpZendClientGenerator.php");
require_once(__DIR__ . "/lib/PojoClientGenerator.php");
require_once(__DIR__ . "/lib/PythonClientGenerator.php");
require_once(__DIR__ . "/lib/RubyClientGenerator.php");
require_once(__DIR__ . "/lib/TestmeDocGenerator.php");
require_once(__DIR__ . "/lib/TestmeGenerator.php");
require_once(__DIR__ . "/lib/Xml2As3ClientGenerator.php");
require_once(__DIR__ . "/lib/SwiftClientGenerator.php");

//the name of the summary file that will be used by the UI -
$summaryFileName = 'summary.kinf';
$tmpXmlFileName = tempnam(sys_get_temp_dir(), 'kaltura.generator.');

$options = getopt('hx:r:t:', array(
	'help',
	'xml:',
	'root:',
	'tests:',
	'dont-gzip',
));

function showHelpAndExit()
{
	echo "Usage:\n";
	echo "\tphp " . __FILE__ . " [options] [client libs] [destination]\n";
	echo "\tOptions:\n";
	echo "\t\t-h, --help:   \tShow this help.\n";
	echo "\t\t-x, --xml:    \tUse XML path or URL as source XML.\n";
	echo "\t\t-r, --root:   \tRoot path, default is /opt/kaltura.\n";
	echo "\t\t-t, --tests:  \tUse different tests configuration, valid values are OVP or OTT, default is OVP.\n";
	echo "\t\t--dont-gzip:  \tTar the packages without gzip.\n";
	
	exit;
}

$schemaXmlPath = null;
$rootPath = realpath('/opt/kaltura');
$testsDir = 'ovp';
$gzip = true;
foreach($options as $option => $value)
{
	if($option == 'h' || $option == 'help')
	{
		showHelpAndExit();
	}
	elseif($option == 'x' || $option == 'xml')
	{
		$schemaXmlPath = $value;
	}
	elseif($option == 'r' || $option == 'root')
	{
		$rootPath = $value;
	}
	elseif($option == 't' || $option == 'tests')
	{
		$testsDir = strtolower($value);
	}
	elseif($option == 'dont-gzip')
	{
		$gzip = false;
	}
	array_shift($argv);
}	 

//pass the name of the generator as the first argument of the command line to
//generate a single library. if this argument is empty or 'all', generator will create all libs.
$generateSingle = isset($argv[1]) ? $argv[1] : null;

//second command line argument specifies the output path, if not specified will default to 
//<content root>/content/clientlibs
if (isset($argv[2]))
{
	$outputPathBase = $argv[2];
}
else
{
	$outputPathBase = fixPath("$rootPath/web/content/clientlibs");
}

if(file_exists($outputPathBase))
{
	if(!$schemaXmlPath)
	{
		if(file_exists('KalturaClient.xml'))
			$schemaXmlPath = realpath('KalturaClient.xml');
		elseif(file_exists("$outputPathBase/KalturaClient.xml"))
			$schemaXmlPath = fixPath("$outputPathBase/KalturaClient.xml");
	}
}
else
{
	mkdir($outputPathBase, 0755, true);
}

if(!file_exists($schemaXmlPath))
	die("XML file [$schemaXmlPath] not found\n");

//pull the generator config ini
$config = new Zend_Config_Ini(__DIR__ . '/config/generator.ini', null, array('allowModifications' => true));

$libsToGenerate = null;
if (strtolower($generateSingle) == 'all')
{
	$generateSingle = null;
}
elseif(!$generateSingle)
{
	$libsToGenerate = file(__DIR__ . '/config/generator.defaults.ini');
	foreach($libsToGenerate as $key => &$default)
		$default = strtolower(trim($default, " \t\r\n"));
}

//if we got specific generator request, tes if this requested generator does exist
if ($generateSingle != null)
{
	$libsToGenerate = array_map('strtolower', array_intersect(explode(',', $generateSingle), array_keys($config->toArray())));
}

KalturaLog::info("Downloading ready-made schema from: $schemaXmlPath");
$contents = file_get_contents($schemaXmlPath);
file_put_contents($tmpXmlFileName, $contents);

$xml = new DOMDocument();
$xml->load($schemaXmlPath);

$documentElement = $xml->documentElement;
$apiVersion = $documentElement->getAttribute("apiVersion");
$generatedDate = date('d-m-Y', $documentElement->getAttribute("generatedDate"));
KalturaLog::info("Generating from api version: $apiVersion, generated at: $generatedDate");

if (file_exists($outputPathBase."/".$summaryFileName)){
    $generatedClients=unserialize(file_get_contents($outputPathBase."/".$summaryFileName));
    $generatedClients['generatedDate']=$generatedDate;
    $generatedClients['apiVersion']=$apiVersion;
}else{
    $generatedClients = array(
	'generatedDate' => $generatedDate,
	'apiVersion' => $apiVersion,
    );
}

// Loop through the config.ini and generate the client libraries -
foreach($config as $name => $item)
{
	/* @var $item Zend_Config */

	if(!$item->tags)
		$item->tags = $name;
	
	//get the generator class name
	$generator = $item->get("generator");
	
	//check if this client should not be packaged as tar.gz file
	$shouldNotPackage = $item->get("nopackage");

	//check if we should create a folder for this client library files, or directly create files on main output folder
	$mainOutput = $item->get("nofolder");

	// check if generator is valid (not null and there is a class by this name)
	if ($generator === null)
		continue;
	
	if (!class_exists($generator))
		throw new Exception("Generator [".$generator."] not found");
	
	if($libsToGenerate && !in_array(strtolower($name), $libsToGenerate))
		continue;

	//check if this client should be internal or public (on the UI)
	if (!$item->get("internal"))
	{
		$params = array(
			'linkhref' => $item->get('linkhref'),
			'linktext' => $item->get('linktext'));
		$generatedClients[$name] = $params;
	}
	
	KalturaLog::info("Now generating: $name using $generator");
	
	// create the API schema to be used by the generator
	$reflectionClass = new ReflectionClass($generator);
	
	$instance = $reflectionClass->newInstance($tmpXmlFileName, $item);
	/* @var $instance ClientGeneratorFromXml */
	
	if($item->get("generateDocs"))
		$instance->setGenerateDocs($item->get("generateDocs"));
		
	if($item->get("package"))
		$instance->setPackage($item->get("package"));
		
	if($item->get("subpackage"))
		$instance->setSubpackage($item->get("subpackage"));
	
	if (isset($item->params) && $item->params)
	{
		foreach($item->params as $key => $val)
		{
			$instance->setParam($key, $val);
		}
	}
	
	if (isset ($item->excludeSourcePaths))
	{
		$instance->setExcludeSourcePaths ($item->excludeSourcePaths);
	}
	
	$copyPath = null;
	if($item->get("copyPath"))
		$copyPath = fixPath("$rootPath/app/" . $item->get("copyPath"));
	
	if ($mainOutput)
	{ 
		$outputPath = $outputPathBase;
	}
	else
	{
		$destination = $name;
		if($item->destinationName)
			$destination = $item->destinationName;
		
		$outputPath = fixPath("$outputPathBase/$destination");
		$clearPath = null;
		if($item->get("clearPath"))
			$clearPath = fixPath("$rootPath/app/" . $item->get("clearPath"));
		else
			$clearPath = $copyPath;
		
		if(!file_exists($clearPath))
			$clearPath = null;
			
		if($clearPath || file_exists($outputPath))
		{
			if (strtoupper(substr(PHP_OS, 0, 3)) === 'WIN')
			{
				$winOutputPath = realpath($outputPath);
				KalturaLog::info("Delete old files [$winOutputPath" . ($clearPath ? ", $clearPath" : "") . "]");
				passthru("rmdir /Q /S $winOutputPath $clearPath");
			}
			else
			{
				KalturaLog::info("Delete old files [$outputPath" . ($clearPath ? ", $clearPath" : "") . "]");
				passthru("rm -fr $outputPath $clearPath");
			}
		}
	}
		
	KalturaLog::info("Generate client library [$name]");
	$instance->setOutputPath($outputPath, $copyPath);
	$instance->setTestsPath($testsDir);
	$instance->generate();
	
	KalturaLog::info("Saving client library to [$outputPath]");
	
	$oldMask = umask();
	umask(0);
		
	$instance->done($outputPath);
	umask($oldMask);
	
	//tar gzip the client library
	if (!$shouldNotPackage) 
		createPackage($outputPath, $name, $generatedDate, $gzip);
		
	KalturaLog::info("$name generated successfully");
}

//delete the api services xml schema file
if (file_exists($tmpXmlFileName))
	unlink($tmpXmlFileName);

//write the summary file (will be used by the generator UI)
file_put_contents($outputPathBase."/".$summaryFileName, serialize($generatedClients));

exit(0);

function fixPath($path)
{
	return str_replace('/', DIRECTORY_SEPARATOR, $path);
}

/**
 * Build a packaged tarball for the client library.
 * @param $outputPath 		The path the client library files are located at.
 * @param $generatorName	The name of the client library.
 */
function createPackage($outputPath, $generatorName, $generatedDate, $gzip)
{
	KalturaLog::info("Trying to package");
	$output = shell_exec("tar --version");
	if ($output === null)
	{
		KalturaLog::warning("Skipping packaging, \"tar\" command not found! On Windows, tar can be installed using Cygwin, and it should be added to the path");
	}
	else
	{
		$fileName = "{$generatorName}_{$generatedDate}.tar.gz";
		$gzipOutputPath = "../".$fileName;
		$options = $gzip ? '-czf' : '-cf';
		$cmd = "tar $options \"$gzipOutputPath\" ../".$generatorName;
		$oldDir = getcwd();
		
		$outputPath = realpath($outputPath);
		KalturaLog::debug("Changing dir to [$outputPath]");
		chdir($outputPath);
		
		KalturaLog::info("Executing: $cmd"); 
		passthru($cmd);
		
		if (file_exists($gzipOutputPath))
			KalturaLog::info("Package created successfully: $gzipOutputPath");
		else
			KalturaLog::err("Failed to create package");
			
		KalturaLog::debug("Restoring dir to [$oldDir]");
		chdir($oldDir);
	}
}
