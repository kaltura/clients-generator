package test

import (
	"encoding/json"
	"testing"

	"github.com/kaltura/KalturaOttGeneratedAPIClientsGo/kalturaclient/enums/channelorderby"
	"github.com/kaltura/KalturaOttGeneratedAPIClientsGo/kalturaclient/enums/groupbyorder"
	"github.com/kaltura/KalturaOttGeneratedAPIClientsGo/kalturaclient/types"
	"github.com/stretchr/testify/assert"
)

func TestPolySerialization(t *testing.T) {
	t.Parallel()

	channelFilter := &types.ChannelFilter{
		OrderBy:      channelorderby.NAME_ASC,
		GroupOrderBy: groupbyorder.COUNT_DESC,
	}
	_ = GetAssets(t, channelFilter)
}

func GetAssets(t *testing.T, filter types.AssetFilterInterface) []types.AssetContainer {
	// request
	bytes, err := json.Marshal(&filter)
	assert.NoError(t, err)
	t.Log(string(bytes))
	assert.Equal(t, `{"name":"","dynamicOrderBy":{"name":"","orderBy":"","objectType":"KalturaDynamicOrderBy"},"kSql":"","groupBy":null,"groupOrderBy":"count_desc","idEqual":0,"excludeWatched":false,"orderBy":"NAME_ASC","objectType":"KalturaChannelFilter"}`, string(bytes))

	// response
	response := []byte(`{ "totalCount":2, "objects": [{"objectType":"KalturaProgramAsset", "id": 1, "epgId": "1b"},{"objectType":"KalturaMediaAsset", "id": 2, "EntryId":"a3"}]}`)
	var assetListResponse types.AssetListResponse
	err = json.Unmarshal(response, &assetListResponse)
	assert.NoError(t, err)

	objects := assetListResponse.Objects
	assert.Equal(t, 2, len(objects))
	objects[0].Get().GetId()
	assert.Equal(t, &types.ProgramAsset{Id: 1, EpgId: "1b"}, objects[0].ProgramAsset)
	assert.Equal(t, &types.MediaAsset{Id: 2, EntryId: "a3"}, objects[1].MediaAsset)

	return objects
}
