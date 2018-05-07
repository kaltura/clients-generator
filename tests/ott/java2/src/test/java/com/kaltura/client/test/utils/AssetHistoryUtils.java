package com.kaltura.client.test.utils;

import com.kaltura.client.Client;
import com.kaltura.client.enums.AssetType;
import com.kaltura.client.enums.BookmarkActionType;
import com.kaltura.client.enums.WatchStatus;
import com.kaltura.client.test.servicesImpl.BookmarkServiceImpl;
import com.kaltura.client.types.AssetHistoryFilter;
import com.kaltura.client.types.Bookmark;
import com.kaltura.client.types.MediaAsset;

import javax.annotation.Nullable;
import java.util.Optional;

import static com.kaltura.client.test.tests.BaseTest.getClient;

public class AssetHistoryUtils extends BaseUtils {

    public static AssetHistoryFilter getAssetHistoryFilter(@Nullable String assetIdIn, @Nullable Integer days, WatchStatus statusEqual, @Nullable String typeIn) {
        AssetHistoryFilter assetHistoryFilter = new AssetHistoryFilter();
        assetHistoryFilter.setAssetIdIn(assetIdIn);
        assetHistoryFilter.setDaysLessThanOrEqual(days);
        assetHistoryFilter.setStatusEqual(statusEqual);
        assetHistoryFilter.setTypeIn(typeIn);

        return assetHistoryFilter;

    }

    // Ingest asset, bookmark it and return the asset id
    public static Long ingestAssetAndPerformBookmark(Client client, String mediaType, int position, BookmarkActionType bookmarkActionType) {
        // Ingest asset
        MediaAsset mediaAsset = IngestUtils.ingestVOD(Optional.empty(), Optional.empty(), true, Optional.empty(), Optional.empty(), Optional.empty(), Optional.empty(),
                Optional.empty(), Optional.empty(), Optional.empty(), Optional.of(String.valueOf(mediaType)), Optional.empty(), Optional.empty());
        Long assetId = mediaAsset.getId();
        int fileId = AssetUtils.getAssetFileIds(String.valueOf(assetId)).get(0);
        // Movie asset bookmark
        AssetType assetType = AssetType.MEDIA;
        Bookmark bookmark = BookmarkUtils.addBookmark(position, String.valueOf(assetId), fileId, assetType, bookmarkActionType);
        //bookmark/action/add - Movie asset
        BookmarkServiceImpl.add(client, bookmark);

        return assetId;
    }
}
