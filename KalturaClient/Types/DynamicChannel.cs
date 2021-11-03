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
	public class DynamicChannel : Channel
	{
		#region Constants
		public const string KSQL = "kSql";
		public const string ASSET_TYPES = "assetTypes";
		public const string GROUP_BY = "groupBy";
		#endregion

		#region Private Fields
		private string _KSql = null;
		private IList<IntegerValue> _AssetTypes;
		private AssetGroupBy _GroupBy;
		#endregion

		#region Properties
		[JsonProperty]
		public string KSql
		{
			get { return _KSql; }
			set 
			{ 
				_KSql = value;
				OnPropertyChanged("KSql");
			}
		}
		[JsonProperty]
		public IList<IntegerValue> AssetTypes
		{
			get { return _AssetTypes; }
			set 
			{ 
				_AssetTypes = value;
				OnPropertyChanged("AssetTypes");
			}
		}
		[JsonProperty]
		public AssetGroupBy GroupBy
		{
			get { return _GroupBy; }
			set 
			{ 
				_GroupBy = value;
				OnPropertyChanged("GroupBy");
			}
		}
		#endregion

		#region CTor
		public DynamicChannel()
		{
		}

		public DynamicChannel(JToken node) : base(node)
		{
			if(node["kSql"] != null)
			{
				this._KSql = node["kSql"].Value<string>();
			}
			if(node["assetTypes"] != null)
			{
				this._AssetTypes = new List<IntegerValue>();
				foreach(var arrayNode in node["assetTypes"].Children())
				{
					this._AssetTypes.Add(ObjectFactory.Create<IntegerValue>(arrayNode));
				}
			}
			if(node["groupBy"] != null)
			{
				this._GroupBy = ObjectFactory.Create<AssetGroupBy>(node["groupBy"]);
			}
		}
		#endregion

		#region Methods
		public override Params ToParams(bool includeObjectType = true)
		{
			Params kparams = base.ToParams(includeObjectType);
			if (includeObjectType)
				kparams.AddReplace("objectType", "KalturaDynamicChannel");
			kparams.AddIfNotNull("kSql", this._KSql);
			kparams.AddIfNotNull("assetTypes", this._AssetTypes);
			kparams.AddIfNotNull("groupBy", this._GroupBy);
			return kparams;
		}
		protected override string getPropertyName(string apiName)
		{
			switch(apiName)
			{
				case KSQL:
					return "KSql";
				case ASSET_TYPES:
					return "AssetTypes";
				case GROUP_BY:
					return "GroupBy";
				default:
					return base.getPropertyName(apiName);
			}
		}
		#endregion
	}
}

