<?php
class Classes {
	public $className;
	public $parentClass;
	public $isAbstract = false;
	public $properties = array();
}
class Property {
	public $type;
	public $pureType;
	public $name;
	public $json;
	public $isEnumImport = false;
	public $enumPackage = null;
	public $enumImport;
}
class GoClientGenerator extends ClientGeneratorFromXml
{
	
	private $_csprojIncludes = array(); 
	private $_classInheritance = array();
	private $_enums = array();
	private $_allClasses = array();
	private $_allContainers = array();

	function __construct($xmlPath, Zend_Config $config, $sourcePath = "go")
	{
		parent::__construct($xmlPath, $sourcePath, $config);
	}

	function getSingleLineCommentMarker()
	{
		return '//';
	}

	function generate()
	{
		parent::generate();

		$xpath = new DOMXPath($this->_doc);
		$this->loadClassInheritance($xpath->query("/xml/classes/class"));
		$this->loadEnums($xpath->query("/xml/enums/enum"));

		// enumes
		$enumNodes = $xpath->query("/xml/enums/enum");
		foreach($enumNodes as $enumNode)
		{
		 	$this->writeEnum($enumNode);
		}

		// types
		$classNodes = $xpath->query("/xml/classes/class");
		foreach($classNodes as $classNode)
		{
			$this->addClass($classNode);
		}
		
		ini_set("memory_limit", "512M");
		foreach($this->_allClasses as $classNode)
		{
			$this->_allContainers[$classNode->className] = $this->extractAllInheritanceClasses($classNode);
		}

		$classNodes = $xpath->query("/xml/classes/class");
		foreach($classNodes as $classNode)
		{
			$this->writeClass($classNode);
		}	

		$serviceNodes = $xpath->query("/xml/services/service");

		foreach($serviceNodes as $serviceNode)
		{
			$this->writeService($serviceNode);
		}

		$requestConfigurationNodes = $xpath->query("/xml/configurations/request/*");

		$this->writeErrors($xpath->query("/xml/errors/error"));


		$this->writeRequestGoFile();
	}

	private function writeErrors(DOMNodeList $errors)
	{
		$s = "";
		$s .= "package errors\n";
		$s .= "\n";
		$s .= "const (\n";
		
		foreach($errors as $item)
		{
			$s .= "		".$item->getAttribute("name")." = \"".$item->getAttribute("code")."\"\n";
		}

		$s .= ")\n";

		$file = "errors/codes.go";
		$this->addFile("KalturaClient/".$file, $s);
	}
	
	private function loadEnums(DOMNodeList $enums)
	{
		foreach($enums as $item)
		{
			$this->_enums[$item->getAttribute("name")] = null;
		}
	}

    function writeEnum(DOMElement $enumNode)
	{
		$enumName = $enumNode->getAttribute("name");
		if(!$this->shouldIncludeType($enumName))
			return;
		
		$enumName = $this->getGOName($enumName);
		$enumNameLower = strtolower($enumName);

		$s = "";
		$s .= "package $enumNameLower\n";
		$s .= "\n";
		$s .= "import (\n";
		$s .= "   \"errors\"\n";

		if(count($enumNode->childNodes) > 0)
		{
			$s .= "   \"encoding/json\"\n";
			$s .= ")\n";
			$s .= "\n";
			$s .= "type $enumName string\n";
			$s .= "\n";
			$s .= "const (\n";

			$propertyType =  $enumNode->getAttribute("enumType");
			$enumValues = array();

			foreach($enumNode->childNodes as $constNode)
			{
				if ($constNode->nodeType != XML_ELEMENT_NODE)
					continue;

				$propertyName = $constNode->getAttribute("name");
				$propertyValue = $constNode->getAttribute("value");
				$enumValues[] = $propertyName;
				$s .= "   $propertyName $enumName = \"$propertyValue\"\n";
			}
			$enumValuesString = implode (", ", $enumValues);

			$s .= ")\n";
			$s .= "\n";
			$s .= "func (e *$enumName) UnmarshalJSON(b []byte) error {\n";
			$s .= "   var s string\n";
			$s .= "   err := json.Unmarshal(b, &s)\n";
			$s .= "   if err != nil {\n";
			$s .= "      return err\n";
			$s .= "   }\n";
			$s .= "   enumValue := $enumName(s)\n";
			$s .= "   switch enumValue {\n";
			$s .= "   case $enumValuesString:\n";
			$s .= "      *e = enumValue\n";
			$s .= "      return nil\n";
			$s .= "   }\n";
			$s .= "   return errors.New(\"invalid $enumName enum value\")\n";
			$s .= "}\n";
			$s .= "\n";
		} else
		{
			$s .= ")\n";
			$s .= "\n";
			$s .= "type $enumName string\n";
			$s .= "func (e *$enumName) UnmarshalJSON(b []byte) error {\n";
			$s .= "	return errors.New(\"invalid $enumName enum value\")\n";
			$s .= "}\n";
		}

		$file = "enums/$enumNameLower/enum.go";
		$this->addFile("KalturaClient/".$file, $s);
	}

