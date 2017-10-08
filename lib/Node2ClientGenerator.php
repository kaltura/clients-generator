<?php
class Node2ClientGenerator extends ClientGeneratorFromXml
{
	/**
	 * @var SimpleXMLElement
	 */
	private $schemaXml;
	
	protected $enumTypes = '';
	protected $voClasses = '';
	protected $serviceClasses = '';
	protected $mainClass = '';
	protected $requestClasses = '';
	
	/**
	 * Constructor.
	 * @param string $xmlPath path to schema xml.
	 * @link http://www.kaltura.com/api_v3/api_schema.php
	 */
	function __construct($xmlPath, Zend_Config $config, $sourcePath = "node2")
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
		
		$this->schemaXml = new SimpleXMLElement(file_get_contents($this->_xmlFile));
		
		$services = null;
		$configurations = null;
		
		//parse object types
		foreach($this->schemaXml->children() as $reflectionType)
		{
			switch($reflectionType->getName())
			{
				case 'enums':
					foreach($reflectionType->children() as $enums_node)
					{
						$this->writeEnum($enums_node);
					}
					break;
					
				case 'classes':
					$this->echoLine($this->voClasses, "const kaltura = require('./KalturaClientBase');");
					foreach($reflectionType->children() as $classes_node)
					{
						$this->writeObjectClass($classes_node);
					}
					break;
					
				case 'services':
					$this->echoLine($this->serviceClasses, "const kaltura = require('./KalturaClientBase');");
					foreach($reflectionType->children() as $services_node)
					{
						$this->writeService($services_node);
					}
					
					$services = $reflectionType->children();
					break;
					
				case 'configurations':
					$configurations = $reflectionType->children();
					break;
			}
		}
		
		//write main class (if needed, this can also be included in the static sources folder if not dynamic)
		$this->writeMainClass();
		$this->writeRequestClasss($configurations);
		
