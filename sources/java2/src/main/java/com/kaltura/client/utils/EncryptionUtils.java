package com.kaltura.client.utils;

import javax.crypto.Cipher;
import javax.crypto.spec.IvParameterSpec;
import javax.crypto.spec.SecretKeySpec;
import java.security.MessageDigest;
import java.security.NoSuchAlgorithmException;
import java.util.Arrays;

/**
 * Created by tehilarozin on 21/08/2016.
 */
public class EncryptionUtils {

    public static final int BLOCK_SIZE = 16;

    public static byte[] encryptSHA1(String str) throws Exception {
        return encryptSHA1(str.getBytes());
    }

    public static byte[] encryptSHA1(byte[] data) throws Exception {
        try {
            MessageDigest algorithm = MessageDigest.getInstance("SHA1");
            algorithm.reset();
            algorithm.update(data);
            byte infoSignature[] = algorithm.digest();
            return infoSignature;
        } catch (NoSuchAlgorithmException e) {
            e.printStackTrace();
            throw new Exception(e);
        }
    }

    public static byte[] encryptAES(String secretKey, byte[] dataBytes) throws Exception {
        // Key
        byte[] encryptedKey = encryptSHA1(secretKey);
        byte[] keyBytes = new byte[BLOCK_SIZE];
        System.arraycopy(encryptedKey, 0, keyBytes, 0, BLOCK_SIZE);

        SecretKeySpec key = new SecretKeySpec(keyBytes, "AES");

        // IV
        byte[] ivBytes = new byte[BLOCK_SIZE];
        IvParameterSpec iv = new IvParameterSpec(ivBytes);

        // Text
        int textSize = ((dataBytes.length + BLOCK_SIZE - 1) / BLOCK_SIZE) * BLOCK_SIZE;
        byte[] textAsBytes = new byte[textSize];
        Arrays.fill(textAsBytes, (byte) 0);
        System.arraycopy(dataBytes, 0, textAsBytes, 0, dataBytes.length);

        // Encrypt
        Cipher cipher = Cipher.getInstance("AES/CBC/NOPADDING");
        cipher.init(Cipher.ENCRYPT_MODE, key, iv);
        return cipher.doFinal(textAsBytes);
    }

    public static String encryptMD5t(String str) {
        if (str == null || str.length() == 0) {
            //throw new IllegalArgumentException("String to encript cannot be null or zero length");
            return "";
        }

        MessageDigest digester = null;
        try {
            digester = MessageDigest.getInstance("MD5");
        } catch (NoSuchAlgorithmException e) {
            e.printStackTrace();
            return "";
        }

        digester.update(str.getBytes());
        byte[] hash = digester.digest();
        StringBuffer hexString = new StringBuffer();
        for (int i = 0; i < hash.length; i++) {
            if ((0xff & hash[i]) < 0x10) {
                hexString.append("0" + Integer.toHexString((0xFF & hash[i])));
            } else {
                hexString.append(Integer.toHexString(0xFF & hash[i]));
            }
        }
        return hexString.toString();
    }

    public static String encryptMD5(String str) {
        if (str == null || str.length() == 0) {
            //throw new IllegalArgumentException("String to encript cannot be null or zero length");
            return "";
        }

        MessageDigest digester = null;
        try {
            digester = MessageDigest.getInstance("MD5");
        } catch (NoSuchAlgorithmException e) {
            e.printStackTrace();
            return "";
        }

        digester.update(str.getBytes());
        byte[] digest = digester.digest();
        StringBuilder sb = new StringBuilder();
        for (byte b : digest) {
            sb.append(String.format("%02x", b & 0xff));
        }

        return sb.toString();
    }
}
