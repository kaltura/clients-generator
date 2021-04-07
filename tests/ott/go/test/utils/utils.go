package utils

import (
	"context"

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
