package com.kaltura.client.test.utils;

import com.kaltura.client.Logger;
import com.kaltura.client.enums.AssetOrderBy;
import com.kaltura.client.enums.AssetReferenceType;
import com.kaltura.client.test.servicesImpl.AssetServiceImpl;
import com.kaltura.client.types.*;
import com.kaltura.client.utils.response.base.Response;
import io.restassured.RestAssured;
import java.text.ParseException;
import java.text.SimpleDateFormat;
import java.util.*;
import java.util.concurrent.Callable;
import java.util.concurrent.TimeUnit;
import static com.kaltura.client.test.IngestConstants.*;
import static com.kaltura.client.test.Properties.*;
import static com.kaltura.client.test.tests.BaseTest.*;
import static io.restassured.path.xml.XmlPath.from;
import static org.awaitility.Awaitility.await;

public class IngestUtils extends BaseUtils {

    // INGEST EPG PARAMS
    // TODO: think about ENUMS if we really need it here? should we create new ENUM class for it? where?
    public static final String DURATION_PERIOD_DAYS = "days";
    public static final String DURATION_PERIOD_HOURS = "hours";
    public static final String DURATION_PERIOD_MINUTES = "minutes";
    public static final String DURATION_PERIOD_SECONDS = "seconds";

    // default values
    public static final int EPG_DEFAULT_COUNT_OF_SEASONS = 1;
    public static final int EPG_DEFAULT_COUNT_OF_PROGRAMMES = 2;
    public static final int EPG_DEFAULT_PROGRAM_DURATION = 30;
    public static final String EPG_DEFAULT_PROGRAM_DURATION_PERIOD_NAME = DURATION_PERIOD_MINUTES;

    private static List<String> durationPeriodNames = new ArrayList<>();
    static {
        durationPeriodNames.add(DURATION_PERIOD_DAYS);
        durationPeriodNames.add(DURATION_PERIOD_HOURS);
        durationPeriodNames.add(DURATION_PERIOD_MINUTES);
        durationPeriodNames.add(DURATION_PERIOD_SECONDS);
    }

    private static String titleOfIngestedItem = "";

    // INGEST MPP PARAMS
    private static boolean MPP_DEFAULT_IS_ACTIVE_VALUE = true;
    private static String MPP_DEFAULT_DESCRIPTION_VALUE = "Ingest MPP description";
    private static String MPP_DEFAULT_START_DATE_VALUE = "20/03/2016 00:00:00";
    private static String MPP_DEFAULT_END_DATE_VALUE = "20/03/2099 00:00:00";
    private static boolean MPP_DEFAULT_IS_RENEWABLE_VALUE = false;
    private static int MPP_DEFAULT_GRACE_PERIOD_VALUE = 0;

    // TODO: THIS VALUES RELATED TO OUR ENV ONLY discuss with Alon
    private static String MPP_DEFAULT_COUPON_GROUP_VALUE =
            "<coupon_group_id>\n" +
                    "<start_date>01/05/2017 00:00:00</start_date>\n" +
                    "<end_date>31/12/2099 23:59:59</end_date>\n" +
                    "<code>100% unlimited</code>\n" +
                    "</coupon_group_id>\n" +
                    "<coupon_group_id>\n" +
                    "<start_date>01/05/2017 00:00:00</start_date>\n" +
                    "<end_date>31/05/2017 23:59:59</end_date>\n" +
                    "<code>Expired coupon group 1</code>\n" +
                    "</coupon_group_id>";
    private static String MPP_DEFAULT_PRODUCT_CODES_VALUE =
            "<product_code>\n" +
                    "<code>ProductCode1</code>\n" +
                    "<verification_payment_gateway>Google</verification_payment_gateway>\n" +
                    "</product_code>\n" +
                    "<product_code>\n" +
                    "<code>ProductCode2</code>\n" +
                    "<verification_payment_gateway>Apple</verification_payment_gateway>\n" +
                    "</product_code>";

    // INGEST PP PARAMS
    private static boolean PP_DEFAULT_IS_ACTIVE_VALUE = true;
    private static int PP_DEFAULT_MAX_VIEWS_VALUE = 0;
    private static boolean PP_DEFAULT_IS_RENEWABLE_VALUE = false;
    private static int PP_DEFAULT_RECURRING_PERIODS_VALUE = 1;

