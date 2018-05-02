package com.kaltura.client.test.utils;

import com.kaltura.client.enums.AssetType;
import com.kaltura.client.enums.BookmarkActionType;
import com.kaltura.client.enums.BookmarkOrderBy;
import com.kaltura.client.types.Bookmark;
import com.kaltura.client.types.BookmarkFilter;
import com.kaltura.client.types.BookmarkPlayerData;

import java.util.List;

public class BookmarkUtils extends BaseUtils {

    public static Bookmark addBookmark(int position, String assetId, int fileId, AssetType assetType,  BookmarkActionType actionType) {

        // instantiate Bookmark object
        Bookmark bookmark = new Bookmark();
        bookmark.setPosition(position);
        bookmark.setId(String.valueOf(assetId));
        bookmark.setType(assetType);

        // instantiate BookmarkPlayerData object
        BookmarkPlayerData playerData = new BookmarkPlayerData();
        BookmarkActionType bookmarkactionType = BookmarkActionType.get(actionType.getValue());
        playerData.setAction(bookmarkactionType);
        playerData.setAverageBitrate(0);
        playerData.setTotalBitrate(0);
        playerData.setCurrentBitrate(0);
        playerData.setFileId((long) fileId);
        bookmark.setPlayerData(playerData);

       return  bookmark;
    }

    public static  BookmarkFilter listBookmark(BookmarkOrderBy orderBy, AssetType assetType, List<String> assetIds) {
        // instantiate BookmarkFilter object
        BookmarkFilter bookmarkFilter = new BookmarkFilter();
        bookmarkFilter.setOrderBy(orderBy.getValue());
        bookmarkFilter.setAssetTypeEqual(assetType);
        String concatenatedAssetIds = String.join(",",assetIds);
        bookmarkFilter.setAssetIdIn(concatenatedAssetIds);

        return bookmarkFilter;
    }


}
