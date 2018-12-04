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
		public int Id
		{
			get { return _Id; }
			set 
			{ 
				_Id = value;
				OnPropertyChanged("Id");
			}
		}
		public int AssetId
		{
			get { return _AssetId; }
			set 
			{ 
				_AssetId = value;
				OnPropertyChanged("AssetId");
			}
		}
		public AssetType AssetType
		{
			get { return _AssetType; }
			set 
			{ 
				_AssetType = value;
				OnPropertyChanged("AssetType");
			}
		}
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

		public AssetComment(XmlElement node) : base(node)
		{
			foreach (XmlElement propertyNode in node.ChildNodes)
			{
				switch (propertyNode.Name)
				{
					case "id":
						this._Id = ParseInt(propertyNode.InnerText);
						continue;
					case "assetId":
						this._AssetId = ParseInt(propertyNode.InnerText);
						continue;
					case "assetType":
						this._AssetType = (AssetType)StringEnum.Parse(typeof(AssetType), propertyNode.InnerText);
						continue;
					case "subHeader":
						this._SubHeader = propertyNode.InnerText;
						continue;
				}
			}
		}

		public AssetComment(IDictionary<string,object> data) : base(data)
		{
			    this._Id = data.TryGetValueSafe<int>("id");
			    this._AssetId = data.TryGetValueSafe<int>("assetId");
			    this._AssetType = (AssetType)StringEnum.Parse(typeof(AssetType), data.TryGetValueSafe<string>("assetType"));
			    this._SubHeader = data.TryGetValueSafe<string>("subHeader");
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

