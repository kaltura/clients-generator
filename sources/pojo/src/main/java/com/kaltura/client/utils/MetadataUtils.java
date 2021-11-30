package com.kaltura.client.utils;

import java.io.ByteArrayInputStream;
import java.io.IOException;
import java.io.StringWriter;
import java.util.ArrayList;
import java.util.HashMap;
import java.util.LinkedHashMap;
import java.util.List;
import java.util.Map;

import javax.xml.parsers.DocumentBuilder;
import javax.xml.parsers.DocumentBuilderFactory;
import javax.xml.parsers.ParserConfigurationException;
import javax.xml.transform.OutputKeys;
import javax.xml.transform.Transformer;
import javax.xml.transform.TransformerConfigurationException;
import javax.xml.transform.TransformerException;
import javax.xml.transform.TransformerFactory;
import javax.xml.transform.dom.DOMSource;
import javax.xml.transform.stream.StreamResult;
import javax.xml.xpath.XPath;
import javax.xml.xpath.XPathConstants;
import javax.xml.xpath.XPathExpression;
import javax.xml.xpath.XPathExpressionException;
import javax.xml.xpath.XPathFactory;

import org.w3c.dom.Document;
import org.w3c.dom.Element;
import org.w3c.dom.Node;
import org.w3c.dom.NodeList;
import org.xml.sax.SAXException;

import com.kaltura.client.KalturaApiException;
import com.kaltura.client.KalturaClient;
import com.kaltura.client.enums.KalturaMetadataObjectType;
import com.kaltura.client.types.KalturaFilterPager;
import com.kaltura.client.types.KalturaMetadata;
import com.kaltura.client.types.KalturaMetadataFilter;
import com.kaltura.client.types.KalturaMetadataListResponse;
import com.kaltura.client.types.KalturaMetadataProfile;
import com.kaltura.client.types.KalturaMetadataProfileFilter;
import com.kaltura.client.types.KalturaMetadataProfileListResponse;

public class MetadataUtils {

	@SuppressWarnings("serial")
	public static class MetadataUtilsFieldNotSetException extends Exception {
		private String xPath;
		
		public MetadataUtilsFieldNotSetException(String xPath) {
			this.xPath = xPath;
		}
		
		public String getMessage(){
			return "No value defined for xPath [" + xPath + "]";
		}
	}
	
	public static void deleteMetadata(KalturaClient client, String objectId, KalturaMetadataObjectType objectType, String profileSystemName) throws KalturaApiException{
		KalturaMetadataProfile profile = getProfile(client, objectType, profileSystemName);
		deleteMetadata(client, objectId, objectType, profile.getId());
	}
	
	public static void deleteMetadata(KalturaClient client, String objectId, KalturaMetadataObjectType objectType, int profileId) throws KalturaApiException {
		KalturaMetadata metadata = get(client, objectId, objectType, profileId);
		if(metadata != null) {
			deleteMetadata(client, metadata.getId());
		}
	}
	
	public static void deleteMetadata(KalturaClient client, int metadataId) throws KalturaApiException {
		client.getMetadataService().delete(metadataId);
	}
	
	public static String getValue(KalturaClient client, String objectId, KalturaMetadataObjectType objectType, String profileSystemName, String xPath) throws KalturaApiException, XPathExpressionException, ParserConfigurationException, SAXException, IOException {
		KalturaMetadataProfile profile = getProfile(client, objectType, profileSystemName);
		return getValue(client, objectId, objectType, profile.getId(), xPath);
	}
		
	public static String getValue(KalturaClient client, String objectId, KalturaMetadataObjectType objectType, int profileId, String xPath) throws KalturaApiException, XPathExpressionException, ParserConfigurationException, SAXException, IOException {
		KalturaMetadata metadata = get(client, objectId, objectType, profileId);
		if(metadata != null) {
			return getValue(metadata.getXml(), xPath);
		}
		
		return null;
	}
	
