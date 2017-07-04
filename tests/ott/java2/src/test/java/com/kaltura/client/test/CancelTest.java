package com.kaltura.client.test;

import java.util.concurrent.ExecutionException;

import org.junit.Test;

import com.kaltura.client.APIOkRequestsExecutor;
import com.kaltura.client.services.FavoriteService;
import com.kaltura.client.types.Favorite;
import com.kaltura.client.types.ListResponse;
import com.kaltura.client.utils.request.RequestBuilder;

/**
 * Created by tehilarozin on 11/09/2016.
 */
public class CancelTest extends TestCommon {
    @Test
    public void testCancel() throws InterruptedException, ExecutionException {
        logger.info("testCancelRequest");

        RequestBuilder<ListResponse<Favorite>> requestBuilder = FavoriteService.list();
        APIOkRequestsExecutor actionsQueue = APIOkRequestsExecutor.getSingleton();
        String reqId = actionsQueue.queue(requestBuilder.build(client));
        actionsQueue.cancelRequest(reqId);
    }
}
