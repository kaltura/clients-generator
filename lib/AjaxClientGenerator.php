<?php
// Configure JS MIN
define('FILE_TYPE', 'text/javascript'); // type of code we're outputting
define('CACHE_LENGTH', 31356000); // length of time to cache output file, default approx 1 year
define('CREATE_ARCHIVE', true); // set to false to suppress writing of code archive, files will be merged on each request
define('JSMIN_AS_LIB', true); // prevents auto-run on include

require_once(__DIR__ . "/infra/jsmin.php");

class AjaxClientGenerator extends ClientGeneratorFromXml
{
	/**
	 * @var SimpleXMLElement
	 */
	private $schemaXml;
	
	/**
	* Constructor.
	* @param string $xmlPath path to schema xml.
	* @link http://www.kaltura.com/api_v3/api_schema.php
	*/
	function __construct($xmlPath, Zend_Config $config, $sourcePath = "sources/ajax")
	{
		parent::__construct($xmlPath, $sourcePath, $config);
	}
	
	function getSingleLineCommentMarker()
	{
		return '//';
	}
	
	/**
	* Parses the higher-level of the schema, divide parsing to five steps:
	* Enum creation, Object (VO) classes, Services and actions, Main, and project file.
	*/
	public function generate() 
	{	
		parent::generate();
	
		$this->schemaXml = new SimpleXMLElement(file_get_contents( $this->_xmlFile ));

		$this->startNewFile('KalturaClient.js');

		$includePaths = array(
				"KalturaClientBase.js",
				"webtoolkit.md5.js",
		);
		foreach($includePaths as $includePath)
		{
			$fileContents = file_get_contents($this->getSourceFilePath($includePath));
			$fileContents = str_replace('@DATE@', date('y-m-d'), $fileContents);
			$this->appendLine($fileContents);
			$this->appendLine();
		}
		
		$configurations = null;
		$services = null;
		foreach ($this->schemaXml->children() as $reflectionType) 
		{
			switch($reflectionType->getName())
			{
				case "services":
					//implement services (api actions)
					foreach($reflectionType->children() as $services_node)
					{
						$this->writeService($services_node);
					}
					$services = $reflectionType->children();
					
				break;	
				
				case "configurations":
					$configurations = $reflectionType->children();
					
				break;	
			}
		}

		$this->writeMainClass($services, $configurations);
		$this->closeFile();
		$this->minify('KalturaClient.js', 'KalturaClient.min.js');
	}

	protected function minify($fileName, $minFileName)
	{
		$filePath = $this->getFilePath($fileName);
		$minFilePath = $this->getFilePath($minFileName);
		
		$sCode = file_get_contents($filePath);
		$jsMin = new JSMin($sCode, false);
		file_put_contents($minFilePath, $jsMin->minify());
	}

	/**
	 * {@inheritDoc}
	 * @see ClientGeneratorFromXml::addFile()
	 */
	protected function addFile($fileName, $fileContents, $addLicense = true)
	{
		$excludePaths = array(
			"KalturaClientBase.js",
			"webtoolkit.md5.js",
		);
		
		foreach($excludePaths as $excludePath)
		{
			if($this->beginsWith($fileName, $excludePath))
				return;
		}
		
		parent::addFile($fileName, $fileContents, $addLicense);
	}
	
