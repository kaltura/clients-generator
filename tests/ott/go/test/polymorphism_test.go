package test

import (
	"encoding/json"
	"regexp"
	"testing"

	"github.com/kaltura/KalturaOttGeneratedAPIClientsGo/kalturaclient/enums/channelorderby"
	"github.com/kaltura/KalturaOttGeneratedAPIClientsGo/kalturaclient/enums/groupbyorder"
	"github.com/kaltura/KalturaOttGeneratedAPIClientsGo/kalturaclient/types"
	"github.com/stretchr/testify/assert"
	"github.com/stretchr/testify/require"
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
	require.JSONEq(t, channelFilterString, string(bytes))

	// response
	assetListResponseString := GetAssetListResponseString()
	response := []byte(assetListResponseString)
	var assetListResponse types.AssetListResponse
	err = json.Unmarshal(response, &assetListResponse)
	assert.NoError(t, err)

	assert.Equal(t, 5, len(assetListResponse.Objects))
	mediaAsset := GetMediaAsset()
	assert.Equal(t, mediaAsset, assetListResponse.Objects[0].MediaAsset)
	assert.Equal(t, mediaAsset, assetListResponse.Objects[0].Get())
	liveAsset := GeLiveAsset()
	assert.Equal(t, liveAsset, assetListResponse.Objects[1].LiveAsset)
	assert.Equal(t, liveAsset, assetListResponse.Objects[1].Get())
	programAsset := GetProgramAsset()
	assert.Equal(t, programAsset, assetListResponse.Objects[2].ProgramAsset)
	assert.Equal(t, programAsset, assetListResponse.Objects[2].Get())
	recordingAsset := GetRecordingAsset()
	assert.Equal(t, recordingAsset, assetListResponse.Objects[3].RecordingAsset)
	assert.Equal(t, recordingAsset, assetListResponse.Objects[3].Get())
	epg := GetEpg()
	assert.Equal(t, epg, assetListResponse.Objects[4].Epg)
	assert.Equal(t, epg, assetListResponse.Objects[4].Get())
	return assetListResponse.Objects
}

func GetChannelFilter() *types.ChannelFilter {
	groupBy := make([]types.AssetGroupByContainer, 1)
	groupBy[0] = types.AssetGroupByContainer { // prop from AssetFilter
		AssetMetaOrTagGroupBy: &types.AssetMetaOrTagGroupBy{
			Value: "4",
		},
	}

	return &types.ChannelFilter{
		OrderBy: string(channelorderby.NAME_ASC), // prop from Filter
		Name:    "NameTest",                      // prop from PersistedFilter
		DynamicOrderBy: types.DynamicOrderBy{ // prop from AssetFilter
			Name: "DynamicOrderByNameTest",
		},
		GroupBy: groupBy,
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

func GetProgramAsset() *types.ProgramAsset {
	helperId3 := int64(3)
	return &types.ProgramAsset{
		Id:   &helperId3, // prop from Asset
		Crid: "CridTest", // prop from ProgramAsset
	}
}

func GetRecordingAsset() *types.RecordingAsset {
	helperId4 := int64(4)
	return &types.RecordingAsset{
		Id:          &helperId4,        // prop from Asset
		Crid:        "CridTest",        // prop from ProgramAsset
		RecordingId: "RecordingIdTest", // prop from RecordingAsset
	}
}

func GetEpg() *types.Epg {
	helperId5 := int64(5)
	return &types.Epg{
		Id:   &helperId5, // prop from Asset
		Crid: "CridTest", // prop from ProgramAsset
	}
}

func GetChannelFilterString() string {
	return `{
		"excludeWatched": true,
		"groupOrderBy": "count_desc",
		"dynamicOrderBy": {
			"name": "DynamicOrderByNameTest",
			"objectType": "KalturaDynamicOrderBy"
		},
		"groupBy": [
			{
		  		"objectType": "KalturaAssetMetaOrTagGroupBy",
		  		"value": "4"
			}
	    ],
		"name": "NameTest",
		"orderBy": "NAME_ASC",
		"objectType": "KalturaChannelFilter"
	  }`
}

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
		  },
		  {
			"objectType": "KalturaProgramAsset",
			"id": 3,
			"crid": "CridTest"
		  },
		  {
			"objectType": "KalturaRecordingAsset",
			"id": 4,
			"crid": "CridTest",
			"recordingId": "RecordingIdTest"

		  },
		  {
			"objectType": "KalturaEpg",
			"id": 5,
			"crid": "CridTest"

		  }
		]
	  }`
}
