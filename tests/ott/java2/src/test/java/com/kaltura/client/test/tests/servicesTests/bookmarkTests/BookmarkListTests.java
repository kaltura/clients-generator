package com.kaltura.client.test.tests.servicesTests.bookmarkTests;

import com.kaltura.client.enums.AssetType;
import com.kaltura.client.enums.BookmarkActionType;
import com.kaltura.client.enums.BookmarkOrderBy;
import com.kaltura.client.services.BookmarkService;
import com.kaltura.client.test.tests.BaseTest;
import com.kaltura.client.test.utils.AssetUtils;
import com.kaltura.client.test.utils.BookmarkUtils;
import com.kaltura.client.types.Bookmark;
import com.kaltura.client.types.BookmarkFilter;
import com.kaltura.client.types.ListResponse;
import com.kaltura.client.utils.response.base.Response;
import io.qameta.allure.Description;
import org.testng.annotations.BeforeClass;
import org.testng.annotations.Test;

import java.util.ArrayList;
import java.util.List;

import static com.kaltura.client.services.BookmarkService.*;
import static com.kaltura.client.test.tests.BaseTest.SharedHousehold.getSharedMasterUserKs;
import static org.assertj.core.api.Assertions.assertThat;

public class BookmarkListTests extends BaseTest {

    private Long assetId;
    private int fileId;


    private Long assetId2;
    private int fileId2;

    private List <String> assetList = new ArrayList<>();

    @BeforeClass
    private void list_tests_before_class() {

        assetId = BaseTest.getSharedMediaAsset().getId();
        List<Integer> assetFileIds = AssetUtils.getAssetFileIds(String.valueOf(assetId));
        fileId = assetFileIds.get(0);
        assetList.add(String.valueOf(assetId));

        assetId2 = BaseTest.getSharedMediaAsset().getId();
        List<Integer> asset2FileIds = AssetUtils.getAssetFileIds(String.valueOf(assetId2));
        fileId2 = asset2FileIds.get(0);
        assetList.add(String.valueOf(assetId2));
    }

    @Description("bookmark/action/list - order by")
    @Test

    private void BookmarkOrderBy() {

        // Bookmark asset1
        Bookmark bookmark = BookmarkUtils.addBookmark(0, String.valueOf(assetId), fileId, AssetType.MEDIA, BookmarkActionType.FIRST_PLAY);
        BookmarkService.AddBookmarkBuilder addBookmarkBuilder = BookmarkService.add(bookmark);
        addBookmarkBuilder.setKs(getSharedMasterUserKs());
        Response<Boolean> booleanResponse = executor.executeSync(addBookmarkBuilder);

        assertThat(booleanResponse.results.booleanValue()).isTrue();

        // Bookmark asset2
        Bookmark bookmark2 = BookmarkUtils.addBookmark(10, String.valueOf(assetId2), fileId2, AssetType.MEDIA, BookmarkActionType.FIRST_PLAY);

        AddBookmarkBuilder addBookmarkBuilder2 = BookmarkService.add(bookmark2);
        addBookmarkBuilder2.setKs(getSharedMasterUserKs());
        Response<Boolean> booleanResponse2 = executor.executeSync(addBookmarkBuilder2);
        assertThat(booleanResponse2.results.booleanValue()).isTrue();

        BookmarkFilter bookmarkFilter = BookmarkUtils.listBookmark(BookmarkOrderBy.POSITION_DESC,AssetType.MEDIA, assetList);
        ListBookmarkBuilder listBookmarkBuilder = BookmarkService.list(bookmarkFilter);
        listBookmarkBuilder.setKs(getSharedMasterUserKs());
        Response<ListResponse<Bookmark>> bookmarkListResponse = executor.executeSync(listBookmarkBuilder);

        Bookmark bookmarkObject = bookmarkListResponse.results.getObjects().get(0);
        Bookmark bookmarkObject2 = bookmarkListResponse.results.getObjects().get(1);

        // Verify that asset2 returned first (bookmark/action/list is response is ordered by POSITION DESC)
        assertThat( bookmarkObject.getId()).isEqualTo(String.valueOf(assetId2));
        assertThat( bookmarkObject2.getId()).isEqualTo(String.valueOf(assetId));

        bookmarkFilter = BookmarkUtils.listBookmark(BookmarkOrderBy.POSITION_ASC,AssetType.MEDIA, assetList);
        listBookmarkBuilder = BookmarkService.list(bookmarkFilter);
        listBookmarkBuilder.setKs(getSharedMasterUserKs());
        bookmarkListResponse = executor.executeSync(listBookmarkBuilder);

        bookmarkObject = bookmarkListResponse.results.getObjects().get(0);
        bookmarkObject2 = bookmarkListResponse.results.getObjects().get(1);

        // Verify that asset1 returned first (bookmark/action/list is response is ordered by POSITION DESC)
        assertThat( bookmarkObject.getId()).isEqualTo(String.valueOf(assetId));
        assertThat( bookmarkObject2.getId()).isEqualTo(String.valueOf(assetId2));
    }
}
