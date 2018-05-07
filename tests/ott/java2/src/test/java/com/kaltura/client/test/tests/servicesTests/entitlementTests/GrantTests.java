package com.kaltura.client.test.tests.servicesTests.entitlementTests;

import com.kaltura.client.Client;
import com.kaltura.client.enums.EntityReferenceBy;
import com.kaltura.client.enums.PurchaseStatus;
import com.kaltura.client.enums.TransactionHistoryOrderBy;
import com.kaltura.client.enums.TransactionType;
import com.kaltura.client.test.servicesImpl.*;
import com.kaltura.client.test.tests.BaseTest;
import com.kaltura.client.test.utils.AssetUtils;
import com.kaltura.client.test.utils.HouseholdUtils;
import com.kaltura.client.test.utils.OttUserUtils;
import com.kaltura.client.types.*;
import com.kaltura.client.utils.response.base.Response;
import io.qameta.allure.Issue;
import org.testng.annotations.BeforeClass;
import org.testng.annotations.Test;

import static com.kaltura.client.test.utils.BaseUtils.getAPIExceptionFromList;
import static org.assertj.core.api.Assertions.assertThat;

public class GrantTests extends BaseTest {

    // TODO: 4/12/2018 remove hardcoded subscription Id
    private final int subscriptionId = 369426;
    private final int ppvId = 30297;
    private final int assetId = 607368;

    private final int numberOfUsersInHousehold = 2;
    private final int numberOfDevicesInHousehold = 1;

    private Response<ListResponse<BillingTransaction>> billingTransactionListResponse;
    private int contentId;
    private Household testSharedHousehold;


    @BeforeClass
    private void grant_test_before_class() {
        contentId = AssetUtils.getAssetFileIds(String.valueOf(assetId)).get(0);
        testSharedHousehold = HouseholdUtils.createHouseHold(numberOfUsersInHousehold, numberOfDevicesInHousehold, false);
    }

    @Test(description = "entitlement/action/grant - grant subscription with history = true")
    private void grant_subscription_with_history() {
        Client client = getClient(getAdministratorKs());

        // set household
        Household household = HouseholdUtils.createHouseHold(numberOfUsersInHousehold, numberOfDevicesInHousehold, false);
        HouseholdUser masterUser = HouseholdUtils.getMasterUserFromHousehold(household);
        HouseholdUser user = HouseholdUtils.getRegularUsersListFromHouseHold(household).get(0);


        // grant subscription - history = true
        client.setUserId(Integer.valueOf(user.getUserId()));
        Response<Boolean> booleanResponse = EntitlementServiceImpl.grant(client, subscriptionId, TransactionType.SUBSCRIPTION, true, null);

        assertThat(booleanResponse.results.booleanValue()).isEqualTo(true);


        // verify other user from the household entitled to granted subscription
        client.setUserId(Integer.valueOf(masterUser.getUserId()));

        ProductPriceFilter productPriceFilter = new ProductPriceFilter();
        productPriceFilter.subscriptionIdIn(String.valueOf(subscriptionId));

        Response<ListResponse<ProductPrice>> productPriceListResponse = ProductPriceServiceImpl.list(client, productPriceFilter);
        ProductPrice productPrice = productPriceListResponse.results.getObjects().get(0);

        assertThat(productPriceListResponse.results.getTotalCount()).isEqualTo(1);
        assertThat(productPrice.getPrice().getAmount()).isEqualTo(0);
        assertThat(productPrice.getPurchaseStatus().getValue()).isEqualTo(PurchaseStatus.SUBSCRIPTION_PURCHASED.getValue());


        // check transaction return in transactionHistory by user
        client.setKs(OttUserUtils.getKs(Integer.parseInt(user.getUserId()), null));
        client.setUserId(null);

        BillingTransaction billingTransaction;
        TransactionHistoryFilter transactionHistoryfilter =  new TransactionHistoryFilter();
        transactionHistoryfilter.orderBy(TransactionHistoryOrderBy.CREATE_DATE_ASC.getValue());
        transactionHistoryfilter.entityReferenceEqual(EntityReferenceBy.USER.getValue());

        billingTransactionListResponse = TransactionHistoryServiceImpl.list(client, transactionHistoryfilter, null);
        assertThat(billingTransactionListResponse.results.getTotalCount()).isEqualTo(1);

        billingTransaction = billingTransactionListResponse.results.getObjects().get(0);
        assertThat(billingTransaction.getPurchasedItemCode()).isEqualTo(String.valueOf(subscriptionId));


        // check transaction return in transactionHistory by household
        transactionHistoryfilter.entityReferenceEqual(EntityReferenceBy.HOUSEHOLD.getValue());

        billingTransactionListResponse = TransactionHistoryServiceImpl.list(client, transactionHistoryfilter, null);
        assertThat(billingTransactionListResponse.results.getTotalCount()).isEqualTo(1);

        billingTransaction = billingTransactionListResponse.results.getObjects().get(0);
        assertThat(billingTransaction.getPurchasedItemCode()).isEqualTo(String.valueOf(subscriptionId));


        //delete household for cleanup
        HouseholdServiceImpl.delete(getClient(getAdministratorKs()), Math.toIntExact(household.getId()));
    }

