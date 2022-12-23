package json

import (
	json_ "encoding/json"
)

func Marshal(v interface{}) ([]byte, error) {
	return json_.Marshal(v)
}

func Unmarshal(data []byte, v interface{}) error {
	return json_.Unmarshal(data, v)
}
