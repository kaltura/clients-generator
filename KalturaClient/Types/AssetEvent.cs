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
	public class AssetEvent : EventObject
	{
		#region Constants
		public const string USER_ID = "userId";
		public const string ASSET_ID = "assetId";
		public const string TYPE = "type";
		public const string EXTERNAL_ID = "externalId";
		#endregion

		#region Private Fields
		private long _UserId = long.MinValue;
		private long _AssetId = long.MinValue;
		private int _Type = Int32.MinValue;
		private string _ExternalId = null;
		#endregion

		#region Properties
		[JsonProperty]
		public long UserId
		{
			get { return _UserId; }
			private set 
			{ 
				_UserId = value;
				OnPropertyChanged("UserId");
			}
		}
		[JsonProperty]
		public long AssetId
		{
			get { return _AssetId; }
			private set 
			{ 
				_AssetId = value;
				OnPropertyChanged("AssetId");
			}
		}
		[JsonProperty]
		public int Type
		{
			get { return _Type; }
			private set 
			{ 
				_Type = value;
				OnPropertyChanged("Type");
			}
		}
		[JsonProperty]
		public string ExternalId
		{
			get { return _ExternalId; }
			private set 
			{ 
				_ExternalId = value;
				OnPropertyChanged("ExternalId");
			}
		}
		#endregion

		#region CTor
		public AssetEvent()
		{
		}

		public AssetEvent(JToken node) : base(node)
		{
			if(node["userId"] != null)
			{
				this._UserId = ParseLong(node["userId"].Value<string>());
			}
			if(node["assetId"] != null)
			{
				this._AssetId = ParseLong(node["assetId"].Value<string>());
			}
			if(node["type"] != null)
			{
				this._Type = ParseInt(node["type"].Value<string>());
			}
			if(node["externalId"] != null)
			{
				this._ExternalId = node["externalId"].Value<string>();
			}
		}
		#endregion

		#region Methods
		public override Params ToParams(bool includeObjectType = true)
		{
			Params kparams = base.ToParams(includeObjectType);
			if (includeObjectType)
				kparams.AddReplace("objectType", "KalturaAssetEvent");
			kparams.AddIfNotNull("userId", this._UserId);
			kparams.AddIfNotNull("assetId", this._AssetId);
			kparams.AddIfNotNull("type", this._Type);
			kparams.AddIfNotNull("externalId", this._ExternalId);
			return kparams;
		}
		protected override string getPropertyName(string apiName)
		{
			switch(apiName)
			{
				case USER_ID:
					return "UserId";
				case ASSET_ID:
					return "AssetId";
				case TYPE:
					return "Type";
				case EXTERNAL_ID:
					return "ExternalId";
				default:
					return base.getPropertyName(apiName);
			}
		}
		#endregion
	}
}

