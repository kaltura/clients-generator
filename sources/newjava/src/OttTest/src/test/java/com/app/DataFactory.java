package com.app;

import java.util.ArrayList;

/**
 * Created by tehilarozin on 05/09/2016.
 */
public class DataFactory {

    static ArrayList<UserLogin> users;

    static {
        fillWithUsers();
    }

    public static UserLogin getUser(){
        int Min = 0;
        int Max = users.size()-1;

        int index = Min + (int)(Math.random() * ((Max - Min) + 1));
        return users.get(index);
    }

    static void fillWithUsers(){
        users = new ArrayList<>();
        users.add(new UserLogin("albert@gmail.com", "123456"));
        users.add(new UserLogin("betsy@gmail.com", "123456"));
        users.add(new UserLogin("Alfred@gmail.com", "123456"));
        users.add(new UserLogin("ziv.ilan@kaltura.com", "123456"));
        users.add(new UserLogin("itan@b.com", "123456"));
    }

    public static class UserLogin{
        public String username;
        public String password;

        public UserLogin(String username, String password){
            this.username = username;
            this.password = password;
        }
    }
}
