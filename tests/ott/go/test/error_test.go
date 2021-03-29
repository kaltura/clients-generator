package test

import (
	"context"
	"testing"

	"github.com/kaltura/KalturaOttGeneratedAPIClientsGo/kalturaclient/errors"
	ottcontext "github.com/kaltura/ott-lib-context"
	"github.com/stretchr/testify/assert"
)

func TestErrorWithArgs(t *testing.T) {
	// TODO AMIT - passwordpolicy
}

func TestErrorLogin(t *testing.T) {
	ctx := ottcontext.WithRequestId(context.Background(), "requestId")
	_, ks, err := login(ctx, "nonexistingusername", "nopassword")
	assert.Error(t, err)
	// TODO - i want to do this option without casting
	assert.Equal(t, (err).(*errors.APIException).Code, errors.WrongPasswordOrUserName)
	assert.Empty(t, ks)
}
