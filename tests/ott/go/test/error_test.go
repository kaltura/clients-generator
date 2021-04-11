package test

import (
	"context"
	"encoding/json"
	"testing"

	"github.com/kaltura/KalturaOttGeneratedAPIClientsGo/kalturaclient/errors"
	apierrors "github.com/kaltura/KalturaOttGeneratedAPIClientsGo/kalturaclient/errors"
	"github.com/kaltura/KalturaOttGeneratedAPIClientsGo/kalturaclient/services"
	"github.com/kaltura/KalturaOttGeneratedAPIClientsGo/kalturaclient/types"
	"github.com/kaltura/KalturaOttGeneratedAPIClientsGo/test/utils"
	"github.com/stretchr/testify/assert"
)

func TestErrorWithArgs(t *testing.T) {
	client, mockHttpClient := utils.CreateClientAndMock()
	ctx := utils.WithRequestId(context.Background(), "requestId")
	password := "nopassword"
	token := ""
	mockHttpClient.SetResponse("/api_v3/service/ottUser/action/setInitialPassword", setInitialPasswordErrorWithArgsResponseFromPhoenix(), 200)

	setInitialPasswordResponse, err := services.NewOttUserService(client).SetInitialPassword(ctx, partnerId, token, password)
	assert.Error(t, err)
	// TODO - i want to do this option without casting
	assert.Equal(t, (err).(*errors.APIException).Code, errors.ArgumentCannotBeEmpty)
	assert.Empty(t, setInitialPasswordResponse)
}

func TestErrorLogin(t *testing.T) {
	client, mockHttpClient := utils.CreateClientAndMock()
	ctx := utils.WithRequestId(context.Background(), "requestId")
	username := "nonexistingusername"
	password := "nopassword"
	mockHttpClient.SetResponse("/api_v3/service/ottUser/action/login", loginErrorResponseFromPhoenix(), 200)
	loginResponse, err := services.NewOttUserService(client).Login(ctx, partnerId, &username, &password, nil, nil)
	var ks string
	if loginResponse != nil {
		ks = loginResponse.LoginSession.Ks
	}
	assert.Error(t, err)
	// TODO - i want to do this option without casting
	assert.Equal(t, (err).(*errors.APIException).Code, errors.WrongPasswordOrUserName)
	assert.Empty(t, ks)
}

func loginErrorResponseFromPhoenix() []byte {
	var result struct {
		Result (struct {
			Error *apierrors.APIException `json:"error"`
		}) `json:"result"`
	}
	apiException := apierrors.APIException{
		Code:    "1011",
		Message: "The username or password is not correct",
	}
	result.Result.Error = &apiException
	resultBytes, _ := json.Marshal(result)
	return resultBytes
}

func setInitialPasswordErrorWithArgsResponseFromPhoenix() []byte {
	var result struct {
		Result (struct {
			Error *apierrors.APIException `json:"error"`
		}) `json:"result"`
	}
	apiException := apierrors.APIException{
		Code:    "50027",
		Message: "Argument [token] cannot be empty",
		Args: []types.ApiExceptionArg{
			{
				Name:  "argument",
				Value: "token",
			},
		},
	}
	result.Result.Error = &apiException
	resultBytes, _ := json.Marshal(result)
	return resultBytes
}
