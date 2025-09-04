<?php
class TestmeDocGenerator extends ClientGeneratorFromXml
{
	function __construct($xmlPath, Zend_Config $config, $sourcePath = "testmeDoc")
	{
		parent::__construct($xmlPath, $sourcePath, $config);
	}
	
	function generate()
	{
		parent::generate();

		$classes = array();
		$enums = array();
		$xpath = new DOMXPath($this->_doc);

		$enumNodes = $xpath->query("/xml/enums/enum");
		foreach($enumNodes as $enumNode)
		{
			$type = $enumNode->getAttribute('name');
			if(!$this->shouldIncludeType($type))
				continue;

			$this->writeEnum($enumNode);
			$enums[] = $type;
		}
		$enumNodes = null;
		
		// classes
		$classNodes = $xpath->query("/xml/classes/class");
		foreach($classNodes as $classNode)
		{
			$type = $classNode->getAttribute('name');
			if(!$this->shouldIncludeType($type) || $type == 'KalturaObject')
				continue;
					
			$this->writeClass($classNode);
			$classes[] = $type;
		}
		$classNodes = null;
		
		
		$serviceNodes = $xpath->query("/xml/services/service");
		foreach($serviceNodes as $serviceNode)
		{
			if(!$this->shouldIncludeService($serviceNode->getAttribute('id')))
				continue;

			$this->writeService($serviceNode);
			$services[] = $serviceNode->getAttribute('name');
		}
		$serviceNodes = null;
		
	    $this->writeMenu($services, $enums, $classes);
	}
	
	/**
	 * @param string $type
	 * @return array<DOMElement>
	 */
	function getExtendingClasses($type)
	{
		$subClasses = array();
		$xpath = new DOMXPath($this->_doc);
		$classNodes = $xpath->query("/xml/classes/class[@base='$type']");
		foreach($classNodes as $classNode)
		{
			$subType = $classNode->getAttribute("name");
			$subClasses[$subType] = $classNode;
			$subSubClasses = $this->getExtendingClasses($subType);
			foreach($subSubClasses as $subSubClass)
			{
				$subSubType = $subSubClass->getAttribute("name");
				$subClasses[$subSubType] = $subSubClass;
			}
		}
		
		return $subClasses;
	}
	
	function appendHeader($cssPrefix = '../', $jquery = '')
	{
		$headers = $this->getTextBlock();
		
		$this->appendLine('<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">');
		$this->appendLine('<html xmlns="http://www.w3.org/1999/xhtml" xml:lang="en" lang="en">');
		$this->appendLine('<head>');
		$this->appendLine('	<meta http-equiv="Content-Type" content="text/html; charset=windows-1252" />');
		$this->appendLine('	<title>Kaltura - TestMe Console Documentation</title>');
		
		$this->appendLine('	<link rel="stylesheet" type="text/css" href="' . $cssPrefix . 'css/main.css" />');
		
		if($jquery)
			$this->appendLine('	<script type="text/javascript" src="' . $jquery . '"></script>');
		
		$this->append($headers);
		$this->appendLine('</head>');
		$this->appendLine('<body>');
	}
	
	function appendFooter()
	{
		$this->appendLine('</body>');
		$this->appendLine('</html>');
	}
	
