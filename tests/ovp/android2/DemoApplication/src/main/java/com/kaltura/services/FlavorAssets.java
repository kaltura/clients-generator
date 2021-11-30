/*
 * To change this template, choose Tools | Templates
 * and open the template in the editor.
 */
package com.kaltura.services;

import java.util.List;

import android.util.Log;

import com.kaltura.client.Client;
import com.kaltura.client.services.BaseEntryService;
import com.kaltura.client.services.FlavorAssetService;
import com.kaltura.client.types.APIException;
import com.kaltura.client.types.EntryContextDataParams;
import com.kaltura.client.types.EntryContextDataResult;
import com.kaltura.client.types.FilterPager;
import com.kaltura.client.types.FlavorAsset;
import com.kaltura.client.types.FlavorAssetFilter;
import com.kaltura.client.types.ListResponse;
import com.kaltura.client.utils.request.RequestBuilder;
import com.kaltura.client.utils.response.OnCompletion;
import com.kaltura.client.utils.response.base.Response;
import com.kaltura.utils.ApiHelper;

/**
 * Retrieve information and invoke actions on Flavor Asset
 */
public class FlavorAssets {

    
    /**
     * Return flavorAsset lists from getContextData call
     * @param TAG
     * @param entryId
     * @param flavorTags
     * @return
     * @throws APIException
     */
    public static void listAllFlavorsFromContext(String TAG, String entryId, String flavorTags, OnCompletion<Response<EntryContextDataResult>> onCompletion) throws APIException {
        EntryContextDataParams params = new EntryContextDataParams();
        params.setFlavorTags(flavorTags);
        BaseEntryService.GetContextDataBaseEntryBuilder requestBuilder = BaseEntryService.getContextData(entryId, params)
        .setCompletion(onCompletion);
    }
}
