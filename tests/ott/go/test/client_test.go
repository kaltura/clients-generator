package test

import (
	"context"

	"github.com/kaltura/KalturaOttGeneratedAPIClientsGo/kalturaclient"

	"github.com/kaltura/KalturaOttGeneratedAPIClientsGo/kalturaclient/services"
	ottcontext "github.com/kaltura/ott-lib-context"
	log "github.com/kaltura/ott-lib-log"

	"testing"

	"github.com/stretchr/testify/assert"
)

const (
	systemAdminUsername string = "systemAdmin_1483"
	systemAdminPassword string = "oneboxMagic"
	adminUsername       string = "automation_1483"
	adminPassword       string = "123456"
	partnerId           int32  = 1483
)

func TestErrorLogin(t *testing.T) {
	ctx := ottcontext.WithRequestId(context.Background(), "requestId")
	_, ks, err := login(ctx, "nonexistingusername", "nopassword")
	assert.Error(t, err)
	assert.Empty(t, ks)
}

func TestPing(t *testing.T) {
	ctx := ottcontext.WithRequestId(context.Background(), "requestId")
	client, ks, err := login(ctx, systemAdminUsername, systemAdminPassword)
	assert.NoError(t, err)
	assert.NotEmpty(t, ks)
	if err != nil {
		return
	}
	pingResult, err := services.NewSystemService(client).Ping(ctx, kalturaclient.KS(ks))
	assert.NoError(t, err)
	if err != nil {
		return
	}
	assert.NotEmpty(t, pingResult)
	assert.Equal(t, true, *pingResult)
}

func TestMetaList(t *testing.T) {
	ctx := ottcontext.WithRequestId(context.Background(), "requestId")
	client, ks, err := login(ctx, adminUsername, adminPassword)
	assert.NoError(t, err)
	if err != nil {
		return
	}
	assert.NotEmpty(t, ks)
	metaListResult, err := services.NewMetaService(client).List(ctx, nil, kalturaclient.KS(ks))
	assert.NoError(t, err)
	if err != nil {
		return
	}
	assert.NotEmpty(t, metaListResult)
	assert.GreaterOrEqual(t, int(metaListResult.TotalCount), 1)
	assert.GreaterOrEqual(t, len(metaListResult.Objects), 1)
}

func login(ctx context.Context, username string, password string) (*kalturaclient.Client, string, error) {
	httpConfig := kalturaclient.Configuration{
		ServiceUrl:                "phoenix.service.consul",
		TimeoutMs:                 30000,
		MaxConnectionsPerHost:     100,
		IdleConnectionTimeoutMs:   1000,
		MaxIdleConnections:        100,
		MaxIdleConnectionsPerHost: 100,
	}
	client := kalturaclient.NewClientFromConfig(httpConfig, HeadersMiddleware, ExtraParamsMiddleware, RequestLoggingMiddleware)
	loginResponse, err := services.NewOttUserService(client).Login(ctx, partnerId, &username, &password, nil, nil)
	var ks string
	if loginResponse != nil {
		ks = loginResponse.LoginSession.Ks
	}
	return client, ks, err
}

func HeadersMiddleware(next kalturaclient.Handler) kalturaclient.Handler {
	return func(request kalturaclient.Request) ([]byte, error) {
		if requestId, ok := ottcontext.GetRequestId(request.GetContext()); ok {
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
		logger, _ := log.GetOrCreateInContext(request.GetContext())
		logger = logger.WithField("client", "phoenix")
		requestBytes, _ := request.GetBodyBytes()
		logger.Infof("request. path:[%s],headers:[%v],body:[%s]", request.GetPath(), request.GetHeaders(), string(requestBytes))
		responseBytes, err := next(request)
		if err != nil {
			logger.Errorf("response. error: %v", err)
		} else {
			logger.Infof("response. body:[%s]", string(responseBytes))
		}

		return responseBytes, err
	}
}
