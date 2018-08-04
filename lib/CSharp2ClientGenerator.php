
<?php
class CSharp2ClientGenerator extends ClientGeneratorFromXml
{
	private $_csprojIncludes = array();
	private $_classInheritance = array();
	private $_enums = array();

	function __construct($xmlPath, Zend_Config $config, $sourcePath = "csharp2")
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

		$this->startNewTextBlock();
		foreach($serviceNodes as $serviceNode)
		{
			$this->writeService($serviceNode);
		}

		$configurationNodes = $xpath->query("/xml/configurations/*");
		$this->writeMainClient($serviceNodes, $configurationNodes);

		$requestConfigurationNodes = $xpath->query("/xml/configurations/request/*");
		$this->writeRequestBuilderConfigurationClass($requestConfigurationNodes);
		$this->writeObjectFactory($classNodes);

		$errorNodes = $xpath->query("/xml/errors/error");
		$this->writeApiException($errorNodes);

		$this->writeCsproj();
	}

	function writeApiException(DOMNodeList $errorNodes)
	{
		$this->startNewTextBlock();
		$this->appendLine('using System;');
		$this->appendLine();
		$this->appendLine('namespace Kaltura');
		$this->appendLine('{');
		$this->appendLine('	public class APIException : ApplicationException');
		$this->appendLine('	{');
		$this->appendLine('		#region Error codes');

		foreach($errorNodes as $errorNode)
		{
			$name = $errorNode->getAttribute("name");
			$code = $errorNode->getAttribute("code");
			$this->appendLine("		public static string $name = \"$code\";");
		}
		
		$this->appendLine('		#endregion');
		$this->appendLine();

		$this->appendLine('		#region Private Fields');
		$this->appendLine('		private string _Code;');
		$this->appendLine('		#endregion');
		$this->appendLine();
		
		$this->appendLine('		#region Properties');
		$this->appendLine('		public string Code');
		$this->appendLine('		{');
		$this->appendLine('			get { return this._Code; }');
		$this->appendLine('		}');
		$this->appendLine('		#endregion');
		$this->appendLine();
				
		$this->appendLine('		public APIException(string code, string message): base(message)');
		$this->appendLine('		{');
		$this->appendLine('			this._Code = code;');
		$this->appendLine('		}');
		$this->appendLine("	}");
		$this->appendLine("}");
		$this->appendLine();

		$this->addFile("KalturaClient/APIException.cs", $this->getTextBlock());
	}
	
	function writeObjectFactory(DOMNodeList $classNodes)
	{
		$this->startNewTextBlock();
		$this->appendLine('using System;');
		$this->appendLine('using System.Collections.Generic;');
		$this->appendLine('using System.Xml;');
		$this->appendLine('using System.Runtime.Serialization;');
		$this->appendLine('using System.Text.RegularExpressions;');
		$this->appendLine('using Kaltura.Types;');
		$this->appendLine();
		$this->appendLine('namespace Kaltura');
		$this->appendLine('{');
		$this->appendLine('	public static class ObjectFactory');
		$this->appendLine('	{');
		$this->appendLine('		private static Regex prefixRegex = new Regex("^Kaltura");');
		$this->appendLine('		');
		$this->appendLine('		public static T Create<T>(XmlElement xmlElement) where T : ObjectBase');
		$this->appendLine('		{');
		$this->appendLine('			if (xmlElement["objectType"] == null)');
		$this->appendLine('			{');
		$this->appendLine('				return null;');
		$this->appendLine('			}');
		$this->appendLine('				');
		$this->appendLine('			var className = xmlElement["objectType"].InnerText;');
		$this->appendLine('			className = prefixRegex.Replace(className, "");');
		$this->appendLine('			');
		$this->appendLine('			var type = Type.GetType("Kaltura.Types." + className);');
		$this->appendLine('			if (type == null)');
		$this->appendLine('			{');
		$this->appendLine('				type = typeof(T);');
		$this->appendLine('			}');
		$this->appendLine('			');
		$this->appendLine('			if (type == null)');
		$this->appendLine('				throw new SerializationException("Invalid object type");');
		$this->appendLine('			');
		$this->appendLine('			return (T)System.Activator.CreateInstance(type, xmlElement);');
        $this->appendLine('		}');
        $this->appendLine('		public static T Create<T>(Dictionary<string,object> data) where T : ObjectBase');
		$this->appendLine('		{');
		$this->appendLine('			if (data["objectType"] == null)');
		$this->appendLine('			{');
		$this->appendLine('				return null;');
		$this->appendLine('			}');
		$this->appendLine('				');
		$this->appendLine('			var className = (string)data["objectType"];');
		$this->appendLine('			className = prefixRegex.Replace(className, "");');
		$this->appendLine('			');
		$this->appendLine('			var type = Type.GetType("Kaltura.Types." + className);');
		$this->appendLine('			if (type == null)');
		$this->appendLine('			{');
		$this->appendLine('				type = typeof(T);');
		$this->appendLine('			}');
		$this->appendLine('			');
		$this->appendLine('			if (type == null)');
		$this->appendLine('				throw new SerializationException("Invalid object type");');
		$this->appendLine('			');
		$this->appendLine('			return (T)System.Activator.CreateInstance(type, data);');
		$this->appendLine('		}');
		$this->appendLine('		');
		$this->appendLine('		public static IListResponse Create(XmlElement xmlElement)');
		$this->appendLine('		{');
		$this->appendLine('			if (xmlElement["objectType"] == null)');
		$this->appendLine('			{');
		$this->appendLine('				return null;');
		$this->appendLine('			}');
		$this->appendLine('			');
		$this->appendLine('			string className = xmlElement["objectType"].InnerText;');
		$this->appendLine('			switch (className)');
		$this->appendLine('			{');
		
		foreach($classNodes as $classNode)
		{
			$type = $classNode->getAttribute("name");
			if ($this->shouldIncludeType($type) && $this->isClassInherit($type, 'KalturaListResponse'))
			{
				$arrayType = $this->getListResponseType($type);
				if($arrayType)
				{
					$this->appendLine("				case \"$type\":");
					$this->appendLine("					return new ListResponse<$arrayType>(xmlElement);");
				}
			}
		}
		
		$this->appendLine('			}');
		$this->appendLine('		');
		$this->appendLine('			return null;');
		$this->appendLine('		}');
        $this->appendLine('		public static IListResponse Create(Dictionary<string,object> data)');
		$this->appendLine('		{');
		$this->appendLine('			if (data["objectType"] == null)');
		$this->appendLine('			{');
		$this->appendLine('				return null;');
		$this->appendLine('			}');
		$this->appendLine('			');
		$this->appendLine('			string className = (string)data["objectType"];');
		$this->appendLine('			switch (className)');
		$this->appendLine('			{');
		
		foreach($classNodes as $classNode)
		{
			$type = $classNode->getAttribute("name");
			if ($this->shouldIncludeType($type) && $this->isClassInherit($type, 'KalturaListResponse'))
			{
				$arrayType = $this->getListResponseType($type);
				if($arrayType)
				{
					$this->appendLine("				case \"$type\":");
					$this->appendLine("					return new ListResponse<$arrayType>(data);");
				}
			}
		}
		
		$this->appendLine('			}');
		$this->appendLine('		');
		$this->appendLine('			return null;');
		$this->appendLine('		}');
        $this->appendLine("	}");
		$this->appendLine("}");
		$this->appendLine();

		$this->addFile("KalturaClient/ObjectFactory.cs", $this->getTextBlock());
	}

	function writeEnum(DOMElement $enumNode)
	{
		$enumName = $enumNode->getAttribute("name");
		if(!$this->shouldIncludeType($enumName))
			return;
		
		$enumName = $this->getCSharpName($enumName);

		$s = "";
		$s .= "namespace Kaltura.Enums"."\n";
		$s .= "{"."\n";

		if ($enumNode->getAttribute("enumType") == "string")
		{
			$s .= "	public sealed class $enumName : StringEnum"."\n";
			$s .= "	{"."\n";

			foreach($enumNode->childNodes as $constNode)
			{
				if ($constNode->nodeType != XML_ELEMENT_NODE)
					continue;

				$propertyName = $constNode->getAttribute("name");
				$propertyValue = $constNode->getAttribute("value");
				$s .= "		public static readonly $enumName $propertyName = new $enumName(\"$propertyValue\");"."\n";
			}
			$s .= "\n";
			$s .= "		private $enumName(string name) : base(name) { }"."\n";
			$s .= "	}"."\n";
			$s .= "}"."\n";
		}
		else
		{
			$s .= "	public enum $enumName"."\n";
			$s .= "	{"."\n";

			foreach($enumNode->childNodes as $constNode)
			{
				if ($constNode->nodeType != XML_ELEMENT_NODE)
					continue;

				$propertyName = $constNode->getAttribute("name");
				$propertyValue = $constNode->getAttribute("value");
				$s .= "		$propertyName = $propertyValue,"."\n";
			}
			$s .= "	}"."\n";
			$s .= "}"."\n";
		}
		$file = "Enums/$enumName.cs";
		$this->addFile("KalturaClient/".$file, $s);
		$this->_csprojIncludes[] = $file;
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
	
	private function classHasProperty($className, $propertyName, $caseSensitive = true)
	{
		$propertyXPath = $caseSensitive ? "@name='$propertyName'" : "translate(@name, 'ABCDEFGHJIKLMNOPQRSTUVWXYZ', 'abcdefghjiklmnopqrstuvwxyz')='" . strtolower($propertyName) . "'";
		$xpath = new DOMXPath($this->_doc);
		$propertyNodes = $xpath->query("/xml/classes/class[@name='$className']/property[$propertyXPath]");

		if($propertyNodes->length)
			return true;

		$classNodes = $xpath->query("/xml/classes/class[@name='$className']");
		$classNode = $classNodes->item(0);
		if($classNode && $classNode->hasAttribute("base"))
			return $this->classHasProperty($classNode->getAttribute("base"), $propertyName, $caseSensitive);
		
		return false;
	}

	private function getCSharpName($name)
	{
		if($name === 'KalturaObject')
			return 'ObjectBase';
		
		if ($this->isClassInherit($name, "KalturaListResponse"))
		{
			$arrayType = $this->getListResponseType($name);
			return "ListResponse<$arrayType>";
		}
		
		return preg_replace('/^Kaltura/', '', $name);
	}

	function writeClass(DOMElement $classNode)
	{
		$type = $classNode->getAttribute("name");
		if(!$this->shouldIncludeType($type))
			return;

		if($type == 'KalturaObject')
			return;

		if ($this->isClassInherit($type, "KalturaListResponse") || $type == "KalturaListResponse")
			return;
				
		$className = $this->getCSharpName($type);
		$this->startNewTextBlock();
		$this->appendLine("using System;");
		$this->appendLine("using System.Xml;");
		$this->appendLine("using System.Collections.Generic;");
		$this->appendLine("using Kaltura.Enums;");
		$this->appendLine("using Kaltura.Request;");
		$this->appendLine();
		$this->appendLine("namespace Kaltura.Types");
		$this->appendLine("{");

		// class definition
		$base = null;
		if ($classNode->hasAttribute("base"))
		{
			$base = $classNode->getAttribute("base");
			$this->appendLine("	public class $className : " . $this->getCSharpName($base));
		}
		else
		{
			$this->appendLine("	public class $className : ObjectBase");
		}
		$this->appendLine("	{");

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
				"isOrderBy" => false
			);

			$propType = $propertyNode->getAttribute("type");
			$propName = $propertyNode->getAttribute("name");
			$isEnum = $propertyNode->hasAttribute("enumType");
			$dotNetPropName = $this->upperCaseFirstLetter($propName);
			$property["apiName"] = $propName;
			$property["name"] = $dotNetPropName;

			if ($isEnum)
			{
				$dotNetPropType = $this->getCSharpName($propertyNode->getAttribute("enumType"));
			}
			else if ($propType == "array")
			{
				$arrayObjectType = $propertyNode->getAttribute("arrayType");
				if($arrayObjectType == 'KalturaObject')
					$arrayObjectType = 'ObjectBase';
				$dotNetPropType = "IList<" . $this->getCSharpName($arrayObjectType) . ">";
			}
			else if ($propType == "map")
			{
				$arrayObjectType = $propertyNode->getAttribute("arrayType");
				if($arrayObjectType == 'KalturaObject')
					$arrayObjectType = 'ObjectBase';
				$dotNetPropType = "IDictionary<string, " . $this->getCSharpName($arrayObjectType) . ">";
			}
			else if ($propType == "bool")
			{
				$dotNetPropType  = "bool?";
			}
			else if ($propType == "bigint")
			{
				$dotNetPropType  = "long";
			}
			else if ($propType == "time")
			{
				$dotNetPropType  = "int";
			}
			else
			{
				$dotNetPropType = $this->getCSharpName($propType);
			}

			$property["type"] = $dotNetPropType;

			if ($isFilter && $dotNetPropName == "OrderBy")
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
				case "bigint":
					$property["default"] = "long.MinValue";
					break;
				case "int":
					if ($isEnum)
						$property["default"] = "($dotNetPropType)Int32.MinValue";
					else
						$property["default"] = "Int32.MinValue";
					break;
				case "string":
					$property["default"] = "null";
					break;
				case "bool":
					$property["default"] = "null";
					break;
				case "float":
					$property["default"] = "Single.MinValue";
					break;
			}

			$properties[] = $property;
		}
		

		// constants
		$this->appendLine("		#region Constants");
		$constants = array();
		foreach($properties as $property)
		{
			$constName = $this->camelCaseToUnderscoreAndUpper($property['name']);
			$constants[$constName] = $property['name'];
			$new = ($property['isOrderBy'] || ($base && $this->classHasProperty($base, $property['apiName'], false))) ? 'new ' : '';
			$this->appendLine("		public {$new}const string $constName = \"{$property['apiName']}\";");
		}
		$this->appendLine("		#endregion");
		$this->appendLine();
		
		
		// private fields
		$this->appendLine("		#region Private Fields");
		foreach($properties as $property)
		{
			$propertyLine = "private {$property['type']} _{$property['name']}";

			if (!is_null($property["default"]))
				$propertyLine .= " = {$property['default']}";

			$propertyLine .= ";";

			$this->appendLine("		" . $propertyLine);
		}
		$this->appendLine("		#endregion");
		$this->appendLine();

		
		// properties
		$this->appendLine("		#region Properties");
		foreach($properties as $property)
		{
			$propertyLine = "public";

			if ($property['isOrderBy'] || $base && $this->classHasProperty($base, $property['apiName']))
				$propertyLine .= " new";

			$propertyLine .= " {$property['type']} {$property['name']}";
			
			if($property['name'] === $className)
				$propertyLine .= 'Value';

			$this->appendLine("		" . $propertyLine);
			$this->appendLine("		{");

			if (!isset($property['writeOnly']) || !$property['writeOnly'])
			{
				$this->appendLine("			get { return _{$property['name']}; }");
			}

			if (!isset($property['readOnly']) || !$property['readOnly'])
			{
				$this->appendLine("			set ");
				$this->appendLine("			{ ");
				$this->appendLine("				_{$property['name']} = value;");
				$this->appendLine("				OnPropertyChanged(\"{$property['name']}\");");
				$this->appendLine("			}");
			}

			$this->appendLine("		}");
		}
		$this->appendLine("		#endregion");
		$this->appendLine();

		$this->appendLine("		#region CTor");
		// CTor
		$this->appendLine("		public $className()");
		$this->appendLine("		{");
		$this->appendLine("		}");
		$this->appendLine("");

		$this->appendLine("		public $className(XmlElement node) : base(node)");
		$this->appendLine("		{");
		if ($classNode->childNodes->length)
		{
			$this->appendLine("			foreach (XmlElement propertyNode in node.ChildNodes)");
			$this->appendLine("			{");
			$this->appendLine("				switch (propertyNode.Name)");
			$this->appendLine("				{");
			foreach($classNode->childNodes as $propertyNode)
			{
				if ($propertyNode->nodeType != XML_ELEMENT_NODE)
					continue;

				$propType = $propertyNode->getAttribute("type");
				$propName = $propertyNode->getAttribute("name");
				$isEnum = $propertyNode->hasAttribute("enumType");
				$dotNetPropName = $this->upperCaseFirstLetter($propName);
				$this->appendLine("					case \"$propName\":");
				switch($propType)
				{
					case "bigint":
						$this->appendLine("						this._$dotNetPropName = ParseLong(propertyNode.InnerText);");
						break;
					case "int":
					case "time":
						if ($isEnum)
						{
							$enumType = $this->getCSharpName($propertyNode->getAttribute("enumType"));
							$this->appendLine("						this._$dotNetPropName = ($enumType)ParseEnum(typeof($enumType), propertyNode.InnerText);");
						}
						else
							$this->appendLine("						this._$dotNetPropName = ParseInt(propertyNode.InnerText);");
						break;
					case "string":
						if ($isEnum)
						{
							$enumType = $this->getCSharpName($propertyNode->getAttribute("enumType"));
							$this->appendLine("						this._$dotNetPropName = ($enumType)StringEnum.Parse(typeof($enumType), propertyNode.InnerText);");
						}
						else
							$this->appendLine("						this._$dotNetPropName = propertyNode.InnerText;");
						break;
					case "bool":
						$this->appendLine("						this._$dotNetPropName = ParseBool(propertyNode.InnerText);");
						break;
					case "float":
						$this->appendLine("						this._$dotNetPropName = ParseFloat(propertyNode.InnerText);");
						break;
					case "array":
						$arrayType = $this->getCSharpName($propertyNode->getAttribute("arrayType"));
						if($arrayType == 'Object')
							$arrayType = 'ObjectBase';

						$this->appendLine("						this._$dotNetPropName = new List<$arrayType>();");
						$this->appendLine("						foreach(XmlElement arrayNode in propertyNode.ChildNodes)");
						$this->appendLine("						{");
						$this->appendLine("							this._$dotNetPropName.Add(ObjectFactory.Create<$arrayType>(arrayNode));");
						$this->appendLine("						}");
						break;
					case "map":
						$arrayType = $this->getCSharpName($propertyNode->getAttribute("arrayType"));
						if($arrayType == 'Object')
							$arrayType = 'ObjectBase';

						$this->appendLine("						{");		// TODO: remove the index once the keys are added to the response
						$this->appendLine("							string key;");
						$this->appendLine("							this._$dotNetPropName = new Dictionary<string, $arrayType>();");
						$this->appendLine("							foreach(XmlElement arrayNode in propertyNode.ChildNodes)");
						$this->appendLine("							{");
						$this->appendLine("								key = arrayNode[\"itemKey\"].InnerText;;");
						$this->appendLine("								this._{$dotNetPropName}[key] = ObjectFactory.Create<$arrayType>(arrayNode);");
						$this->appendLine("							}");
						$this->appendLine("						}");
						break;
					default: // sub object
						$propType = $this->getCSharpName($propType);
						$this->appendLine("						this._$dotNetPropName = ObjectFactory.Create<$propType>(propertyNode);");
						break;
				}
				$this->appendLine("						continue;");
			}
			$this->appendLine("				}");
			$this->appendLine("			}");
		}
        $this->appendLine("		}");
        
		$this->appendLine("");
        $this->appendLine("		public $className(Dictionary<string,object> data) : base(data)");
		$this->appendLine("		{");
        foreach($classNode->childNodes as $propertyNode)
        {
            if ($propertyNode->nodeType != XML_ELEMENT_NODE)
					continue;
            $propType = $propertyNode->getAttribute("type");
            $propName = $propertyNode->getAttribute("name");
            $isEnum = $propertyNode->hasAttribute("enumType");
            $dotNetPropName = $this->upperCaseFirstLetter($propName);
            
            switch($propType)
            {
                case "bigint":
                    $this->appendLine("			    this._$dotNetPropName = data.TryGetValueSafe<long>(\"$propName\");");
                    break;
                case "int":
                case "time":
                    if ($isEnum)
                    {
                        $enumType = $this->getCSharpName($propertyNode->getAttribute("enumType"));
                        $this->appendLine("			    this._$dotNetPropName = ($enumType)ParseEnum(typeof($enumType), data.TryGetValueSafe<string>(\"$propName\"));");
                    }
                    else
                        $this->appendLine("			    this._$dotNetPropName = data.TryGetValueSafe<int>(\"$propName\");");
                    break;
                case "string":
                    if ($isEnum)
                    {
                        $enumType = $this->getCSharpName($propertyNode->getAttribute("enumType"));
                        $this->appendLine("			    this._$dotNetPropName = ($enumType)StringEnum.Parse(typeof($enumType), data.TryGetValueSafe<string>(\"$propName\"));");
                    }
                    else
                        $this->appendLine("			    this._$dotNetPropName = data.TryGetValueSafe<string>(\"$propName\");");
                    break;
                case "bool":
                case "float":
                        $this->appendLine("			    this._$dotNetPropName = data.TryGetValueSafe<$propType>(\"$propName\");");
                    break;
                case "array":
                    $arrayType = $this->getCSharpName($propertyNode->getAttribute("arrayType"));
                    if($arrayType == 'Object')
                        $arrayType = 'ObjectBase';
                    $this->appendLine("			    this._$dotNetPropName = new List<$arrayType>();");
                    $this->appendLine("			    foreach(var dataDictionary in data.TryGetValueSafe(\"$propName\", new List<Dictionary<string,object>>()))");
                    $this->appendLine("			    {");
                    $this->appendLine("			        this._$dotNetPropName.Add(ObjectFactory.Create<$arrayType>(dataDictionary));");
                    $this->appendLine("			    }");
                    break;
                case "map":
                    $arrayType = $this->getCSharpName($propertyNode->getAttribute("arrayType"));
                    if($arrayType == 'Object')
                        $arrayType = 'ObjectBase';

                    $this->appendLine("			    this._$dotNetPropName = new Dictionary<string, $arrayType>();");
                    $this->appendLine("			    foreach(var keyValuePair in data.TryGetValueSafe(\"$propName\", new Dictionary<string, object>()))");
                    $this->appendLine("			    {");
                    $this->appendLine("			        this._{$dotNetPropName}[keyValuePair.Key] = ObjectFactory.Create<$arrayType>((Dictionary<string,object>)keyValuePair.Value);");
                    $this->appendLine("				}");
                    break;
                default:
                    $propType = $this->getCSharpName($propType);
                    $this->appendLine("			    this._$dotNetPropName = ObjectFactory.Create<$propType>(data.TryGetValueSafe<Dictionary<string,object>>(\"$propName\"));");
                    break;
            }

        }
		$this->appendLine("		}");



		$this->appendLine("		#endregion");
		$this->appendLine("");

		$this->appendLine("		#region Methods");
		// ToParams method
		$this->appendLine("		public override Params ToParams(bool includeObjectType = true)");
		$this->appendLine("		{");
		$this->appendLine("			Params kparams = base.ToParams(includeObjectType);");
		$this->appendLine("			if (includeObjectType)");
		$this->appendLine("				kparams.AddReplace(\"objectType\", \"$type\");");
		foreach($classNode->childNodes as $propertyNode)
		{
			if ($propertyNode->nodeType != XML_ELEMENT_NODE)
				continue;

			$propName = $propertyNode->getAttribute("name");
			$dotNetPropName = $this->upperCaseFirstLetter($propName);
			$this->appendLine("			kparams.AddIfNotNull(\"$propName\", this._{$dotNetPropName});");
		}
		$this->appendLine("			return kparams;");
		$this->appendLine("		}");

		$this->appendLine("		protected override string getPropertyName(string apiName)");
		$this->appendLine("		{");
		$this->appendLine("			switch(apiName)");
		$this->appendLine("			{");
		foreach($constants as $constant => $name)
		{
			$this->appendLine("				case $constant:");
			$this->appendLine("					return \"$name\";");
		}
		$this->appendLine("				default:");
		$this->appendLine("					return base.getPropertyName(apiName);");
		$this->appendLine("			}");
		$this->appendLine("		}");
		
		$this->appendLine("		#endregion");

		// close class
		$this->appendLine("	}");
		$this->appendLine("}");
		$this->appendLine();

		$file = "Types/$className.cs";
		$this->addFile("KalturaClient/".$file, $this->getTextBlock());
		$this->_csprojIncludes[] = $file;
	}

	function writeCsproj()
	{
		$csprojDoc = new DOMDocument();
		$csprojDoc->formatOutput = true;
		$csprojDoc->load($this->_sourcePath."/KalturaClient/KalturaClient.csproj");
		$csprojXPath = new DOMXPath($csprojDoc);
		$csprojXPath->registerNamespace("m", "http://schemas.microsoft.com/developer/msbuild/2003");
		$compileNodes = $csprojXPath->query("//m:ItemGroup/m:Compile/..");
		$compileItemGroupElement = $compileNodes->item(0);

		foreach($this->_csprojIncludes as $include)
		{
			$compileElement = $csprojDoc->createElement("Compile");
			$compileElement->setAttribute("Include", str_replace("/","\\", $include));
			$compileItemGroupElement->appendChild($compileElement);
		}
		$this->addFile("KalturaClient/KalturaClient.csproj", $csprojDoc->saveXML(), false);
	}

	function writeService(DOMElement $serviceNode)
	{
		$serviceId = $serviceNode->getAttribute("id");
		if(!$this->shouldIncludeService($serviceId))
			return;

		$this->startNewTextBlock();
		$this->appendLine("using System;");
		$this->appendLine("using System.Xml;");
		$this->appendLine("using System.Collections.Generic;");
		$this->appendLine("using System.IO;");
		$this->appendLine("using Kaltura.Request;");
		$this->appendLine("using Kaltura.Types;");
		$this->appendLine("using Kaltura.Enums;");
		$this->appendLine();
		$this->appendLine("namespace Kaltura.Services");
		$this->appendLine("{");
		$serviceName = $serviceNode->getAttribute("name");


		$dotNetServiceName = $this->upperCaseFirstLetter($serviceName)."Service";

		$actionNodes = $serviceNode->childNodes;
		foreach($actionNodes as $actionNode)
		{
			if ($actionNode->nodeType != XML_ELEMENT_NODE)
				continue;
		
				$this->writeRequestBuilder($serviceId, $serviceName, $actionNode);
		}
		
		$this->appendLine();
		$this->appendLine("	public class $dotNetServiceName");
		$this->appendLine("	{");
		$this->appendLine("		private $dotNetServiceName()");
		$this->appendLine("		{");
		$this->appendLine("		}");


		$actionNodes = $serviceNode->childNodes;
		foreach($actionNodes as $actionNode)
		{
			if ($actionNode->nodeType != XML_ELEMENT_NODE)
				continue;

			$this->writeAction($serviceId, $serviceName, $actionNode);
		}
		$this->appendLine("	}");
		$this->appendLine("}");

		$file = "Services/".$dotNetServiceName.".cs";
		$this->addFile("KalturaClient/".$file, $this->getTextBlock());
		$this->_csprojIncludes[] = $file;
	}

	function writeRequestBuilder($serviceId, $serviceName, DOMElement $actionNode)
	{
		$actionName = $actionNode->getAttribute("name");
		if(!$this->shouldIncludeAction($serviceId, $actionName))
			return;

		$resultNode = $actionNode->getElementsByTagName("result")->item(0);
		$resultType = $resultNode->getAttribute("type");
		$arrayObjectType = ($resultType == 'array') ? $resultNode->getAttribute("arrayType" ) : null;

		if($resultType == 'file')
			return;

		$enableInMultiRequest = true;
		if($actionNode->hasAttribute("enableInMultiRequest"))
		{
			$enableInMultiRequest = intval($actionNode->getAttribute("enableInMultiRequest"));
		}

		switch($resultType)
		{
			case null:
				$dotNetOutputType = "object";
				break;
			case "array":
				$arrayType = $this->getCSharpName($resultNode->getAttribute("arrayType"));
				$dotNetOutputType = "IList<".$arrayType.">";
				break;
			case "map":
				$arrayType = $this->getCSharpName($resultNode->getAttribute("arrayType"));
				$dotNetOutputType = "IDictionary<string, ".$arrayType.">";
				break;
			case "bigint":
				$dotNetOutputType = "long";
				break;
			default:
				$dotNetOutputType = $this->getCSharpName($resultType);
				break;
		}

		$requestBuilderName = ucfirst($serviceName) . ucfirst($actionName) . 'RequestBuilder';

		$paramNodes = $actionNode->getElementsByTagName("param");
		$signature = $this->getSignature($paramNodes, false);
		
		$parentType = $enableInMultiRequest ? "RequestBuilder<$dotNetOutputType>" : "RequestBuilder<$dotNetOutputType>";
		$this->appendLine("	public class $requestBuilderName : $parentType");
		$this->appendLine("	{");
		

		$this->appendLine("		#region Constants");
		$constants = array();
		$news = array();
		foreach($paramNodes as $paramNode)
		{
			$paramName = $paramNode->getAttribute("name");
			$new = '';
			
			if($this->classHasProperty('KalturaRequestConfiguration', $paramName))
			{
				$news[$paramName] = true;
				$new = 'new ';
			}
			$constName = $this->camelCaseToUnderscoreAndUpper($paramName);
			$this->appendLine("		public {$new}const string $constName = \"$paramName\";");
		}
		$this->appendLine("		#endregion");
		$this->appendLine();
		
		
		$hasFiles = false;
		$params = array();
		foreach($paramNodes as $paramNode)
		{
			$paramType = $paramNode->getAttribute("type");
			$paramName = $paramNode->getAttribute("name");
			$isEnum = $paramNode->hasAttribute("enumType");
			$isFile = false;

			switch($paramType)
			{
				case "array":
					$dotNetType = "IList<" . $this->getCSharpName($paramNode->getAttribute("arrayType")) . ">";
					break;
				case "map":
					$dotNetType = "IDictionary<string, " . $this->getCSharpName($paramNode->getAttribute("arrayType")) . ">";
					break;
				case "file":
					$dotNetType = "Stream";
					$isFile = true;
					$hasFiles = true;
					break;
				case "bigint":
					$dotNetType = "long";
					break;
				case "int":
					if ($isEnum)
						$dotNetType = $this->getCSharpName($paramNode->getAttribute("enumType"));
					else
						$dotNetType = $paramType;
					break;
				default:
					if ($isEnum)
						$dotNetType = $paramNode->getAttribute("enumType");
					else
						$dotNetType = $paramType;
					
					$dotNetType = $this->getCSharpName($dotNetType);
					break;
			}


			$param = $this->fixParamName($paramName);
			$new = isset($news[$paramName]) ? 'new ' : '';
			$paramName = ucfirst($param);
			$this->appendLine("		public {$new}$dotNetType $paramName");
			$this->appendLine("		{");
			$this->appendLine("			set;");
			$this->appendLine("			get;");
			$this->appendLine("		}");
			$params[$param] = $isFile;
		}

		$this->appendLine();
		$this->appendLine("		public $requestBuilderName()");
		$this->appendLine("			: base(\"$serviceId\", \"$actionName\")");
		$this->appendLine("		{");
		$this->appendLine("		}");
		
		if(count($params))
		{
			$this->appendLine();
			$this->appendLine("		public $requestBuilderName($signature)");
			$this->appendLine("			: this()");
			$this->appendLine("		{");
			foreach($params as $param => $isFile)
			{
				$paramName = ucfirst($param);
				$this->appendLine("			this.$paramName = $param;");
			}
			$this->appendLine("		}");
		}
		
		$this->appendLine();
		$this->appendLine("		public override Params getParameters(bool includeServiceAndAction)");
		$this->appendLine("		{");
		$this->appendLine("			Params kparams = base.getParameters(includeServiceAndAction);");
		foreach($params as $param => $isFile)
		{
			if(!$isFile)
			{
				$paramName = ucfirst($param);
				$this->appendLine("			if (!isMapped(\"$param\"))");
				$this->appendLine("				kparams.AddIfNotNull(\"$param\", $paramName);");
			}
		}
		$this->appendLine("			return kparams;");
		$this->appendLine("		}");
		
		$this->appendLine();
		$this->appendLine("		public override Files getFiles()");
		$this->appendLine("		{");
		$this->appendLine("			Files kfiles = base.getFiles();");
		foreach($params as $param => $isFile)
		{
			if($isFile)
			{
				$paramName = ucfirst($param);
				$this->appendLine("			kfiles.Add(\"$param\", $paramName);");
			}
		}
		$this->appendLine("			return kfiles;");
		$this->appendLine("		}");

		$this->appendLine();
		$this->appendLine("		public override object Deserialize(XmlElement result)");
		$this->appendLine("		{");
		if ($resultType)
		{
			switch ($resultType)
			{
				case "array":
					$arrayType = $this->getCSharpName($resultNode->getAttribute("arrayType"));
					$this->appendLine("			IList<$arrayType> list = new List<$arrayType>();");
					$this->appendLine("			foreach(XmlElement node in result.ChildNodes)");
					$this->appendLine("			{");
					$this->appendLine("				list.Add(ObjectFactory.Create<$arrayType>(node));");
					$this->appendLine("			}");
					$this->appendLine("			return list;");
					break;
				case "map":
					$arrayType = $this->getCSharpName($resultNode->getAttribute("arrayType"));
					$this->appendLine("			string key;");
					$this->appendLine("			IDictionary<string, $arrayType> map = new Dictionary<string, $arrayType>();");
					$this->appendLine("			foreach(XmlElement node in result.ChildNodes)");
					$this->appendLine("			{");
					$this->appendLine("				key = xmlElement[\"itemKey\"]");
					$this->appendLine("				map.Add(key, ObjectFactory.Create<$arrayType>(node));");
					$this->appendLine("			}");
					$this->appendLine("			return map;");
					break;
				case "bigint":
					$this->appendLine("			return long.Parse(result.InnerText);");
					break;
				case "int":
					$this->appendLine("			return int.Parse(result.InnerText);");
					break;
				case "float":
					$this->appendLine("			return Single.Parse(result.InnerText);");
					break;
				case "bool":
					$this->appendLine("			if (result.InnerText.Equals(\"1\") || result.InnerText.ToLower().Equals(\"true\"))");
					$this->appendLine("				return true;");
					$this->appendLine("			return false;");
					break;
				case "string":
					$this->appendLine("			return result.InnerText;");
					break;
				default:
					$resultType = $this->getCSharpName($resultType);
					$this->appendLine("			return ObjectFactory.Create<$resultType>(result);");
					break;
			}
		}
		else 
		{
			$this->appendLine("			return null;");
		}
		$this->appendLine("		}");
        
        $this->appendLine("		public override object DeserializeObject(object result)");
		$this->appendLine("		{");
		if ($resultType)
		{
			switch ($resultType)
			{
				case "array":
					$arrayType = $this->getCSharpName($resultNode->getAttribute("arrayType"));
					$this->appendLine("			var list = new List<$arrayType>();");
					$this->appendLine("			foreach(var node in (List<Dictionary<string,object>>)result)");
					$this->appendLine("			{");
					$this->appendLine("				list.Add(ObjectFactory.Create<$arrayType>(node));");
					$this->appendLine("			}");
					$this->appendLine("			return list;");
					break;
				case "map":
					$arrayType = $this->getCSharpName($resultNode->getAttribute("arrayType"));
					$this->appendLine("			var map = new Dictionary<string, $arrayType>();");
					$this->appendLine("			foreach(var node in (Dictionary<string,Dictionary<string,object>>)result)");
					$this->appendLine("			{");
					$this->appendLine("				map.Add(node.Key, ObjectFactory.Create<$arrayType>(node.Value));");
					$this->appendLine("			}");
					$this->appendLine("			return map;");
					break;
				case "bigint":
					$this->appendLine("			return (long)result;");
					break;
				case "int":
					$this->appendLine("			return (int)(result);");
					break;
				case "float":
					$this->appendLine("			return (float)result;");
					break;
				case "bool":
                    $this->appendLine("			var resultStr = (string)result;");
                    $this->appendLine("			if (resultStr.Equals(\"1\") || resultStr.ToLower().Equals(\"true\"))");
                    $this->appendLine("				return true;");
                    $this->appendLine("			return false;");
					break;
				case "string":
					$this->appendLine("			return (string)result;");
					break;
				default:
					$resultType = $this->getCSharpName($resultType);
					$this->appendLine("			return ObjectFactory.Create<$resultType>((Dictionary<string,object>)result);");
					break;
			}
		}
		else 
		{
			$this->appendLine("			return null;");
		}
		$this->appendLine("		}");

		$this->appendLine("	}");
		$this->appendLine();
		
	}

	function writeAction($serviceId, $serviceName, DOMElement $actionNode)
	{
		$action = $actionNode->getAttribute("name");
		if(!$this->shouldIncludeAction($serviceId, $action))
			return;

		$resultNode = $actionNode->getElementsByTagName("result")->item(0);
		$resultType = $resultNode->getAttribute("type");
		$arrayObjectType = ($resultType == 'array') ? $resultNode->getAttribute("arrayType" ) : null;

		if($resultType == 'file')
			return;

		switch($resultType)
		{
			case null:
				$dotNetOutputType = "void";
				break;
			case "array":
				$arrayType = $resultNode->getAttribute("arrayType");
				$dotNetOutputType = "IList<".$arrayType.">";
				break;
			case "map":
				$arrayType = $resultNode->getAttribute("arrayType");
				$dotNetOutputType = "IDictionary<string, ".$arrayType.">";
				break;
			case "bigint":
				$dotNetOutputType = "long";
				break;
			default:
				$dotNetOutputType = $resultType;
				break;
		}
		$requestBuilderName = ucfirst($serviceName) . ucfirst($action) . 'RequestBuilder';

		$signaturePrefix = "public static $requestBuilderName ".$this->upperCaseFirstLetter($action);

		$paramNodes = $actionNode->getElementsByTagName("param");
		$signature = $this->getSignature($paramNodes);

		$params = array();
		foreach($paramNodes as $paramNode)
		{
			$paramName = $paramNode->getAttribute("name");
			$params[] = $this->fixParamName($paramName);
		}
		
		// write the overload
		$this->appendLine();
		$this->appendLine("		$signaturePrefix($signature)");
		$this->appendLine("		{");
		$this->appendLine("			return new $requestBuilderName(" . implode(', ', $params) . ");");
		$this->appendLine("		}");
	}

	function getSignature($paramNodes, $enableOptionals = true)
	{
		$params = array();
		foreach($paramNodes as $paramNode)
		{
			$paramType = $paramNode->getAttribute("type");
			$paramName = $paramNode->getAttribute("name");
			$isEnum = $paramNode->hasAttribute("enumType");

			switch($paramType)
			{
				case "array":
					$dotNetType = "IList<" . $this->getCSharpName($paramNode->getAttribute("arrayType")) . ">";
					break;
				case "map":
					$dotNetType = "IDictionary<string, " . $this->getCSharpName($paramNode->getAttribute("arrayType")) . ">";
					break;
				case "file":
					$dotNetType = "Stream";
					break;
				case "bigint":
					$dotNetType = "long";
					break;
				case "int":
					if ($isEnum)
						$dotNetType = $this->getCSharpName($paramNode->getAttribute("enumType"));
					else
						$dotNetType = $paramType;
					break;
				default:
					if ($isEnum)
						$dotNetType = $paramNode->getAttribute("enumType");
					else
						$dotNetType = $paramType;
						$dotNetType = $this->getCSharpName($dotNetType);
					break;
			}


			$param = "$dotNetType ".$this->fixParamName($paramName);
			$optional = $paramNode->getAttribute("optional");
			if ($enableOptionals && $optional == "1")
			{
				$param .= ' = ';
				$type = $paramNode->getAttribute("type");
				if ($type == "string")
				{
					$value = $paramNode->getAttribute("default");
					if($value == 'null')
						$param .=  "null";
					else
						$param .=  "\"".$paramNode->getAttribute("default")."\"";
				}
				else if ($type == "int" && $paramNode->hasAttribute("enumType"))
				{
					$value = $paramNode->getAttribute("default");
					if ($value == "null")
						$value = "Int32.MinValue";
					
					$param .=  "(" . $this->getCSharpName($paramNode->getAttribute("enumType")) . ")($value)";
				}
				elseif ($type == "int" && $paramNode->getAttribute("default") == "null") // because Partner.GetUsage has an int field with empty default value
				{
					$param .= "Int32.MinValue";
				}
				else
				{
					$param .=  $paramNode->getAttribute("default");
				}
			}
							
			$params[] = $param;
		}
		
		return implode(', ', $params);
	}

	function writeMainClient(DOMNodeList $serviceNodes, DOMNodeList $configurationNodes)
	{
		$apiVersion = $this->_doc->documentElement->getAttribute('apiVersion');
		$date = date('y-m-d');

		$this->startNewTextBlock();

		$this->appendLine("using System;");
		$this->appendLine("using Kaltura.Types;");
		$this->appendLine("using Kaltura.Enums;");
		$this->appendLine();

		$this->appendLine("namespace Kaltura");
		$this->appendLine("{");
		$this->appendLine("	public class Client : ClientBase");
		$this->appendLine("	{");
		$this->appendLine("		public Client(Configuration config) : base(config)");
		$this->appendLine("		{");
		$this->appendLine("				ApiVersion = \"$apiVersion\";");
		$this->appendLine("				ClientTag = \"dotnet:$date\";");
		$this->appendLine("		}");
		$this->appendLine("	");


		$this->appendLine("		#region Properties");
		foreach($configurationNodes as $configurationNode)
		{
			/* @var $configurationNode DOMElement */
			$configurationName = $configurationNode->nodeName;
			$attributeName = lcfirst($configurationName) . "Configuration";

			foreach($configurationNode->childNodes as $configurationPropertyNode)
			{
				/* @var $configurationPropertyNode DOMElement */

				if ($configurationPropertyNode->nodeType != XML_ELEMENT_NODE)
				{
					continue;
				}

				$configurationProperty = $configurationPropertyNode->localName;

				if($configurationPropertyNode->hasAttribute('volatile') && $configurationPropertyNode->getAttribute('volatile'))
				{
					continue;
				}

				$type = $configurationPropertyNode->getAttribute('type');
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
		$this->appendLine("		#endregion");
		$this->appendLine("	}");
		$this->appendLine("}");

		$this->addFile("KalturaClient/Client.cs", $this->getTextBlock());
	}

	function writeRequestBuilderConfigurationClass($requestConfigurationNodes)
	{
		$this->startNewTextBlock();

		$this->appendLine("using System;");
		$this->appendLine("using System.Collections.Generic;");
		$this->appendLine("using Kaltura.Types;");
		$this->appendLine("using Kaltura.Enums;");
		$this->appendLine();

		$this->appendLine("namespace Kaltura.Request");
		$this->appendLine("{");
		$this->appendLine("	public static class RequestBuilderExtensions");
		$this->appendLine("	{");
		foreach($requestConfigurationNodes as $configurationPropertyNode)
		{
			if ($configurationPropertyNode->nodeType != XML_ELEMENT_NODE)
				continue;

			$configurationProperty = $configurationPropertyNode->localName;

			$type = $configurationPropertyNode->getAttribute('type');
			$description = null;

			if($configurationPropertyNode->hasAttribute('description'))
			{
				$description = $configurationPropertyNode->getAttribute('description');
			}

			$this-> writeRequestBuilderConfigurationExtentionMethod($configurationProperty, $configurationProperty, $type, $description);
        }
        
        $this->appendLine("");
        $this->appendLine("		public static T TryGetValueSafe<T>(this IDictionary<string,object> sourceDictionary, string key, T defaultReturnValue = default(T))");
        $this->appendLine("		{");
        $this->appendLine("				var returnVal = defaultReturnValue;");
        $this->appendLine("				object objValue;");
        $this->appendLine("				sourceDictionary.TryGetValue(key, out objValue);");
        $this->appendLine("				if (objValue != null) { returnVal = (T)objValue; }");
        $this->appendLine("");
        $this->appendLine("				return returnVal;");
        $this->appendLine("		}");
		$this->appendLine("	}");
		$this->appendLine("}");

		$this->addFile("KalturaClient/Request/RequestBuilderExtensions.cs", $this->getTextBlock());
	}

	protected function writeRequestBuilderConfigurationExtentionMethod($name, $paramName, $type, $description)
	{
		$paramName = ucfirst($paramName);
		$name = ucfirst($name);

		$null = 'null';
		switch($type)
		{
			case 'int':
				$null = 'int.MinValue';
				break;

			case 'float':
				$null = 'float.MinValue';
				break;

			case 'bigint':
				$type = 'long';
				$null = 'long.MinValue';
				break;

			default:
				$type = $this->getCSharpName($type);
				break;
		}

		$this->appendLine("		/// <summary>");
	    $this->appendLine("		/// $description");
	    $this->appendLine("		/// </summary>");
		$this->appendLine("		public static BaseRequestBuilder<T> With$name<T>(this BaseRequestBuilder<T> requestBuilder, $type value)");
		$this->appendLine("		{");
		$this->appendLine("			requestBuilder.$name = value;");
		$this->appendLine("			return requestBuilder;");
		$this->appendLine("		}");
	}

	protected function writeConfigurationProperty($configurationName, $name, $paramName, $type, $description)
	{
		$paramName = ucfirst($paramName);
		$name = ucfirst($name);
		if($name == 'Ks')
			$name = 'KS';

		$null = 'null';
		switch($type)
		{
			case 'int':
				$null = 'int.MinValue';
				break;

			case 'float':
				$null = 'float.MinValue';
				break;

			case 'bigint':
				$type = 'long';
				$null = 'long.MinValue';
				break;

			default:
				$type = $this->getCSharpName($type);
				break;
		}

		$this->appendLine("			");
		$this->appendLine(" 		public $type $name");
		$this->appendLine(" 		{");
		$this->appendLine(" 			get");
		$this->appendLine(" 			{");
		$this->appendLine(" 				return {$configurationName}Configuration.$paramName;");
		$this->appendLine(" 			}");
		$this->appendLine(" 			set");
		$this->appendLine(" 			{");
		$this->appendLine(" 				{$configurationName}Configuration.$paramName = value;");
		$this->appendLine(" 			}");
		$this->appendLine(" 		}");
		$this->appendLine("			");
		$this->appendLine(" 		public void set{$name}($type value)");
		$this->appendLine(" 		{");
		$this->appendLine(" 			$name = value;");
		$this->appendLine(" 		}");
		$this->appendLine("			");
		$this->appendLine(" 		public $type get{$name}()");
		$this->appendLine(" 		{");
		$this->appendLine(" 			return $name;");
		$this->appendLine(" 		}");
	}

	private function loadEnums(DOMNodeList $enums)
	{
		foreach($enums as $item)
		{
			$this->_enums[$item->getAttribute("name")] = null;
		}
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

	protected function addFile($fileName, $fileContents, $addLicense = true)
	{
		if ($fileName == "KalturaClient.suo")
			return;

		$dirname = pathinfo($fileName, PATHINFO_DIRNAME);
		if ($this->endsWith($dirname, "Debug") || $this->endsWith($dirname, "Release"))
			return;

		parent::addFile($fileName, $fileContents, $addLicense);
	}

	/**
	 * Fix .net reserved words
	 *
	 * @param string $param
	 * @return string
	 */
	private function fixParamName($param)
	{
		switch ($param)
		{
			case "event":
				return "kevent";
			case "params":
				return "params_";
			case "override":
				return "override_";
			default:
				return $param;
		}
	}
}