    // ingest new EPG (Programmes) // TODO: complete one-by-one needed fields to cover util ingest_epg from old project
    public static Response<ListResponse<Asset>> ingestEPG(String epgChannelName, Optional<Integer> programCount, Optional<String> firstProgramStartDate,
                                                          Optional<Integer> programDuration, Optional<String> programDurationPeriodName,
                                                          Optional<Boolean> isCridUnique4AllPrograms, Optional<Integer> seasonCount,
                                                          Optional<String> coguid, Optional<String> crid, Optional<String> seriesId) {

        int programCountValue = programCount.orElse(EPG_DEFAULT_COUNT_OF_PROGRAMMES);
        if (programCountValue <= 0) {
            Logger.getLogger(IngestUtils.class).error("Invalid programCount value " + programCount.get() +
                    ". Should be bigger than 0");
            return null;
        }
        int seasonCountValue = seasonCount.orElse(EPG_DEFAULT_COUNT_OF_SEASONS);

        String datePattern = "MM/yy/dd HH:mm:ss";
        SimpleDateFormat dateFormat = new SimpleDateFormat(datePattern);
        String firstProgramStartDateValue = firstProgramStartDate.orElse(getCurrentDataInFormat(datePattern));
        Calendar startDate = Calendar.getInstance();
        try {
            startDate.setTime(dateFormat.parse(firstProgramStartDateValue));
        } catch (ParseException e) {
            e.printStackTrace();
        }

        int programDurationValue = programDuration.orElse(EPG_DEFAULT_PROGRAM_DURATION);
        if (programDurationValue <= 0) {
            Logger.getLogger(IngestUtils.class).error("Invalid programDuration value " + programDuration.get() +
                    ". Should be bigger than 0");
            return null;
        }
        String programDurationPeriodNameValue = programDurationPeriodName.orElse(EPG_DEFAULT_PROGRAM_DURATION_PERIOD_NAME);
        if (!durationPeriodNames.contains(programDurationPeriodNameValue)) {
            Logger.getLogger(IngestUtils.class).error("Invalid programDurationPeriodName value " + programDurationPeriodName.get() +
                    ". Should be one from " + durationPeriodNames);
            return null;
        }
        boolean isCridUnique4AllProgramsValue = isCridUnique4AllPrograms.orElse(true);

        String coguidValue = coguid.orElseGet(() -> getCurrentDataInFormat("yyMMddHHmmssSS"));
        String cridValue = crid.orElse(coguidValue);
        String seriesIdValue = seriesId.orElse(coguidValue);
        int seasonId = 1;
        Date endDate;
        String output = "";
        String oneProgrammOutput = "";
        SimpleDateFormat df2 = new SimpleDateFormat("yyyyMMddHHmmss");
        Date now = Calendar.getInstance().getTime();
        while (seasonId <= seasonCountValue) {
            int idx = 1;
            while(idx <= programCountValue) {
                endDate = loadEndDate(startDate.getTime(), programDurationValue, programDurationPeriodNameValue);
                oneProgrammOutput = getProgrammeXML(idx, df2.format(startDate.getTime()), df2.format(endDate),
                        epgChannelName, coguidValue, cridValue, "Program", df2.format(now),
                        seriesIdValue, String.valueOf(seasonId), isCridUnique4AllProgramsValue);
                startDate.setTime(endDate);
                output = output + oneProgrammOutput;
                idx = idx + 1;
            }
            seasonId = seasonId + 1;
        }
        String epgChannelIngestXml = getChannelXML(partnerId, epgChannelName, output);

        String url = getProperty(INGEST_BASE_URL) + "/Ingest_" + getProperty(API_VERSION) + "/Service.svc?wsdl";
        HashMap headerMap = new HashMap<>();
        headerMap.put("Content-Type", "text/xml;charset=UTF-8");
        headerMap.put("SOAPAction", "\"http://tempuri.org/IService/IngestKalturaEpg\"");
        String reqBody = "<s:Envelope xmlns:s=\"http://schemas.xmlsoap.org/soap/envelope/\">\n" +
                "   <s:Header/>\n" +
                "   <s:Body>\n" +
                "      <IngestKalturaEpg xmlns=\"http://tempuri.org/\">" +
                "           <request xmlns:i=\"http://www.w3.org/2001/XMLSchema-instance\">" +
                "           <userName xmlns=\"\">" + getProperty(INGEST_USER_USERNAME) + "</userName><passWord xmlns=\"\">" + getProperty(INGEST_USER_PASSWORD) + "</passWord><data xmlns=\"\">" +
                epgChannelIngestXml + "\n" +
                "           </data>\n" +
                "           </request>\n" +
                "           </IngestKalturaEpg>\n" +
                "   </s:Body>\n" +
                "</s:Envelope>";
        io.restassured.response.Response resp = RestAssured.given()
                .log().all()
                .headers(headerMap)
                .body(reqBody)
                .post(url);

        Logger.getLogger(IngestUtils.class).debug(reqBody);
        Logger.getLogger(IngestUtils.class).debug(resp.asString());
        int epgChannelId = DBUtils.getEpgChannelId(epgChannelName);
        // TODO: create method getting epoch value from String and pattern
        long epoch = 0L;
        try {
            Date firstProgramStartDateAsDate = dateFormat.parse(firstProgramStartDateValue);
            epoch = firstProgramStartDateAsDate.getTime()/1000; // 1000 milliseconds in 1 second
        } catch (ParseException e) {
            e.printStackTrace();
        }
        String firstProgramStartDateEpoch = String.valueOf(epoch);

        SearchAssetFilter assetFilter = new SearchAssetFilter();
        assetFilter.setOrderBy(AssetOrderBy.START_DATE_ASC.getValue());
        assetFilter.setKSql("(and epg_channel_id='" + epgChannelId + "' start_date >= '" + firstProgramStartDateEpoch + "' Series_ID='" + seriesIdValue + "' end_date >= '" + firstProgramStartDateEpoch + "')");
        int delayBetweenRetriesInSeconds = 3;
        int maxTimeExpectingValidResponseInSeconds = 60;
        await().pollInterval(delayBetweenRetriesInSeconds, TimeUnit.SECONDS).atMost(maxTimeExpectingValidResponseInSeconds, TimeUnit.SECONDS)
                .until(isDataReturned(assetFilter, programCountValue*seasonCountValue));

        Response<ListResponse<Asset>> ingestedProgrammes = AssetServiceImpl.list(getClient(getAnonymousKs()), assetFilter, null);
        // TODO: complete Asset.json at least for programs
        return ingestedProgrammes;
    }

    private static Callable<Boolean> isDataReturned(SearchAssetFilter assetFilter, int totalCount) {
        return () -> (AssetServiceImpl.list(getClient(getAnonymousKs()), assetFilter, null).error == null &&
                AssetServiceImpl.list(getClient(getAnonymousKs()), assetFilter, null).results.getTotalCount() == totalCount);
    }

    private static String getChannelXML(int partnerId, String epgChannelName, String programsXml) {
        return "<![CDATA[" +
                "<?xml version=\"1.0\" encoding=\"UTF-8\"?> " +
                "<EpgChannels xmlns=\"http://tempuri.org/xmltv\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\" xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" " +
                "parent-group-id=\"" + String.valueOf(partnerId) + "\" group-id=\"" + String.valueOf(partnerId + 1) + "\" main-lang=\"eng\" updater-id=\"587\">" +
                "<channel id=\"" + epgChannelName + "\"/>" +
                programsXml +
                "</EpgChannels>" +
                "]]>";
    }

    // generate XML for ingest of 1 program
    private static String getProgrammeXML(int idx, String startDate, String endDate, String channel, String coguid, String crid,
                                          String programNamePrefix, String currentDate, String seriesId, String seasonNumber,
                                          boolean isCridUnique4AllPrograms) {
        String name = programNamePrefix + "_" + startDate + "_ser" + seriesId + "_seas" + seasonNumber + "_e" + idx;
        if ("".equals(titleOfIngestedItem)) {
            titleOfIngestedItem = name;
        }
        // TODO: complete to cover util from old project completely
        String CRID = "<crid>" + crid + "_" + idx + "</crid>";
        if (isCridUnique4AllPrograms) {
            CRID = "<crid>" + crid + "_" + seasonNumber + "_" + idx + "</crid>";
        }
        String output =
                "<programme start=\"" + startDate + "\" stop=\"" + endDate + "\" channel=\"" + channel + "\" external_id=\"" + coguid + "_" + seasonNumber + "_" + idx + "\">" +
                        "<title lang=\"eng\">" + name + "</title>" +
                        CRID +
                        "<desc lang=\"eng\">" + startDate + " until " + endDate + "</desc>" +
                        "<date>" + currentDate + "</date>" +
                        "<language lang=\"eng\">eng</language>" +
                        //"<icon ratio=\"" + ratio + "\" src=\"" + thumb + "\"/>" +
                        //"<enable-cdvr>" + enableCDVR + "</enable-cdvr>" +
                        //"<enable-catch-up>" + enableCatchUp + "</enable-catch-up>"+
                        //"<enable-start-over>" + enableStartOver + "</enable-start-over>" +
                        //"<enable-trick-play>" + enableTrickPlay + "</enable-trick-play>" +
                        //"<metas>" +
                        //"<MetaType>" + metaName + "</MetaType>" +
                        //"<MetaValues lang=\"eng\">" + metaValue + "</MetaValues>" +
                        //"</metas>" +
                        "<metas>" +
                        // TODO: that meta should be added into property file
                        "<MetaType>season_num</MetaType>" +
                        "<MetaValues lang=\"eng\">" + seasonNumber + "</MetaValues>" +
                        "</metas>" +
                        "<metas>" +
                        // TODO: that meta should be added into property file
                        "<MetaType>Series_ID</MetaType>" +
                        "<MetaValues lang=\"eng\">" + seriesId + "</MetaValues>" +
                        "</metas>" +
                        "<metas>" +
                        // TODO: that meta should be added into property file
                        "<MetaType>Episode number</MetaType>" +
                        "<MetaValues lang=\"eng\">" + idx + "</MetaValues>" +
                        "</metas>" +
            /*			"<tags>" +
                            "<TagType>Season</TagType>" +
                            "<TagValues lang=\"eng\">" + seasonNumber + "</TagValues>" +
                        "</tags>" +
                        "<tags>" +
                            "<TagType>Episode</TagType>" +
                            "<TagValues lang=\"eng\">" + id + "</TagValues>" +
                        "</tags>" +
            */
                        //"<metas>" +
                        //"<MetaType>Country</MetaType>" +
                        //"<MetaValues lang=\"eng\">Israel</MetaValues>" +
                        //"</metas>" +
                        //"<metas>" +
                        //"<MetaType>YEAR</MetaType>" +
                        //"<MetaValues lang=\"eng\">2015</MetaValues>" +
                        //"</metas>" +
                        //"<tags>" +
                        //"<TagType>Genre</TagType>" +
                        //"<TagValues lang=\"eng\">" + genre + "</TagValues>" +
                        //"</tags>" +
                        //"<tags>" +
                        //"<TagType>Actors</TagType>" +
                        //"<TagValues lang=\"eng\">Shay</TagValues>" +
                        //"<TagValues lang=\"eng\">Ortal</TagValues>" +
                        //"</tags>" +
                        //"<tags>" +
                        //"<TagType>" + tagName + "</TagType>" +
                        //"<TagValues lang=\"eng\">" + tagValue + "</TagValues>" +
                        //"</tags>" +
                        //"<tags>" +
                        //"<TagType>" + parentalFieldName + "</TagType>" +
                        //"<TagValues lang=\"eng\">" + parentalValue + "</TagValues>" +
                        //"</tags>" +
                        //"<tags>" +
                        //"<TagType>" + parentalFieldName + "</TagType>" +
                        //"<TagValues lang=\"eng\">" + parentalValue2 + "</TagValues>" +
                        //"</tags>" +
                        "</programme>";
        return output;
    }

