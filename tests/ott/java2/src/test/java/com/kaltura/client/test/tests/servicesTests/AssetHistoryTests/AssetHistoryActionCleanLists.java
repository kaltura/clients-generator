package com.kaltura.client.test.tests.servicesTests.AssetHistoryTests;

import com.kaltura.client.enums.BookmarkActionType;
import com.kaltura.client.enums.WatchStatus;
import com.kaltura.client.services.AssetHistoryService;
import com.kaltura.client.test.tests.BaseTest;
import com.kaltura.client.test.utils.AssetHistoryUtils;
import com.kaltura.client.test.utils.HouseholdUtils;
import com.kaltura.client.types.AssetHistory;
import com.kaltura.client.types.AssetHistoryFilter;
import com.kaltura.client.types.Household;
import com.kaltura.client.types.ListResponse;
import com.kaltura.client.utils.response.base.Response;
import io.qameta.allure.Description;
import org.testng.annotations.BeforeClass;
import org.testng.annotations.Test;

import static com.kaltura.client.test.IngestConstants.EPISODE_MEDIA_TYPE;
import static com.kaltura.client.test.IngestConstants.MOVIE_MEDIA_TYPE;
import static com.kaltura.client.test.Properties.*;
import static org.assertj.core.api.Assertions.assertThat;

import static com.kaltura.client.services.AssetHistoryService.*;

public class AssetHistoryActionCleanLists extends BaseTest {

    private int position1 = 10;
    private int position2 = 20;
    private int numbOfDevices = 1;
    private int numOfUsers = 1;

    @BeforeClass
    private void add_tests_before_class() {

    }

    @Description("/assetHistory/action/clean - no filtering")
    @Test
    private void cleanHistory() {

        Household household = HouseholdUtils.createHouseHold(numOfUsers, numbOfDevices, false);

        // Ingest and bookmark first asset
        AssetHistoryUtils.ingestAssetAndPerformBookmark(client, MOVIE_MEDIA_TYPE, position1, BookmarkActionType.FIRST_PLAY);
        // Ingest and bookmark second asset
        AssetHistoryUtils.ingestAssetAndPerformBookmark(client, MOVIE_MEDIA_TYPE, position2, BookmarkActionType.FIRST_PLAY);

        AssetHistoryFilter assetHistoryFilter = AssetHistoryUtils.getAssetHistoryFilter(null, null, WatchStatus.ALL, null);

        //assetHistory/action/list - both assets should returned

        ListAssetHistoryBuilder listAssetHistoryBuilder = AssetHistoryService.list(assetHistoryFilter, null);
        listAssetHistoryBuilder.setKs(HouseholdUtils.getHouseholdMasterUserKs(household, null));
        Response<ListResponse<AssetHistory>> assetHistoryListResponse = executor.executeSync(listAssetHistoryBuilder);

        assertThat(assetHistoryListResponse.results.getTotalCount()).isEqualTo(2);

        //assetHistory/action/clean

        CleanAssetHistoryBuilder cleanAssetHistoryBuilder = AssetHistoryService.clean(assetHistoryFilter);
        cleanAssetHistoryBuilder.setKs(HouseholdUtils.getHouseholdMasterUserKs(household, null));
        executor.executeSync(cleanAssetHistoryBuilder);

        // assetHistory/action/list - after clean - no object returned
        listAssetHistoryBuilder = AssetHistoryService.list(assetHistoryFilter, null);
        assetHistoryListResponse = executor.executeSync(listAssetHistoryBuilder);

        assertThat(assetHistoryListResponse.results.getTotalCount()).isEqualTo(0);
    }

