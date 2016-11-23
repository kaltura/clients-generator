<?php
CONST NewLine = "\n";

class Service
{
	public $id;
	public $name;
	public $description;
	public $actions = array();
}

class ServiceAction
{
	public $name;
	public $resultType;
	public $params = array();
	public $description;
	public $enableInMultiRequest = 1;
}

class ServiceActionParam
{
	public $name;
	public $type;
	public $optional;
	public $default;
}

class NG2TypescriptClientGenerator extends ClientGeneratorFromXml
{
	private $_csprojIncludes = array();
	protected $_baseClientPath = "src";
	protected $_usePrivateAttributes;

	private $services = array();
	private $enums = array();
	private $types = array();


	function __construct($xmlPath, Zend_Config $config, $sourcePath = "ng2-typescript")
	{
		parent::__construct($xmlPath, $sourcePath, $config);
		$this->_usePrivateAttributes = isset($config->usePrivateAttributes) ? $config->usePrivateAttributes : false;
	}

	public function generate()
	{
		parent::generate();

		$xpath = new DOMXPath ($this->_doc);
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
		$this->extractData();

		$this->addFile("services-schema.json", json_encode($this->services,JSON_PRETTY_PRINT),false);

		$this->createServices();

//
//		$configurationNodes = $xpath->query("/xml/configurations/*");
//		$this->writeMainClient($serviceNodes, $configurationNodes);
	}

	/////////////////////////////////////////////////////////////
	//Private functions
	/////////////////////////////////////////////////////////////

	function toLispCase($input)
	{
		preg_match_all('!([A-Z][A-Z0-9]*(?=$|[A-Z][a-z0-9])|[A-Za-z][a-z0-9]+)!', $input, $matches);
		$ret = $matches[0];
		foreach ($ret as &$match) {
			$match = $match == strtoupper($match) ? strtolower($match) : lcfirst($match);
		}
		return implode('-', $ret);
	}

	function toSnakeCase($input)
	{
		preg_match_all('!([A-Z][A-Z0-9]*(?=$|[A-Z][a-z0-9])|[A-Za-z][a-z0-9]+)!', $input, $matches);
		$ret = $matches[0];
		foreach ($ret as &$match) {
			$match = $match == strtoupper($match) ? strtolower($match) : lcfirst($match);
		}
		return implode('_', $ret);
	}

	// General functions
	private function createDocumentationExp($spacer, $documentation)
	{
		if ($documentation) {
			return "/** " . NewLine . "{$spacer}* " . wordwrap(str_replace(array("\t", "\n", "\r"), " ", $documentation), 80, NewLine ."{$spacer}* ") . NewLine . "{$spacer}**/";
		}
		return "";
	}

	private function getBanner()
	{
//		$currentFile = $_SERVER ["SCRIPT_NAME"];
//		$parts = Explode('/', $currentFile);
//		$currentFile = $parts [count($parts) - 1];
//
		$banner = "";
//		$banner .= "/**\n";
//		$banner .= " * This class was auto generated using $currentFile\n";
//		$banner .= " * against an XML schema provided by Kaltura.\n";
//		$banner .= " * \n";
//		$banner .= " * MANUAL CHANGES TO THIS CLASS WILL BE OVERWRITTEN.\n";
//		$banner .= " */\n";

		return $banner;
	}


	/////////////////////////////////////////////////////////////
	// Extract data functions
	/////////////////////////////////////////////////////////////


	function extractData()
	{
		$xpath = new DOMXPath ($this->_doc);

		$serviceNodes = $xpath->query("/xml/services/service");
		foreach ($serviceNodes as $serviceNode) {
			if ($this->shouldIncludeService($serviceNode->getAttribute("id"))) {
				$arrayData = new Service;
				$this->services[] = $arrayData;

				$arrayData->name = $this->upperCaseFirstLetter($serviceNode->getAttribute("name"));
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

	/////////////////////////////////////////////////////////////
	//Service functions
	/////////////////////////////////////////////////////////////



	function createServices()
	{
		foreach ($this->services as $service) {
			$this->createService($service);
		}
	}

	function createService(Service $serviceData)
	{
		$serviceName = $this->upperCaseFirstLetter($serviceData->name);
		$actionsFilePath = "./{$this->toLispCase($serviceName)}-actions.ts";
		$desc = $serviceData->description;

		$serviceActionsContent = $this->createServiceActionsContent($serviceData);
		$serviceActionsContentExp = join(NewLine, $serviceActionsContent);

		$imports = $this->getBanner() . NewLine;
		$imports .= "import { Injectable } from '@angular/core';
import * as actions from \"{$actionsFilePath}\";


{$this->createDocumentationExp('',$desc)}
export class {$serviceName}Service {

    constructor(){
        throw new Error('This class should not be initialized (you should use its static functions to create new requests)');
    }

	{$serviceActionsContentExp}
}";

		$file = $this->_baseClientPath . "/services/{$this->toLispCase($serviceName)}.service.ts";
		$this->addFile($file, $imports);
	}


	function createServiceActionsContent($serviceData)
	{
		$result = array();

		foreach ($serviceData->actions as $actionData) {

			if (!($actionData instanceof ServiceAction)) {
				continue;
			}

			$requiredParams = array();
			$optionalParams = array();
			$actionConstuctorParams = array();

			foreach ($actionData->params as $actionParam) {
				if (!($actionParam instanceof ServiceActionParam)) {
					continue;
				}

				if ($actionParam->optional == true) {
					$optionalParams[] = "{$actionParam->name}? : {$actionParam->type}";
				} else {
					$requiredParams[] = "{$actionParam->name} : {$actionParam->type}";
					$actionConstuctorParams[] = "{$actionParam->name}";
				}

			}

			$functionParams = array_merge($requiredParams);

			if (count($optionalParams)) {
				$functionParams[] = "additional? : { " . join(', ', $optionalParams) . " }";
				$actionConstuctorParams[] = 'additional';
			}

			$functionParamsExp = join(', ', $functionParams);
			$actionConstuctorParamsExp = join(', ', $actionConstuctorParams);
			$actionResultType = "{$serviceData->name}" . ucwords($actionData->name) . "Builder";
			$functionName = lcfirst($actionData->name);

			$result[] = "
	{$this->createDocumentationExp('	', $actionData->description)}
	static {$functionName}({$functionParamsExp}) : {$actionResultType}
	{
		return new actions.{$actionResultType}({$actionConstuctorParamsExp});
	}";
		}

		return $result;
	}

}
