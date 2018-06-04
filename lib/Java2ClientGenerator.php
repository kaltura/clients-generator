<?php

class Java2ClientGenerator extends ClientGeneratorFromXml 
{
	private $_csprojIncludes = array();
	protected $_baseClientPath = "src/main/java/com/kaltura/client";
	
	function __construct($xmlPath, Zend_Config $config, $sourcePath = "java2")
	{
		parent::__construct($xmlPath, $sourcePath, $config);
	}
	
	function getSingleLineCommentMarker()
	{
		return '//';
	}
	
	public function generate() 
	{
		parent::generate();
		
		$xpath = new DOMXPath($this->_doc);
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
		foreach($serviceNodes as $serviceNode) 
		{
			$this->writeService($serviceNode);
		}
		
		$configurationNodes = $xpath->query("/xml/configurations/*");
		$this->writeMainClient($serviceNodes, $configurationNodes);
		$this->writeRequestBuilderData($configurationNodes);
	}
	
	//Private functions
	/////////////////////////////////////////////////////////////
	private function addDescription($propertyNode, $prefix) {
		
		if($propertyNode->hasAttribute("description"))
		{
			$desc = $propertyNode->getAttribute("description");
			$desc = str_replace(array("&", "<", ">"), array("&amp;", "&lt;", "&gt;"), $desc);
			$formatDesc = wordwrap(str_replace(array("\t", "\n", "\r"), " ", $desc), 80, "\n" . $prefix . "  ");
			if($desc)
			{
				$ret = "$prefix/**\n";
				$ret .= "$prefix * $formatDesc\n";
			
				$paramNodes = $propertyNode->getElementsByTagName("param");
				if($paramNodes->length)
				{
					$ret .= "$prefix * \n";
				}
				
				foreach($paramNodes as $paramNode) 
				{
					$paramName = $paramNode->getAttribute("name");
					$paramDesc = $paramNode->getAttribute("description");
					$paramDesc = str_replace(array("&", "<", ">"), array("&amp;", "&lt;", "&gt;"), $paramDesc);
					$paramDesc = str_replace(array("\t", "\n", "\r"), array('', "\n$prefix * ", ''), wordwrap($paramDesc, 80, "\n"));
					
					$argName = $this->replaceReservedWords($paramName);
				
					$ret .= "$prefix * @param $argName $paramDesc\n";
				}
				
				$ret .= "$prefix */";
				return $ret;
			}
		}
		return "";
	}
	
	function writeEnum(DOMElement $enumNode) 
	{
		$enumName = $enumNode->getAttribute("name");
		if(!$this->shouldIncludeType($enumName) || $enumName === 'KalturaNullableBoolean')
			return;
		
		$enumName = $this->getJavaTypeName($enumName);
		
		$enumType = $enumNode->getAttribute("enumType");
		$baseInterface = ($enumType == "string") ? "EnumAsString": "EnumAsInt";
		
		$str = "";
		$str = "package com.kaltura.client.enums;\n";
		$str .= "\n";
		$str .= $this->getBanner();
		
		$desc = $this->addDescription($enumNode, "");
		if($desc)
			$str .= $desc . "\n";
		$str .= "public enum $enumName implements $baseInterface {\n";
		
		// Print enum values
		$enumCount = $this->generateEnumValues($enumNode, $str);
		
		// Generate hash code function
		$this->generateEnumValueFunctions($str, $enumType, $enumName);
		
		// Generate get function if needed
		if($enumCount) 
		{
			$this->generateEnumGetFunction($str, $enumNode, $enumType,  $enumName);
		}
		else 
		{
			$this->generateEmptyEnumGetFunction($str, $enumNode, $enumType,  $enumName);
		}
		
		$str .= "}\n";
		$file = $this->_baseClientPath . "/enums/$enumName.java";
		$this->addFile($file, $str);
	}
	
	function generateEnumValues($enumNode, &$str)
	{
		$enumType = $enumNode->getAttribute("enumType");
		$enumCount = 0;
		$enumValues = array();
		$processedValues = array();
		
		foreach($enumNode->childNodes as $constNode)
		{
			if($constNode->nodeType != XML_ELEMENT_NODE)
				continue;
				
			$propertyName = $constNode->getAttribute("name");
			$propertyValue = $constNode->getAttribute("value");
			
			if(in_array($propertyValue, $processedValues))
				continue;			// Java does not allow duplicate values in enums
			$processedValues[] = $propertyValue;

			if($enumType == "string")
			{
				$propertyValue = "\"$propertyValue\"";
			}
			$enumValues[] = "$propertyName($propertyValue)";
		}
		
		if(count($enumValues) == 0)
			$str .= "	/** Place holder for future values */";
		else  {
			$enums = implode(",\n	", $enumValues);
			$str .= "	$enums";
		}
		
		$str .= ";\n\n";
		return count($enumValues);
	}
	
	function generateEnumValueFunctions(&$str, $enumType, $enumName)
	{
		$type = 'int';
		if($enumType == "string"){
			$type = 'String';
		}

		$str .= "	private $type value;\n\n";
		$str .= "	$enumName($type value) {\n";
		$str .= "		this.value = value;\n";
		$str .= "	}\n\n";
		$str .= "	@Override\n";
		$str .= "	public $type getValue() {\n";
		$str .= "		return this.value;\n";
		$str .= "	}\n\n";
		$str .= "	public void setValue($type value) {\n";
		$str .= "		this.value = value;\n";
		$str .= "	}\n\n";
	}
		
	function generateEmptyEnumGetFunction(&$str, $enumNode, $enumType,  $enumName) 
	{
		$str .= "	public static $enumName get(String value) {\n";
		$str .= "		return null;\n";
		$str .= "	}\n";
	}


