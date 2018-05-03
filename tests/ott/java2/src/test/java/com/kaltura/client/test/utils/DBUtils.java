package com.kaltura.client.test.utils;

import com.kaltura.client.Logger;
import com.kaltura.client.test.tests.BaseTest;
import com.microsoft.sqlserver.jdbc.SQLServerDataSource;
import com.microsoft.sqlserver.jdbc.SQLServerException;
import org.json.JSONArray;
import org.json.JSONObject;

import java.sql.*;

import static com.kaltura.client.test.Properties.*;

public class DBUtils extends BaseUtils {

    private static SQLServerDataSource dataSource;
    private static Connection conn;
    private static Statement stam;
    private static ResultSet rs;

    //selects
    private static final String ACTIVATION_TOKEN_SELECT = "SELECT [ACTIVATION_TOKEN] FROM [Users].[dbo].[users] WHERE [USERNAME] = '%S'";
    private static final String EPG_CHANNEL_ID_SELECT = "SELECT [ID] FROM [TVinci].[dbo].[epg_channels] WHERE [GROUP_ID] = %d AND [NAME] = '%S'";
    private static final String USER_BY_ROLE_SELECT = "select top(1) u.username, u.[password]\n" +
                                                        "from [Users].[dbo].[users] u with(nolock)\n" +
                                                        "join [Users].[dbo].[users_roles] ur with(nolock) on (u.id=ur.[user_id])\n" +
                                                        "join [TVinci].[dbo].[roles] r with(nolock) on (r.id=ur.role_id)\n" +
                                                        "where r.[NAME]='%S' and u.is_active=1 and u.[status]=1 and u.group_id=%d";
    private static final String DISCOUNT_BY_PERCENT_AND_CURRENCY = "select TOP (1) *\n" +
                                                        "from [Pricing].[dbo].[discount_codes] dc with(nolock)\n" +
                                                        "join [Pricing].[dbo].[lu_currency] lc with(nolock) on (dc.currency_cd=lc.id)\n" +
                                                        "where lc.code3='%S'\n" + // CURRENCY
                                                        "and dc.discount_percent=%d\n" + // percent amount
                                                        "and dc.group_id=%d\n" + // group
                                                        "and dc.[status]=1 and dc.is_active=1";

    //TODO - change existing methods to work with the new convertToJSON method

    // Return json array from DB
    public static JSONArray convertToJSON(String query) throws Exception {
        openConnection();
        JSONArray jsonArray = new JSONArray();
        rs = stam.executeQuery(query);
        ResultSetMetaData rsmd = rs.getMetaData();

        if (rs != null && rs.isBeforeFirst()) {
            while (rs.next()) {
                int numColumns = rsmd.getColumnCount();
                JSONObject obj = new JSONObject();

                for (int i = 1; i < numColumns + 1; i++) {
                    String column_name = rsmd.getColumnName(i).toLowerCase();

                    if (rsmd.getColumnType(i) == java.sql.Types.ARRAY) {
                        obj.put(column_name, rs.getArray(column_name));
                    } else if (rsmd.getColumnType(i) == java.sql.Types.BIGINT) {
                        obj.put(column_name, rs.getInt(column_name));
                    } else if (rsmd.getColumnType(i) == java.sql.Types.BOOLEAN) {
                        obj.put(column_name, rs.getBoolean(column_name));
                    } else if (rsmd.getColumnType(i) == java.sql.Types.BLOB) {
                        obj.put(column_name, rs.getBlob(column_name));
                    } else if (rsmd.getColumnType(i) == java.sql.Types.DOUBLE) {
                        obj.put(column_name, rs.getDouble(column_name));
                    } else if (rsmd.getColumnType(i) == java.sql.Types.FLOAT) {
                        obj.put(column_name, rs.getFloat(column_name));
                    } else if (rsmd.getColumnType(i) == java.sql.Types.INTEGER) {
                        obj.put(column_name, rs.getInt(column_name));
                    } else if (rsmd.getColumnType(i) == java.sql.Types.NVARCHAR) {
                        obj.put(column_name, rs.getNString(column_name));
                    } else if (rsmd.getColumnType(i) == java.sql.Types.VARCHAR) {
                        obj.put(column_name, rs.getString(column_name));
                    } else if (rsmd.getColumnType(i) == java.sql.Types.TINYINT) {
                        obj.put(column_name, rs.getInt(column_name));
                    } else if (rsmd.getColumnType(i) == java.sql.Types.SMALLINT) {
                        obj.put(column_name, rs.getInt(column_name));
                    } else if (rsmd.getColumnType(i) == java.sql.Types.DATE) {
                        obj.put(column_name, rs.getDate(column_name));
                    } else if (rsmd.getColumnType(i) == java.sql.Types.TIMESTAMP) {
                        obj.put(column_name, rs.getTimestamp(column_name));
                    } else {
                        obj.put(column_name, rs.getObject(column_name));
                    }
                }
                jsonArray.put(obj);
            }
            closeConnection();
            return jsonArray;

        } else {
            Logger.getLogger(DBUtils.class).error("No result found");
            closeConnection();
            return null;
        }

    }

