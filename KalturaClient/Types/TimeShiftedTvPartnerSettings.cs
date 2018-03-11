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
// Copyright (C) 2006-2018  Kaltura Inc.
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
using System.Collections.Generic;
using Kaltura.Enums;
using Kaltura.Request;

namespace Kaltura.Types
{
	public class TimeShiftedTvPartnerSettings : ObjectBase
	{
		#region Constants
		public const string CATCH_UP_ENABLED = "catchUpEnabled";
		public const string CDVR_ENABLED = "cdvrEnabled";
		public const string START_OVER_ENABLED = "startOverEnabled";
		public const string TRICK_PLAY_ENABLED = "trickPlayEnabled";
		public const string RECORDING_SCHEDULE_WINDOW_ENABLED = "recordingScheduleWindowEnabled";
		public const string PROTECTION_ENABLED = "protectionEnabled";
		public const string CATCH_UP_BUFFER_LENGTH = "catchUpBufferLength";
		public const string TRICK_PLAY_BUFFER_LENGTH = "trickPlayBufferLength";
		public const string RECORDING_SCHEDULE_WINDOW = "recordingScheduleWindow";
		public const string PADDING_BEFORE_PROGRAM_STARTS = "paddingBeforeProgramStarts";
		public const string PADDING_AFTER_PROGRAM_ENDS = "paddingAfterProgramEnds";
		public const string PROTECTION_PERIOD = "protectionPeriod";
		public const string PROTECTION_QUOTA_PERCENTAGE = "protectionQuotaPercentage";
		public const string RECORDING_LIFETIME_PERIOD = "recordingLifetimePeriod";
		public const string CLEANUP_NOTICE_PERIOD = "cleanupNoticePeriod";
		public const string SERIES_RECORDING_ENABLED = "seriesRecordingEnabled";
		public const string NON_ENTITLED_CHANNEL_PLAYBACK_ENABLED = "nonEntitledChannelPlaybackEnabled";
		public const string NON_EXISTING_CHANNEL_PLAYBACK_ENABLED = "nonExistingChannelPlaybackEnabled";
		public const string QUOTA_OVERAGE_POLICY = "quotaOveragePolicy";
		public const string PROTECTION_POLICY = "protectionPolicy";
		public const string RECOVERY_GRACE_PERIOD = "recoveryGracePeriod";
		#endregion

		#region Private Fields
		private bool? _CatchUpEnabled = null;
		private bool? _CdvrEnabled = null;
		private bool? _StartOverEnabled = null;
		private bool? _TrickPlayEnabled = null;
		private bool? _RecordingScheduleWindowEnabled = null;
		private bool? _ProtectionEnabled = null;
		private long _CatchUpBufferLength = long.MinValue;
		private long _TrickPlayBufferLength = long.MinValue;
		private long _RecordingScheduleWindow = long.MinValue;
		private long _PaddingBeforeProgramStarts = long.MinValue;
		private long _PaddingAfterProgramEnds = long.MinValue;
		private int _ProtectionPeriod = Int32.MinValue;
		private int _ProtectionQuotaPercentage = Int32.MinValue;
		private int _RecordingLifetimePeriod = Int32.MinValue;
		private int _CleanupNoticePeriod = Int32.MinValue;
		private bool? _SeriesRecordingEnabled = null;
		private bool? _NonEntitledChannelPlaybackEnabled = null;
		private bool? _NonExistingChannelPlaybackEnabled = null;
		private QuotaOveragePolicy _QuotaOveragePolicy = null;
		private ProtectionPolicy _ProtectionPolicy = null;
		private int _RecoveryGracePeriod = Int32.MinValue;
		#endregion