    @Test(description = "entitlement/action/grant - grant subscription with history = false")
    private void grant_subscription_without_history() {
        Client client = getClient(getAdministratorKs());

        // set household
        Household household = HouseholdUtils.createHouseHold(numberOfUsersInHousehold, numberOfDevicesInHousehold, false);
        HouseholdUser user = HouseholdUtils.getRegularUsersListFromHouseHold(household).get(0);


        // grant subscription - history = true
        client.setUserId(Integer.valueOf(user.getUserId()));
        Response<Boolean> booleanResponse = EntitlementServiceImpl.grant(client, subscriptionId, TransactionType.SUBSCRIPTION, false, null);

        assertThat(booleanResponse.results.booleanValue()).isEqualTo(true);


        // check transaction not return in transactionHistory by user
        client.setKs(OttUserUtils.getKs(Integer.parseInt(user.getUserId()), null));
        client.setUserId(null);

        TransactionHistoryFilter transactionHistoryfilter = new TransactionHistoryFilter();
        transactionHistoryfilter.orderBy(TransactionHistoryOrderBy.CREATE_DATE_ASC.getValue());
        transactionHistoryfilter.entityReferenceEqual(EntityReferenceBy.USER.getValue());

        billingTransactionListResponse = TransactionHistoryServiceImpl.list(client, transactionHistoryfilter, null);
        assertThat(billingTransactionListResponse.results.getTotalCount()).isEqualTo(0);


        // check transaction not return in transactionHistory by household
        transactionHistoryfilter.entityReferenceEqual(EntityReferenceBy.HOUSEHOLD.getValue());

        billingTransactionListResponse = TransactionHistoryServiceImpl.list(client, transactionHistoryfilter, null);
        assertThat(billingTransactionListResponse.results.getTotalCount()).isEqualTo(0);


        //delete household for cleanup
        HouseholdServiceImpl.delete(getClient(getAdministratorKs()), Math.toIntExact(household.getId()));
    }

    @Test(description = "entitlement/action/grant - grant ppv with history = true")
    private void grant_ppv_with_history() {
        Client client = getClient(getAdministratorKs());

        // set household
        Household household = HouseholdUtils.createHouseHold(numberOfUsersInHousehold, numberOfDevicesInHousehold, false);
        HouseholdUser masterUser = HouseholdUtils.getMasterUserFromHousehold(household);
        HouseholdUser user = HouseholdUtils.getRegularUsersListFromHouseHold(household).get(0);


        // grant subscription - history = true
        client.setUserId(Integer.valueOf(user.getUserId()));
        Response<Boolean> booleanResponse = EntitlementServiceImpl.grant(client, ppvId, TransactionType.PPV, true, contentId);

        assertThat(booleanResponse.results.booleanValue()).isEqualTo(true);


        // verify other user from the household entitled to granted subscription
        client.setUserId(Integer.valueOf(masterUser.getUserId()));

        ProductPriceFilter productPriceFilter = new ProductPriceFilter();
        productPriceFilter.fileIdIn(String.valueOf(contentId));

        Response<ListResponse<ProductPrice>> productPriceListResponse = ProductPriceServiceImpl.list(client, productPriceFilter);
        ProductPrice productPrice = productPriceListResponse.results.getObjects().get(0);

        assertThat(productPriceListResponse.results.getTotalCount()).isEqualTo(1);
        assertThat(productPrice.getPrice().getAmount()).isEqualTo(0);
        assertThat(productPrice.getPurchaseStatus().getValue()).isEqualTo(PurchaseStatus.PPV_PURCHASED.getValue());


        // check transaction return in transactionHistory by user
        client.setKs(OttUserUtils.getKs(Integer.parseInt(user.getUserId()), null));
        client.setUserId(null);

        BillingTransaction billingTransaction;
        TransactionHistoryFilter transactionHistoryfilter = new TransactionHistoryFilter();
        transactionHistoryfilter.orderBy(TransactionHistoryOrderBy.CREATE_DATE_ASC.getValue());
        transactionHistoryfilter.entityReferenceEqual(EntityReferenceBy.USER.getValue());

        billingTransactionListResponse = TransactionHistoryServiceImpl.list(client, transactionHistoryfilter, null);
        assertThat(billingTransactionListResponse.results.getTotalCount()).isEqualTo(1);

        billingTransaction = billingTransactionListResponse.results.getObjects().get(0);
        assertThat(billingTransaction.getPurchasedItemCode()).isEqualTo(String.valueOf(assetId));


        // check transaction return in transactionHistory by household
        transactionHistoryfilter.entityReferenceEqual(EntityReferenceBy.HOUSEHOLD.getValue());

        billingTransactionListResponse = TransactionHistoryServiceImpl.list(client, transactionHistoryfilter, null);
        assertThat(billingTransactionListResponse.results.getTotalCount()).isEqualTo(1);

        billingTransaction = billingTransactionListResponse.results.getObjects().get(0);
        assertThat(billingTransaction.getPurchasedItemCode()).isEqualTo(String.valueOf(assetId));


        //delete household for cleanup
        HouseholdServiceImpl.delete(getClient(getAdministratorKs()), Math.toIntExact(household.getId()));
    }

