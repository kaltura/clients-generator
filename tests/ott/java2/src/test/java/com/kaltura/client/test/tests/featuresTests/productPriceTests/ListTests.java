package com.kaltura.client.test.tests.featuresTests.productPriceTests;

import com.kaltura.client.Client;
import com.kaltura.client.test.tests.BaseTest;
import org.testng.annotations.BeforeClass;

public class ListTests extends BaseTest {

    private Client client;

    @BeforeClass
    public void beforeClass() {
        client = getClient(getOperatorKs());

        /*Ppv ppv = IngestUtils.ingestPPV(INGEST_ACTION_INSERT, true, "My ingest PPV", getProperty(FIFTY_PERCENTS_ILS_DISCOUNT_NAME),
                Double.valueOf(getProperty(PRICE_CODE_AMOUNT_4_99)), CURRENCY_EUR, getProperty(DEFAULT_USAGE_MODULE_4_INGEST_PPV), false, false,
                getProperty(DEFAULT_PRODUCT_CODE), getProperty(WEB_FILE_TYPE), getProperty(MOBILE_FILE_TYPE));*/

        /*Response<ListResponse<Asset>> ingestedProgrammes = IngestUtils.ingestEPG("Shmulik_Series_1", Optional.of(2), Optional.empty(), Optional.of(30),
                Optional.of("minutes"), Optional.empty(), Optional.of(1), Optional.empty(), Optional.empty(), Optional.empty());
        System.out.println("ID:" + ingestedProgrammes.results.getObjects().get(0).getId());*/
    }
}
