package test

import (
	"encoding/json"
	"fmt"
	"github.com/kaltura/KalturaOttGeneratedAPIClientsGo/kalturaclient/enums/metaorderby"
	"github.com/stretchr/testify/assert"
	"testing"
)

func TestSerialization(t *testing.T) {
	t.Parallel()
	order := metaorderby.NAME_ASC
	var testCases = []SomeStruct{
		{OrderBy: &order},
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

func TestDeserialization(t *testing.T) {
	t.Parallel()

	deserialized := SomeStruct{}
	err := json.Unmarshal([]byte(`{"orderBy":""}`), &deserialized)
	assert.Error(t, err)

	err = json.Unmarshal([]byte(`{"orderBy":null}`), &deserialized)
	assert.NoError(t, err)
	assert.Equal(t, SomeStruct{}, deserialized)
}

type SomeStruct struct {
	OrderBy *metaorderby.MetaOrderBy `json:"orderBy,omitempty"`
}
