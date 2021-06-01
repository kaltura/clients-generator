package kalturaclient

import "context"

type Client struct {
	handler Handler
}

type Handler func(request Request) ([]byte, error)

func NewClient(handler Handler) *Client {
	return &Client{
		handler: handler,
	}
}

func NewClientFromConfig(config Configuration, middlewares ...Middleware) *Client {
	handler := NewHttpHandlerFromConfig(config)

	var client *Client
	if len(middlewares) > 0 {
		middleware := Middlewares(middlewares...)
		client = NewClient(middleware(handler.Execute))
	} else {
		client = NewClient(handler.Execute)
	}

	return client
}

func (p *Client) Execute(ctx context.Context, path string, body map[string]interface{}, extraParams []Param) ([]byte, error) {
	request := NewRequest(ctx, path, body)
	for _, p := range extraParams {
		request = request.WithParamForce(p)
	}

	return p.handler(request)
}
