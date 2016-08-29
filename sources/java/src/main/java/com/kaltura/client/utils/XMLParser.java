package com.kaltura.client.utils;

import com.kaltura.client.types.KalturaAPIException;
import com.kaltura.client.KalturaObjectFactory;
import org.w3c.dom.Element;
import org.w3c.dom.Node;
import org.w3c.dom.NodeList;

import java.util.ArrayList;
import java.util.HashMap;

/**
 * Created by tehilarozin on 07/08/2016.
 */
public class XMLParser {
    public static <T> ArrayList<T> parseArray(Node aNode, Class<T> clz ) throws KalturaAPIException {
        ArrayList<T> tmpList = new ArrayList<T>();
        NodeList subNodeList = aNode.getChildNodes();
        for (int j = 0; j < subNodeList.getLength(); j++) {
            Node arrayNode = subNodeList.item(j);
            tmpList.add((T) KalturaObjectFactory.create((Element) arrayNode, clz));
        }
        return tmpList;
    }

    public static <T> HashMap<String, T> parseMap(Node aNode, Class<T> clz) throws KalturaAPIException {
        HashMap<String, T> tmpMap = new HashMap<String, T>();
        NodeList subNodeList = aNode.getChildNodes();
        for (int j = 0; j < subNodeList.getLength(); j++) {
            Node itemNode = subNodeList.item(j);
            if(itemNode instanceof Element){
                NodeList nameNodes = ((Element)itemNode).getElementsByTagName("itemKey");
                String name = nameNodes.item(0).getTextContent();

                tmpMap.put(name, (T) KalturaObjectFactory.create((Element) itemNode, clz));
            }
        }
        return tmpMap;
    }

    public static <T> T parseObject(Node aNode, Class<T> clz) throws KalturaAPIException {
        return (T) KalturaObjectFactory.create((Element)aNode, clz);
    }

}
