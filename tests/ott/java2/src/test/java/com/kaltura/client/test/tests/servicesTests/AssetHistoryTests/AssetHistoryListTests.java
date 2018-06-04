package com.kaltura.client.test.tests.servicesTests.AssetHistoryTests;

import com.kaltura.client.enums.AssetType;
import com.kaltura.client.enums.BookmarkActionType;
import com.kaltura.client.enums.WatchStatus;
import com.kaltura.client.test.tests.BaseTest;
import com.kaltura.client.test.utils.AssetHistoryUtils;
import com.kaltura.client.types.AssetHistory;
import com.kaltura.client.types.AssetHistoryFilter;
import com.kaltura.client.types.Household;
import com.kaltura.client.types.ListResponse;
import com.kaltura.client.utils.response.base.Response;
import io.qameta.allure.Description;
import org.testng.annotations.Test;

import java.util.ArrayList;
import java.util.List;

import static com.kaltura.client.services.AssetHistoryService.*;
import static com.kaltura.client.test.IngestConstants.EPISODE_MEDIA_TYPE;
import static com.kaltura.client.test.IngestConstants.MOVIE_MEDIA_TYPE;
import static com.kaltura.client.test.Properties.MOVIE_MEDIA_TYPE_ID;
import static com.kaltura.client.test.Properties.getProperty;
import static com.kaltura.client.test.utils.BaseUtils.getConcatenatedString;
import static com.kaltura.client.test.utils.BaseUtils.getTimeInEpoch;
import static com.kaltura.client.test.utils.HouseholdUtils.createHousehold;
import static com.kaltura.client.test.utils.HouseholdUtils.getHouseholdMasterUserKs;
import static org.assertj.core.api.Assertions.assertThat;

public class AssetHistoryListTests extends BaseTest {

    private final int position1 = 10;
    private final int position2 = 20;
    private final int numbOfDevices = 1;
    private final int numOfUsers = 1;


    @Description("/AssetHistory/action/list - with no filter")
    @Test
    private void vodAssetHistory() {
        Household household = createHousehold(numOfUsers, numbOfDevices, false);
        String masterUserKs = getHouseholdMasterUserKs(household, null);

        // Ingest and bookmark first asset
        Long assetId1 = AssetHistoryUtils.ingestAssetAndPerformBookmark(masterUserKs, MOVIE_MEDIA_TYPE, position1, BookmarkActionType.FIRST_PLAY);
        // Ingest and bookmark second asset
        Long assetId2 = AssetHistoryUtils.ingestAssetAndPerformBookmark(masterUserKs, MOVIE_MEDIA_TYPE, position2, BookmarkActionType.FIRST_PLAY);

        AssetHistoryFilter assetHistoryFilter = AssetHistoryUtils.getAssetHistoryFilter(null, null, WatchStatus.ALL, null);

        //assetHistory/action/list - both assets should returned
        ListAssetHistoryBuilder listAssetHistoryBuilder = list(assetHistoryFilter, null)
                .setKs(masterUserKs);
        Response<ListResponse<AssetHistory>> assetHistoryListResponse = executor.executeSync(listAssetHistoryBuilder);

        // First object
        AssetHistory assetHistoryObject1 = assetHistoryListResponse.results.getObjects().get(0);
        // Second object
        AssetHistory assetHistoryObject2 = assetHistoryListResponse.results.getObjects().get(1);

        // Assertions for first object returned
        assertThat(assetHistoryObject1.getAssetId()).isEqualTo(assetId2);
        assertThat(assetHistoryObject1.getAssetType()).isEqualTo(AssetType.MEDIA);
        assertThat(assetHistoryObject1.getPosition()).isEqualTo(position2);
        assertThat(assetHistoryObject1.getDuration()).isGreaterThan(0);

        // Verify that flag is set to false (user hasn't finish watching the asset)
        assertThat(assetHistoryObject1.getFinishedWatching()).isFalse();
        assertThat(assetHistoryObject1.getWatchedDate()).isLessThanOrEqualTo(getTimeInEpoch(0));

        // Assertions for second object returned
        assertThat(assetHistoryObject2.getAssetId()).isEqualTo(assetId1);
        assertThat(assetHistoryObject2.getAssetType()).isEqualTo(AssetType.MEDIA);
        assertThat(assetHistoryObject2.getPosition()).isEqualTo(position1);

        // Assert total count = 2 (two bookmarks)
        assertThat(assetHistoryListResponse.results.getTotalCount()).isEqualTo(2);

        // clean
        clean_asset_history(assetHistoryFilter, household);
    }

