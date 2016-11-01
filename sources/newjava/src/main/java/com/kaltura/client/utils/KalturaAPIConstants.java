package com.kaltura.client.utils;

/**
 * Created by tehilarozin on 28/07/2016.
 */
public class KalturaAPIConstants {

    public static final String UrlApiVersion = "api_v3/";

    public static final String DefaultContentType = "application/json";

    //generated client config properties
    /*public static final String ConfigClientClientTag = "clientTag";
    public static final String ConfigClientTagValue = "java:16-08-08";
    public static final String ConfigClientAPIVersion = "apiVersion";
    public static final String ConfigClientAPIVersionValue = "3.6.287.27685";
*/
    //kaltura client config properties
    //public static boolean RetryOnConnectionFailure = true;

    //generated Request config properties:
    public static final String ConfigRequestPartnerId = "partnerId";
    public static final String ConfigRequestKs = "ks";


    public static final String HeaderAccept = "Accept";
    public static final String HeaderAcceptCharset = "Accept-Charset";
    public static final String HeaderAcceptEncoding = "Accept-Encoding";
    public static final String HeaderContentEncoding = "Content-Encoding";
    public static final String HeaderContentType = "Content-Type";
    public static final String HeaderEncodingGzip = "gzip";


    //response properties names:
    public static final String PropertyExecutionTime = "executionTime";
    public static final String PropertyResult = "result";
    public static final String ResultOk = "true";


    public static class Codes{
        public final static int EMPTY = -1;

        public final static int USER_NOT_FOUND = 2000;


    }


}