    private static Date loadEndDate(Date startDate, int durationValue, String periodName) {
        Calendar calendar = Calendar.getInstance();
        calendar.setTime(startDate);
        switch (periodName) {
            case DURATION_PERIOD_DAYS:
                calendar.add(Calendar.DATE, durationValue);
                break;
            case DURATION_PERIOD_HOURS:
                calendar.add(Calendar.HOUR, durationValue);
                break;
            case DURATION_PERIOD_MINUTES:
                calendar.add(Calendar.MINUTE, durationValue);
                break;
            case DURATION_PERIOD_SECONDS:
                calendar.add(Calendar.SECOND, durationValue);
        }
        return calendar.getTime();
    }

    /**
     * IMPORTANT: please delete inserted by that method items
     *
     * @param action - can be "insert" or "delete" ("update" looks like broken)
     * @param mppCode - should have value in case "action" is "delete"
     * @param isActive
     * @param title
     * @param description
     * @param startDate
     * @param endDate
     * @param internalDiscount
     * @param productCode
     * @param isRenewable
     * @param gracePeriodMinute
     * @param pricePlanCode1
     * @param pricePlanCode2
     * @param channel1
     * @param channel2
     * @param fileType1
     * @param fileType2
     * @param couponGroup
     * @param productCodes
     * @return MPP data
     *
     *      !!!Only created by that method MPP can be deleted!!!
     *
     *      to delete existed MPP use corresponded action and value mpp.getName() as "mppCode"
     *      (where mpp is a variable that contains mpp data).
     *
     *
     *      don't forget after deletion of mpp delete also price plan using by deleted mpp (if it was created by ingestPP method)
     */
    // ingest new MPP
    public static Subscription ingestMPP(Optional<String> action, Optional<String> mppCode, Optional<Boolean> isActive,
                                         Optional<String> title, Optional<String> description, Optional<String> startDate,
                                         Optional<String> endDate, Optional<String> internalDiscount,
                                         Optional<String> productCode, Optional<Boolean> isRenewable,
                                         Optional<Integer> gracePeriodMinute, Optional<String> pricePlanCode1,
                                         Optional<String> pricePlanCode2, Optional<String> channel1,
                                         Optional<String> channel2, Optional<String> fileType1,
                                         Optional<String> fileType2, Optional<String> couponGroup, Optional<String> productCodes) {
        String mppCodeValue = mppCode.orElse(getRandomValue("MPP_", 9999999999L));
        String actionValue = action.orElse(INGEST_ACTION_INSERT);
        boolean isActiveValue = isActive.orElse(MPP_DEFAULT_IS_ACTIVE_VALUE);
        String titleValue = INGEST_ACTION_INSERT.equals(actionValue) ? mppCodeValue : title.orElse(mppCodeValue);
        String descriptionValue = description.orElse(MPP_DEFAULT_DESCRIPTION_VALUE);
        String startDateValue = startDate.orElse(MPP_DEFAULT_START_DATE_VALUE);
        String endDateValue = endDate.orElse(MPP_DEFAULT_END_DATE_VALUE);
        String internalDiscountValue = internalDiscount.orElse(getProperty(HUNDRED_PERCENTS_UKP_DISCOUNT_NAME));
        String productCodeValue = productCode.orElse("");
        boolean isRenewableValue = isRenewable.orElse(MPP_DEFAULT_IS_RENEWABLE_VALUE);
        int gracePeriodMinuteValue = gracePeriodMinute.orElse(MPP_DEFAULT_GRACE_PERIOD_VALUE);

        String pricePlanCode1Value = pricePlanCode1.orElse(getProperty(DEFAULT_USAGE_MODULE_4_INGEST_MPP));
        String pricePlanCode2Value = pricePlanCode2.orElse("");
        String channel1Value = channel1.orElse(getProperty(DEFAULT_CHANNEL));
        String channel2Value = channel2.orElse("");
        String fileType1Value = fileType1.orElse("");
        String fileType2Value = fileType2.orElse("");
        String couponGroupValue = couponGroup.orElse(MPP_DEFAULT_COUPON_GROUP_VALUE);
        String productCodesValue = productCodes.orElse(MPP_DEFAULT_PRODUCT_CODES_VALUE);


        String url = getProperty(INGEST_BASE_URL) + "/Ingest_" + getProperty(API_VERSION) + "/Service.svc?wsdl";
        HashMap headerMap = new HashMap<>();
        headerMap.put("Content-Type", "text/xml;charset=UTF-8");
        headerMap.put("SOAPAction", "http://tempuri.org/IService/IngestBusinessModules");

        String reqBody = "<soapenv:Envelope xmlns:soapenv=\"http://schemas.xmlsoap.org/soap/envelope/\" xmlns:tem=\"http://tempuri.org/\">\n" +
                "   <soapenv:Header/>\n" +
                "   <soapenv:Body>\n" +
                "      <tem:IngestBusinessModules><tem:username>" + getProperty(INGEST_BUSINESS_MODULE_USER_USERNAME) + "</tem:username><tem:password>" +
                getProperty(INGEST_BUSINESS_MODULE_USER_PASSWORD) + "</tem:password><tem:xml>" +
                "         <![CDATA[" + buildIngestMppXML(actionValue, mppCodeValue, isActiveValue, titleValue,
                descriptionValue, startDateValue, endDateValue, internalDiscountValue, productCodeValue,
                isRenewableValue, gracePeriodMinuteValue, pricePlanCode1Value, pricePlanCode2Value,
                channel1Value, channel2Value, fileType1Value, fileType2Value, couponGroupValue, productCodesValue) +
                "                 ]]></tem:xml></tem:IngestBusinessModules>\n" +
                "   </soapenv:Body>\n" +
                "</soapenv:Envelope>";

        io.restassured.response.Response resp = RestAssured.given()
                .log().all()
                .headers(headerMap)
                .body(reqBody)
                .post(url);

        //Logger.getLogger(IngestUtils.class).debug(reqBody);
        Logger.getLogger(IngestUtils.class).debug(resp.asString());

        String reportId = from(resp.asString()).get("Envelope.Body.IngestBusinessModulesResponse.IngestBusinessModulesResult.ReportId").toString();
        //Logger.getLogger(IngestUtils.class).debug("ReportId = " + reportId);

        url = getProperty(INGEST_REPORT_URL) + "/" + getProperty(PARTNER_ID) + "/" + reportId;
        resp = RestAssured.given()
                .log().all()
                .get(url);

        Logger.getLogger(IngestUtils.class).debug(resp.asString());
        //System.out.println(resp.asString().split(" = ")[1].replaceAll("\\.", ""));

        String id = resp.asString().split(" = ")[1].replaceAll("\\.", "");

        Subscription subscription = new Subscription();
        subscription.setId(id);
        subscription.setName(titleValue);
        subscription.setDescription(descriptionValue);
        // TODO: complete COMMENTED IF NEEDED
        //subscription.setStartDate();
        //subscription.setEndDate();
        //subscription.setDiscountModule();
        //subscription.setProductCodes();
        subscription.isRenewable(String.valueOf(isRenewableValue));
        subscription.setGracePeriodMinutes(gracePeriodMinuteValue);
        //subscription.setPricePlanIds();
        //subscription.setChannels();
        //subscription.setFileTypes();
        //subscription.setCouponsGroups();
        return subscription;
    }

