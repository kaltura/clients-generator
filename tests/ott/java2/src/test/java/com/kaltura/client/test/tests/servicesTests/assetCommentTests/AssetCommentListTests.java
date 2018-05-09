package com.kaltura.client.test.tests.servicesTests.assetCommentTests;

import com.kaltura.client.enums.AssetCommentOrderBy;
import com.kaltura.client.enums.AssetType;
import com.kaltura.client.services.AssetCommentService;
import com.kaltura.client.test.tests.BaseTest;
import com.kaltura.client.test.utils.AssetCommentUtils;
import com.kaltura.client.test.utils.IngestUtils;
import com.kaltura.client.types.AssetComment;
import com.kaltura.client.types.AssetCommentFilter;
import com.kaltura.client.types.Household;
import com.kaltura.client.types.ListResponse;
import com.kaltura.client.utils.response.base.Response;
import io.qameta.allure.Description;
import org.testng.annotations.BeforeClass;
import org.testng.annotations.Test;

import java.util.Optional;

import static com.kaltura.client.services.AssetCommentService.ListAssetCommentBuilder;
import static com.kaltura.client.test.IngestConstants.MOVIE_MEDIA_TYPE;
import static com.kaltura.client.test.utils.HouseholdUtils.createHousehold;
import static com.kaltura.client.test.utils.HouseholdUtils.getHouseholdMasterUserKs;
import static org.assertj.core.api.Assertions.assertThat;

public class AssetCommentListTests extends BaseTest {

    private Household household;
    private String householdMasterUserKs;

    @BeforeClass
    // TODO: 5/3/2018 edit before method name 
    private void add_tests_before_class() {
        int numOfUsers = 1;
        int numOfDevices = 1;
        household = createHousehold(numOfUsers, numOfDevices, false);
        householdMasterUserKs = getHouseholdMasterUserKs(household, null);
    }

    @Description("AssetComment/action/list - check order by functionality")
    @Test
    private void checkCommentsOrder() {

        String writer = "Shmulik";
        Long createDate = 0L;
        String header = "header";
        String subHeader = "subHeader";
        String text = "A lot of text";

        Long assetId = IngestUtils.ingestVOD(Optional.empty(), Optional.empty(), true, Optional.empty(), Optional.empty(), Optional.empty(), Optional.empty(),
                Optional.empty(), Optional.empty(), Optional.empty(), Optional.of(String.valueOf(MOVIE_MEDIA_TYPE)), Optional.empty(), Optional.empty()).getId();

        // Initialize assetComment object
        AssetComment assetComment = AssetCommentUtils.assetComment(Math.toIntExact(assetId), AssetType.MEDIA, writer, text, createDate, subHeader, header);

        // AssetComment/action/add - first comment

        AssetCommentService.AddAssetCommentBuilder addAssetCommentBuilder = AssetCommentService.add(assetComment)
                .setKs(householdMasterUserKs);
        Response<AssetComment> assetComment1Response = executor.executeSync(addAssetCommentBuilder);

        // AssetComment/action/add - second comment comment
        AssetCommentService.AddAssetCommentBuilder addAssetCommentBuilder2 = AssetCommentService.add(assetComment)
                .setKs(householdMasterUserKs);
        Response<AssetComment> assetComment2Response = executor.executeSync(addAssetCommentBuilder2);

        //Initialize assetCommentFilter object
        AssetCommentFilter assetCommentFilter = AssetCommentUtils.assetCommentFilter(Math.toIntExact(assetId), AssetType.MEDIA,
                AssetCommentOrderBy.CREATE_DATE_DESC);

        //AssetComment/action/list - return both comments

        ListAssetCommentBuilder listAssetCommentBuilder = AssetCommentService.list(assetCommentFilter, null)
                .setKs(householdMasterUserKs);
        Response<ListResponse<AssetComment>> assetCommentListResponse = executor.executeSync(listAssetCommentBuilder);

        AssetComment assetCommentObjectResponse = assetCommentListResponse.results.getObjects().get(0);
        AssetComment assetComment2ObjectResponse = assetCommentListResponse.results.getObjects().get(1);

        // Assert that total count = 2 (two comments added)
        assertThat(assetCommentListResponse.results.getTotalCount()).isEqualTo(2);

        // Assert that second comment return first because order by = CREATE_DATE_DESC (newest comment return first)
        assertThat(assetCommentObjectResponse.getAssetId()).isEqualTo(assetComment2Response.results.getAssetId());
        assertThat(assetComment2ObjectResponse.getAssetId()).isEqualTo(assetComment1Response.results.getAssetId());
    }
}