		$this->addFile('KalturaTypes.js', $this->enumTypes);
		$this->addFile('KalturaModel.js', $this->voClasses);
		$this->addFile('KalturaServices.js', $this->serviceClasses);
		$this->addFile('KalturaClient.js', $this->mainClass);
		$this->addFile('KalturaRequestData.js', $this->requestClasses);
		//write project file (if needed, this can also be included in the static sources folder if not dynamic)
		$this->writeProjectFile();
	}
	
	/**
	 * dump a given text to a given variable and end one line.
	 * @param $addto the parameter to add the text to.
	 * @param $text the text to add.
	 */
	protected function echoLine(&$addto, $text = '')
	{
		$addto .= $text . "\n";
	}
	
	/**
	 * util function to capitalize the first letter of a given text.
	 * @param $wordtxt the text to capitalize.
	 */
	protected function upperCaseFirstLetter($wordtxt)
	{
		if(strlen($wordtxt) > 0)
			$wordtxt = strtoupper(substr($wordtxt, 0, 1)) . substr($wordtxt, 1);
		return $wordtxt;
	}
	
	protected function getClassName($name)
	{
		return preg_replace('/^Kaltura/', '', $name);
	}
	
	private function escapeString($str) 
	{
		return str_replace("'", "\\'", $str);
	}
	
	/**
	 * Parses Enum (aka. types) classes.
	 * @param $enumNode the xml node from the api schema representing an enum.
	 */
	protected function writeEnum(SimpleXMLElement $enumNode)
	{
		$className = $enumNode->attributes()->name;
		if(!$this->shouldIncludeType($className))
			return;
		
		$className = $this->getClassName($className);
		
		$this->echoLine($this->enumTypes, "\nmodule.exports.$className = {");
		//parse the constants
		foreach($enumNode->children() as $child)
		{
			switch($enumNode->attributes()->enumType)
			{
				case 'string':
					$this->echoLine($this->enumTypes, $child->attributes()->name . " : '" . $this->escapeString($child->attributes()->value) . "',");
					break;
				default:
					$this->echoLine($this->enumTypes, $child->attributes()->name . " : " . $child->attributes()->value . ",");
					break;
			}
		}
		$this->echoLine($this->enumTypes, "};");
	}
	
	/**
	 * Parses Object (aka. VO) classes.
	 * @param $classNode the xml node from the api schema representing a type class (Value Object).
	 */
	protected function writeObjectClass(SimpleXMLElement $classNode)
	{
		$className = $classNode->attributes()->name;
		if(!$this->shouldIncludeType($className))
			return;
		
		$clasName = $this->getClassName($className);

		$this->echoLine($this->voClasses, "");
		$this->echoLine($this->voClasses, "/**");
		$this->echoLine($this->voClasses, " *");
		$this->echoLine($this->voClasses, " */");

		if($classNode->attributes()->base)
		{
			$parentClass = $this->getClassName($classNode->attributes()->base);
			$this->echoLine($this->voClasses, "class $clasName extends $parentClass{");
		} 
		else
		{
			$this->echoLine($this->voClasses, "class $clasName extends kaltura.BaseObject{");
		}
		
		$this->echoLine($this->voClasses, "	");
		$this->echoLine($this->voClasses, "	constructor(object = null) {");
		$this->echoLine($this->voClasses, "		super(object);");
		$this->echoLine($this->voClasses, "		this.objectType = '$className';");
		$this->echoLine($this->voClasses, "	}");
		
		//parse the class properties
		foreach($classNode->children() as $classProperty)
		{
			$propType = $classProperty->attributes()->type;
			$propType = $this->getJSType($propType);
			$propName = $classProperty->attributes()->name;
			$methodName = ucfirst($propName);
			
			// to format multi-line descriptions
			$description = array_map('trim', explode("\n", trim($classProperty->attributes()->description, ".\n\t\r "))); 
			$description = implode("\n * ", $description);

			$this->echoLine($this->voClasses, "	");
			$this->echoLine($this->voClasses, "	/**");
			$this->echoLine($this->voClasses, "	 * $description");
			$this->echoLine($this->voClasses, "	 * @return $propType");
			$this->echoLine($this->voClasses, "	 */");
			$this->echoLine($this->voClasses, "	 get{$methodName}() {");
			$this->echoLine($this->voClasses, "	 	return this.$propName;");
			$this->echoLine($this->voClasses, "	 }");
			
			if($classProperty->attributes()->readOnly != '1')
			{
				$this->echoLine($this->voClasses, "	");
				$this->echoLine($this->voClasses, "	/**");
				$this->echoLine($this->voClasses, "	 * @param $propName $propType $description");
				$this->echoLine($this->voClasses, "	 */");
				$this->echoLine($this->voClasses, "	 set{$methodName}($propName) {");
				$this->echoLine($this->voClasses, "	 	this.$propName = $propName;");
				$this->echoLine($this->voClasses, "	 }");
			}
		}

		$this->echoLine($this->voClasses, "}");
		$this->echoLine($this->voClasses, "module.exports.$clasName = $clasName;");
	}
	
	/**
	 * Parses Services and actions (calls that can be performed on the objects).
	 * @param $serviceNodes the xml node from the api schema representing an api service.
	 */
	protected function writeService(SimpleXMLElement $serviceNodes)
	{
		$serviceId = $serviceNodes->attributes()->id;
		if(!$this->shouldIncludeService($serviceId))
			return;
				
		$serviceName = $serviceNodes->attributes()->name;
		$serviceClass = "class $serviceName{\n";
		
		$serviceClassDesc = "\n/**\n";
		$serviceClassDesc .= " *Class definition for the Kaltura service: $serviceName.\n";
		$actionsList = " * The available service actions:\n";
		
		//parse the service actions
		foreach($serviceNodes->children() as $action)
		{
			
			if($action->result->attributes()->type == 'file')
				continue;

			$actionDesc = "	\n";
			$actionDesc .= "	/**\n";
			// to format multi-line descriptions
			$description = array_map('trim', explode("\n", trim($action->attributes()->description, ".\n\t\r "))); 
			$description = implode("\n * ", $description);
			$actionDesc .= "	 * $description.\n";
			$actionsList .= " * @action " . $action->attributes()->name . " $description.\n";
			
			foreach($action->children() as $actionParam)
			{
				if($actionParam->getName() == 'param')
				{
					$paramType = $actionParam->attributes()->type;
					$paramType = $this->getJSType($paramType);
					$paramName = $actionParam->attributes()->name;
					$optionalp = ($actionParam->attributes()->optional == '1');
					$defaultValue = trim($actionParam->attributes()->default);
					$enumType = $actionParam->attributes()->enumType;
					 
					// to format multi-line descriptions
					$description = array_map('trim', explode("\n", trim($actionParam->attributes()->description, ".\n\t\r "))); 
					$description = implode("\n * ", $description);
					
					$info = array();
					if($optionalp)
						$info[] = 'optional';
					if($enumType && $enumType != '')
						$info[] = "enum: $enumType";
					if($defaultValue && $defaultValue != '')
						$info[] = "default: $defaultValue";
					
					$infoTxt = '';
					if(count($info) > 0)
						$infoTxt = ' (' . join(', ', $info) . ')';
					$vardesc = "	 * @param $paramName $paramType {$description}{$infoTxt}";
					$actionDesc .= "$vardesc\n";
				} 
				elseif($actionParam->getName() == 'result' && $actionParam->attributes()->type)
				{
					$rettype = $actionParam->attributes()->type;
					$actionDesc .= "	 * @return $rettype\n";
				}
			}
			
			$actionDesc .= "	 */";
			$actionClass = $actionDesc . "\n";
			
			//create a service action
			$actionName = $action->attributes()->name;
			
			$paramNames = array();
			foreach($action->children() as $actionParam)
			{
				if($actionParam->getName() == 'param')
				{
					$param = $actionParam->attributes()->name;

					if($actionParam->attributes()->optional == '1')
					{
						$defaultValue = 'null';
						switch($actionParam->attributes()->type)
						{
							case 'string':
							case 'float':
							case 'int':
							case 'bigint':
							case 'bool':
							case 'array':
								$defaultValue = strtolower($actionParam->attributes()->default);
								if($defaultValue != 'false' && $defaultValue != 'true' && $defaultValue != 'null' && !is_numeric($defaultValue))
								{
									$defaultValue = "'" . $actionParam->attributes()->default . "'";
								}
								break;
							default: //is Object
								$defaultValue = 'null';
								break;
						}
						$param .= " = $defaultValue";
					}
						
					$paramNames[] = $param;
				}
			}
			$paramNames = join(', ', $paramNames);
			
			// action method signature
			if(in_array($actionName, array('list', 'clone', 'delete', 'export'))) // because list & clone are preserved in PHP
				$actionSignature = $actionName . "Action($paramNames)";
			else
				$actionSignature = $actionName . "($paramNames)";
			
			$actionClass .= "	static $actionSignature{\n";
			$actionClass .= "		let kparams = {};\n";
			
			$haveFiles = false;
			//parse the actions parameters and result types
			foreach($action->children() as $actionParam)
			{
				if($actionParam->getName() != 'param')
					continue;
				$paramName = $actionParam->attributes()->name;
				if($haveFiles === false && $actionParam->attributes()->type == 'file')
				{
					$haveFiles = true;
					$actionClass .= "		let kfiles = {};\n";
				}
				switch($actionParam->attributes()->type)
				{
					case 'file':
						$actionClass .= "		kfiles.$paramName = $paramName;\n";
						break;
					case 'string':
					case 'float':
					case 'int':
					case 'bigint':
					case 'bool':
					case 'array':
					default: //is Object
						$actionClass .= "		kparams.$paramName = $paramName;\n";
						break;
				}
			}
			if($haveFiles)
			{
				$actionClass .= "		return new kaltura.RequestBuilder('$serviceId', '$actionName', kparams, kfiles);\n";
			}
			else
			{
				$actionClass .= "		return new kaltura.RequestBuilder('$serviceId', '$actionName', kparams);\n";
			}
			$actionClass .= "	};";
			$this->echoLine($serviceClass, $actionClass);
		}
		$this->echoLine($serviceClass, "}");
		$this->echoLine($serviceClass, "module.exports.$serviceName = $serviceName;");
		
		$serviceClassDesc .= $actionsList;
		$serviceClassDesc .= " */";
		$serviceClass = $serviceClassDesc . "\n" . $serviceClass;
		$this->echoLine($this->serviceClasses, $serviceClass);
	}
	
	/**
	 * Create the main class of the client library, may parse Services and actions.
	 * initialize the service and assign to client to provide access to servcies and actions through the Kaltura client object.
	 */
	protected function writeMainClass()
	{
		$apiVersion = $this->schemaXml->attributes()->apiVersion;
		$date = date('y-m-d');
		
		$this->echoLine($this->mainClass, "/**");
		$this->echoLine($this->mainClass, " * The Kaltura Client - this is the facade through which all service actions should be called.");
		$this->echoLine($this->mainClass, " * @param config the Kaltura configuration object holding partner credentials (type: KalturaConfiguration).");
		$this->echoLine($this->mainClass, " */");
		$this->echoLine($this->mainClass, "var util = require('util');");
		$this->echoLine($this->mainClass, "var kaltura = require('./KalturaClientBase');");
		$this->echoLine($this->mainClass, "kaltura.services = require('./KalturaServices');");
		$this->echoLine($this->mainClass, "kaltura.objects = require('./KalturaModel');");
		$this->echoLine($this->mainClass, "kaltura.enums = require('./KalturaTypes');");
		$this->echoLine($this->mainClass, "");
		$this->echoLine($this->mainClass, "class Client extends kaltura.ClientBase {");
		$this->echoLine($this->mainClass, "");
		$this->echoLine($this->mainClass, "	/**");
		$this->echoLine($this->mainClass, "	 * @param Configuration config");
		$this->echoLine($this->mainClass, "	 */");
		$this->echoLine($this->mainClass, "	constructor(config) {");
		$this->echoLine($this->mainClass, "		super(config);");
		$this->echoLine($this->mainClass, "		this.setApiVersion('$apiVersion');");
		$this->echoLine($this->mainClass, "		this.setClientTag('node:$date');");
		$this->echoLine($this->mainClass, "	}");
		$this->echoLine($this->mainClass, "}");
		$this->echoLine($this->mainClass, "");
		$this->echoLine($this->mainClass, "module.exports = kaltura;");
		$this->echoLine($this->mainClass, "module.exports.Client = Client;");
		$this->echoLine($this->mainClass, "");
	}

	protected function writeRequestClasss(SimpleXMLElement $configurationNodes)
	{
		$this->echoLine($this->requestClasses, "");
		$this->echoLine($this->requestClasses, "class RequestData {");
		$this->echoLine($this->requestClasses, "");
		$this->echoLine($this->requestClasses, "	constructor() {");
		$this->echoLine($this->requestClasses, "		this.requestData = {};");
		$this->echoLine($this->requestClasses, "	}");
		$this->echoLine($this->requestClasses, "	");
		
		foreach($configurationNodes as $configurationNode)
		{
			/* @var $configurationNode SimpleXMLElement */
			foreach($configurationNode->children() as $configurationProperty => $configurationPropertyNode)
			{
				/* @var $configurationPropertyNode SimpleXMLElement */
		
				if($configurationPropertyNode->attributes()->volatile)
				{
					continue;
				}
		
				$type = $configurationPropertyNode->attributes()->type;
				$description = null;
		
				if($configurationPropertyNode->attributes()->description)
				{
					$description = $configurationPropertyNode->attributes()->description;
				}
		
				$this->writeConfigurationProperty($configurationProperty, $configurationProperty, $type, $description);
		
				if($configurationPropertyNode->attributes()->alias)
				{
					$this->writeConfigurationProperty($configurationPropertyNode->attributes()->alias, $configurationProperty, $type, $description);
				}
			}
		}

		$this->echoLine($this->requestClasses, "}");
		$this->echoLine($this->requestClasses, "");
		
		$this->echoLine($this->requestClasses, "class VolatileRequestData extends RequestData {");
		$this->echoLine($this->requestClasses, "");
		
		foreach($configurationNodes as $configurationNode)
		{
			/* @var $configurationNode SimpleXMLElement */
			foreach($configurationNode->children() as $configurationProperty => $configurationPropertyNode)
			{
				/* @var $configurationPropertyNode SimpleXMLElement */
		
				if($configurationPropertyNode->attributes()->volatile)
				{
					$type = $configurationPropertyNode->attributes()->type;
					$description = null;
			
					if($configurationPropertyNode->attributes()->description)
					{
						$description = $configurationPropertyNode->attributes()->description;
					}
			
					$this->writeConfigurationProperty($configurationProperty, $configurationProperty, $type, $description);
			
					if($configurationPropertyNode->attributes()->alias)
					{
						$this->writeConfigurationProperty($configurationPropertyNode->attributes()->alias, $configurationProperty, $type, $description);
					}
				}
			}
		}
		
		$this->echoLine($this->requestClasses, "}");
		$this->echoLine($this->requestClasses, "");
		
		$this->echoLine($this->requestClasses, "module.exports = {");
		$this->echoLine($this->requestClasses, "	RequestData: RequestData,");
		$this->echoLine($this->requestClasses, "	VolatileRequestData: VolatileRequestData,");
		$this->echoLine($this->requestClasses, "};");
		$this->echoLine($this->requestClasses, "");
	}
	
	protected function writeConfigurationProperty($name, $paramName, $type, $description)
	{
		$methodsName = ucfirst($name);
		
		$this->echoLine($this->requestClasses, "	/**");
		if($description)
		{
			$this->echoLine($this->requestClasses, "	 * $description");
			$this->echoLine($this->requestClasses, "	 * ");
		}
		$this->echoLine($this->requestClasses, "	 * @param $type $name");
		$this->echoLine($this->requestClasses, "	 */");
		$this->echoLine($this->requestClasses, "	set{$methodsName}($name){");
		$this->echoLine($this->requestClasses, "		this.requestData['{$paramName}'] = {$name};");
		$this->echoLine($this->requestClasses, "	};");
		$this->echoLine($this->requestClasses, "	");
	
		
		$this->echoLine($this->requestClasses, "	/**");
		if($description)
		{
			$this->echoLine($this->requestClasses, "	 * $description");
			$this->echoLine($this->requestClasses, "	 * ");
		}
		$this->echoLine($this->requestClasses, "	 * @return $type");
		$this->echoLine($this->requestClasses, "	 */");
		$this->echoLine($this->requestClasses, "	get{$methodsName}(){");
		$this->echoLine($this->requestClasses, "		return this.requestData['{$paramName}'];");
		$this->echoLine($this->requestClasses, "	};");
		$this->echoLine($this->requestClasses, "	");
	}
	
	/**
	 * Create the project file (when needed).
	 */
	protected function writeProjectFile()
	{
		//override to implement the parsing and file creation.
	//to add a new file, use: $this->addFile('path to new file', 'file contents');
	//echo "Create Project File.\n";
	}
	
	public function getJSType($propType)
	{
		switch($propType)
		{
			case 'bigint':
				return 'int';
			
			default:
				return $this->getClassName($propType);
		}
	}
}
