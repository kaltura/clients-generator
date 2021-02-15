// ===================================================================================================
//                           _  __     _ _
//                          | |/ /__ _| | |_ _  _ _ _ __ _
//                          | ' </ _` | |  _| || | '_/ _` |
//                          |_|\_\__,_|_|\__|\_,_|_| \__,_|
//
// This file is part of the Kaltura Collaborative Media Suite which allows users
// to do with audio, video, and animation what Wiki platfroms allow them to do with
// text.
//
// Copyright (C) 2006-2021  Kaltura Inc.
//
// This program is free software: you can redistribute it and/or modify
// it under the terms of the GNU Affero General Public License as
// published by the Free Software Foundation, either version 3 of the
// License, or (at your option) any later version.
//
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU Affero General Public License for more details.
//
// You should have received a copy of the GNU Affero General Public License
// along with this program.  If not, see <http://www.gnu.org/licenses/>.
//
// @ignore
// ===================================================================================================
using System;
using System.Xml;
using System.Runtime.Serialization;
using System.Text.RegularExpressions;
using Kaltura.Types;
using Newtonsoft.Json.Linq;

namespace Kaltura
{
	public static class ObjectFactory
	{
		private static Regex prefixRegex = new Regex("^Kaltura");
		
		public static T Create<T>(JToken jToken) where T : ObjectBase
		{
			if (jToken["objectType"] == null)
			{
				return null;
			}
				
			string className = jToken["objectType"].Value<string>();
			className = prefixRegex.Replace(className, "");
			
			Type type = Type.GetType("Kaltura.Types." + className);
			if (type == null)
			{
				type = typeof(T);
			}
			
			if (type == null)
				throw new SerializationException("Invalid object type");
			
			return (T)System.Activator.CreateInstance(type, jToken);
		}
		