    @Test(description = "entitlement/action/grant - grant ppv with history = false")
    private void grant_ppv_without_history() {
        Client client = getClient(getAdministratorKs());

        // set household
        Household household = HouseholdUtils.createHouseHold(numberOfUsersInHousehold, numberOfDevicesInHousehold, false);
        HouseholdUser masterUser = HouseholdUtils.getMasterUserFromHousehold(household);
        HouseholdUser user = HouseholdUtils.getRegularUsersListFromHouseHold(household).get(0);


        // grant subscription - history = true
        client.setUserId(Integer.valueOf(user.getUserId()));
        Response<Boolean> booleanResponse = EntitlementServiceImpl.grant(client, ppvId, TransactionType.PPV, true, contentId);

        assertThat(booleanResponse.results.booleanValue()).isEqualTo(true);


        // check transaction return in transactionHistory by user
        client.setKs(OttUserUtils.getKs(Integer.parseInt(user.getUserId()), null));
        client.setUserId(null);

        BillingTransaction billingTransaction;
        TransactionHistoryFilter transactionHistoryfilter = new TransactionHistoryFilter();
        transactionHistoryfilter.orderBy(TransactionHistoryOrderBy.CREATE_DATE_ASC.getValue());
        transactionHistoryfilter.entityReferenceEqual(EntityReferenceBy.USER.getValue());

        billingTransactionListResponse = TransactionHistoryServiceImpl.list(client, transactionHistoryfilter, null);
        assertThat(billingTransactionListResponse.results.getTotalCount()).isEqualTo(1);

        billingTransaction = billingTransactionListResponse.results.getObjects().get(0);
        assertThat(billingTransaction.getPurchasedItemCode()).isEqualTo(String.valueOf(assetId));


        // check transaction return in transactionHistory by household
        transactionHistoryfilter.entityReferenceEqual(EntityReferenceBy.HOUSEHOLD.getValue());

        billingTransactionListResponse = TransactionHistoryServiceImpl.list(client, transactionHistoryfilter, null);
        assertThat(billingTransactionListResponse.results.getTotalCount()).isEqualTo(1);

        billingTransaction = billingTransactionListResponse.results.getObjects().get(0);
        assertThat(billingTransaction.getPurchasedItemCode()).isEqualTo(String.valueOf(assetId));


        //delete household for cleanup
        HouseholdServiceImpl.delete(getClient(getAdministratorKs()), Math.toIntExact(household.getId()));
    }

    @Test(description = "entitlement/action/grant - grant ppv with wrong id - error 6001")
    private void grant_ppv_with_wrong_id() {
        int productId = 1;

        // get user form test shared household
        HouseholdUser user = HouseholdUtils.getRegularUsersListFromHouseHold(testSharedHousehold).get(0);

        // grant ppv with wrong id
        Client client = getClient(getAdministratorKs());
        client.setUserId(Integer.valueOf(user.getUserId()));
        Response<Boolean> booleanResponse = EntitlementServiceImpl.grant(client, productId, TransactionType.PPV, true, contentId);

        // assert error 6001 is return
        assertThat(booleanResponse.results).isEqualTo(null);
        assertThat(booleanResponse.error.getCode()).isEqualTo(getAPIExceptionFromList(6001).getCode());
    }