    private static String buildIngestMppXML(String action, String mppCode, boolean isActive, String title, String description,
                                            String startDate, String endDate, String internalDiscount, String productCode,
                                            boolean isRenewable, int gracePeriodMinute, String pricePlanCode1,
                                            String pricePlanCode2, String channel1, String channel2, String fileType1,
                                            String fileType2, String couponGroup, String productCodes) {
        return "<ingest id=\"" + mppCode + "\">\n" +
                    "<multi_price_plans>\n" +
                        "<multi_price_plan code=\"" + mppCode + "\" action=\"" + action + "\" is_active=\"" + isActive + "\">\n" +
                            "<titles>\n" +
                                "<title lang=\"eng\">" + title + "</title>\n" +
                            "</titles>\n" +
                            "<descriptions>\n" +
                                "<description lang=\"eng\">" + description + "</description>" +
                            "</descriptions>\n" +
                            "<start_date>" + startDate + "</start_date>\n" +
                            "<end_date>" + endDate + "</end_date>\n" +
                            "<internal_discount>" + internalDiscount + "</internal_discount>\n" +
                            "<coupon_group/>\n" +
                            "<product_code>" + productCode  + "</product_code>\n" +
                            "<is_renewable>" + isRenewable + "</is_renewable>\n" +
                            "<priview_module/>\n" +
                            "<grace_period_minutes>" + gracePeriodMinute + "</grace_period_minutes>\n" +
                            "<price_plan_codes>\n" +
                                "<price_plan_code>" + pricePlanCode1 + "</price_plan_code>\n" +
                                "<price_plan_code>" + pricePlanCode2 + "</price_plan_code>\n" +
                            "</price_plan_codes>\n" +
                            "<channels>\n" +
                                "<channel>" + channel1 + "</channel>\n" +
                                "<channel>" + channel2 + "</channel>\n" +
                            "</channels>\n" +
                            "<file_types>\n" +
                                "<file_type>" + fileType1 + "</file_type>\n" +
                                "<file_type>" + fileType2 + "</file_type>\n" +
                            "</file_types>\n" +
                            "<order_number/>\n" +
                            "<subscription_coupon_group>" + couponGroup + "</subscription_coupon_group>\n" +
                            "<product_codes>" + productCodes + "</product_codes>\n" +
                        "</multi_price_plan>\n" +
                    "</multi_price_plans>\n" +
                "</ingest>\n";
    }

    // ingest new PP

