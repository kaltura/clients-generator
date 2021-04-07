package utils

import (
	"bytes"
	"errors"
	"io/ioutil"
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
	responses map[string]interface{}
}

func NewMockHttpClient() *MockHttpClient {
	mock := &MockHttpClient{
		responses: map[string]interface{}{},
	}
	return mock
}
func (p *MockHttpClient) Do(request *http.Request) (*http.Response, error) {
	response, ok := p.responses[request.URL.Path]
	if !ok {
		return nil, errors.New("no maching response")
	}
	return response.(*http.Response), nil
}

func (p *MockHttpClient) SetResponse(requestUrlPath string, response []byte, statusCode int) {
	httpResponse := &http.Response{
		StatusCode: statusCode,
		Body:       ioutil.NopCloser(bytes.NewBuffer(response)),
	}
	p.responses[requestUrlPath] = httpResponse
}