	function writeEnum(DOMElement $enumNode)
	{
		$type = $enumNode->getAttribute("name");
		$enumType = $enumNode->getAttribute("enumType");

		$this->startNewFile("enums/$type.html");
		$this->appendHeader();
		
		$this->appendLine('	<div id="doc">');
		$this->appendLine('		<h2>Kaltura API</h2>');
		$this->appendLine('		<table class="action" width="80%">');
		$this->appendLine('			<tr>');
		$this->appendLine("				<th class=\"service_action_title\">$type ($enumType)</th>");
		$this->appendLine('			</tr>');
		$this->appendLine('			<tr>');
		$this->appendLine('				<td class="title">Enumerations</td>');
		$this->appendLine('			</tr>');
		$this->appendLine('			<tr>');
		$this->appendLine('				<td>');
		$this->appendLine('					<table style="">');
		$this->appendLine('						<tbody>');
		$this->appendLine('							<tr>');
		$this->appendLine('								<th class="subtitle">Name</th>');
		$this->appendLine('								<th class="subtitle">Value</th>');
		$this->appendLine('							</tr>');

		$odd = false;
		foreach($enumNode->childNodes as $constNode)
		{
			if ($constNode->nodeType != XML_ELEMENT_NODE)
				continue;

			$constName = $constNode->getAttribute('name');
			$constValue = $constNode->getAttribute('value');
			
			$this->appendLine('							<tr ' . ($odd ? 'class="odd"' : '') . '>');
			$this->appendLine("								<td>$constName</td>");
			$this->appendLine("								<td>$constValue</td>");
			$this->appendLine('							</tr>');

			$odd = !$odd;
		}
		
		$this->appendLine('						</tbody>');
		$this->appendLine('					</table>');
		$this->appendLine('				</td>');
		$this->appendLine('			</tr>');
		$this->appendLine('		</table>');
		$this->appendLine('	</div>');
	
		$this->appendFooter();
		
		$this->closeFile();
	}
	
	function writeClassBase($class, DOMElement $classNode)
	{
		$type = $classNode->getAttribute("type");
		$base = $classNode->getAttribute("base");
		
		if($base)
		{
			$xpath = new DOMXPath($this->_doc);
			$baseNodes = $xpath->query("/xml/classes/class[@name='$base']");
			$baseNode = $baseNodes->item(0);
			if(!$baseNode)
				throw new Exception("parent type [$base] not found for type []");

			$this->writeClassBase($class, $baseNode);

			$hasProperties = false;
			foreach($baseNode->childNodes as $propertyNode)
			{
				if ($propertyNode->nodeType == XML_ELEMENT_NODE)
				{
					$hasProperties = true;
					break;
				}
			}
			
			$this->appendLine('			<tr>');
			$this->appendLine('				<td>');
			$this->appendLine('					<table style="">');
			$this->appendLine('						<tbody>');
			$this->appendLine('							<tr>');
			$this->appendLine('								<td class="inheritedFrom">');
			
			if($hasProperties)
			{
				$this->appendLine("									<b><img class=\"base-attribute\" src=\"../images/collapsed.gif\" alt=\"$base\" /> Inherited from <a href=\"$base.html\">$base</a></b>");
			}
			else
			{
				$this->appendLine("									<b>Inherited from <a href=\"$base.html\">$base</a></b>");
			}
			$this->appendLine('								</td>');
			$this->appendLine('							</tr>');
			$this->appendLine('						</tbody>');
			$this->appendLine('					</table>');
			

			if($hasProperties)
			{
				$this->appendLine('					<table style="display: none">');
				$this->appendLine('						<tbody>');
				$this->appendLine('							<tr>');
				$this->appendLine('								<th class="subtitle">Name</th>');
				$this->appendLine('								<th class="subtitle">Type</th>');
				$this->appendLine('								<th class="subtitle">Writable</th>');
				$this->appendLine('								<th class="subtitle">Restrictions</th>');
				$this->appendLine('								<th class="subtitle">Description</th>');
				$this->appendLine('							</tr>');
				
				$this->writeClassProperties($class, $baseNode);
	
				$this->appendLine('						</tbody>');
				$this->appendLine('					</table>');
			}
			
			$this->appendLine('				</td>');
			$this->appendLine('			</tr>');
		}
		else 
		{
			$this->appendLine('			<tr>');
			$this->appendLine('				<td>');
			$this->appendLine('					<table style="">');
			$this->appendLine('						<tbody>');
			$this->appendLine('							<tr>');
			$this->appendLine('								<td class="inheritedFrom">');
			$this->appendLine('									<b><img class="base-attribute" src="../images/collapsed.gif" alt="KalturaObjectBase" /> Inherited from KalturaObjectBase</b>');
			$this->appendLine('								</td>');
			$this->appendLine('							</tr>');
			$this->appendLine('						</tbody>');
			$this->appendLine('					</table>');
			$this->appendLine('					<table style="display: none">');
			$this->appendLine('						<tbody>');
			$this->appendLine('							<tr>');
			$this->appendLine('								<th class="subtitle">Name</th>');
			$this->appendLine('								<th class="subtitle">Type</th>');
			$this->appendLine('								<th class="subtitle">Writable</th>');
			$this->appendLine('								<th class="subtitle">Restrictions</th>');
			$this->appendLine('								<th class="subtitle">Description</th>');
			$this->appendLine('							</tr>');
			$this->appendLine('							<tr>');
			$this->appendLine('								<td>relatedObjects</td>');
			$this->appendLine('								<td>map of <a href="KalturaListResponse.html">KalturaListResponse</a></td>');
			$this->appendLine('								<td></td>');
			$this->appendLine('								<td></td>');
			$this->appendLine('								<td></td>');
			$this->appendLine('							</tr>');
			$this->appendLine('						</tbody>');
			$this->appendLine('					</table>');
			$this->appendLine('				</td>');
			$this->appendLine('			</tr>');
		}
	}
	
