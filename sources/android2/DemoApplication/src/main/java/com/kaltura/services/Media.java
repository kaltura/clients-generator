package com.kaltura.services;

import java.io.File;
import java.util.List;

import android.util.Log;

import com.kaltura.client.Client;
import com.kaltura.client.enums.EntryType;
import com.kaltura.client.enums.MediaType;
import com.kaltura.client.services.BaseEntryService;
import com.kaltura.client.services.MediaService;
import com.kaltura.client.types.APIException;
import com.kaltura.client.types.BaseEntry;
import com.kaltura.client.types.FilterPager;
import com.kaltura.client.types.MediaEntry;
import com.kaltura.client.types.MediaEntryFilter;
import com.kaltura.client.types.ListResponse;
import com.kaltura.client.utils.request.RequestBuilder;
import com.kaltura.client.utils.response.OnCompletion;
import com.kaltura.utils.ApiHelper;

/**
 * Media service lets you upload and manage media files (images / videos &
 * audio)
 */
public class Media {

    /**
     * Get a list of all media data from the kaltura server
     *
     * @param TAG constant in your class
     * @param mediaType Type of entries
     * @param pageSize The number of objects to retrieve. (Default is 30,
     * maximum page size is 500)
     *
     * @throws APIException
     */
    public static void listAllEntriesByIdCategories(final String TAG, MediaEntryFilter filter, int pageIndex, int pageSize, final OnCompletion<List<MediaEntry>> onCompletion) {
        // create a new pager to choose how many and which entries should be recieved
        // out of the filtered entries - not mandatory
        FilterPager pager = new FilterPager();
        pager.setPageIndex(pageIndex);
        pager.setPageSize(pageSize);

        // execute the list action of the mediaService object to recieve the list of entries
        RequestBuilder<ListResponse<MediaEntry>> requestBuilder = MediaService.list(filter, pager)
        .setCompletion(new OnCompletion<ListResponse<MediaEntry>>() {
            @Override
            public void onComplete(ListResponse<MediaEntry> listResponse, APIException error) {
                if(error == null && listResponse != null) {
                    // loop through all entries in the reponse list and print their id.
                    Log.w(TAG, "Entries list :");
                    int i = 0;
                    for (MediaEntry entry : listResponse.getObjects()) {
                        Log.w(TAG, ++i + " id:" + entry.getId() + " name:" + entry.getName() + " type:" + entry.getType() + " dataURL: " + entry.getDataUrl());
                    }
                }
                onCompletion.onComplete(listResponse.getObjects(), error);
            }
        });
        ApiHelper.execute(requestBuilder);
    }

    /**
     * Get media entry by ID
     *
     * @param TAG constant in your class
     * @param entryId Media entry id
     *
     * @return Information about the entry
     *
     * @throws APIException
     */
    public static void getEntrybyId(final String TAG, String entryId, final OnCompletion<MediaEntry> onCompletion) {
        RequestBuilder<MediaEntry> requestBuilder = MediaService.get(entryId)
        .setCompletion(new OnCompletion<MediaEntry>() {
            @Override
            public void onComplete(MediaEntry entry, APIException error) {
                Log.w(TAG, "Entry:");
                Log.w(TAG, " id:" + entry.getId() + " name:" + entry.getName() + " type:" + entry.getType() + " categories: " + entry.getCategories());
                onCompletion.onComplete(entry, error);
            }
        });
        ApiHelper.execute(requestBuilder);
    }

    /**
     * Creates an empty media entry and assigns basic metadata to it.
     *
     * @param TAG constant in your class
     * @param category Category name which belongs to an entry
     * @param name Name of an entry
     * @param description Description of an entry
     * @param tag Tag of an entry
     *
     * @return Information about created the entry
     *
     *
     */
    public static void addEmptyEntry(final String TAG, String category, String name, String description, String tag, final OnCompletion<MediaEntry> onCompletion) {

        Log.w(TAG, "\nCreating an empty  Entry (without actual media binary attached)...");

        MediaEntry entry = new MediaEntry();
        entry.setMediaType(MediaType.VIDEO);
        entry.setCategories(category);
        entry.setName(name);
        entry.setDescription(description);
        entry.setTags(tag);

        RequestBuilder<MediaEntry> requestBuilder = MediaService.add(entry)
        .setCompletion(new OnCompletion<MediaEntry>() {
            @Override
            public void onComplete(MediaEntry newEntry, APIException e) {
            if(e != null) {
                e.printStackTrace();
                Log.w(TAG, "err: " + e.getMessage());
            }
            else if(newEntry != null) {
                Log.w(TAG, "\nThe id of our new Video Entry is: " + newEntry.getId());
            }
            onCompletion.onComplete(newEntry, e);
            }
        });
        ApiHelper.execute(requestBuilder);
    }
}
