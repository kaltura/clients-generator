package com.kaltura.client.test.tests.servicesTests.channelTests;

import com.kaltura.client.Client;
import com.kaltura.client.enums.AssetOrderBy;
import com.kaltura.client.services.ChannelService;
import com.kaltura.client.test.tests.BaseTest;
import com.kaltura.client.test.utils.ChannelUtils;
import com.kaltura.client.types.Channel;
import com.kaltura.client.utils.response.base.Response;
import io.qameta.allure.Description;
import org.testng.annotations.BeforeClass;
import org.testng.annotations.Test;

import static com.kaltura.client.services.ChannelService.*;
import static com.kaltura.client.test.utils.BaseUtils.getAPIExceptionFromList;
import static org.assertj.core.api.Assertions.assertThat;

public class ChannelDeleteTests extends BaseTest {

    private Channel channel = new Channel();
    private String channelName;
    private String Description;
    private Boolean isActive;
    private String filterExpression;
    private int channelId;


    @BeforeClass
    private void get_tests_before_class() {
        channelName = "Channel_12345";
        Description = "description of channel";
        isActive = true;
        filterExpression = "name ~ 'movie'";
    }

    @Description("channel/action/delete")
    @Test
    private void DeleteChannel() {

        channel = ChannelUtils.addChannel(channelName, Description, isActive, filterExpression, AssetOrderBy.LIKES_DESC, null, null);

        // channel/action/add
        // channel/action/add

        ChannelService.AddChannelBuilder addChannelBuilder = ChannelService.add(channel);
        addChannelBuilder.setKs(getManagerKs());
        Response<Channel> channelResponse = executor.executeSync(addChannelBuilder);

        channelId = Math.toIntExact(channelResponse.results.getId());

        // channel/action/delete

        ChannelService.DeleteChannelBuilder deleteChannelBuilder = ChannelService.delete(channelId);
        deleteChannelBuilder.setKs(getManagerKs());
        Response<Boolean> deleteResponse = executor.executeSync(deleteChannelBuilder);

        assertThat(deleteResponse.results.booleanValue()).isTrue();

        // channel/action/get - verify channel wasn't found

        GetChannelBuilder getChannelBuilder = ChannelService.get(channelId);
        getChannelBuilder.setKs(getManagerKs());
        Response<Channel> getResponse = executor.executeSync(getChannelBuilder);

        assertThat(getResponse.error.getCode()).isEqualTo(getAPIExceptionFromList(500007).getCode());
    }
}