    function writeClass(DOMElement $classNode)
	{
		$type = $classNode->getAttribute("name");
		
		if(!$this->shouldIncludeType($type))
		{
			return;
		}
		if($type == 'KalturaObject')
		{
			return;
		}
		
		$className = $this->getGOName($type);
		$currentClass = $this->findClassByName($className);
		$s = "";
		$prefixText = "";		
		$this->startNewTextBlock();
		$prefixText .= "package types\n";
		$prefixText .= "\n";
		$isImport = false;

		if(!$currentClass->isAbstract)
		{
			$isImport = true;
			$prefixText .= "import (\n";
			$prefixText .= "  \"encoding/json\"\n";
		}

		// class definition
		$s .= "type $className struct {\n";

		$allInheritanceProperties = array();
		$allInheritanceProperties = $this->extractAllInheritanceProperties($currentClass, $allInheritanceProperties);

		$isHaveBase = false;
		
		if ($classNode->hasAttribute("base"))
		{
			$isHaveBase = true;
		}

		$properties = array();
		$enumsImported = array();
		$isNullable = false;
		
		foreach($allInheritanceProperties as $property)
		{
			if ($property->isEnumImport)
			{
				if(!$isImport){
					$prefixText .= "import (\n";
					$isImport = true;
				}

				if(!in_array($property->enumPackage, $enumsImported)){
					$prefixText .= $property->enumImport;
					$enumsImported[] = $property->enumPackage;
				}
			}

			$propertyLine = $property->name." ".$property->type." ".$property->json;

			if(array_filter($this->_allClasses, function($toCheck) use ($property) { 
				return $toCheck->className == $property->pureType; 
			}) && $this->isContainerByName($property->pureType))
			{
				$propertyLine = $property->name." ".$property->type."Container ".$property->json;
			}
			$s .= "		" . $propertyLine."\n";
		}
		
		$s .= "}\n";
		$s .= "\n";
		$s .= $this->writeInterfaces($isHaveBase, $currentClass, $allInheritanceProperties, $className, $type);
		$isNeedJson = false;
		$s .= $this->writeContainer($currentClass, $className, $isNeedJson);

		if($currentClass->isAbstract && $isNeedJson)
		{
			if(!$isImport)
			{
				$isImport = true;
				$prefixText .= "import (\n";
			}
			$prefixText .= "  \"encoding/json\"\n";
			$prefixText .= "  \"errors\"\n";
		} else if($isNeedJson)
		{
			$prefixText .= "  \"errors\"\n";
		}

		if($isImport){
			$prefixText .= ")\n";
		}
		$prefixText .= "\n";

		$fileName = $this->from_camel_case($className);
		$file = "types/$fileName.go";
		$allFile = $prefixText.$s;
		$this->addFile("KalturaClient/".$file, $allFile);
	}

	function writeInterfaces($isHaveBase, $currentClass, $allInheritanceProperties, $className, $type)
	{
		$s = "";

		if(!$currentClass->isAbstract)
		{
			$s .= "func (o *$className) MarshalJSON() ([]byte, error) {\n";
			$s .= "	   type Alias $className\n";
			$s .= "	   return json.Marshal(&struct {\n";
			$s .= "	   		*Alias\n";
			$s .= "	   		ObjectType string `json:\"objectType\"`\n";
			$s .= "	   }{\n";
			$s .= "	   Alias:      (*Alias)(o),\n";
			$s .= "	   ObjectType: \"$type\",\n";
			$s .= "	   })\n";
			$s .= "}\n";
		}

		$s .= "type $className"."Interface interface {\n";

		if($isHaveBase)
		{
			$s .= "		".$currentClass->parentClass."Interface\n";
		}

		$textIfEnumOrContainer = "";
		foreach($currentClass->properties as $currProperty)
		{
			$textIfEnumOrContainer = "		Get".$currProperty->name."() ".$currProperty->type."\n";

			if(array_filter($this->_allClasses, function($toCheck) use ($currProperty) { 
				return $toCheck->className == $currProperty->pureType; 
			}) && $this->isContainerByName($currProperty->pureType))
			{
				$textIfEnumOrContainer = "		Get".$currProperty->name."() ".$currProperty->type."Container\n";
			}

			$s .= $textIfEnumOrContainer;
		}
		$s .= "}\n";

		if(!$currentClass->isAbstract)
		{
			$textIfEnumOrContainer = "";
			$textIfPointer = "";
			foreach($allInheritanceProperties as $currProperty)
			{
				$textIfEnumOrContainer = "func (f *$className) Get".$currProperty->name."() ".$currProperty->type." {\n";
				if(array_filter($this->_allClasses, function($toCheck) use ($currProperty) { 
					return $toCheck->className == $currProperty->pureType; 
				}) && $this->isContainerByName($currProperty->pureType))
				{
					$textIfEnumOrContainer = "func (f *$className) Get".$currProperty->name."() ".$currProperty->type."Container {\n";
				}
				$textIfEnumOrContainer .= "		return f.".$currProperty->name."\n";

				$s .= $textIfEnumOrContainer;
				$s .= "}\n";

				$textIfPointer = "";
			}
		}
		return $s;
	}

