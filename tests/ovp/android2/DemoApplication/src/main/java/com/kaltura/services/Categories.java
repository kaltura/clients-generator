package com.kaltura.services;

import java.util.List;

import android.util.Log;

import com.kaltura.client.Client;
import com.kaltura.client.services.CategoryService;
import com.kaltura.client.types.APIException;
import com.kaltura.client.types.Category;
import com.kaltura.client.types.CategoryFilter;
import com.kaltura.client.types.ListResponse;
import com.kaltura.client.types.FilterPager;
import com.kaltura.client.utils.request.RequestBuilder;
import com.kaltura.client.utils.response.OnCompletion;
import com.kaltura.client.utils.response.base.Response;
import com.kaltura.utils.ApiHelper;

/**
 * Add & Manage Categories *
 */
public class Categories {

    /**
     * Get a list of all categories on the kaltura server
     *
     * @param TAG constant in your class
     * @param pageindex The page number for which {pageSize} of objects should
     * be retrieved (Default is 1)
     * @param pageSize The number of objects to retrieve. (Default is 30,
     * maximum page size is 500)
     *
     * @return The list of all categories
     *
     * @throws APIException
     */
    public static void listAllCategories(final String TAG, int pageIndex, int pageSize, final OnCompletion<Response<ListResponse<Category>>> onCompletion) throws APIException {
        // create a new filter to filter entries - not mandatory
        CategoryFilter filter = new CategoryFilter();
        //filter.mediaTypeEqual = mediaType;

        // create a new pager to choose how many and which entries should be recieved
        // out of the filtered entries - not mandatory
        FilterPager pager = new FilterPager();
        pager.setPageIndex(pageIndex);
        pager.setPageSize(pageSize);

        // execute the list action of the mediaService object to recieve the list of entries
        RequestBuilder<ListResponse<Category>> requestBuilder = CategoryService.list(filter)
        .setCompletion(new OnCompletion<Response<ListResponse<Category>>>() {
            @Override
            public void onComplete(Response<ListResponse<Category>> response) {

                // loop through all entries in the reponse list and print their id.
                Log.w(TAG, "Entries list :");
                int i = 0;
                for (Category entry : response.results.getObjects()) {
                    Log.w(TAG, ++i + " id:" + entry.getId() + " name:" + entry.getName() + " depth: " + entry.getDepth() + " fullName: " + entry.getFullName());
                }

                onCompletion.onComplete(response);
            }
        });
        ApiHelper.execute(requestBuilder);
    }
}