	function generateEnumGetFunction(&$str, $enumNode, $enumType,  $enumName){
		$type = 'Integer';
		if($enumType == "string"){
			$type = 'String';
		}

		$str .= "	public static $enumName get($type value) {\n";
		$str .= "		if(value == null)\n";
		$str .= "		{\n";
		$str .= "			return null;\n";
		$str .= "		}\n";
		$str .= "		\n";

		$str .= "		// goes over $enumName defined values and compare the inner value with the given one:\n";
		$str .= "		for($enumName item: values()) {\n";
		if($enumType == "string") {
			$str .= "			if(item.getValue().equals(value)) {\n";
		} else {
			$str .= "			if(item.getValue() == value) {\n";
		}
		$str .= "				return item;\n";
		$str .= "			}\n";
		$str .= "		}\n";

		$str .= "		// in case the requested value was not found in the enum values, we return the first item as default.\n";
		$str .= "		return $enumName.values().length > 0 ? $enumName.values()[0]: null;\n";

		$str .= "   }\n";

	}

	function writeClass(DOMElement $classNode) 
	{
		$type = $classNode->getAttribute("name");
		if(!$this->shouldIncludeType($type) || $type === 'KalturaObject' || preg_match('/ListResponse$/', $type))
			return;
		
		$type = $this->getJavaTypeName($type);
		
		// File name
		$file = $this->_baseClientPath . "/types/$type.java";
		
		// Basic imports
		$imports = array();
		$imports[] = "import com.kaltura.client.Params;";
		$imports[] = "import com.kaltura.client.utils.request.MultiRequestBuilder;";

		// Add Banner
		$this->startNewTextBlock();
		$this->appendLine("");
		$this->appendLine($this->getBanner());
		
		$desc = $this->addDescription($classNode, "");
		if($desc)
			$this->appendLine($desc);
		
		// class definition
		$abstract = '';
		if($classNode->hasAttribute("abstract"))
			$abstract = ' abstract';
		
		$needsSuperConstructor = false;
		$this->appendLine('@SuppressWarnings("serial")');
		$this->appendLine("@MultiRequestBuilder.Tokenizer($type.Tokenizer.class)");
		$baseClass = 'ObjectBase';
		if($classNode->hasAttribute("base")) 
		{
			$baseClass = $this->getJavaTypeName($classNode->getAttribute("base"));
			$needsSuperConstructor = true;
		} 
		else 
		{
			$imports[] = "import com.kaltura.client.types.ObjectBase;";
		}
		$this->appendLine("public{$abstract} class $type extends $baseClass {");

		$hasProperties = $this->generateMultiRequestTokens($imports, $classNode, $baseClass);
		
		if($hasProperties) {
			$imports[] = "import com.kaltura.client.utils.GsonParser;";
		}
		
		// Generate parameters declaration
		$this->generateParametersDeclaration($imports, $classNode);
		$this->appendLine("");
		//$this->appendLine("");

		// Generate empty constructor
		$this->appendLine("	public $type() {");
		$this->appendLine("		super();");
		$this->appendLine("	}");
		$this->appendLine("");
		
		// Generate Full constructor
		$this->generateJsonConstructor($imports, $classNode, $needsSuperConstructor);
		$this->appendLine("");

		// Generate to params method
		$this->generateToParamsMethod($classNode);
		$this->appendLine("");

		$this->finalizeClass($imports, $classNode);

		$package = "package com.kaltura.client.types;\n\n";
		sort($imports);
		$imports = implode("\n", array_unique($imports));
		$this->addFile($file, $package . $imports . "\n" . $this->getTextBlock());
	}
	
	public function generateMultiRequestTokens(array &$imports, $classNode, $baseClass) {
		$this->appendLine("	");
		$this->appendLine("	public interface Tokenizer extends $baseClass.Tokenizer {");

		$hasProperties = false;
		foreach($classNode->childNodes as $propertyNode)
		{
			if($propertyNode->nodeType != XML_ELEMENT_NODE) {
				continue;
			}

			$propReadOnly = $propertyNode->getAttribute("readOnly");
			if($propReadOnly == "1") {
			    continue;
			}
			
			$hasProperties = true;
			$propName = $this->replaceReservedWords($propertyNode->getAttribute("name"));
			$propType = $propertyNode->getAttribute("type");
			
			if($this->isSimpleType($propType)) {
				$this->appendLine("		String $propName();");
			}
			elseif ($propType == 'array') {
				$arrayType = $this->getJavaTypeName($propertyNode->getAttribute("arrayType"));
				$this->appendLine("		RequestBuilder.ListTokenizer<$arrayType.Tokenizer> $propName();");
				$imports[] = 'import com.kaltura.client.utils.request.RequestBuilder;';
			}
			elseif ($propType == 'map') {
				$arrayType = $this->getJavaTypeName($propertyNode->getAttribute("arrayType"));
				$this->appendLine("		RequestBuilder.MapTokenizer<$arrayType.Tokenizer> $propName();");
				$imports[] = 'import com.kaltura.client.utils.request.RequestBuilder;';
			}
			elseif (preg_match('/ListResponse$/', $propType)) {
				$arrayType = $this->getJavaTypeName($propertyNode->getAttribute("arrayType"));
				$this->appendLine("		ListResponse.Tokenizer<$arrayType.Tokenizer> $propName();");
				$imports[] = 'import com.kaltura.client.types.ListResponse;';
			}
			else {
				$propType = $this->getJavaTypeName($propType);
				$this->appendLine("		$propType.Tokenizer $propName();");
			}
		}
		
		$this->appendLine("	}");
		
		return $hasProperties;
	}
	
