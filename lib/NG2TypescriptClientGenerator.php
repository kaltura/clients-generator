<?php
CONST NewLine = "\n";

require_once (__DIR__ . '/ng2-typescript/GeneratedFileData.php');
require_once (__DIR__ . '/ng2-typescript/ServerMetadata.php');
require_once (__DIR__ . '/ng2-typescript/ServicesGenerator.php');
require_once (__DIR__ . '/ng2-typescript/ServiceActionsGenerator.php');
require_once (__DIR__ . '/ng2-typescript/ClassTypesGenerator.php');



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
		$this->serverMetadata = new ServerMetadata();
		$xpath = new DOMXPath ($this->_doc);
		$this->extractData($xpath);

		$files = array_merge(
			(new ServicesGenerator($this->serverMetadata))->generate(),
			(new ServiceActionsGenerator($this->serverMetadata))->generate(),
			(new ClassTypesGenerator($this->serverMetadata))->generate()
		);

		foreach($files as $file)
		{
			$this->addFile($this->_baseClientPath . "/" . $file->path, $file->content);
		}


//		$enumNodes = $xpath->query ( "/xml/enums/enum" );
//		foreach ( $enumNodes as $enumNode )
//		{
//			$this->writeEnum ( $enumNode );
//		}
//

//


		//$this->createServices();
		//$this->createServiceActions();

//
//		$configurationNodes = $xpath->query("/xml/configurations/*");
//		$this->writeMainClient($serviceNodes, $configurationNodes);
	}


	public function extractData($xpath)
	{
		$errors = array();

		$serviceNodes = $xpath->query("/xml/services/service");
		foreach ($serviceNodes as $serviceNode) {
			if ($this->shouldIncludeService($serviceNode->getAttribute("id"))) {
				$serviceItem = new Service;
				$this->serverMetadata->services[] = $serviceItem;

				$serviceItem->name = $serviceNode->getAttribute("name");
				$serviceItem->id = $serviceNode->getAttribute("id");
				$serviceItem->description = $serviceNode->getAttribute("description");
				$serviceItem->actions = $this->extractServiceActions($serviceNode);

				$errors = array_merge($errors, $serviceItem->validate());
			}
		}

//		$classNodes = $xpath->query ( "/xml/classes/class" );
//		foreach ( $classNodes as $classNode )
//		{
//			if ($this->shouldIncludeType($classNode->getAttribute("name"))) {
//				$classTypeItem = new ClassType();
//				$this->serverMetadata->classTypes[] = $classTypeItem;
//
//				$classTypeItem->name = $classNode->getAttribute("name");
//				$classTypeItem->base = $classNode->getAttribute("base");
//				$classTypeItem->plugin = $classNode->getAttribute("plugin");
//				$classTypeItem->description = $classNode->getAttribute("description");
//				$classTypeItem->properties = $this->extractClassTypeProperties($classNode);
//
//				$errors = array_merge($errors, $classTypeItem->validate());
//
//			}
//		}


		// dump schema as json for diagnostics
		$this->addFile("services-schema.json", json_encode($this->serverMetadata,JSON_PRETTY_PRINT),false);


		$errorsCount = count($errors);
		if ( $errorsCount != "0") {
			$errorMessage = "Parsing from xml failed with {$errorsCount} error(s):" . NewLine . join(NewLine, $errors);
			throw new Exception($errorMessage);
		}
	}

	function extractClassTypeProperties(DOMElement $classNode)
	{
		$className = $classNode->getAttribute("name");

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
			$arrayTypeValue = $xmlnode->getAttribute("arrayType");
			if (isset($arrayTypeValue) && $arrayTypeValue != "") {
				$result->type = KalturaServerTypes::ArrayObject;
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
				case "string":
					$result->type = KalturaServerTypes::Simple;
					$result->className = $typeValue;
					break;
				case "int":
					$enumType = $xmlnode->getAttribute("enumType");
					
					if ($enumType)
					{
						$result->type = KalturaServerTypes::Enum;
						$result->className = $enumType;

					}else
					{
						$result->type = KalturaServerTypes::Simple;
						$result->className = "int";
					}
					break;
				default:
					$result->type = KalturaServerTypes::Object;
					$result->className = $typeValue;
					break;
			}
		}

		return $result;
	}
}
