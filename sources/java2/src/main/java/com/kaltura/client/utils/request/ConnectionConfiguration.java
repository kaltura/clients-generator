package com.kaltura.client.utils.request;

/**
 * Created by tehilarozin on 30/10/2016.
 */
public interface ConnectionConfiguration {
    int getConnectTimeout();

    int getReadTimeout();

    int getWriteTimeout();

    boolean getAcceptGzipEncoding();

    int getMaxRetry(int defaultVal);

    String getEndpoint();

    int getTypeFormat(); //kalturaServiceResponseTypeFormat
}
