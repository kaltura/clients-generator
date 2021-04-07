package utils

import (
	"fmt"
	"io/ioutil"
	"net/http"
	"net/http/httptest"
	"strings"
	"sync"
	"sync/atomic"
)

type MockRequest struct {
	Url  string
	Body string
}

type MockResponse struct {
	Body  []byte
	Error error
	Count int32
}

type MockHttpServer struct {
	Url               string
	RequestToResponse sync.Map
	server            *httptest.Server
}

func NewMockHttpServer() *MockHttpServer {
	mock := &MockHttpServer{}
	server := httptest.NewServer(
		http.HandlerFunc(func(w http.ResponseWriter, r *http.Request) {
			w.Header().Add("Content-Type", "application/json")
			_, _ = w.Write(mock.GetResponse(r))
		}),
	)
	mock.Url = strings.TrimPrefix(server.URL, "http://")
	mock.server = server
	return mock
}

func (s *MockHttpServer) SetResponse(request MockRequest, response []byte) {
	_, loaded := s.RequestToResponse.LoadOrStore(request.Url, &MockResponse{Body: response, Count: 0})
	if loaded {
		panic(fmt.Sprintf("MockHttpServer request [%v] already registered", request))
	}
}

func (s *MockHttpServer) GetResponse(r *http.Request) []byte {
	bytes, _ := ioutil.ReadAll(r.Body)
	body := string(bytes)

	request := MockRequest{Url: r.URL.Path, Body: body}
	if object, found := s.RequestToResponse.Load(request.Url); found {
		response := object.(*MockResponse)
		atomic.AddInt32(&(response.Count), 1)
		return response.Body
	} else {
		return []byte(`[MockHttpServer] unknown request`)
	}
}

func (p *MockHttpServer) Close() {
	p.server.Close()
}