	/**
	* Parses Services and actions (calls that can be performed on the objects).
	* @param $serviceNodes		the xml node from the api schema representing an api service.
	*/
	protected function writeService(SimpleXMLElement $serviceNodes)
	{
		$serviceId = $serviceNodes->attributes()->id;
		if(!$this->shouldIncludeService($serviceId))
			return;
				
		$serviceName = $serviceNodes->attributes()->name;
		$serviceClassName = "Kaltura".ucfirst($serviceName)."Service";
		$serviceClass = "function $serviceClassName(client){\r\n";
		$serviceClass .= "\tthis.init(client);\r\n";
		$serviceClass .= "}\r\n";
		//$serviceClass .= "$serviceClassName.prototype = new KalturaServiceBase();\r\n";
		//$serviceClass .= "$serviceClassName.prototype.constructor = $serviceClassName;\r\n";
		$serviceClass .= "$serviceClassName.inheritsFrom (KalturaServiceBase);\r\n";
		
		$serviceClassDesc = "/**\r\n";
		$serviceClassDesc .= " *Class definition for the Kaltura service: $serviceName.\r\n";
		$actionsList = " * The available service actions:\r\n";
		
		//parse the service actions
		foreach($serviceNodes->children() as $action) {
			
			if($action->result->attributes()->type == 'file')
				continue;
				
			$actionDesc = "/**\r\n";
			$description = str_replace("\n", "\n *\t", $action->attributes()->description); // to format multi-line descriptions
			$actionDesc .= " * $description.\r\n";
			$actionsList .= " * @action\t".$action->attributes()->name."\t$description.\r\n";
			
			foreach($action->children() as $actionParam) {
				if($actionParam->getName() == "param" ) {
					$paramType = $actionParam->attributes()->type;
					
					$paramType = $this->getJSType($paramType);
					
					$paramName = $actionParam->attributes()->name;
					$optionalp = (boolean)$actionParam->attributes()->optional;
					$defaultValue = trim($actionParam->attributes()->default);
					$enumType = $actionParam->attributes()->enumType;
					$description = str_replace("\n", "\n *\t", $actionParam->attributes()->description); // to format multi-line descriptions
					$info = array();
					if ($optionalp)
						$info[] = "optional";
					if ($enumType && $enumType != '')
						$info[] = "enum: $enumType";
					if ($defaultValue && $defaultValue != '')
						$info[] = "default: $defaultValue";
					if (count($info)>0)
						$infoTxt = ' ('.join(', ', $info).')';
					$vardesc = " * @param\t$paramName\t$paramType\t\t{$description}{$infoTxt}";
					$actionDesc .= "$vardesc.\r\n";
				} else {
					$rettype = $actionParam->attributes()->type;
					$actionDesc .= " * @return\t$rettype.\r\n";
				}
			}
			
			$actionDesc .= " */";
			$actionClass = $actionDesc . "\r\n";
			
			//create a service action
			$actionName = $action->attributes()->name;
			
			$paramNames = array();
			foreach($action->children() as $actionParam)
				if($actionParam->getName() == "param" ) 
					$paramNames[] = $actionParam->attributes()->name;
			$paramNames = join(', ', $paramNames);
			
			// action method signature
			if (in_array($actionName, array("list", "clone", "delete", "export"))) // because list & clone are preserved in PHP And export is preserved in js
				$actionSignature = "$serviceClassName.prototype.".$actionName."Action = function($paramNames)";
			else
				$actionSignature = "$serviceClassName.prototype.".$actionName." = function($paramNames)";
			
			$actionClass .= $actionSignature."{\r\n";
			
			//validate parameter default values
			foreach($action->children() as $actionParam) {
				if($actionParam->getName() != "param" )
					continue;
				if ($actionParam->attributes()->optional == '0')
					continue;
				
				$paramName = $actionParam->attributes()->name;
				switch($actionParam->attributes()->type)
				{
					case "string":
					case "float":
					case "int":
					case "bigint":
					case "bool":
					case "array":
						$defaultValue = strtolower($actionParam->attributes()->default);
						if ($defaultValue != 'false' && 
							$defaultValue != 'true' && 
							$defaultValue != 'null' && 
							!is_numeric($defaultValue))
						{
							$defaultValue = '"'.$actionParam->attributes()->default.'"';
						}
						break;
					default: //is Object
						$defaultValue = "null";
						break;
				}
				
				$actionClass .= "\tif(!$paramName)\r\n";
				$actionClass .= "\t\t$paramName = $defaultValue;\r\n";
			}
			 
			$actionClass .= "\tvar kparams = new Object();\r\n";
			
			$haveFiles = false;
			//parse the actions parameters and result types
			foreach($action->children() as $actionParam) {
				if($actionParam->getName() != "param" ) 
					continue;
				$paramName = $actionParam->attributes()->name;
				if ($haveFiles === false && $actionParam->attributes()->type == "file") {
			        $haveFiles = true;
		        	$actionClass .= "\tkfiles = new Object();\r\n";
		    	}
				switch($actionParam->attributes()->type)
				{
					case "string":
					case "float":
					case "int":
					case "bigint":
					case "bool":
					case "array":
						$actionClass .= "\tkparams.$paramName = $paramName;\r\n";
						break;
					case "file":
						$actionClass .= "\tkfiles.$paramName = $paramName;\r\n";
						break;
					default: //is Object
						if ($actionParam->attributes()->optional == '1') {
							$actionClass .= "\tif ($paramName != null)\r\n\t";
						}
						$actionClass .= "\tkparams.$paramName = $paramName;\r\n";
						break;
				}
			}
			if ($haveFiles)
				$actionClass .= "\treturn new KalturaRequestBuilder(this.client, \"$serviceId\", \"$actionName\", kparams, kfiles);\r\n";
			else
				$actionClass .= "\treturn new KalturaRequestBuilder(this.client, \"$serviceId\", \"$actionName\", kparams);\r\n";
			$actionClass .= "}";
			$serviceClass .= $actionClass . "\r\n";
		}
		$serviceClassDesc .= $actionsList;
		$serviceClassDesc .= "*/";
		
		$this->appendLine($serviceClassDesc);
		$this->appendLine($serviceClass);
	}
	