	public static String getValue(String xml, String xPath) throws ParserConfigurationException, SAXException, IOException, XPathExpressionException {
		DocumentBuilderFactory docBuilderFactory = DocumentBuilderFactory.newInstance();
		docBuilderFactory.setIgnoringElementContentWhitespace(true);

		DocumentBuilder docBuilder = docBuilderFactory.newDocumentBuilder();
		Document doc = docBuilder.parse(new ByteArrayInputStream(xml.getBytes()));

		XPathFactory xPathFactory = XPathFactory.newInstance();
		XPath xpath = xPathFactory.newXPath();
		XPathExpression compiledExpression = xpath.compile(xPath);

		return (String) compiledExpression.evaluate(doc, XPathConstants.STRING);
	}
	
	public static List<String> getFields(String xsd) throws ParserConfigurationException, SAXException, IOException, XPathExpressionException
	{
		DocumentBuilderFactory docBuilderFactory = DocumentBuilderFactory.newInstance();
		docBuilderFactory.setIgnoringElementContentWhitespace(true);

		DocumentBuilder docBuilder = docBuilderFactory.newDocumentBuilder();
		Document doc = docBuilder.parse(new ByteArrayInputStream(xsd.getBytes()));

		NodeList nodeList = doc.getElementsByTagName("xsd:element");
		List<String> list = new ArrayList<String>();
		for (int i = 0; i < nodeList.getLength(); i++) {
			Element element = (Element)nodeList.item(i);
			String fieldName = element.getAttribute("name");
			if (fieldName.equals("metadata"))
				continue;
			
			list.add(fieldName);
		}
		
		return list;
		
	}
	
	public static LinkedHashMap<String, List<String>> getValues(String xml) throws XPathExpressionException, ParserConfigurationException, SAXException, IOException
	{
		DocumentBuilderFactory docBuilderFactory = DocumentBuilderFactory.newInstance();
		docBuilderFactory.setIgnoringElementContentWhitespace(true);

		DocumentBuilder docBuilder = docBuilderFactory.newDocumentBuilder();
		Document doc = docBuilder.parse(new ByteArrayInputStream(xml.getBytes()));

		LinkedHashMap<String, List<String>> values = new LinkedHashMap<String, List<String>>();
		NodeList nodeList = doc.getElementsByTagName("metadata");
		if (nodeList.getLength() > 0) {
			Element metadataElement = (Element)nodeList.item(0);
			NodeList metadataNodes = metadataElement.getChildNodes();
			for (int i = 0; i < metadataNodes.getLength(); i++) {
				Node tempNode = metadataNodes.item(i);
				if (!(tempNode instanceof Element))
					continue;
				String metadataFieldName = tempNode.getNodeName();
				String metadataFieldValue = tempNode.getTextContent();
				
				if (!values.containsKey(metadataFieldName))
					values.put(metadataFieldName, new ArrayList<String>());
				
				values.get(metadataFieldName).add(metadataFieldValue);
			}
		
		}
		return values;
	}
	
	public static LinkedHashMap<String, List<String>> getValuesByXsd(String xml, String xsd) throws XPathExpressionException, ParserConfigurationException, SAXException, IOException
	{
		List<String> fields = getFields(xsd);
		LinkedHashMap<String, List<String>> values = getValues(xml);
		
		LinkedHashMap<String, List<String>> valuesFromXsd = new LinkedHashMap<String, List<String>>();
		for (int i = 0; i < fields.size(); i++) {
			String field = fields.get(i);
			if (values.containsKey(field))
				valuesFromXsd.put(field, values.get(field));
			else
				valuesFromXsd.put(field, null);
		}
			
		return valuesFromXsd;
	}

	public static void setValue(KalturaClient client, String objectId, KalturaMetadataObjectType objectType, String profileSystemName, String xPath, String value) throws XPathExpressionException, ParserConfigurationException, SAXException, IOException, TransformerException, KalturaApiException, MetadataUtilsFieldNotSetException {
		KalturaMetadataProfile profile = getProfile(client, objectType, profileSystemName);
		setValue(client, objectId, objectType, profile.getId(), xPath, value);
	}