	function getOrderByEnum($class)
	{
		$enumType = preg_replace('/Filter$/', 'OrderBy', preg_replace('/BaseFilter$/', 'Filter', $class));

		$xpath = new DOMXPath($this->_doc);
		$enumNodes = $xpath->query("/xml/enums/enum[@name='$enumType']");
		if($enumNodes->length)
			return $enumType;

		$classNodes = $xpath->query("/xml/classes/class[@name='$class']");
		$classNode = $classNodes->item(0);
		if($classNode && $classNode->hasAttribute('base'))
		{
			$parentClass = $classNode->getAttribute('base');
			$parentNodes = $xpath->query("/xml/classes/class[@name='$parentClass']");
			$parentNode = $parentNodes->item(0);
			if($parentNode)
				return $this->getOrderByEnum($parentClass);
		}
			
		return null;
	}
	
	function writeClassProperties($class, DOMElement $classNode)
	{
		$odd = false;
		foreach($classNode->childNodes as $propertyNode)
		{
			if ($propertyNode->nodeType != XML_ELEMENT_NODE)
				continue;

			$name = $propertyNode->getAttribute('name');
			$type = $propertyNode->getAttribute('type');
			$description = $propertyNode->getAttribute('description');
			$writable = $propertyNode->getAttribute('readOnly') == '1' ? '' : 'V';
			
			$restrictions = array();
			if($propertyNode->getAttribute('minLength'))
				$restrictions[] = 'Minimum length: ' . $propertyNode->getAttribute('minLength');
			if($propertyNode->getAttribute('maxLength'))
				$restrictions[] = 'Maximum length: ' . $propertyNode->getAttribute('maxLength');
			if($propertyNode->getAttribute('minValue'))
				$restrictions[] = 'Minimum value: ' . $propertyNode->getAttribute('minValue');
			if($propertyNode->getAttribute('maxValue'))
				$restrictions[] = 'Maximum value: ' . $propertyNode->getAttribute('maxValue');
			
			$restrictions = implode('<br/>', $restrictions);

			if($name == 'orderBy' && $this->endsWith($class, 'Filter') && $class != 'KalturaFilter')
			{
				$enumType = $this->getOrderByEnum($class);
				if($enumType)
					$type = "<a href=\"../enums/$enumType.html\">$enumType</a>";
			}
			elseif($type == 'array' || $type == 'map')
			{
				$arrayType = $propertyNode->getAttribute("arrayType");
				if($arrayType == 'KalturaObject')
					$arrayType = 'KalturaObjectBase';
				
				$type = "$type of <a href=\"$arrayType.html\">$arrayType</a>";
			}
			elseif($propertyNode->getAttribute("enumType"))
			{
				$enumType = $propertyNode->getAttribute("enumType");
				$type = "<a href=\"../enums/$enumType.html\">$enumType</a>";
			}
			elseif(!$this->isSimpleType($type) && $type != 'file')
			{
				$type = "<a href=\"$type.html\">$type</a>";
			}

			$this->appendLine('							<tr ' . ($odd ? 'class="odd"' : '') . '>');
			$this->appendLine("								<td>$name</td>");
			$this->appendLine("								<td>$type</td>");
			$this->appendLine("								<td>$writable</td>");
			$this->appendLine("								<td>$restrictions</td>");
			$this->appendLine("								<td>$description</td>");
			$this->appendLine('							</tr>');
			
			$odd = !$odd;
		}
	}
	