		#region Properties
		public bool? CatchUpEnabled
		{
			get { return _CatchUpEnabled; }
			set 
			{ 
				_CatchUpEnabled = value;
				OnPropertyChanged("CatchUpEnabled");
			}
		}
		public bool? CdvrEnabled
		{
			get { return _CdvrEnabled; }
			set 
			{ 
				_CdvrEnabled = value;
				OnPropertyChanged("CdvrEnabled");
			}
		}
		public bool? StartOverEnabled
		{
			get { return _StartOverEnabled; }
			set 
			{ 
				_StartOverEnabled = value;
				OnPropertyChanged("StartOverEnabled");
			}
		}
		public bool? TrickPlayEnabled
		{
			get { return _TrickPlayEnabled; }
			set 
			{ 
				_TrickPlayEnabled = value;
				OnPropertyChanged("TrickPlayEnabled");
			}
		}
		public bool? RecordingScheduleWindowEnabled
		{
			get { return _RecordingScheduleWindowEnabled; }
			set 
			{ 
				_RecordingScheduleWindowEnabled = value;
				OnPropertyChanged("RecordingScheduleWindowEnabled");
			}
		}
		public bool? ProtectionEnabled
		{
			get { return _ProtectionEnabled; }
			set 
			{ 
				_ProtectionEnabled = value;
				OnPropertyChanged("ProtectionEnabled");
			}
		}
		public long CatchUpBufferLength
		{
			get { return _CatchUpBufferLength; }
			set 
			{ 
				_CatchUpBufferLength = value;
				OnPropertyChanged("CatchUpBufferLength");
			}
		}
		public long TrickPlayBufferLength
		{
			get { return _TrickPlayBufferLength; }
			set 
			{ 
				_TrickPlayBufferLength = value;
				OnPropertyChanged("TrickPlayBufferLength");
			}
		}
		public long RecordingScheduleWindow
		{
			get { return _RecordingScheduleWindow; }
			set 
			{ 
				_RecordingScheduleWindow = value;
				OnPropertyChanged("RecordingScheduleWindow");
			}
		}
		public long PaddingBeforeProgramStarts
		{
			get { return _PaddingBeforeProgramStarts; }
			set 
			{ 
				_PaddingBeforeProgramStarts = value;
				OnPropertyChanged("PaddingBeforeProgramStarts");
			}
		}
		public long PaddingAfterProgramEnds
		{
			get { return _PaddingAfterProgramEnds; }
			set 
			{ 
				_PaddingAfterProgramEnds = value;
				OnPropertyChanged("PaddingAfterProgramEnds");
			}
		}
		public int ProtectionPeriod
		{
			get { return _ProtectionPeriod; }
			set 
			{ 
				_ProtectionPeriod = value;
				OnPropertyChanged("ProtectionPeriod");
			}
		}
		public int ProtectionQuotaPercentage
		{
			get { return _ProtectionQuotaPercentage; }
			set 
			{ 
				_ProtectionQuotaPercentage = value;
				OnPropertyChanged("ProtectionQuotaPercentage");
			}
		}
		public int RecordingLifetimePeriod
		{
			get { return _RecordingLifetimePeriod; }
			set 
			{ 
				_RecordingLifetimePeriod = value;
				OnPropertyChanged("RecordingLifetimePeriod");
			}
		}
		public int CleanupNoticePeriod
		{
			get { return _CleanupNoticePeriod; }
			set 
			{ 
				_CleanupNoticePeriod = value;
				OnPropertyChanged("CleanupNoticePeriod");
			}
		}
		public bool? SeriesRecordingEnabled
		{
			get { return _SeriesRecordingEnabled; }
			set 
			{ 
				_SeriesRecordingEnabled = value;
				OnPropertyChanged("SeriesRecordingEnabled");
			}
		}
		public bool? NonEntitledChannelPlaybackEnabled
		{
			get { return _NonEntitledChannelPlaybackEnabled; }
			set 
			{ 
				_NonEntitledChannelPlaybackEnabled = value;
				OnPropertyChanged("NonEntitledChannelPlaybackEnabled");
			}
		}
		public bool? NonExistingChannelPlaybackEnabled
		{
			get { return _NonExistingChannelPlaybackEnabled; }
			set 
			{ 
				_NonExistingChannelPlaybackEnabled = value;
				OnPropertyChanged("NonExistingChannelPlaybackEnabled");
			}
		}
		public QuotaOveragePolicy QuotaOveragePolicy
		{
			get { return _QuotaOveragePolicy; }
			set 
			{ 
				_QuotaOveragePolicy = value;
				OnPropertyChanged("QuotaOveragePolicy");
			}
		}
		public ProtectionPolicy ProtectionPolicy
		{
			get { return _ProtectionPolicy; }
			set 
			{ 
				_ProtectionPolicy = value;
				OnPropertyChanged("ProtectionPolicy");
			}
		}
		public int RecoveryGracePeriod
		{
			get { return _RecoveryGracePeriod; }
			set 
			{ 
				_RecoveryGracePeriod = value;
				OnPropertyChanged("RecoveryGracePeriod");
			}
		}
		#endregion