    @Description("/assetHistory/action/clean - filtered by asset id")
    @Test
    private void cleanSpecifcAssetHistory() {

        Household household = HouseholdUtils.createHouseHold(numOfUsers, numbOfDevices, false);

        // Ingest and bookmark first asset
        Long assetId1 = AssetHistoryUtils.ingestAssetAndPerformBookmark(client, MOVIE_MEDIA_TYPE, position1, BookmarkActionType.FIRST_PLAY);
        // Ingest and bookmark second asset
        Long assetId2 = AssetHistoryUtils.ingestAssetAndPerformBookmark(client, MOVIE_MEDIA_TYPE, position2, BookmarkActionType.FIRST_PLAY);

        AssetHistoryFilter assetHistoryFilter = AssetHistoryUtils.getAssetHistoryFilter(String.valueOf(assetId1), null, WatchStatus.ALL, null);

        //assetHistory/action/clean

        CleanAssetHistoryBuilder cleanAssetHistoryBuilder = AssetHistoryService.clean(assetHistoryFilter);
        cleanAssetHistoryBuilder.setKs(HouseholdUtils.getHouseholdMasterUserKs(household, null));
        executor.executeSync(cleanAssetHistoryBuilder);

        // Update assetHistoryFilter object (assetIdIn = null)
        assetHistoryFilter = AssetHistoryUtils.getAssetHistoryFilter(null, null, WatchStatus.ALL, null);

        // assetHistory/action/list - after clean - only asset id 2 returned (was not cleaned)
        ListAssetHistoryBuilder listAssetHistoryBuilder = AssetHistoryService.list(assetHistoryFilter, null);
        listAssetHistoryBuilder.setKs(HouseholdUtils.getHouseholdMasterUserKs(household, null));
        Response<ListResponse<AssetHistory>> assetHistoryListResponse = executor.executeSync(listAssetHistoryBuilder);

        assertThat(assetHistoryListResponse.results.getTotalCount()).isEqualTo(1);
        assertThat(assetHistoryListResponse.results.getObjects().get(0).getAssetId()).isEqualTo(assetId2);
    }

    @Description("/assetHistory/action/clean - filtered by asset type")
    @Test
    private void cleanSpecifcAssetTypeHistory() {

        Household household = HouseholdUtils.createHouseHold(numOfUsers, numbOfDevices, false);

        // Ingest and bookmark first asset
        Long assetId1 = AssetHistoryUtils.ingestAssetAndPerformBookmark(client, MOVIE_MEDIA_TYPE, position1, BookmarkActionType.FIRST_PLAY);
        // Ingest and bookmark second asset
        Long assetId2 = AssetHistoryUtils.ingestAssetAndPerformBookmark(client, EPISODE_MEDIA_TYPE, position2, BookmarkActionType.FIRST_PLAY);

        AssetHistoryFilter assetHistoryFilter = AssetHistoryUtils.getAssetHistoryFilter(null, null, WatchStatus.ALL, getProperty(EPISODE_MEDIA_TYPE_ID));

        //assetHistory/action/clean - only episode type (asset id 2)

        CleanAssetHistoryBuilder cleanAssetHistoryBuilder = AssetHistoryService.clean(assetHistoryFilter);
        cleanAssetHistoryBuilder.setKs(HouseholdUtils.getHouseholdMasterUserKs(household, null));
        executor.executeSync(cleanAssetHistoryBuilder);

        // Update assetHistoryFilter object (assetIdIn = null)
        assetHistoryFilter = AssetHistoryUtils.getAssetHistoryFilter(null, null, WatchStatus.ALL, null);

        // assetHistory/action/list - after clean - only asset id 1 returned (was not cleaned)

        ListAssetHistoryBuilder listAssetHistoryBuilder = AssetHistoryService.list(assetHistoryFilter, null);
        listAssetHistoryBuilder.setKs(HouseholdUtils.getHouseholdMasterUserKs(household, null));
        Response<ListResponse<AssetHistory>> assetHistoryListResponse = executor.executeSync(listAssetHistoryBuilder);

        assertThat(assetHistoryListResponse.results.getTotalCount()).isEqualTo(1);
        assertThat(assetHistoryListResponse.results.getObjects().get(0).getAssetId()).isEqualTo(assetId1);
    }