	public static void setValue(KalturaClient client, String objectId, KalturaMetadataObjectType objectType, int profileId, String xPath, String value) throws XPathExpressionException, ParserConfigurationException, SAXException, IOException, TransformerException, KalturaApiException, MetadataUtilsFieldNotSetException {
		KalturaMetadata metadata = get(client, objectId, objectType, profileId);
		if(metadata == null) {
			addMetadata(client, objectId, objectType, profileId, xPath, value);
		}
		else if (hasValue(metadata, xPath)) {
			setValue(client, metadata, xPath, value);
		}
		else {
			throw new MetadataUtilsFieldNotSetException(xPath);
		}
	}

	public static void setValue(KalturaClient client, KalturaMetadata metadata, String xPath, String value) throws XPathExpressionException, ParserConfigurationException, SAXException, IOException, TransformerException, KalturaApiException {
		DocumentBuilderFactory docBuilderFactory = DocumentBuilderFactory.newInstance();
		docBuilderFactory.setIgnoringElementContentWhitespace(true);

		DocumentBuilder docBuilder = docBuilderFactory.newDocumentBuilder();
		Document doc = docBuilder.parse(new ByteArrayInputStream(metadata.getXml().getBytes()));

		XPathFactory xPathFactory = XPathFactory.newInstance();
		XPath xpath = xPathFactory.newXPath();
		XPathExpression compiledExpression = xpath.compile(xPath);

		Node node = (Node) compiledExpression.evaluate(doc, XPathConstants.NODE);
		node.setTextContent(value);

		String xml = documentToString(doc);

		metadata = client.getMetadataService().update(metadata.getId(), xml);
	}
	
	public static String getXmlFromValues(LinkedHashMap<String, List<String>> values) throws ParserConfigurationException, TransformerException
	{
		DocumentBuilderFactory docFactory = DocumentBuilderFactory.newInstance();
		DocumentBuilder docBuilder = docFactory.newDocumentBuilder();

		Document doc = docBuilder.newDocument();
		Element metadataElement = doc.createElement("metadata");
		doc.appendChild(metadataElement);
		
		for (String field : values.keySet())
		{
			List<String> fieldValues = values.get(field);
			if (fieldValues == null)
				continue;
		
			for(String fieldValue : fieldValues)
			{
				Element fieldElement = doc.createElement(field);
				fieldElement.setTextContent(fieldValue);
				metadataElement.appendChild(fieldElement);
			}
		}
		
		String xml = documentToString(doc);
		return xml;
	}

	public static KalturaMetadata addMetadata(KalturaClient client, String objectId, KalturaMetadataObjectType objectType, String profileSystemName, String xPath, String value) throws ParserConfigurationException, TransformerException, KalturaApiException {
		KalturaMetadataProfile profile = getProfile(client, objectType, profileSystemName);
		return addMetadata(client, objectId, objectType, profile.getId(), xPath, value);
	}
	
	public static KalturaMetadata addMetadata(KalturaClient client, String objectId, KalturaMetadataObjectType objectType, int profileId, String xPath, String value) throws ParserConfigurationException, TransformerException, KalturaApiException {
		DocumentBuilderFactory docFactory = DocumentBuilderFactory.newInstance();
		DocumentBuilder docBuilder = docFactory.newDocumentBuilder();

		Document doc = docBuilder.newDocument();

		String[] elements = xPath.split("/");
		Node parentElement = doc;
		Element element = null;
		for (String elementName : elements) {
			if (elementName.length() > 0) {
				element = doc.createElement(elementName);
				parentElement.appendChild(element);
				parentElement = element;
			}
		}
		parentElement.setTextContent(value);

		String xml = documentToString(doc);
		
		return addMetadata(client, objectId, objectType, profileId, xml);
	}

	public static KalturaMetadata addMetadata(KalturaClient client, String objectId, KalturaMetadataObjectType objectType, String profileSystemName, String xml) throws ParserConfigurationException, TransformerException, KalturaApiException {
		KalturaMetadataProfile profile = getProfile(client, objectType, profileSystemName);
		return client.getMetadataService().add(profile.getId(), objectType, objectId, xml);
	}

