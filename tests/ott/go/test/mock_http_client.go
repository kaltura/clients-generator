package test

import (
	"errors"
	"net/http"
)

// HTTPClient interface
type HTTPClient interface {
	Do(req *http.Request) (*http.Response, error)
}

var (
	Client HTTPClient
)

func init() {
	Client = &http.Client{}
}

type MockHttpClient struct {
}

func NewMockHttpClient() *MockHttpClient {
	mock := &MockHttpClient{}
	return mock
}
func (p *MockHttpClient) Do(request *http.Request) (response *http.Response, err error) {
	//httpResponse, err := p.server.Client().Do(request)
	//request.URL
	print(request)
	// TODO amit
	return nil, errors.New("need todo do")
}

// mockHttpServer := utils.NewMockHttpServer()
// config := kalturaclient.Configuration{
//    ServiceUrl:                mockHttpServer.Url,
//    TimeoutMs:                 30000,
//    MaxConnectionsPerHost:     1024,
//    IdleConnectionTimeoutMs:   30000,
//    MaxIdleConnections:        1024,
//    MaxIdleConnectionsPerHost: 1024,
// }
// handler := kalturaclient.NewHttpHandler(kalturaclient.GetBaseUrl(config), utils.NewMockHttpClient())
// var client *kalturaclient.Client
// client = kalturaclient.NewClient(handler.Execute)
