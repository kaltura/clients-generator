package test

import (
	log "github.com/kaltura/ott-lib-log"
	"github.com/kaltura/ott-service-partner-accounts-setup/logic/tempexternal/kalturaclient"
	"github.com/kaltura/ott-service-partner-accounts-setup/logic/tempexternal/kalturaclient/services"

	"testing"

	"github.com/stretchr/testify/assert"
)

func TestLogin(t *testing.T) {
	configuration := kalturaclient.NewConfiguration("tcm.service.consul")
	logger := log.NewLogger()
	client := kalturaclient.NewClient(*configuration, logger)
	username := "@username@"
	password := "@password@"
	loginResponse, err := services.NewOttUserService(client).Login(1483, username, password)
	assert.NoError(t, err)
	assert.NotEmpty(t, loginResponse.LoginSession.Ks)
}
