package com.kaltura.client.test.utils;

import com.kaltura.client.enums.AssetReferenceType;
import com.kaltura.client.enums.ProductPriceOrderBy;
import com.kaltura.client.enums.TransactionType;
import com.kaltura.client.services.AssetService;
import com.kaltura.client.services.AssetService.GetAssetBuilder;
import com.kaltura.client.services.ProductPriceService;
import com.kaltura.client.services.ProductPriceService.ListProductPriceBuilder;
import com.kaltura.client.services.TransactionService;
import com.kaltura.client.services.TransactionService.PurchaseTransactionBuilder;
import com.kaltura.client.types.*;
import com.kaltura.client.utils.response.base.Response;
import javax.annotation.Nullable;
import java.util.HashMap;
import java.util.Map;
import java.util.Optional;
import static com.kaltura.client.test.tests.BaseTest.executor;

public class PurchaseUtils {

    public static Map<String, String> purchasePpvDetailsMap;
    public static Map<String, String> purchaseSubscriptionDetailsMap;

    private static Response<ListResponse<ProductPrice>> productPriceResponse;
    private static Response<Asset> assetResponse;

    // TODO: 14/MAR/2018 add return
    public static void purchasePpv(String ks, Optional<Integer> mediaId, Optional<Integer> fileId, @Nullable String purchaseCurrency) {
        purchasePpvDetailsMap = new HashMap<>();
        int paymentGatewayId = 0;

        int internalFileId =-1;
        if (fileId.isPresent()) {
            internalFileId = fileId.get();
        } else {
            GetAssetBuilder mediaAsset = AssetService.get(String.valueOf(mediaId.get()), AssetReferenceType.MEDIA);
            assetResponse = executor.executeSync(mediaAsset.setKs(ks));
            internalFileId = assetResponse.results.getMediaFiles().get(0).getId();
        }

        ProductPriceFilter filter = new ProductPriceFilter();
        filter.setOrderBy(ProductPriceOrderBy.PRODUCT_ID_ASC.getValue());
        filter.setFileIdIn(String.valueOf(internalFileId));
        filter.setIsLowest(false);

        ListProductPriceBuilder productPriceListBeforePurchase = ProductPriceService.list(filter);
        productPriceResponse = executor.executeSync(productPriceListBeforePurchase.setKs(ks));

        double price = productPriceResponse.results.getObjects().get(0).getPrice().getAmount();
        String ppvModuleId = ((PpvPrice)productPriceResponse.results.getObjects().get(0)).getPpvModuleId();

        Purchase purchase = new Purchase();
        purchase.setProductId(Integer.valueOf(ppvModuleId));
        purchase.setContentId(internalFileId);
        String currency = purchaseCurrency;
        if (purchaseCurrency == null || purchaseCurrency.isEmpty()) {
            currency = productPriceResponse.results.getObjects().get(0).getPrice().getCurrency();
        }
        purchase.setCurrency(currency);
        purchase.setPrice(price);
        purchase.setProductType(Optional.of(TransactionType.PPV).get());
        purchase.setPaymentGatewayId(Optional.of(paymentGatewayId).get());

        PurchaseTransactionBuilder transactionBuilder = TransactionService.purchase(purchase);
        executor.executeSync(transactionBuilder.setKs(ks));

        // TODO: complete the purchase ppv test
        purchasePpvDetailsMap.put("price", String.valueOf(price));
        purchasePpvDetailsMap.put("currency", currency);
        purchasePpvDetailsMap.put("ppvModuleId", ppvModuleId);
        purchasePpvDetailsMap.put("fileId", String.valueOf(internalFileId));
    }

    // TODO: 3/7/2018 add return
    // private as not completed
    private static void purchaseSubscription(int subscriptionId) {
        purchaseSubscriptionDetailsMap = new HashMap<>();
        int paymentGatewayId = 0;

        ProductPriceFilter filter = new ProductPriceFilter();
        filter.setSubscriptionIdIn(String.valueOf(subscriptionId));
        filter.setIsLowest(false);
        ListProductPriceBuilder productPriceListBeforePurchase = ProductPriceService.list(filter);
        productPriceResponse = executor.executeSync(productPriceListBeforePurchase);

        String currency = productPriceResponse.results.getObjects().get(0).getPrice().getCurrency();
        double price = productPriceResponse.results.getObjects().get(0).getPrice().getAmount();

        Purchase purchase = new Purchase();
        purchase.setProductId(subscriptionId);
        purchase.setContentId(0);
        purchase.setCurrency(currency);
        purchase.setPrice(price);
        purchase.setProductType(Optional.of(TransactionType.SUBSCRIPTION).get());
        purchase.setPaymentGatewayId(Optional.of(paymentGatewayId).get());

        PurchaseTransactionBuilder transactionBuilder = TransactionService.purchase(purchase);
        executor.executeSync(transactionBuilder);

        // TODO: complete the purchase subscription test
        purchaseSubscriptionDetailsMap.put("price", String.valueOf(price));
        purchaseSubscriptionDetailsMap.put("currency", currency);
    }
}