    public static String getDiscountByPercentAndCurrency(String currency, int percent) {
        openConnection();
        try {
            rs = stam.executeQuery(String.format(DISCOUNT_BY_PERCENT_AND_CURRENCY, currency, percent, BaseTest.partnerId));
        } catch (SQLException e) {
            e.printStackTrace();
        }
        try {
            rs.next();
        } catch (SQLException e) {
            e.printStackTrace();
            Logger.getLogger(DBUtils.class).error("No data about discount with currency " + currency + "and percent " + percent + " in account " + BaseTest.partnerId);
        }
        String code = "";
        try {
            code = rs.getString("code");
            if ("".equals(code)) {
                throw new SQLException();
            }
        } catch (SQLException e) {
            e.printStackTrace();
            Logger.getLogger(DBUtils.class).error("code can't be null");
        }
        closeConnection();
        return code;
    }

    public static String getUserDataByRole(String userRole) {
        openConnection();
        try {
            rs = stam.executeQuery(String.format(USER_BY_ROLE_SELECT, userRole, BaseTest.partnerId));
        } catch (SQLException e) {
            e.printStackTrace();
        }
        try {
            rs.next();
        } catch (SQLException e) {
            e.printStackTrace();
            Logger.getLogger(DBUtils.class).error("No data about user with role " + userRole + " in account " + BaseTest.partnerId);
        }
        String userdData = "";
        try {
            userdData = rs.getString("username") + ":" + rs.getString("password");
        } catch (SQLException e) {
            e.printStackTrace();
            Logger.getLogger(DBUtils.class).error("username/password can't be null");
        }
        closeConnection();
        return userdData;
    }

    public static String getActivationToken(String username) {
        openConnection();
        try {
            rs = stam.executeQuery(String.format(ACTIVATION_TOKEN_SELECT, username));
        } catch (SQLException e) {
            e.printStackTrace();
        }
        try {
            rs.next();
        } catch (SQLException e) {
            e.printStackTrace();
        }
        String activationToken = null;
        try {
            activationToken = rs.getString("ACTIVATION_TOKEN");
        } catch (SQLException e) {
            e.printStackTrace();
            Logger.getLogger(DBUtils.class).error("activationToken can't be null");
        }
        closeConnection();
        return activationToken;
    }

    public static int getEpgChannelId(String channelName) {
        openConnection();
        try {
            rs = stam.executeQuery(String.format(EPG_CHANNEL_ID_SELECT, BaseTest.partnerId + 1, channelName));
        } catch (SQLException e) {
            e.printStackTrace();
        }
        try {
            rs.next();
        } catch (SQLException e) {
            e.printStackTrace();
        }
        int epgChannelId = -1;
        try {
            epgChannelId = rs.getInt("ID");
        } catch (SQLException e) {
            e.printStackTrace();
            Logger.getLogger(DBUtils.class).error("epgChannelId can't be null");
        }
        closeConnection();
        return epgChannelId;
    }

    private static void openConnection() {
        dataSource = new SQLServerDataSource();
        dataSource.setUser(getProperty(DB_USER));
        dataSource.setPassword(getProperty(DB_PASSWORD));
        dataSource.setServerName(getProperty(DB_URL));
        dataSource.setApplicationIntent("ReadOnly");
        dataSource.setMultiSubnetFailover(true);

        try {
            conn = dataSource.getConnection();
        } catch (SQLServerException e) {
            e.printStackTrace();
        }
        try {
            stam = conn.createStatement();
        } catch (SQLException e) {
            e.printStackTrace();
        }
    }

    private static void closeConnection() {
        try {
            rs.close();
        } catch (SQLException e) {
            e.printStackTrace();
        }
        try {
            stam.close();
        } catch (SQLException e) {
            e.printStackTrace();
        }
        try {
            conn.close();
        } catch (SQLException e) {
            e.printStackTrace();
        }
    }
}