	function writeContainer($currentClass, $className, &$isNeedJson)
	{
		$s = "";

		$inheritanceClasses = $this->_allContainers[$currentClass->className];
		if($this->isContainer($inheritanceClasses))
		{
			$isNeedJson = true;
			$inheritanceClasses = array_unique($inheritanceClasses);
			$containerGetFunc = "";
			$containerGetFunc .= "func (a *".$className."Container) Get() $className"."Interface {\n";

			$s .= "type ".$className."Container struct {\n";
	
			foreach($inheritanceClasses as $currClassName)
			{
				// Container fields
				$s .= "	".$currClassName."   *".$currClassName."\n";

				// ContainerGet function
				$containerGetFunc .= "	if a.".$currClassName." != nil {\n";
				$containerGetFunc .= "		return a.".$currClassName."\n";
				$containerGetFunc .= "	}\n";
			}	
			$s .= "}\n";

			$containerGetFunc .= "	return nil\n";
			$containerGetFunc .= "}\n";

			$s .= "func (b *".$className."Container) UnmarshalJSON(bytes []byte) error {\n";
			$s .= "	var objectType ObjectType\n";
			$s .= "	err := json.Unmarshal(bytes, &objectType)\n";
			$s .= "	if err != nil {\n";
			$s .= "		return err\n";
			$s .= "	}\n";
			$s .= "	switch objectType.Type {\n";
			foreach($inheritanceClasses as $currClassName)
			{
				$s .= "	case \"Kaltura".$currClassName."\":\n";
				$s .= "		a := &".$currClassName."{}\n";
				$s .= "		err = json.Unmarshal(bytes, a)\n";
				$s .= "	if err != nil {\n";
				$s .= "		return err\n";
				$s .= "	}\n";
				$s .= "	b.".$currClassName." = a\n";
			}	
			$s .= "	default:\n";
			$s .= "		return errors.New(\"unknown ".$className."Container type\")\n";
			$s .= "	}\n";
			$s .= "	return nil\n";
			$s .= "}\n";

			$s .= "func (o ".$className."Container) MarshalJSON() ([]byte, error) {\n";
			$s .= "	realObject := o.Get()\n";
			$s .= "	if realObject != nil {\n";
			$s .= "		return json.Marshal(realObject)\n";
			$s .= "	}\n";
			$s .= "	return nil, nil\n";
			$s .= "}\n";

			$s .= $containerGetFunc;
		}

		return $s;
	}

	function from_camel_case($input) {
		preg_match_all('!([A-Z][A-Z0-9]*(?=$|[A-Z][a-z0-9])|[A-Za-z][a-z0-9]+)!', $input, $matches);
		$ret = $matches[0];
		foreach ($ret as &$match) {
		  $match = $match == strtoupper($match) ? strtolower($match) : lcfirst($match);
		}
		return implode('_', $ret);
	}

