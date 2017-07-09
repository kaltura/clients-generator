package com.kaltura.services;

//<editor-fold defaultstate="collapsed" desc="comment">
import android.os.Handler;
import android.util.Log;

import com.kaltura.client.types.APIException;
import com.kaltura.client.Client;
import com.kaltura.client.Configuration;
import com.kaltura.client.services.UserService;
import com.kaltura.client.utils.request.RequestBuilder;
import com.kaltura.client.utils.response.OnCompletion;
import com.kaltura.utils.ApiHelper;
//</editor-fold>

/**
 * Manage details for the administrative user
 *
 */
public class AdminUser {

    private static boolean userIsLogin;

    /**
     */
    public static boolean userIsLogin() {
        return userIsLogin;
    }

    /**
     * Get an admin session using admin email and password (Used for login to
     * the KMC application)
     *
     * @param TAG constant in your class
     * @param email
     * @param password
     *
     * @throws APIException
     */
    public static void login(final String TAG, final String email, final String password, final LoginTaskListener loginTaskListener) {
        final Handler handler = new Handler();
        Runnable runnable = new Runnable() {

            @Override
            public void run() {
                final Client client = ApiHelper.getClient();

                RequestBuilder<String> requestBuilder = UserService.loginByLoginId(email, password)
                .setCompletion(new OnCompletion<String>() {
                    @Override
                    public void onComplete(String ks, final APIException e) {
                        if(ks != null) {
                            Log.w(TAG, ks);
                            // set the kaltura client to use the recieved ks as default for all future operations
                            client.setSessionId(ks);
                            userIsLogin = true;
                            handler.post(new Runnable() {

                                @Override
                                public void run() {
                                    loginTaskListener.onLoginSuccess();
                                }
                            });
                        }
                        else if(e != null) {
                            e.printStackTrace();
                            Log.w(TAG, "Login error: " + e.getMessage() + " error code: " + e.getCode());
                            userIsLogin = false;
                            handler.post(new Runnable() {

                                @Override
                                public void run() {
                                    loginTaskListener.onLoginError(e.getMessage());
                                }
                            });
                        }
                    }
                });
                ApiHelper.getRequestQueue().queue(requestBuilder.build(client));
            }
        };
        new Thread(runnable).start();
    }

    public interface LoginTaskListener {

        void onLoginSuccess();

        void onLoginError(String errorMessage);
    }
}
