package com.kaltura.activity;

import java.util.ArrayList;
import java.util.List;
import java.util.concurrent.CountDownLatch;

import android.app.Activity;
import android.content.pm.ActivityInfo;
import android.os.AsyncTask;
import android.os.Bundle;
import android.util.Log;
import android.view.MotionEvent;
import android.view.View;
import android.widget.EditText;
import android.widget.TextView;
import android.widget.Toast;

import com.kaltura.bar.ActionBar;
import com.kaltura.client.types.APIException;
import com.kaltura.client.types.Category;
import com.kaltura.client.types.ListResponse;
import com.kaltura.client.utils.response.OnCompletion;
import com.kaltura.client.utils.response.base.Response;
import com.kaltura.enums.States;
import com.kaltura.mediatorActivity.TemplateActivity;
import com.kaltura.services.Categories;
import com.kaltura.utils.SpinnerCategory;
import com.kaltura.utils.Utils;

public class VideoInfo extends TemplateActivity {

    /**
     *
     */
    private SpinnerCategory spCategoty;
    private Activity activity;
    private String pathfromURI;
    private EditText et_title;
    private EditText et_description;
    private EditText et_tags;

    @Override
    public void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        init();
        setContentView(R.layout.video_info);
        setRequestedOrientation(ActivityInfo.SCREEN_ORIENTATION_PORTRAIT);
        bar = new ActionBar(this, TAG);
        bar.setTitle(getText(R.string.upload));
        bar.setVisibleBackButton(View.GONE);

        extractBundle();

        activity = this;

        spCategoty = new SpinnerCategory(TAG, this, "Category", null);
        new DownloadListCatigoriesTask().execute();

        et_title = ((EditText) findViewById(R.id.et_title));
        et_description = ((EditText) findViewById(R.id.et_description));
        et_tags = ((EditText) findViewById(R.id.et_tags));
        setFont();
    }

    @Override
    public void onRestoreInstanceState(Bundle savedInstanceState) {
        super.onRestoreInstanceState(savedInstanceState);
        new DownloadListCatigoriesTask().execute();
    }

    private void extractBundle() {
        try {
            Bundle extras = getIntent().getExtras();
            pathfromURI = extras.getString("pathfromURI");
            Log.w(TAG, "real path videos uri: " + pathfromURI);
        } catch (Exception e) {
            e.printStackTrace();
            Log.w(TAG, "err: " + e.getMessage());
            pathfromURI = "";
        }
    }

    private void setFont() {
        ((TextView) findViewById(R.id.tv_category)).setTypeface(typeFont);
        ((TextView) findViewById(R.id.tv_title)).setTypeface(typeFont);
        ((TextView) findViewById(R.id.tv_description)).setTypeface(typeFont);
        ((TextView) findViewById(R.id.tv_tags)).setTypeface(typeFont);
        et_title.setTypeface(typeFont);
        et_description.setTypeface(typeFont);
        et_tags.setTypeface(typeFont);
    }

    /**
     * Called to process touch screen events.
     */
    @Override
    public boolean dispatchTouchEvent(MotionEvent ev) {

        switch (ev.getAction()) {
            case MotionEvent.ACTION_DOWN:
                break;
            case MotionEvent.ACTION_UP:
                break;
            case MotionEvent.ACTION_MOVE:
                //Hide the keyboard on the screen of a finger        	
                imm.hideSoftInputFromWindow(getWindow().getCurrentFocus().getWindowToken(), 0);
                break;
        }
        return super.dispatchTouchEvent(ev);
    }

    public void onClick(View v) {
        switch (v.getId()) {
            case R.id.rl_upload:
                Log.w(TAG, "click on upload button");
                getActivityMediator().showUploading(this.pathfromURI, spCategoty.getSelectedItem(), et_title.getText().toString(),
                        et_description.getText().toString(), et_tags.getText().toString());
                break;
            case R.id.iv_bar_menu:
                getActivityMediator().showMain();
                break;
            default:
                break;
        }
    }

    private class DownloadListCatigoriesTask extends AsyncTask<Void, States, Void> {

        private String message;
        private List<Category> listCategory;

        @Override
        protected Void doInBackground(Void... params) {
            final CountDownLatch doneSignal = new CountDownLatch(1);
            // Test for connection
            try {
                if (Utils.checkInternetConnection(getApplicationContext())) {
                    /**
                     * Getting list of all categories
                     */
                    publishProgress(States.LOADING_DATA);
                    Categories.listAllCategories(TAG, 1, 500, new OnCompletion<Response<ListResponse<Category>>>() {
                        @Override
                        public void onComplete(Response<ListResponse<Category>> response) {
                            if(response.isSuccess()) {
                                listCategory = response.results.getObjects();
                            }
                            else {
                                response.error.printStackTrace();
                                message = response.error.getMessage();
                                Log.w(TAG, message);
                                publishProgress(States.ERR);
                            }
                            doneSignal.countDown();
                        }
                    });
                }
            } catch (Exception e) {
                e.printStackTrace();
                message = e.getMessage();
                Log.w(TAG, message);
                publishProgress(States.NO_CONNECTION);
            }

            try {
                doneSignal.await();
            } catch (InterruptedException e) {
                e.printStackTrace();
            }
            return null;
        }

        @Override
        protected void onPostExecute(Void param) {
            progressDialog.hide();

            List<String> ls = new ArrayList<String>();
            for (Category list : listCategory) {
                ls.add(list.getName());
            }
            spCategoty.addData(ls);
        }

        @Override
        protected void onProgressUpdate(States... progress) {
            for (States state : progress) {
                if (state == States.LOADING_DATA) {
                    progressDialog.hide();
                    showProgressDialog("Loading data...");
                }
                if (state == States.NO_CONNECTION) {
                    progressDialog.hide();
                    Toast.makeText(context, message, Toast.LENGTH_SHORT).show();
                }
                if (state == States.ERR) {
                    progressDialog.hide();
                    Toast.makeText(context, message, Toast.LENGTH_SHORT).show();
                }
            }
        }
    }
}
