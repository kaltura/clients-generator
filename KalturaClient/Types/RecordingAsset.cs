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
// Copyright (C) 2006-2019  Kaltura Inc.
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
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Kaltura.Types
{
	public class RecordingAsset : ProgramAsset
	{
		#region Constants
		public const string RECORDING_ID = "recordingId";
		public const string RECORDING_TYPE = "recordingType";
		#endregion

		#region Private Fields
		private string _RecordingId = null;
		private RecordingType _RecordingType = null;
		#endregion

		#region Properties
		[JsonProperty]
		public string RecordingId
		{
			get { return _RecordingId; }
			set 
			{ 
				_RecordingId = value;
				OnPropertyChanged("RecordingId");
			}
		}
		[JsonProperty]
		public RecordingType RecordingType
		{
			get { return _RecordingType; }
			set 
			{ 
				_RecordingType = value;
				OnPropertyChanged("RecordingType");
			}
		}
		#endregion

		#region CTor
		public RecordingAsset()
		{
		}

		public RecordingAsset(JToken node) : base(node)
		{
			if(node["recordingId"] != null)
			{
				this._RecordingId = node["recordingId"].Value<string>();
			}
			if(node["recordingType"] != null)
			{
				this._RecordingType = (RecordingType)StringEnum.Parse(typeof(RecordingType), node["recordingType"].Value<string>());
			}
		}
		#endregion

		#region Methods
		public override Params ToParams(bool includeObjectType = true)
		{
			Params kparams = base.ToParams(includeObjectType);
			if (includeObjectType)
				kparams.AddReplace("objectType", "KalturaRecordingAsset");
			kparams.AddIfNotNull("recordingId", this._RecordingId);
			kparams.AddIfNotNull("recordingType", this._RecordingType);
			return kparams;
		}
		protected override string getPropertyName(string apiName)
		{
			switch(apiName)
			{
				case RECORDING_ID:
					return "RecordingId";
				case RECORDING_TYPE:
					return "RecordingType";
				default:
					return base.getPropertyName(apiName);
			}
		}
		#endregion
	}
}