	/**
	* Create the main class of the client library, may parse Services and actions.
	* initialize the service and assign to client to provide access to servcies and actions through the Kaltura client object.
	*/
	protected function writeMainClass(SimpleXMLElement $servicesNodes, SimpleXMLElement $configurations)
	{
		$apiVersion = $this->schemaXml->attributes()->apiVersion;
		$date = date('y-m-d');
		
		$this->appendLine("/**");
		$this->appendLine(" * The Kaltura Client - this is the facade through which all service actions should be called.");
		$this->appendLine(" * @param config the Kaltura configuration object holding partner credentials (type: KalturaConfiguration).");
		$this->appendLine(" */");
		$this->appendLine("function KalturaClient(config){");
		$this->appendLine("\tthis.init(config);");
		$this->appendLine("\tthis.setClientTag('ajax:$date');");
		$this->appendLine("\tthis.setApiVersion('$apiVersion');");
		$this->appendLine("}");
		$this->appendLine("KalturaClient.inheritsFrom (KalturaClientBase);");
		
		foreach($servicesNodes as $serviceNode)
		{
			if(!$this->shouldIncludeService($serviceNode->attributes()->id))
				continue;
				
			$serviceName = $serviceNode->attributes()->name;
			$serviceClassName = "Kaltura".ucfirst($serviceName)."Service";
			$this->appendLine("/**");
			$description = str_replace("\n", "\n *\t", $serviceNode->attributes()->description); // to format multi-line descriptions
			$this->appendLine(" * " . $description);
			$this->appendLine(" * @param $serviceClassName");
			$this->appendLine(" */");
			$this->appendLine("KalturaClient.prototype.$serviceName = null;");
		}
		
		$this->appendLine("/**");
		$this->appendLine(" * The client constructor.");
		$this->appendLine(" * @param config the Kaltura configuration object holding partner credentials (type: KalturaConfiguration).");
		$this->appendLine(" */");
		$this->appendLine("KalturaClient.prototype.init = function(config){");
		$this->appendLine("\t//call the super constructor:");
		$this->appendLine("\tKalturaClientBase.prototype.init.apply(this, arguments);");
		$this->appendLine("\t//initialize client services:");
		foreach($servicesNodes as $serviceNode)
		{
			if(!$this->shouldIncludeService($serviceNode->attributes()->id))
				continue;
				
			$serviceName = $serviceNode->attributes()->name;
			$serviceClassName = "Kaltura".ucfirst($serviceName)."Service";
			$this->appendLine("\tthis.$serviceName = new $serviceClassName(this);");
			$this->appendLine();
		}
		$this->appendLine("};");
		$this->appendLine();

		$this->writeConfigurationProperties('KalturaClient', $configurations, false);
		$this->writeConfigurationProperties('KalturaRequestBuilder', $configurations, true);
	}

	protected function writeConfigurationProperties($class, SimpleXMLElement $configurations, $enableVolatile)
	{
		foreach($configurations as $configurationName => $configuration)
		{
			/* @var $configurationNode DOMElement */
			$attributeName = lcfirst($configurationName) . "Configuration";
			$volatileProperties[$attributeName] = array();
		
			foreach($configuration->children() as $configurationProperty => $configurationPropertyNode)
			{			
				if(!$enableVolatile && $configurationPropertyNode->attributes()->volatile)
					continue;

				$type = $configurationPropertyNode->attributes()->type;
				$description = $configurationPropertyNode->attributes()->description;
				$this->writeConfigurationProperty($class, $configurationName, $configurationProperty, $configurationProperty, $type, $description);
	
				if($configurationPropertyNode->attributes()->alias)
				{
					$this->writeConfigurationProperty($class, $configurationName, $configurationPropertyNode->attributes()->alias, $configurationProperty, $type, $description);
				}
			}
		}
	}

	protected function writeConfigurationProperty($class, $configurationName, $name, $paramName, $type, $description)
	{
		$methodsName = ucfirst($name);
	
	
		$this->appendLine("/**");
		if($description)
		{
			$this->appendLine(" * $description");
			$this->appendLine(" * ");
		}
		$this->appendLine(" * @param $type \${$name}");
		$this->appendLine(" */");
		$this->appendLine("$class.prototype.set{$methodsName} = function($name){");
		$this->appendLine("	this.requestData.$paramName = $name;");
		$this->appendLine("};");
		$this->appendLine("");
	
	
		$this->appendLine("/**");
		if($description)
		{
			$this->appendLine(" * $description");
			$this->appendLine(" * ");
		}
		$this->appendLine(" * @return $type");
		$this->appendLine(" */");
		$this->appendLine("$class.prototype.get{$methodsName} = function(){");
		$this->appendLine("	return this.requestData.$paramName;");
		$this->appendLine("};");
		$this->appendLine("");
	}
	
	public function getJSType($propType)
	{		
		switch ($propType) 
		{	
			case "bigint" :
				return "int";
				
			default :
				return $propType;
		}
	}
}