	public function generateParametersDeclaration(array &$imports, $classNode) {

		$needsArrayList = false;
		$needsHashMap = false;
		$arrImportsEnums = array();
		$arrFunctions = array();

		$this->appendLine("");
		foreach($classNode->childNodes as $propertyNode) 
		{
			if($propertyNode->nodeType != XML_ELEMENT_NODE)
				continue;
			
			$propName = $propertyNode->getAttribute("name");
			$propType = $propertyNode->getAttribute("type");
			$isEnum = $propertyNode->hasAttribute("enumType");
			
			$javaType = $this->getJavaType($propertyNode, true);

			if($isEnum && $javaType != 'Boolean') 
				$arrImportsEnums[] = $this->getJavaTypeName($javaType); 
			
			if($propType == "array")
				$needsArrayList = true;
			
			if($propType == "map")
				$needsHashMap = true;
				
			if(strpos($propType, 'Kaltura') === 0)
			{
				$propType = $this->getJavaTypeName($propType);
				$imports[] =  "import com.kaltura.client.types.$propType;";
			}
						
			$functionName = ucfirst($propName);
			$arrFunctions[] = "	// $propName:";
			$arrFunctions[] = "	public $javaType get{$functionName}(){";
			$arrFunctions[] = "		return this.$propName;";
			$arrFunctions[] = "	}";
			
			$propReadOnly = $propertyNode->getAttribute("readOnly");
			if($propReadOnly != "1")
			{
			    $arrFunctions[] = "	public void set{$functionName}($javaType $propName){";
			    $arrFunctions[] = "		this.$propName = $propName;";
			    $arrFunctions[] = "	}\n";
			    if($this->isSimpleType($propType)) {
			        $arrFunctions[] = "	public void $propName(String multirequestToken){";
			        $arrFunctions[] = "		setToken(\"$propName\", multirequestToken);";
			        $arrFunctions[] = "	}\n";
			    }
			}
			
			$propertyLine = "private $javaType $propName";
			
// 			$initialValue = $this->getInitialPropertyValue($propertyNode);
// 			if($initialValue != "") 
// 				$propertyLine .= " = " . $initialValue;
			
			$desc = $this->addDescription($propertyNode,"\t");
			if($desc)
				$this->appendLine($desc);
			
			$this->appendLine("	$propertyLine;");
		}

		$this->appendLine("");
		foreach($arrFunctions as $arrFunctionsLine){		
			$this->appendLine($arrFunctionsLine);
		}
		
		$arrImportsEnums = array_unique($arrImportsEnums);
		foreach($arrImportsEnums as $import) 
			$imports[] = "import com.kaltura.client.enums.$import;";
		
		if($needsArrayList) {
			$imports[] = "import java.util.List;";
		}
		if($needsHashMap) {
			$imports[] = "import java.util.Map;";
		}
	}
	
	public function generateToParamsMethod($classNode) 
	{	
		$type = $classNode->getAttribute("name");
		$this->appendLine("	public Params toParams() {");//throws APIException
		$this->appendLine("		Params kparams = super.toParams();");
		$this->appendLine("		kparams.add(\"objectType\", \"$type\");");
		
		foreach($classNode->childNodes as $propertyNode) 
		{
			if($propertyNode->nodeType != XML_ELEMENT_NODE)
				continue;
			
			$propReadOnly = $propertyNode->getAttribute("readOnly");
			if($propReadOnly == "1")
				continue;
			
			$propType = $propertyNode->getAttribute("type");
			$propName = $propertyNode->getAttribute("name");
			$this->appendLine("		kparams.add(\"$propName\", this.$propName);");
		}
		$this->appendLine("		return kparams;");
		$this->appendLine("	}");
	}

	public function generateJsonConstructor(array &$imports, $classNode, $needsSuperConstructor)
	{
		$imports[] = "import com.google.gson.JsonObject;";

		$type = $this->getJavaTypeName($classNode->getAttribute("name"));
		$this->appendLine("	public $type(JsonObject jsonObject) throws APIException {");
		$this->appendLine("		super(jsonObject);");

		if($classNode->childNodes->length)
		{
			$this->appendLine("\n		if(jsonObject == null) return;\n");


			/*$this->appendLine("		$type temp = gson.fromJson(json, $type.class);\n");
			$this->appendLine("		if(temp == null) return;\n");*/

			$propBlock = "		// set members values:\n";

			foreach($classNode->childNodes as $propertyNode)
			{
				if($propertyNode->nodeType != XML_ELEMENT_NODE)
					continue;

				$propName = $propertyNode->getAttribute("name");
				$propType = $propertyNode->getAttribute("type");

				$propBlock .= "		$propName = ".$this->getPropertyValue($propName, $propType, $propertyNode).";\n";

			}

			$this->appendLine($propBlock);
		}
		$this->appendLine("	}");
	}
	
	public function getPropertyValue($propName, $propType, $propertyNode) {
		$propEnumType = null;
		$primitiveType = "";

		switch($propType) {
			case "bigint":
			case "time":
				$primitiveType = "long";
				break;
			case "bool":
				$primitiveType = "boolean";
				break;
			case "float":
				$primitiveType = "double";
				break;
			case "int":
			case "string":
				$primitiveType = $propType;
				$propEnumType = $propertyNode->hasAttribute("enumType") ? $this->getJavaTypeName($propertyNode->getAttribute("enumType")): null;
				if($propEnumType === 'Boolean') 
				{
					$primitiveType = 'boolean';
					$propEnumType = null;
				}
			break;

			case "map":
				$propArrayType = $this->getJavaTypeName($propertyNode->getAttribute("arrayType"));
				return "GsonParser.parseMap(jsonObject.getAsJsonObject(\"".$propName."\"), ". $propArrayType.".class)";
				break;

			case "array":
				$propArrayType = $this->getJavaTypeName($propertyNode->getAttribute("arrayType"));
				return "GsonParser.parseArray(jsonObject.getAsJsonArray(\"".$propName."\"), ". $propArrayType.".class)";
				break;

			/*case "enum":
				$propEnumType = $propertyNode->hasAttribute("enumType") ? $propertyNode->getAttribute("enumType"): null;
				break;*/
		}

		if($primitiveType != ""){
			$methodName = $this->upperCaseFirstLetter($primitiveType);
			$parsedProperty = "GsonParser.parse{$methodName}(jsonObject.get(\"".$propName."\"))";
			if($propEnumType != null) {
				$parsedProperty = $propEnumType . ".get(".$parsedProperty.")";
			}
			return $parsedProperty;

		} else {
			$propType = $this->getJavaTypeName($propType);
			return "GsonParser.parseObject(jsonObject.getAsJsonObject(\"$propName\"), $propType.class)";
		}

	}