	public static KalturaMetadata addMetadata(KalturaClient client, String objectId, KalturaMetadataObjectType objectType, int profileId, String xml) throws ParserConfigurationException, TransformerException, KalturaApiException {
		return client.getMetadataService().add(profileId, objectType, objectId, xml);
	}

	public static boolean hasValue(KalturaClient client, String objectId, KalturaMetadataObjectType objectType, String profileSystemName, String xPath) throws KalturaApiException, XPathExpressionException, ParserConfigurationException, SAXException, IOException {
		KalturaMetadataProfile profile = getProfile(client, objectType, profileSystemName);
		return hasValue(client, objectId, objectType, profile.getId(), xPath);
	}
		
	public static boolean hasValue(KalturaClient client, String objectId, KalturaMetadataObjectType objectType, int profileId, String xPath) throws KalturaApiException, XPathExpressionException, ParserConfigurationException, SAXException, IOException {
		KalturaMetadata metadata = get(client, objectId, objectType, profileId);
		return hasValue(metadata, xPath);
	}
	
	public static boolean hasValue(KalturaMetadata metadata, String xPath) throws ParserConfigurationException, SAXException, IOException, XPathExpressionException {
		return hasValue(metadata.getXml(), xPath);
	}
	
	public static boolean hasValue(String xml, String xPath) throws ParserConfigurationException, SAXException, IOException, XPathExpressionException {
		DocumentBuilderFactory docBuilderFactory = DocumentBuilderFactory.newInstance();
		docBuilderFactory.setIgnoringElementContentWhitespace(true);

		DocumentBuilder docBuilder = docBuilderFactory.newDocumentBuilder();
		Document doc = docBuilder.parse(new ByteArrayInputStream(xml.getBytes()));

		XPathFactory xPathFactory = XPathFactory.newInstance();
		XPath xpath = xPathFactory.newXPath();
		XPathExpression compiledExpression = xpath.compile(xPath);

		return (Boolean) compiledExpression.evaluate(doc, XPathConstants.BOOLEAN);
	}
	
	public static KalturaMetadataProfile getProfile(KalturaClient client, KalturaMetadataObjectType objectType, String profileSystemName) throws KalturaApiException {
		KalturaMetadataProfileFilter filter = new KalturaMetadataProfileFilter();
		filter.setMetadataObjectTypeEqual(objectType);
		filter.setSystemNameEqual(profileSystemName);
		
		KalturaFilterPager pager = new KalturaFilterPager();
		pager.setPageSize(1);
		
		KalturaMetadataProfileListResponse metadataProfileList = client.getMetadataProfileService().list(filter, pager);
		if(metadataProfileList.getObjects().size() > 0){
			return metadataProfileList.getObjects().get(0);
		}
		
		return null;
	}
	
	public static KalturaMetadata get(KalturaClient client, String objectId, KalturaMetadataObjectType objectType, int profileId) throws KalturaApiException {
		KalturaMetadataFilter filter = new KalturaMetadataFilter();
		filter.setObjectIdEqual(objectId);
		filter.setMetadataObjectTypeEqual(objectType);
		filter.setMetadataProfileIdEqual(profileId);
		
		KalturaFilterPager pager = new KalturaFilterPager();
		pager.setPageSize(1);
		
		KalturaMetadataListResponse metadataList = client.getMetadataService().list(filter, pager);
		if(metadataList.getObjects().size() > 0){
			return metadataList.getObjects().get(0);
		}
		
		return null;
	}

	private static String documentToString(Document doc) throws TransformerException
	{
		TransformerFactory transformerFactory = TransformerFactory.newInstance();
		Transformer transformer = transformerFactory.newTransformer();
		transformer.setOutputProperty(OutputKeys.OMIT_XML_DECLARATION, "yes");
		StringWriter writer = new StringWriter();
		transformer.transform(new DOMSource(doc), new StreamResult(writer));
		String xml = writer.getBuffer().toString().replaceAll("\n|\r", "");
		return xml;
	}
}