    /**
     * IMPORTANT: please delete inserted by that method items
     *
     * @param action - can be "insert", "update" and "delete"
     * @param ppCode - should have value in case "action" one of {"update" and "delete"}
     * @param isActive
     * @param fullLifeCycle
     * @param viewLifeCycle
     * @param maxViews
     * @param price
     * @param currency
     * @param discount
     * @param isRenewable
     * @param recurringPeriods
     * @return PricePlan data
     *
     * to update or delete existed price plan use corresponded action and value pricePlan.getName() as "ppCode"
     * (where pricePlan is a variable that contains price plan data)
     *
     * !!!Only created by that method PP can be deleted/updated!!!
     */
    public static PricePlan ingestPP(Optional<String> action, Optional<String> ppCode, Optional<Boolean> isActive,
                                     Optional<String> fullLifeCycle, Optional<String> viewLifeCycle, Optional<Integer> maxViews,
                                     Optional<String> price, Optional<String> currency,
                                     Optional<String> discount, Optional<Boolean> isRenewable,
                                     Optional<Integer> recurringPeriods) {
        String ppCodeValue = ppCode.orElse(getRandomValue("AUTOPricePlan_", MAX_RANDOM_GENERATED_VALUE_4_INGEST));
        String actionValue = action.orElse(INGEST_ACTION_INSERT);
        boolean isActiveValue = isActive.orElse(PP_DEFAULT_IS_ACTIVE_VALUE);
        String fullLifeCycleValue = fullLifeCycle.orElse(FIVE_MINUTES_PERIOD);
        String viewLifeCycleValue = viewLifeCycle.orElse(FIVE_MINUTES_PERIOD);
        int maxViewsValue = maxViews.orElse(PP_DEFAULT_MAX_VIEWS_VALUE);
        String priceValue = price.orElse(getProperty(PRICE_CODE_AMOUNT));
        String currencyValue = currency.orElse(CURRENCY_EUR);
        String discountValue = discount.orElse(getProperty(HUNDRED_PERCENTS_UKP_DISCOUNT_NAME));
        boolean isRenewableValue = isRenewable.orElse(PP_DEFAULT_IS_RENEWABLE_VALUE);
        int recurringPeriodsValue = recurringPeriods.orElse(PP_DEFAULT_RECURRING_PERIODS_VALUE);

        String url = getProperty(INGEST_BASE_URL) + "/Ingest_" + getProperty(API_VERSION) + "/Service.svc?wsdl";
        HashMap headerMap = new HashMap<>();
        headerMap.put("Content-Type", "text/xml;charset=UTF-8");
        headerMap.put("SOAPAction", "http://tempuri.org/IService/IngestBusinessModules");

        String reqBody = "<soapenv:Envelope xmlns:soapenv=\"http://schemas.xmlsoap.org/soap/envelope/\" xmlns:tem=\"http://tempuri.org/\">\n" +
                "   <soapenv:Header/>\n" +
                "   <soapenv:Body>\n" +
                "      <tem:IngestBusinessModules><tem:username>" + getProperty(INGEST_BUSINESS_MODULE_USER_USERNAME) + "</tem:username><tem:password>" +
                getProperty(INGEST_BUSINESS_MODULE_USER_PASSWORD) + "</tem:password><tem:xml>" +
                "         <![CDATA[" + buildIngestPpXML(actionValue, ppCodeValue, isActiveValue, fullLifeCycleValue,
                viewLifeCycleValue, maxViewsValue, priceValue, currencyValue, discountValue,
                isRenewableValue, recurringPeriodsValue) +
                "                 ]]></tem:xml></tem:IngestBusinessModules>\n" +
                "   </soapenv:Body>\n" +
                "</soapenv:Envelope>";

        io.restassured.response.Response resp = RestAssured.given()
                .log().all()
                .headers(headerMap)
                .body(reqBody)
                .post(url);

        Logger.getLogger(IngestUtils.class).debug(reqBody);
        Logger.getLogger(IngestUtils.class).debug(resp.asString());

        String reportId = from(resp.asString()).get("Envelope.Body.IngestBusinessModulesResponse.IngestBusinessModulesResult.ReportId").toString();
        //Logger.getLogger(IngestUtils.class).debug("ReportId = " + reportId);

        url = getProperty(INGEST_REPORT_URL) + "/" + getProperty(PARTNER_ID) + "/" + reportId;
        resp = RestAssured.given()
                .log().all()
                .get(url);

        Logger.getLogger(IngestUtils.class).debug(resp.asString());
        //System.out.println(resp.asString().split(" = ")[1].replaceAll("\\.", ""));

        String id = resp.asString().split(" = ")[1].trim().replaceAll("\\.", "");
        //Logger.getLogger(IngestUtils.class).debug("ID: " + id);

        PricePlan pricePlan = new PricePlan();
        pricePlan.setId(Long.valueOf(id));
        pricePlan.setMaxViewsNumber(maxViewsValue);
        pricePlan.setIsRenewable(isRenewableValue);
        pricePlan.setRenewalsNumber(recurringPeriodsValue);
        pricePlan.setName(ppCodeValue);
        // TODO: complete COMMENTED IF NEEDED
        //pricePlan.setFullLifeCycle();
        //pricePlan.setViewLifeCycle();
        //pricePlan.setPriceDetailsId();
        //pricePlan.setDiscountId();
        return pricePlan;
    }

    private static String buildIngestPpXML(String action, String ppCode, boolean isActive, String fullLifeCycle,
                                           String viewLifeCycle, int maxViews, String price, String currency,
                                           String discount, boolean isRenewable, int recurringPeriods) {
        String id = "reportIngestPricePlan" + action.substring(0, 1).toUpperCase() + action.substring(1);
        return "<ingest id=\"" + id + "\">\n" +
                    "<price_plans>\n" +
                        "<price_plan code=\"" + ppCode + "\"  action=\"" + action + "\" is_active=\"" + isActive + "\">\n" +
                            "<full_life_cycle>" + fullLifeCycle + "</full_life_cycle>\n" +
                            "<view_life_cycle>" + viewLifeCycle + "</view_life_cycle>\n" +
                            "<max_views>" + maxViews + "</max_views>\n" +
                            "<price_code>\n" +
                                "<price>" + price + "</price>\n" +
                                "<currency>" + currency + "</currency>\n" +
                            "</price_code>\n" +
                            "<discount>" + discount + "</discount>\n" +
                            "<is_renewable>" + isRenewable + "</is_renewable>\n" +
                            "<recurring_periods>" + recurringPeriods + "</recurring_periods>\n" +
                        "</price_plan>\n" +
                    "</price_plans>\n" +
                "</ingest>\n";
    }

