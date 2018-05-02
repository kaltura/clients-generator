package com.kaltura.client.test.tests.servicesTests.bookmarkTests;

import com.kaltura.client.enums.*;
import com.kaltura.client.services.BookmarkService;
import com.kaltura.client.test.tests.BaseTest;
import com.kaltura.client.test.utils.AssetUtils;
import com.kaltura.client.test.utils.BookmarkUtils;
import com.kaltura.client.types.*;
import com.kaltura.client.utils.response.base.Response;
import io.qameta.allure.Description;
import org.testng.annotations.BeforeClass;
import org.testng.annotations.Test;

import java.util.ArrayList;
import java.util.List;

import static com.kaltura.client.services.BookmarkService.*;
import static com.kaltura.client.test.tests.BaseTest.SharedHousehold.*;
import static com.kaltura.client.test.utils.BaseUtils.getAPIExceptionFromList;
import static org.assertj.core.api.Assertions.assertThat;


public class BookmarkAddTests extends BaseTest {

    private long assetId;
    private int fileId;
    private BookmarkActionType actionType;
    private int position;
    private List<String> assetList = new ArrayList<>();
    // instantiate Bookmark object
    private Bookmark bookmark = new Bookmark();
    // instantiate BookmarkFilter object
    private BookmarkFilter bookmarkFilter = new BookmarkFilter();

    @BeforeClass
    private void add_tests_before_class() {

        getSharedHousehold();
        assetId = BaseTest.getSharedMediaAsset().getId();
        fileId = AssetUtils.getAssetFileIds(String.valueOf(assetId)).get(0);

        assetList.add(String.valueOf(assetId));

        // Initialize bookmarkFilter object parameters
        bookmarkFilter = BookmarkUtils.listBookmark(BookmarkOrderBy.POSITION_ASC, AssetType.MEDIA, assetList);
    }

    @Description("bookmark/action/add - first play")
    @Test
    private void firstPlayback() {
        actionType = BookmarkActionType.FIRST_PLAY;
        position = 0;
        bookmark = BookmarkUtils.addBookmark(position, String.valueOf(assetId), fileId, AssetType.MEDIA, actionType);

        // Invoke bookmark/action/add request
        AddBookmarkBuilder addBookmarkBuilder = BookmarkService.add(bookmark);
        addBookmarkBuilder.setKs(getSharedMasterUserKs());
        Response<Boolean> booleanResponse = executor.executeSync(addBookmarkBuilder);

        // Verify response return true
        assertThat(booleanResponse.results.booleanValue()).isTrue();
        // Verify no error returned
        assertThat(booleanResponse.error).isNull();


        // Invoke bookmark/action/list to verify insertion of bookmark position
        ListBookmarkBuilder listBookmarkBuilder = BookmarkService.list(bookmarkFilter);
        listBookmarkBuilder.setKs(getSharedMasterUserKs());
        Response<ListResponse<Bookmark>> bookmarkListResponse = executor.executeSync(listBookmarkBuilder);

        Bookmark bookmark1 = bookmarkListResponse.results.getObjects().get(0);

        // Match content of asset id
        assertThat(bookmark1.getId()).isEqualTo(String.valueOf(assetId));

        // Match content of asset position
        assertThat(bookmark1.getPosition()).isEqualTo(this.position);

        // verify finishedWatching = false
        assertThat(bookmark1.getFinishedWatching()).isFalse();

        // Verify positionOwner = user
        assertThat(bookmark1.getPositionOwner()).isEqualTo(PositionOwner.USER);

        // Verify asset type = media
        assertThat(bookmark1.getType()).isEqualTo(AssetType.MEDIA);

        // Verify total count = 1
        assertThat(bookmarkListResponse.results.getTotalCount()).isEqualTo(1);

    }

    @Description("bookmark/action/add - pause")
    @Test
    private void pausePlayback() {
        // Set action type to "PAUSE"
        actionType = BookmarkActionType.PAUSE;
        position = 30;
        bookmark = BookmarkUtils.addBookmark(position, String.valueOf(assetId), fileId, AssetType.MEDIA, actionType);

        // Invoke bookmark/action/add request
        AddBookmarkBuilder addBookmarkBuilder = BookmarkService.add(bookmark);
        addBookmarkBuilder.setKs(getSharedMasterUserKs());
        Response<Boolean> booleanResponse = executor.executeSync(addBookmarkBuilder);

        // Verify response return true
        assertThat(booleanResponse.results.booleanValue()).isTrue();
        // Verify no error returned
        assertThat(booleanResponse.error).isNull();

        // Invoke bookmark/action/list to verify insertion of bookmark position
        ListBookmarkBuilder listBookmarkBuilder = BookmarkService.list(bookmarkFilter);
        listBookmarkBuilder.setKs(getSharedMasterUserKs());
        Response<ListResponse<Bookmark>> bookmarkListResponse = executor.executeSync(listBookmarkBuilder);
        Bookmark bookmark = bookmarkListResponse.results.getObjects().get(0);

        // Match content of asset position
        assertThat(bookmark.getPosition()).isEqualTo(this.position);
    }

