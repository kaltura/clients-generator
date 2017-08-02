package com.kaltura.activity;

import java.util.ArrayList;
import java.util.Collections;
import java.util.List;
import java.util.concurrent.CountDownLatch;

import android.app.Activity;
import android.content.pm.ActivityInfo;
import android.os.AsyncTask;
import android.os.Bundle;
import android.util.Log;
import android.view.MotionEvent;
import android.view.SurfaceHolder;
import android.view.SurfaceView;
import android.view.View;
import android.view.WindowManager;
import android.widget.Toast;

import com.kaltura.client.types.APIException;
import com.kaltura.client.types.EntryContextDataResult;
import com.kaltura.client.types.FlavorAsset;
import com.kaltura.client.utils.response.OnCompletion;
import com.kaltura.client.utils.response.base.Response;
import com.kaltura.enums.States;
import com.kaltura.mediatorActivity.TemplateActivity;
import com.kaltura.services.FlavorAssets;
import com.kaltura.utils.Sort;
import com.kaltura.utils.Utils;

public class Player extends TemplateActivity implements SurfaceHolder.Callback {

	private com.kaltura.player.ViewPlayer viewPlayer;
	private String dataUrl;
	private String entryId;
	private SurfaceView surface;
	private SurfaceHolder holder;
	private int duration;
	private Activity activity;
	private int partnerId;
	private List<FlavorAsset> listFlavorAssets = new ArrayList<FlavorAsset>();
	private List<FlavorAsset> copyListFlavorAssets;
	private String url;

	@Override
	public void onCreate(Bundle savedInstanceState) {

		super.onCreate(savedInstanceState);
		init();
		/**
		 * Screen orientation is portrait
		 */
		setRequestedOrientation(ActivityInfo.SCREEN_ORIENTATION_LANDSCAPE);
		/**
		 * Hide notification bar
		 */
		getWindow().setFlags(WindowManager.LayoutParams.FLAG_FULLSCREEN,
				WindowManager.LayoutParams.FLAG_FULLSCREEN);
		setContentView(R.layout.player);

		extractBundle();

		activity = this;

		surface = (SurfaceView) findViewById(R.id.surfaceView1);
	}

	// @Override
	// public void onResume() {
	// super.onResume();
	// holder = surface.getHolder();
	// holder.addCallback(this);
	// try {
	// holder.setType(SurfaceHolder.SURFACE_TYPE_PUSH_BUFFERS);
	// holder.setFixedSize(400, 300);
	// viewPlayer = new com.kaltura.player.ViewPlayer(TAG, activity, holder,
	// duration, entryId);
	// viewPlayer.setThumb(url);
	// new DownloadTask().execute();
	// } catch (IllegalArgumentException e) {
	// e.printStackTrace();
	// Log.w(TAG, "err: " + e);
	// } catch (SecurityException e) {
	// e.printStackTrace();
	// Log.w(TAG, "err: " + e);
	// } catch (IllegalStateException e) {
	// e.printStackTrace();
	// Log.w(TAG, "err: " + e);
	// }
	// }
	//
	public void onResume() {
		super.onResume();
		holder = surface.getHolder();
		holder.addCallback(this);
	}

	public void surfaceCreated(SurfaceHolder arg0) {		
		Log.w(TAG, "surfaceCreated called");
		try {
			holder.setType(SurfaceHolder.SURFACE_TYPE_PUSH_BUFFERS);
			holder.setFixedSize(400, 300);
			viewPlayer = new com.kaltura.player.ViewPlayer(TAG, activity,
					holder, duration, entryId, partnerId);
			viewPlayer.setThumb(url);
			new DownloadTask().execute();
		} catch (IllegalArgumentException e) {
			e.printStackTrace();
			Log.w(TAG, "err: " + e);
		} catch (SecurityException e) {
			e.printStackTrace();
			Log.w(TAG, "err: " + e);
		} catch (IllegalStateException e) {
			e.printStackTrace();
			Log.w(TAG, "err: " + e);
		}
	}

	@Override
	protected void onPause() {
		super.onPause();
		if (viewPlayer != null) {
			viewPlayer.setRelease();
		}
	}

	@Override
	protected void onDestroy() {
		super.onDestroy();
		if (viewPlayer != null) {
			viewPlayer.setRelease();
			viewPlayer = null;
		}
	}

	public void surfaceChanged(SurfaceHolder arg0, int arg1, int arg2, int arg3) {
		Log.w(TAG, "surfaceChanged called");
	}