	/**
	 * @param propType
	 */
	public function handleSinglePropByType($propertyNode, &$propBlock, &$txtIsUsed) {
		
		$propType = $this->getJavaTypeName($propertyNode->getAttribute("type"));
		$propName = $propertyNode->getAttribute("name");
		$isEnum = $propertyNode->hasAttribute("enumType");
		$propBlock .= "this.$propName = ";
		
		switch($propType) 
		{
			case "bigint":
			case "time":
			case "int":
			case "string":
			case "bool":
			case "float":
				if($propType == "float")
				{
					$propType = "double";
				}
				if($propType == "time")
				{
					$propType = "long";//"bigint";
				}

				$txtIsUsed = true;
				$parsedProperty = "GsonParser.parse".ucfirst($propType)."(txt)";
				if($isEnum) 
				{
					$enumType = $this->getJavaTypeName($propertyNode->getAttribute("enumType"));
					$propBlock .= "$enumType.get($parsedProperty);\n";
				} 
				else
				{
					$propBlock .= "$parsedProperty;\n";
				}
				break;
				
			case "array":
				$arrayType = $this->getJavaTypeName($propertyNode->getAttribute("arrayType"));
				$propBlock .= "GsonParser.parseArray(aNode, $arrayType.class);\n";
				break;
				
			case "map":
				$arrayType = $this->getJavaTypeName($propertyNode->getAttribute("arrayType"));
				$propBlock .= "GsonParser.parseMap(aNode, $arrayType.class);\n";
				break;
				
			default: // sub object
				$propBlock .= "GsonParser.parseObject(aNode, $propType.class);\n";
				break;
		}
	}

	function writeService(DOMElement $serviceNode) 
	{
		$serviceId = $serviceNode->getAttribute("id");
		if(!$this->shouldIncludeService($serviceId))
			return;

		$imports = "";
		$imports .= "package com.kaltura.client.services;\n\n";
		$serviceName = $serviceNode->getAttribute("name");
		
		$javaServiceName = $this->upperCaseFirstLetter($serviceName) . "Service";
		$javaServiceType = $javaServiceName;
		
		$this->startNewTextBlock();
		$this->appendLine();
		$this->appendLine($this->getBanner());
		$desc = $this->addDescription($serviceNode, "");
		if($desc)
			$this->appendLine($desc);
		
		$this->appendLine("public class $javaServiceType {");
		
		$actionNodes = $serviceNode->childNodes;
		$serviceImports = array();

		foreach($actionNodes as $actionNode)
		{
			if($actionNode->nodeType != XML_ELEMENT_NODE)
				continue;
					
				try
				{
					$this->writeRequestBuilder($serviceId, $serviceName, $actionNode, $serviceImports);
					$this->writeAction($serviceId, $serviceName, $actionNode, $serviceImports);
				}
				catch(Exception $e)
				{
					KalturaLog::err($e->getMessage());
				}
		}
		
		$this->appendLine("}");
		
		// Update imports
		$serviceImports = array_unique($serviceImports);
		sort($serviceImports);
		foreach($serviceImports as $import) 
			$imports .= "import $import;\n";
		
		$file = $this->_baseClientPath . "/services/" . $javaServiceType . ".java";
		$this->addFile($file, $imports . $this->getTextBlock());
	}
	
	function getListResponseInternalType($listResponseType) 
	{
		$xpath = new DOMXPath($this->_doc);
		$objectsNodes = $xpath->query("/xml/classes/class[@name = 'Kaltura{$listResponseType}']/property[@name = 'objects']");
		if(!$objectsNodes->length)
		{
			throw new Exception('List response type [$listResponseType] has no objects array property');
		}
		
		$objectsNode = $objectsNodes->item(0);
		if(!$objectsNode->hasAttribute('arrayType'))
		{
			throw new Exception('List response type [$listResponseType] objects has no array type');
		}
		
		return $this->getJavaTypeName($objectsNode->getAttribute('arrayType'));
	}
	
