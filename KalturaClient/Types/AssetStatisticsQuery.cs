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
	public class AssetStatisticsQuery : ObjectBase
	{
		#region Constants
		public const string ASSET_ID_IN = "assetIdIn";
		public const string ASSET_TYPE_EQUAL = "assetTypeEqual";
		public const string START_DATE_GREATER_THAN_OR_EQUAL = "startDateGreaterThanOrEqual";
		public const string END_DATE_GREATER_THAN_OR_EQUAL = "endDateGreaterThanOrEqual";
		#endregion

		#region Private Fields
		private string _AssetIdIn = null;
		private AssetType _AssetTypeEqual = null;
		private long _StartDateGreaterThanOrEqual = long.MinValue;
		private long _EndDateGreaterThanOrEqual = long.MinValue;
		#endregion

		#region Properties
		public string AssetIdIn
		{
			get { return _AssetIdIn; }
			set 
			{ 
				_AssetIdIn = value;
				OnPropertyChanged("AssetIdIn");
			}
		}
		public AssetType AssetTypeEqual
		{
			get { return _AssetTypeEqual; }
			set 
			{ 
				_AssetTypeEqual = value;
				OnPropertyChanged("AssetTypeEqual");
			}
		}
		public long StartDateGreaterThanOrEqual
		{
			get { return _StartDateGreaterThanOrEqual; }
			set 
			{ 
				_StartDateGreaterThanOrEqual = value;
				OnPropertyChanged("StartDateGreaterThanOrEqual");
			}
		}
		public long EndDateGreaterThanOrEqual
		{
			get { return _EndDateGreaterThanOrEqual; }
			set 
			{ 
				_EndDateGreaterThanOrEqual = value;
				OnPropertyChanged("EndDateGreaterThanOrEqual");
			}
		}
		#endregion

		#region CTor
		public AssetStatisticsQuery()
		{
		}

		public AssetStatisticsQuery(XmlElement node) : base(node)
		{
			foreach (XmlElement propertyNode in node.ChildNodes)
			{
				switch (propertyNode.Name)
				{
					case "assetIdIn":
						this._AssetIdIn = propertyNode.InnerText;
						continue;
					case "assetTypeEqual":
						this._AssetTypeEqual = (AssetType)StringEnum.Parse(typeof(AssetType), propertyNode.InnerText);
						continue;
					case "startDateGreaterThanOrEqual":
						this._StartDateGreaterThanOrEqual = ParseLong(propertyNode.InnerText);
						continue;
					case "endDateGreaterThanOrEqual":
						this._EndDateGreaterThanOrEqual = ParseLong(propertyNode.InnerText);
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
				kparams.AddReplace("objectType", "KalturaAssetStatisticsQuery");
			kparams.AddIfNotNull("assetIdIn", this._AssetIdIn);
			kparams.AddIfNotNull("assetTypeEqual", this._AssetTypeEqual);
			kparams.AddIfNotNull("startDateGreaterThanOrEqual", this._StartDateGreaterThanOrEqual);
			kparams.AddIfNotNull("endDateGreaterThanOrEqual", this._EndDateGreaterThanOrEqual);
			return kparams;
		}
		protected override string getPropertyName(string apiName)
		{
			switch(apiName)
			{
				case ASSET_ID_IN:
					return "AssetIdIn";
				case ASSET_TYPE_EQUAL:
					return "AssetTypeEqual";
				case START_DATE_GREATER_THAN_OR_EQUAL:
					return "StartDateGreaterThanOrEqual";
				case END_DATE_GREATER_THAN_OR_EQUAL:
					return "EndDateGreaterThanOrEqual";
				default:
					return base.getPropertyName(apiName);
			}
		}
		#endregion
	}
}

