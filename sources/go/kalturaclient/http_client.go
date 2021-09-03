package kalturaclient

import (
	"net/http"
	"net/url"
	"time"
)

type HttpClient interface {
	Do(request *http.Request) (response *http.Response, err error)
}

func NewHttpClientFromConfig(config Configuration) HttpClient {
	tr := &http.Transport{
		MaxConnsPerHost:     config.MaxConnectionsPerHost,
		MaxIdleConns:        config.MaxIdleConnections,
		MaxIdleConnsPerHost: config.MaxConnectionsPerHost,
		IdleConnTimeout:     time.Duration(config.IdleConnectionTimeoutMs) * time.Millisecond,
	}
	var httpClient HttpClient = &http.Client{
		Transport: tr,
		Timeout:   time.Duration(config.TimeoutMs) * time.Millisecond,
	}
	return httpClient
}

func GetBaseUrl(config Configuration) string {
	var scheme string
	if config.UseHttps {
		scheme = "https"
	} else {
		scheme = "http"
	}
	var baseUrl = url.URL{
		Scheme: scheme,
		Host:   config.ServiceUrl,
	}
	return baseUrl.String()
}
