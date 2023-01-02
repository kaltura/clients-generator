package json

import (
	json_ "encoding/json"
)

type MarshalFn func(v interface{}) ([]byte, error)
type UnmarshalFn func(data []byte, v interface{}) error

var (
	Marshal   MarshalFn   = json_.Marshal
	Unmarshal UnmarshalFn = json_.Unmarshal
)