	function writeRequestBuilder($serviceId, $serviceName, DOMElement $actionNode, &$serviceImports)
	{
		$action = $actionNode->getAttribute("name");
		if(!$this->shouldIncludeAction($serviceId, $action)) {
			return;
		}

		$builderName = ucfirst($action) . ucfirst($serviceName) . 'Builder';

		$resultNode = $actionNode->getElementsByTagName("result")->item(0);
		$resultType = $this->getJavaTypeName($resultNode->getAttribute("type"));
		
		$arrayType = '';
		$fallbackClass = null;
		
		if($resultType) {
    		$fallbackClass = $this->getObjectType($resultType);
		}
    	
		if($resultType == "file") {
    		$fallbackClass = null;
		}
		
		if($resultType == "array") {
			$arrayType = $this->getJavaTypeName($resultNode->getAttribute("arrayType"));
			$fallbackClass = $arrayType;
		}
		elseif($resultType == "map") {
			$arrayType = $this->getJavaTypeName($resultNode->getAttribute("arrayType"));
			$fallbackClass = $arrayType;
		}
    	elseif($resultType && ($resultType != 'file') && !$this->isSimpleType($resultType))
    	{
    		if(preg_match('/ListResponse$/', $resultType))
	    	{
	    		$arrayType = $this->getListResponseInternalType($resultType);
	    		$resultType = 'ListResponse';
				$fallbackClass = $arrayType;
	    	}
    	}
		
		$javaOutputType = $this->getResultType($resultType, $arrayType, $builderName, $serviceImports);

		$paramNodes = $actionNode->getElementsByTagName("param");
		$paramNodesArr = array();
		foreach($paramNodes as $paramNode)
		{
			$paramNodesArr[] = $paramNode;
		}
		
		$signature = $this->getSignature($paramNodesArr, array('' => 'FileHolder'), $serviceImports, true);

		$this->appendLine("	");
		$this->appendLine("	public static class $builderName extends $javaOutputType {");
		$this->appendLine("		");
		$this->appendLine("		public $builderName($signature {");
		if($fallbackClass) {
			$this->appendLine("			super($fallbackClass.class, \"$serviceId\", \"$action\");");
		}
		else {
			$this->appendLine("			super(\"$serviceId\", \"$action\");");
		}
		
		$haveFiles = false;
		foreach($paramNodes as $paramNode)
		{
			$paramType = $paramNode->getAttribute("type");
			$paramName = $paramNode->getAttribute("name");
			$argName = $this->replaceReservedWords($paramName);
		
			if($haveFiles === false && $paramType === "file")
			{
				$serviceImports[] = "com.kaltura.client.Files";
				$serviceImports[] = "com.kaltura.client.FileHolder";
				$haveFiles = true;
				$this->appendLine("			files = new Files();");
			}
				
			if($paramType == "file")
			{
				$this->appendLine("			files.add(\"$paramName\", $argName);");
			}
			else
				$this->appendLine("			params.add(\"$paramName\", $argName);");
		}
		$this->appendLine("		}");
		
		foreach($paramNodes as $paramNode)
		{
			$paramType = $paramNode->getAttribute("type");
			$paramName = $paramNode->getAttribute("name");
			
			if(!$this->isSimpleType($paramType)) {
				continue;
			}

			$this->appendLine("		");
			$this->appendLine("		public void $paramName(String multirequestToken) {");
			$this->appendLine("			params.add(\"$paramName\", multirequestToken);");
			$this->appendLine("		}");
		}
		$this->appendLine("	}");
	}
	
	function writeAction($serviceId, $serviceName, DOMElement $actionNode, &$serviceImports) 
	{
		$action = $actionNode->getAttribute("name");
		if(!$this->shouldIncludeAction($serviceId, $action))
			return;

		$builderName = ucfirst($action) . ucfirst($serviceName) . 'Builder';
		$action = $this->replaceReservedWords($action);
		
		$signaturePrefix = "public static $builderName $action(";

		$paramNodes = $actionNode->getElementsByTagName("param");
		$paramNodesArr = array();
		foreach($paramNodes as $paramNode) 
		{
			$paramNodesArr[] = $paramNode;
		}
		
		$this->writeActionOverloads($signaturePrefix, $action, $paramNodesArr, $serviceImports);
		
		$signature = $this->getSignature($paramNodesArr, array('' => 'FileHolder'), $serviceImports);
		
		$this->appendLine();
		
		$desc = $this->addDescription($actionNode, "\t");
		if($desc)
			$this->appendLine($desc);
		$this->appendLine("    $signaturePrefix$signature  {");//throws APIException
		

		$params = array();
		foreach($paramNodesArr as $paramNode)
		{
			$paramName = $paramNode->getAttribute("name");
			$params[] = $paramName;
		}
		$paramsStr = implode(', ', $params);
		
		$this->appendLine("		return new $builderName($paramsStr);");
		$this->appendLine("	}");
	}

	public function writeActionOverloads($signaturePrefix, $action, $paramNodes, &$serviceImports)
	{
		// split the parameters into mandatory and optional
		$mandatoryParams = array();
		$optionalParams = array();
		foreach($paramNodes as $paramNode) 
		{
			$optional = $paramNode->getAttribute("optional");
			if($optional == "1")
				$optionalParams [] = $paramNode;
			else
				$mandatoryParams [] = $paramNode;
		}
		
		for($overloadNumber = 0; $overloadNumber < count($optionalParams) + 1; $overloadNumber ++) 
		{
			$prototypeParams = array_slice($paramNodes, 0, count($mandatoryParams) + $overloadNumber);
			$callParams = array_slice($paramNodes, 0, count($mandatoryParams) + $overloadNumber + 1);
			
			// find which file overloads need to be generated
			$hasFiles = false;
			foreach($prototypeParams as $paramNode)
			{
				if($paramNode->getAttribute("type") == "file") {
					$hasFiles = true;
					break;
				}
			}

			if($hasFiles)
			{
				$fileOverloads = array(   
					array('' => 'FileHolder'),
					array('' => 'File'),
					array('' => 'InputStream', 'MimeType' => 'String', 'Name' => 'String', 'Size' => 'long'),
					array('' => 'FileInputStream', 'MimeType' => 'String', 'Name' => 'String'),
				);
			}
			else
			{
				$fileOverloads = array(
					array('' => 'FileHolder'),
				);
			}

			foreach($fileOverloads as $fileOverload)
			{
				if(reset($fileOverload) == 'FileHolder' && $overloadNumber == count($optionalParams))
					continue;			// this is the main overload
				
				// build the function prototype
				$signature = $this->getSignature($prototypeParams, $fileOverload, $serviceImports);
								
				// build the call parameters
				$params = array();
				foreach($callParams as $paramNode) 
				{
					$optional = $paramNode->getAttribute("optional");
					$paramName = $paramNode->getAttribute("name");
					$paramType = $paramNode->getAttribute("type");
					
					if(/*$optional == "1" &&*/ ! in_array($paramNode, $prototypeParams, true))
					{
						$params[] = $this->getDefaultParamValue($paramNode);
						continue;
					} 
						
					if($paramType != "file" || reset($fileOverload) == 'FileHolder')
					{
						$params[] = $paramName;
						continue;
					}
					
					$fileParams = array();
					foreach($fileOverload as $namePostfix => $paramType)
					{
						$fileParams[] = $paramName . $namePostfix;
					}
					$params[] = "new FileHolder(" . implode(', ', $fileParams) . ")";
				}				
				$paramsStr = implode(', ', $params);
				
				// write the result
				$this->appendLine();
				$this->appendLine("	$signaturePrefix$signature  {"); // throws APIException
				$this->appendLine("		return $action($paramsStr);");
				$this->appendLine("	}");
			}
		}
	}
	
