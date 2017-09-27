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
// Copyright (C) 2006-2017  Kaltura Inc.
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
	public class ScheduledRecordingProgramFilter : AssetFilter
	{
		#region Constants
		public const string RECORDING_TYPE_EQUAL = "recordingTypeEqual";
		public const string CHANNELS_IN = "channelsIn";
		public const string START_DATE_GREATER_THAN_OR_NULL = "startDateGreaterThanOrNull";
		public const string END_DATE_LESS_THAN_OR_NULL = "endDateLessThanOrNull";
		#endregion

		#region Private Fields
		private ScheduledRecordingAssetType _RecordingTypeEqual = null;
		private string _ChannelsIn = null;
		private long _StartDateGreaterThanOrNull = long.MinValue;
		private long _EndDateLessThanOrNull = long.MinValue;
		#endregion

		#region Properties
		public ScheduledRecordingAssetType RecordingTypeEqual
		{
			get { return _RecordingTypeEqual; }
			set 
			{ 
				_RecordingTypeEqual = value;
				OnPropertyChanged("RecordingTypeEqual");
			}
		}
		public string ChannelsIn
		{
			get { return _ChannelsIn; }
			set 
			{ 
				_ChannelsIn = value;
				OnPropertyChanged("ChannelsIn");
			}
		}
		public long StartDateGreaterThanOrNull
		{
			get { return _StartDateGreaterThanOrNull; }
			set 
			{ 
				_StartDateGreaterThanOrNull = value;
				OnPropertyChanged("StartDateGreaterThanOrNull");
			}
		}
		public long EndDateLessThanOrNull
		{
			get { return _EndDateLessThanOrNull; }
			set 
			{ 
				_EndDateLessThanOrNull = value;
				OnPropertyChanged("EndDateLessThanOrNull");
			}
		}
		#endregion

		#region CTor
		public ScheduledRecordingProgramFilter()
		{
		}

		public ScheduledRecordingProgramFilter(XmlElement node) : base(node)
		{
			foreach (XmlElement propertyNode in node.ChildNodes)
			{
				switch (propertyNode.Name)
				{
					case "recordingTypeEqual":
						this._RecordingTypeEqual = (ScheduledRecordingAssetType)StringEnum.Parse(typeof(ScheduledRecordingAssetType), propertyNode.InnerText);
						continue;
					case "channelsIn":
						this._ChannelsIn = propertyNode.InnerText;
						continue;
					case "startDateGreaterThanOrNull":
						this._StartDateGreaterThanOrNull = ParseLong(propertyNode.InnerText);
						continue;
					case "endDateLessThanOrNull":
						this._EndDateLessThanOrNull = ParseLong(propertyNode.InnerText);
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
				kparams.AddReplace("objectType", "KalturaScheduledRecordingProgramFilter");
			kparams.AddIfNotNull("recordingTypeEqual", this._RecordingTypeEqual);
			kparams.AddIfNotNull("channelsIn", this._ChannelsIn);
			kparams.AddIfNotNull("startDateGreaterThanOrNull", this._StartDateGreaterThanOrNull);
			kparams.AddIfNotNull("endDateLessThanOrNull", this._EndDateLessThanOrNull);
			return kparams;
		}
		protected override string getPropertyName(string apiName)
		{
			switch(apiName)
			{
				case RECORDING_TYPE_EQUAL:
					return "RecordingTypeEqual";
				case CHANNELS_IN:
					return "ChannelsIn";
				case START_DATE_GREATER_THAN_OR_NULL:
					return "StartDateGreaterThanOrNull";
				case END_DATE_LESS_THAN_OR_NULL:
					return "EndDateLessThanOrNull";
				default:
					return base.getPropertyName(apiName);
			}
		}
		#endregion
	}
}

