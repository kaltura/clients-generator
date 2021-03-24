package test

/*
import (
	"fmt"
	"io/ioutil"
	"net/http"
	"net/http/httptest"
	"strings"
	"sync"
	"sync/atomic"
)

type MockHttpServer struct {
	Url               string
	RequestToResponse sync.Map
	server            *httptest.Server
}
type MockRequest struct {
	Url  string
	Body string
}

type MockResponse struct {
	Body  string
	Count int32
}

func StartMockHttpServer() *MockHttpServer {
	mock := &MockHttpServer{}

	server := httptest.NewServer(
		http.HandlerFunc(func(w http.ResponseWriter, r *http.Request) {
			w.Header().Add("Content-Type", "application/json")
			_, _ = w.Write([]byte(mock.GetResponse(r)))
		}),
	)

	mock.Url = strings.TrimPrefix(server.URL, "http://")
	mock.server = server
	return mock
}

func (s *MockHttpServer) SetResponse(request MockRequest, response string) {
	_, loaded := s.RequestToResponse.LoadOrStore(request, &MockResponse{Body: response, Count: 0})
	if loaded {
		panic(fmt.Sprintf("MockHttpServer request [%v] already registered", request))
	}
}

func (s *MockHttpServer) GetRequestsCount(request MockRequest) int {
	if r, ok := s.RequestToResponse.Load(request); ok {
		return int(r.(*MockResponse).Count)
	}
	return -1
}

func (s *MockHttpServer) GetResponse(r *http.Request) string {
	bytes, _ := ioutil.ReadAll(r.Body)
	body := string(bytes)

	request := MockRequest{Url: r.URL.Path, Body: body}
	if object, found := s.RequestToResponse.Load(request); found {
		response := object.(*MockResponse)
		atomic.AddInt32(&(response.Count), 1)
		return response.Body
	} else {
		return `[MockHttpServer] unknown request`
	}
}

func (s *MockHttpServer) Close() {
	s.server.Close()
}


 */