package test

import (
	"context"
	"github.com/kaltura/KalturaOttGeneratedAPIClientsGo/kalturaclient"

	ottcontext "github.com/kaltura/ott-lib-context"
	log "github.com/kaltura/ott-lib-log"
	"github.com/kaltura/KalturaOttGeneratedAPIClientsGo/kalturaclient/services"

	"github.com/stretchr/testify/assert"
	"testing"
)

func TestLogin(t *testing.T) {
	t.Parallel()

	httpConfig := kalturaclient.Configuration{
		ServiceUrl:                "tcm.service.consul",
		TimeoutMs:                 30000,
		MaxConnectionsPerHost:     100,
		IdleConnectionTimeoutMs:   1000,
		MaxIdleConnections:        100,
		MaxIdleConnectionsPerHost: 100,
	}
	client := kalturaclient.NewClientFromConfig(httpConfig, HeadersMiddleware, ExtraParamsMiddleware, RequestLoggingMiddleware)
	ctx := ottcontext.WithRequestId(context.Background(), "requestId")

	username := "automation_1483"
	password := "123456"
	loginResponse, err := services.NewOttUserService(client).Login(ctx, 1483, &username, &password, nil, nil)
	assert.NoError(t, err)
	assert.NotEmpty(t, loginResponse.LoginSession.Ks)
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