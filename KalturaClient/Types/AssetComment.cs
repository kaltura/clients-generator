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
	public class AssetComment : SocialComment
	{
		#region Constants
		public const string ID = "id";
		public const string ASSET_ID = "assetId";
		public const string ASSET_TYPE = "assetType";
		public const string SUB_HEADER = "subHeader";
		#endregion

		#region Private Fields
		private int _Id = Int32.MinValue;
		private int _AssetId = Int32.MinValue;
		private AssetType _AssetType = null;
		private string _SubHeader = null;
		#endregion

		#region Properties
		[JsonProperty]
		public int Id
		{
			get { return _Id; }
			set 
			{ 
				_Id = value;
				OnPropertyChanged("Id");
			}
		}
		[JsonProperty]
		public int AssetId
		{
			get { return _AssetId; }
			set 
			{ 
				_AssetId = value;
				OnPropertyChanged("AssetId");
			}
		}
		[JsonProperty]
		public AssetType AssetType
		{
			get { return _AssetType; }
			set 
			{ 
				_AssetType = value;
				OnPropertyChanged("AssetType");
			}
		}
		[JsonProperty]
		public string SubHeader
		{
			get { return _SubHeader; }
			set 
			{ 
				_SubHeader = value;
				OnPropertyChanged("SubHeader");
			}
		}
		#endregion

		#region CTor
		public AssetComment()
		{
		}

		public AssetComment(JToken node) : base(node)
		{
			if(node["id"] != null)
			{
				this._Id = ParseInt(node["id"].Value<string>());
			}
			if(node["assetId"] != null)
			{
				this._AssetId = ParseInt(node["assetId"].Value<string>());
			}
			if(node["assetType"] != null)
			{
				this._AssetType = (AssetType)StringEnum.Parse(typeof(AssetType), node["assetType"].Value<string>());
			}
			if(node["subHeader"] != null)
			{
				this._SubHeader = node["subHeader"].Value<string>();
			}
		}
		#endregion

		#region Methods
		public override Params ToParams(bool includeObjectType = true)
		{
			Params kparams = base.ToParams(includeObjectType);
			if (includeObjectType)
				kparams.AddReplace("objectType", "KalturaAssetComment");
			kparams.AddIfNotNull("id", this._Id);
			kparams.AddIfNotNull("assetId", this._AssetId);
			kparams.AddIfNotNull("assetType", this._AssetType);
			kparams.AddIfNotNull("subHeader", this._SubHeader);
			return kparams;
		}
		protected override string getPropertyName(string apiName)
		{
			switch(apiName)
			{
				case ID:
					return "Id";
				case ASSET_ID:
					return "AssetId";
				case ASSET_TYPE:
					return "AssetType";
				case SUB_HEADER:
					return "SubHeader";
				default:
					return base.getPropertyName(apiName);
			}
		}
		#endregion
	}
}