	function writeService(DOMElement $serviceNode)
	{
		$serviceId = $serviceNode->getAttribute("id");
		if(!$this->shouldIncludeService($serviceId))
			return;

		// Importing the requests to service
		$prefixText = "";
		$prefixText .= "package services\n";
		$prefixText .= "\n";
		$prefixText .= "import (\n";
		$prefixText .= " \"fmt\"\n";
		$prefixText .= " \"context\"\n";
		$prefixText .= " \"encoding/json\"\n";
		$prefixText .= " \"github.com/kaltura/KalturaOttGeneratedAPIClientsGo/kalturaclient\"\n";
		
		$serviceName = $serviceNode->getAttribute("name");

		$goServiceName = $this->upperCaseFirstLetter($serviceName)."Service";

		$actionNodes = $serviceNode->childNodes;

		$s = "";
		$s .= "type $goServiceName struct {\n";
		$s .= "	client *kalturaclient.Client\n";
		$s .= "}\n";
		$s .= "\n";
		$s .= "func New$goServiceName(client *kalturaclient.Client) *$goServiceName {\n";
		$s .= "		return &$goServiceName{\n";
		$s .= "			   client:	client,\n";
		$s .= "		}\n";
		$s .= "}\n";

		$importedEnums = array();

		$actionNodes = $serviceNode->childNodes;
		foreach($actionNodes as $actionNode)
		{
			if ($actionNode->nodeType != XML_ELEMENT_NODE)
				 continue;
			$s .= "\n";
			$s .= $this->writeAction($serviceId, $serviceName, $actionNode, $prefixText, $importedEnums);
		}

		if(strpos($s, 'types') !== FALSE)
		{
			$prefixText .= " \"github.com/kaltura/KalturaOttGeneratedAPIClientsGo/kalturaclient/types\"\n";
		}
		$prefixText .= ")\n";
		$allFile = "$prefixText\n$s";
		$fileName = $this->from_camel_case($serviceName);
		$file = "services/".$fileName.".go";
		$this->addFile("KalturaClient/".$file, $allFile);
	}

	function writeAction($serviceId, $serviceName, DOMElement $actionNode, &$prefixText, &$importedEnums)
	{
		$text = "";
		$action = $actionNode->getAttribute("name");
		if(!$this->shouldIncludeAction($serviceId, $action))
			return;

		$resultNode = $actionNode->getElementsByTagName("result")->item(0);
		$resultType = $resultNode->getAttribute("type");
		$arrayObjectType = ($resultType == 'array') ? $resultNode->getAttribute("arrayType" ) : null;
		$comaIfNeeded = ", ";
		$nilIfNeeded = "nil, ";
		$isKalturaType = false;

		if($resultType == 'file')
			return;

		switch($resultType)
		{
			case null:
				$nilIfNeeded = "";
				$comaIfNeeded = "";
				$goOutputType = "";
				break;
			case "array":
				$arrayType = $resultNode->getAttribute("arrayType");
				$newName = $arrayType;
				if(strpos($arrayType, 'Kaltura') !== FALSE)
				{
					$newName = "types.".$this->getGOName($arrayType);
					if($this->isContainerByName($this->getGOName($arrayType)))
					{
						$newName = "types.".$this->getGOName($arrayType)."Container";
					}
					$isKalturaType = true;
				}
				$goOutputType = "[]".$newName;
				break;
			case "map":
				$arrayType = $this->getGOName($resultNode->getAttribute("arrayType"));
				if(strpos($resultNode->getAttribute("arrayType"), 'Kaltura') !== FALSE)
				{
					$arrayType = "types.".$arrayType;
					if($this->isContainerByName($arrayType))
					{
						$arrayType = "types.".$arrayType."Container";
					}
				}
				
				$goOutputType = "map[string]".$arrayType;
				break;
			case "bigint":
				$goOutputType = "*int64";
				break;
			case "int":
				$goOutputType = "*int32";
				break;
			case "float":
				$goOutputType = "*float32";
				break;
			case "bool":
				$goOutputType = "*bool";
				break;
			case "string":
				$goOutputType = "*string";	
				break;
			case "KalturaStringValue":
				$goOutputType = "*string";
				break;
			default:
				$resultGo = $this->getGOName($resultType);
				$goOutputType = "*types.$resultGo";
				if($this->isContainerByName($resultGo))
				{
					$goOutputType = "*types.$resultGo"."Container";
				}
				$isKalturaType = true;
				break;
		}
		
		$requestName = ucfirst($serviceName).'ListResponse';
		$goServiceName = $this->upperCaseFirstLetter($serviceName)."Service";
		$goActionName = $this->upperCaseFirstLetter($action);
		$signaturePrefix = "func (s *$goServiceName) $goActionName";
		$paramNodes = $actionNode->getElementsByTagName("param");
		$signature = $this->getSignature($paramNodes, $prefixText, $importedEnums);
		array_push($signature[0], "extra ...kalturaclient.Param");
		$signatureParamsWithTypes = implode (", ", $signature[0]);
		$signatureParamsWithoutTypes = $signature[1];

		// write the overload
		$text .= "\n";
		$text .= "$signaturePrefix(ctx context.Context, $signatureParamsWithTypes) (".$goOutputType.$comaIfNeeded."error){\n";
		$text .= "	path := \"service/$serviceName/action/$action\"\n";
		$text .= "	requestMap := map[string]interface{}{}\n";

		foreach($signatureParamsWithoutTypes as $currParam)
		{
			$addedGetParams = ""	;

			if(strpos($currParam, '*') !== FALSE)
			{
				$withoutOptional = str_replace('*','',$currParam);
				$text .= "	if $withoutOptional != nil {\n";
				$text .= "		requestMap[\"$withoutOptional\"] = ".$withoutOptional.$addedGetParams."\n";
				$text .= "	}\n";
			} else{
				$text .= "	requestMap[\"$currParam\"] = ".$currParam.$addedGetParams."\n";
			}
		}

		$text .= "	byteResponse, err := s.client.Execute(ctx, path, requestMap, extra)\n";
		$text .= "	if err != nil {\n";
		$text .= "		return ".$nilIfNeeded."err\n";
		$text .= "	}\n";
		if($comaIfNeeded == "")
		{
			$text .= "	var result struct {\n";
			$text .= "      Result interface{} `json:\"result\"`\n";
			$text .= "  }\n";
			$text .= "	err = json.Unmarshal(byteResponse, &result)\n";
			$text .= "	if err != nil {\n";
			$text .= "		return ".$nilIfNeeded."fmt.Errorf(\"failed to parse json: %w\", err)\n";
			$text .= "	}\n";
			$text .= "	return nil\n";
		} else
		{
			$text .= "	var result struct {\n";
			$text .= "      Result $goOutputType `json:\"result\"`\n";
			$text .= "  }\n";
			$text .= "	err = json.Unmarshal(byteResponse, &result)\n";
			$text .= "	if err != nil {\n";
			$text .= "		return ".$nilIfNeeded."fmt.Errorf(\"failed to parse json: %w\", err)\n";
			$text .= "	}\n";
			$text .= "	return result.Result, nil\n";
		}
		$text .= "}\n";

		return $text;
	}

