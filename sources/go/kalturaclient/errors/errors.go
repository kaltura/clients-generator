package errors

import (
	"fmt"

	"github.com/kaltura/KalturaOttGeneratedAPIClientsGo/kalturaclient/types"
)

type BadStatusCodeError struct {
	StatusCode int
	Status     string
}

func NewBadStatusCodeError(statusCode int, status string) *BadStatusCodeError {
	return &BadStatusCodeError{
		StatusCode: statusCode,
		Status:     status,
	}
}

func (a *BadStatusCodeError) Error() string {
	return fmt.Sprintf("bad HTTP statusCode: %d, status: %s", a.StatusCode, a.Status)
}

type APIException struct {
	Code    string                  `json:"code"`
	Message string                  `json:"message"`
	Args    []types.ApiExceptionArg `json:"args,omitempty"`
}

func (a *APIException) Error() string {
	return fmt.Sprintf("got an error from kaltura : code: %s, message: %s", a.Code, a.Message)
}
