package com.kaltura.client.utils.request;

import com.kaltura.client.KalturaParams;
import com.kaltura.client.utils.response.OnCompletion;

/**
 * Created by tehilarozin on 14/08/2016.
 */
public abstract class ActionBase<T> implements ActionElement<T> {

    KalturaParams params;
    OnCompletion onCompletion;
    boolean isMultiparts = false;

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


    @Override
    public String getMethod() {
        return "POST";
    }

    public abstract String getUrlTail();

    @Override
    public String getBody() {
        return params.toString();
    }

    public boolean isMultiparts() {
        return isMultiparts;
    }

    public <T extends ActionBase> T setCompletion(OnCompletion onCompletion) {
        this.onCompletion = onCompletion;
        return getThis();
    }

    public <T extends ActionBase> T setMultiparts(boolean multiparts) {
        isMultiparts = multiparts;
        return getThis();
    }

	public KalturaParams getParams() {
        return params;
    }
	
    @Override
    public void setParams(Object objParams) {
        params.add((KalturaParams) objParams); // !! null params should be checked - should not appear in request body or be presented as empty string.
	}
	
    protected <AB extends ActionBase> AB getThis(){
        return (AB)this;
    }
}