    @Description("bookmark/action/add - 95% watching == finish watching")
    @Test
    private void watchingNinetyFive() {
        actionType = BookmarkActionType.PLAY;
        position = 999;
        bookmark = BookmarkUtils.addBookmark(position, String.valueOf(assetId), fileId, AssetType.MEDIA, actionType);

        // Invoke bookmark/action/add request
        AddBookmarkBuilder addBookmarkBuilder = BookmarkService.add(bookmark);
        addBookmarkBuilder.setKs(getSharedMasterUserKs());
        Response<Boolean> booleanResponse = executor.executeSync(addBookmarkBuilder);
        // Verify response return true
        assertThat(booleanResponse.results.booleanValue()).isTrue();
        // Verify no error returned
        assertThat(booleanResponse.error).isNull();

        // Invoke bookmark/action/list to verify insertion of bookmark position
        ListBookmarkBuilder listBookmarkBuilder = BookmarkService.list(bookmarkFilter);
        listBookmarkBuilder.setKs(getSharedMasterUserKs());
        Response<ListResponse<Bookmark>> bookmarkListResponse = executor.executeSync(listBookmarkBuilder);
        Bookmark bookmark3 = bookmarkListResponse.results.getObjects().get(0);

        // Verify finishedWatching = true
        assertThat(bookmark3.getFinishedWatching()).isTrue();

    }

    @Description("bookmark/action/add - back to start - position:0")
    @Test
    private void backToStart() {
        actionType = BookmarkActionType.STOP;
        position = 0;
        bookmark = BookmarkUtils.addBookmark(position, String.valueOf(assetId), fileId, AssetType.MEDIA, actionType);

        AddBookmarkBuilder addBookmarkBuilder = BookmarkService.add(bookmark);
        addBookmarkBuilder.setKs(getSharedMasterUserKs());
        Response<Boolean> booleanResponse = executor.executeSync(addBookmarkBuilder);
        assertThat(booleanResponse.results.booleanValue()).isTrue();
        ListBookmarkBuilder listBookmarkBuilder = BookmarkService.list(bookmarkFilter);
        listBookmarkBuilder.setKs(getSharedMasterUserKs());
        Response<ListResponse<Bookmark>> bookmarkListResponse = executor.executeSync(listBookmarkBuilder);
        Bookmark bookmark = bookmarkListResponse.results.getObjects().get(0);
        // Verify finishedWatching = false
        assertThat(bookmark.getFinishedWatching()).isFalse();
    }

    @Description("bookmark/action/add - finish watching")
    @Test
    private void finishWatching() {
        // Set action type to "FINISH"
        actionType = BookmarkActionType.FINISH;
        position = 60;
        bookmark = BookmarkUtils.addBookmark(position, String.valueOf(assetId), fileId, AssetType.MEDIA, actionType);

        // Invoke bookmark/action/add request
        AddBookmarkBuilder addBookmarkBuilder = BookmarkService.add(bookmark);
        addBookmarkBuilder.setKs(getSharedMasterUserKs());
        Response<Boolean> booleanResponse = executor.executeSync(addBookmarkBuilder);
        // Verify response return true
        assertThat(booleanResponse.results.booleanValue()).isTrue();
        // Verify no error returned
        assertThat(booleanResponse.error).isNull();

        // Invoke bookmark/action/list to verify insertion of bookmark position
        ListBookmarkBuilder listBookmarkBuilder = BookmarkService.list(bookmarkFilter);
        listBookmarkBuilder.setKs(getSharedMasterUserKs());
        Response<ListResponse<Bookmark>> bookmarkListResponse = executor.executeSync(listBookmarkBuilder);
        Bookmark bookmark = bookmarkListResponse.results.getObjects().get(0);

        // Verify finishedWatching = true
        assertThat(bookmark.getFinishedWatching()).isTrue();

    }

    // Error validations

    @Description("bookmark/action/add - empty asset id")
    @Test
    private void emptyAssetId() {
        bookmark = BookmarkUtils.addBookmark(0, null, fileId, AssetType.MEDIA, BookmarkActionType.FIRST_PLAY);
        AddBookmarkBuilder addBookmarkBuilder = BookmarkService.add(bookmark);
        addBookmarkBuilder.setKs(getSharedMasterUserKs());
        Response<Boolean> booleanResponse = executor.executeSync(addBookmarkBuilder);
        assertThat(booleanResponse.results).isNull();
        // Verify exception returned - code: 500003 ("Invalid Asset id")
        assertThat(booleanResponse.error.getCode()).isEqualTo(getAPIExceptionFromList(500003).getCode());
    }


    // TODO - Add test for EPG bookmark
    // TODO - Add test for recording bookmark
}
