package utils

import (
	"context"

	"github.com/kaltura/KalturaOttGeneratedAPIClientsGo/kalturaclient"
	log "github.com/sirupsen/logrus"
)

const (
	_requestIdKey string = "test-key-requestId"
	_loggerKey    string = "test-key-logger"
)

func GetLogFromContext(ctx context.Context) *log.Logger {
	l, ok := ctx.Value(_loggerKey).(*log.Logger)
	if !ok {
		l = log.New()
	}
	return l
}

func GetRequestId(ctx context.Context) (string, bool) {
	val, ok := ctx.Value(_requestIdKey).(string)
	return val, ok
}

func WithRequestId(c context.Context, requestId string) context.Context {
	return context.WithValue(c, _requestIdKey, requestId)
}

func CreateClientAndMock() (*kalturaclient.Client, *MockHttpClient) {
	config := kalturaclient.Configuration{
		ServiceUrl:                "test.com",
		TimeoutMs:                 30000,
		MaxConnectionsPerHost:     1024,
		IdleConnectionTimeoutMs:   30000,
		MaxIdleConnections:        1024,
		MaxIdleConnectionsPerHost: 1024,
	}
	mockHttpClient := NewMockHttpClient()
	httpHandler := kalturaclient.NewHttpHandler(kalturaclient.GetBaseUrl(config), mockHttpClient)
	var client *kalturaclient.Client
	client = kalturaclient.NewClient(httpHandler.Execute)

	return client, mockHttpClient
}
