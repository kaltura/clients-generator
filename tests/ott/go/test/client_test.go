package test

import (
	"context"
	"encoding/json"
	"testing"

	"github.com/kaltura/KalturaOttGeneratedAPIClientsGo/kalturaclient"
	"github.com/kaltura/KalturaOttGeneratedAPIClientsGo/kalturaclient/enums/householdsuspensionstate"
	"github.com/kaltura/KalturaOttGeneratedAPIClientsGo/kalturaclient/enums/metadatatype"
	"github.com/kaltura/KalturaOttGeneratedAPIClientsGo/kalturaclient/enums/userstate"
	"github.com/kaltura/KalturaOttGeneratedAPIClientsGo/kalturaclient/errors"
	"github.com/kaltura/KalturaOttGeneratedAPIClientsGo/kalturaclient/services"
	"github.com/kaltura/KalturaOttGeneratedAPIClientsGo/kalturaclient/types"
	"github.com/kaltura/KalturaOttGeneratedAPIClientsGo/test/utils"
	"github.com/stretchr/testify/assert"
)

func TestPing(t *testing.T) {
	client, mockHttpClient := utils.CreateClientAndMock()
	mockHttpClient.SetResponse("/api_v3/service/system/action/ping", pingResponseFromPhoenix(), 200)
	ctx := utils.WithRequestId(context.Background(), "requestId")
	pingResult, err := services.NewSystemService(client).Ping(ctx)
	assert.NoError(t, err)
	if err != nil {
		return
	}
	assert.NotEmpty(t, pingResult)
	assert.Equal(t, true, *pingResult)
}

func TestMetaListWithMiddlewares(t *testing.T) {
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
	assert.GreaterOrEqual(t, int(metaListResult.TotalCount), 1)
	assert.GreaterOrEqual(t, len(metaListResult.Objects), 1)
}

func TestErrorWithArgs(t *testing.T) {
	client, mockHttpClient := utils.CreateClientAndMock()
	ctx := utils.WithRequestId(context.Background(), "requestId")
	password := "nopassword"
	token := ""
	partnerId := int32(1483)
	mockHttpClient.SetResponse("/api_v3/service/ottUser/action/setInitialPassword", errorWithArgsResponseFromPhoenix(), 200)
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
	mockHttpClient.SetResponse("/api_v3/service/ottUser/action/login", errorResponseFromPhoenix(), 200)
	partnerId := int32(1483)
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

func loginResponseFromPhoenix() []byte {
	var result struct {
		Result *types.LoginResponse `json:"result"`
	}

	NOT_SUSPENDED := householdsuspensionstate.NOT_SUSPENDED
	OK := userstate.OK

	loginResponse := types.LoginResponse{
		LoginSession: types.LoginSession{
			Ks: "sudfjksdfsjdgf",
		},
		User: types.OTTUser{
			SuspensionState: &NOT_SUSPENDED,
			UserState:       &OK,
		},
	}
	result.Result = &loginResponse
	resultBytes, _ := json.Marshal(result)
	return resultBytes
}

func errorWithArgsResponseFromPhoenix() []byte {
	var result struct {
		Result (struct {
			Error *errors.APIException `json:"error"`
		}) `json:"result"`
	}
	apiException := errors.APIException{
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

func errorResponseFromPhoenix() []byte {
	var result struct {
		Result (struct {
			Error *errors.APIException `json:"error"`
		}) `json:"result"`
	}
	apiException := errors.APIException{
		Code:    "1011",
		Message: "The username or password is not correct",
	}
	result.Result.Error = &apiException
	resultBytes, _ := json.Marshal(result)
	return resultBytes
}

func pingResponseFromPhoenix() []byte {
	var result struct {
		Result bool `json:"result"`
	}

	result.Result = bool(true)
	resultBytes, _ := json.Marshal(result)
	return resultBytes
}

func metaListResponseFromPhoenix() []byte {
	var result struct {
		Result *types.MetaListResponse `json:"result"`
	}

	STRING := metadatatype.STRING
	Id := "1234"
	Name := "Amit meta"
	SystemName := "SystemName Amit"
	CreateDate := int64(77)
	UpdateDate := int64(7777)
	IsProtected := true

	loginResponse := types.MetaListResponse{
		Objects: []types.Meta{
			{
				Id:               &Id,
				Name:             &Name,
				MultilingualName: []types.TranslationToken{},
				SystemName:       &SystemName,
				DataType:         &STRING,
				MultipleValue:    true,
				IsProtected:      &IsProtected,
				HelpText:         "Text that helps",
				Features:         "Aamazing features",
				ParentId:         "12345",
				CreateDate:       &CreateDate,
				UpdateDate:       &UpdateDate,
				DynamicData:      nil,
			},
		},
		TotalCount: 1,
	}
	result.Result = &loginResponse
	resultBytes, _ := json.Marshal(result)
	return resultBytes
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
		logger := utils.GetLogFromContext(request.GetContext())
		contextLogger := logger.WithField("client", "phoenix")
		requestBytes, _ := request.GetBodyBytes()
		contextLogger.Infof("request. path:[%s],headers:[%v],body:[%s]", request.GetPath(), request.GetHeaders(), string(requestBytes))
		responseBytes, err := next(request)
		if err != nil {
			contextLogger.Errorf("response. error: %v", err)
		} else {
			contextLogger.Infof("response. body:[%s]", string(responseBytes))
		}

		return responseBytes, err
	}
}
