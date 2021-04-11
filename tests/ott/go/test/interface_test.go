package test

import (
	"testing"

	"github.com/kaltura/KalturaOttGeneratedAPIClientsGo/kalturaclient/enums/announcementrecipientstype"
	"github.com/kaltura/KalturaOttGeneratedAPIClientsGo/kalturaclient/enums/announcementstatus"
	"github.com/kaltura/KalturaOttGeneratedAPIClientsGo/kalturaclient/enums/groupbyorder"
	"github.com/kaltura/KalturaOttGeneratedAPIClientsGo/kalturaclient/enums/ruleactiontype"

	"github.com/kaltura/KalturaOttGeneratedAPIClientsGo/kalturaclient/types"
	"github.com/stretchr/testify/assert"
)

func TestAnnouncementInterface(t *testing.T) {
	status := announcementstatus.NOTSENT
	id := int32(12345)
	var announcement = types.Announcement{
		Name:         "amit Announcement",
		Message:      "amit message",
		Enabled:      true,
		StartTime:    77,
		Timezone:     "UTC",
		Status:       &status,
		Recipients:   announcementrecipientstype.ALL,
		Id:           &id,
		ImageUrl:     "image url",
		IncludeMail:  true,
		MailTemplate: "mail template",
		MailSubject:  "mail subject",
		IncludeSms:   true,
		IncludeIot:   true,
	}
	ValidateAnnouncementInterface(t, announcement, &announcement)
}

func ValidateAnnouncementInterface(t *testing.T, announcement types.Announcement, announcementInterface types.AnnouncementInterface) {
	assert.Equal(t, announcement.Name, announcementInterface.GetName())
	assert.Equal(t, announcement.Message, announcementInterface.GetMessage())
	assert.Equal(t, announcement.Enabled, announcementInterface.GetEnabled())
	assert.Equal(t, announcement.StartTime, announcementInterface.GetStartTime())
	assert.Equal(t, announcement.Timezone, announcementInterface.GetTimezone())
	assert.Equal(t, announcement.Status, announcementInterface.GetStatus())
	assert.Equal(t, announcement.Id, announcementInterface.GetId())
	assert.Equal(t, announcement.ImageUrl, announcementInterface.GetImageUrl())
	assert.Equal(t, announcement.IncludeMail, announcementInterface.GetIncludeMail())
	assert.Equal(t, announcement.MailTemplate, announcementInterface.GetMailTemplate())
	assert.Equal(t, announcement.MailSubject, announcementInterface.GetMailSubject())
	assert.Equal(t, announcement.IncludeSms, announcementInterface.GetIncludeSms())
	assert.Equal(t, announcement.IncludeIot, announcementInterface.GetIncludeIot())
}

func TestApplyDiscountModuleActionInterface(t *testing.T) {
	Type := ruleactiontype.BLOCK
	var applyDiscountModuleAction = types.ApplyDiscountModuleAction{
		DiscountModuleId: 1234,
		Type:             &Type,
		Description:      "amit",
	}
	ValidateApplyDiscountModuleActionInterface(t, applyDiscountModuleAction, &applyDiscountModuleAction)
}

func ValidateApplyDiscountModuleActionInterface(t *testing.T, applyDiscountModuleAction types.ApplyDiscountModuleAction, applyDiscountModuleActionInterface types.ApplyDiscountModuleActionInterface) {
	assert.Equal(t, applyDiscountModuleAction.DiscountModuleId, applyDiscountModuleActionInterface.GetDiscountModuleId())
	assert.Equal(t, applyDiscountModuleAction.Type, applyDiscountModuleActionInterface.GetType())
	assert.Equal(t, applyDiscountModuleAction.Description, applyDiscountModuleActionInterface.GetDescription())
}

func TestChannelFilterInterface(t *testing.T) {
	var channelFilter = types.ChannelFilter{
		IdEqual:        1234,
		ExcludeWatched: true,
		KSql:           "amit",
		GroupBy:        []types.AssetGroupByContainer{},
		GroupOrderBy:   groupbyorder.COUNT_ASC,
		DynamicOrderBy: types.DynamicOrderBy{},
		Name:           "amit test",
		OrderBy:        "orderby",
	}
	ValidateChannelFilter(t, channelFilter, &channelFilter)
}

func ValidateChannelFilter(t *testing.T, channelFilter types.ChannelFilter, channelFilterInterface types.ChannelFilterInterface) {
	assert.Equal(t, channelFilter.IdEqual, channelFilterInterface.GetIdEqual())
	assert.Equal(t, channelFilter.ExcludeWatched, channelFilterInterface.GetExcludeWatched())
	assert.Equal(t, channelFilter.KSql, channelFilterInterface.GetKSql())
	assert.Equal(t, channelFilter.GroupBy, channelFilterInterface.GetGroupBy())
	assert.Equal(t, channelFilter.GroupOrderBy, channelFilterInterface.GetGroupOrderBy())
	assert.Equal(t, channelFilter.DynamicOrderBy, channelFilterInterface.GetDynamicOrderBy())
	assert.Equal(t, channelFilter.Name, channelFilterInterface.GetName())
	assert.Equal(t, channelFilter.OrderBy, channelFilterInterface.GetOrderBy())
}