	function writeClass(DOMElement $classNode)
	{
		$type = $classNode->getAttribute("name");
		if(!$this->shouldIncludeType($type))
			return;

		$this->startNewFile("objects/$type.html");

		$this->appendHeader('../', '../../testme/js/jquery-1.3.1.min.js');
		
		$this->appendLine('	<script type="text/javascript">');
		$this->appendLine('	');
		$this->appendLine('		$(document).ready(function() {');
		$this->appendLine('			$(".base-attribute").click(function() {');
		$this->appendLine('				var classHeaderTable = $(this).parents("table:first");');
		$this->appendLine('				var propertiesTable = classHeaderTable.parent().find("table:last");');
		$this->appendLine('				if ($(this).attr("src").indexOf("collapsed.gif") >= 0) {');
		$this->appendLine('				$(this).attr("src", "../images/expended.gif");');
		$this->appendLine('					propertiesTable.show();');
		$this->appendLine('				}');
		$this->appendLine('				else {');
		$this->appendLine('					$(this).attr("src", "../images/collapsed.gif");');
		$this->appendLine('					propertiesTable.hide();');
		$this->appendLine('				}');
		$this->appendLine('			});');
		$this->appendLine('		});');
		$this->appendLine('	');
		$this->appendLine('	</script>');
		
		$this->appendLine('	<div id="doc">');
		$this->appendLine('		<h2>Kaltura API</h2>');
		$this->appendLine('		<table class="action" width="80%">');
		$this->appendLine('			<tr>');
		$this->appendLine('				<th class="service_action_title">' . $type . '</th>');
		$this->appendLine('			</tr>');
		$this->appendLine('			<tr>');
		$this->appendLine('				<td class="title">Properties</td>');
		$this->appendLine('			</tr>');
		
		$this->writeClassBase($type, $classNode);
		
		$this->appendLine('			<tr>');
		$this->appendLine('				<td>');
		$this->appendLine('					<table style="">');
		$this->appendLine('						<tbody>');
		$this->appendLine('							<tr>');
		$this->appendLine('								<td class="inheritedFrom"><b>' . $type . '</b></td>');
		$this->appendLine('							</tr>');
		$this->appendLine('						</tbody>');
		$this->appendLine('					</table>');
		$this->appendLine('					<table style="">');
		
		$this->appendLine('						<tbody>');
		$this->appendLine('							<tr>');
		$this->appendLine('								<th class="subtitle">Name</th>');
		$this->appendLine('								<th class="subtitle">Type</th>');
		$this->appendLine('								<th class="subtitle">Writable</th>');
		$this->appendLine('								<th class="subtitle">Restrictions</th>');
		$this->appendLine('								<th class="subtitle">Description</th>');
		$this->appendLine('							</tr>');
		
		$this->writeClassProperties($type, $classNode);
		
		$this->appendLine('						</tbody>');
		$this->appendLine('					</table>');
		$this->appendLine('				</td>');
		$this->appendLine('			</tr>');

		$subClasses = $this->getExtendingClasses($type);
		$links = array();
		foreach($subClasses as $subClass)
		{
			/* @var $subClass DOMElement */
			$subClassType = $subClass->getAttribute('name');
			if($this->shouldIncludeType($subClassType))
				$links[] = "<a href=\"$subClassType.html\">$subClassType</a>";
		}
		
		if(count($links))
		{
			$this->appendLine('			<tr>');
			$this->appendLine('				<td>Sub classes: ' . implode(', ', $links) . '</td>');
			$this->appendLine('			</tr>');
		}
		
		$this->appendLine('		</table>');
		$this->appendLine('	</div>');
		
		$this->appendFooter();
		
		if($type == 'KalturaObject')
			$type = 'KalturaObjectBase';
		
		$this->closeFile();
	}
	