    /**
     * IMPORTANT: please delete inserted by that method items
     *
     * @param action - can be "insert", "update" and "delete"
     * @param ppvCode - should have value in case "action" one of {"update" and "delete"}
     * @param isActive
     * @param description
     * @param discount
     * @param price
     * @param currency
     * @param usageModule
     * @param isSubscriptionOnly
     * @param isFirstDeviceLimitation
     * @param productCode
     * @param firstFileType
     * @param secondFileType
     * @return PPV data
     *
     *  to update or delete existed ppv use corresponded action and value ppv.getName() as "ppvCode"
     *  (where ppv is a variable that contains ppv data)
     *
     *  !!!Only created by that method PPV can be deleted/update!!!
     */
    // ingest new PPV
    public static Ppv ingestPPV(Optional<String> action, Optional<String> ppvCode, Optional<Boolean> isActive, Optional<String> description,
                                Optional<String> discount, Optional<Double> price, Optional<String> currency,
                                Optional<String> usageModule, Optional<Boolean> isSubscriptionOnly,
                                Optional<Boolean> isFirstDeviceLimitation, Optional<String> productCode,
                                Optional<String> firstFileType, Optional<String> secondFileType) {
        String actionValue = action.orElse(INGEST_ACTION_INSERT);
        String ppvCodeValue = ppvCode.orElse(getRandomValue("PPV_", MAX_RANDOM_GENERATED_VALUE_4_INGEST));
        boolean isActiveValue = isActive.isPresent() ? isActive.get() : true;
        String descriptionValue = description.orElse("My ingest PPV");
        String discountValue = discount.orElseGet(() -> getProperty(FIFTY_PERCENTS_ILS_DISCOUNT_NAME));
        double priceValue = price.orElseGet(() -> Double.valueOf(getProperty(PRICE_CODE_AMOUNT)));
        String currencyValue = currency.orElse(CURRENCY_EUR);
        String usageModuleValue = usageModule.orElseGet(() -> getProperty(DEFAULT_USAGE_MODULE_4_INGEST_PPV));
        boolean isSubscriptionOnlyValue = isSubscriptionOnly.isPresent() ? isSubscriptionOnly.get() : false;
        boolean isFirstDeviceLimitationValue = isFirstDeviceLimitation.isPresent() ? isFirstDeviceLimitation.get() : false;
        String productCodeValue = productCode.orElseGet(() -> getProperty(DEFAULT_PRODUCT_CODE));
        String firstFileTypeValue = firstFileType.orElseGet(() -> getProperty(WEB_FILE_TYPE));
        String secondFileTypeValue = secondFileType.orElseGet(() -> getProperty(MOBILE_FILE_TYPE));

        String url = getProperty(INGEST_BASE_URL) + "/Ingest_" + getProperty(API_VERSION) + "/Service.svc?wsdl";
        HashMap headerMap = new HashMap<>();
        headerMap.put("Content-Type", "text/xml;charset=UTF-8");
        headerMap.put("SOAPAction", "http://tempuri.org/IService/IngestBusinessModules");

        String reqBody = "<soapenv:Envelope xmlns:soapenv='http://schemas.xmlsoap.org/soap/envelope/' xmlns:tem='http://tempuri.org/'>\n" +
                "   <soapenv:Header/>\n" +
                "   <soapenv:Body>\n" +
                "      <tem:IngestBusinessModules><tem:username>" + getProperty(INGEST_BUSINESS_MODULE_USER_USERNAME) + "</tem:username><tem:password>" +
                getProperty(INGEST_BUSINESS_MODULE_USER_PASSWORD) + "</tem:password><tem:xml>" +
                "         <![CDATA[" + buildIngestPpvXML(actionValue, ppvCodeValue, isActiveValue, descriptionValue,
                discountValue, priceValue, currencyValue, usageModuleValue, isSubscriptionOnlyValue,
                isFirstDeviceLimitationValue, productCodeValue, firstFileTypeValue, secondFileTypeValue) +
                "                 ]]></tem:xml></tem:IngestBusinessModules>\n" +
                "   </soapenv:Body>\n" +
                "</soapenv:Envelope>";

        io.restassured.response.Response resp = RestAssured.given()
                .log().all()
                .headers(headerMap)
                .body(reqBody)
                .post(url);

        Logger.getLogger(IngestUtils.class).debug(reqBody);
        Logger.getLogger(IngestUtils.class).debug(resp.asString());

        String reportId = from(resp.asString()).get("Envelope.Body.IngestBusinessModulesResponse.IngestBusinessModulesResult.ReportId").toString();
        //System.out.println("ReportId = " + reportId);

        url = getProperty(INGEST_REPORT_URL) + "/" + getProperty(PARTNER_ID) + "/" + reportId;
        resp = RestAssured.given()
                .log().all()
                .get(url);

        System.out.println(resp.asString());
        System.out.println(resp.asString().split(" = ")[1].replaceAll("\\.", ""));

        String id = resp.asString().split(" = ")[1].replaceAll("\\.", "");

        Ppv ppv = new Ppv();
        ppv.setId(id);
        List<TranslationToken> descriptions = new ArrayList<>();
        TranslationToken translationToken = new TranslationToken();
        translationToken.setValue(descriptionValue);
        descriptions.add(translationToken);
        ppv.setDescriptions(descriptions);
        PriceDetails priceDetails = new PriceDetails();
        Price priceObj = new Price();
        priceObj.setAmount(priceValue);
        priceObj.setCurrency(currencyValue);
        priceDetails.setPrice(priceObj);
        ppv.setPrice(priceDetails);
        UsageModule usageModuleObj = new UsageModule();
        usageModuleObj.setName(usageModuleValue);
        ppv.setUsageModule(usageModuleObj);
        ppv.setIsSubscriptionOnly(isSubscriptionOnlyValue);
        ppv.setFirstDeviceLimitation(isFirstDeviceLimitationValue);
        ppv.setProductCode(productCodeValue);
        ppv.setName(ppvCodeValue);

        return ppv;
    }

    private static String buildIngestPpvXML(String action, String ppvCode, boolean isActive, String description, String discount,
                                            double price, String currency, String usageModule, boolean isSubscriptionOnly,
                                            boolean isFirstDeviceLimitation, String productCode, String firstFileType,
                                            String secondFileType) {
        return "<ingest id=\"" + ppvCode + "\">\n" +
                "  <ppvs>\n" +
                "    <ppv code=\"" + ppvCode + "\" action=\"" + action + "\" is_active=\"" + isActive + "\">\n" +
                "      <descriptions>\n" +
                "        <description lang=\"eng\">" + description + "</description>\n" +
                "      </descriptions>\n" +
                "      <price_code>\n" +
                "        <price>" + price + "</price>\n" +
                "        <currency>" + currency + "</currency>\n" +
                "      </price_code>\n" +
                "      <usage_module>" + usageModule + "</usage_module>\n" +
                "      <discount>" + discount + "</discount>\n" +
                "      <coupon_group/>\n" +
                "      <subscription_only>" + isSubscriptionOnly + "</subscription_only>\n" +
                "      <first_device_limitation>" + isFirstDeviceLimitation + "</first_device_limitation>\n" +
                "      <product_code>" + productCode + "</product_code>\n" +
                "      <file_types>\n" +
                "        <file_type>" + firstFileType + "</file_type>\n" +
                "        <file_type>" + secondFileType + "</file_type>\n" +
                "      </file_types>\n" +
                "    </ppv>\n" +
                "  </ppvs>\n" +
                "</ingest>\n";
    }

