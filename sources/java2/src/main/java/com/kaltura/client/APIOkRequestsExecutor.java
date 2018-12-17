package com.kaltura.client;


import com.kaltura.client.utils.ErrorElement;
import com.kaltura.client.utils.request.ConnectionConfiguration;
import com.kaltura.client.utils.request.ExecutedRequest;
import com.kaltura.client.utils.request.RequestElement;
import com.kaltura.client.utils.response.base.ResponseElement;

import java.io.IOException;
import java.io.InputStream;
import java.util.List;
import java.util.UUID;
import java.util.concurrent.TimeUnit;

import okhttp3.Call;
import okhttp3.Callback;
import okhttp3.ConnectionPool;
import okhttp3.Dispatcher;
import okhttp3.Headers;
import okhttp3.MediaType;
import okhttp3.MultipartBody;
import okhttp3.MultipartBody.Builder;
import okhttp3.OkHttpClient;
import okhttp3.Request;
import okhttp3.RequestBody;
import okhttp3.Response;
import okhttp3.internal.Util;
import okio.Buffer;
import okio.BufferedSink;
import okio.Okio;
import okio.Source;

/**
 * @hide
 */
public class APIOkRequestsExecutor implements RequestQueue {

    public static final String TAG = "APIOkRequestsExecutor";

    public interface IdFactory {
        String factorId(String factor);
    }

    private static class InputStreamRequestBody extends RequestBody {

        private InputStream inputStream;
        private MediaType mediaType;

        public InputStreamRequestBody(MediaType mediaType, InputStream inputStream) {
            this.mediaType = mediaType;
            this.inputStream = inputStream;
        }

        @Override
        public MediaType contentType() {
            return mediaType;
        }

        @Override
        public long contentLength() {
            try {
                return inputStream.available();
            } catch (IOException e) {
                return 0;
            }
        }

        @Override
        public void writeTo(BufferedSink sink) throws IOException {
            Source source = null;
            try {
                source = Okio.source(inputStream);
                sink.writeAll(source);
            } finally {
                Util.closeQuietly(source);
            }
        }
    }


    static final MediaType JSON_MediaType = MediaType.parse("application/json");

    private ConnectionConfiguration defaultConfiguration = new ConnectionConfiguration() {
        @Override
        public int getReadTimeout() {
            return 20000;
        }

        @Override
        public int getWriteTimeout() {
            return 20000;
        }

        @Override
        public int getConnectTimeout() {
            return 10000;
        }

		@Override
		public boolean getAcceptGzipEncoding() {
			return false;
		}

		@Override
		public int getMaxRetry(int defaultVal) {
			return defaultVal;
		}

		@Override
		public String getEndpoint() {
			return "https://www.kaltura.com";
		}

		@Override
		public int getTypeFormat() {
			return ServiceResponseTypeFormat.RESPONSE_TYPE_JSON.getValue();
		}
    };

    private IdFactory idFactory = new IdFactory() {
        @Override
        public String factorId(String factor) {
            return UUID.randomUUID().toString() + "::" + (factor != null ? factor : System.currentTimeMillis());
        }
    };
    

    private OkHttpClient mOkClient;
    private boolean enableLogs = true;
    private static ILogger logger = Logger.getLogger(TAG);

    protected static APIOkRequestsExecutor self;


    public static APIOkRequestsExecutor getExecutor() {
        if (self == null) {
            self = new APIOkRequestsExecutor();
        }
        return self;
    }

    public APIOkRequestsExecutor() {
        mOkClient = configClient(createOkClientBuilder(), defaultConfiguration).build();
    }

    public APIOkRequestsExecutor(ConnectionConfiguration defaultConfiguration) {
        setDefaultConfiguration(defaultConfiguration);
    }


    public APIOkRequestsExecutor setRequestIdFactory(IdFactory factory) {
        this.idFactory = factory;
        return this;
    }

    /**
     * in case of specific request configurations, pass newly built client based on mOkClient instance.
     *
     * @param configuration
     * @return
     */
    private OkHttpClient getOkClient(ConnectionConfiguration configuration) {

        if (configuration != null) {
            // returns specific client for configuration
            return configClient(mOkClient.newBuilder(), configuration).build();
        }
        //default configurable client instance
        return mOkClient;
    }

