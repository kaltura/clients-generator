import com.kaltura.client.APIOkRequestsExecutor;
import com.kaltura.client.services.FavoriteService;
import com.kaltura.client.types.Favorite;
import com.kaltura.client.types.ListResponse;
import com.kaltura.client.utils.response.OnCompletion;
import com.kaltura.client.utils.response.base.Response;

import org.awaitility.Awaitility;
import org.awaitility.core.ConditionTimeoutException;
import org.junit.Assert;
import org.junit.Test;

import java.util.concurrent.ExecutionException;
import java.util.concurrent.TimeUnit;
import java.util.concurrent.atomic.AtomicBoolean;

public class CancelTest extends TestCommon {

    AtomicBoolean beenHere = new AtomicBoolean(false);

    @Test(timeout = 3000, expected = ConditionTimeoutException.class)
    public void testCancel() throws InterruptedException, ExecutionException {
        logger.info("testCancelRequest");

        FavoriteService.ListAction requestBuilder = FavoriteService.list();
        APIOkRequestsExecutor actionsQueue = APIOkRequestsExecutor.getExecutor();
        String reqId = actionsQueue.queue(requestBuilder.setCompletion(new OnCompletion<Response<ListResponse<Favorite>>>() {
            @Override
            public void onComplete(Response<ListResponse<Favorite>> result) {
                beenHere.set(true);
            }
        }).build(client));

        actionsQueue.cancelRequest(reqId);

        Awaitility.await().timeout(2, TimeUnit.SECONDS);

        Assert.assertFalse(beenHere.get());
    }
}
