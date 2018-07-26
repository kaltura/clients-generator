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
	public class LiveAsset : MediaAsset
	{
		#region Constants
		public const string ENABLE_CDVR_STATE = "enableCdvrState";
		public const string ENABLE_CATCH_UP_STATE = "enableCatchUpState";
		public const string ENABLE_START_OVER_STATE = "enableStartOverState";
		public const string BUFFER_CATCH_UP_SETTING = "bufferCatchUpSetting";
		public const string BUFFER_TRICK_PLAY_SETTING = "bufferTrickPlaySetting";
		public const string ENABLE_RECORDING_PLAYBACK_NON_ENTITLED_CHANNEL_STATE = "enableRecordingPlaybackNonEntitledChannelState";
		public const string ENABLE_TRICK_PLAY_STATE = "enableTrickPlayState";
		public const string EXTERNAL_EPG_INGEST_ID = "externalEpgIngestId";
		public const string EXTERNAL_CDVR_ID = "externalCdvrId";
		public const string ENABLE_CDVR = "enableCdvr";
		public const string ENABLE_CATCH_UP = "enableCatchUp";
		public const string ENABLE_START_OVER = "enableStartOver";
		public const string CATCH_UP_BUFFER = "catchUpBuffer";
		public const string TRICK_PLAY_BUFFER = "trickPlayBuffer";
		public const string ENABLE_RECORDING_PLAYBACK_NON_ENTITLED_CHANNEL = "enableRecordingPlaybackNonEntitledChannel";
		public const string ENABLE_TRICK_PLAY = "enableTrickPlay";
		public const string CHANNEL_TYPE = "channelType";
		#endregion

		#region Private Fields
		private TimeShiftedTvState _EnableCdvrState = null;
		private TimeShiftedTvState _EnableCatchUpState = null;
		private TimeShiftedTvState _EnableStartOverState = null;
		private long _BufferCatchUpSetting = long.MinValue;
		private long _BufferTrickPlaySetting = long.MinValue;
		private TimeShiftedTvState _EnableRecordingPlaybackNonEntitledChannelState = null;
		private TimeShiftedTvState _EnableTrickPlayState = null;
		private string _ExternalEpgIngestId = null;
		private string _ExternalCdvrId = null;
		private bool? _EnableCdvr = null;
		private bool? _EnableCatchUp = null;
		private bool? _EnableStartOver = null;
		private long _CatchUpBuffer = long.MinValue;
		private long _TrickPlayBuffer = long.MinValue;
		private bool? _EnableRecordingPlaybackNonEntitledChannel = null;
		private bool? _EnableTrickPlay = null;
		private LinearChannelType _ChannelType = null;
		#endregion

		#region Properties
		public TimeShiftedTvState EnableCdvrState
		{
			get { return _EnableCdvrState; }
			set 
			{ 
				_EnableCdvrState = value;
				OnPropertyChanged("EnableCdvrState");
			}
		}
		public TimeShiftedTvState EnableCatchUpState
		{
			get { return _EnableCatchUpState; }
			set 
			{ 
				_EnableCatchUpState = value;
				OnPropertyChanged("EnableCatchUpState");
			}
		}
		public TimeShiftedTvState EnableStartOverState
		{
			get { return _EnableStartOverState; }
			set 
			{ 
				_EnableStartOverState = value;
				OnPropertyChanged("EnableStartOverState");
			}
		}
		public long BufferCatchUpSetting
		{
			get { return _BufferCatchUpSetting; }
			set 
			{ 
				_BufferCatchUpSetting = value;
				OnPropertyChanged("BufferCatchUpSetting");
			}
		}
		public long BufferTrickPlaySetting
		{
			get { return _BufferTrickPlaySetting; }
			set 
			{ 
				_BufferTrickPlaySetting = value;
				OnPropertyChanged("BufferTrickPlaySetting");
			}
		}
		public TimeShiftedTvState EnableRecordingPlaybackNonEntitledChannelState
		{
			get { return _EnableRecordingPlaybackNonEntitledChannelState; }
			set 
			{ 
				_EnableRecordingPlaybackNonEntitledChannelState = value;
				OnPropertyChanged("EnableRecordingPlaybackNonEntitledChannelState");
			}
		}
		public TimeShiftedTvState EnableTrickPlayState
		{
			get { return _EnableTrickPlayState; }
			set 
			{ 
				_EnableTrickPlayState = value;
				OnPropertyChanged("EnableTrickPlayState");
			}
		}
		public string ExternalEpgIngestId
		{
			get { return _ExternalEpgIngestId; }
			set 
			{ 
				_ExternalEpgIngestId = value;
				OnPropertyChanged("ExternalEpgIngestId");
			}
		}
		public string ExternalCdvrId
		{
			get { return _ExternalCdvrId; }
			set 
			{ 
				_ExternalCdvrId = value;
				OnPropertyChanged("ExternalCdvrId");
			}
		}
		public bool? EnableCdvr
		{
			get { return _EnableCdvr; }
		}
		public bool? EnableCatchUp
		{
			get { return _EnableCatchUp; }
		}
		public bool? EnableStartOver
		{
			get { return _EnableStartOver; }
		}
		public long CatchUpBuffer
		{
			get { return _CatchUpBuffer; }
		}
		public long TrickPlayBuffer
		{
			get { return _TrickPlayBuffer; }
		}
		public bool? EnableRecordingPlaybackNonEntitledChannel
		{
			get { return _EnableRecordingPlaybackNonEntitledChannel; }
		}
		public bool? EnableTrickPlay
		{
			get { return _EnableTrickPlay; }
		}
		public LinearChannelType ChannelType
		{
			get { return _ChannelType; }
			set 
			{ 
				_ChannelType = value;
				OnPropertyChanged("ChannelType");
			}
		}
		#endregion

		#region CTor
		public LiveAsset()
		{
		}

		public LiveAsset(XmlElement node) : base(node)
		{
			foreach (XmlElement propertyNode in node.ChildNodes)
			{
				switch (propertyNode.Name)
				{
					case "enableCdvrState":
						this._EnableCdvrState = (TimeShiftedTvState)StringEnum.Parse(typeof(TimeShiftedTvState), propertyNode.InnerText);
						continue;
					case "enableCatchUpState":
						this._EnableCatchUpState = (TimeShiftedTvState)StringEnum.Parse(typeof(TimeShiftedTvState), propertyNode.InnerText);
						continue;
					case "enableStartOverState":
						this._EnableStartOverState = (TimeShiftedTvState)StringEnum.Parse(typeof(TimeShiftedTvState), propertyNode.InnerText);
						continue;
					case "bufferCatchUpSetting":
						this._BufferCatchUpSetting = ParseLong(propertyNode.InnerText);
						continue;
					case "bufferTrickPlaySetting":
						this._BufferTrickPlaySetting = ParseLong(propertyNode.InnerText);
						continue;
					case "enableRecordingPlaybackNonEntitledChannelState":
						this._EnableRecordingPlaybackNonEntitledChannelState = (TimeShiftedTvState)StringEnum.Parse(typeof(TimeShiftedTvState), propertyNode.InnerText);
						continue;
					case "enableTrickPlayState":
						this._EnableTrickPlayState = (TimeShiftedTvState)StringEnum.Parse(typeof(TimeShiftedTvState), propertyNode.InnerText);
						continue;
					case "externalEpgIngestId":
						this._ExternalEpgIngestId = propertyNode.InnerText;
						continue;
					case "externalCdvrId":
						this._ExternalCdvrId = propertyNode.InnerText;
						continue;
					case "enableCdvr":
						this._EnableCdvr = ParseBool(propertyNode.InnerText);
						continue;
					case "enableCatchUp":
						this._EnableCatchUp = ParseBool(propertyNode.InnerText);
						continue;
					case "enableStartOver":
						this._EnableStartOver = ParseBool(propertyNode.InnerText);
						continue;
					case "catchUpBuffer":
						this._CatchUpBuffer = ParseLong(propertyNode.InnerText);
						continue;
					case "trickPlayBuffer":
						this._TrickPlayBuffer = ParseLong(propertyNode.InnerText);
						continue;
					case "enableRecordingPlaybackNonEntitledChannel":
						this._EnableRecordingPlaybackNonEntitledChannel = ParseBool(propertyNode.InnerText);
						continue;
					case "enableTrickPlay":
						this._EnableTrickPlay = ParseBool(propertyNode.InnerText);
						continue;
					case "channelType":
						this._ChannelType = (LinearChannelType)StringEnum.Parse(typeof(LinearChannelType), propertyNode.InnerText);
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
				kparams.AddReplace("objectType", "KalturaLiveAsset");
			kparams.AddIfNotNull("enableCdvrState", this._EnableCdvrState);
			kparams.AddIfNotNull("enableCatchUpState", this._EnableCatchUpState);
			kparams.AddIfNotNull("enableStartOverState", this._EnableStartOverState);
			kparams.AddIfNotNull("bufferCatchUpSetting", this._BufferCatchUpSetting);
			kparams.AddIfNotNull("bufferTrickPlaySetting", this._BufferTrickPlaySetting);
			kparams.AddIfNotNull("enableRecordingPlaybackNonEntitledChannelState", this._EnableRecordingPlaybackNonEntitledChannelState);
			kparams.AddIfNotNull("enableTrickPlayState", this._EnableTrickPlayState);
			kparams.AddIfNotNull("externalEpgIngestId", this._ExternalEpgIngestId);
			kparams.AddIfNotNull("externalCdvrId", this._ExternalCdvrId);
			kparams.AddIfNotNull("enableCdvr", this._EnableCdvr);
			kparams.AddIfNotNull("enableCatchUp", this._EnableCatchUp);
			kparams.AddIfNotNull("enableStartOver", this._EnableStartOver);
			kparams.AddIfNotNull("catchUpBuffer", this._CatchUpBuffer);
			kparams.AddIfNotNull("trickPlayBuffer", this._TrickPlayBuffer);
			kparams.AddIfNotNull("enableRecordingPlaybackNonEntitledChannel", this._EnableRecordingPlaybackNonEntitledChannel);
			kparams.AddIfNotNull("enableTrickPlay", this._EnableTrickPlay);
			kparams.AddIfNotNull("channelType", this._ChannelType);
			return kparams;
		}
		protected override string getPropertyName(string apiName)
		{
			switch(apiName)
			{
				case ENABLE_CDVR_STATE:
					return "EnableCdvrState";
				case ENABLE_CATCH_UP_STATE:
					return "EnableCatchUpState";
				case ENABLE_START_OVER_STATE:
					return "EnableStartOverState";
				case BUFFER_CATCH_UP_SETTING:
					return "BufferCatchUpSetting";
				case BUFFER_TRICK_PLAY_SETTING:
					return "BufferTrickPlaySetting";
				case ENABLE_RECORDING_PLAYBACK_NON_ENTITLED_CHANNEL_STATE:
					return "EnableRecordingPlaybackNonEntitledChannelState";
				case ENABLE_TRICK_PLAY_STATE:
					return "EnableTrickPlayState";
				case EXTERNAL_EPG_INGEST_ID:
					return "ExternalEpgIngestId";
				case EXTERNAL_CDVR_ID:
					return "ExternalCdvrId";
				case ENABLE_CDVR:
					return "EnableCdvr";
				case ENABLE_CATCH_UP:
					return "EnableCatchUp";
				case ENABLE_START_OVER:
					return "EnableStartOver";
				case CATCH_UP_BUFFER:
					return "CatchUpBuffer";
				case TRICK_PLAY_BUFFER:
					return "TrickPlayBuffer";
				case ENABLE_RECORDING_PLAYBACK_NON_ENTITLED_CHANNEL:
					return "EnableRecordingPlaybackNonEntitledChannel";
				case ENABLE_TRICK_PLAY:
					return "EnableTrickPlay";
				case CHANNEL_TYPE:
					return "ChannelType";
				default:
					return base.getPropertyName(apiName);
			}
		}
		#endregion
	}
}