	public void surfaceDestroyed(SurfaceHolder arg0) {
		Log.w(TAG, "surfaceDestroyed called");
	}

	private void extractBundle() {
		try {
			Bundle extras = getIntent().getExtras();
			entryId = extras.getString("entryId");
			dataUrl = extras.getString("dataUrl");
			url = extras.getString("url");
			duration = extras.getInt("duration");
			partnerId = extras.getInt("partnerId");
			Log.w(TAG, "dataUrl: " + dataUrl + " duration: " + duration);
		} catch (Exception e) {
			e.printStackTrace();
		}
	}

	/**
	 * Called to process touch screen events.
	 */
	@Override
	public boolean dispatchTouchEvent(MotionEvent ev) {

		switch (ev.getAction()) {
		case MotionEvent.ACTION_UP:
			if (viewPlayer != null
					&& viewPlayer.getStatePanel() == View.VISIBLE) {// &&
																	// !viewPlayer.isRun()){
				// Panel is visible
			}
			break;
		case MotionEvent.ACTION_DOWN:
			break;

		case MotionEvent.ACTION_MOVE:
			if (viewPlayer != null) {
				viewPlayer.hidePanel();
				viewPlayer.setStatePanel(View.VISIBLE);
			} else {
			}
			break;
		}
		return super.dispatchTouchEvent(ev);
	}

	private class DownloadTask extends AsyncTask<Void, States, Void> {

		private String message;

		@Override
		protected Void doInBackground(Void... params) {
			final CountDownLatch doneSignal = new CountDownLatch(1);
			// Test for connection
			try {
				if (Utils.checkInternetConnection(getApplicationContext())) {
					// Getting list of all entries category
					publishProgress(States.LOADING_DATA);

					FlavorAssets.listAllFlavorsFromContext(TAG, entryId, "widevine_mbr,widevine,iphonenew", new OnCompletion<Response<EntryContextDataResult>>() {
						@Override
						public void onComplete(Response<EntryContextDataResult> response) {
							listFlavorAssets = response.results.getFlavorAssets();

							copyListFlavorAssets =  new ArrayList<FlavorAsset>();
							Collections.sort(listFlavorAssets,
									new Sort<FlavorAsset>("bitrate",
											"reverse"));
							for (FlavorAsset f : listFlavorAssets) {
								Log.w(TAG, "FLAVORS:  containerFormat: "
										+ f.getContainerFormat() + " description: "
										+ f.getDescription() + " bitrate: " + f.getBitrate()
										+ " frameRate: " + f.getFrameRate() + " size: "
										+ f.getSize() + " height: " + f.getHeight()
										+ " width: " + f.getWidth() + " fileExt "
										+ f.getFileExt() + " partnerDescription "
										+ f.getPartnerDescription() + " tags: " + f.getTags()
										+ " videoCodecId " + f.getVideoCodecId());
							}
							for (FlavorAsset FlavorAsset : listFlavorAssets) {
								if (!new Integer(
										Math.round(FlavorAsset.getBitrate() / 100) * 100)
										.equals(0)) {
									copyListFlavorAssets.add(FlavorAsset);
								}
							}
							doneSignal.countDown();
						}
					});
				}
				Log.w(TAG, "Thread is end!");
			} catch (Exception e) {
				e.printStackTrace();
				message = e.getMessage();
				Log.w(TAG, "" + message);
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

			Log.w(TAG, "----------------");
			for (FlavorAsset f : copyListFlavorAssets) {
				Log.w(TAG, "FLAVORS:  containerFormat: " + f.getContainerFormat()
						+ " description: " + f.getDescription() + " bitrate: "
						+ f.getBitrate() + " frameRate: " + f.getFrameRate() + " size: "
						+ f.getSize() + " height: " + f.getHeight() + " width: "
						+ f.getWidth() + " fileExt " + f.getFileExt()
						+ " partnerDescription " + f.getPartnerDescription()
						+ " tags " + f.getTags() + " videoCodecId " + f.getVideoCodecId());

			}
			if (viewPlayer != null) {
				viewPlayer.addListRates(copyListFlavorAssets);
				viewPlayer.autoStart();
			}
		}

		@Override
		protected void onProgressUpdate(States... progress) {
			for (States state : progress) {
				if (state == States.LOADING_DATA) {
					progressDialog.hide();
					if (viewPlayer != null) {
						viewPlayer.setVisibleLoading();
					}

				}
				if (state == States.NO_CONNECTION) {
					progressDialog.hide();
					Toast.makeText(context, message, Toast.LENGTH_SHORT).show();
				}
			}
		}
	}
}