    @Test(description = "entitlement/action/grant - grant purchased ppv - error 3021")
    private void grant_purchased_ppv() {
        // get user form test shared household
        HouseholdUser user = HouseholdUtils.getRegularUsersListFromHouseHold(testSharedHousehold).get(0);

        // set client
        Client client = getClient(getAdministratorKs());
        client.setUserId(Integer.valueOf(user.getUserId()));

        // grant ppv - first time
        EntitlementServiceImpl.grant(client, ppvId, TransactionType.PPV, true, contentId);

        // grant ppv - second time
        Response<Boolean> booleanResponse = EntitlementServiceImpl.grant(client, ppvId, TransactionType.PPV, true, contentId);

        // assert error 3021 is return
        assertThat(booleanResponse.results).isEqualTo(null);
        assertThat(booleanResponse.error.getCode()).isEqualTo(getAPIExceptionFromList(3021).getCode());
    }

    @Test(description = "entitlement/action/grant - grant purchased subscription - error 3024")
    private void grant_purchased_subscription() {
        // get user form test shared household
        HouseholdUser user = HouseholdUtils.getRegularUsersListFromHouseHold(testSharedHousehold).get(0);

        // set client
        Client client = getClient(getAdministratorKs());
        client.setUserId(Integer.valueOf(user.getUserId()));

        // grant subscription - first time
        EntitlementServiceImpl.grant(client, subscriptionId, TransactionType.SUBSCRIPTION, false, null);

        // grant subscription - second time
        Response<Boolean> booleanResponse = EntitlementServiceImpl.grant(client, subscriptionId, TransactionType.SUBSCRIPTION, false, null);

        // assert error 3024 is return
        assertThat(booleanResponse.results).isEqualTo(null);
        assertThat(booleanResponse.error.getCode()).isEqualTo(getAPIExceptionFromList(3024).getCode());
    }

    @Issue("BEO-5022")
    @Test(description = "entitlement/action/grant - error 3023")
    private void grant_3023() {
        // TODO: 4/30/2018 implement test
    }

    @Issue("BEO-5022")
    @Test(description = "entitlement/action/grant - grant ppv with missing content id - error 3018")
    private void grant_ppv_with_missing_contentId() {
        // TODO: 4/30/2018 implement test
    }

    @Test(description = "entitlement/action/grant - user not in domain - error 1005")
    private void grant_ppv_user_not_in_domain() {
        // get user form test shared household
        Client client = getClient(null);
        Response<OTTUser> ottUserResponse = OttUserServiceImpl.register(client, partnerId, OttUserUtils.generateOttUser(), defaultUserPassword);
        OTTUser user = ottUserResponse.results;

        // set client with user not from household
        client.setKs(getAdministratorKs());
        client.setUserId(Integer.valueOf(user.getId()));

        // grant subscription
        Response<Boolean> booleanResponse = EntitlementServiceImpl.grant(client, ppvId, TransactionType.PPV, false, contentId);

        // assert error 1005 is return
        assertThat(booleanResponse.results).isEqualTo(null);
        assertThat(booleanResponse.error.getCode()).isEqualTo(getAPIExceptionFromList(1005).getCode());
    }

    @Test(description = "entitlement/action/grant - user suspend - error 2001")
    private void grant_ppv_user_suspend() {

        // set household
        Household household = HouseholdUtils.createHouseHold(numberOfUsersInHousehold, numberOfDevicesInHousehold, false);
        HouseholdUser masterUser = HouseholdUtils.getMasterUserFromHousehold(household);

        // suspend household
        Client client = getClient(getAdministratorKs());
        client.setUserId(Integer.valueOf(masterUser.getUserId()));
        HouseholdServiceImpl.suspend(client, null);

        // grant subscription to suspend user
        Response<Boolean> booleanResponse = EntitlementServiceImpl.grant(client, subscriptionId, TransactionType.SUBSCRIPTION, false, null);

        // assert error 2001 is return
        assertThat(booleanResponse.results).isEqualTo(null);
        assertThat(booleanResponse.error.getCode()).isEqualTo(getAPIExceptionFromList(2001).getCode());
    }

    // TODO: 4/16/2018 finish negative scenarios
//    @Test(description = "entitlement/action/grant - UnableToPurchaseFree - error 3022")
}