		#region CTor
		public TimeShiftedTvPartnerSettings()
		{
		}

		public TimeShiftedTvPartnerSettings(XmlElement node) : base(node)
		{
			foreach (XmlElement propertyNode in node.ChildNodes)
			{
				switch (propertyNode.Name)
				{
					case "catchUpEnabled":
						this._CatchUpEnabled = ParseBool(propertyNode.InnerText);
						continue;
					case "cdvrEnabled":
						this._CdvrEnabled = ParseBool(propertyNode.InnerText);
						continue;
					case "startOverEnabled":
						this._StartOverEnabled = ParseBool(propertyNode.InnerText);
						continue;
					case "trickPlayEnabled":
						this._TrickPlayEnabled = ParseBool(propertyNode.InnerText);
						continue;
					case "recordingScheduleWindowEnabled":
						this._RecordingScheduleWindowEnabled = ParseBool(propertyNode.InnerText);
						continue;
					case "protectionEnabled":
						this._ProtectionEnabled = ParseBool(propertyNode.InnerText);
						continue;
					case "catchUpBufferLength":
						this._CatchUpBufferLength = ParseLong(propertyNode.InnerText);
						continue;
					case "trickPlayBufferLength":
						this._TrickPlayBufferLength = ParseLong(propertyNode.InnerText);
						continue;
					case "recordingScheduleWindow":
						this._RecordingScheduleWindow = ParseLong(propertyNode.InnerText);
						continue;
					case "paddingBeforeProgramStarts":
						this._PaddingBeforeProgramStarts = ParseLong(propertyNode.InnerText);
						continue;
					case "paddingAfterProgramEnds":
						this._PaddingAfterProgramEnds = ParseLong(propertyNode.InnerText);
						continue;
					case "protectionPeriod":
						this._ProtectionPeriod = ParseInt(propertyNode.InnerText);
						continue;
					case "protectionQuotaPercentage":
						this._ProtectionQuotaPercentage = ParseInt(propertyNode.InnerText);
						continue;
					case "recordingLifetimePeriod":
						this._RecordingLifetimePeriod = ParseInt(propertyNode.InnerText);
						continue;
					case "cleanupNoticePeriod":
						this._CleanupNoticePeriod = ParseInt(propertyNode.InnerText);
						continue;
					case "seriesRecordingEnabled":
						this._SeriesRecordingEnabled = ParseBool(propertyNode.InnerText);
						continue;
					case "nonEntitledChannelPlaybackEnabled":
						this._NonEntitledChannelPlaybackEnabled = ParseBool(propertyNode.InnerText);
						continue;
					case "nonExistingChannelPlaybackEnabled":
						this._NonExistingChannelPlaybackEnabled = ParseBool(propertyNode.InnerText);
						continue;
					case "quotaOveragePolicy":
						this._QuotaOveragePolicy = (QuotaOveragePolicy)StringEnum.Parse(typeof(QuotaOveragePolicy), propertyNode.InnerText);
						continue;
					case "protectionPolicy":
						this._ProtectionPolicy = (ProtectionPolicy)StringEnum.Parse(typeof(ProtectionPolicy), propertyNode.InnerText);
						continue;
					case "recoveryGracePeriod":
						this._RecoveryGracePeriod = ParseInt(propertyNode.InnerText);
						continue;
				}
			}
		}
		#endregion

