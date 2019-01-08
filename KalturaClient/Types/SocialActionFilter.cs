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
	public class SocialActionFilter : Filter
	{
		#region Constants
		public const string ASSET_ID_IN = "assetIdIn";
		public const string ASSET_TYPE_EQUAL = "assetTypeEqual";
		public const string ACTION_TYPE_IN = "actionTypeIn";
		public new const string ORDER_BY = "orderBy";
		#endregion

		#region Private Fields
		private string _AssetIdIn = null;
		private AssetType _AssetTypeEqual = null;
		private string _ActionTypeIn = null;
		private SocialActionOrderBy _OrderBy = null;
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
		public string ActionTypeIn
		{
			get { return _ActionTypeIn; }
			set 
			{ 
				_ActionTypeIn = value;
				OnPropertyChanged("ActionTypeIn");
			}
		}
		public new SocialActionOrderBy OrderBy
		{
			get { return _OrderBy; }
			set 
			{ 
				_OrderBy = value;
				OnPropertyChanged("OrderBy");
			}
		}
		#endregion

		#region CTor
		public SocialActionFilter()
		{
		}

		public SocialActionFilter(XmlElement node) : base(node)
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
					case "actionTypeIn":
						this._ActionTypeIn = propertyNode.InnerText;
						continue;
					case "orderBy":
						this._OrderBy = (SocialActionOrderBy)StringEnum.Parse(typeof(SocialActionOrderBy), propertyNode.InnerText);
						continue;
				}
			}
		}

		public SocialActionFilter(IDictionary<string,object> data) : base(data)
		{
			    this._AssetIdIn = data.TryGetValueSafe<string>("assetIdIn");
			    this._AssetTypeEqual = (AssetType)StringEnum.Parse(typeof(AssetType), data.TryGetValueSafe<string>("assetTypeEqual"));
			    this._ActionTypeIn = data.TryGetValueSafe<string>("actionTypeIn");
			    this._OrderBy = (SocialActionOrderBy)StringEnum.Parse(typeof(SocialActionOrderBy), data.TryGetValueSafe<string>("orderBy"));
		}
		#endregion

		#region Methods
		public override Params ToParams(bool includeObjectType = true)
		{
			Params kparams = base.ToParams(includeObjectType);
			if (includeObjectType)
				kparams.AddReplace("objectType", "KalturaSocialActionFilter");
			kparams.AddIfNotNull("assetIdIn", this._AssetIdIn);
			kparams.AddIfNotNull("assetTypeEqual", this._AssetTypeEqual);
			kparams.AddIfNotNull("actionTypeIn", this._ActionTypeIn);
			kparams.AddIfNotNull("orderBy", this._OrderBy);
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
				case ACTION_TYPE_IN:
					return "ActionTypeIn";
				case ORDER_BY:
					return "OrderBy";
				default:
					return base.getPropertyName(apiName);
			}
		}
		#endregion
	}
}