	function writeRequestBuilderData(DOMNodeList $configurationNodes)
	{
		$imports = "";
		$imports .= "package com.kaltura.client.utils.request;\n";
		$imports .= "\n";
		$imports .= "import com.kaltura.client.Params;\n";
		
		$this->startNewTextBlock();
		$this->appendLine($this->getBanner());
		$this->appendLine("public abstract class RequestBuilderData<SelfType> {");
		$this->appendLine("	");
		$this->appendLine("	protected Params params = new Params();");
		$this->appendLine("	");
		$this->appendLine("	protected RequestBuilderData() {");
		$this->appendLine("	}");
		$this->appendLine("	");
		
	
		//$volatileProperties = array();
		foreach($configurationNodes as $configurationNode)
		{
			/* @var $configurationNode DOMElement */
			$configurationName = $configurationNode->nodeName;
			$attributeName = lcfirst($configurationName) . "Configuration";

			$constantsPropertiesKeys = "";

			foreach($configurationNode->childNodes as $configurationPropertyNode)
			{
				/* @var $configurationPropertyNode DOMElement */
				
				if($configurationPropertyNode->nodeType != XML_ELEMENT_NODE)
					continue;
			
				$configurationProperty = $configurationPropertyNode->localName;

				$constantsPropertiesKeys .= "public static final String ". ucwords($configurationName)." = \"$configurationName\";\n";

				$type = $configurationPropertyNode->getAttribute("type");
				if(!$this->isSimpleType($type) && !$this->isArrayType($type))
				{
					$type = $this->getJavaTypeName($type);
					$imports .= "import com.kaltura.client.types.$type;\n";
				}
				
				$type = $this->getJavaType($configurationPropertyNode, true);
				$description = null;
				
				if($configurationPropertyNode->hasAttribute('description'))
				{
					$description = $configurationPropertyNode->getAttribute('description');
				}
				
				$this->writeConfigurationParam($configurationProperty, $configurationProperty, $type, $description);
				
				if($configurationPropertyNode->hasAttribute('alias'))
				{
					$this->writeConfigurationParam($configurationPropertyNode->getAttribute('alias'), $configurationProperty, $type, $description);					
				}
			}
		}
		
		$this->appendLine("}");
		
		$imports .= "\n";
		
		$this->addFile($this->_baseClientPath . "/utils/request/RequestBuilderData.java", $imports . $this->getTextBlock());
	}
	
	function writeMainClient(DOMNodeList $serviceNodes, DOMNodeList $configurationNodes) 
	{
		$apiVersion = $this->_doc->documentElement->getAttribute('apiVersion'); //located at input file top
		$date = date('y-m-d');
		
		$imports = "";
		$imports .= "package com.kaltura.client;\n";
		$imports .= "\n";
		$imports .= "import com.kaltura.client.utils.request.ConnectionConfiguration;\n";
		
		$this->startNewTextBlock();
		$this->appendLine($this->getBanner());
		$this->appendLine('@SuppressWarnings("serial")');
		$this->appendLine("public class Client extends ClientBase {");
		$this->appendLine("	");
		$this->appendLine("	public Client(ConnectionConfiguration config) {");
		$this->appendLine("		super(config);");
		$this->appendLine("		");
		$this->appendLine("		this.setClientTag(\"java:$date\");");
		$this->appendLine("		this.setApiVersion(\"$apiVersion\");");
		$this->appendLine("		this.clientConfiguration.put(\"format\", 1); // JSON");
		$this->appendLine("	}");
		$this->appendLine("	");
		
	
		//$volatileProperties = array();
		foreach($configurationNodes as $configurationNode)
		{
			/* @var $configurationNode DOMElement */
			$configurationName = $configurationNode->nodeName;
			$attributeName = lcfirst($configurationName) . "Configuration";

			$constantsPropertiesKeys = "";

			foreach($configurationNode->childNodes as $configurationPropertyNode)
			{
				/* @var $configurationPropertyNode DOMElement */
				
				if($configurationPropertyNode->nodeType != XML_ELEMENT_NODE)
					continue;
			
				$configurationProperty = $configurationPropertyNode->localName;

				$constantsPropertiesKeys .= "public static final String ". ucwords($configurationName)." = \"$configurationName\";\n";

				$type = $configurationPropertyNode->getAttribute("type");
				if(!$this->isSimpleType($type) && !$this->isArrayType($type))
				{
					$type = $this->getJavaTypeName($type);
					$imports .= "import com.kaltura.client.types.$type;\n";
				}
				
				$type = $this->getJavaType($configurationPropertyNode, true);
				$description = null;
				
				if($configurationPropertyNode->hasAttribute('description'))
				{
					$description = $configurationPropertyNode->getAttribute('description');
				}
				
				$this->writeConfigurationProperty($configurationName, $configurationProperty, $configurationProperty, $type, $description);
				
				if($configurationPropertyNode->hasAttribute('alias'))
				{
					$this->writeConfigurationProperty($configurationName, $configurationPropertyNode->getAttribute('alias'), $configurationProperty, $type, $description);					
				}
			}
		}
		
		$this->appendLine("}");
		
		$imports .= "\n";
		
		$this->addFile($this->_baseClientPath . "/Client.java", $imports . $this->getTextBlock());
	}
	
