// ===================================================================================================
//                           _  __     _ _
//                          | |/ /__ _| | |_ _  _ _ _ __ _
//                          | ' </ _` | |  _| || | '_/ _` |
//                          |_|\_\__,_|_|\__|\_,_|_| \__,_|
//
// This file is part of the Kaltura Collaborative Media Suite which allows users
// to do with audio, video, and animation what Wiki platforms allow them to do with
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
using System.Collections.Generic;
using Kaltura.Enums;
using Kaltura.Request;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Kaltura.Types
{
	public class StreamingDevice : ObjectBase
	{
		#region Constants
		public const string ASSET = "asset";
		public const string USER_ID = "userId";
		public const string UDID = "udid";
		#endregion

		#region Private Fields
		private SlimAsset _Asset;
		private string _UserId = null;
		private string _Udid = null;
		#endregion

		#region Properties
		[JsonProperty]
		public SlimAsset Asset
		{
			get { return _Asset; }
			private set 
			{ 
				_Asset = value;
				OnPropertyChanged("Asset");
			}
		}
		[JsonProperty]
		public string UserId
		{
			get { return _UserId; }
			private set 
			{ 
				_UserId = value;
				OnPropertyChanged("UserId");
			}
		}
		[JsonProperty]
		public string Udid
		{
			get { return _Udid; }
			set 
			{ 
				_Udid = value;
				OnPropertyChanged("Udid");
			}
		}
		#endregion

		#region CTor
		public StreamingDevice()
		{
		}

		public StreamingDevice(JToken node) : base(node)
		{
			if(node["asset"] != null)
			{
				this._Asset = ObjectFactory.Create<SlimAsset>(node["asset"]);
			}
			if(node["userId"] != null)
			{
				this._UserId = node["userId"].Value<string>();
			}
			if(node["udid"] != null)
			{
				this._Udid = node["udid"].Value<string>();
			}
		}
		#endregion

		#region Methods
		public override Params ToParams(bool includeObjectType = true)
		{
			Params kparams = base.ToParams(includeObjectType);
			if (includeObjectType)
				kparams.AddReplace("objectType", "KalturaStreamingDevice");
			kparams.AddIfNotNull("asset", this._Asset);
			kparams.AddIfNotNull("userId", this._UserId);
			kparams.AddIfNotNull("udid", this._Udid);
			return kparams;
		}
		protected override string getPropertyName(string apiName)
		{
			switch(apiName)
			{
				case ASSET:
					return "Asset";
				case USER_ID:
					return "UserId";
				case UDID:
					return "Udid";
				default:
					return base.getPropertyName(apiName);
			}
		}
		#endregion
	}
}

