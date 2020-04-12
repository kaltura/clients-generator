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
using System.Xml;
using System.Collections.Generic;
using Kaltura.Enums;
using Kaltura.Request;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Kaltura.Types
{
	public class IotDefault : ObjectBase
	{
		#region Constants
		public const string POOL_ID = "poolId";
		public const string REGION = "region";
		public const string APP_CLIENT_ID = "appClientId";
		#endregion

		#region Private Fields
		private string _PoolId = null;
		private string _Region = null;
		private string _AppClientId = null;
		#endregion

		#region Properties
		[JsonProperty]
		public string PoolId
		{
			get { return _PoolId; }
			set 
			{ 
				_PoolId = value;
				OnPropertyChanged("PoolId");
			}
		}
		[JsonProperty]
		public string Region
		{
			get { return _Region; }
			set 
			{ 
				_Region = value;
				OnPropertyChanged("Region");
			}
		}
		[JsonProperty]
		public string AppClientId
		{
			get { return _AppClientId; }
			set 
			{ 
				_AppClientId = value;
				OnPropertyChanged("AppClientId");
			}
		}
		#endregion

		#region CTor
		public IotDefault()
		{
		}

		public IotDefault(JToken node) : base(node)
		{
			if(node["poolId"] != null)
			{
				this._PoolId = node["poolId"].Value<string>();
			}
			if(node["region"] != null)
			{
				this._Region = node["region"].Value<string>();
			}
			if(node["appClientId"] != null)
			{
				this._AppClientId = node["appClientId"].Value<string>();
			}
		}
		#endregion

		#region Methods
		public override Params ToParams(bool includeObjectType = true)
		{
			Params kparams = base.ToParams(includeObjectType);
			if (includeObjectType)
				kparams.AddReplace("objectType", "KalturaIotDefault");
			kparams.AddIfNotNull("poolId", this._PoolId);
			kparams.AddIfNotNull("region", this._Region);
			kparams.AddIfNotNull("appClientId", this._AppClientId);
			return kparams;
		}
		protected override string getPropertyName(string apiName)
		{
			switch(apiName)
			{
				case POOL_ID:
					return "PoolId";
				case REGION:
					return "Region";
				case APP_CLIENT_ID:
					return "AppClientId";
				default:
					return base.getPropertyName(apiName);
			}
		}
		#endregion
	}
}