	function writeService(DOMElement $serviceNode)
	{
		$serviceId = $serviceNode->getAttribute("id");
		$description = $serviceNode->getAttribute("description");
		
		$actionNodes = $serviceNode->getElementsByTagName("action");
		foreach($actionNodes as $actionNode)
		{
            $this->writeAction($serviceId, $actionNode);
		}

		$this->startNewFile("services/$serviceId.html");
		$this->appendHeader();
		
		$this->appendLine('	<div id="doc">');
		$this->appendLine('		<h2>Kaltura API</h2>');
		$this->appendLine('		<table id="serviceInfo">');
		$this->appendLine('			<tr>');
		$this->appendLine('				<td class="title">Service Name</td>');
		$this->appendLine('				<td class="odd">' . $serviceId . '</td>');
		$this->appendLine('			</tr>');
		$this->appendLine('			<tr>');
		$this->appendLine('				<td class="title">Description</td>');
		$this->appendLine('				<td>' . $description . '<br />');
		$this->appendLine('				</td>');
		$this->appendLine('			</tr>');
		$this->appendLine('			<tr>');
		$this->appendLine('				<td class="title">Actions</td>');
		$this->appendLine('				<td class="odd">');
		$this->appendLine('					<table cellspacing="0" class="service_actions">');
		$this->appendLine('						<tr>');
		$this->appendLine('							<th>Name</th>');
		$this->appendLine('							<th>Description</th>');
		$this->appendLine('						</tr>');
		
		foreach($actionNodes as $actionNode)
		{
			$actionId = $actionNode->getAttribute('name');
			if(!$this->shouldIncludeAction($serviceId, $actionId))
				continue;
			
			$actionDescription = $actionNode->getAttribute('description');
			
			$this->appendLine('						<tr>');
			$this->appendLine("							<td><a href=\"../actions/$serviceId.$actionId.html\">$actionId</td>");
			$this->appendLine('							<td>' . $actionDescription . '</td>');
			$this->appendLine('						</tr>');
		}
		
		$this->appendLine('					</table>');
		$this->appendLine('				</td>');
		$this->appendLine('			</tr>');
		$this->appendLine('		</table>');
		$this->appendLine('	</div>');

		$this->appendFooter();
		
		$this->closeFile();
	}
	
	function appendJsonType($propertyNode, $indent = "\t", array $loadedTypes = array())
	{
		$name = $propertyNode->getAttribute("name");
		$type = $propertyNode->getAttribute("type");
		if(!$name || !$type)
			return;
	
		if($this->isSimpleType($type) || $type == 'file')
		{
			$value = ($type == 'string' || $type == 'file') ? '"value"' : 'value';
			$this->append($indent);
			$this->append("$name: ");
			$this->append($value);
		}
		elseif($type == 'array')
		{
			$arrayType = $propertyNode->getAttribute("arrayType");
			$this->append($indent);
			$this->append("$name: [");
			$this->appendJsonObject($arrayType, $indent, $loadedTypes);
			$this->append("]");
		}
		elseif($type == 'map')
		{
			$arrayType = $propertyNode->getAttribute("arrayType");
			$this->append($indent);
			$this->appendLine("$name: {");
			$this->append($indent);
			$this->append("\t'item1': ");
			$this->appendJsonObject($arrayType, $indent, $loadedTypes);
			$this->appendLine();
			$this->append("}");
		}
		else {
			$this->append("{$indent}{$name}: ");
			$this->appendJsonObject($type, $indent, $loadedTypes);
		}
	}
	
	private function appendJsonObjectAttributes(DOMElement $classNode, $indent, array $loadedTypes)
	{	
		foreach($classNode->childNodes as $propertyNode)
		{
			if ($propertyNode->nodeType != XML_ELEMENT_NODE)
				continue;

			if($propertyNode->getAttribute('readOnly') == '1')
				continue;
			
			$this->appendLine(',');
			$this->appendJsonType($propertyNode, "$indent\t", $loadedTypes);
		}
	}
	
