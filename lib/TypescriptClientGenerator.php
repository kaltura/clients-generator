<?php
CONST NewLine = "\n";

class TypescriptClientGenerator extends ClientGeneratorFromXml
{
	protected $_baseClientPath = "src/api";
	protected $_usePrivateAttributes;
	private $serverMetadata;
	private $_serverType = null;
	private $_disableDateParsing = false;
    private $_framework;
    private $_targetKalturaServer;

	function __construct($xmlPath, Zend_Config $config, $framework = "typescript")
	{
		parent::__construct($xmlPath, $framework, $config);

		$this->setAdditionalSourcesPath('shared/typescript-ngx');
		$this->_usePrivateAttributes = isset($config->usePrivateAttributes) ? $config->usePrivateAttributes : false;
		$this->_framework = $framework;
		KalturaLog::info("typescript generator: setting target client framework to '$framework'");
	}

    public function setTestsPath($testsDir)
    {
        parent::setTestsPath($testsDir);
        $this->_serverType = $testsDir;
        $this->_targetKalturaServer = $testsDir;
    }


	public function generate()
	{
		$this->_disableDateParsing = $this->getParam('disableDateParsing') ? true : false;

		if ($this->_disableDateParsing)
		{
			KalturaLog::info("typescript generator: disable date parsing feature");
		}

		parent::generate();

		// Convert xml strcuture to plain old php objects
		$xpath = new DOMXPath ($this->_doc);
		$this->serverMetadata = $this->extractData($xpath);

		$classesGenerator = new ClassesGenerator($this->serverMetadata, $this->_framework, $this->_disableDateParsing, $this->_targetKalturaServer);
		$enumsGenerator = new EnumsGenerator($this->serverMetadata);
		$files = array_merge(
			$classesGenerator->generate(),
			$enumsGenerator->generate()
		);

		foreach($files as $file)
		{
			$this->addFile($this->_baseClientPath . "/" . $file->path, $file->content,false);
		}
	}


	public function extractData($xpath)
	{
		$result = new KalturaServerMetadata();
		$errors = array();

		$serviceNodes = $xpath->query("/xml/services/service");
		foreach ($serviceNodes as $serviceNode) {
			if ($this->shouldIncludeService($serviceNode->getAttribute("id"))) {
				$service = new Service;
				$result->services[] = $service;

				$service->name = $serviceNode->getAttribute("name");
				$service->id = $serviceNode->getAttribute("id");
				$service->description = $serviceNode->getAttribute("description");
				$service->actions = $this->extractServiceActions($serviceNode);
			}
		}

		$classNodes = $xpath->query ( "/xml/classes/class" );
		foreach ( $classNodes as $classNode )
		{
			$className = $classNode->getAttribute("name");

			if ($this->shouldIncludeType($className) && $className != "KalturaClientConfiguration") {

				if ($className == "KalturaServerObject")
				{
					$errors[] = "Class name 'KalturaServerObject' cannot be generated";
				}

				$classType = new ClassType();

				$classType->name = $this->fixKalturaTypeName($classNode->getAttribute("name"));
				$classType->abstract = $classNode->getAttribute("abstract") == 1;
				$classType->base = $this->fixKalturaTypeName($classNode->getAttribute("base"));
				$classType->plugin = $classNode->getAttribute("plugin");
				$classType->description = $classNode->getAttribute("description");
				$classType->properties = $this->extractClassTypeProperties($classNode);

				$result->classTypes[] = $classType;
			}
		}

		// TODO - should adjust logic to sort also by properties types dependencies (not only by inheritance)
		//$result->classTypes = $this->sortClassesByDependencies($result->classTypes);

		$enumNodes = $xpath->query ( "/xml/enums/enum" );
		foreach ( $enumNodes as $enumNode ) {

			if ($this->shouldIncludeType($enumNode->getAttribute("name"))) {
				$enumType = new EnumType();
				$result->enumTypes[] = $enumType;

				$enumType->name = $this->fixKalturaTypeName($enumNode->getAttribute("name"));
				$enumType->type = $enumNode->getAttribute("enumType");

				$enumValueNodes = $enumNode->childNodes;
				foreach ($enumValueNodes as $enumValueNode) {
					if ($enumValueNode->nodeName == 'const') {
						$enumType->values[] = new EnumValue($enumValueNode->getAttribute('name'), $enumValueNode->getAttribute('value'));
					}
				}
			}
		}


		$result->apiVersion = $xpath->query("/xml")->item(0)->getAttribute('apiVersion');


		$requestConfigurationNodes = $xpath->query("/xml/configurations/request");

		foreach($requestConfigurationNodes as $requestConfigurationNode)
		{
			$children = $requestConfigurationNode->childNodes;
			foreach ($children as $childrenNode) {
				if ($childrenNode->nodeType != XML_ELEMENT_NODE) {
					continue;
				}
				$item = new stdClass();
				$result->requestSharedParameters[$childrenNode->nodeName] = $item;
				$item->name = $childrenNode->nodeName;
				$item->optional = true;
				$item->readOnly = false;
				$item->default = "";
				$item->description = $childrenNode->getAttribute("description");
				$itemTypeData = $this->mapToTypescriptType($childrenNode,false);
				$item->type = $itemTypeData->type;
				$item->typeClassName = $itemTypeData->className;
			}
		}

		$errors = array_merge(
			$errors,
			$result->prepare()
		);

		// dump schema as json for diagnostics
		$this->addFile("services-schema.json", self::json_readable_encode($result),false);


		$errorsCount = count($errors);
		if ( $errorsCount != "0") {
			$errorMessage = "Parsing from xml failed with {$errorsCount} error(s):" . NewLine . join(NewLine, $errors);
			throw new Exception($errorMessage);
		}

		return $result;
	}