    /**
     * IMPORTANT: please delete inserted by that method items
     *
     * @param action - can be "insert", "update" and "delete"
     * @param coguid - should have value in case "action" one of {"update" and "delete"}
     * @param isActive
     * @param name
     * @param thumbUrl
     * @param description
     * @param catalogStartDate
     * @param catalogEndDate
     * @param startDate
     * @param endDate
     * @param mediaType
     * @param ppvWebName
     * @param ppvMobileName
     * @return
     *
     * to update or delete existed VOD use corresponded action and value vod.getName() as "coguid"
     *      (where vod is a variable that contains VOD data)
     *
     *      !!!Only created by that method VOD can be deleted/update!!!
     */
    // ingest new VOD (Media) // TODO: complete one-by-one needed fields to cover util ingest_vod from old project
    public static MediaAsset ingestVOD(Optional<String> action, Optional<String> coguid, boolean isActive, Optional<String> name, Optional<String> thumbUrl, Optional<String> description,
                                       Optional<String> catalogStartDate, Optional<String> catalogEndDate, Optional<String> startDate, Optional<String> endDate,
                                       Optional<String> mediaType, Optional<String> ppvWebName, Optional<String> ppvMobileName) {
        String startEndDatePattern = "dd/MM/yyyy hh:mm:ss";
        String coguidDatePattern = "yyMMddHHmmssSS";
        String maxEndDateValue = "14/10/2099 17:00:00";
        String ppvModuleName = "Shai_Regression_PPV"; // TODO: update on any generated value
        int defaultDayOffset =-1;

        String actionValue = action.orElse(INGEST_ACTION_INSERT);
        String coguidValue = coguid.orElse(getCurrentDataInFormat(coguidDatePattern));
        String nameValue = INGEST_ACTION_INSERT.equals(actionValue) ? coguidValue : name.orElse(coguidValue);
        String thumbUrlValue = thumbUrl.orElse(INGEST_VOD_DEFAULT_THUMB);
        String descriptionValue = description.orElse("description of " + coguidValue);
        String catalogStartDateValue = catalogStartDate.orElse(getOffsetDateInFormat(defaultDayOffset, startEndDatePattern));
        String catalogEndDateValue = catalogEndDate.orElse(maxEndDateValue);
        String startDateValue = startDate.orElse(getOffsetDateInFormat(defaultDayOffset, startEndDatePattern));
        String endDateValue = endDate.orElse(maxEndDateValue);
        String mediaTypeValue = mediaType.orElse(MOVIE_MEDIA_TYPE);
        String ppvWebNameValue = ppvWebName.orElse(ppvModuleName);
        String ppvMobileNameValue = ppvMobileName.orElse(ppvModuleName);
        // TODO: check if ingest url is the same for all ingest actions
        String url = getProperty(INGEST_BASE_URL) + "/Ingest_" + getProperty(API_VERSION) + "/Service.svc?wsdl";
        HashMap headerMap = new HashMap<>();
        headerMap.put("Content-Type", "text/xml;charset=UTF-8");
        headerMap.put("SOAPAction", "\"http://tempuri.org/IService/IngestTvinciData\"");
        String reqBody = "<soapenv:Envelope xmlns:soapenv=\"http://schemas.xmlsoap.org/soap/envelope/\" xmlns:tem=\"http://tempuri.org/\">\n" +
                "   <soapenv:Header/>\n" +
                "   <soapenv:Body>\n" +
                "      <tem:IngestTvinciData><tem:request><userName>" + getProperty(INGEST_USER_USERNAME) + "</userName><passWord>" + getProperty(INGEST_USER_PASSWORD) + "</passWord><data>" +
                "         <![CDATA[" + buildIngestVodXml(actionValue, coguidValue, isActive, nameValue, thumbUrlValue, descriptionValue, catalogStartDateValue,
                catalogEndDateValue, startDateValue, endDateValue, mediaTypeValue, ppvWebNameValue, ppvMobileNameValue) +
                "                 ]]></data></tem:request></tem:IngestTvinciData>\n" +
                "   </soapenv:Body>\n" +
                "</soapenv:Envelope>";
        io.restassured.response.Response resp = RestAssured.given()
                .log().all()
                .headers(headerMap)
                .body(reqBody)
                .post(url);

        Logger.getLogger(IngestUtils.class).debug(reqBody);
        Logger.getLogger(IngestUtils.class).debug(resp.asString());

        String id;
        if (INGEST_ACTION_INSERT.equals(actionValue)) {
            id = from(resp.asString()).get("Envelope.Body.IngestTvinciDataResponse.IngestTvinciDataResult.AssetsStatus.IngestAssetStatus.InternalAssetId").toString();
        } else {
            id = from(resp.asString()).get("Envelope.Body.IngestTvinciDataResponse.IngestTvinciDataResult.tvmID").toString();
        }

        MediaAsset mediaAsset = new MediaAsset();
        mediaAsset.setName(nameValue);
        mediaAsset.setId(Long.valueOf(id));
        mediaAsset.setDescription(descriptionValue);
        //mediaAsset.setStartDate(startDate);
        //mediaAsset.setEndDate(endDate);

        int delayBetweenRetriesInSeconds = 3;
        int maxTimeExpectingValidResponseInSeconds = 60;
        await().pollInterval(delayBetweenRetriesInSeconds, TimeUnit.SECONDS).atMost(maxTimeExpectingValidResponseInSeconds, TimeUnit.SECONDS).until(isDataReturned(id, actionValue));
        Response<Asset> mediaAssetDetails = AssetServiceImpl.get(getClient(getAnonymousKs()), id, AssetReferenceType.MEDIA);
        if (!INGEST_ACTION_DELETE.equals(actionValue)) {
            mediaAsset.setMediaFiles(mediaAssetDetails.results.getMediaFiles());
        }

        // TODO: 4/15/2018 add log for ingest and index failures
        return mediaAsset;
    }

    private static Callable<Boolean> isDataReturned(String mediaId, String action) {
        if (INGEST_ACTION_DELETE.equals(action)) {
            return () -> AssetServiceImpl.get(getClient(getAnonymousKs()), mediaId, AssetReferenceType.MEDIA).error != null;
        } else {
            return () -> AssetServiceImpl.get(getClient(getAnonymousKs()), mediaId, AssetReferenceType.MEDIA).error == null;
        }
    }