		#region Methods
		public override Params ToParams(bool includeObjectType = true)
		{
			Params kparams = base.ToParams(includeObjectType);
			if (includeObjectType)
				kparams.AddReplace("objectType", "KalturaTimeShiftedTvPartnerSettings");
			kparams.AddIfNotNull("catchUpEnabled", this._CatchUpEnabled);
			kparams.AddIfNotNull("cdvrEnabled", this._CdvrEnabled);
			kparams.AddIfNotNull("startOverEnabled", this._StartOverEnabled);
			kparams.AddIfNotNull("trickPlayEnabled", this._TrickPlayEnabled);
			kparams.AddIfNotNull("recordingScheduleWindowEnabled", this._RecordingScheduleWindowEnabled);
			kparams.AddIfNotNull("protectionEnabled", this._ProtectionEnabled);
			kparams.AddIfNotNull("catchUpBufferLength", this._CatchUpBufferLength);
			kparams.AddIfNotNull("trickPlayBufferLength", this._TrickPlayBufferLength);
			kparams.AddIfNotNull("recordingScheduleWindow", this._RecordingScheduleWindow);
			kparams.AddIfNotNull("paddingBeforeProgramStarts", this._PaddingBeforeProgramStarts);
			kparams.AddIfNotNull("paddingAfterProgramEnds", this._PaddingAfterProgramEnds);
			kparams.AddIfNotNull("protectionPeriod", this._ProtectionPeriod);
			kparams.AddIfNotNull("protectionQuotaPercentage", this._ProtectionQuotaPercentage);
			kparams.AddIfNotNull("recordingLifetimePeriod", this._RecordingLifetimePeriod);
			kparams.AddIfNotNull("cleanupNoticePeriod", this._CleanupNoticePeriod);
			kparams.AddIfNotNull("seriesRecordingEnabled", this._SeriesRecordingEnabled);
			kparams.AddIfNotNull("nonEntitledChannelPlaybackEnabled", this._NonEntitledChannelPlaybackEnabled);
			kparams.AddIfNotNull("nonExistingChannelPlaybackEnabled", this._NonExistingChannelPlaybackEnabled);
			kparams.AddIfNotNull("quotaOveragePolicy", this._QuotaOveragePolicy);
			kparams.AddIfNotNull("protectionPolicy", this._ProtectionPolicy);
			kparams.AddIfNotNull("recoveryGracePeriod", this._RecoveryGracePeriod);
			return kparams;
		}
		protected override string getPropertyName(string apiName)
		{
			switch(apiName)
			{
				case CATCH_UP_ENABLED:
					return "CatchUpEnabled";
				case CDVR_ENABLED:
					return "CdvrEnabled";
				case START_OVER_ENABLED:
					return "StartOverEnabled";
				case TRICK_PLAY_ENABLED:
					return "TrickPlayEnabled";
				case RECORDING_SCHEDULE_WINDOW_ENABLED:
					return "RecordingScheduleWindowEnabled";
				case PROTECTION_ENABLED:
					return "ProtectionEnabled";
				case CATCH_UP_BUFFER_LENGTH:
					return "CatchUpBufferLength";
				case TRICK_PLAY_BUFFER_LENGTH:
					return "TrickPlayBufferLength";
				case RECORDING_SCHEDULE_WINDOW:
					return "RecordingScheduleWindow";
				case PADDING_BEFORE_PROGRAM_STARTS:
					return "PaddingBeforeProgramStarts";
				case PADDING_AFTER_PROGRAM_ENDS:
					return "PaddingAfterProgramEnds";
				case PROTECTION_PERIOD:
					return "ProtectionPeriod";
				case PROTECTION_QUOTA_PERCENTAGE:
					return "ProtectionQuotaPercentage";
				case RECORDING_LIFETIME_PERIOD:
					return "RecordingLifetimePeriod";
				case CLEANUP_NOTICE_PERIOD:
					return "CleanupNoticePeriod";
				case SERIES_RECORDING_ENABLED:
					return "SeriesRecordingEnabled";
				case NON_ENTITLED_CHANNEL_PLAYBACK_ENABLED:
					return "NonEntitledChannelPlaybackEnabled";
				case NON_EXISTING_CHANNEL_PLAYBACK_ENABLED:
					return "NonExistingChannelPlaybackEnabled";
				case QUOTA_OVERAGE_POLICY:
					return "QuotaOveragePolicy";
				case PROTECTION_POLICY:
					return "ProtectionPolicy";
				case RECOVERY_GRACE_PERIOD:
					return "RecoveryGracePeriod";
				default:
					return base.getPropertyName(apiName);
			}
		}
		#endregion
	}
}