    @Description("/AssetHistory/action/list -filtered by movie asset id")
    @Test
    private void vodAssetHistoryFilteredByAssetId() {

        Household household = createHousehold(numOfUsers, numbOfDevices, false);
        String masterUserKs = getHouseholdMasterUserKs(household, null);

        List<Long> assetIds = new ArrayList<>();
        int numOfBookmarks = 3;
        for (int i = 0; i < numOfBookmarks; i++) {
            long assetId = AssetHistoryUtils.ingestAssetAndPerformBookmark(masterUserKs, MOVIE_MEDIA_TYPE, position1, BookmarkActionType.FIRST_PLAY);
            assetIds.add(assetId);
        }

        // Ingest and bookmark first asset
        AssetHistoryFilter assetHistoryFilter = AssetHistoryUtils.getAssetHistoryFilter(String.valueOf(assetIds.get(1)), null, WatchStatus.ALL, null);

        //assetHistory/action/list - filter by asset 2 id
        ListAssetHistoryBuilder listAssetHistoryBuilder = list(assetHistoryFilter, null)
                .setKs(masterUserKs);
        Response<ListResponse<AssetHistory>> assetHistoryListResponse = executor.executeSync(listAssetHistoryBuilder);

        assertThat(assetHistoryListResponse.results.getTotalCount()).isEqualTo(1);
        assertThat(assetHistoryListResponse.results.getObjects().get(0).getAssetId()).isEqualTo(assetIds.get(1));

        String concatenatedString = getConcatenatedString(String.valueOf(assetIds.get(1)), String.valueOf(assetIds.get(2)));

        //assetHistory/action/list - filter by asset 2 and asset 3 ids
        assetHistoryFilter = AssetHistoryUtils.getAssetHistoryFilter(concatenatedString, null, WatchStatus.ALL, null);

        listAssetHistoryBuilder = list(assetHistoryFilter, null)
                .setKs(masterUserKs);
        assetHistoryListResponse = executor.executeSync(listAssetHistoryBuilder);

        List<AssetHistory> assetHistoryList = assetHistoryListResponse.results.getObjects();
        assertThat(assetHistoryList);

        List<Long> assetHistoryIdsList = new ArrayList<>();
        for (AssetHistory assetHistory : assetHistoryList) {
            assetHistoryIdsList.add(assetHistory.getAssetId());
        }
        assertThat(assetHistoryIdsList).containsOnly(assetIds.get(1), assetIds.get(2));

        // clean
        clean_asset_history(assetHistoryFilter, household);
    }

    @Description("/AssetHistory/action/list -filtered by movie type id")
    @Test
    private void vodAssetHistoryFilteredByAssetType() {

        Household household = createHousehold(numOfUsers, numbOfDevices, false);
        String userKs = getHouseholdMasterUserKs(household, null);


        // Ingest and bookmark first asset (movie in first play)
        Long assetId1 = AssetHistoryUtils.ingestAssetAndPerformBookmark(userKs, MOVIE_MEDIA_TYPE, 10, BookmarkActionType.FIRST_PLAY);
        // Ingest and bookmark second asset (movie in finish action)
        Long assetId2 = AssetHistoryUtils.ingestAssetAndPerformBookmark(userKs, EPISODE_MEDIA_TYPE, 10, BookmarkActionType.FIRST_PLAY);

        AssetHistoryFilter assetHistoryFilter = AssetHistoryUtils.getAssetHistoryFilter(null, null, WatchStatus.ALL,
                getProperty(MOVIE_MEDIA_TYPE_ID));

        //assetHistory/action/list - filter by in progress assets only

        ListAssetHistoryBuilder listAssetHistoryBuilder = list(assetHistoryFilter, null);
        listAssetHistoryBuilder.setKs(getHouseholdMasterUserKs(household, null));
        Response<ListResponse<AssetHistory>> assetHistoryListResponse = executor.executeSync(listAssetHistoryBuilder);

        assertThat(assetHistoryListResponse.results.getTotalCount()).isEqualTo(1);
        assertThat(assetHistoryListResponse.results.getObjects().get(0).getAssetId()).isEqualTo(assetId1);

        // clean
        clean_asset_history(assetHistoryFilter, household);
    }


    @Description("/AssetHistory/action/list -filtered by assets progress")
    @Test
    private void vodAssetHistoryFilteredByAssetProgress() {

        Household household = createHousehold(numOfUsers, numbOfDevices, false);
        String userKs = getHouseholdMasterUserKs(household, null);


        // Ingest and bookmark first asset (movie in first play)
        Long assetId1 = AssetHistoryUtils.ingestAssetAndPerformBookmark(userKs, MOVIE_MEDIA_TYPE, 10, BookmarkActionType.FIRST_PLAY);
        // Ingest and bookmark second asset (movie in finish action)
        Long assetId2 = AssetHistoryUtils.ingestAssetAndPerformBookmark(userKs, EPISODE_MEDIA_TYPE, 100, BookmarkActionType.FINISH);

        AssetHistoryFilter assetHistoryFilter = AssetHistoryUtils.getAssetHistoryFilter(null, null, WatchStatus.PROGRESS, null);

        //assetHistory/action/list - filter by in progress assets only

        ListAssetHistoryBuilder listAssetHistoryBuilder = list(assetHistoryFilter, null);
        listAssetHistoryBuilder.setKs(getHouseholdMasterUserKs(household, null));
        Response<ListResponse<AssetHistory>> assetHistoryListResponse = executor.executeSync(listAssetHistoryBuilder);

        assertThat(assetHistoryListResponse.results.getTotalCount()).isEqualTo(1);
        assertThat(assetHistoryListResponse.results.getObjects().get(0).getAssetId()).isEqualTo(assetId1);

        assetHistoryFilter = AssetHistoryUtils.getAssetHistoryFilter(null, null, WatchStatus.DONE, null);

        //assetHistory/action/list - filter by finished assets only

        listAssetHistoryBuilder = list(assetHistoryFilter, null);
        assetHistoryListResponse = executor.executeSync(listAssetHistoryBuilder);

        assertThat(assetHistoryListResponse.results.getTotalCount()).isEqualTo(1);
        assertThat(assetHistoryListResponse.results.getObjects().get(0).getAssetId()).isEqualTo(assetId2);

        // clean
        clean_asset_history(assetHistoryFilter, household);
    }


    private void clean_asset_history(AssetHistoryFilter assetHistoryFilter, Household household) {
        CleanAssetHistoryBuilder cleanAssetHistoryBuilder = clean(assetHistoryFilter);
        cleanAssetHistoryBuilder.setKs(getHouseholdMasterUserKs(household, null));
        executor.executeSync(cleanAssetHistoryBuilder);
    }

    //todo - Currently EPG program not returned in response (Ticket was opened to Omer - BEO-4594]
}
