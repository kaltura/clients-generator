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
// Copyright (C) 2006-2020  Kaltura Inc.
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

namespace Kaltura
{
	public class APIException : ApplicationException
	{
		#region Error codes
		public static string DomainAlreadyExists = "1000";
		public static string ExceededLimit = "1001";
		public static string DeviceTypeNotAllowed = "1002";
		public static string DeviceNotInDomain = "1003";
		public static string MasterEmailAlreadyExists = "1004";
		public static string UserNotInDomain = "1005";
		public static string DomainNotExists = "1006";
		public static string HouseholdUserFailed = "1007";
		public static string DomainCreatedWithoutNPVRAccount = "1008";
		public static string DomainSuspended = "1009";
		public static string DlmNotExist = "1010";
		public static string WrongPasswordOrUserName = "1011";
		public static string DomainAlreadySuspended = "1012";
		public static string DomainAlreadyActive = "1013";
		public static string LimitationPeriod = "1014";
		public static string DeviceAlreadyExists = "1015";
		public static string DeviceExistsInOtherDomains = "1016";
		public static string NoUsersInDomain = "1017";
		public static string UserExistsInOtherDomains = "1018";
		public static string DeviceNotExists = "1019";
		public static string UserNotExistsInDomain = "1020";
		public static string ActionUserNotMaster = "1021";
		public static string ExceededUserLimit = "1022";
		public static string DomainNotInitialized = "1023";
		public static string DeviceNotConfirmed = "1024";
		public static string RequestFailed = "1025";
		public static string InvalidUser = "1026";
		public static string UserNotAllowed = "1027";
		public static string DuplicatePin = "1028";
		public static string UserAlreadyInDomain = "1029";
		public static string NotAllowedToDelete = "1030";
		public static string HomeNetworkAlreadyExists = "1031";
		public static string HomeNetworkLimitation = "1032";
		public static string HomeNetworkDoesNotExist = "1033";
		public static string HomeNetworkFrequency = "1034";
		public static string RegionDoesNotExist = "1037";
		public static string UserDoesNotExist = "2000";
		public static string UserSuspended = "2001";
		public static string PinNotExists = "2003";
		public static string PinExpired = "2004";
		public static string NoValidPin = "2006";
		public static string MissingSecurityParameter = "2007";
		public static string SecretIsWrong = "2008";
		public static string LoginViaPinNotAllowed = "2009";
		public static string PinNotInTheRightLength = "2010";
		public static string PinAlreadyExists = "2011";
		public static string UserExists = "2014";
		public static string InsideLockTime = "2015";
		public static string UserNotActivated = "2016";
		public static string UserAllreadyLoggedIn = "2017";
		public static string UserDoubleLogIn = "2018";
		public static string DeviceNotRegistered = "2019";
		public static string ErrorOnInitUser = "2021";
		public static string UserNotMasterApproved = "2023";
		public static string UserWithNoDomain = "2024";
		public static string UserTypeDoesNotExist = "2025";
		public static string ActivationTokenNotFound = "2026";
		public static string UserAlreadyMasterApproved = "2027";
		public static string LoginServerDown = "2028";
		public static string RoleAlreadyAssignedToUser = "2029";
		public static string DefaultUserCannotBeDeleted = "2030";
		public static string ExclusiveMasterUserCannotBeDeleted = "2031";
		public static string ItemNotFound = "2032";
		public static string ExternalIdAlreadyExists = "2054";
		public static string ExternalError = "500063";
		public static string ParentIdShouldNotPointToItself = "2041";
		public static string ParentIdNotExist = "2059";
		public static string UserFavoriteNotDeleted = "2060";
		public static string InvalidPurchase = "3000";
		public static string CancelationWindowPeriodExpired = "3001";
		public static string SubscriptionNotRenewable = "3002";
		public static string ServiceNotAllowed = "3003";
		public static string InvalidBaseLink = "3004";
		public static string ContentAlreadyConsumed = "3005";
		public static string ReasonUnknown = "3011";
		public static string ChargeStatusUnknown = "3015";
		public static string ContentIDMissing = "3016";
		public static string NoMediaRelatedToFile = "3017";
		public static string NoContentID = "3018";
		public static string NoProductID = "3019";
		public static string CouponNotValid = "3020";
		public static string UnableToPurchasePPVPurchased = "3021";
		public static string UnableToPurchaseFree = "3022";
		public static string UnableToPurchaseForPurchaseSubscriptionOnly = "3023";
		public static string UnableToPurchaseSubscriptionPurchased = "3024";
		public static string NotForPurchase = "3025";
		public static string Fail = "3026";
		public static string UnableToPurchaseCollectionPurchased = "3027";
		public static string FileToMediaMismatch = "3028";
		public static string ReconciliationFrequencyLimitation = "3029";
		public static string InvalidCustomDataIdentifier = "3030";
		public static string InvalidFileType = "3031";
		public static string NotEntitled = "3032";
		public static string AccountCdvrNotEnabled = "3033";
		public static string AccountCatchUpNotEnabled = "3034";
		public static string ProgramCdvrNotEnabled = "3035";
		public static string ProgramCatchUpNotEnabled = "3036";
		public static string CatchUpBufferLimitation = "3037";
		public static string ProgramNotInRecordingScheduleWindow = "3038";
		public static string RecordingNotFound = "3039";
		public static string RecordingFailed = "3040";
		public static string PaymentMethodIsUsedByHousehold = "3041";
		public static string ExceededQuota = "3042";
		public static string RecordingStatusNotValid = "3043";
		public static string ExceededProtectionQuota = "3044";
		public static string AccountProtectRecordNotEnabled = "3045";
		public static string AccountSeriesRecordingNotEnabled = "3046";
		public static string AlreadyRecordedAsSeriesOrSeason = "3047";
		public static string SeriesRecordingNotFound = "3048";
		public static string EpgIdNotPartOfSeries = "3049";
		public static string RecordingPlaybackNotAllowedForNonExistingEpgChannel = "3050";
		public static string RecordingPlaybackNotAllowedForNotEntitledEpgChannel = "3051";
		public static string SeasonNumberNotMatch = "3052";
		public static string SubscriptionCancellationIsBlocked = "3074";
		public static string UnknownPriceReason = "3077";
		public static string SubscriptionDoesNotExist = "3081";
		public static string OtherCouponIsAlreadyAppliedForSubscription = "3082";
		public static string CampaignIsAlreadyAppliedForSubscription = "3083";
		public static string MediaConcurrencyLimitation = "4000";
		public static string ConcurrencyLimitation = "4001";
		public static string BadSearchRequest = "4002";
		public static string IndexMissing = "4003";
		public static string SyntaxError = "4004";
		public static string InvalidSearchField = "4005";
		public static string NoRecommendationEngineToInsert = "4006";
		public static string RecommendationEngineNotExist = "4007";
		public static string RecommendationEngineIdentifierRequired = "4008";
		public static string RecommendationEngineParamsRequired = "4009";
		public static string NoExternalChannelToInsert = "4010";
		public static string ExternalChannelNotExist = "4011";
		public static string NoExternalChannelToUpdate = "4012";
		public static string ExternalChannelIdentifierRequired = "4013";
		public static string ExternalChannelHasNoRecommendationEngine = "4014";
		public static string NoRecommendationEngineToUpdate = "4015";
		public static string InactiveExternalChannelEnrichment = "4016";
		public static string IdentifierRequired = "4017";
		public static string ObjectNotExist = "4018";
		public static string NoObjectToInsert = "4019";
		public static string InvalidMediaType = "4020";
		public static string InvalidAssetType = "4021";
		public static string ProgramDoesntExist = "4022";
		public static string ActionNotRecognized = "4023";
		public static string InvalidAssetId = "4024";
		public static string CountryNotFound = "4025";
		public static string AssetStructNameAlreadyInUse = "4026";
		public static string AssetStructSystemNameAlreadyInUse = "4027";
		public static string MetaIdsDoesNotExist = "4031";
		public static string AssetStructDoesNotExist = "4028";
		public static string AssetStructMetasConatinSystemNameDuplication = "4085";
		public static string CanNotChangePredefinedAssetStructSystemName = "4029";
		public static string CanNotDeletePredefinedAssetStruct = "4030";
		public static string MetaSystemNameAlreadyInUse = "4032";
		public static string InvalidMutlipleValueForMetaType = "4036";
		public static string MetaDoesNotExist = "4033";
		public static string CanNotChangePredefinedMetaSystemName = "4034";
		public static string CanNotDeletePredefinedMeta = "4035";
		public static string AssetStructMissingBasicMetaIds = "4037";
		public static string AssetExternalIdMustBeUnique = "4038";
		public static string InvalidMetaType = "4040";
		public static string InvalidValueSentForMeta = "4041";
		public static string DeviceRuleDoesNotExistForGroup = "4042";
		public static string GeoBlockRuleDoesNotExistForGroup = "4043";
		public static string AssetDoesNotExist = "4039";
		public static string MetaIdsDoesNotExistOnAsset = "4050";
		public static string MediaFileTypeNameAlreadyInUse = "4051";
		public static string MediaFileTypeDoesNotExist = "4052";
		public static string CanNotRemoveBasicMetaIds = "4055";
		public static string RatioAlreadyExist = "4049";
		public static string RatioDoesNotExist = "4070";
		public static string InvalidUrlForImage = "4066";
		public static string MediaFileWithThisTypeAlreadyExistForAsset = "4065";
		public static string DefaultCdnAdapterProfileNotConfigurd = "4063";
		public static string CdnAdapterProfileDoesNotExist = "4062";
		public static string InvalidRatioForImage = "4059";
		public static string ExternaldAndAltExternalIdMustBeUnique = "4058";
		public static string MediaFileAltExternalIdMustBeUnique = "4057";
		public static string MediaFileExternalIdMustBeUnique = "4056";
		public static string MediaFileNotBelongToAsset = "4054";
		public static string MediaFileDoesNotExist = "4053";
		public static string ImageDoesNotExist = "4048";
		public static string DefaultImageInvalidImageType = "4069";
		public static string ImageTypeDoesNotExist = "4047";
		public static string ImageTypeAlreadyInUse = "4046";
		public static string TagDoesNotExist = "4045";
		public static string TagAlreadyInUse = "4044";
		public static string DuplicateLanguageSent = "500069";
		public static string InvalidValueForFeature = "500070";
		public static string DefaultLanguageMustBeSent = "500071";
		public static string GroupDoesNotContainLanguage = "500072";
		public static string GlobalLanguageParameterMustBeAsterisk = "500073";
		public static string MultiValueWasNotSentForMetaDataTypeString = "500074";
		public static string TagTranslationNotAllowed = "500075";
		public static string ChannelSystemNameAlreadyInUse = "4060";
		public static string ChannelDoesNotExist = "4064";
		public static string ChannelMetaOrderByIsInvalid = "4061";
		public static string AccountIsNotOpcSupported = "4074";
		public static string CanNotDeleteParentAssetStruct = "4072";
		public static string InvalidBulkUploadStructure = "4086";
		public static string BulkUploadDoesNotExist = "4082";
		public static string BulkUploadResultIsMissing = "4083";
		public static string RelatedEntitiesExceedLimitation = "4087";
		public static string AccountEpgIngestVersionDoesNotSupportBulk = "4088";
		public static string CanNotDeleteObjectVirtualAssetMeta = "4090";
		public static string CategoryNotExist = "4091";
		public static string ChildCategoryNotExist = "4092";
		public static string ChildCategoryAlreadyBelongsToAnotherCategory = "4093";
		public static string ChildCategoryCannotBeTheCategoryItself = "4094";
		public static string InvalidAssetStruct = "4098";
		public static string NoNextEpisode = "4099";
		public static string CannotDeleteAssetStruct = "4100";
		public static string CategoryTypeNotExist = "4101";
		public static string ExtendedTypeValueCannotBeChanged = "4102";
		public static string NoPinDefined = "5001";
		public static string PinMismatch = "5002";
		public static string RuleNotExists = "5003";
		public static string NoOSSAdapterToInsert = "5004";
		public static string NameRequired = "5005";
		public static string SharedSecretRequired = "5006";
		public static string OSSAdapterIdentifierRequired = "5007";
		public static string OSSAdapterNotExist = "5008";
		public static string OSSAdapterParamsRequired = "5009";
		public static string UnknownOSSAdapterState = "5010";
		public static string ActionIsNotAllowed = "5011";
		public static string NoOSSAdapterToUpdate = "5012";
		public static string AdapterUrlRequired = "5013";
		public static string ConflictedParams = "5014";
		public static string PurchaseSettingsTypeInvalid = "5015";
		public static string ExportTaskNotFound = "5016";
		public static string ExportNotificationUrlRequired = "5017";
		public static string ExportFrequencyMinValue = "5018";
		public static string AliasMustBeUnique = "5019";
		public static string AliasRequired = "5020";
		public static string UserParentalRuleNotExists = "5021";
		public static string TimeShiftedTvPartnerSettingsNotFound = "5022";
		public static string TimeShiftedTvPartnerSettingsNotSent = "5023";
		public static string TimeShiftedTvPartnerSettingsNegativeBufferSent = "5024";
		public static string CDNPartnerSettingsNotFound = "5025";
		public static string PermissionNameNotExists = "5028";
		public static string AssetRuleNotExists = "5030";
		public static string AssetUserRuleDoesNotExists = "5031";
		public static string UserAlreadyAttachedToAssetUserRule = "5032";
		public static string AssetUserRulesOperationsDisable = "5033";
		public static string RoleDoesNotExists = "5038";
		public static string FileDoesNotExists = "5040";
		public static string FileAlreadyExists = "5041";
		public static string ErrorSavingFile = "5042";
		public static string FileIdNotInCorrectLength = "5043";
		public static string IllegalExcelFile = "5045";
		public static string EnqueueFailed = "5044";
		public static string ExcelMandatoryValueIsMissing = "5046";
		public static string AssetRuleStatusNotWritable = "5061";
		public static string PermissionNotFound = "5062";
		public static string PermissionNameAlreadyInUse = "5063";
		public static string EventNotificationIdIsMissing = "5064";
		public static string EventNotificationIdNotFound = "5065";
		public static string RegionNotFound = "5066";
		public static string RegionCannotBeParent = "5067";
		public static string DefaultRegionCannotBeDeleted = "5068";
		public static string CannotDeleteRegionInUse = "5069";
		public static string FileExceededMaxSize = "4095";
		public static string FileExtensionNotSupported = "4096";
		public static string FileMimeDifferentThanExpected = "4097";
		public static string PermissionItemNotFound = "5071";
		public static string PermissionReadOnly = "5072";
		public static string PermissionPermissionItemNotFound = "5073";
		public static string PermissionPermissionItemAlreadyExists = "5074";
		public static string RoleReadOnly = "5075";
		public static string IncorrectPrice = "6000";
		public static string UnKnownPPVModule = "6001";
		public static string ExpiredCard = "6002";
		public static string CellularPermissionsError = "6003";
		public static string UnKnownBillingProvider = "6004";
		public static string PaymentGatewayIdRequired = "6005";
		public static string PaymentGatewayParamsRequired = "6006";
		public static string PaymentGatewayNotSetForHousehold = "6007";
		public static string PaymentGatewayNotExist = "6008";
		public static string PaymentGatewayChargeIdRequired = "6009";
		public static string NoConfigurationFound = "6011";
		public static string AdapterAppFailure = "6012";
		public static string SignatureMismatch = "6013";
		public static string ErrorSavingPaymentGatewayTransaction = "6014";
		public static string ErrorSavingPaymentGatewayPending = "6015";
		public static string ExternalIdentifierRequired = "6016";
		public static string ErrorSavingPaymentGatewayHousehold = "6017";
		public static string NoPaymentGateway = "6018";
		public static string PaymentGatewayNameRequired = "6020";
		public static string PaymentGatewaySharedSecretRequired = "6021";
		public static string HouseholdAlreadySetToPaymentGateway = "6024";
		public static string ChargeIdAlreadySetToHouseholdPaymentGateway = "6025";
		public static string ChargeIdNotSetToHousehold = "6026";
		public static string HouseholdNotSetToPaymentGateway = "6027";
		public static string PaymentGatewaySelectionIsDisabled = "6028";
		public static string NoResponseFromPaymentGateway = "6030";
		public static string InvalidAccount = "6031";
		public static string InsufficientFunds = "6032";
		public static string UnknownPaymentGatewayResponse = "6033";
		public static string PaymentGatewayAdapterUserKnown = "6034";
		public static string PaymentGatewayAdapterReasonUnknown = "6035";
		public static string SignatureDoesNotMatch = "6036";
		public static string ErrorUpdatingPendingTransaction = "6037";
		public static string PaymentGatewayTransactionNotFound = "6038";
		public static string PaymentGatewayTransactionIsNotPending = "6039";
		public static string ExternalIdentifierMustBeUnique = "6040";
		public static string NoPaymentGatewayToInsert = "6041";
		public static string UnknownTransactionState = "6042";
		public static string PaymentGatewayNotValid = "6043";
		public static string HouseholdRequired = "6044";
		public static string PaymentGatewayAdapterFailReasonUnknown = "6045";
		public static string NoPartnerConfigurationToUpdate = "6046";
		public static string NoConfigurationValueToUpdate = "6047";
		public static string PaymentMethodNotSetForHousehold = "6048";
		public static string PaymentMethodNotExist = "6049";
		public static string PaymentMethodIdRequired = "6050";
		public static string PaymentGatewaySuspended = "6051";
		public static string PaymentGatewayExternalVerification = "6052";
		public static string PaymentMethodAlreadySetToHouseholdPaymentGateway = "6054";
		public static string PaymentMethodNameRequired = "6055";
		public static string PaymentGatewayNotSupportPaymentMethod = "6056";
		public static string Conflict = "7000";
		public static string MinFriendsLimitation = "7001";
		public static string InvalidParameters = "7010";
		public static string NoNotificationSettingsSent = "8000";
		public static string PushNotificationFalse = "8001";
		public static string NoNotificationPartnerSettings = "8002";
		public static string NoNotificationSettings = "8003";
		public static string AnnouncementMessageIsEmpty = "8004";
		public static string AnnouncementInvalidStartTime = "8005";
		public static string AnnouncementNotFound = "8006";
		public static string AnnouncementUpdateNotAllowed = "8007";
		public static string AnnouncementInvalidTimezone = "8008";
		public static string FeatureDisabled = "8009";
		public static string AnnouncementMessageTooLong = "8010";
		public static string FailCreateAnnouncement = "8011";
		public static string UserNotFollowing = "8012";
		public static string UserAlreadyFollowing = "8013";
		public static string MessagePlaceholdersInvalid = "8014";
		public static string DatetimeFormatIsInvalid = "8015";
		public static string MessageTemplateNotFound = "8016";
		public static string URLPlaceholdersInvalid = "8017";
		public static string InvalidMessageTTL = "8018";
		public static string MessageIdentifierRequired = "8019";
		public static string UserInboxMessagesNotExist = "8020";
		public static string FailCreateTopicNotification = "8041";
		public static string TopicNotificationNotFound = "8042";
		public static string TopicNotificationMessageNotFound = "8043";
		public static string WrongTopicNotification = "8044";
		public static string WrongTopicNotificationTrigger = "8045";
		public static string InvalidPriceCode = "9000";
		public static string InvalidValue = "9001";
		public static string InvalidDiscountCode = "9002";
		public static string InvalidPricePlan = "9003";
		public static string CodeMustBeUnique = "9004";
		public static string CodeNotExist = "9005";
		public static string InvalidCodeNotExist = "9006";
		public static string InvalidChannels = "9008";
		public static string InvalidFileTypes = "9009";
		public static string InvalidPreviewModule = "9010";
		public static string MandatoryField = "9011";
		public static string UniqueFiled = "9012";
		public static string InvalidUsageModule = "9013";
		public static string InvalidCouponGroup = "9014";
		public static string InvalidCurrency = "9015";
		public static string ModuleNotExists = "9016";
		public static string PricePlanDoesNotExist = "9017";
		public static string PriceDetailsDoesNotExist = "9018";
		public static string CouponCodeIsMissing = "9023";
		public static string CouponCodeAlreadyLoaded = "9024";
		public static string CouponCodeNotInHousehold = "9025";
		public static string ExceededHouseholdCouponLimit = "9026";
		public static string AdapterNotExists = "10000";
		public static string AdapterIdentifierRequired = "10001";
		public static string AdapterIsRequired = "10002";
		public static string NoAdapterToInsert = "10003";
		public static string CanNotDeleteDefaultAdapter = "10004";
		public static string IllegalXml = "11000";
		public static string MissingExternalIdentifier = "11001";
		public static string UnknownIngestType = "11002";
		public static string EPGSProgramDatesError = "11003";
		public static string IngestProfileNotExists = "5048";
		public static string IngestProfileIdRequired = "5060";
		public static string NoIngestProfileToInsert = "5049";
		public static string EPGLanguageNotFound = "11004";
		public static string BadRequest = "500003";
		public static string InvalidVersion = "500057";
		public static string ServiceForbidden = "500004";
		public static string PropertyActionForbidden = "500051";
		public static string ActionArgumentForbidden = "500052";
		public static string InvalidKS = "500015";
		public static string ExpiredKS = "500016";
		public static string PartnerInvalid = "500008";
		public static string InvalidRefreshToken = "500017";
		public static string RefreshTokenFailed = "500034";
		public static string UnauthorizedUser = "500035";
		public static string InvalidUdid = "500060";
		public static string InvalidService = "500011";
		public static string InvalidAction = "500012";
		public static string ActionNotSpecified = "500033";
		public static string InvalidActionParameter = "500054";
		public static string InvalidActionParameters = "500013";
		public static string RequestAborted = "500079";
		public static string RequestSkipped = "500080";
		public static string InvalidArgument = "50026";
		public static string ArgumentMustBeNumeric = "500031";
		public static string ArgumentCannotBeEmpty = "50027";
		public static string ArgumentReadonly = "500036";
		public static string ArgumentInsertonly = "500037";
		public static string EnumValueNotSupported = "500041";
		public static string ArgumentShouldBeEnum = "500044";
		public static string ArgumentShouldContainMinValueCrossed = "500058";
		public static string ArgumentShouldContainMaxValueCrossed = "500059";
		public static string ArgumentsCannotBeEmpty = "500056";
		public static string ArgumentsConflictsEachOther = "500038";
		public static string TimeInPast = "500039";
		public static string ArgumentMaxLengthCrossed = "500045";
		public static string ArgumentMinLengthCrossed = "500046";
		public static string ArgumentMaxValueCrossed = "500047";
		public static string ArgumentMinValueCrossed = "500048";
		public static string ArgumentsConflictEachOther = "500061";
		public static string ArgumentsDuplicate = "500066";
		public static string InvalidArgumentValue = "500067";
		public static string OneOfArgumentsCannotBeEmpty = "500081";
		public static string TypeNotSupported = "500083";
		public static string FormatNotSupported = "500084";
		public static string MediaIdsMustBeNumeric = "500029";
		public static string EpgInternalIdsMustBeNumeric = "500030";
		public static string ListTypeCannotBeEmptyOrAll = "500032";
		public static string DuplicateAsset = "500049";
		public static string DuplicateFile = "500050";
		public static string UnableToCreateHouseholdForRole = "500062";
		public static string HttpMethodNotSupported = "500065";
		public static string PropertyIsOpcSupported = "500082";
		public static string KeyCannotBeEmptyOrNull = "500086";
		public static string MissingMandatoryArgumentInProperty = "500087";
		public static string MaxArguments = "500088";
		public static string HouseholdForbidden = "500028";
		public static string SwitchingUsersIsNotAllowedForPartner = "50024";
		public static string NotActiveAppToken = "50023";
		public static string InvalidAppTokenHash = "50022";
		public static string ExpiredAppToken = "50021";
		public static string NotAllowed = "7013";
		public static string GroupMissMatch = "500085";
		public static string Error = "1";
		public static string MissingConfiguration = "500006";
		public static string NotFound = "500007";
		public static string ObjectIdNotFound = "500055";
		public static string InvalidMultirequestToken = "50025";
		public static string InvalidObjectType = "500076";
		public static string AbstractParameter = "500018";
		public static string MissingParameter = "500053";
		public static string MultirequestIndexNotZeroBased = "500042";
		public static string MultirequestInvalidIndex = "500043";
		public static string MultirequestGenericMethod = "500064";
		public static string MultirequestInvalidOperatorForConditionType = "500078";
		public static string MultirequestInvalidConditionValue = "500077";
		#endregion

		#region Private Fields
		private string _Code;
		#endregion

		#region Properties
		public string Code
		{
			get { return this._Code; }
		}
		#endregion

		public APIException(string code, string message): base(message)
		{
			this._Code = code;
		}
	}
}

