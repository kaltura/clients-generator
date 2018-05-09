package com.kaltura.client.test.utils;

import com.kaltura.client.enums.AssetReferenceType;
import com.kaltura.client.services.AssetService;
import com.kaltura.client.types.*;
import com.kaltura.client.utils.response.base.Response;

import javax.annotation.Nullable;
import java.util.ArrayList;
import java.util.List;

import static com.kaltura.client.services.AssetService.*;
import static com.kaltura.client.test.tests.BaseTest.SharedHousehold.getSharedMasterUserKs;
import static com.kaltura.client.test.tests.BaseTest.executor;

public class AssetUtils extends BaseUtils {


    public static SearchAssetFilter getSearchAssetFilter(@Nullable String ksql, @Nullable String idIn, @Nullable String typeIn,
                                                         @Nullable DynamicOrderBy dynamicOrderBy, List<AssetGroupBy> groupBy, String name, String orderBy) {
        SearchAssetFilter searchAssetFilter = new SearchAssetFilter();
        searchAssetFilter.setKSql(ksql);
        searchAssetFilter.setIdIn(idIn);
        searchAssetFilter.setTypeIn(typeIn);
        searchAssetFilter.setDynamicOrderBy(dynamicOrderBy);
        searchAssetFilter.setGroupBy(groupBy);
        searchAssetFilter.setName(name);
        searchAssetFilter.setOrderBy(orderBy);

        return searchAssetFilter;
    }

    public static ChannelFilter getChannelFilter(int idEqual, @Nullable String ksql, @Nullable DynamicOrderBy dynamicOrderBy, @Nullable String orderBy) {
        ChannelFilter channelFilter = new ChannelFilter();
        channelFilter.setIdEqual(idEqual);
        channelFilter.setKSql(ksql);
        channelFilter.setDynamicOrderBy(dynamicOrderBy);
        channelFilter.setOrderBy(orderBy);

        return channelFilter;
    }

    public static List<Integer> getAssetFileIds(String assetId) {

        AssetReferenceType assetReferenceType = AssetReferenceType.get(AssetReferenceType.MEDIA.getValue());

        GetAssetBuilder getAssetBuilder = AssetService.get(assetId,assetReferenceType);
        getAssetBuilder.setKs(getSharedMasterUserKs());
        Response<Asset> assetResponse = executor.executeSync(getAssetBuilder);

        List<MediaFile> mediafiles = assetResponse.results.getMediaFiles();

        List<Integer> fileIdsList = new ArrayList<>();
        for (MediaFile mediaFile : mediafiles) {
            fileIdsList.add(mediaFile.getId());
        }

        return fileIdsList;
    }
}