    private static String buildIngestVodXml(String action, String coguid, boolean isActive, String name, String thumbUrl,
                                            String description, String catalogStartDate, String catalogEndDate,
                                            String startDate, String endDate, String mediaType, String ppvWebName,
                                            String ppvMobileName) {
        return "<feed>\n" +
                "  <export>\n" +
                "    <media co_guid=\"" + coguid + "\" entry_id=\"entry_" + coguid + "\" action=\"" + action + "\" is_active=\"" + isActive + "\" erase=\"false\">\n" +
                "      <basic>\n" +
                "        <name>\n" +
                "          <value lang=\"eng\">" + name + "</value>\n" +
                "        </name>\n" +
                "        <thumb url=\"" + thumbUrl + "\"/>\n" +
                "        <description>\n" +
                "          <value lang=\"eng\">" + description + "</value>\n" +
                "        </description>\n" +
                "        <dates>\n" +
                "          <catalog_start>" + catalogStartDate + "</catalog_start>\n" +
                "          <start>" + startDate + "</start>\n" +
                "          <catalog_end>" + catalogEndDate + "</catalog_end>\n" +
                "          <end>" + endDate + "</end>\n" +
                "        </dates>\n" +
                "        <pic_ratios>\n" +
                "          <ratio thumb=\"" + thumbUrl + "\" ratio=\"4:3\"/>\n" +
                "          <ratio thumb=\"" + thumbUrl + "\" ratio=\"16:9\"/>\n" +
                "        </pic_ratios>\n" +
                "        <media_type>" + mediaType + "</media_type>\n" +
                "        <rules>\n" +
                //"          <geo_block_rule>${#TestCase#i_geo_block_rule}</geo_block_rule>\n" +
                // TODO: check where to put that value (is it env-dependent?)
                "          <watch_per_rule>Parent Allowed</watch_per_rule>\n" +
                //"          <device_rule>${#TestCase#i_device_block_rule}</device_rule>\n" +
                "        </rules>\n" +
                "      </basic>\n" +
                "      <structure>\n" +
                //"        <strings>\n" +
                //"          <meta name=\"Synopsis\" ml_handling=\"unique\">\n" +
                //"            <value lang=\"eng\">syno pino sister</value>\n" +
                //"          </meta>\n" +
                //"          <meta name=\"${#TestCase#i_meta_name}\" ml_handling=\"unique\">\n" +
                //"            <value lang=\"eng\">${#TestCase#i_meta_value}</value>\n" +
                //"          </meta>\n" +
                //"        </strings>\n" +
                //"        <booleans/>\n" +
                //"        <doubles>\n" +
                //"          <meta name=\"${#TestCase#i_double_meta_name}\" ml_handling=\"unique\">${#TestCase#i_double_meta_value}</meta>\n" +
                //"        </doubles>\n" +
                //"        <dates>\n" +
                //"          <meta name=\"${#TestCase#i_date_meta_name}\" ml_handling=\"unique\">${#TestCase#i_date_meta_value}</meta>\n" +
                //"        </dates>\n" +
                //"        <metas>\n" +
                //"          <meta name=\"Country\" ml_handling=\"unique\">\n" +
                //"            <container>\n" +
                //"              <value lang=\"eng\">Costa Rica;Israel</value>\n" +
                //"            </container>\n" +
                //"          </meta>\n" +
                //"          <meta name=\"Genre\" ml_handling=\"unique\">\n" +
                //"            <container>\n" +
                //"              <value lang=\"eng\">GIH</value>\n" +
                //"            </container>\n" +
                //"            <container>\n" +
                //"              <value lang=\"eng\">ABC</value>\n" +
                //"            </container>\n" +
                //"            <container>\n" +
                //"              <value lang=\"eng\">DEF</value>\n" +
                //"            </container>\n" +
                //"          </meta>\n" +
                //"          <meta name=\"Series name\" ml_handling=\"unique\">\n" +
                //"            <container>\n" +
                //"              <value lang=\"eng\">Shay_Series</value>\n" +
                //"            </container>\n" +
                //"          </meta>\n" +
                //"          <meta name=\"Free\" ml_handling=\"unique\">\n" +
                //"            <container>\n" +
                //"              <value lang=\"eng\">${#TestCase#i_tag_free_value}</value>\n" +
                //"            </container>\n" +
                //"          </meta>\n" +
                //"          <meta name=\"${#TestCase#i_parental_field_name}\" ml_handling=\"unique\">\n" +
                //"            <container>\n" +
                //"              <value lang=\"eng\">${#TestCase#i_parental_value}</value>\n" +
                //"            </container>\n" +
                //"          </meta>\n" +
                //"          <meta name=\"${#TestCase#i_tag_name}\" ml_handling=\"unique\">\n" +
                //"            <container>\n" +
                //"              <value lang=\"eng\">${#TestCase#i_tag_value}</value>\n" +
                //"            </container>\n" +
                //"          </meta>\n" +
                //"        </metas>\n" +
                "      </structure>\n" +
                "      <files>\n" +
                "        <file type=\"" + getProperty(WEB_FILE_TYPE) + "\" assetDuration=\"1000\" quality=\"HIGH\" handling_type=\"CLIP\" cdn_name=\"Default CDN\" cdn_code=\"http://cdntesting.qa.mkaltura.com/p/231/sp/23100/playManifest/entryId/0_3ugsts44/format/hdnetworkmanifest/tags/mbr/protocol/http/f/a.a4m\" alt_cdn_code=\"http://alt_cdntesting.qa.mkaltura.com/p/231/sp/23100/playManifest/entryId/0_3ugsts44/format/hdnetworkmanifest/tags/mbr/protocol/http/f/a.a4m\" co_guid=\"web_" + coguid + "\" billing_type=\"Tvinci\" PPV_MODULE=\"" + ppvWebName + "\" product_code=\"productExampleCode\"/>\n" +
                "        <file type=\"" + getProperty(MOBILE_FILE_TYPE) + "\" assetDuration=\"1000\" quality=\"HIGH\" handling_type=\"CLIP\" cdn_name=\"Default CDN\" cdn_code=\"http://cdntesting.qa.mkaltura.com/p/231/sp/23100/playManifest/entryId/0_3ugsts44/format/applehttp/tags/ipadnew,ipad/protocol/http/f/a.m3u8\" alt_cdn_code=\"http://alt_cdntesting.qa.mkaltura.com/p/231/sp/23100/playManifest/entryId/0_3ugsts44/format/applehttp/tags/ipadnew,ipad/protocol/http/f/a.m3u8\" co_guid=\"ipad_" + coguid + "\" billing_type=\"Tvinci\" PPV_MODULE=\"" + ppvMobileName + "\" product_code=\"productExampleCode\"/>\n" +
                "      </files>\n" +
                "    </media>\n" +
                "  </export>\n" +
                "</feed>";
    }

    // Provide only media type (mandatory) and media name (Optional - if not provided will generate a name)
    public static MediaAsset ingestBasicVOD(Optional<String> name, String mediaType) {
        String coguidValue = getCurrentDataInFormat("yyMMddHHmmssSS");
        String nameValue = name.orElseGet(() -> MOVIE_MEDIA_TYPE + "_" + coguidValue);
        String thumbUrlValue = INGEST_VOD_DEFAULT_THUMB;
        String descriptionValue = "description of " + coguidValue;
        String catalogStartDateValue = getOffsetDateInFormat(-1, "dd/MM/yyyy hh:mm:ss");
        String catalogEndDateValue = "14/10/2099 17:00:00";
        String startDateValue = getOffsetDateInFormat(-1, "dd/MM/yyyy hh:mm:ss");
        String endDateValue = "14/10/2099 17:00:00";
        String mediaTypeValue = mediaType;
        String ppvWebNameValue = "Shai_Regression_PPV";
        String ppvMobileNameValue = "Shai_Regression_PPV"; // TODO: update on any generated value
        // TODO: check if ingest url is the same for all ingest actions
        String url = getProperty(INGEST_BASE_URL) + "/Ingest_" + getProperty(API_VERSION) + "/Service.svc?wsdl";
        HashMap headermap = new HashMap<>();
        headermap.put("Content-Type", "text/xml;charset=UTF-8");
        headermap.put("SOAPAction", "\"http://tempuri.org/IService/IngestTvinciData\"");
        String reqBody = "<soapenv:Envelope xmlns:soapenv=\"http://schemas.xmlsoap.org/soap/envelope/\" xmlns:tem=\"http://tempuri.org/\">\n" +
                "   <soapenv:Header/>\n" +
                "   <soapenv:Body>\n" +
                "      <tem:IngestTvinciData><tem:request><userName>" + getProperty(INGEST_USER_USERNAME) + "</userName><passWord>" + getProperty(INGEST_USER_PASSWORD) + "</passWord><data>" +
                "         <![CDATA[" + buildIngestVodXml(INGEST_ACTION_INSERT, coguidValue, true, nameValue, thumbUrlValue, descriptionValue, catalogStartDateValue,
                catalogEndDateValue, startDateValue, endDateValue, mediaTypeValue, ppvWebNameValue, ppvMobileNameValue) +
                "                 ]]></data></tem:request></tem:IngestTvinciData>\n" +
                "   </soapenv:Body>\n" +
                "</soapenv:Envelope>";
        io.restassured.response.Response resp = RestAssured.given()
                .log().all()
                .headers(headermap)
                .body(reqBody)
                .post(url);
        //System.out.println("RESPONSE: " + resp.asString());
        String id = from(resp.asString()).get("Envelope.Body.IngestTvinciDataResponse.IngestTvinciDataResult.AssetsStatus.IngestAssetStatus.InternalAssetId").toString();

        MediaAsset mediaAsset = new MediaAsset();
        mediaAsset.setName(nameValue);
        mediaAsset.setId(Long.valueOf(id));
        mediaAsset.setDescription(descriptionValue);
        //mediaAsset.setStartDate(startDate);
        //mediaAsset.setEndDate(endDate);

        await().pollInterval(3, TimeUnit.SECONDS).atMost(45, TimeUnit.SECONDS).until(isDataReturned(INGEST_ACTION_INSERT, id));
        Response<Asset> mediaAssetDetails = AssetServiceImpl.get(getClient(getAnonymousKs()), id, AssetReferenceType.MEDIA);
        mediaAsset.setMediaFiles(mediaAssetDetails.results.getMediaFiles());

        // TODO: 4/15/2018 add log for ingest and index failures
        return mediaAsset;
    }
}