	static function json_readable_encode($in, $indent_string = "    ", $indent = 0, Closure $_escape = null)
	{
	    if (version_compare(PHP_VERSION, '5.4.0') >= 0) {
	      $ret = json_encode($in, JSON_PRETTY_PRINT|JSON_UNESCAPED_SLASHES|JSON_UNESCAPED_UNICODE);
	      $ret = preg_replace("/\[\s+\]/", "", $ret);
	      $ret = preg_replace("/\{\s+\}/", "", $ret);
	    }
	    if (__CLASS__ && isset($this))
	    {
		$_myself = array($this, __FUNCTION__);
	    }
	    elseif (__CLASS__)
	    {
		$_myself = array('self', __FUNCTION__);
	    }
	    else
	    {
		$_myself = __FUNCTION__;
	    }
	    if (is_null($_escape))
	    {
		$_escape = function ($str)
		{
		    return str_replace(
			array('\\', '"', "\n", "\r", "\b", "\f", "\t", '\\\\u'),
			array('\\\\', '\\"', "\\n", "\\r", "\\b", "\\f", "\\t", '\\u'),
			$str);
		};
	    }
	    $out = '';
	    // TODO: format value (unicode, slashes, ...)
	    if((!is_array($in)) && (!is_object($in)))
	      return json_encode($in);
	    // see http://stackoverflow.com/a/173479
	    if(is_array($in)){
		$is_assoc = array_keys($in) !== range(0, count($in) -1);
	    }else{
		$is_assoc = get_object_vars($in);
	    }
	    foreach ($in as $key=>$value)
	    {
		if($is_assoc) {
		  $out .= str_repeat($indent_string, $indent + 1);
		  $out .= "\"".$_escape((string)$key)."\": ";
		}
		else {
		  $out .= str_repeat($indent_string, $indent + 1);
		}
		if ((is_object($value) || is_array($value)) && (!count($value))) {
		    $out .= "[]";
		}
		elseif (is_object($value) || is_array($value))
		{
		    $out .= call_user_func($_myself, $value, $indent_string, $indent + 1, $_escape);
		}
		elseif (is_bool($value))
		{
		    $out .= $value ? 'true' : 'false';
		}
		elseif (is_null($value))
		{
		    $out .= 'null';
		}
		elseif (is_string($value))
		{
		    $out .= "\"" . $_escape($value) ."\"";
		}
		else
		{
		    $out .= $value;
		}
		$out .= ",\n";
	    }
	    if (!empty($out))
	    {
		$out = substr($out, 0, -2);
	    }
	    if($is_assoc) {
	      $out =  "{\n" . $out;
	      $out .= "\n" . str_repeat($indent_string, $indent) . "}";
	    }
	    else {
	      $out = "[\n" . $out;
	      $out .= "\n" . str_repeat($indent_string, $indent) . "]";
	    }
	    return $out;
	}

	function extractClassTypeProperties(DOMElement $classNode)
	{
		$result = array();

		$propertyNodes = $classNode->childNodes;
		foreach ($propertyNodes as $propertyNode) {
			if ($propertyNode->nodeType != XML_ELEMENT_NODE || $propertyNode->nodeName !== 'property')
				continue;

			$propertyItem = new ClassTypeProperty();
			$propertyItem->name = $propertyNode->getAttribute("name");
			$propertyItem->writeOnly = $propertyNode->getAttribute("writeOnly") == "1";
			$propertyItem->readOnly = $propertyNode->getAttribute("readOnly") == "1";
			$propertyItem->insertOnly = $propertyNode->getAttribute("insertOnly") == "1";

			$itemTypeData = $this->mapToTypescriptType($propertyNode,false);
			$propertyItem->type = $itemTypeData->type;
			$propertyItem->typeClassName = $itemTypeData->className;
			$propertyItem->description = $propertyNode->getAttribute("description");

			$result[] = $propertyItem;
		}

		return $result;
	}

	function fixKalturaTypeName($name)
	{
		return $name;
		//return $name == "KalturaObjectBase" ? "KalturaObjectBase" : preg_replace('/^Kaltura/', '',$name);
	}

