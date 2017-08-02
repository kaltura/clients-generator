package com.kaltura.utils;

import java.util.ArrayList;
import java.util.List;
import java.util.Observable;

import android.text.Editable;
import android.text.TextWatcher;
import android.util.Log;
import android.widget.EditText;

import com.kaltura.client.types.MediaEntry;

/**
 * Seeking a string entered in the search for the entry
 */
public class SearchTextEntry extends Observable implements TextWatcher {

    private int textlength;
    private List<MediaEntry> copyList;
    private List<MediaEntry> listCategory;
    private List<String> copyListInLowerCase;
    private EditText editText;
    private String TAG;

    public SearchTextEntry() {
    }

    public void init(String TAG, EditText editText, List<MediaEntry> listCategory) {
        this.TAG = TAG;
        this.editText = editText;
        this.listCategory = listCategory;
        textlength = 0;
        copyList = new ArrayList<MediaEntry>();
        copyListInLowerCase = new ArrayList<String>();
        copyList.addAll(listCategory);
        for (MediaEntry mediaEntry : listCategory) {
            copyListInLowerCase.add(mediaEntry.getName().toLowerCase());
        }
    }

    @Override
    public void beforeTextChanged(CharSequence s, int start, int count, int after) {
    }

    @Override
    public void onTextChanged(CharSequence s, int start, int before, int count) {
        textlength = editText.getText().length();

        listCategory.clear();

        for (int i = 0; i < copyList.size(); i++) {
            Log.w(TAG, "search - str: " + copyList.get(i).getName());
            //    Matcher m1 = p1.matcher(copyList.get(i).name);

            if (copyListInLowerCase.get(i).indexOf(editText.getText().toString().toLowerCase()) != -1) {
                // Log.w(TAG, "search - res: " + m1.group(1));
                listCategory.add(copyList.get(i));
            } else {
                Log.w(TAG, "search - not found");
            }
        }
        setChanged();
        notifyObservers(listCategory);
    }

    @Override
    public void afterTextChanged(Editable s) {
    }
}
