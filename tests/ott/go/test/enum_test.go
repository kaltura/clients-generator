package test

import (
	"encoding/json"
	"fmt"
	"testing"

	"github.com/kaltura/KalturaOttGeneratedAPIClientsGo/kalturaclient/enums/epgorderby"
	"github.com/kaltura/KalturaOttGeneratedAPIClientsGo/kalturaclient/enums/userroleprofile"
	"github.com/kaltura/KalturaOttGeneratedAPIClientsGo/kalturaclient/enums/userroletype"
	"github.com/kaltura/KalturaOttGeneratedAPIClientsGo/kalturaclient/types"
	"github.com/stretchr/testify/assert"
)

func TestSerializationNoValues(t *testing.T) {
	t.Parallel()

	var testCases = []SomeStruct{
		{OrderBy: nil},
	}
	for _, before := range testCases {
		t.Run(fmt.Sprintf("order: %v", before.OrderBy), func(t *testing.T) {
			bytes, err := json.Marshal(before)
			assert.NoError(t, err)
			after := SomeStruct{}
			err = json.Unmarshal(bytes, &after)
			assert.NoError(t, err)
			assert.Equal(t, before, after)
		})
	}
}

func TestSerializationValues(t *testing.T) {
	t.Parallel()
	customType := userroletype.CUSTOM
	userroleprofileType := userroleprofile.PARTNER

	var testCases = []types.UserRole{
		{Type: &customType, Profile: userroleprofileType},
		{Type: nil},
	}
	for _, before := range testCases {
		t.Run(fmt.Sprintf("order: %v", before.Type), func(t *testing.T) {
			bytes, err := json.Marshal(before)
			assert.NoError(t, err)
			after := types.UserRole{}
			err = json.Unmarshal(bytes, &after)
			assert.NoError(t, err)
			assert.Equal(t, before, after)
		})
	}
}

func TestDeserializationNoValues(t *testing.T) {
	t.Parallel()

	deserialized := SomeStruct{}
	err := json.Unmarshal([]byte(`{"orderBy":""}`), &deserialized)
	assert.Error(t, err)

	err = json.Unmarshal([]byte(`{"orderBy":null}`), &deserialized)
	assert.NoError(t, err)
	assert.Equal(t, SomeStruct{}, deserialized)
}

func TestDeserializationValues(t *testing.T) {
	t.Parallel()
	deserialized := types.UserRole{}
	userRoleEmptyString := GetUserRoleEmptyString()
	err := json.Unmarshal([]byte(userRoleEmptyString), &deserialized)
	assert.Error(t, err)

	userRoleNullString := GetUserRoleNullString()
	err = json.Unmarshal([]byte(userRoleNullString), &deserialized)
	assert.NoError(t, err)
	assert.Equal(t, types.UserRole{Profile: userroleprofile.USER}, deserialized)

	userRoleValuesString := GetUserRoleValuesString()
	err = json.Unmarshal([]byte(userRoleValuesString), &deserialized)
	assert.NoError(t, err)
	userroleType := userroletype.CUSTOM
	assert.Equal(t, types.UserRole{Profile: userroleprofile.PARTNER,
		Type: &userroleType,
	}, deserialized)
	t.Log("End of TestDeserializationValues")
}

type SomeStruct struct {
	OrderBy *epgorderby.EpgOrderBy `json:"orderBy,omitempty"`
}

func GetUserRoleEmptyString() string {
	return `{
		"type": "",
		"profile": "",
		"objectType": "KalturaUserRole"
	  }`
}

func GetUserRoleNullString() string {
	return `{
		"type": null,
		"profile": "USER",
		"objectType": "KalturaUserRole"
	  }`
}

func GetUserRoleValuesString() string {
	return `{
		"type": "CUSTOM",
		"profile": "PARTNER",
		"objectType": "KalturaUserRole"
	  }`
}