	protected function writeConfigurationParam($name, $paramName, $type, $description)
	{
		$methodsName = ucfirst($name);
		
		$this->appendLine("	/**");
		$this->appendLine("	 * @param $name $description");
		$this->appendLine("	 */");
		$this->appendLine("	public SelfType set{$methodsName}($type $name){");
		$this->appendLine("		params.add(\"$paramName\", $name);");
		$this->appendLine("		return (SelfType) this;");
		$this->appendLine("	}");
		$this->appendLine("	");
	}
	
	protected function writeConfigurationProperty($configurationName, $name, $paramName, $type, $description)
	{
		$methodsName = ucfirst($name);
		
		$this->appendLine("	/**");
		$this->appendLine("	 * @param $name $description");
		$this->appendLine("	 */");
		$this->appendLine("	public void set{$methodsName}($type $name){");
		$this->appendLine("		this.{$configurationName}Configuration.put(\"$paramName\", $name);");
		$this->appendLine("	}");
		$this->appendLine("	");
	
		
		$this->appendLine("	/**");
		if($description)
		{
			$this->appendLine("	 * $description");
			$this->appendLine("	 * ");
		}
		$this->appendLine("	 * @return $type");
		$this->appendLine("	 */");
		$this->appendLine("	public $type get{$methodsName}(){");
		$this->appendLine("		if(this.{$configurationName}Configuration.containsKey(\"{$paramName}\")){");
		$this->appendLine("			return($type) this.{$configurationName}Configuration.get(\"{$paramName}\");");
		$this->appendLine("		}");
		$this->appendLine("		");
		$this->appendLine("		return ".$this->getDefaultValue($type).";");
		$this->appendLine("	}");
		$this->appendLine("	");
	}
	
	function getSignature($paramNodes, $fileOverload, &$serviceImports, $replaceReservedWords = false) 
	{
		$signature = array();
		foreach($paramNodes as $paramNode) 
		{
			$paramType = $paramNode->getAttribute("type");
			$paramName = $paramNode->getAttribute("name");
			$arrayType = $paramNode->getAttribute("arrayType");
			$enumType = $paramNode->getAttribute("enumType");
			if($replaceReservedWords) {
				$paramName = $this->replaceReservedWords($paramName);
			}

			if($paramType == "array")
			{
				$arrayType = $this->getJavaTypeName($arrayType);
				$serviceImports[] = "java.util.List";
				$serviceImports[] = "com.kaltura.client.types.$arrayType";
			}	
			elseif($paramType == "map")
			{
				$arrayType = $this->getJavaTypeName($arrayType);
				$serviceImports[] = "java.util.Map";
				$serviceImports[] = "com.kaltura.client.types.$arrayType";
			}	
			elseif($enumType)
			{
				$enumType = $this->getJavaTypeName($enumType);
				if($enumType != 'Boolean') 
				{
					$serviceImports[] = "com.kaltura.client.enums.$enumType";
				}
			}
			
			if($paramType == "file")
			{
				$serviceImports = array_merge(
					$serviceImports, 
					array("java.io.File", "java.io.FileInputStream", "java.io.InputStream"));
 
				foreach($fileOverload as $namePostfix => $paramType)
				{
					$signature[] = "{$paramType} {$paramName}{$namePostfix}";
				}
				continue;
			}
			
			if(strpos($paramType, 'Kaltura') === 0 && !$enumType)
			{
				$paramType = $this->getJavaTypeName($paramType);
				$serviceImports[] = "com.kaltura.client.types.$paramType";
			}
			
			$javaType = $this->getJavaType($paramNode, false);
			
			$signature[] = "$javaType $paramName";
		}
		return implode(', ', $signature) . ")";
	}
	
	private function getBanner() 
	{
		$currentFile = $_SERVER ["SCRIPT_NAME"];
		$parts = Explode('/', $currentFile);
		$currentFile = $parts [count($parts) - 1];
		
		$banner = "";
		$banner .= "/**\n";
		$banner .= " * This class was generated using $currentFile\n";
		$banner .= " * against an XML schema provided by Kaltura.\n";
		$banner .= " * \n";
		$banner .= " * MANUAL CHANGES TO THIS CLASS WILL BE OVERWRITTEN.\n";
		$banner .= " */\n";
		
		return $banner;
	}

	protected function replaceReservedWords($name)
	{
		switch($name)
		{
		case "notify":
		case "goto":
		case "params":
			return "{$name}_";
		default:
			return $name;
		}
	}

	public function getInitialPropertyValue($propertyNode)
	{
		$propType = $propertyNode->getAttribute("type");
		switch($propType) 
		{
		case "float":
			return "Double.MIN_VALUE";
			
		case "bigint":
		case "time":
			return "Long.MIN_VALUE";
		case "int":
			if($propertyNode->hasAttribute("enumType")) 
				return ""; // we do not want to initialize enums
			else 
				return "Integer.MIN_VALUE";
					
		default:
			return "";
		}
	}

