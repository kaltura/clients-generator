package test

import (
	"encoding/json"
	"testing"

	"github.com/kaltura/KalturaOttGeneratedAPIClientsGo/kalturaclient/types"
	"github.com/stretchr/testify/assert"
)

func TestPolySerialization(t *testing.T) {
	t.Parallel()

	groupByFilter := types.NewKalturaGroupByFilter()
	groupByFilter.OrderBy = "name"
	groupByFilter.GroupBy = "series_id"
	assets := GetAssets(t, groupByFilter)
	_ = castToEpgAssetArray(assets)
}

func GetAssets(t *testing.T, filter types.KalturaFilterInterface) []types.AssetInterface {
	// request
	bytes, err := json.Marshal(&filter)
	assert.NoError(t, err)
	t.Log(string(bytes))
	assert.Equal(t, `{"objectType":"KalturaGroupByFilter","orderBy":"name","groupBy":"series_id"}`, string(bytes))

	// response
	response := []byte(`{"objects": [{"objectType":"EpgAsset", "id": 1, "epgId":1},{"objectType":"Asset", "id": 2}]}`)
	var assetResponse types.AssetResponse
	err = json.Unmarshal(response, &assetResponse)
	assert.NoError(t, err)

	objects := assetResponse.Objects
	assert.Equal(t, 2, len(objects))
	assert.Equal(t, &types.EpgAsset{Asset: types.Asset{Id: 1}, EpgId: 1, ObjectType: types.ObjectType{Type: "EpgAsset"}}, objects[0])
	assert.Equal(t, &types.Asset{Id: 2, ObjectType: types.ObjectType{Type: "Asset"}}, objects[1])

	return objects
}

func castToEpgAssetArray(array []types.AssetInterface) []*types.EpgAsset {
	var result []*types.EpgAsset
	for _, value := range array {
		if t, ok := value.(*types.EpgAsset); ok {
			result = append(result, t)
		}
	}
	return result
}
