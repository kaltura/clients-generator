package com.kaltura.client.test.utils;

import com.kaltura.client.Logger;
import com.kaltura.client.types.APIException;
import org.w3c.dom.Document;
import org.w3c.dom.Element;
import org.w3c.dom.NodeList;
import org.xml.sax.SAXException;

import javax.xml.parsers.DocumentBuilder;
import javax.xml.parsers.DocumentBuilderFactory;
import javax.xml.parsers.ParserConfigurationException;
import java.io.IOException;
import java.net.URL;
import java.text.DateFormat;
import java.text.SimpleDateFormat;
import java.util.*;
import java.util.concurrent.ThreadLocalRandom;

import static com.kaltura.client.test.Properties.API_VERSION;
import static com.kaltura.client.test.Properties.getProperty;

public class BaseUtils {

    private static final String API_SCHEMA_URL = "https://api-preprod.ott.kaltura.com/" + getProperty(API_VERSION) + "/clientlibs/KalturaClient.xml";
    private static List<APIException> exceptions;

    // to get e.g. yesterday date in specific date format need call the method so: getOffsetDateInFormat(-1, pattern);
    public static String getOffsetDateInFormat(int offsetDay, String pattern) {
        DateFormat dateFormat = new SimpleDateFormat(pattern);
        Calendar cal = Calendar.getInstance();
        if (offsetDay != 0) {
            cal.add(Calendar.DATE, offsetDay);
        }
        return dateFormat.format(cal.getTime());
    }

    // Get Date time according to off set parameter provided (with the pattern: dd/MM/yyyy HH:mm:ss)
    public static String getTimeInDate(int offSetInMinutes) {
        DateFormat dateFormat = new SimpleDateFormat("dd/MM/yyyy HH:mm:ss");
        Date dNow = new Date();
        Calendar calendar = Calendar.getInstance();
        calendar.setTime(dNow);
        calendar.add(Calendar.MINUTE, offSetInMinutes);
        dNow = calendar.getTime();

        return dateFormat.format(dNow);
    }

    // Get epoch time in seconds according to off set parameter provided (in minutes)
    public static long getTimeInEpoch(int offSetInMinutes) {
        //DateFormat dateFormat = new SimpleDateFormat("dd/MM/yyyy HH:mm:ss");
        Date dNow = new Date();
        Calendar calendar = Calendar.getInstance();
        calendar.setTime(dNow);
        calendar.add(Calendar.MINUTE, offSetInMinutes);

        return calendar.getTimeInMillis() / 1000;
    }

    // generate current data String in specified format
    public static String getCurrentDataInFormat(String pattern) {
        return getOffsetDateInFormat(0, pattern);
    }

    // generate string containing prefix and random long suffix
    public static String getRandomValue(String prefix, long maxValue) {
        long randomLongValue = ThreadLocalRandom.current().nextLong(maxValue);
        return prefix + randomLongValue;
    }

    // generate random string
    public static String getRandomString() {
        String chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        StringBuilder sb = new StringBuilder();
        Random r = new Random();
        while (sb.length() < 12) {
            int index = (int) (r.nextFloat() * chars.length());
            sb.append(chars.charAt(index));
        }
        return sb.toString().toLowerCase();
    }

    // get api exception
    public static APIException getAPIExceptionFromList(int code) {
        getApiExceptionList();

        for (APIException exception : exceptions) {
            if (exception.getCode().equals(String.valueOf(code))) {
                return exception;
            }
        }

        Logger.getLogger(BaseUtils.class).error("No such error code in the API schema");
        return null;
    }

    // generate apiException list base on the api schema
    private static List<APIException> getApiExceptionList() {
        if (exceptions == null) {
            DocumentBuilderFactory dbf = DocumentBuilderFactory.newInstance();
            DocumentBuilder db = null;
            try {
                db = dbf.newDocumentBuilder();
            } catch (ParserConfigurationException e) {
                e.printStackTrace();
            }
            Document doc = null;
            try {
                doc = db.parse(new URL(API_SCHEMA_URL).openStream());
            } catch (SAXException e) {
                e.printStackTrace();
            } catch (IOException e) {
                e.printStackTrace();
            }
            NodeList nodeList = doc.getElementsByTagName("error");

            exceptions = new ArrayList<>();

            for (int i = 0; i < nodeList.getLength(); i++) {
                Element element = (Element) nodeList.item(i);
                APIException exception = new APIException(null, element.getAttribute("description"), element.getAttribute("code"));
                exceptions.add(exception);
            }
        }
        return exceptions;
    }

    // Get concatenated string
    public static String getConcatenatedString(String... args) {
        List<String> assetIds = new ArrayList<>();
        for (String arg : args) {
            assetIds.add(arg);
        }
        return String.join(",", assetIds);
    }
}
