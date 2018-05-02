package com.kaltura.client.test.utils;

import com.kaltura.client.enums.AssetOrderBy;
import com.kaltura.client.types.Channel;
import com.kaltura.client.types.IntegerValue;
import com.kaltura.client.types.MediaImage;

import javax.annotation.Nullable;
import java.util.List;

public class ChannelUtils extends BaseUtils {

    public static Channel addChannel(String name, @Nullable String description, @Nullable Boolean isActive, @Nullable String filterExpression,
                                     @Nullable AssetOrderBy orderBy, @Nullable List<IntegerValue> assetTypes, @Nullable List<MediaImage> images) {
        Channel channel = new Channel();
        channel.setName(name);
        channel.setDescription(description);
        channel.setIsActive(isActive);
        channel.setFilterExpression(filterExpression);
        channel.setOrder(orderBy);
        channel.setAssetTypes(assetTypes);
        channel.setImages(images);

        return channel;
    }
}
