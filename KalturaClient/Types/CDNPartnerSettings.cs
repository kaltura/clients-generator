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
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Kaltura.Types
{
	public class CDNPartnerSettings : ObjectBase
	{
		#region Constants
		public const string DEFAULT_ADAPTER_ID = "defaultAdapterId";
		public const string DEFAULT_RECORDING_ADAPTER_ID = "defaultRecordingAdapterId";
		#endregion

		#region Private Fields
		private int _DefaultAdapterId = Int32.MinValue;
		private int _DefaultRecordingAdapterId = Int32.MinValue;
		#endregion

		#region Properties
		[JsonProperty]
		public int DefaultAdapterId
		{
			get { return _DefaultAdapterId; }
			set 
			{ 
				_DefaultAdapterId = value;
				OnPropertyChanged("DefaultAdapterId");
			}
		}
		[JsonProperty]
		public int DefaultRecordingAdapterId
		{
			get { return _DefaultRecordingAdapterId; }
			set 
			{ 
				_DefaultRecordingAdapterId = value;
				OnPropertyChanged("DefaultRecordingAdapterId");
			}
		}
		#endregion

		#region CTor
		public CDNPartnerSettings()
		{
		}

		public CDNPartnerSettings(JToken node) : base(node)
		{
			if(node["defaultAdapterId"] != null)
			{
				this._DefaultAdapterId = ParseInt(node["defaultAdapterId"].Value<string>());
			}
			if(node["defaultRecordingAdapterId"] != null)
			{
				this._DefaultRecordingAdapterId = ParseInt(node["defaultRecordingAdapterId"].Value<string>());
			}
		}
		#endregion

		#region Methods
		public override Params ToParams(bool includeObjectType = true)
		{
			Params kparams = base.ToParams(includeObjectType);
			if (includeObjectType)
				kparams.AddReplace("objectType", "KalturaCDNPartnerSettings");
			kparams.AddIfNotNull("defaultAdapterId", this._DefaultAdapterId);
			kparams.AddIfNotNull("defaultRecordingAdapterId", this._DefaultRecordingAdapterId);
			return kparams;
		}
		protected override string getPropertyName(string apiName)
		{
			switch(apiName)
			{
				case DEFAULT_ADAPTER_ID:
					return "DefaultAdapterId";
				case DEFAULT_RECORDING_ADAPTER_ID:
					return "DefaultRecordingAdapterId";
				default:
					return base.getPropertyName(apiName);
			}
		}
		#endregion
	}
}

