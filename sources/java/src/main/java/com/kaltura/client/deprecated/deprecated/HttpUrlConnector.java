package com.kaltura.client.utils.deprecated;

import com.kaltura.client.types.KalturaAPIException;
import com.kaltura.client.utils.request.ActionElement;
import connection.ConnectionConfig;

import java.io.*;
import java.net.*;

import static responses.base.KalturaApiException.ExceptionSource;

/**
 * Created by tehilarozin on 08/08/2016.
 */
public class HttpUrlConnector {

    private HttpURLConnection mUrlConnection;
    private ActionElement mAction;

    public HttpUrlConnector(ActionElement request){
        this.mAction = request;
        configureConnection();

    }

    private void configureConnection() throws KalturaAPIException {
        if(mAction == null){
            return;
        }

        try {
            URL obj = new URL(mAction.getUrl());//getParams()


        mUrlConnection = (HttpURLConnection) obj.openConnection();
        mUrlConnection.setConnectTimeout(ConnectionConfig.CONNECTION_TIMEOUT); // timeout for connection establishing
        mUrlConnection.setReadTimeout(ConnectionConfig.CONNECTION_TIMEOUT); // timeout for available data to read from connection
        mUrlConnection.setRequestMethod(mAction.getMethod());
        mUrlConnection.setDoOutput(true);

        setConnectionProperties(mAction.getParams().length());
        } catch (MalformedURLException maex) {
            throw new KalturaAPIException(KalturaAPIException.FailureStep.OnConfigure, "Failed request execution, invalid url");
        } catch (Exception ex){
            throw new KalturaAPIException(KalturaAPIException.FailureStep.OnConfigure, "Failed establishing configured connection to destination");
        }

    }

    /*disconnect to permanently close socket
    * close - to close current connection but socket is cached and stays open
    *
    * You also have to close error stream if the HTTP request fails (anything but 200):
http://scotte.github.io/2015/01/httpurlconnection-socket-leak/
try {
  ...
}
catch (IOException e) {
  connection.getErrorStream().close();
}*/

    private void setConnectionProperties(int bodyLength) {
        mUrlConnection.setRequestProperty("User-Agent", ConnectionConfig.USER_AGENT_HEADER);
        mUrlConnection.setRequestProperty("Content-Type", ConnectionConfig.CONTENT_TYPE_JSON_HEADER);
        mUrlConnection.setRequestProperty("Content-Length", String.valueOf(bodyLength));

        // USE this if we need to terminate connection on every request execution ending: System.setProperty("http.keepAlive","false");
    }

    private boolean isRequestStatusOk(int statusCode){
        return statusCode == HttpURLConnection.HTTP_OK || statusCode == HttpURLConnection.HTTP_CREATED;
    }

    public String execute() throws KalturaAPIException {

        try {
            mUrlConnection.connect();
            if(mAction.hasBody()) {
                OutputStream outputStream = mUrlConnection.getOutputStream();
                outputStream.write(mAction.getParams().getBytes(ConnectionConfig.REQUEST_BODY_ENCODING));
                outputStream.flush();

                if (!isRequestStatusOk(mUrlConnection.getResponseCode())) {
                    throw new KalturaAPIException(KalturaAPIException.FailureStep.OnResponse, "failed passing request, statusCode ="+mUrlConnection.getResponseCode()+", message = "+(mUrlConnection.getResponseMessage() != null ? mUrlConnection.getResponseMessage() : "Unavailable"));
                }

                outputStream.close();

                mResponse = readFromInputStream(mUrlConnection.getInputStream());

                return mUrlConnection.getResponseCode();
            }
        } catch (SocketTimeoutException e){
            throw new KalturaAPIException(ExceptionSource.OnPass, "Request was timed out");

        } catch (UnknownServiceException e){
            throw new KalturaAPIException(ExceptionSource.OnPass, "Unsupported service on read response");

        } catch (IOException e) {
            try {
                int respCode = ((HttpURLConnection)mUrlConnection).getResponseCode();
                InputStream errorStream = ((HttpURLConnection)mUrlConnection).getErrorStream();

throw new KalturaAPIException();

            } catch(IOException ex) {
                // deal with the exception
                throw new KalturaAPIException();
            }
        }


    }

    private String readFromInputStream(InputStream inputStream) throws IOException{
        if(inputStream == null) return "";

        BufferedReader br = new BufferedReader(new InputStreamReader(inputStream));
        StringBuilder response = new StringBuilder("");
        String line;
        System.out.println("read input from destination... \n");

        while ((line = br.readLine()) != null) {
            System.out.println(response);
            response.append(line);
        }

        br.close();
        return response.toString();
    }

    public ActionElement getActionRequest(){
        return mAction;
    }
}