    private function getGOName($name)
	{
		if($name === 'KalturaObject')
			return 'ObjectBase';
		
		if ($this->isClassInherit($name, "KalturaListResponse"))
		{
			$arrayType = $this->getListResponseType($name);
			return $arrayType."ListResponse";
		}

		$name =  preg_replace('/^Kaltura/', '', $name);
		if(in_array($name , array("Group")))
		{
			return $name . "_";
		}
		
		return $name;
	}

	function getSignature($paramNodes, &$prefixText, &$importedEnums)
	{
		// We need 2 strings, one with the types and one without them
		$isAddedEnums = false;
		$params = array();
		$paramsString = array();

		foreach($paramNodes as $paramNode)
		{
			$paramType = $paramNode->getAttribute("type");
			$paramName = $paramNode->getAttribute("name");
			$isEnum = $paramNode->hasAttribute("enumType");
			$optional = $paramNode->getAttribute("optional");
			$enumPackage = strtolower($this->getGOName($paramNode->getAttribute("enumType")));

			if($isEnum && !in_array($enumPackage, $importedEnums))
			{
				// Importing enums to service
				$prefixText .= "	\"github.com/kaltura/KalturaOttGeneratedAPIClientsGo/kalturaclient/enums/$enumPackage\"\n";
				$importedEnums[] = $enumPackage;
			}

			$isInterface = false;

			switch($paramType)
			{
				case "array":
					$arrayType = $paramNode->getAttribute("arrayType");
					$newName = $arrayType;
					if(strpos($arrayType, 'Kaltura') !== FALSE)
					{
						$newName = "types.".$this->getGOName($arrayType)."Interface";
						$isInterface = true;
					}
					$goType = "[]".$newName;
					break;
				case "map":
					$arrayType = $paramNode->getAttribute("arrayType");
					$newName = $arrayType;
					if(strpos($arrayType, 'Kaltura') !== FALSE)
					{
						$newName = "types.".$this->getGOName($arrayType)."Interface";
						$isInterface = true;
					}
					$goType = "map[string]" . $newName;
					break;
				case "file":
					$goType = "[]byte";
					break;
				case "bigint":
					$goType = "int64";
					break;
				case "int":
					if ($isEnum)
						$goType = $this->getGOName($paramNode->getAttribute("enumType"));
					else
						$goType = "int32";
					break;
				case "float":
					$goType = "float32";
						break;
				case "KalturaStringValue":
					$goOutputType = "string";
					break;
				default:
					if ($isEnum)
						$goType = $this->getGOName($paramNode->getAttribute("enumType"));
					else if(strpos($paramType, 'Kaltura') !== FALSE)
					{
						$goType = "types.".substr($paramType, 7)."Interface";
						$isInterface = true;
					}
					else
					{
						$goType = "$paramType";
					}
					break;
			}

			if($paramName == "type")
			{
				$paramName = $this->lowerCaseFirstLetter($goType);
			}

			if($isEnum)
			{
				$goType = $enumPackage.".".$goType;
			}

			$param = "$paramName ";

			// If optional we need to add *
			if ($optional == "1" && !$isInterface)
			{
				$param .= "*$goType";
				$paramsString[] = "*$paramName";
			} else if($optional == "1" && $isInterface)
			{
				$param .= "$goType";
				$paramsString[] = "*$paramName";
			} else
			{
				$param .= "$goType";
				$paramsString[] = "$paramName";
			}
							
			$params[] = "$param";
		}

		return array($params, $paramsString);
	}

