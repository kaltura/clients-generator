package test

import (
	"encoding/json"
	"testing"

	"github.com/kaltura/KalturaOttGeneratedAPIClientsGo/kalturaclient/types"
	"github.com/stretchr/testify/assert"
)

func TestPolySerialization(t *testing.T) {
	t.Parallel()

	groupByFilter := types.KalturaFilterInterface{
		KalturaGroupByFilter: &types.KalturaGroupByFilter{
			OrderBy: "name",
			GroupBy: "series_id",
		},
	}
	_ = GetAssets(t, groupByFilter)
}

func GetAssets(t *testing.T, filter types.KalturaFilterInterface) []types.AssetContainer {
	// request
	bytes, err := json.Marshal(&filter)
	assert.NoError(t, err)
	t.Log(string(bytes))
	assert.Equal(t, `{"objectType":"KalturaGroupByFilter","orderBy":"name","groupBy":"series_id"}`, string(bytes))

	// response
	response := []byte(`{ "totalCount":2, "totalSize":{"size":5},  "objects": [{"objectType":"EpgAsset", "id": 1, "epgId":1},{"objectType":"MediaAsset", "id": 2, "mediaId":3}]}`)
	var assetResponse types.AssetResponse
	err = json.Unmarshal(response, &assetResponse)
	assert.NoError(t, err)

	objects := assetResponse.Objects
	assert.Equal(t, 2, len(objects))
	objects[0].Get().GetId()
	assert.Equal(t, &types.EpgAsset{Id: 1, EpgId: 1}, objects[0].EpgAsset)
	assert.Equal(t, &types.MediaAsset{Id: 2, MediaId: 3}, objects[1].MediaAsset)

	return objects
}