		public static IListResponse Create(JToken jToken)
		{
			if (jToken["objectType"] == null)
			{
				return null;
			}
			
			string className = jToken["objectType"].Value<string>();
			switch (className)
			{
				case "KalturaAnnouncementListResponse":
					return new ListResponse<Announcement>(jToken);
				case "KalturaFavoriteListResponse":
					return new ListResponse<Favorite>(jToken);
				case "KalturaOTTUserListResponse":
					return new ListResponse<OTTUser>(jToken);
				case "KalturaSSOAdapterProfileListResponse":
					return new ListResponse<SSOAdapterProfile>(jToken);
				case "KalturaUserInterestListResponse":
					return new ListResponse<UserInterest>(jToken);
				case "KalturaBulkUploadListResponse":
					return new ListResponse<BulkUpload>(jToken);
				case "KalturaSocialActionListResponse":
					return new ListResponse<SocialAction>(jToken);
				case "KalturaSocialCommentListResponse":
					return new ListResponse<SocialComment>(jToken);
				case "KalturaSocialFriendActivityListResponse":
					return new ListResponse<SocialFriendActivity>(jToken);
				case "KalturaSegmentationTypeListResponse":
					return new ListResponse<SegmentationType>(jToken);
				case "KalturaUserSegmentListResponse":
					return new ListResponse<UserSegment>(jToken);
				case "KalturaAssetFilePpvListResponse":
					return new ListResponse<AssetFilePpv>(jToken);
				case "KalturaCollectionListResponse":
					return new ListResponse<Collection>(jToken);
				case "KalturaCouponsGroupListResponse":
					return new ListResponse<CouponsGroup>(jToken);
				case "KalturaDiscountDetailsListResponse":
					return new ListResponse<DiscountDetails>(jToken);
				case "KalturaPpvListResponse":
					return new ListResponse<Ppv>(jToken);
				case "KalturaPriceDetailsListResponse":
					return new ListResponse<PriceDetails>(jToken);
				case "KalturaPricePlanListResponse":
					return new ListResponse<PricePlan>(jToken);
				case "KalturaProductPriceListResponse":
					return new ListResponse<ProductPrice>(jToken);
				case "KalturaProductsPriceListResponse":
					return new ListResponse<ProductPrice>(jToken);
				case "KalturaSubscriptionListResponse":
					return new ListResponse<Subscription>(jToken);
				case "KalturaSubscriptionSetListResponse":
					return new ListResponse<SubscriptionSet>(jToken);
				case "KalturaPartnerConfigurationListResponse":
					return new ListResponse<PartnerConfiguration>(jToken);
				case "KalturaPersonalListListResponse":
					return new ListResponse<PersonalList>(jToken);
				case "KalturaEngagementAdapterListResponse":
					return new ListResponse<EngagementAdapter>(jToken);
				case "KalturaEngagementListResponse":
					return new ListResponse<Engagement>(jToken);
				case "KalturaInboxMessageListResponse":
					return new ListResponse<InboxMessage>(jToken);
				case "KalturaFollowTvSeriesListResponse":
					return new ListResponse<FollowTvSeries>(jToken);
				case "KalturaPersonalFeedListResponse":
					return new ListResponse<PersonalFeed>(jToken);
				case "KalturaReminderListResponse":
					return new ListResponse<Reminder>(jToken);
				case "KalturaTopicListResponse":
					return new ListResponse<Topic>(jToken);
				case "KalturaTopicNotificationListResponse":
					return new ListResponse<TopicNotification>(jToken);
				case "KalturaTopicNotificationMessageListResponse":
					return new ListResponse<TopicNotificationMessage>(jToken);
				case "KalturaGenericListResponse":
					return new ListResponse<T>(jToken);
				case "KalturaIntegerValueListResponse":
					return new ListResponse<IntegerValue>(jToken);
				case "KalturaReportListResponse":
					return new ListResponse<Report>(jToken);
				case "KalturaHomeNetworkListResponse":
					return new ListResponse<HomeNetwork>(jToken);
				case "KalturaHouseholdDeviceListResponse":
					return new ListResponse<HouseholdDevice>(jToken);
				case "KalturaHouseholdLimitationsListResponse":
					return new ListResponse<HouseholdLimitations>(jToken);
				case "KalturaHouseholdUserListResponse":
					return new ListResponse<HouseholdUser>(jToken);
				case "KalturaConfigurationGroupDeviceListResponse":
					return new ListResponse<ConfigurationGroupDevice>(jToken);
				case "KalturaConfigurationGroupListResponse":
					return new ListResponse<ConfigurationGroup>(jToken);
				case "KalturaConfigurationGroupTagListResponse":
					return new ListResponse<ConfigurationGroupTag>(jToken);
				case "KalturaConfigurationsListResponse":
					return new ListResponse<Configurations>(jToken);
				case "KalturaBillingTransactionListResponse":
					return new ListResponse<BillingTransaction>(jToken);
				case "KalturaCDVRAdapterProfileListResponse":
					return new ListResponse<CDVRAdapterProfile>(jToken);
				case "KalturaEntitlementListResponse":
					return new ListResponse<Entitlement>(jToken);
				case "KalturaHouseholdPremiumServiceListResponse":
					return new ListResponse<HouseholdPremiumService>(jToken);
				case "KalturaRecordingListResponse":
					return new ListResponse<Recording>(jToken);
				case "KalturaSeriesRecordingListResponse":
					return new ListResponse<SeriesRecording>(jToken);
				case "KalturaAssetCommentListResponse":
					return new ListResponse<AssetComment>(jToken);
				case "KalturaAssetCountListResponse":
					return new ListResponse<AssetsCount>(jToken);
				case "KalturaAssetListResponse":
					return new ListResponse<Asset>(jToken);
				case "KalturaAssetStatisticsListResponse":
					return new ListResponse<AssetStatistics>(jToken);
				case "KalturaAssetStructListResponse":
					return new ListResponse<AssetStruct>(jToken);
				case "KalturaAssetStructMetaListResponse":
					return new ListResponse<AssetStructMeta>(jToken);
				case "KalturaBookmarkListResponse":
					return new ListResponse<Bookmark>(jToken);
				case "KalturaChannelListResponse":
					return new ListResponse<Channel>(jToken);
				case "KalturaImageListResponse":
					return new ListResponse<Image>(jToken);
				case "KalturaImageTypeListResponse":
					return new ListResponse<ImageType>(jToken);
				case "KalturaMediaFileListResponse":
					return new ListResponse<MediaFile>(jToken);
				case "KalturaMediaFileTypeListResponse":
					return new ListResponse<MediaFileType>(jToken);
				case "KalturaRatioListResponse":
					return new ListResponse<Ratio>(jToken);
				case "KalturaStreamingDeviceListResponse":
					return new ListResponse<StreamingDevice>(jToken);
				case "KalturaTagListResponse":
					return new ListResponse<Tag>(jToken);
				case "KalturaAssetHistoryListResponse":
					return new ListResponse<AssetHistory>(jToken);
				case "KalturaHouseholdPaymentGatewayListResponse":
					return new ListResponse<HouseholdPaymentGateway>(jToken);
				case "KalturaHouseholdPaymentMethodListResponse":
					return new ListResponse<HouseholdPaymentMethod>(jToken);
				case "KalturaPaymentGatewayProfileListResponse":
					return new ListResponse<PaymentGatewayProfile>(jToken);
				case "KalturaPaymentMethodProfileListResponse":
					return new ListResponse<PaymentMethodProfile>(jToken);
				case "KalturaAssetRuleListResponse":
					return new ListResponse<AssetRule>(jToken);
				case "KalturaAssetUserRuleListResponse":
					return new ListResponse<AssetUserRule>(jToken);
				case "KalturaBusinessModuleRuleListResponse":
					return new ListResponse<BusinessModuleRule>(jToken);
				case "KalturaCDNAdapterProfileListResponse":
					return new ListResponse<CDNAdapterProfile>(jToken);
				case "KalturaCountryListResponse":
					return new ListResponse<Country>(jToken);
				case "KalturaCurrencyListResponse":
					return new ListResponse<Currency>(jToken);
				case "KalturaDeviceBrandListResponse":
					return new ListResponse<DeviceBrand>(jToken);
				case "KalturaDeviceFamilyListResponse":
					return new ListResponse<DeviceFamily>(jToken);
				case "KalturaDrmProfileListResponse":
					return new ListResponse<DrmProfile>(jToken);
				case "KalturaExportTaskListResponse":
					return new ListResponse<ExportTask>(jToken);
				case "KalturaExternalChannelProfileListResponse":
					return new ListResponse<ExternalChannelProfile>(jToken);
				case "KalturaIngestProfileListResponse":
					return new ListResponse<IngestProfile>(jToken);
				case "KalturaLanguageListResponse":
					return new ListResponse<Language>(jToken);
				case "KalturaMediaConcurrencyRuleListResponse":
					return new ListResponse<MediaConcurrencyRule>(jToken);
				case "KalturaMetaListResponse":
					return new ListResponse<Meta>(jToken);
				case "KalturaOSSAdapterProfileListResponse":
					return new ListResponse<OSSAdapterProfile>(jToken);
				case "KalturaParentalRuleListResponse":
					return new ListResponse<ParentalRule>(jToken);
				case "KalturaPermissionListResponse":
					return new ListResponse<Permission>(jToken);
				case "KalturaPlaybackProfileListResponse":
					return new ListResponse<PlaybackProfile>(jToken);
				case "KalturaRecommendationProfileListResponse":
					return new ListResponse<RecommendationProfile>(jToken);
				case "KalturaRegionListResponse":
					return new ListResponse<Region>(jToken);
				case "KalturaRegistrySettingsListResponse":
					return new ListResponse<RegistrySettings>(jToken);
				case "KalturaSearchHistoryListResponse":
					return new ListResponse<SearchHistory>(jToken);
				case "KalturaTvmRuleListResponse":
					return new ListResponse<TvmRule>(jToken);
				case "KalturaUserAssetRuleListResponse":
					return new ListResponse<UserAssetRule>(jToken);
				case "KalturaUserRoleListResponse":
					return new ListResponse<UserRole>(jToken);
				case "KalturaEpgListResponse":
					return new ListResponse<Epg>(jToken);
				case "KalturaCampaignListResponse":
					return new ListResponse<Campaign>(jToken);
				case "KalturaCategoryItemListResponse":
					return new ListResponse<CategoryItem>(jToken);
				case "KalturaCategoryVersionListResponse":
					return new ListResponse<CategoryVersion>(jToken);
				case "KalturaCouponListResponse":
					return new ListResponse<Coupon>(jToken);
				case "KalturaDeviceReferenceDataListResponse":
					return new ListResponse<DeviceReferenceData>(jToken);
				case "KalturaDynamicListListResponse":
					return new ListResponse<DynamicList>(jToken);
				case "KalturaEventNotificationListResponse":
					return new ListResponse<EventNotification>(jToken);
				case "KalturaHouseholdListResponse":
					return new ListResponse<Household>(jToken);
				case "KalturaHouseholdCouponListResponse":
					return new ListResponse<HouseholdCoupon>(jToken);
				case "KalturaHouseholdSegmentListResponse":
					return new ListResponse<HouseholdSegment>(jToken);
				case "KalturaPasswordPolicyListResponse":
					return new ListResponse<PasswordPolicy>(jToken);
				case "KalturaPermissionItemListResponse":
					return new ListResponse<PermissionItem>(jToken);
				case "KalturaSmsAdapterProfileListResponse":
					return new ListResponse<SmsAdapterProfile>(jToken);
			}
		
			return null;
		}
	}
}