    private function loadClassInheritance(DOMNodeList $classes)
	{
		// first fill the base classes
		foreach($classes as $item)
		{
			$class = $item->getAttribute("name");
			if (!$item->hasAttribute("base"))
			{
				$this->_classInheritance[$class] = array();
			}
		}

		// now fill recursively the childs
		foreach($this->_classInheritance as $class => $null)
		{
			$this->loadChildsForInheritance($classes, $class, $this->_classInheritance);
		}
	}

	private function loadChildsForInheritance(DOMNodeList $classes, $baseClass, array &$baseClassChilds)
	{
		$baseClassChilds[$baseClass] = $this->getChildsForParentClass($classes, $baseClass);

		foreach($baseClassChilds[$baseClass] as $childClass => $null)
		{
			$this->loadChildsForInheritance($classes, $childClass, $baseClassChilds[$baseClass]);
		}
	}

	private function getChildsForParentClass(DOMNodeList $classes, $parentClass)
	{
		$childs = array();
		foreach($classes as $item2)
		{
			$currentParentClass = $item2->getAttribute("base");
			$class = $item2->getAttribute("name");
			if ($currentParentClass === $parentClass)
			{
				$childs[$class] = array();
			}
		}
		return $childs;
	}

	private function isClassInherit($class, $baseClass)
	{
		$classTree = $this->getClassChildsTree($this->_classInheritance, $baseClass);
		if (is_null($classTree))
			return false;
		else
		{
			if (is_null($this->getClassChildsTree($classTree, $class)))
				return false;
			else
				return true;
		}
	}

	/**
	 * Finds the class in the multidimensional array and returns a multidimensional array with its child classes
	 * Null if not found
	 *
	 * @param array $classes
	 * @param string $class
	 */
	private function getClassChildsTree(array $classes, $class)
	{
		foreach($classes as $tempClass => $null)
		{
			if ($class === $tempClass)
			{
				return $classes[$class];
			}
			else
			{
				$subArray = $this->getClassChildsTree($classes[$tempClass], $class);
				if (!is_null($subArray))
					return $subArray;
			}
		}
		return null;
	}

	private function enumExists($enum)
	{
		return array_key_exists($enum, $this->_enums);
	}

	private function getListResponseType($name)
	{
		$xpath = new DOMXPath($this->_doc);
		$propertyNodes = $xpath->query("/xml/classes/class[@name='$name']/property[@name='objects']");
		$propertyNode = $propertyNodes->item(0);
		if(!$propertyNode)
			throw new Exception("Property [objects] not found for type [$name]");
		
		return $this->getGOName($propertyNode->getAttribute("arrayType"));
	}

