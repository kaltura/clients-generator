package test

import (
	"encoding/json"
	"fmt"
	"testing"

	"github.com/kaltura/KalturaOttGeneratedAPIClientsGo/kalturaclient/enums/epgorderby"
	"github.com/kaltura/KalturaOttGeneratedAPIClientsGo/kalturaclient/enums/ruleactiontype"
	"github.com/kaltura/KalturaOttGeneratedAPIClientsGo/kalturaclient/types"
	"github.com/stretchr/testify/assert"
)

func TestSerializationWithoutValues(t *testing.T) {
	t.Parallel()

	var testCases = []SomeStruct{
		{OrderBy: nil},
	}
	for _, before := range testCases {
		t.Run(fmt.Sprintf("order: %v", before.OrderBy), func(t *testing.T) {
			bytes, err := json.Marshal(before)
			assert.NoError(t, err)
			t.Log(string(bytes))

			after := SomeStruct{}
			err = json.Unmarshal(bytes, &after)
			assert.NoError(t, err)

			assert.Equal(t, before, after)
		})
	}
}

func TestSerializationOfValues(t *testing.T) {
	t.Parallel()
	order := ruleactiontype.START_DATE_OFFSET

	var testCases = []types.AccessControlBlockAction{
		{Type: &order},
	}
	for _, before := range testCases {
		t.Run(fmt.Sprintf("order: %v", before.Type), func(t *testing.T) {
			bytes, err := json.Marshal(before)
			assert.NoError(t, err)
			t.Log(string(bytes))

			after := types.AccessControlBlockAction{}
			err = json.Unmarshal(bytes, &after)
			assert.NoError(t, err)

			assert.Equal(t, before, after)
		})
	}
}

func TestDeserialization(t *testing.T) {
	t.Parallel()

	deserialized := SomeStruct{}
	err := json.Unmarshal([]byte(`{"orderBy":""}`), &deserialized)
	assert.Error(t, err)

	err = json.Unmarshal([]byte(`{"orderBy":null}`), &deserialized)
	assert.NoError(t, err)
	assert.Equal(t, SomeStruct{}, deserialized)
}

// epgcache
// type SomeStruct struct {
// 	OrderBy *epgorderby.EpgOrderBy`json:"orderBy,omitempty"`
// }
type SomeStruct struct {
	OrderBy *epgorderby.EpgOrderBy `json:"orderBy,omitempty"`
}
