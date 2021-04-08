package test

import (
	"context"
	"encoding/json"
	"testing"

	"github.com/kaltura/KalturaOttGeneratedAPIClientsGo/kalturaclient"
	"github.com/kaltura/KalturaOttGeneratedAPIClientsGo/kalturaclient/errors"
	apierrors "github.com/kaltura/KalturaOttGeneratedAPIClientsGo/kalturaclient/errors"
	"github.com/kaltura/KalturaOttGeneratedAPIClientsGo/kalturaclient/services"
	"github.com/kaltura/KalturaOttGeneratedAPIClientsGo/test/utils"
	"github.com/stretchr/testify/assert"
)

func TestErrorWithArgs(t *testing.T) {
	// TODO AMIT - passwordpolicy
}

func TestErrorLogin(t *testing.T) {
	ctx := utils.WithRequestId(context.Background(), "requestId")
	config := kalturaclient.Configuration{
		ServiceUrl:                "test.com",
		TimeoutMs:                 30000,
		MaxConnectionsPerHost:     1024,
		IdleConnectionTimeoutMs:   30000,
		MaxIdleConnections:        1024,
		MaxIdleConnectionsPerHost: 1024,
	}
	mockHttpClient := utils.NewMockHttpClient()
	httpHandler := kalturaclient.NewHttpHandler(kalturaclient.GetBaseUrl(config), mockHttpClient)
	var client *kalturaclient.Client
	client = kalturaclient.NewClient(httpHandler.Execute)
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