	private function addClass(DOMElement $classNode)
	{
		$type = $classNode->getAttribute("name");
		$isImport = false;

		if(!$this->shouldIncludeType($type))
		{
			return;
		}
		if($type == 'KalturaObject')
		{
			return;
		} 

		$className = $this->getGOName($type);
		$newClass = new Classes();
		$newClass->className = $className;
		$newClass->parentClass = "";
		
		if ($classNode->hasAttribute("abstract"))
		{	
			$newClass->isAbstract = (bool) $classNode->getAttribute("abstract");
		}
		$baseName = null;

		if ($classNode->hasAttribute("base"))
		{
			$base = $classNode->getAttribute("base");
			$baseName = $this->getGOName($base);
			$newClass->parentClass = $baseName;
		}

		$properties = array();
		$enumsImported = array();
		foreach($classNode->childNodes as $propertyNode)
		{
			if ($propertyNode->nodeType != XML_ELEMENT_NODE)
				continue;

			$property = array(
				"apiName" => null,
				"name" => null,
				"type" => null,
				"default" => null,
				"isReadOnly" => false,
				"isWriteOnly" => false,
				"isInsertOnly" => false,
				"isOrderBy" => false,
				"nullable" => null,
				"enumPackageName" => null,
				"enumImport" => null,
				"isEnumImport" => false,
				"pureType" => null
			);

			$propType = $propertyNode->getAttribute("type");
			$propName = $propertyNode->getAttribute("name");
			$property["apiName"] = $propName;
			$isEnum = $propertyNode->hasAttribute("enumType");
			$goPropName = $this->upperCaseFirstLetter($propName);
			if ($baseName != null && $baseName == $goPropName){
				$goPropName = $goPropName."Property";
			}
			$property["name"] = $goPropName;

			if ($isEnum)
			{
				$goPropType = $this->getGOName($propertyNode->getAttribute("enumType"));
				$enumPackage = strtolower($goPropType);
				$goPropType = $enumPackage.".".$goPropType;
				if(!$isImport){
					$isImport = true;
				}
				if(!in_array($enumPackage, $enumsImported)){
					$property["isEnumImport"] = true;
					$property["enumPackageName"] = $enumPackage;
					$property["enumImport"] = "  \"github.com/kaltura/KalturaOttGeneratedAPIClientsGo/kalturaclient/enums/$enumPackage\"\n";
					$enumsImported[] = $enumPackage;
				}
				$property["pureType"] = "enum";
			}
			else if ($propType == "array")
			{
				$arrayObjectType = $propertyNode->getAttribute("arrayType");
				if($arrayObjectType == 'KalturaObject')
                    $arrayObjectType = 'ObjectBase';
				$goPropType = "[]" . $this->getGOName($arrayObjectType);
				$property["pureType"] = $this->getGOName($arrayObjectType);
			}
			else if ($propType == "map")
			{
				$arrayObjectType = $propertyNode->getAttribute("arrayType");
				if($arrayObjectType == 'KalturaObject')
					$arrayObjectType = 'ObjectBase';
				$goPropType = "map[string]" . $this->getGOName($arrayObjectType);
				$property["pureType"] = $this->getGOName($arrayObjectType);
			}
			else if ($propType == "bool")
			{
				$goPropType  = "bool";
				$property["pureType"] = "bool"; 
			}
			else if ($propType == "bigint")
			{
				$goPropType  = "int64";
				$property["pureType"] = "int64"; 
			}
			else if ($propType == "time")
			{
				$goPropType  = "int32";
				$property["pureType"] = "int32"; 
			}
			else if ($propType == "int")
			{
				$goPropType  = "int32";
				$property["pureType"] = "int32"; 
			}
			else if ($propType == "float")
			{
				$goPropType  = "float32";
				$property["pureType"] = "float32"; 
			}
			else
			{
				$goPropType = $this->getGOName($propType);
				$property["pureType"] = $this->getGOName($propType); 
			}

			$property["type"] = $goPropType;
			$property["arrayType"] = $propertyNode->getAttribute("arrayType");

			if($propertyNode->hasAttribute("nullable"))
			{
				$property["nullable"] = (bool)$propertyNode->getAttribute("nullable");
			}
			if ($propertyNode->hasAttribute("readOnly"))
			{
				$property["isReadOnly"] = (bool)$propertyNode->getAttribute("readOnly");
			}
			if ($propertyNode->hasAttribute("writeOnly"))
			{
				$property["isWriteOnly"] = (bool)$propertyNode->getAttribute("writeOnly");
			}
			if ($propertyNode->hasAttribute("insertOnly"))
			{
				$property["isInsertOnly"] = (bool)$propertyNode->getAttribute("insertOnly");
			}
			
			switch($propType)
			{
				case "string":
					$property["default"] = "nil";
					break;
				case "bool":
					$property["default"] = "nil";
					break;
			}

			$properties[] = $property;
		}

		$isNullable = false;

		// properties
		foreach($properties as $property)
		{
			if($property['nullable'] || $property['isReadOnly'] || $property['isWriteOnly'] || $property['isInsertOnly'])
			{
				$isNullable = true;
			}

			$currName = $this->lowerCaseFirstLetter($property['apiName']);

			$propertyForClass = new Property();
			$propertyForClass->name = $property['name'];
			$propertyForClass->type = $property['type'];
			$propertyForClass->json = "`json:\"$currName,omitempty\"`";
			$propertyForClass->pureType = $property['pureType'];

			if($property["isEnumImport"])
			{
				$propertyForClass->isEnumImport = true;
				$propertyForClass->enumPackage = $property["enumPackageName"];
				$propertyForClass->enumImport = $property["enumImport"];
			}

			if($isNullable)
			{
				$propertyForClass->type = "*".$property['type'];
				$propertyForClass->json = "`json:\"$currName,omitempty\"`";
			} else if($className == $property['type'])
			{
				$propertyForClass->type = "*".$property['type'];
			}
			$isNullable = false;

            $newClass->properties[] = $propertyForClass;
		}
		$this->_allClasses[] = $newClass;
	}

