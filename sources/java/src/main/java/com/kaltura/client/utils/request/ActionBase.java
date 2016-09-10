package com.kaltura.client.utils.request;

import com.kaltura.client.ConnectionConfiguration;
import com.kaltura.client.KalturaClient;
import com.kaltura.client.KalturaClientBase;
import com.kaltura.client.KalturaParams;
import com.kaltura.client.utils.EncryptionUtils;
import com.kaltura.client.utils.KalturaAPIConstants;
import com.kaltura.client.utils.response.OnCompletion;
import com.kaltura.client.utils.response.base.GeneralResponse;
import com.kaltura.client.utils.response.base.ResponseElement;
import com.kaltura.client.utils.response.base.ResponseParser;

import java.util.HashMap;

/**
 * Created by tehilarozin on 14/08/2016.
 */
public abstract class ActionBase implements OnCompletion<GeneralResponse>, RequestElement {

    protected String id;
    protected String url;
    protected KalturaParams params;
    protected HashMap<String, String> headers;
    private ConnectionConfiguration connectionConfig;

    /**
     * callback for the parsed response.
     */
    protected OnCompletion<GeneralResponse> onCompletion;



    protected ActionBase() {
        params = new KalturaParams();
    }

    protected ActionBase(KalturaParams params) {
        this.params = params;
    }


    public abstract String getUrlTail();

    public abstract String getTag();


    @Override
    public String getMethod() {
        return "POST";
    }

    @Override
    public String getBody() {
        return params.toString();
    }

    @Override
    public String getId() {
        return id;
    }

	public KalturaParams getParams() {
        return params;
    }
	
    public void setParams(Object objParams) {
        params.putAll((KalturaParams) objParams); // !! null params should be checked - should not appear in request body or be presented as empty string.
	}

    @Override
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

    @Override
    public String getContentType() {
        return headers != null ? headers.get(KalturaAPIConstants.HeaderContentType) : KalturaAPIConstants.DefaultContentType;
    }

    @Override
    public String getUrl() {
        return url;
    }

    @Override
    public ConnectionConfiguration config() {
        return connectionConfig;
    }

    /**
     * Builds the final list of parameters including the default params and the configured params.
     *
     * @param configurations
     * @param addSignature
     * @return
     */
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

    @Override
    public void onComplete(ResponseElement response) {
        ResponseParser.parse(this, response, this);
    }

    @Override
    public void onComplete(GeneralResponse response) {
        if (onCompletion != null) {
            onCompletion.onComplete(response);
        }
    }

    public RequestElement build(final KalturaClient client, boolean addSignature) {
        connectionConfig = client != null ? client.getConnectionConfiguration() : ConnectionConfiguration.getDefaults();

        prepareParams(client, addSignature);
        prepareHeaders(connectionConfig);
        prepareUrl(connectionConfig.getEndpoint());

        return this;
    }

    private void prepareHeaders(ConnectionConfiguration config) {
        if (headers == null) {
            headers = new HashMap<String, String>();
        }
        addDefaultHeaders();

        if (!headers.containsKey(KalturaAPIConstants.HeaderAcceptEncoding) && config.getAcceptGzipEncoding()) {
            headers.put(KalturaAPIConstants.HeaderAcceptEncoding, KalturaAPIConstants.HeaderEncodingGzip);
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


}