    private OkHttpClient.Builder createOkClientBuilder() {
        return new OkHttpClient.Builder().connectionPool(new ConnectionPool()); // default connection pool - holds 5 connections up to 5 minutes idle time
    }

    private OkHttpClient.Builder configClient(OkHttpClient.Builder builder, ConnectionConfiguration config) {
        builder.followRedirects(true).connectTimeout(config.getConnectTimeout(), TimeUnit.MILLISECONDS)
                .readTimeout(config.getReadTimeout(), TimeUnit.MILLISECONDS)
                .writeTimeout(config.getWriteTimeout(), TimeUnit.MILLISECONDS)
                .retryOnConnectionFailure(config.getMaxRetry(1) > 0);

        return builder;
    }

    @Override
    public void setDefaultConfiguration(ConnectionConfiguration defaultConfiguration) {
        this.defaultConfiguration = defaultConfiguration;
        mOkClient = configClient(createOkClientBuilder(), defaultConfiguration).build();
    }

    @Override
    public void enableLogs(boolean enable) {
        this.enableLogs = enable;
        if (enable) {
            logger = Logger.getLogger(TAG);
        } else {
            logger = new LoggerNull(TAG);
        }
    }

    @SuppressWarnings("rawtypes")
	@Override
    public String queue(final RequestElement requestElement) {
        final Request request = buildRestRequest(requestElement);
        return queue(request, requestElement);
    }

    @SuppressWarnings("rawtypes")
	private String queue(final Request request, final RequestElement action) {       
        try {
            Call call = getOkClient(action.config()).newCall(request);
            call.enqueue(new Callback() {
                @Override
                public void onFailure(Call call, IOException e) { //!! in case of request error on client side

                    if (call.isCanceled()) {
                        logger.warn("onFailure: call "+call.request().tag()+" was canceled. not passing results");
                        return;
                    }
                    // handle failures: create response from exception
                    //action.onComplete(new ExecutedRequest().error(e).success(false).handler(handler));
                    ExecutedRequest responseElement = new ExecutedRequest().error(e).success(false);
                    postCompletion(action, responseElement);
                }

                @Override
                public void onResponse(Call call, Response response) throws IOException {
                    if (call.isCanceled()) {
                        logger.warn("call "+call.request().tag()+" was canceled. not passing results");
                        return;
                    }

                    // pass parsed response to action completion block
                    postCompletion(action, onGotResponse(response, action));
                }
            });
            return (String) call.request().tag();

        } catch (Exception e) {
            e.printStackTrace();
            ExecutedRequest responseElement = new ExecutedRequest().response(getErrorResponse(e)).success(false);
            postCompletion(action, responseElement);

        }
        return null; // no call id to return.
    }

    @SuppressWarnings({ "rawtypes", "unchecked" })
	protected void postCompletion(final RequestElement action, ResponseElement responseElement) {

        final com.kaltura.client.utils.response.base.Response<?> apiResponse = action.parseResponse(responseElement);
        action.onComplete(apiResponse);
    }


    private String getErrorResponse(Exception e) {
        return e.getClass().getName() + ": " + e.getMessage();
    }

    @SuppressWarnings("rawtypes")
	@Override
    public com.kaltura.client.utils.response.base.Response<?> execute(RequestElement request) {
        try {
            Response response = getOkClient(request.config()).newCall(buildRestRequest(request)).execute();
            return request.parseResponse(onGotResponse(response, request));

        } catch (IOException e) {
            // failure on request execution - create error response
            ResponseElement responseElement = new ExecutedRequest().response(getErrorResponse(e)).success(false);
            return request.parseResponse(responseElement);
        }
    }

    //TODO: cancel check on executor + null check on provider

    //@Override
    public boolean hasRequest(String reqId) {
        Dispatcher dispatcher = getOkClient(null).dispatcher();

        Call call = findCall(reqId, dispatcher.queuedCalls());
        if (call != null) {
            return true;
        }
        call = findCall(reqId, dispatcher.runningCalls());
        return call != null;
    }

