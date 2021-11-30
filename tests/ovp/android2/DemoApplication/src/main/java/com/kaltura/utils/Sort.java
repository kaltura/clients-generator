package com.kaltura.utils;

import java.util.Comparator;

import com.kaltura.client.types.Category;
import com.kaltura.client.types.FlavorAsset;
import com.kaltura.client.types.MediaEntry;

/**
 * The class performs a sort
 */
public class Sort<T> implements Comparator<T> {

    private String filter = "name";
    private String direction = "compareTo";

    /**
     * Constructor Description of Sort<T>
     *
     * @param filter Specify which field to sort
     * @param direction Specifies the sort direction
     */
    public Sort(String filter, String direction) {
        this.filter = filter;
        this.direction = direction;
    }

    /**
     * Compares its two arguments for order. Returns a negative integer, zero,
     * or a positive integer as the first argument is less than, equal to, or
     * greater than the second.
     *
     * @param paramT1 the first object to be compared.
     * @param paramT2 the second object to be compared.
     *
     * @return a negative integer, zero, or a positive integer as the first
     * argument is less than, equal to, or greater than the second.
     *
     * @throws ClassCastException - if the arguments' types prevent them from
     * being compared by this Comparator.
     */
    @Override
    public int compare(T paramT1, T paramT2) {

        int res = 0;
        if (paramT1 instanceof MediaEntry && paramT2 instanceof MediaEntry) {
            if (this.filter.equals("name")) {
                res = ((MediaEntry) paramT1).getName().compareTo(((MediaEntry) paramT2).getName());
            }
            if (this.filter.equals("plays") && this.direction.equals("compareTo")) {
                res = new Integer(((MediaEntry) paramT1).getPlays()).compareTo(new Integer(((MediaEntry) paramT2).getPlays()));
            } else {
                res = ((MediaEntry) paramT2).getPlays() - ((MediaEntry) paramT1).getPlays();
            }
            if (this.filter.equals("createdAt")) {
                res = new Integer(((MediaEntry) paramT1).getCreatedAt()).compareTo(new Integer(((MediaEntry) paramT2).getCreatedAt()));
            }
        }
        if (paramT1 instanceof Category && paramT2 instanceof Category) {
            res = ((Category) paramT1).getName().compareTo(((Category) paramT2).getName());
        }
        if (paramT1 instanceof FlavorAsset && paramT2 instanceof FlavorAsset) {
            res = ((FlavorAsset) paramT2).getBitrate() - ((FlavorAsset) paramT1).getBitrate();
        }
        return res;
    }
}
