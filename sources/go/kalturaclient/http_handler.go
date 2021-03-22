package kalturaclient

import (
	"bytes"
	"encoding/json"
	"fmt"
	"io"
	"io/ioutil"
	"net/http"
)

const (
	postMethod        = "POST"
	contentTypeHeader = "Content-Type"
	contentTypeJSON   = "application/json"
)

type HttpHandler struct {
	httpClient        HttpClient
	baseUrl           string
	defaultBodyParams map[string]interface{}
}

func NewHttpHandler(baseUrl string, client HttpClient) *HttpHandler {
	return &HttpHandler{
		httpClient: client,
		baseUrl:    baseUrl,
	}
}

func NewHttpHandlerFromConfig(config Configuration) *HttpHandler {
	return NewHttpHandler(GetBaseUrl(config), NewHttpClientFromConfig(config))
}

func (p *HttpHandler) Execute(request Request) ([]byte, error) {
	var requestUrl = p.baseUrl + "/api_v3/" + request.path
	requestBody, err := request.GetBodyBytes()
	if err != nil {
		return nil, fmt.Errorf("failed to create HTTP request: %w", err)
	}
	httpRequest, err := http.NewRequestWithContext(request.ctx, postMethod, requestUrl, bytes.NewBuffer(requestBody))
	if err != nil {
		return nil, fmt.Errorf("failed to create HTTP request: %w", err)
	}

	httpRequest.Header.Set(contentTypeHeader, contentTypeJSON)
	if request.headers != nil {
		for key, value := range request.headers {
			httpRequest.Header.Set(key, value)
		}
	}

	httpResponse, err := p.httpClient.Do(httpRequest)
	if err != nil {
		return nil, fmt.Errorf("failed to execute HTTP request: %w", err)
	}
	defer p.closeIt(httpResponse.Body)
	if httpResponse.StatusCode != 200 {
		return nil, NewBadStatusCodeError(httpResponse.StatusCode, httpResponse.Status)
	}
	byteResponse, err := ioutil.ReadAll(httpResponse.Body)
	if err != nil {
		return nil, fmt.Errorf("failed to read response body: %w", err)
	}
	apiException := p.getAPIExceptionFromResponse(byteResponse)
	return byteResponse, apiException
}

func (p *HttpHandler) getAPIExceptionFromResponse(byteResponse []byte) error {
	var apiExceptionResult APIExceptionResult
	err := json.Unmarshal(byteResponse, &apiExceptionResult)
	if err != nil {
		return fmt.Errorf("failed to parse json: %w", err)
	}
	if apiExceptionResult.Result == nil {
		return fmt.Errorf("failed to parse json: %w", err)
	}
	if apiExceptionResult.Result.Error != nil {
		return apiExceptionResult.Result.Error
	}
	return nil
}

func (p *HttpHandler) closeIt(c io.Closer) {
	if err := c.Close(); err != nil {
		// nothing
	}
}
