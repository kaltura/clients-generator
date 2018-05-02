package com.kaltura.client.test.utils;


import com.kaltura.client.enums.AssetCommentOrderBy;
import com.kaltura.client.enums.AssetType;
import com.kaltura.client.types.AssetComment;
import com.kaltura.client.types.AssetCommentFilter;

import javax.annotation.Nullable;

public class AssetCommentUtils extends BaseUtils {

    public static AssetComment assetComment(int assetId, AssetType assetType, @Nullable String writer, @Nullable String text,
                                            @Nullable Long createDate, @Nullable String subHeader, @Nullable String header) {
        AssetComment assetComment = new AssetComment();
        assetComment.setAssetId(assetId);
        assetComment.setAssetType(assetType);
        assetComment.setWriter(writer);
        assetComment.setText(text);
        assetComment.setCreateDate(createDate);
        assetComment.setSubHeader(subHeader);
        assetComment.setHeader(header);

        return assetComment;
    }

    public static AssetCommentFilter assetCommentFilter(int assetId, AssetType assetType, AssetCommentOrderBy orderBy) {
        AssetCommentFilter assetCommentFilter = new AssetCommentFilter();
        assetCommentFilter.setAssetIdEqual(assetId);
        assetCommentFilter.setAssetTypeEqual(assetType);
        assetCommentFilter.setOrderBy(orderBy.toString());

        return assetCommentFilter;
    }
}
