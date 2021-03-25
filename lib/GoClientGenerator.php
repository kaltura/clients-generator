<?php
class GoClientGenerator extends ClientGeneratorFromXml
{
	private $_csprojIncludes = array();
	private $_classInheritance = array();
	private $_enums = array();

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

		// enumes $ types
		$enumNodes = $xpath->query("/xml/enums/enum");
		foreach($enumNodes as $enumNode)
		{
		 	$this->writeEnum($enumNode);
		}

		$classNodes = $xpath->query("/xml/classes/class");
		foreach($classNodes as $classNode)
		{
			$this->writeClass($classNode);
		}

		$serviceNodes = $xpath->query("/xml/services/service");

		//$this->startNewTextBlock();
		foreach($serviceNodes as $serviceNode)
		{
			$this->writeService($serviceNode);
		}

		// $configurationNodes = $xpath->query("/xml/configurations/*");
		// $this->writeClient();

		$requestConfigurationNodes = $xpath->query("/xml/configurations/request/*");
		//$this->writeRequestBuilderConfigurationClass($requestConfigurationNodes);
		//$this->writeObjectFactory($classNodes);

		$errorNodes = $xpath->query("/xml/errors/error");

		// TODO write a client file like java
		
		//$this->writeApiException($errorNodes);

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
		