	function extractServiceActions(DOMElement $serviceNode)
	{
		$serviceName = $serviceNode->getAttribute("name");

		$result = array();

		$actionNodes = $serviceNode->childNodes;
		foreach ($actionNodes as $actionNode) {
			if ($actionNode->nodeType != XML_ELEMENT_NODE)
				continue;

			$serviceAction = new ServiceAction();
			$serviceAction->name = $actionNode->getAttribute("name");
			$serviceAction->params = array();
			$serviceAction->resultClassName = "";
			$serviceAction->description = $actionNode->getAttribute("description");

			$result[] = $serviceAction;


			foreach ($actionNode->childNodes as $child) {
				if ($child->nodeType != XML_ELEMENT_NODE)
					continue;

				switch ($child->nodeName) {
					case 'param':

						$paramData = new ServiceActionParam();
						$serviceAction->params[] = $paramData;

						$paramData->name = $child->getAttribute("name");
						$itemTypeData = $this->mapToTypescriptType($child, false);
						$paramData->type = $itemTypeData->type;
						$paramData->typeClassName = $itemTypeData->className;
						$paramData->optional = $child->getAttribute("optional") == "1";

						$paramData->default = $child->getAttribute("default");
						break;
					case 'result':
						$itemTypeData = $this->mapToTypescriptType($child,true);
						$serviceAction->resultType = $itemTypeData->type;
						$serviceAction->resultClassName = $itemTypeData->className;
						break;
				}
			}
		}


		return $result;
	}

	function mapToTypescriptType(DOMElement $xmlnode, $allowEmptyTypes)
	{
		$result = new stdClass();
		$result->type = KalturaServerTypes::Unknown;
		$result->className = null;

		$typeValue = $xmlnode->hasAttribute("type") ? $xmlnode->getAttribute("type") : null;

		if (!$typeValue || $typeValue == "")
		{
			if ($allowEmptyTypes)
			{
				$result->type = KalturaServerTypes::Void;
				$result->className =  null;
			}
		}else
		{
			switch($typeValue)
			{
			    case "array":
			        $arrayTypeValue = $this->fixKalturaTypeName($xmlnode->getAttribute("arrayType"));
                    if (isset($arrayTypeValue) && $arrayTypeValue != "") {
                        $result->type = KalturaServerTypes::ArrayOfObjects;
                        $result->className = $arrayTypeValue;
                    }
			        break;
                case "map":
                    $arrayTypeValue = $this->fixKalturaTypeName($xmlnode->getAttribute("arrayType"));
                    if (isset($arrayTypeValue) && $arrayTypeValue != "") {
                        $result->type = KalturaServerTypes::MapOfObjects;
                        $result->className = $arrayTypeValue;
                    }
			        break;
				case "file":
					$result->type = KalturaServerTypes::File;
					$result->className = null;
					break;
				case "bigint":
				case "float":
				case "bool":
					$result->type = KalturaServerTypes::Simple;
					$result->className = $typeValue;
					break;
				case "string":
				case "int":
					$enumType = $xmlnode->getAttribute("enumType");
					$isTime = $xmlnode->getAttribute("isTime");

					if ($isTime == "1" && $this->_disableDateParsing === false)
					{
						$result->type = KalturaServerTypes::Date;
						$result->className = "";
					}else if ($enumType)
					{
						if ($typeValue == "string")
						{
							$result->type = KalturaServerTypes::EnumOfString;
						}else
						{
							$result->type = KalturaServerTypes::EnumOfInt;
						}

						$result->className = $this->fixKalturaTypeName($enumType);

					}else
					{
						$result->type = KalturaServerTypes::Simple;
						$result->className = $typeValue;
					}
					break;
				default:
					$result->type = KalturaServerTypes::Object;
					$result->className = $this->fixKalturaTypeName( $typeValue);
					break;
			}
		}

		return $result;
	}

	function sortClassesByDependencies(array $classTypes)
	{
		// Create empty lists for sorted and unsorted vertices/edges
		$unsorted = array();
		$sorted = array();

		// Move any non-edged nodes to the unsorted list.
		// Nodes without edges should be run before any other
		// nodes, in no particular order.
		for ($i=count($classTypes)-1; $i>=0; $i--) {
			if ($classTypes[$i]->base == "") {			// Non-arrays are unedged vertices
				$spliced=array_splice($classTypes, $i, 1);
				array_push($unsorted, $spliced[0]);
			}
		}

		// While there are vertices left to sort
		while(count($unsorted)) {

			// pull the first and push it on to the sorted list
			$item = array_shift($unsorted);
			array_push($sorted, $item);

			// loop backwards through remaining contexts
			for ($i=count($classTypes)-1; $i>=0; $i--) {
				// move nodes whose incoming edge has been moved to sorted
				// to the unsorted list
				if($classTypes[$i]->base == $item->name) {
					$spliced=array_splice($classTypes, $i, 1);
					array_push($unsorted, $spliced[0]);
				}
			}
		}

		if (count($classTypes) != 0)
		{
			throw new Exception("circular dependency detected between class types. First invalid item named {$classTypes[0]->name}");
		}

		return $sorted;
	}
}