    @Override
    public void cancelRequest(String reqId) {
        Dispatcher dispatcher = getOkClient(null).dispatcher();

        Call call = findCall(reqId, dispatcher.queuedCalls());
        if (call != null) {
            call.cancel();
        }
        call = findCall(reqId, dispatcher.runningCalls());
        if (call != null) {
            call.cancel();
        }
    }

    private Call findCall(String reqId, List<Call> calls) {
        for (Call call : calls) {
            if (call.request().tag().equals(reqId)) {
                return call;
            }
        }
        return null;
    }

    @Override
    public void clearRequests() {
        if (mOkClient != null) {
            mOkClient.dispatcher().cancelAll();
        }
    }

    @Override
    public boolean isEmpty() {
        return mOkClient == null || mOkClient.dispatcher().queuedCallsCount() == 0;
    }

    @SuppressWarnings("rawtypes")
	protected ResponseElement onGotResponse(Response response, RequestElement action) {
        String requestId = getRequestId(response);

        if (!response.isSuccessful()) { // in case response has failure status
            return new ExecutedRequest().requestId(requestId).error(ErrorElement.fromCode(response.code(), response.message())).success(false);

        } else {

            String responseString = null;
            try {
                responseString = response.body().string();
            } catch (IOException e) {
                e.printStackTrace();
                logger.error("failed to retrieve the response body!");
            }

            if (enableLogs) {
            	logger.debug("response [" + requestId + "] body:\n" + responseString);
            }
            
            return new ExecutedRequest().requestId(requestId).response(responseString).code(response.code()).success(responseString != null);
        }
    }

    protected String getRequestId(Response response) {
        try {
            return response.request().tag().toString();
        } catch (NullPointerException e) {
            return "";
        }
    }

    private interface BodyBuilder {
        @SuppressWarnings("rawtypes")
		RequestBody build(RequestElement requestElement);

        BodyBuilder Default = new BodyBuilder() {
            @SuppressWarnings("rawtypes")
			@Override
            public RequestBody build(RequestElement requestElement) {
                return requestElement.getBody() != null ? RequestBody.create(JSON_MediaType, requestElement.getBody().getBytes()) : null;
            }
        };
    }

    @SuppressWarnings({ "rawtypes", "unchecked" })
	private Request buildRestRequest(RequestElement request) {

    	RequestBody body;
    	Files files = request.getFiles();
    	if(files == null) {
	        body = BodyBuilder.Default.build(request);
    	}
    	else {
    		Builder bodyBuilder = new MultipartBody.Builder()
	        .setType(MultipartBody.FORM)
	        .addFormDataPart("json", request.getBody());
    		
    		for(String fieldName : files.keySet()) {
    			FileHolder fileHolder = files.get(fieldName);
    			MediaType mediaType = MediaType.parse(fileHolder.getMimeType()); 
    			
    			if(fileHolder.getFile() != null) { 
    				bodyBuilder.addFormDataPart(fieldName, fileHolder.getName(), RequestBody.create(mediaType, fileHolder.getFile()));
    			}
    			else if(fileHolder.getInputStream() != null) {
    				bodyBuilder.addFormDataPart(fieldName, fileHolder.getName(), new InputStreamRequestBody(mediaType, fileHolder.getInputStream()));
    			}
    		}
    		body = bodyBuilder.build();
    	}
    	
        String url = request.getUrl();

        String requestId = idFactory.factorId(request.getTag());
        if (enableLogs) {
        	logger.debug("request [" + requestId + "] url: " + url + "\nbody:\n" + request.getBody() + "\n");
        }

        return new Request.Builder()
                .headers(Headers.of(request.getHeaders()))
                .method(request.getMethod(), body)
                .url(url)
                .tag(requestId)
                .build();
    }

    public static String getRequestBody(Request request) {
        try {
            final Request copy = request.newBuilder().build();
            final Buffer buffer = new Buffer();
            copy.body().writeTo(buffer);
            return buffer.readUtf8();
        } catch (final IOException e) {
            return "did not work";
        }
    }
}
