package com.kaltura.client.test.tests.servicesTests.assetCommentTests;

import com.kaltura.client.enums.AssetCommentOrderBy;
import com.kaltura.client.enums.AssetType;
import com.kaltura.client.services.AssetCommentService;
import com.kaltura.client.test.tests.BaseTest;
import com.kaltura.client.test.utils.AssetCommentUtils;
import com.kaltura.client.test.utils.BaseUtils;
import com.kaltura.client.test.utils.HouseholdUtils;
import com.kaltura.client.test.utils.IngestUtils;
import com.kaltura.client.types.*;
import com.kaltura.client.utils.response.base.Response;
import io.qameta.allure.Description;
import org.testng.annotations.BeforeClass;
import org.testng.annotations.Test;

import java.util.Optional;

import static com.kaltura.client.services.AssetCommentService.*;
import static org.assertj.core.api.AssertionsForClassTypes.assertThat;

public class AssetCommentAddTests extends BaseTest {

    private String writer = "Shmulik";
    private Long createDate = 0L;
    private String header = "header";
    private String subHeader = "subHeader";
    private String text = "A lot of text";
    private Household household;

    @BeforeClass
    private void add_tests_before_class() {
        household = HouseholdUtils.createHouseHold(1, 1, false);
    }

    @Description("AssetComment/action/add - vod asset")
    @Test
    private void addCommentForVod() {

        Long assetId = BaseTest.getSharedMediaAsset().getId();

        // Initialize assetComment object
        AssetComment assetComment = AssetCommentUtils.assetComment(Math.toIntExact(assetId), AssetType.MEDIA, writer, text, createDate, subHeader, header);

        // AssetComment/action/add
        AddAssetCommentBuilder addAssetCommentBuilder = AssetCommentService.add(assetComment);
        addAssetCommentBuilder.setKs(HouseholdUtils.getHouseholdMasterUserKs(household,null));
        Response<AssetComment> assetCommentResponse = executor.executeSync(addAssetCommentBuilder);

        //Assertions for AssetComment/action/add
        assertThat(assetCommentResponse.results.getId()).isGreaterThan(0);
        assertThat(assetCommentResponse.results.getAssetId()).isEqualTo(Math.toIntExact(assetId));
        assertThat(assetCommentResponse.results.getAssetType()).isEqualTo(AssetType.MEDIA);
        assertThat(assetCommentResponse.results.getWriter()).isEqualTo(writer);
        assertThat(assetCommentResponse.results.getText()).isEqualTo(text);
        assertThat(assetCommentResponse.results.getSubHeader()).isEqualTo(subHeader);
        assertThat(assetCommentResponse.results.getHeader()).isEqualTo(header);
        assertThat(assetCommentResponse.results.getCreateDate()).isLessThanOrEqualTo(BaseUtils.getTimeInEpoch(0));


        //Initialize assetCommentFilter object
        AssetCommentFilter assetCommentFilter = AssetCommentUtils.assetCommentFilter(Math.toIntExact(assetId), AssetType.MEDIA,
                AssetCommentOrderBy.CREATE_DATE_DESC);

        //AssetComment/action/list
        ListAssetCommentBuilder listAssetCommentBuilder = AssetCommentService.list(assetCommentFilter,null);
        listAssetCommentBuilder.setKs(HouseholdUtils.getHouseholdMasterUserKs(household,null));
        Response<ListResponse<AssetComment>> assetCommentListResponse = executor.executeSync(listAssetCommentBuilder);
        AssetComment assetCommentObjectResponse = assetCommentListResponse.results.getObjects().get(0);

        //Assertions for AssetComment/action/list
        assertThat(assetCommentObjectResponse.getId()).isEqualTo(assetCommentResponse.results.getId());
        assertThat(assetCommentObjectResponse.getAssetId()).isEqualTo(assetCommentResponse.results.getAssetId());
        assertThat(assetCommentObjectResponse.getAssetType()).isEqualTo(assetCommentResponse.results.getAssetType());
        assertThat(assetCommentObjectResponse.getSubHeader()).isEqualTo(assetCommentResponse.results.getSubHeader());
        assertThat(assetCommentObjectResponse.getHeader()).isEqualTo(assetCommentResponse.results.getHeader());
        assertThat(assetCommentObjectResponse.getText()).isEqualTo(assetCommentResponse.results.getText());
        assertThat(assetCommentObjectResponse.getWriter()).isEqualTo(assetCommentResponse.results.getWriter());
        assertThat(assetCommentObjectResponse.getCreateDate()).isLessThanOrEqualTo(BaseUtils.getTimeInEpoch(0));
    }


    @Description("AssetComment/action/add - EPG program")
    @Test
    private void addCommentForEPGProgram() {

        // Ingest EPG program
        Response<ListResponse<Asset>> epgProgram = IngestUtils.ingestEPG("Shmulik_Series_1", Optional.empty(), Optional.empty(), Optional.empty(), Optional.empty(),
                Optional.empty(), Optional.empty(), Optional.empty(), Optional.empty(), Optional.empty());
        Long epgProgramId = epgProgram.results.getObjects().get(0).getId();

        // Initialize assetComment object
        AssetComment assetComment = AssetCommentUtils.assetComment(Math.toIntExact(epgProgramId), AssetType.EPG, writer, text, createDate, subHeader, header);

        // AssetComment/action/add
        AddAssetCommentBuilder addAssetCommentBuilder = AssetCommentService.add(assetComment);
        addAssetCommentBuilder.setKs(HouseholdUtils.getHouseholdMasterUserKs(household,null));
        Response<AssetComment> assetCommentResponse = executor.executeSync(addAssetCommentBuilder);

        //Assertions for AssetComment/action/add
        assertThat(assetCommentResponse.results.getId()).isGreaterThan(0);
        assertThat(assetCommentResponse.results.getAssetId()).isEqualTo(Math.toIntExact(epgProgramId));
        assertThat(assetCommentResponse.results.getAssetType()).isEqualTo(AssetType.EPG);

        //Initialize assetCommentFilter object
        AssetCommentFilter assetCommentFilter = AssetCommentUtils.assetCommentFilter(Math.toIntExact(epgProgramId), AssetType.EPG,
                AssetCommentOrderBy.CREATE_DATE_DESC);

        //AssetComment/action/list
        ListAssetCommentBuilder listAssetCommentBuilder = AssetCommentService.list(assetCommentFilter,null);
        listAssetCommentBuilder.setKs(HouseholdUtils.getHouseholdMasterUserKs(household,null));
        Response<ListResponse<AssetComment>> assetCommentListResponse = executor.executeSync(listAssetCommentBuilder);
        AssetComment assetCommentObjectResponse = assetCommentListResponse.results.getObjects().get(0);

        //Assertions for AssetComment/action/list
        assertThat(assetCommentObjectResponse.getId()).isEqualTo(assetCommentResponse.results.getId());
        assertThat(assetCommentObjectResponse.getAssetId()).isEqualTo(assetCommentResponse.results.getAssetId());
        assertThat(assetCommentObjectResponse.getAssetType()).isEqualTo(assetCommentResponse.results.getAssetType());
    }

    // todo - Add error validations tests
    // todo - Add tests for recording
}
