package test

import (
	"encoding/json"
	"regexp"
	"testing"

	"github.com/kaltura/KalturaOttGeneratedAPIClientsGo/kalturaclient/enums/channelorderby"
	"github.com/kaltura/KalturaOttGeneratedAPIClientsGo/kalturaclient/enums/groupbyorder"
	"github.com/kaltura/KalturaOttGeneratedAPIClientsGo/kalturaclient/types"
	"github.com/stretchr/testify/assert"
)

func TestPolySerialization(t *testing.T) {
	t.Parallel()
	channelFilter := GetChannelFilter()
	_ = GetAssets(t, channelFilter)
}

func GetAssets(t *testing.T, filter types.AssetFilterInterface) []types.AssetContainer {
	// request
	bytes, err := json.Marshal(&filter)
	assert.NoError(t, err)
	space := regexp.MustCompile(`\s+`)
	channelFilterString := space.ReplaceAllString(GetChannelFilterString(), "")
	assert.Equal(t, channelFilterString, string(bytes))

	// response
	assetListResponseString := space.ReplaceAllString(GetAssetListResponseString(), "")
	response := []byte(assetListResponseString)
	var assetListResponse types.AssetListResponse
	err = json.Unmarshal(response, &assetListResponse)
	assert.NoError(t, err)

	assert.Equal(t, 2, len(assetListResponse.Objects))
	mediaAsset := GetMediaAsset()
	assert.Equal(t, mediaAsset, assetListResponse.Objects[0].MediaAsset)
	assert.Equal(t, mediaAsset, assetListResponse.Objects[0].Get())
	liveAsset := GeLiveAsset()
	assert.Equal(t, liveAsset, assetListResponse.Objects[1].LiveAsset)
	assert.Equal(t, liveAsset, assetListResponse.Objects[1].Get())
	//TODO AMIT - ASSERT ALL TYPES
	return assetListResponse.Objects
}

func GetChannelFilter() *types.ChannelFilter {
	return &types.ChannelFilter{
		OrderBy: string(channelorderby.NAME_ASC), // prop from Filter
		Name:    "NameTest",                      // prop from PersistedFilter
		DynamicOrderBy: types.DynamicOrderBy{ // prop from AssetFilter
			Name: "DynamicOrderByNameTest",
		},
		GroupOrderBy:   groupbyorder.COUNT_DESC, // prop from BaseSearchAssetFilter
		ExcludeWatched: true,                    // prop from ChannelFilter
	}
}

func GetMediaAsset() *types.MediaAsset {
	helperId1 := int64(1)
	return &types.MediaAsset{
		Id:      &helperId1,         // prop from Asset
		EntryId: "MediaEntryIdTest", // prop from MediaAsset
	}
}

func GeLiveAsset() *types.LiveAsset {
	helperId2 := int64(2)
	return &types.LiveAsset{
		Id:             &helperId2,           // prop from Asset
		EntryId:        "LiveEntryIdTest",    // prop from MediaAsset
		ExternalCdvrId: "ExternalCdvrIdTest", // prop from LiveAsset
	}
}

//TODO AMIT - ProgramAsset, RecordingAsset, Epg - set one property from each inheritence

func GetChannelFilterString() string {
	return `{
		"excludeWatched": true,
		"groupOrderBy": "count_desc",
		"dynamicOrderBy": {
			"name": "DynamicOrderByNameTest",
			"objectType": "KalturaDynamicOrderBy"
		},
		"name": "NameTest",
		"orderBy": "NAME_ASC",
		"objectType": "KalturaChannelFilter"
	  }`
}

// TODO AMIT - ADD VALUES TO RESPONSE STRING
func GetAssetListResponseString() string {
	return `{
		"totalCount": 2,
		"objects": [
		  {
			"objectType": "KalturaMediaAsset",
			"id": 1,
			"entryId": "MediaEntryIdTest"
		  },
		  {
			"objectType": "KalturaLiveAsset",
			"id": 2,
			"entryId": "LiveEntryIdTest",
			"externalCdvrId": "ExternalCdvrIdTest"
		  }
		]
	  }`
}
