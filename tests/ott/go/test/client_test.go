package test

import (
	"context"
	"log"
	"testing"

	"github.com/kaltura/KalturaOttGeneratedAPIClientsGo/kalturaclient"
	"github.com/kaltura/KalturaOttGeneratedAPIClientsGo/kalturaclient/enums/metadatatype"
	"github.com/kaltura/KalturaOttGeneratedAPIClientsGo/kalturaclient/errors"
	"github.com/kaltura/KalturaOttGeneratedAPIClientsGo/kalturaclient/services"
	"github.com/kaltura/KalturaOttGeneratedAPIClientsGo/kalturaclient/types"
	"github.com/kaltura/KalturaOttGeneratedAPIClientsGo/test/utils"
	"github.com/stretchr/testify/assert"
)

func TestPing(t *testing.T) {
	t.Parallel()
	client, mockHttpClient := utils.CreateClientAndMock()
	mockHttpClient.SetResponse("/api_v3/service/system/action/ping", pingResponseFromPhoenix(), 200)
	ctx := utils.WithRequestId(context.Background(), "requestId")
	pingResult, err := services.NewSystemService(client).Ping(ctx)
	if assert.NoError(t, err) {
		if assert.NotEmpty(t, pingResult) {
			assert.Equal(t, true, *pingResult)
		}
	}
}

func TestVoid(t *testing.T) {
	t.Parallel()
	client, mockHttpClient := utils.CreateClientAndMock()
	mockHttpClient.SetResponse("/api_v3/service/permission/action/delete", voidResponseFromPhoenix(), 200)
	ctx := utils.WithRequestId(context.Background(), "requestId")
	err := services.NewPermissionService(client).Delete(ctx, 1)
	assert.NoError(t, err)
}
func TestMetaListWithMiddlewares(t *testing.T) {
	t.Parallel()
	ctx := utils.WithRequestId(context.Background(), "requestId")
	config := utils.CreateTestConfig()
	mockHttpClient := utils.NewMockHttpClient()
	httpHandler := kalturaclient.NewHttpHandler(kalturaclient.GetBaseUrl(config), mockHttpClient)
	var client *kalturaclient.Client
	middleware := kalturaclient.Middlewares(HeadersMiddleware, ExtraParamsMiddleware, RequestLoggingMiddleware)
	client = kalturaclient.NewClient(middleware(httpHandler.Execute))
	mockHttpClient.SetResponse("/api_v3/service/ottUser/action/login", loginResponseFromPhoenix(), 200)
	username := "someusername"
	password := "somepassword"
	partnerId := int32(1483)
	loginResponse, err := services.NewOttUserService(client).Login(ctx, partnerId, &username, &password, nil, nil)
	var ks string
	if loginResponse != nil {
		ks = loginResponse.LoginSession.Ks
	}
	assert.NoError(t, err)
	if err != nil {
		return
	}
	assert.NotEmpty(t, ks)
	filter := types.MetaFilter{
		DataTypeEqual: metadatatype.DATE,
	}
	metaService := services.NewMetaService(client)
	mockHttpClient.SetResponse("/api_v3/service/meta/action/list", metaListResponseFromPhoenix(), 200)
	metaListResult, err := metaService.List(ctx, &filter, kalturaclient.KS(ks))
	assert.NoError(t, err)
	if err != nil {
		return
	}
	assert.NotEmpty(t, metaListResult)
	assert.Equal(t, int(metaListResult.TotalCount), 1)
	assert.Equal(t, len(metaListResult.Objects), 1)
}

func TestErrorWithArgs(t *testing.T) {
	t.Parallel()
	client, mockHttpClient := utils.CreateClientAndMock()
	ctx := utils.WithRequestId(context.Background(), "requestId")
	password := "nopassword"
	token := ""
	mockHttpClient.SetResponse("/api_v3/service/ottUser/action/setInitialPassword", errorWithArgsResponseFromPhoenix(), 200)
	partnerId := int32(1483)
	setInitialPasswordResponse, err := services.NewOttUserService(client).SetInitialPassword(ctx, partnerId, token, password)
	assert.Error(t, err)
	expectedError := getAPIExceptionWithArgs()

	assert.Equal(t, (err).(*errors.APIException).Code, errors.ArgumentCannotBeEmpty)
	assert.Empty(t, setInitialPasswordResponse)
	assert.Equal(t, &expectedError, err)
	t.Log("End of TestErrorWithArgs")
}

