<?php
CONST NewLine = "\n";

require_once (__DIR__ . '/ng2-typescript/GeneratedFileData.php');
require_once (__DIR__ . '/ng2-typescript/KalturaServerMetadata.php');
require_once (__DIR__ . '/ng2-typescript/ServiceActionsGenerator.php');
require_once (__DIR__ . '/ng2-typescript/ClassesGenerator.php');
require_once (__DIR__ . '/ng2-typescript/KalturaObjectsGenerator.php');
require_once (__DIR__ . '/ng2-typescript/KalturaBaseRequestGenerator.php');
require_once (__DIR__ . '/ng2-typescript/EnumsGenerator.php');



class NG2TypescriptClientGenerator extends ClientGeneratorFromXml
{
	protected $_baseClientPath = "src";
	protected $_usePrivateAttributes;
	private $serverMetadata;

	function __construct($xmlPath, Zend_Config $config, $sourcePath = "ng2-typescript")
	{
		parent::__construct($xmlPath, $sourcePath, $config);
		$this->_usePrivateAttributes = isset($config->usePrivateAttributes) ? $config->usePrivateAttributes : false;
	}



	public function generate()
	{
		parent::generate();

		// Convert xml strcuture to plain old php objects
		$xpath = new DOMXPath ($this->_doc);
		$this->serverMetadata = $this->extractData($xpath);

		$files = array_merge(
			(new ServiceActionsGenerator($this->serverMetadata))->generate(),
			(new ClassesGenerator($this->serverMetadata))->generate(),
			(new KalturaObjectsGenerator($this->serverMetadata))->generate(),
			(new KalturaBaseRequestGenerator($this->serverMetadata))->generate(),
			(new EnumsGenerator($this->serverMetadata))->generate()
		);

		foreach($files as $file)
		{
			$this->addFile($this->_baseClientPath . "/" . $file->path, $file->content);
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
				$classType->base = $this->fixKalturaTypeName($classNode->getAttribute("base"));
				$classType->plugin = $classNode->getAttribute("plugin");
				$classType->description = $classNode->getAttribute("description");
				$classType->properties = $this->extractClassTypeProperties($classNode);

				$result->classTypes[$classType->name] = $classType;
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

		$requestConfigurationNodes = $xpath->query("/xml/configurations/request");


		$item = new stdClass();
		$result->requestSharedParameters["service"] = $item;
		$item->name = "service";
		$item->readonly = false;
		$item->defaultValue = "";
		$item->description = "";
		$item->transparentToUser = true;
		$item->optional = false;
		$item->type = KalturaServerTypes::Simple;
		$item->typeClassName = "string";

		$item = new stdClass();
		$result->requestSharedParameters["apiVersion"] = $item;
		$item->name = "apiVersion";
		$item->readonly = true;
		$item->defaultValue = $xpath->query("/xml")->item(0)->getAttribute('apiVersion');
		$item->description = "";
		$item->transparentToUser = true;
		$item->optional = false;
		$item->type = KalturaServerTypes::Simple;
		$item->typeClassName = "string";

		$item = new stdClass();
		$result->requestSharedParameters["action"] = $item;
		$item->name = "action";
		$item->readonly = false;
		$item->defaultValue = "";
		$item->description = "";
		$item->transparentToUser = true;
		$item->optional = false;
		$item->type = KalturaServerTypes::Simple;
		$item->typeClassName = "string";

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
				$item->transparentToUser = false;
				$item->optional = true;
				$item->readonly = false;
				$item->defaultValue = "";
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
		$this->addFile("services-schema.json", json_encode($result,JSON_PRETTY_PRINT),false);


		$errorsCount = count($errors);
		if ( $errorsCount != "0") {
			$errorMessage = "Parsing from xml failed with {$errorsCount} error(s):" . NewLine . join(NewLine, $errors);
			throw new Exception($errorMessage);
		}

		return $result;
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
			$propertyItem->readOnly = $propertyNode->getAttribute("name") == "1";
			$propertyItem->insertOnly = $propertyNode->getAttribute("name") == "1";

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
		}else if ($this->isArrayType($typeValue))
		{
			$arrayTypeValue = $this->fixKalturaTypeName($xmlnode->getAttribute("arrayType"));
			if (isset($arrayTypeValue) && $arrayTypeValue != "") {
				$result->type = KalturaServerTypes::ArrayOfObjects;
				$result->className = $arrayTypeValue;
			}
		}else
		{
			switch($typeValue)
			{
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
//
//					if ($isTime == "1")
//					{
//						$result->type = KalturaServerTypes::Date;
//						$result->className = "";
//					}else
						if ($enumType)
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
		$unsorted = [];
		$sorted = [];

		// Move any non-edged nodes to the unsorted list.
		// Nodes without edges should be run before any other
		// nodes, in no particular order.
		for ($i=count($classTypes)-1; $i>=0; $i--) {
			if ($classTypes[$i]->base == "") {			// Non-arrays are unedged vertices
				array_push($unsorted, array_splice($classTypes, $i, 1)[0]);
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
					array_push($unsorted, array_splice($classTypes, $i, 1)[0]);
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
