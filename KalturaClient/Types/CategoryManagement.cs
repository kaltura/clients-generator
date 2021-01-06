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
	public class CategoryManagement : ObjectBase
	{
		#region Constants
		public const string DEFAULT_TREE_ID = "defaultTreeId";
		public const string DEVICE_FAMILY_TO_CATEGORY_TREE = "deviceFamilyToCategoryTree";
		#endregion

		#region Private Fields
		private long _DefaultTreeId = long.MinValue;
		private IDictionary<string, LongValue> _DeviceFamilyToCategoryTree;
		#endregion

		#region Properties
		[JsonProperty]
		public long DefaultTreeId
		{
			get { return _DefaultTreeId; }
			set 
			{ 
				_DefaultTreeId = value;
				OnPropertyChanged("DefaultTreeId");
			}
		}
		[JsonProperty]
		public IDictionary<string, LongValue> DeviceFamilyToCategoryTree
		{
			get { return _DeviceFamilyToCategoryTree; }
			set 
			{ 
				_DeviceFamilyToCategoryTree = value;
				OnPropertyChanged("DeviceFamilyToCategoryTree");
			}
		}
		#endregion

		#region CTor
		public CategoryManagement()
		{
		}

		public CategoryManagement(JToken node) : base(node)
		{
			if(node["defaultTreeId"] != null)
			{
				this._DefaultTreeId = ParseLong(node["defaultTreeId"].Value<string>());
			}
			if(node["deviceFamilyToCategoryTree"] != null)
			{
				{
					string key;
					this._DeviceFamilyToCategoryTree = new Dictionary<string, LongValue>();
					foreach(var arrayNode in node["deviceFamilyToCategoryTree"].Children<JProperty>())
					{
						key = arrayNode.Name;
						this._DeviceFamilyToCategoryTree[key] = ObjectFactory.Create<LongValue>(arrayNode.Value);
					}
				}
			}
		}
		#endregion

		#region Methods
		public override Params ToParams(bool includeObjectType = true)
		{
			Params kparams = base.ToParams(includeObjectType);
			if (includeObjectType)
				kparams.AddReplace("objectType", "KalturaCategoryManagement");
			kparams.AddIfNotNull("defaultTreeId", this._DefaultTreeId);
			kparams.AddIfNotNull("deviceFamilyToCategoryTree", this._DeviceFamilyToCategoryTree);
			return kparams;
		}
		protected override string getPropertyName(string apiName)
		{
			switch(apiName)
			{
				case DEFAULT_TREE_ID:
					return "DefaultTreeId";
				case DEVICE_FAMILY_TO_CATEGORY_TREE:
					return "DeviceFamilyToCategoryTree";
				default:
					return base.getPropertyName(apiName);
			}
		}
		#endregion
	}
}

