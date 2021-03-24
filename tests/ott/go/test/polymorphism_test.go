
package test
/*
import (
	"encoding/json"
	"github.com/kaltura/KalturaOttGeneratedAPIClientsGo/kalturaclient/types"
	"github.com/stretchr/testify/assert"
	"testing"
)

func TestPolySerialization(t *testing.T) {
	t.Parallel()

	groupByFilter := types.NewKalturaGroupByFilter()
	groupByFilter.OrderBy = "name"
	groupByFilter.GroupBy = "series_id"
	assets := GetAssets(t, groupByFilter)
	_ = castToProgramAssetArray(assets)
}

func GetAssets(t *testing.T, filter types.AssetFilterInterface) []types.AssetInterface {
	// request
	bytes, err := json.Marshal(&filter)
	assert.NoError(t, err)
	t.Log(string(bytes))
	assert.Equal(t, `{"objectType":"KalturaGroupByFilter","orderBy":"name","groupBy":"series_id"}`, string(bytes))

	// response
	response := []byte(`{"objects": [{"objectType":"ProgramAsset", "id": 1, "ProgramId":1},{"objectType":"Asset", "id": 2}]}`)
	var assetResponse types.Assetlistresponse
	err = json.Unmarshal(response, &assetResponse)
	assert.NoError(t, err)

	objects := assetResponse.Objects
	assert.Equal(t, 2, len(objects))
	assert.Equal(t, &types.ProgramAsset {Asset: types.Asset{Id: 1}, ProgramId: 1, ObjectType: types.ObjectType{Type: "ProgramAsset"}}, objects[0])
	assert.Equal(t, &types.Asset{Id: 2, ObjectType: types.ObjectType{Type: "Asset"}}, objects[1])

	return objects
}

func castToProgramAssetArray(array []types.AssetInterface) []*types.ProgramAsset {
	var result []*types.ProgramAsset
	for _, value := range array {
		if t, ok := value.(*types.ProgramAsset); ok {
			result = append(result, t)
		}
	}
	return result
}
*/