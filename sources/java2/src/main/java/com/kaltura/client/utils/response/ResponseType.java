package com.kaltura.client.utils.response;

/**
 * Created by tehilarozin on 24/07/2016.
 *
 * All optional types that can be parsed from json, should implement this empty interface for parsing from json.
 */
public interface ResponseType {

    // can be actual result dynamic object or error
    String toString();
}
