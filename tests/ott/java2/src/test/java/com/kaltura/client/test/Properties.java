package com.kaltura.client.test;

import java.util.ResourceBundle;

public class Properties {

    private static ResourceBundle resourceBundle;

    // url properties
    public static final String API_BASE_URL = "api_base_url";
    public static final String API_VERSION = "api_version";
    public static final String INGEST_BASE_URL = "ingest_base_url";
    public static final String INGEST_REPORT_URL = "ingest_report_url";

    // DB properties
    public static final String DB_URL = "db_url";
    public static final String DB_USER = "db_user";
    public static final String DB_PASSWORD = "db_password";

    // Request properties
    public static final String PARTNER_ID = "partner_id";

    // global users
    public static final String DEFAULT_USER_PASSWORD = "default_user_password";

    // file types
    public static final String WEB_FILE_TYPE = "web_file_type";
    public static final String MOBILE_FILE_TYPE = "mobile_file_type";

    // media types ids
    public static final String MOVIE_MEDIA_TYPE_ID = "movie_media_type_id";
    public static final String EPISODE_MEDIA_TYPE_ID = "episode_media_type_id";

    // ingest
    public static final String INGEST_USER_USERNAME = "ingest_user_username";
    public static final String INGEST_USER_PASSWORD = "ingest_user_password";

    public static final String INGEST_BUSINESS_MODULE_USER_USERNAME = "ingest_business_module_user_username";
    public static final String INGEST_BUSINESS_MODULE_USER_PASSWORD = "ingest_business_module_user_password";

    // channels
    public static final String DEFAULT_CHANNEL = "default_channel"; // automatic channel with "Cut Tags Type"="Or", Tags "Series name"="Shay_Series;" and "Free"="Shay_Series;"

    // price codes
    public static final String PRICE_CODE_AMOUNT = "price_code_amount"; // 4.99

    // usage modules
    public static final String DEFAULT_USAGE_MODULE_4_INGEST_PPV = "default_usage_module_4_ingest_ppv"; // module has 10 Minutes life cycles, 0 maximum views
    public static final String DEFAULT_USAGE_MODULE_4_INGEST_MPP = "default_usage_module_4_ingest_mpp"; // module has 1 Day life cycles, 0 maximum views

    // product codes
    public static final String DEFAULT_PRODUCT_CODE = "default_product_code";

    public static String getProperty(String propertyKey) {
        if (resourceBundle == null) {
            resourceBundle = ResourceBundle.getBundle("test");
        }
        return resourceBundle.getString(propertyKey);
    }
}

// todo global list
// TODO: 3/12/2018 open conference page with all the documentation problems
// TODO: 12/MAR/2018 decide if we need that autoskip logic for tests with known opened bugs:
// https://dzone.com/articles/how-to-automatically-skip-tests-based-on-defects-s
// TODO: 3/19/2018 update readme file with project structure and list of services
