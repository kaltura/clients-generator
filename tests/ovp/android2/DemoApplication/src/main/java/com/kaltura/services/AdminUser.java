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
import com.kaltura.client.utils.response.base.Response;
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

                UserService.LoginByLoginIdUserBuilder requestBuilder = UserService.loginByLoginId(email, password)
                .setCompletion(new OnCompletion<Response<String>>() {
                    @Override
                    public void onComplete(final Response<String> response) {
                        if(response.isSuccess()) {
                            Log.w(TAG, response.results);
                            // set the kaltura client to use the recieved ks as default for all future operations
                            client.setSessionId(response.results);
                            userIsLogin = true;
                            handler.post(new Runnable() {

                                @Override
                                public void run() {
                                    loginTaskListener.onLoginSuccess();
                                }
                            });
                        }
                        else {
                            response.error.printStackTrace();
                            Log.w(TAG, "Login error: " + response.error.getMessage() + " error code: " + response.error.getCode());
                            userIsLogin = false;
                            handler.post(new Runnable() {

                                @Override
                                public void run() {
                                    loginTaskListener.onLoginError(response.error.getMessage());
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