	function appendJsonObject($type, $indent, array $loadedTypes)
	{
		$xpath = new DOMXPath($this->_doc);
		$classNodes = $xpath->query("/xml/classes/class[@name='$type']");
		$classNode = $classNodes->item(0);
		if(!$classNode)
			throw new Exception("Type [$type] not found");
		$classNodes = null;

		$this->appendLine("{");
		$this->append("{$indent}\tobjectType: \"$type\"");

		if(!isset($loadedTypes[$type]))
		{
			$loadedTypes[$type] = true;
			$base = $classNode->getAttribute('base');
			if($base)
			{
				$baseNodes = $xpath->query("/xml/classes/class[@name='$base']");
				$baseNode = $baseNodes->item(0);
				if(!$baseNode)
					throw new Exception("Type [$base] not found");
				$baseNodes = null;
				
				$this->appendJsonObjectAttributes($baseNode, $indent, $loadedTypes);
				$baseNode = null;
			}
			
			$this->appendJsonObjectAttributes($classNode, $indent, $loadedTypes);
		}
		$this->appendLine();
		$this->append("{$indent}}");
	}
	
	function writeAction($serviceId, DOMElement $actionNode)
	{
		$actionId = $actionNode->getAttribute('name');
		if(!$this->shouldIncludeAction($serviceId, $actionId))
			return;
		
		$description = $actionNode->getAttribute('description');

		$serviceTitle = $serviceId.$actionId;
		if ($actionNode->getAttribute('beta'))
			$serviceTitle .= ' (beta)';


		$this->startNewFile("actions/$serviceId.$actionId.html");
		$this->appendHeader();
		
		$this->appendLine('	<div id="doc">');
		$this->appendLine('		<h2>Kaltura API</h2>');
		$this->appendLine('		<table class="action">');
		$this->appendLine('			<tr>');
		$this->appendLine("				<th colspan=\"5\" class=\"service_action_title\">$serviceTitle</th>");
		$this->appendLine('			</tr>');
		$this->appendLine('			<tr>');
		$this->appendLine('				<td colspan="5" class="title">Description:</td>');
		$this->appendLine('			</tr>');
		$this->appendLine('			<tr>');
		$this->appendLine('				<td class="description" colspan="5">' . $description . '</td>');
		$this->appendLine('			</tr>');
		$this->appendLine('			<tr>');
		$this->appendLine('				<td colspan="5" class="title">Input Params</td>');
		$this->appendLine('			</tr>');
		$this->appendLine('			<tr>');
		$this->appendLine('				<th class="subtitle">Name</th>');
		$this->appendLine('				<th class="subtitle">Type</th>');
		$this->appendLine('				<th class="subtitle">Description</th>');
		$this->appendLine('				<th class="subtitle">Required</th>');
		$this->appendLine('				<th class="subtitle">Default Value</th>');
		$this->appendLine('			</tr>');
		
		$description = null;

		$paramNodes = $actionNode->getElementsByTagName('param');
		foreach($paramNodes as $paramNode)
		{
			if ($paramNode->nodeType != XML_ELEMENT_NODE)
				continue;
			
			$paramType = $paramNode->getAttribute('type');
			$paramName = $paramNode->getAttribute('name');
			$paramDescription = $paramNode->getAttribute('description');
			$enumType = $paramNode->getAttribute('enumType');
			$required = $paramNode->getAttribute('optional') ? '' : 'V';
			$default = $paramNode->hasAttribute('default');

			if($enumType)
				$paramType = $enumType;
		
			$this->appendLine('			<tr>');
			$this->appendLine("				<td>$paramName</td>");
			if($this->isComplexType($paramType))
			{
				if($paramType == 'array' || $paramType == 'map')
				{
					$arrayType = $paramNode->getAttribute("arrayType");
					$this->appendLine("				<td colspan=\"5\">$paramType of <a href=\"../objects/$arrayType.html\">$arrayType</a></td>");
				}
				else if($enumType)
				{
					$this->appendLine("				<td><a href=\"../enums/$paramType.html\">$paramType</a></td>");
				}
				else
				{
					$this->appendLine("				<td><a href=\"../objects/$paramType.html\">$paramType</a></td>");
				}
			}
			else
			{
				$this->appendLine("				<td>$paramType</td>");
			}
			$this->appendLine("				<td>$paramDescription</td>");
			$this->appendLine("				<td>$required</td>");
			$this->appendLine("				<td>$default</td>");
			$this->appendLine('			</tr>');
		}

		$resultNode = $actionNode->getElementsByTagName("result")->item(0);
		$resultType = $resultNode->getAttribute("type");
		if($resultType)
		{
			$this->appendLine('			<tr>');
			$this->appendLine('				<td colspan="5" class="title">Output Type</td>');
			$this->appendLine('			</tr>');
			$this->appendLine('			<tr>');
			if($this->isComplexType($resultType))
			{
				if($resultType == 'array' || $resultType == 'map')
				{
					$arrayType = $resultNode->getAttribute("arrayType");
					$this->appendLine("				<td colspan=\"5\">$resultType of <a href=\"../objects/$arrayType.html\">$arrayType</a></td>");
				}
				else
				{
					$this->appendLine("				<td colspan=\"5\"><a href=\"../objects/$resultType.html\">$resultType</a></td>");
				}
			}
			else
			{
				$this->appendLine("				<td colspan=\"5\">$resultType</td>");
			}
			$this->appendLine('			</tr>');
		}
		$resultNode = null;
		$resultType = null;
		
		$this->appendLine('			<tr>');
		$this->appendLine('				<td colspan="5" class="title">Example HTTP Hit</td>');
		$this->appendLine('			</tr>');
		$this->appendLine('			<tr>');
		$this->appendLine("				<td colspan=\"5\">https://www.kaltura.com/api_v3/service/$serviceId/action/$actionId<br />");
		$this->appendLine('				<strong>JSON object:</strong>');
		$this->appendLine('				<pre>');
		$this->appendLine('				<div class="post_fields">');
		$this->appendLine("{");
		
		$first = true;
		foreach($paramNodes as $paramNode)
		{
			if ($paramNode->nodeType != XML_ELEMENT_NODE)
				continue;
			
			if($first)
			{
				$first = false;
			}
			else
			{
				$this->appendLine(',');				
			}
			$this->appendJsonType($paramNode);
		}
		
		$this->appendLine();
		$this->appendLine("}");
		$this->appendLine('				</pre></td>');
		$this->appendLine('			</tr>');
		$this->appendLine('		</table>');
		$this->appendLine('	</div>');

		$this->appendFooter();
		
		$this->closeFile();
	}
	
