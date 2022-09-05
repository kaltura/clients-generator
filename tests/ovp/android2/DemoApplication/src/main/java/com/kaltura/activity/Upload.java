package com.kaltura.activity;

import android.Manifest;
import android.content.Intent;
import android.content.pm.ActivityInfo;
import android.content.pm.PackageManager;
import android.database.Cursor;
import android.net.Uri;
import android.os.Bundle;
import android.provider.MediaStore;
import android.util.Log;
import android.view.View;

import androidx.core.app.ActivityCompat;
import androidx.core.content.ContextCompat;

import com.kaltura.bar.ActionBar;
import com.kaltura.mediatorActivity.TemplateActivity;

public class Upload extends TemplateActivity {

    private static final int GALLERY = 1;
    private static final int RECORD_VIDEO = 2;

    private static final int PERMISSIONS_REQUEST_CAMERA = 1;
    private static final int PERMISSIONS_REQUEST_READ_EXTERNAL_STORAGE = 2;

    @Override
    public void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setRequestedOrientation(ActivityInfo.SCREEN_ORIENTATION_PORTRAIT);
        init();
        setContentView(R.layout.upload);

    }

    public void onClick(View v) {
        switch (v.getId()) {
            case R.id.rl_recod_video:
                startCamera(true);
                break;
            case R.id.rl_pick_from_gallery:
                startGallery(true);
                break;
            case R.id.iv_bar_menu:
                getActivityMediator().showMain();
                break;
            default:
                break;
        }
    }

    private void startGallery(boolean requestIfNotPermitted) {
        if (ContextCompat.checkSelfPermission(this, Manifest.permission.READ_EXTERNAL_STORAGE) == PackageManager.PERMISSION_GRANTED) {
            Intent intent = new Intent(Intent.ACTION_PICK, null);
            intent.setType("video/*");
            startActivityForResult(intent, 1);
        }
        else {
            ActivityCompat.requestPermissions(this, new String[]{Manifest.permission.READ_EXTERNAL_STORAGE}, PERMISSIONS_REQUEST_READ_EXTERNAL_STORAGE);
        }
    }

    private void startCamera(boolean requestIfNotPermitted) {
        if (ContextCompat.checkSelfPermission(this, Manifest.permission.CAMERA) == PackageManager.PERMISSION_GRANTED && ContextCompat.checkSelfPermission(this, Manifest.permission.WRITE_EXTERNAL_STORAGE) == PackageManager.PERMISSION_GRANTED) {
            Intent intent = new Intent(MediaStore.ACTION_VIDEO_CAPTURE);
            intent.putExtra(MediaStore.EXTRA_VIDEO_QUALITY, 1); // set the video image quality to high
            intent.putExtra(MediaStore.EXTRA_DURATION_LIMIT, 0);
            // start the Video Capture Intent
            startActivityForResult(intent, RECORD_VIDEO);
        }
        else {
            ActivityCompat.requestPermissions(this, new String[]{Manifest.permission.CAMERA, Manifest.permission.WRITE_EXTERNAL_STORAGE}, PERMISSIONS_REQUEST_CAMERA);
        }
    }

    @Override
    public void onRequestPermissionsResult(int requestCode, String permissions[], int[] grantResults) {
        switch (requestCode) {
            case PERMISSIONS_REQUEST_CAMERA: {
                startCamera(false);
                break;
            }
            case PERMISSIONS_REQUEST_READ_EXTERNAL_STORAGE: {
                startGallery(false);
                break;
            }
        }
    }

    @Override
    public void onStart() {
        super.onStart();
        bar = new ActionBar(this, TAG);
        bar.setTitle(getText(R.string.upload));
        bar.setVisibleBackButton(View.INVISIBLE);
        bar.setVisibleSearchButon(View.INVISIBLE);

    }

    @Override
    protected void onActivityResult(int requestCode, int resultCode, Intent data) {
        if ((resultCode == RESULT_OK) && (data != null)) {
            switch (requestCode) {
                case RECORD_VIDEO:
                    Log.w(TAG, "real path: " + getRealPathFromURI(data.getData()));
                    getActivityMediator().showVideoInfo(getRealPathFromURI(data.getData()));
                    break;
                case GALLERY:
                    Log.w(TAG, "real path: " + getRealPathFromURI(data.getData()));
                    getActivityMediator().showVideoInfo(getRealPathFromURI(data.getData()));
                    break;
                default:
                    break;
            }
        }
    }

    public String getRealPathFromURI(Uri currImageURI) {
        // can post image
        String[] proj = {MediaStore.Video.Media.DATA};
        Cursor cursor = managedQuery(currImageURI,
                proj, // Which columns to return
                null, // WHERE clause; which rows to return (all rows)
                null, // WHERE clause selection arguments (none)
                null); // Order-by clause (ascending by name)
        int column_index = cursor.getColumnIndexOrThrow(MediaStore.Video.Media.DATA);
        cursor.moveToFirst();
        return cursor.getString(column_index);
    }
}
