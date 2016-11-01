package com.kaltura.client.utils.deprecated;

import com.kaltura.client.utils.core.response.OnCompletion;

import java.util.concurrent.Callable;
import java.util.concurrent.ExecutionException;
import java.util.concurrent.FutureTask;

/**
 * Created by tehilarozin on 21/07/2016.
 */
public class RequestFutureTask<T> extends FutureTask {

    OnCompletion<T> onCompletion;

    public RequestFutureTask(Callable callable) {
        super(callable);
    }

    public RequestFutureTask(Callable callable, OnCompletion onCompletion) {
        super(callable);

        this.onCompletion = onCompletion;
    }

    @Override
    protected void done() {
        if(onCompletion != null){
            try {
                onCompletion.onComplete((T) get());
            } catch (InterruptedException e) {
                e.printStackTrace();
            } catch (ExecutionException e) {
                e.printStackTrace();
            }
        }
    }
}