    @Description("/assetHistory/action/clean - filtered by asset progress")
    @Test
    private void cleanAssetsAccordingToWatchStatusDone() {

        Household household = HouseholdUtils.createHouseHold(numOfUsers, numbOfDevices, false);

        // Ingest and bookmark first asset
        Long assetId1 = AssetHistoryUtils.ingestAssetAndPerformBookmark(client, MOVIE_MEDIA_TYPE, position1, BookmarkActionType.FIRST_PLAY);
        // Ingest and bookmark second asset
        Long assetId2 = AssetHistoryUtils.ingestAssetAndPerformBookmark(client, EPISODE_MEDIA_TYPE, position2, BookmarkActionType.FINISH);

        AssetHistoryFilter assetHistoryFilter = AssetHistoryUtils.getAssetHistoryFilter(null, null, WatchStatus.DONE, null);

        //assetHistory/action/clean - only asset that were finished (asset 2)

        CleanAssetHistoryBuilder cleanAssetHistoryBuilder = AssetHistoryService.clean(assetHistoryFilter);
        cleanAssetHistoryBuilder.setKs(HouseholdUtils.getHouseholdMasterUserKs(household, null));
        executor.executeSync(cleanAssetHistoryBuilder);

        assetHistoryFilter = AssetHistoryUtils.getAssetHistoryFilter(null, null, WatchStatus.ALL, null);

        // assetHistory/action/list - after clean - only asset id 1 returned (was not cleaned)

        ListAssetHistoryBuilder listAssetHistoryBuilder = AssetHistoryService.list(assetHistoryFilter, null);
        listAssetHistoryBuilder.setKs(HouseholdUtils.getHouseholdMasterUserKs(household, null));
        Response<ListResponse<AssetHistory>> assetHistoryListResponse = executor.executeSync(listAssetHistoryBuilder);

        assertThat(assetHistoryListResponse.results.getTotalCount()).isEqualTo(1);
        assertThat(assetHistoryListResponse.results.getObjects().get(0).getAssetId()).isEqualTo(assetId1);
    }

    @Description("/assetHistory/action/clean - filtered by asset progress")
    @Test
    private void cleanAssetsAccordingToWatchStatusProgress() {

        Household household = HouseholdUtils.createHouseHold(numOfUsers, numbOfDevices, false);

        // Ingest and bookmark first asset
        Long assetId1 = AssetHistoryUtils.ingestAssetAndPerformBookmark(client, MOVIE_MEDIA_TYPE, position1, BookmarkActionType.FIRST_PLAY);
        // Ingest and bookmark second asset
        Long assetId2 = AssetHistoryUtils.ingestAssetAndPerformBookmark(client, EPISODE_MEDIA_TYPE, position2, BookmarkActionType.FINISH);

        AssetHistoryFilter assetHistoryFilter = AssetHistoryUtils.getAssetHistoryFilter(null, null, WatchStatus.PROGRESS, null);

        //assetHistory/action/clean - only asset that in progress (asset 1)

        CleanAssetHistoryBuilder cleanAssetHistoryBuilder = AssetHistoryService.clean(assetHistoryFilter);
        cleanAssetHistoryBuilder.setKs(HouseholdUtils.getHouseholdMasterUserKs(household, null));
        executor.executeSync(cleanAssetHistoryBuilder);

        assetHistoryFilter = AssetHistoryUtils.getAssetHistoryFilter(null, null, WatchStatus.ALL, null);

        // assetHistory/action/list - after clean - only asset id 2 returned (was not cleaned)

        ListAssetHistoryBuilder listAssetHistoryBuilder = AssetHistoryService.list(assetHistoryFilter, null);
        listAssetHistoryBuilder.setKs(HouseholdUtils.getHouseholdMasterUserKs(household, null));
        Response<ListResponse<AssetHistory>> assetHistoryListResponse = executor.executeSync(listAssetHistoryBuilder);

        assertThat(assetHistoryListResponse.results.getTotalCount()).isEqualTo(1);
        assertThat(assetHistoryListResponse.results.getObjects().get(0).getAssetId()).isEqualTo(assetId2);
    }
}