		$enumName = $this->getCSharpName($enumName);
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
			$s .= "   return errors.New(\"invalid enum value\")\n";
			$s .= "}\n";
			$s .= "\n";
		} else
		{
			$s .= ")\n";
			$s .= "\n";
			$s .= "type $enumName string\n";
			$s .= "func (e *$enumName) UnmarshalJSON(b []byte) error {\n";
			$s .= "	return errors.New(\"invalid enum value\")\n";
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
		
		// if ($this->isClassInherit($type, "KalturaListResponse") || $type == "KalturaListResponse")
		// {
		// 	KalturaLog::info("$type third if");
		// 	return;
		// }
            
		$s = "";
		$prefixText = "";		
		$className = $this->getCSharpName($type);
		$this->startNewTextBlock();
		$prefixText .= "package types\n";
		$prefixText .= "\n";
		$isImport = false;
		// $this->appendLine("import (");
		// //$this->appendLine(" \"strconv\" ");
		// $this->appendLine(" \"encoding/json\" ");
		// $this->appendLine(")");

		// class definition
		$s .= "type $className struct {\n";
		
		$s .= "		ObjectType\n";
		$baseName = null;
		if ($classNode->hasAttribute("base"))
		{
			$base = $classNode->getAttribute("base");
			$baseName = $this->getCSharpName($base);
			$s .= $this->upperCaseFirstLetter("		".$baseName)."\n";
		}

		// we want to make the orderBy property strongly typed with the corresponding string enum
		$isFilter = false;
		if ($this->isClassInherit($type, "KalturaFilter"))
		{
			$orderByType = str_replace("Filter", "OrderBy", $type);
			if ($this->enumExists($orderByType))
			{
				$orderByElement = $classNode->ownerDocument->createElement("property");
				$orderByElement->setAttribute("name", "orderBy");
				$orderByElement->setAttribute("type", "string");
				$orderByElement->setAttribute("enumType", $orderByType);
				$classNode->appendChild($orderByElement);
				$isFilter = true;
			}
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
				"isOrderBy" => false,
				"nullable" => null
			);

			$propType = $propertyNode->getAttribute("type");
			$propName = $propertyNode->getAttribute("name");
			$property["apiName"] = $propName;
			$isEnum = $propertyNode->hasAttribute("enumType");
			$goPropName = $this->upperCaseFirstLetter($propName);
			if ($baseName != null && $baseName == $goPropName){
				$goPropName = $goPropName."Property";
			}
			if($propertyNode->hasAttribute("nullable"))
			{
				$property["nullable"] = $propertyNode->getAttribute("nullable");
			}
			$property["name"] = $goPropName;

			if ($isEnum)
			{
				$dotNetPropType = $this->getCSharpName($propertyNode->getAttribute("enumType"));
				$enumPackage = strtolower($dotNetPropType);
				$dotNetPropType = $enumPackage.".".$dotNetPropType;
				if(!$isImport){
					$prefixText .= "import (\n";
					$isImport = true;
				}
				if(!in_array($enumPackage, $enumsImported)){
					$prefixText .= "  \"github.com/kaltura/KalturaOttGeneratedAPIClientsGo/kalturaclient/enums/$enumPackage\"\n";
					$enumsImported[] = $enumPackage;
				}
			}
			else if ($propType == "array")
			{
				$arrayObjectType = $propertyNode->getAttribute("arrayType");
				if($arrayObjectType == 'KalturaObject')
                    $arrayObjectType = 'ObjectBase';
                $dotNetPropType = "[]" . $this->getCSharpName($arrayObjectType);
			}
			else if ($propType == "map")
			{
				$arrayObjectType = $propertyNode->getAttribute("arrayType");
				if($arrayObjectType == 'KalturaObject')
					$arrayObjectType = 'ObjectBase';
				$dotNetPropType = "map[string]" . $this->getCSharpName($arrayObjectType);
			}
			else if ($propType == "bool")
			{
				$dotNetPropType  = "bool";
			}
			else if ($propType == "bigint")
			{
				$dotNetPropType  = "int64";
			}
			else if ($propType == "time")
			{
				$dotNetPropType  = "int32";
			}
			else if ($propType == "int")
			{
				$dotNetPropType  = "int32";
			}
			else if ($propType == "float")
			{
				$dotNetPropType  = "float32";
			}
			else
			{
				$dotNetPropType = $this->getCSharpName($propType);
			}

			$property["type"] = $dotNetPropType;

			if ($isFilter && $goPropName == "OrderBy")
				$property["isOrderBy"] = true;
				
			$property["arrayType"] = $propertyNode->getAttribute("arrayType");

			if ($propertyNode->hasAttribute("readOnly"))
			{
				$property["readOnly"] = (bool)$propertyNode->getAttribute("readOnly");
			}

			if ($propertyNode->hasAttribute("writeOnly"))
			{
				$property["writeOnly"] = (bool)$propertyNode->getAttribute("writeOnly");
			}

			switch($propType)
			{
                // GO zero value of these types
				// case "bigint":
				// 	$property["default"] = "long.MinValue";
				// 	break;
				// case "int":
				// 	if ($isEnum)
				// 		$property["default"] = "($dotNetPropType)Int32.MinValue";
				// 	else
				// 		$property["default"] = "Int32.MinValue";
				// 	break;
				case "string":
					$property["default"] = "nil";
					break;
				case "bool":
					$property["default"] = "nil";
					break;
				// case "float":
				// 	$property["default"] = "Single.MinValue";
				// 	break;
			}

			$properties[] = $property;
		}
		
		// constants
		// $constants = array();

		// if($properties){
		// 	$this->appendLine("		const ( ");
		// }

		// foreach($properties as $property)
		// {
		// 	$constName = $this->camelCaseToUnderscoreAndUpper($property['name']);
		// 	$constants[$constName] = $property['name'];
		// 	//$new = ($property['isOrderBy'] || ($base && $this->classHasProperty($base, $property['apiName'], false))) ? 'new ' : '';
		// 	$this->appendLine("		$constName = \"{$property['apiName']}\"");
		// }

		// if($properties){
		// 	$this->appendLine(")");
		// }
		
		// $this->appendLine("		//endregion");

		$isNullable = false;

		// properties
		foreach($properties as $property)
		{
			//TODO : not working
			// if(array_key_exists('nullable', $property))
			// {
				if($property['nullable'] == "1")
				{
					$isNullable = true;
				}
			// }

			$currName = $this->lowerCaseFirstLetter($property['apiName']);
			$propertyLine = "{$property['name']} {$property['type']}	`json:\"$currName\"`";

			if($isNullable)
			{
				$propertyLine = "{$property['name']} *{$property['type']}	`json:\"$currName,omitempty\"`";
			}else if($className == $property['type']){
				$propertyLine = "{$property['name']} *{$property['type']}	`json:\"$currName\"`";
			}
			$isNullable = false;

            $s .= "		" . $propertyLine."\n";
		}
		
		$s .= "}\n";
		$s .= "\n";
		// $this->appendLine("func(s *$className) GetParams() map[string]interface{}{");
		// $this->appendLine("	params := map[string]interface{}{}");
		// foreach($classNode->childNodes as $propertyNode)
		// {
		// 	if ($propertyNode->nodeType != XML_ELEMENT_NODE)
		// 		continue;

		// 	$propName = $propertyNode->getAttribute("name");
		// 	$lowerCaseProp = $this->lowerCaseFirstLetter($goPropName);
		// 	$goPropName = $this->upperCaseFirstLetter($propName);
		// 	$this->appendLine("	if s.$goPropName != nil {");
		// 	$this->appendLine("		params[\"$lowerCaseProp\"]= s.$goPropName");
		// 	$this->appendLine("	}");
		// }
		// $this->appendLine("	return params");
		// $this->appendLine("}");

		$s .= "func New$className() *$className{\n";
		$s .= "	   return &$className{ObjectType: ObjectType{Type: \"$type\"}}\n";
		$s .= "}\n";

		if($isImport){
			$prefixText .= ")\n";
		}
		$prefixText .= "\n";

		$fileName = $this->from_camel_case($className);
		$file = "types/$fileName.go";
		$allFile = $prefixText.$s;
		$this->addFile("KalturaClient/".$file, $allFile);
		//$this->_csprojIncludes[] = $file;
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
		//$prefixText .= " \"github.com/kaltura/KalturaOttGeneratedAPIClientsGo/kalturaclient/types\"\n";
		
		$serviceName = $serviceNode->getAttribute("name");

		// TODO : check if needs to change from uppercase
		$dotNetServiceName = $this->upperCaseFirstLetter($serviceName)."Service";

		$actionNodes = $serviceNode->childNodes;
		foreach($actionNodes as $actionNode)
		{
			if ($actionNode->nodeType != XML_ELEMENT_NODE)
				continue;
		
				//$this->writeRequestBuilder($serviceId, $serviceName, $actionNode);
		}

		$s = "";
		$s .= "type $dotNetServiceName struct {\n";
		$s .= "	client *kalturaclient.Client\n";
		$s .= "}\n";
		$s .= "\n";
		$s .= "func New$dotNetServiceName(client *kalturaclient.Client) *$dotNetServiceName {\n";
		$s .= "		return &$dotNetServiceName{\n";
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

		if(str_contains($s, "types"))
		{
			$prefixText .= " \"github.com/kaltura/KalturaOttGeneratedAPIClientsGo/kalturaclient/types\"\n";
		}
		$prefixText .= ")\n";
		$allFile = "$prefixText\n$s";
		$fileName = $this->from_camel_case($serviceName);
		$file = "services/".$fileName.".go";
		$this->addFile("KalturaClient/".$file, $allFile);
	}

	// function writeRequestBuilder($serviceId, $serviceName, DOMElement $actionNode)
	// {
	// 	$actionName = $actionNode->getAttribute("name");
	// 	if(!$this->shouldIncludeAction($serviceId, $actionName))
	// 		return;

	// 	$resultNode = $actionNode->getElementsByTagName("result")->item(0);
	// 	$resultType = $resultNode->getAttribute("type");
	// 	$arrayObjectType = ($resultType == 'array') ? $resultNode->getAttribute("arrayType" ) : null;

	// 	if($resultType == 'file')
	// 		return;

	// 	$enableInMultiRequest = true;
	// 	if($actionNode->hasAttribute("enableInMultiRequest"))
	// 	{
	// 		$enableInMultiRequest = intval($actionNode->getAttribute("enableInMultiRequest"));
	// 	}

	// 	switch($resultType)
	// 	{
	// 		case null:
	// 			$dotNetOutputType = "";
	// 			break;
	// 		case "array":
	// 			$arrayType = $this->getCSharpName($resultNode->getAttribute("arrayType"));
	// 			$dotNetOutputType = $arrayType."[]";
	// 			break;
	// 		case "map":
	// 			$arrayType = $this->getCSharpName($resultNode->getAttribute("arrayType"));
	// 			$dotNetOutputType = "map[string]".$arrayType;
	// 			break;
	// 		case "bigint":
	// 			$dotNetOutputType = "int64";
    //             break;
    //         case "bool":
	// 			$dotNetOutputType = "bool";
    //             break;
    //         case "string":
	// 			$dotNetOutputType = "string";
	// 			break;
	// 		default:
	// 			$dotNetOutputType = $this->getCSharpName($resultType);
	// 			break;
	// 	}

	// 	$requestBuilderName = ucfirst($serviceName) . ucfirst($actionName) . 'RequestBuilder';

	// 	$paramNodes = $actionNode->getElementsByTagName("param");
	// 	$signature = $this->getSignature($paramNodes, false);
		
	// 	$parentType = $enableInMultiRequest ? $dotNetOutputType."RequestBuilder" : $dotNetOutputType."RequestBuilder";
	// 	$this->appendLine("	type $requestBuilderName struct (");
	// 	$this->appendLine(" 	$parentType");
		

	// 	$this->appendLine("		//region Constants");
	// 	$constants = array();
	// 	$news = array();
	// 	foreach($paramNodes as $paramNode)
	// 	{
	// 		$paramName = $paramNode->getAttribute("name");
	// 		$new = '';
			
	// 		// TODO removeeeeeeeeeeeeeeeeeee
	// 		// if($this->classHasProperty('KalturaRequestConfiguration', $paramName))
	// 		// {
	// 		// 	$news[$paramName] = true;
	// 		// 	$new = 'new ';
	// 		// }
	// 		$constName = $this->camelCaseToUnderscoreAndUpper($paramName);
	// 		$this->appendLine("		const $constName = \"$paramName\";");
	// 	}
	// 	$this->appendLine("		//endregion");
	// 	$this->appendLine();
		
		
	// 	$hasFiles = false;
	// 	$params = array();
	// 	foreach($paramNodes as $paramNode)
	// 	{
	// 		$paramType = $paramNode->getAttribute("type");
	// 		$paramName = $paramNode->getAttribute("name");
	// 		$isEnum = $paramNode->hasAttribute("enumType");
	// 		$isFile = false;

	// 		switch($paramType)
	// 		{
	// 			case "array":
	// 				$dotNetType = $this->getCSharpName($paramNode->getAttribute("arrayType")) . "[]";
	// 				break;
	// 			case "map":
	// 				$dotNetType = "map[string]" . $this->getCSharpName($paramNode->getAttribute("arrayType"));
	// 				break;
	// 				//TODO check stream in go 
	// 			case "file":
	// 				$dotNetType = "[]byte";
	// 				$isFile = true;
	// 				$hasFiles = true;
	// 				break;
	// 			case "bigint":
	// 				$dotNetType = "int64";
	// 				break;
	// 			//TODO removeeeeeeeeee
	// 			// case "int":
	// 			// 	if ($isEnum)
	// 			// 		$dotNetType = $this->getCSharpName($paramNode->getAttribute("enumType"));
	// 			// 	else
	// 			// 		$dotNetType = $paramType;
	// 			// 	break;
	// 			default:
	// 				$dotNetType = $paramType;
					
	// 				$dotNetType = $this->getCSharpName($dotNetType);
	// 				break;
	// 		}


	// 		$param = $this->fixParamName($paramName);
	// 		// TODO CHECK IF NEED TO ADD new
	// 		//$new = isset($news[$paramName]) ? 'new ' : '';
	// 		$paramName = ucfirst($param);
	// 		//$this->appendLine("		public {$new}$dotNetType $paramName { get; set; }");
	// 		$this->appendLine("		$paramName $dotNetType");
    //         $params[$param] = $isFile;
            
    //         if ($isFile){
    //             $this->appendLine("		{$paramName}_FileName string");
    //         }
	// 	}

	// 	$this->appendLine();
	// 	// $this->appendLine("		public $requestBuilderName()");
	// 	// $this->appendLine("			: base(\"$serviceId\", \"$actionName\")");
	// 	// $this->appendLine("		{");
	// 	// $this->appendLine("		}");
		
	// 	if(count($params))
	// 	{
	// 		$this->appendLine();
	// 		$this->appendLine("		func ($requestBuilderName *$requestBuilderName) New$requestBuilderName($signature) { ");
	// 		// $this->appendLine("			: this()");
	// 		foreach($params as $param => $isFile)
	// 		{
	// 			$paramName = ucfirst($param);
	// 			$this->appendLine("			$requestBuilderName.$paramName = $param;");
	// 		}
	// 		$this->appendLine("		}");
	// 	}
		
	// 	$this->appendLine();
	// 	$this->appendLine("		func ($requestBuilderName *$requestBuilderName) getParameters(bool includeServiceAndAction) Params {");
	// 	$this->appendLine("			kparams := $requestBuilderName.getParameters(includeServiceAndAction)");
	// 	foreach($params as $param => $isFile)
	// 	{
	// 		if(!$isFile)
	// 		{
	// 			$paramName = ucfirst($param);
	// 			$this->appendLine("			if !isMapped(\"$param\")");
	// 			$this->appendLine("				kparams.AddIfNotNull(\"$param\", $paramName)");
	// 		}
	// 	}
	// 	$this->appendLine("			return kparams");
	// 	$this->appendLine("		}");
		
	// 	$this->appendLine();
	// 	$this->appendLine("		func ($requestBuilderName *$requestBuilderName) getFiles() Files {");
    //     $this->appendLine("			kfiles := base.getFiles()");
	// 	foreach($params as $param => $isFile)
	// 	{
	// 		if($isFile)
	// 		{
	// 			$paramName = ucfirst($param);
	// 			$this->appendLine("			kfiles.Add(\"$param\", new FileData($paramName, {$paramName}_FileName))");
	// 		}
	// 	}
	// 	$this->appendLine("			return kfiles");
	// 	$this->appendLine("		}");

	// 	$this->appendLine();
	// 	$this->appendLine("		func ($requestBuilderName *$requestBuilderName) Deserialize(byte[] result) object {");
	// 	if ($resultType)
	// 	{
	// 		// TODO stopped here
	// 		switch ($resultType)
	// 		{
	// 			case "bigint":
	// 				$this->appendLine("			return result.Value<int64>();");
	// 				break;
	// 			case "int":
	// 				$this->appendLine("			return result.Value<int>();");
	// 				break;
	// 			case "float":
	// 				$this->appendLine("			return result.Value<float>();");
	// 				break;
	// 			case "bool":
	// 				$this->appendLine("			if (result.Value<string>().Equals(\"1\") || result.Value<string>().ToLower().Equals(\"true\"))");
	// 				$this->appendLine("				return true;");
	// 				$this->appendLine("			return false;");
	// 				break;
	// 			case "string":
	// 				$this->appendLine("			return result.Value<string>();");
	// 				break;
	// 			default:
	// 				$resultType = $this->getCSharpName($resultType);
	// 				$this->appendLine("			return ObjectFactory.Create<$resultType>(result);");
	// 				break;
	// 		}
	// 	}
	// 	else 
	// 	{
	// 		$this->appendLine("			return nil");
	// 	}
	// 	$this->appendLine("		}");
		
	// 	$this->appendLine("	}");
	// 	$this->appendLine();
		
	// }

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
				$dotNetOutputType = "";
				break;
			case "array":
				$arrayType = $resultNode->getAttribute("arrayType");
				$newName = $arrayType;
				if(str_contains($arrayType, 'Kaltura'))
				{
					$newName = "types.".$this->getCSharpName($arrayType);
					$isKalturaType = true;
				}
				$dotNetOutputType = "[]".$newName;
				break;
			case "map":
				$arrayType = $resultNode->getAttribute("arrayType");
				$dotNetOutputType = "map[string]".$arrayType;
				break;
			case "bigint":
				$dotNetOutputType = "*int64";
				break;
			case "int":
				$dotNetOutputType = "*int32";
				break;
			case "float":
				$dotNetOutputType = "*float32";
				break;
			case "bool":
				$dotNetOutputType = "*bool";
				break;
			case "string":
				$dotNetOutputType = "*string";
				break;
			case "KalturaStringValue":
				$dotNetOutputType = "*string";
				break;
			default:
				$resultGo = $this->getCSharpName($resultType);
				$dotNetOutputType = "*types.$resultGo";
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
		$text .= "$signaturePrefix(ctx context.Context, $signatureParamsWithTypes) (".$dotNetOutputType.$comaIfNeeded."error){\n";
		$text .= "	path := \"service/$serviceName/action/$action\"\n";
		$text .= "	requestMap := map[string]interface{}{}\n";

		foreach($signatureParamsWithoutTypes as $currParam)
		{
			$addedGetParams = ""	;

			// if(stripos($signatureParamsWithTypes, "$currParam types.".$serviceName) || stripos($signatureParamsWithTypes, "$currParam *types.".$serviceName))
			// {
			// 	$addedGetParams = ".GetParams()";
			// }

			if(str_contains($currParam, '*'))
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
			$text .= "      Result string `json:\"result\"`\n";
			$text .= "  }\n";
			$text .= "	err = json.Unmarshal(byteResponse, &result)\n";
			$text .= "	if err != nil {\n";
			$text .= "		return ".$nilIfNeeded."fmt.Errorf(\"failed to parse json: %w\", err)\n";
			$text .= "	}\n";
			$text .= "	return nil\n";
		} else
		{
			$text .= "	var result struct {\n";
			$text .= "      Result $dotNetOutputType `json:\"result\"`\n";
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

    private function getCSharpName($name)
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
			$enumPackage = strtolower($this->getCSharpName($paramNode->getAttribute("enumType")));

			if($isEnum && !in_array($enumPackage, $importedEnums))
			{
				// Importing enums to service
				$prefixText .= "	\"github.com/kaltura/KalturaOttGeneratedAPIClientsGo/kalturaclient/enums/$enumPackage\"\n";
				$importedEnums[] = $enumPackage;
			}

			switch($paramType)
			{
				case "array":
					$arrayType = $paramNode->getAttribute("arrayType");
					$newName = $arrayType;
					if(str_contains($arrayType, 'Kaltura'))
					{
						$newName = "types.".$this->getCSharpName($arrayType);
					}
					$dotNetType = "[]".$newName;
					break;
				case "map":
					$arrayType = $paramNode->getAttribute("arrayType");
					$newName = $arrayType;
					if(str_contains($arrayType, 'Kaltura'))
					{
						$newName = "types.".$this->getCSharpName($arrayType);
					}
					$dotNetType = "map[string]" . $newName;
					break;
				case "file":
					$dotNetType = "[]byte";
					break;
				case "bigint":
					$dotNetType = "int64";
					break;
				case "int":
					// TODO : ADD ENUM REPRESENTATION TO FUNC
					if ($isEnum)
						$dotNetType = $this->getCSharpName($paramNode->getAttribute("enumType"));
					else
						$dotNetType = "int32";
					break;
				case "float":
					$dotNetType = "float32";
						break;
				case "KalturaStringValue":
					$dotNetOutputType = "string";
					break;
				default:
					if ($isEnum)
						$dotNetType = $this->getCSharpName($paramNode->getAttribute("enumType"));
					else if(str_contains($paramType, 'Kaltura'))
					{
						$dotNetType = "types.".substr($paramType, 7);
					}
					else
					{
						$dotNetType = "$paramType";
					}
						
					break;
			}

			if($paramName == "type")
			{
				$paramName = $this->lowerCaseFirstLetter($dotNetType);
			}

			if($isEnum)
			{
				$dotNetType = $enumPackage.".".$dotNetType;
			}

			$param = "$paramName ";

			// If optional we need to add *
			if ($optional == "1")
			{
				$param .= "*$dotNetType";
				$paramsString[] = "*$paramName";
			} else 
			{
				$param .= "$dotNetType";
				$paramsString[] = "$paramName";
			}
							
			$params[] = "$param";
		}

		// if(strlen($params) > 1)
		// {
		// 	$params = substr($params, 0, -2);
		// }

		// if(strlen($paramsString) > 1)
		// {
		// 	$paramsString = substr($paramsString, 0, -2);
		// }

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
		
		return $this->getCSharpName($propertyNode->getAttribute("arrayType"));
	}

	private function writeClient()
	{
		$version = $this->_doc->documentElement->getAttribute('apiVersion'); //located at input file top
		$date = date('d-m-y');
		$text = 'package kalturaclient
		
		import (
			"bytes"
			"encoding/json"
			"io"
			"io/ioutil"
			"net/http"
			"net/url"
			"time"
			"context"
		)
		
		const (
			postMethod        = "POST"
			contentTypeHeader = "Content-Type"
			contentTypeJSON   = "application/json"
		)
		
		type HTTPClient interface {
			Do(request *http.Request) (response *http.Response, err error)
		}
		
		type Middleware interface {
			Execute(path string, request map[string]interface{}, extraParams []Param, headers []Header) ([]byte, error)
			SetNext(m Middleware)
		}
		
		// start ContextMiddleware ----------------------------------------
		type ContextMiddleware struct {
			Next Middleware
			ctx context.Context
		}
		func NewContextMiddleware(ctx context.Context) *ContextMiddleware{
			return &ContextMiddleware{
				ctx: ctx,
			}
		}
		func (p *ContextMiddleware) SetNext(m Middleware){
			p.Next = m
		}
		func (p *ContextMiddleware) Execute(path string, request map[string]interface{}, extraParams []Param, headers []Header) ([]byte, error) {
			requestId := p.ctx.Value(RequestIdHeader).(string)
			requestIdHeader := RequestId(requestId)
			headers = append(headers, requestIdHeader)
			return p.Next.Execute(path , request, extraParams, headers)
		}
		// end ContextMiddleware ----------------------------------------
		
		// start KSMiddleware ----------------------------------------
		// note: shared variable watch out - not thread safe! ???
		type KSMiddleware struct {
			Next Middleware
			ks string
		}
		func NewKSMiddleware(ks string) *KSMiddleware{
			return &KSMiddleware{
				ks: ks,
			}
		}
		func (p *KSMiddleware) SetNext(m Middleware){
			p.Next = m
		}
		func (p *KSMiddleware) Execute(path string, request map[string]interface{}, extraParams []Param, headers []Header) ([]byte, error) {
			ksParam := KS(p.ks)
			extraParams = append(extraParams, ksParam)
			return p.Next.Execute(path , request, extraParams, headers)
		}
		// end KSMiddleware ----------------------------------------
		
		type Client struct {
			middleware Middleware
		}
		
		func NewClient(config Configuration, logger Logger) *Client {
			return &Client{
				middleware: NewClientMiddleware(config, logger),
			}
		}
		
		// LoggingMiddl -> AuthenticationMiddle -> RequestIdAdder -> RealExecute
		func (p *Client) AddMiddleware(m Middleware) {
			m.SetNext(p.middleware)
			p.middleware = m
		}
		
		func (p *Client) Execute(path string, request map[string]interface{}, extraParams []Param) ([]byte, error) {
			return p.middleware.Execute(path, request, extraParams, nil)
		}
		
		// start ClientMiddleware ----------------------------------------
		type ClientMiddleware struct {
			Config        Configuration
			httpClient    HTTPClient
			baseUrl       string
			logger        Logger
			defaultParams map[string]interface{}
		}
		
		func NewClientMiddleware(config Configuration, logger Logger) *ClientMiddleware {
			var scheme string
			if config.UseHttps {
				scheme = "https"
			} else {
				scheme = "http"
			}
			var baseUrl = url.URL{
				Scheme: scheme,
				Host:   config.ServiceUrl,
			}
		
			return &ClientMiddleware{
				Config:     config,
				httpClient: newHttpClientFromConfig(config),
				logger:     logger,
				baseUrl:    baseUrl.String(),
				defaultParams: map[string]interface{}{
					"clientTag":  "go:'.$date.'",
					"apiVersion": "'.$version.'",
					"format":     "1",
					"language":   "*",
				},
			}
		}
		
		func newHttpClientFromConfig(config Configuration) HTTPClient {
			tr := &http.Transport{
				MaxConnsPerHost:     config.MaxConnectionsPerHost,
				MaxIdleConns:        config.MaxIdleConnections,
				MaxIdleConnsPerHost: config.MaxConnectionsPerHost,
				IdleConnTimeout:     time.Duration(config.IdleConnectionTimeoutMs) * time.Millisecond,
			}
			var httpClient HTTPClient = &http.Client{
				Transport: tr,
				Timeout:   time.Duration(config.TimeoutMs) * time.Millisecond,
			}
			return httpClient
		}
		
		func (p *ClientMiddleware) Execute(path string, request map[string]interface{}, extraParams []Param, headers []Header) ([]byte, error) {
			var requestUrl = p.baseUrl + "/api_v3/" + path
			requestBody := p.getRequestBodyBytes(request, extraParams)
			httpRequest, err := http.NewRequest(postMethod, requestUrl, bytes.NewBuffer(requestBody))
			if err != nil {
				p.logger.Errorf("[phoenix] create httpRequest error: %s", err.Error())
				return nil, FailedToCreateHttpRequest
			}
		
			httpRequest.Header.Set(contentTypeHeader, contentTypeJSON)
			if headers != nil {
				for _, v := range headers{
					httpRequest.Header.Set(v.key, v.value)
				}
			}
		
			httpResponse, err := p.httpClient.Do(httpRequest)
			if err != nil {
				p.logger.Errorf("[kaltura] httpRequest error: %s", err.Error())
				return nil, FailedToExecuteHttpRequest
			}
			defer p.closeIt(httpResponse.Body)
			if httpResponse.StatusCode != 200 {
		
				p.logger.Errorf("[kaltura] status code error: %d", httpResponse.StatusCode)
				return nil, NewBadStatusCodeError(httpResponse.StatusCode, httpResponse.Status)
			}
			byteResponse, err := ioutil.ReadAll(httpResponse.Body)
			if err != nil {
				p.logger.Errorf("[kaltura] read httpResponse error: %s", err.Error())
				return nil, FailedToReadResponseBody
			}
			apiException := p.getAPIExceptionFromResponse(byteResponse)
			return byteResponse, apiException
		}
		
		func (p *ClientMiddleware) SetNext(m Middleware){
		}
		
		func (p *ClientMiddleware) getAPIExceptionFromResponse(byteResponse []byte) error {
			var apiExceptionResult APIExceptionResult
			err := json.Unmarshal(byteResponse, &apiExceptionResult)
			if err != nil {
				p.logger.Errorf("[kaltura] parse json error: %s", err.Error())
				return FailedToParseJson
			}
			if apiExceptionResult.Result == nil {
				p.logger.Error("[kaltura] json without `result` field")
				return FailedToParseJson
			}
			if apiExceptionResult.Result.Error != nil {
				errorMessage := apiExceptionResult.Result.Error
				p.logger.Errorf("[kaltura] error response. code: %s, message: %s", errorMessage.Code, errorMessage.Message)
				return apiExceptionResult.Result.Error
			}
			return nil
		}
		
		func (p *ClientMiddleware) getRequestBodyBytes(request map[string]interface{}, extraParams []Param) []byte {
			for key, value := range p.defaultParams {
				request[key] = value
			}
			if extraParams != nil {
				for _, param := range extraParams {
					if param.wasCreatedUsingConstructor {
						request[param.key] = param.value
					}
				}
			}
			bytes, _ := json.Marshal(request)
			return bytes
		}
		
		func (p *ClientMiddleware) closeIt(c io.Closer) {
			if err := c.Close(); err != nil {
				p.logger.Error(err)
			}
		}';

		$file = "kalturaclient/client.go";
		$this->addFile($file, $text);
	}
}
