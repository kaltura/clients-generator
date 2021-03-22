package kalturaclient

import (
	"context"
	"encoding/json"
	"errors"
)

type Request struct {
	ctx     context.Context
	path    string
	body    map[string]interface{}
	headers map[string]string
}

var emptyParamKeyError = errors.New("empty key")
var alreadyExistsError = errors.New("parameter already exists")
var defaultBodyParams = map[string]interface{}{
	"clientTag":  "go:12-03-21",
	"apiVersion": "6.1.0.28909",
	"format":     "1",
	"language":   "*",
}

func NewRequest(ctx context.Context, path string, body map[string]interface{}) Request {
	addDefaultParams(body)
	return Request{
		ctx:     ctx,
		path:    path,
		body:    body,
		headers: make(map[string]string),
	}
}

func (r Request) GetContext() context.Context {
	return r.ctx
}

func (r Request) GetPath() string {
	return r.path
}

func (r Request) GetHeaders() map[string]string {
	return r.headers
}

func (r Request) GetBodyBytes() ([]byte, error) {
	return json.Marshal(r.body)
}

func (r Request) WithParamForce(param Param) Request {
	if param.key == "" {
		return r
	}
	if param.inBody {
		r.body[param.key] = param.value
	} else {
		r.headers[param.key] = param.value.(string)
	}

	return r
}

func (r Request) WithParam(param Param) (Request, error) {
	if param.key == "" {
		return r, emptyParamKeyError
	}

	if param.inBody {
		if _, exists := r.body[param.key]; !exists {
			r.body[param.key] = param.value
		} else {
			return r, alreadyExistsError
		}
	} else {
		if _, exists := r.headers[param.key]; !exists {
			r.headers[param.key] = param.value.(string)
		} else {
			return r, alreadyExistsError
		}
	}

	return r, nil
}

func addDefaultParams(request map[string]interface{}) {
	for key, value := range defaultBodyParams {
		if _, exists := request[key]; !exists {
			request[key] = value
		}
	}
}
