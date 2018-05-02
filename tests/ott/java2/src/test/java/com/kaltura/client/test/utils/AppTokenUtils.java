package com.kaltura.client.test.utils;

import com.kaltura.client.enums.AppTokenHashType;
import com.kaltura.client.types.AppToken;

import javax.annotation.Nullable;

public class AppTokenUtils extends BaseUtils {

    public static AppToken addAppToken(@Nullable String userId, @Nullable AppTokenHashType appTokenHashType, @Nullable String sessionPrivileges, @Nullable Integer expiryDate) {
        AppToken appToken = new AppToken();
        appToken.setHashType(appTokenHashType);
        appToken.setSessionUserId(userId);
        appToken.setSessionPrivileges(sessionPrivileges);
        appToken.setExpiry(expiryDate);

        return appToken;
    }

    // Return hashed token according to the hash type provides
    public static String getTokenHash(AppTokenHashType hashType, String anonymousKs, String token) {

        String concatenatedString = anonymousKs + token;
        String hashedString = "";

        if (hashType.equals(AppTokenHashType.MD5)) {
            hashedString = org.apache.commons.codec.digest.DigestUtils.md5Hex(concatenatedString);
        } else if (hashType.equals(AppTokenHashType.SHA1)) {
            hashedString = org.apache.commons.codec.digest.DigestUtils.sha1Hex(concatenatedString);
        } else if (hashType.equals(AppTokenHashType.SHA256)) {
            hashedString = org.apache.commons.codec.digest.DigestUtils.sha256Hex(concatenatedString);
        } else if (hashType.equals(AppTokenHashType.SHA512)) {
            hashedString = org.apache.commons.codec.digest.DigestUtils.sha512Hex(concatenatedString);
        }

        return hashedString;
    }


}

