package com.kaltura.client.utils.request;

import com.kaltura.client.ConnectionConfiguration;
import com.kaltura.client.KalturaClient;
import com.kaltura.client.KalturaClientBase;
import com.kaltura.client.KalturaParams;
import com.kaltura.client.utils.EncryptionUtils;
import com.kaltura.client.utils.KalturaAPIConstants;
import com.kaltura.client.utils.response.OnCompletion;
import com.kaltura.client.utils.response.base.GeneralResponse;

import java.util.HashMap;

/**
 * Created by tehilarozin on 14/08/2016.
 */
public abstract class ActionBase implements OnCompletion<GeneralResponse> /*implements RequestElement<T>*/ {

    protected KalturaParams params;
    protected OnCompletion onCompletion;
    protected HashMap<String, String> headers;
    protected String url;
    protected String id;

    //boolean isMultiparts = false;

    public ActionBase() {
        params = new KalturaParams();
    }

    protected ActionBase(KalturaParams params, OnCompletion onCompletion) {
        this.params = params;
        this.onCompletion = onCompletion;
    }

    protected ActionBase(KalturaParams params) {
        this.params = params;
    }

    /**
     * builds the path to forwarded response properties value
     * @param id - id/number of the request in the multirequest, to which property value is binded
     * @param propertyPath - keys path to the binded response property
     * @return value pattern for the binded property (exp. [2, request number]:result:[user:name, property path]
     */
    public static String path(String id, String propertyPath) {
        return id + ":result:" + propertyPath.replace(".", ":");
    }


    //@Override
    public String getMethod() {
        return "POST";
    }

    public abstract String getUrlTail();

    //@Override
    public String getBody() {
        return params.toString();
    }

    public String getId() {
        return id;
    }


    /*public boolean isMultiparts() {
        return isMultiparts;
    }*/

    public abstract <T extends ActionBase> T setCompletion(OnCompletion onCompletion);

    /*public <T extends ActionBase> T setCompletion(OnCompletion onCompletion) {
        this.onCompletion = onCompletion;
        return getThis();
    }*/

    /*public <T extends ActionBase> T setMultiparts(boolean multiparts) {
        isMultiparts = multiparts;
        return getThis();
    }*/

	public KalturaParams getParams() {
        return params;
    }
	
    //@Override
    public void setParams(Object objParams) {
        params.putAll((KalturaParams) objParams); // !! null params should be checked - should not appear in request body or be presented as empty string.
	}
	
    /*protected <AB extends ActionBase> AB getThis(){
        return (AB)this;
    }*/

    public HashMap<String, String> getHeaders() {
        return headers;
    }

    public void setHeaders(HashMap<String, String> headers) {
        this.headers = headers;
    }

    public void setHeaders(String ... nameValueHeaders){
        for (int i = 0 ; i < nameValueHeaders.length-1 ; i+=2){
            this.headers.put(nameValueHeaders[i], nameValueHeaders[i+1]);
        }
    }

    private KalturaParams prepareParams(KalturaClientBase configurations, boolean addSignature) {

        if(params == null){
            params = new KalturaParams();
        }

        // add default params:
        //params.add("format", configurations.getConnectionConfiguration().getServiceResponseTypeFormat());
        params.add("ignoreNull", true);
        if(configurations != null) {
            params.putAll(configurations.getClientConfiguration());
            params.putAll(configurations.getRequestConfiguration());
        }
        if (addSignature) {
            params.add("kalsig", EncryptionUtils.encryptMD5(params.toString()));
        }
        return params;
    }

    private void prepareUrl(String endPoint) {
        if (url == null) {
            StringBuilder urlBuilder = new StringBuilder(endPoint)
                    .append(KalturaAPIConstants.UrlApiVersion);

            urlBuilder.append(getUrlTail());
            url = urlBuilder.toString();
        }
    }

    public RequestElement build(final KalturaClient client) {
        return build(client, false);
    }

    public RequestElement build(final KalturaClient client, boolean addSignature) {
        prepareParams(client, addSignature);
        prepareHeaders(client);
        prepareUrl(client != null ? client.getConnectionConfiguration().getEndpoint() : "");

        return new RequestElement() {
            @Override
            public String getContentType() {
                return headers != null ? headers.get(KalturaAPIConstants.HeaderContentType) : KalturaAPIConstants.DefaultContentType;
            }

            @Override
            public String getMethod() {
                return "POST";
            }

            @Override
            public String getUrl() {
                return url;
            }

            @Override
            public String getBody() {
                return ActionBase.this.getBody();
            }

            @Override
            public HashMap<String, String> getHeaders() {
                return headers;
            }

            @Override
            public String getId() {
                return id;
            }

            @Override
            public ConnectionConfiguration config() {
                return client != null ? client.getConnectionConfiguration() : ConnectionConfiguration.getDefaults();
            }

            @Override
            public String tag() {
                return getTag();
            }

            @Override
            public void onComplete(GeneralResponse response) {
                ActionBase.this.onComplete(response);
            }
        };

    }

    private void prepareHeaders(KalturaClient client) {
        if (headers == null) {
            headers = new HashMap<String, String>();
        }
        addDefaultHeaders();

        if (!headers.containsKey(KalturaAPIConstants.HeaderAcceptEncoding) && client.getConnectionConfiguration().getAcceptGzipEncoding()) {
            //headers.put(KalturaAPIConstants.HeaderAcceptEncoding, KalturaAPIConstants.HeaderEncodingGzip);
        }
    }

    private void addDefaultHeaders() {
        if(!headers.containsKey(KalturaAPIConstants.HeaderAccept)) {
            headers.put(KalturaAPIConstants.HeaderAccept, "application/json");
        }
        if(!headers.containsKey(KalturaAPIConstants.HeaderAcceptCharset)) {
            headers.put(KalturaAPIConstants.HeaderAcceptCharset, "utf-8,ISO-8859-1;q=0.7,*;q=0.5");
        }
    }

    protected abstract String getTag();

}














