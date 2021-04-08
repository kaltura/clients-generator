package test

import (
	"testing"

	"github.com/kaltura/KalturaOttGeneratedAPIClientsGo/kalturaclient/types"
	"github.com/stretchr/testify/assert"
)

func TestAnnouncementInterface(t *testing.T) {
	//TODO AMIT - TEST THAT INTERFACE IMPLEMENTE GETTERS CORRECT FOR ALL PROPERTIES
	//STEPS:
	// CREATE Announcement object and fill all proprs - announcement
	//ValidateAnnouncementInterface(t, announcement, announcement)
}

func ValidateAnnouncementInterface(t *testing.T, announcement types.Announcement, announcementInterface types.AnnouncementInterface) {
	assert.Equal(t, announcement.Name, announcementInterface.GetName())
	assert.Equal(t, announcement.Message, announcementInterface.GetMessage())
	assert.Equal(t, announcement.Enabled, announcementInterface.GetEnabled())
	assert.Equal(t, announcement.StartTime, announcementInterface.GetStartTime())
	// TODO AMIT - ALL PROPERTIES
}

func TestApplyDiscountModuleActionInterface(t *testing.T) {
	//TODO AMIT - TEST THAT INTERFACE IMPLEMENTE GETTERS CORRECT FOR ALL PROPERTIES and inheritence of them!
	//STEPS:
	// CREATE Announcement object and fill all proprs - applyDiscountModuleAction
	//ValidateApplyDiscountModuleActionInterface(t, applyDiscountModuleAction, applyDiscountModuleAction)
}

func TestChannelFilterInterface(t *testing.T) {
	//TODO AMIT - TEST THAT INTERFACE IMPLEMENTE GETTERS CORRECT FOR ALL PROPERTIES and inheritence of them!
	//STEPS:
	// CREATE ChannelFilter object and fill all proprs - channelFilter
	//ValidateChannelFilterInterface(t, channelFilter, channelFilter)
}