func TestErrorLogin(t *testing.T) {
	t.Parallel()
	client, mockHttpClient := utils.CreateClientAndMock()
	ctx := utils.WithRequestId(context.Background(), "requestId")
	username := "nonexistingusername"
	password := "nopassword"
	mockHttpClient.SetResponse("/api_v3/service/ottUser/action/login", errorResponseFromPhoenix(), 200)
	partnerId := int32(1483)
	loginResponse, err := services.NewOttUserService(client).Login(ctx, partnerId, &username, &password, nil, nil)
	var ks string
	if loginResponse != nil {
		ks = loginResponse.LoginSession.Ks
	}
	assert.Error(t, err)
	expectedError := getAPIException()

	assert.Equal(t, (err).(*errors.APIException).Code, errors.WrongPasswordOrUserName)
	assert.Empty(t, ks)
	assert.Equal(t, &expectedError, err)
	t.Log("End of TestErrorLogin")
}

func loginResponseFromPhoenix() []byte {
	return []byte(`{"result": {
		"loginSession": {
		  "expiry": 0,
		  "ks": "sudfjksdfsjdgf"
		},
		"user": {
		  "firstName": "",
		  "id": "",
		  "lastName": "",
		  "objectType": "KalturaBaseOTTUser",
		  "username": "",
		  "suspensionState": "NOT_SUSPENDED",
		  "userState": "ok",
		  "userType": {
			"description": "",
			"id": 0
		  }
		}
	  }}`)
}

func errorWithArgsResponseFromPhoenix() []byte {
	return []byte(`{ "result": {
		"error": {
			"code": "50027",
			"message": "Argument [token] cannot be empty",
			"args": [
						{
							"name":  "argument",
							"value": "token"
						}
					]
			}
		}
	}
	`)
}
func getAPIExceptionWithArgs() errors.APIException {
	return errors.APIException{
		Code:    "50027",
		Message: "Argument [token] cannot be empty",
		Args: []types.ApiExceptionArg{
			{
				Name:  "argument",
				Value: "token",
			},
		},
	}
}

func errorResponseFromPhoenix() []byte {
	return []byte(`{ "result": {
		"error": {
			"code": "1011",
			"message": "The username or password is not correct"
			}
		}
	}
	`)
}

func getAPIException() errors.APIException {
	return errors.APIException{
		Code:    "1011",
		Message: "The username or password is not correct",
	}
}

func pingResponseFromPhoenix() []byte {
	return []byte(`{"result": true
	  }`)
}

func voidResponseFromPhoenix() []byte {
	return []byte(`{"result": {}
	  }`)
}

func metaListResponseFromPhoenix() []byte {
	return []byte(`{"result": {
		"objects": [
			{
				"id": "1234",
				"name": "Amit meta",
				"systemname": "SystemName Amit",
				"datatype": "STRING",
				"multiplevalue": true,
				"isprotected": true,
				"helptext": "Text that helps",
				"features": "Aamazing features",
				"parentid": "12345",
				"createdate": 77,
				"updatedate": 7777
			}
		],
		"totalcount": 1
	  }}`)
}

func HeadersMiddleware(next kalturaclient.Handler) kalturaclient.Handler {
	return func(request kalturaclient.Request) ([]byte, error) {
		if requestId, ok := utils.GetRequestId(request.GetContext()); ok {
			requestIdParam := kalturaclient.RequestId(requestId)
			request, _ = request.WithParam(requestIdParam)
		}
		return next(request)
	}
}

func ExtraParamsMiddleware(next kalturaclient.Handler) kalturaclient.Handler {
	return func(request kalturaclient.Request) ([]byte, error) {
		request, _ = request.WithParam(kalturaclient.Language("eng"))
		return next(request)
	}
}

func RequestLoggingMiddleware(next kalturaclient.Handler) kalturaclient.Handler {
	return func(request kalturaclient.Request) ([]byte, error) {
		requestBytes, _ := request.GetBodyBytes()
		log.Printf("INFO - request. path:[%s],headers:[%v],body:[%s]", request.GetPath(), request.GetHeaders(), string(requestBytes))
		responseBytes, err := next(request)
		if err != nil {
			log.Printf("Error - response. error: %v", err)
		} else {
			log.Printf("INFO - response. body:[%s]", string(responseBytes))
		}

		return responseBytes, err
	}
}