	public function getDefaultValue($resultType) 
	{
		switch($resultType)
		{
		case "":
			return '';
		
		case "int":
		case "float":
		case "bigint":
		case "time":
		case "Integer":
			return '0';
			
		case "bool":
			return 'false';
			
		default:
			return 'null';				
		}
	}
	
	public function getDefaultParamValue($paramNode)
	{
		$type = $paramNode->getAttribute("type");
		$defaultValue = $paramNode->getAttribute("default");
		
		switch($type)
		{
		case "string": 
			if($defaultValue == 'null')
				return 'null';
			else
				return "\"" . $defaultValue . "\"";
		case "bigint":
		case "time":
			$value = trim($defaultValue);
			if($value == 'null')
				$value = "Long.MIN_VALUE";
			return $value;
		case "int": 
			$value = trim($defaultValue);
			if($value == 'null')
				$value = "Integer.MIN_VALUE";
			
			if($paramNode->hasAttribute("enumType")) 
			{
				$enumType = $this->getJavaTypeName($paramNode->getAttribute("enumType"));
				if($enumType === 'Boolean')
				{
					return $defaultValue == 1 ? 'true' : ($defaultValue == 0 ? 'false' : 'null');
				}
				else 
				{
					return "$enumType.get($value)";
				}
			}
			else
			{
				return $value;
			}
				
		case "file":
			return '(FileHolder)null';
		
		default:
			return $defaultValue;
		}
	}
	
	public function getResultType($resultType, $arrayType, $builderName, &$serviceImports)
	{
		switch($resultType)
		{
		case null:
			$serviceImports[] = "com.kaltura.client.utils.request.NullRequestBuilder";
			return "NullRequestBuilder";
			
		case "ListResponse":
			$serviceImports[] = "com.kaltura.client.types.$arrayType";
			$serviceImports[] = "com.kaltura.client.utils.request.ListResponseRequestBuilder";
			return("ListResponseRequestBuilder<" . $arrayType . ", $arrayType.Tokenizer, $builderName>");
			
		case "array":
			$serviceImports[] = "com.kaltura.client.types.$arrayType";
			$serviceImports[] = "com.kaltura.client.utils.request.ArrayRequestBuilder";
			return("ArrayRequestBuilder<" . $arrayType . ", $arrayType.Tokenizer, $builderName>");
			
		case "map":
			$serviceImports[] = "com.kaltura.client.types.$arrayType";
			$serviceImports[] = "com.kaltura.client.utils.request.MapRequestBuilder";
			return("MapRequestBuilder<" . $arrayType . ", $arrayType.Tokenizer, $builderName>");

		case "int":
			$serviceImports[] = "com.kaltura.client.utils.request.RequestBuilder";
			return("RequestBuilder<Integer, String, $builderName>");

		case "bigint":
		case "time":
			$serviceImports[] = "com.kaltura.client.utils.request.RequestBuilder";
			return("RequestBuilder<Long, String, $builderName>");
		
		case "bool":
			$serviceImports[] = "com.kaltura.client.utils.request.RequestBuilder";
			return("RequestBuilder<Boolean, String, $builderName>");
			
		case "string":
			$serviceImports[] = "com.kaltura.client.utils.request.RequestBuilder";
			return("RequestBuilder<String, String, $builderName>");
			
		case "file":
			$serviceImports[] = "com.kaltura.client.utils.request.ServeRequestBuilder";
			return("ServeRequestBuilder");
			
		default:
			$serviceImports[] = "com.kaltura.client.utils.request.RequestBuilder";
			$serviceImports[] = "com.kaltura.client.types.$resultType";
			return("RequestBuilder<$resultType, $resultType.Tokenizer, $builderName>");
		}
	}
	
	public function getJavaTypeName($type)
	{
		if($type === 'KalturaNullableBoolean'){
			return 'Boolean';
		}
		if($type === 'KalturaString'){
			$type = 'KalturaStringHolder';
		}
		elseif($type === 'KalturaObject'){
			$type = 'KalturaObjectBase';
		}
		
		return preg_replace('/^Kaltura/', '', $type);
	}
	
	public function getObjectType($type, $enforceObject = true)
	{
		switch($type) 
		{
		case "bool":
			return $enforceObject ? "Boolean": "boolean";

		case "float":
			return $enforceObject ? "Double": "double";

		case "bigint":
		case "time":
			return $enforceObject ? "Long": "long";
			
		case "int":
			return $enforceObject ? "Integer": "int";

		case "string":
			return "String";

		case "file":
			return "FileHolder";
			
		default:
			return $this->getJavaTypeName($type);
		}
	}
	
	public function getJavaType($propertyNode, $enforceObject = false)
	{
		$propType = $propertyNode->getAttribute("type");
		$isEnum = $propertyNode->hasAttribute("enumType");
		
		switch($propType) 
		{
		case "int":
			if($isEnum) 
				return $this->getJavaTypeName($propertyNode->getAttribute("enumType"));
			else 
				return $this->getObjectType($propType, $enforceObject);

		case "string":
			if($isEnum) 
				return $this->getJavaTypeName($propertyNode->getAttribute("enumType"));
			else 
				return $this->getObjectType($propType, $enforceObject);

		case "array":
			$arrayType = $this->getObjectType($propertyNode->getAttribute("arrayType"), $enforceObject);
			return "List<$arrayType>";

		case "map":
			$arrayType = $this->getObjectType($propertyNode->getAttribute("arrayType"), $enforceObject);
			return "Map<String, $arrayType>";
			
		default:
			return $this->getObjectType($propType, $enforceObject);
		}
	}

	public function finalizeClass(array &$imports, $classNode)
	{
		// close class
		$this->appendLine("}");
		$this->appendLine();
	}
}
