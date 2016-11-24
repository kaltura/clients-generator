<?php
CONST NewLine = "\n";

require_once (__DIR__ . '/ng2-typescript/GeneratedFileData.php');
require_once (__DIR__ . '/ng2-typescript/ServerMetadata.php');
require_once (__DIR__ . '/ng2-typescript/GeneratorBase.php');
require_once (__DIR__ . '/ng2-typescript/ServicesGenerator.php');
require_once (__DIR__ . '/ng2-typescript/ServiceActionsGenerator.php');



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


		// dump schema as json for diagnostics
		$this->addFile("services-schema.json", json_encode($this->serverMetadata,JSON_PRETTY_PRINT),false);

		$files = array_merge(
			(new ServicesGenerator($this->serverMetadata))->generate(),
			(new ServiceActionsGenerator($this->serverMetadata))->generate()
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
//		$classNodes = $xpath->query ( "/xml/classes/class" );
//		foreach ( $classNodes as $classNode )
//		{
//			$this->writeClass ( $classNode );
//		}
//


		//$this->createServices();
		//$this->createServiceActions();

//
//		$configurationNodes = $xpath->query("/xml/configurations/*");
//		$this->writeMainClient($serviceNodes, $configurationNodes);
	}


	public function extractData($xpath)
	{
		$serviceNodes = $xpath->query("/xml/services/service");
		foreach ($serviceNodes as $serviceNode) {
			if ($this->shouldIncludeService($serviceNode->getAttribute("id"))) {
				$arrayData = new Service;
				$this->serverMetadata->services[] = $arrayData;

				$arrayData->name = $serviceNode->getAttribute("name");
				$arrayData->id = $serviceNode->getAttribute("id");
				$arrayData->description = $serviceNode->getAttribute("description");
				$arrayData->actions = $this->extractServiceActionsData($serviceNode);
			}
		}
	}

	function extractServiceActionsData(DOMElement $serviceNode)
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
			$serviceAction->resultType = "";
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
						$paramData->type = $this->mapToTypescriptType($child->getAttribute("type"), $child);
						$paramData->enumType = $child->getAttribute("enumType");
						$paramData->default = $child->getAttribute("default");
						$paramData->optional = $child->getAttribute("optional") == "1";

						if (!$paramData->type) {

							throw new Exception("Failed to extract information for service {$serviceName} / action {$serviceAction->name} / param {$paramData->name}");
						}
						break;
					case 'result':
						$serviceAction->resultType = $this->mapToTypescriptType($child->getAttribute("type"), $child);

						if (!$serviceAction->resultType) {

							if ($child->hasAttribute("type")) {
								throw new Exception("Failed to extract information for service {$serviceName} / action {$serviceAction->name} / result type");
							}else
							{
								$serviceAction->resultType = "void";
							}
						}
						break;
				}
			}

		}

		return $result;
	}

	function mapToTypescriptType($type, $xmlnode)
	{
		$result = $type;
		if ($this->isArrayType($type))
		{
			$result = $xmlnode->getAttribute("arrayType") . "[]";
		}else
		{
			switch($type)
			{
				case "file":
					// TODO
					break;
				case "bool":
					$result = "boolean";
					break;
				case "int":
					$enumType = $xmlnode->getAttribute("arrayType");
					if ($enumType)
					{
						$result = $enumType;
					}else
					{
						$result = "number";
					}
					break;
			}
		}

		return $result;
	}
}