	private function findClassByName($name)
	{
		foreach ( $this->_allClasses as $class ) {
			if ( strcasecmp($name, $class->className) == 0 ) {
				return $class;
			}
		}

		return false;
	}

	private function extractAllInheritanceProperties($currClass, $allInheritanceProperties)
	{
		$propertiesToAdd = array();
		if($currClass)
		{
			foreach ($currClass->properties as $currProperty)
			{
				if(!array_filter($allInheritanceProperties, function($toCheck) use ($currProperty) { 
					return $toCheck->name == $currProperty->name; 
				}))
				{
					$propertiesToAdd[] = $currProperty;
				}
			}
			$allInheritanceProperties = array_merge($allInheritanceProperties, $propertiesToAdd);

			$propertiesToAdd = array_merge($propertiesToAdd, $this->extractAllInheritanceProperties($this->findClassByName($currClass->parentClass), $allInheritanceProperties));
		}

		return $propertiesToAdd;
	}

	private function extractAllInheritanceClasses($currClass)
	{
		$calssesToAdd = array();

		if($currClass)
		{
			if(!$currClass->isAbstract)
			{
				$calssesToAdd[] = $currClass->className;
			}

			foreach($this->_allClasses as $currentClassObject)
			{	
				if($currentClassObject->parentClass == $currClass->className)
				{
					$classByName = $this->findClassByName($currentClassObject->className);
					$allInheritanceClasses = $this->extractAllInheritanceClasses($classByName);
					$calssesToAdd = array_merge($calssesToAdd, $allInheritanceClasses);
				}
			}
		}

		return $calssesToAdd;
	}

	private function isContainerByName($className){
		return $this->isContainer($this->_allContainers[$className]) ;
	}

	private function isContainer($classes){
		return count($classes) > 1;
	}

	private function writeRequestGoFile(){
		$version = $this->_doc->documentElement->getAttribute('apiVersion'); //located at input file top
        $date = date('d-m-y');
        $text = 'package kalturaclient

import (
	"context"
	"encoding/json"
	"errors"
)

type Request struct {
	ctx     context.Context
	path    string
	body    map[string]interface{}
	headers map[string]string
}

var emptyParamKeyError = errors.New("empty key")
var alreadyExistsError = errors.New("parameter already exists")
var defaultBodyParams = map[string]interface{}{
	"clientTag":  "go:'.$date.'",
	"apiVersion": "'.$version.'",
	"format":     "1",
	"language":   "*",
}

func NewRequest(ctx context.Context, path string, body map[string]interface{}) Request {
	addDefaultParams(body)
	return Request{
		ctx:     ctx,
		path:    path,
		body:    body,
		headers: make(map[string]string),
	}
}

func (r Request) GetContext() context.Context {
	return r.ctx
}

func (r Request) GetPath() string {
	return r.path
}

func (r Request) GetHeaders() map[string]string {
	return r.headers
}

func (r Request) GetBodyBytes() ([]byte, error) {
	return json.Marshal(r.body)
}

func (r Request) WithParamForce(param Param) Request {
	if param.key == "" {
		return r
	}
	if param.inBody {
		r.body[param.key] = param.value
	} else {
		r.headers[param.key] = param.value.(string)
	}

	return r
}

func (r Request) WithParam(param Param) (Request, error) {
	if param.key == "" {
		return r, emptyParamKeyError
	}

	if param.inBody {
		if _, exists := r.body[param.key]; !exists {
			r.body[param.key] = param.value
		} else {
			return r, alreadyExistsError
		}
	} else {
		if _, exists := r.headers[param.key]; !exists {
			r.headers[param.key] = param.value.(string)
		} else {
			return r, alreadyExistsError
		}
	}

	return r, nil
}

func addDefaultParams(request map[string]interface{}) {
	for key, value := range defaultBodyParams {
		if _, exists := request[key]; !exists {
			request[key] = value
		}
	}
}';
		$file = "request.go";
		$this->addFile("KalturaClient/".$file, $text);
	}
}
