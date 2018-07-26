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
		public const string BUFFER_CATCH_UP = "bufferCatchUp";
		public const string BUFFER_TRICK_PLAY = "bufferTrickPlay";
		public const string ENABLE_RECORDING_PLAYBACK_NON_ENTITLED_CHANNEL_STATE = "enableRecordingPlaybackNonEntitledChannelState";
		public const string ENABLE_TRICK_PLAY_STATE = "enableTrickPlayState";
		public const string EXTERNAL_EPG_INGEST_ID = "externalEpgIngestId";
		public const string EXTERNAL_CDVR_ID = "externalCdvrId";
		public const string CDVR_ENABLED = "cdvrEnabled";
		public const string CATCH_UP_ENABLED = "catchUpEnabled";
		public const string START_OVER_ENABLED = "startOverEnabled";
		public const string SUMMED_CATCH_UP_BUFFER = "summedCatchUpBuffer";
		public const string SUMMED_TRICK_PLAY_BUFFER = "summedTrickPlayBuffer";
		public const string RECORDING_PLAYBACK_NON_ENTITLED_CHANNEL_ENABLED = "recordingPlaybackNonEntitledChannelEnabled";
		public const string TRICK_PLAY_ENABLED = "trickPlayEnabled";
		public const string CHANNEL_TYPE = "channelType";
		#endregion

		#region Private Fields
		private TimeShiftedTvState _EnableCdvrState = null;
		private TimeShiftedTvState _EnableCatchUpState = null;
		private TimeShiftedTvState _EnableStartOverState = null;
		private long _BufferCatchUp = long.MinValue;
		private long _BufferTrickPlay = long.MinValue;
		private TimeShiftedTvState _EnableRecordingPlaybackNonEntitledChannelState = null;
		private TimeShiftedTvState _EnableTrickPlayState = null;
		private string _ExternalEpgIngestId = null;
		private string _ExternalCdvrId = null;
		private bool? _CdvrEnabled = null;
		private bool? _CatchUpEnabled = null;
		private bool? _StartOverEnabled = null;
		private long _SummedCatchUpBuffer = long.MinValue;
		private long _SummedTrickPlayBuffer = long.MinValue;
		private bool? _RecordingPlaybackNonEntitledChannelEnabled = null;
		private bool? _TrickPlayEnabled = null;
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
		public long BufferCatchUp
		{
			get { return _BufferCatchUp; }
			set 
			{ 
				_BufferCatchUp = value;
				OnPropertyChanged("BufferCatchUp");
			}
		}
		public long BufferTrickPlay
		{
			get { return _BufferTrickPlay; }
			set 
			{ 
				_BufferTrickPlay = value;
				OnPropertyChanged("BufferTrickPlay");
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
		public bool? CdvrEnabled
		{
			get { return _CdvrEnabled; }
		}
		public bool? CatchUpEnabled
		{
			get { return _CatchUpEnabled; }
		}
		public bool? StartOverEnabled
		{
			get { return _StartOverEnabled; }
		}
		public long SummedCatchUpBuffer
		{
			get { return _SummedCatchUpBuffer; }
		}
		public long SummedTrickPlayBuffer
		{
			get { return _SummedTrickPlayBuffer; }
		}
		public bool? RecordingPlaybackNonEntitledChannelEnabled
		{
			get { return _RecordingPlaybackNonEntitledChannelEnabled; }
		}
		public bool? TrickPlayEnabled
		{
			get { return _TrickPlayEnabled; }
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
					case "bufferCatchUp":
						this._BufferCatchUp = ParseLong(propertyNode.InnerText);
						continue;
					case "bufferTrickPlay":
						this._BufferTrickPlay = ParseLong(propertyNode.InnerText);
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
					case "cdvrEnabled":
						this._CdvrEnabled = ParseBool(propertyNode.InnerText);
						continue;
					case "catchUpEnabled":
						this._CatchUpEnabled = ParseBool(propertyNode.InnerText);
						continue;
					case "startOverEnabled":
						this._StartOverEnabled = ParseBool(propertyNode.InnerText);
						continue;
					case "summedCatchUpBuffer":
						this._SummedCatchUpBuffer = ParseLong(propertyNode.InnerText);
						continue;
					case "summedTrickPlayBuffer":
						this._SummedTrickPlayBuffer = ParseLong(propertyNode.InnerText);
						continue;
					case "recordingPlaybackNonEntitledChannelEnabled":
						this._RecordingPlaybackNonEntitledChannelEnabled = ParseBool(propertyNode.InnerText);
						continue;
					case "trickPlayEnabled":
						this._TrickPlayEnabled = ParseBool(propertyNode.InnerText);
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
			kparams.AddIfNotNull("bufferCatchUp", this._BufferCatchUp);
			kparams.AddIfNotNull("bufferTrickPlay", this._BufferTrickPlay);
			kparams.AddIfNotNull("enableRecordingPlaybackNonEntitledChannelState", this._EnableRecordingPlaybackNonEntitledChannelState);
			kparams.AddIfNotNull("enableTrickPlayState", this._EnableTrickPlayState);
			kparams.AddIfNotNull("externalEpgIngestId", this._ExternalEpgIngestId);
			kparams.AddIfNotNull("externalCdvrId", this._ExternalCdvrId);
			kparams.AddIfNotNull("cdvrEnabled", this._CdvrEnabled);
			kparams.AddIfNotNull("catchUpEnabled", this._CatchUpEnabled);
			kparams.AddIfNotNull("startOverEnabled", this._StartOverEnabled);
			kparams.AddIfNotNull("summedCatchUpBuffer", this._SummedCatchUpBuffer);
			kparams.AddIfNotNull("summedTrickPlayBuffer", this._SummedTrickPlayBuffer);
			kparams.AddIfNotNull("recordingPlaybackNonEntitledChannelEnabled", this._RecordingPlaybackNonEntitledChannelEnabled);
			kparams.AddIfNotNull("trickPlayEnabled", this._TrickPlayEnabled);
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
				case BUFFER_CATCH_UP:
					return "BufferCatchUp";
				case BUFFER_TRICK_PLAY:
					return "BufferTrickPlay";
				case ENABLE_RECORDING_PLAYBACK_NON_ENTITLED_CHANNEL_STATE:
					return "EnableRecordingPlaybackNonEntitledChannelState";
				case ENABLE_TRICK_PLAY_STATE:
					return "EnableTrickPlayState";
				case EXTERNAL_EPG_INGEST_ID:
					return "ExternalEpgIngestId";
				case EXTERNAL_CDVR_ID:
					return "ExternalCdvrId";
				case CDVR_ENABLED:
					return "CdvrEnabled";
				case CATCH_UP_ENABLED:
					return "CatchUpEnabled";
				case START_OVER_ENABLED:
					return "StartOverEnabled";
				case SUMMED_CATCH_UP_BUFFER:
					return "SummedCatchUpBuffer";
				case SUMMED_TRICK_PLAY_BUFFER:
					return "SummedTrickPlayBuffer";
				case RECORDING_PLAYBACK_NON_ENTITLED_CHANNEL_ENABLED:
					return "RecordingPlaybackNonEntitledChannelEnabled";
				case TRICK_PLAY_ENABLED:
					return "TrickPlayEnabled";
				case CHANNEL_TYPE:
					return "ChannelType";
				default:
					return base.getPropertyName(apiName);
			}
		}
		#endregion
	}
}