	function writeMenu(array $services, array $enums, array $classes)
	{
		$xpath = new DOMXPath($this->_doc);

		sort($services);
		sort($enums);
		sort($classes);
		
		$this->startNewFile('menu.html');

		$this->appendHeader('', '../testme/js/jquery-1.3.1.min.js');
		
		$this->appendLine('	<script type="text/javascript">');
		$this->appendLine('	');
		$this->appendLine('	function collapseService($service){');
		$this->appendLine('		$service.removeClass("expended");');
		$this->appendLine('		var $img = $service.children();');
		$this->appendLine('		var src = $img.attr("src").replace("expended", "collapsed");');
		$this->appendLine('		$img.attr("src", src);');
		$this->appendLine('	}');
		$this->appendLine('	');
		$this->appendLine('	function expandService($service){');
		$this->appendLine('		$service.addClass("expended");');
		$this->appendLine('		var $img = $service.children();');
		$this->appendLine('		var src = $img.attr("src").replace("collapsed", "expended");');
		$this->appendLine('		$img.attr("src", src);');
		$this->appendLine('	}');
		$this->appendLine('	');
		$this->appendLine('	$(function() {');
		$this->appendLine('		$(".tree-content").click(function(){');
		$this->appendLine('			var $service = $(this).parent();');
		$this->appendLine('			expandService($service);');
		$this->appendLine('		});');
		$this->appendLine('		');		
		$this->appendLine('		$(".tree-item").click(function(){');
		$this->appendLine('			var $service = $(this).parent();');
		$this->appendLine('			if($service.hasClass("expended")){');
		$this->appendLine('				collapseService($service);');
		$this->appendLine('			}');
		$this->appendLine('			else {');
		$this->appendLine('				expandService($service);');
		$this->appendLine('			}');
		$this->appendLine('		});');
		$this->appendLine('	});');

		$this->appendLine('	</script>');
			
		$this->appendLine('	<div class="left">');
		$this->appendLine('		<div class="left-content">');
		$this->appendLine('			<div id="general">');
		$this->appendLine('				<h2>General</h2>');
		$this->appendLine('				<ul>');
		$this->appendLine('					<li><a href="general/overview.html" target="main">Overview</a></li>');
		$this->appendLine('					<li><a href="general/terminology.html" target="main">Terminology</a></li>');
		$this->appendLine('					<li><a href="general/inout.html" target="main">Request/Response structure</a></li>');
		$this->appendLine('					<li><a href="general/multirequest.html" target="main">multiRequest</a></li>');
		$this->appendLine('					<li><a href="general/notifications.html" target="main">Notifications</a></li>');
		$this->appendLine('				</ul>');
		$this->appendLine('			</div>');

		$this->appendLine('			<div id="services">');
		$this->appendLine('				<h2>Services</h2>');
		$this->appendLine('				<ul class="services">');

		foreach($services as $serviceName)
		{
			$serviceNodes = $xpath->query("/xml/services/service[@name = '$serviceName']");
			$serviceNode = $serviceNodes->item(0);

			$serviceId = $serviceNode->getAttribute('id');
			$pluginName = $serviceNode->getAttribute('plugin');
			if($serviceNode->getAttribute('deprecated'))
				$serviceName .= ' (deprecated)';

			$this->appendLine("					<li class=\"service\" id=\"$serviceId\">");
			$this->appendLine('						<img class="tree-item" src="images/collapsed.gif" />');
			$this->appendLine("						<a class=\"tree-content\" href=\"services/$serviceId.html\" target=\"main\">$serviceName</a>");
			$this->appendLine('						<ul class="actions">');

			$actionNodes = $serviceNode->getElementsByTagName("action");
			foreach($actionNodes as $actionNode)
			{
				$actionId = $actionNode->getAttribute('name');
				if(!$this->shouldIncludeAction($serviceId, $actionId))
					continue;
				
				$this->appendLine("							<li class=\"action\"><a href=\"actions/$serviceId.$actionId.html\" target=\"main\">$actionId</a></li>");
			}
			$this->appendLine('						</ul></li>');
		}

		$this->appendLine('				</ul>');
		$this->appendLine('			</div>');
		
		
		

		$this->appendLine('			<div id="objects">');
		$this->appendLine('				<h2>General Objects</h2>');
		$this->appendLine('				<ul>');

		foreach($classes as $type)
		{
			if($this->endsWith($type, 'Filter'))
				continue;
		
			$this->appendLine("					<li id=\"object_{$type}\"><a href=\"objects/$type.html\" target=\"main\">$type</a></li>");
		}
		
		$this->appendLine('				</ul>');
		$this->appendLine('			</div>');
		
		
		

		$this->appendLine('			<div id="objects">');
		$this->appendLine('				<h2>Filter Objects</h2>');
		$this->appendLine('				<ul>');

		foreach($classes as $type)
		{
			if($this->endsWith($type, 'Filter'))
				$this->appendLine("					<li id=\"object_{$type}\"><a href=\"objects/$type.html\" target=\"main\">$type</a></li>");
		}
		
		$this->appendLine('				</ul>');
		$this->appendLine('			</div>');
		
		
		

		$this->appendLine('			<div id="enums">');
		$this->appendLine('				<h2>Enums</h2>');
		$this->appendLine('				<ul>');

		foreach($enums as $type)
		{
			$this->appendLine("					<li id=\"object_{$type}\"><a href=\"enums/$type.html\" target=\"main\">$type</a></li>");
		}
		
		$this->appendLine('				</ul>');
		$this->appendLine('			</div>');
		
		

		$this->appendLine('		</div>');
		$this->appendLine('	</div>');
		$this->appendFooter();
		
		$this->closeFile();
	}
}
