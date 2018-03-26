/*
 * To change this template, choose Tools | Templates
 * and open the template in the editor.
 */
package com.kaltura.services;

import java.io.File;
import java.io.FileInputStream;
import java.io.FileNotFoundException;
import java.io.FileOutputStream;
import java.io.IOException;
import java.util.Observable;
import java.util.concurrent.atomic.AtomicInteger;
import java.util.logging.Level;
import java.util.logging.Logger;

import android.os.Environment;
import android.util.Log;

import com.kaltura.client.services.MediaService;
import com.kaltura.client.services.UploadTokenService;
import com.kaltura.client.types.APIException;
import com.kaltura.client.types.MediaEntry;
import com.kaltura.client.types.UploadToken;
import com.kaltura.client.types.UploadedFileTokenResource;
import com.kaltura.client.utils.request.RequestBuilder;
import com.kaltura.client.utils.response.OnCompletion;
import com.kaltura.client.utils.response.base.Response;
import com.kaltura.utils.ApiHelper;

/**
 * Upload files to the server
 */
public class FileUploader extends Observable {
    
    private String TAG;
    private File fileData;
    private UploadToken uploadToken;
    private int maxRetries;
    private MediaEntry entry;
    private double remainingUploadFileSize;
    private boolean startUpload = true;
    private int readSum = 0;
    private OnUploadCompletion onCompletion = null;

    private int sizeBuf = 1024 * 1000;
    private byte buf[] = new byte[sizeBuf];
    private String PATH = Environment.getExternalStorageDirectory() + "/download/";
    private FileOutputStream fos = null;
    private FileInputStream fis = null;
    private int attemptUpload;
    private int chunkIndex;

    public interface OnUploadCompletion {
        void onComplete(UploadToken uploadToken, Exception error);
    }

    /**
     * Constructor Description of UploadToken
     *
     * @param TAG constant in your class
     * @param maxRetries quantity attempts upload to the server
     */
    public FileUploader(String TAG, int maxRetries) {
        this.TAG = TAG;
        this.maxRetries = maxRetries;
    }
    
    /**
     * Get size uploading file in percent
     *
     * @return size uploading file in percent
     */
    private int getUploadedFileSize() {
        int res = 0;
        try {
            res = (int) (uploadToken.getUploadedFileSize() / fileData.length() * 100.0);
        } catch (ArithmeticException e) {
            e.printStackTrace();
            Log.w(TAG, e);
            res = 0;
        }
        return res;
    }
    
    public void setStartUpload(boolean startUpload) {
        this.startUpload = startUpload;
    }

    private void uploadChunk(final File outFile, final int numRead, final boolean finalChunk, boolean resume, int resumeAt) {
        UploadTokenService.UploadUploadTokenBuilder requestBuilder = UploadTokenService.upload(uploadToken.getId(), outFile, resume, finalChunk, resumeAt)
                .setCompletion(new OnCompletion<Response<UploadToken>>() {
                    @Override
                    public void onComplete(Response<UploadToken> response) {
                        if(response.isSuccess()) {
                            uploadToken = response.results;
                            readSum += numRead;
                        }
                        else {
                            retry(response.error);
                        }

                        outFile.delete();
                        setChanged();
                        notifyObservers(getUploadedFileSize());

                        if(finalChunk) {
                            done();
                        }
                        else {
                            uploadNextChunk();
                        }
                    }
                });
        ApiHelper.execute(requestBuilder);
    }

    private void uploadNextChunk() {
        File outFile;
        int numRead;

        try {
            Log.w(TAG, "Available bytes: " + fis.available());
            remainingUploadFileSize = fis.available();
            if (remainingUploadFileSize == 0) {
                done();
                return;
            }
            buf = new byte[sizeBuf];
            numRead = fis.read(buf);
            Log.w(TAG, "Readed bytes: " + numRead);

            outFile = new File(PATH, "upload.dat");
            fos = new FileOutputStream(outFile);
            fos.write(buf, 0, numRead);
            fos.close();
        }
        catch(IOException e) {
            retry(e);
            return;
        }

        chunkIndex = 0;
        attemptUpload = 0;
        Log.w(TAG, "HASH:" + Float.valueOf(fileData.length()).hashCode());

        final boolean finalChunk = (remainingUploadFileSize <= sizeBuf);
        boolean resume = readSum > 0;
        int resumeAt = resume ? readSum : -1;

        uploadChunk(outFile, numRead, finalChunk, resume, resumeAt);
    }

    private void done() {
        startUpload = false;

        UploadedFileTokenResource fileTokenResource = new UploadedFileTokenResource();
        fileTokenResource.setToken(uploadToken.getId());

        MediaService.AddContentMediaBuilder requestBuilder =  MediaService.addContent(entry.getId(), fileTokenResource)
        .setCompletion(new OnCompletion<Response<MediaEntry>>() {
            @Override
            public void onComplete(Response<MediaEntry> response) {
                if(response.isSuccess()) {
                    Log.w(TAG, "\nUploaded a new Video file to entry: " + response.results.getId());
                }
                else {
                    response.error.printStackTrace();
                    Log.w(TAG, "err: " + response.error.getMessage());
                }

                onCompletion.onComplete(uploadToken, response.error);
            }
        });
        ApiHelper.execute(requestBuilder);
    }

    private void retry(Exception error) {
        attemptUpload++;
        if(attemptUpload > maxRetries) {
            onCompletion.onComplete(null, error);
        }
        else {
            uploadNextChunk();
        }
    }

    public void upload(MediaEntry entry, final String pathfromURI, OnUploadCompletion onCompletion) {
        this.onCompletion = onCompletion;
        this.entry = entry;
        fileData = new File(pathfromURI);

        remainingUploadFileSize = 0;

        Log.w(TAG, "\nUploading a video file...");
        readSum = 0;

        UploadTokenService.AddUploadTokenBuilder requestBuilder = UploadTokenService.add().setCompletion(new OnCompletion<Response<UploadToken>>() {
            @Override
            public void onComplete(Response<UploadToken> response) {
                if(response.isSuccess()) {
                    uploadToken = response.results;
                    try {
                        fis = new FileInputStream(fileData);
                    } catch (FileNotFoundException ex) {
                        Log.w(TAG, "err: ", ex);
                    }

                    uploadNextChunk();
                }
                else {
                    Logger.getLogger(UploadToken.class.getName()).log(Level.SEVERE, null, response.error);
                    return;
                }
            }
        });
        ApiHelper.execute(requestBuilder);
    }
}
