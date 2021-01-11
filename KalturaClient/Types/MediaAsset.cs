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
	public class MediaAsset : Asset
	{
		#region Constants
		public const string EXTERNAL_IDS = "externalIds";
		public const string ENTRY_ID = "entryId";
		public const string DEVICE_RULE_ID = "deviceRuleId";
		public const string GEO_BLOCK_RULE_ID = "geoBlockRuleId";
		public const string STATUS = "status";
		public const string INHERITANCE_POLICY = "inheritancePolicy";
		#endregion

		#region Private Fields
		private string _ExternalIds = null;
		private string _EntryId = null;
		private int _DeviceRuleId = Int32.MinValue;
		private int _GeoBlockRuleId = Int32.MinValue;
		private bool? _Status = null;
		private AssetInheritancePolicy _InheritancePolicy = null;
		#endregion

		#region Properties
		[JsonProperty]
		public string ExternalIds
		{
			get { return _ExternalIds; }
			set 
			{ 
				_ExternalIds = value;
				OnPropertyChanged("ExternalIds");
			}
		}
		[JsonProperty]
		public string EntryId
		{
			get { return _EntryId; }
			set 
			{ 
				_EntryId = value;
				OnPropertyChanged("EntryId");
			}
		}
		[JsonProperty]
		public int DeviceRuleId
		{
			get { return _DeviceRuleId; }
			set 
			{ 
				_DeviceRuleId = value;
				OnPropertyChanged("DeviceRuleId");
			}
		}
		[JsonProperty]
		public int GeoBlockRuleId
		{
			get { return _GeoBlockRuleId; }
			set 
			{ 
				_GeoBlockRuleId = value;
				OnPropertyChanged("GeoBlockRuleId");
			}
		}
		[JsonProperty]
		public bool? Status
		{
			get { return _Status; }
			set 
			{ 
				_Status = value;
				OnPropertyChanged("Status");
			}
		}
		[JsonProperty]
		public AssetInheritancePolicy InheritancePolicy
		{
			get { return _InheritancePolicy; }
			set 
			{ 
				_InheritancePolicy = value;
				OnPropertyChanged("InheritancePolicy");
			}
		}
		#endregion

		#region CTor
		public MediaAsset()
		{
		}

		public MediaAsset(JToken node) : base(node)
		{
			if(node["externalIds"] != null)
			{
				this._ExternalIds = node["externalIds"].Value<string>();
			}
			if(node["entryId"] != null)
			{
				this._EntryId = node["entryId"].Value<string>();
			}
			if(node["deviceRuleId"] != null)
			{
				this._DeviceRuleId = ParseInt(node["deviceRuleId"].Value<string>());
			}
			if(node["geoBlockRuleId"] != null)
			{
				this._GeoBlockRuleId = ParseInt(node["geoBlockRuleId"].Value<string>());
			}
			if(node["status"] != null)
			{
				this._Status = ParseBool(node["status"].Value<string>());
			}
			if(node["inheritancePolicy"] != null)
			{
				this._InheritancePolicy = (AssetInheritancePolicy)StringEnum.Parse(typeof(AssetInheritancePolicy), node["inheritancePolicy"].Value<string>());
			}
		}
		#endregion

		#region Methods
		public override Params ToParams(bool includeObjectType = true)
		{
			Params kparams = base.ToParams(includeObjectType);
			if (includeObjectType)
				kparams.AddReplace("objectType", "KalturaMediaAsset");
			kparams.AddIfNotNull("externalIds", this._ExternalIds);
			kparams.AddIfNotNull("entryId", this._EntryId);
			kparams.AddIfNotNull("deviceRuleId", this._DeviceRuleId);
			kparams.AddIfNotNull("geoBlockRuleId", this._GeoBlockRuleId);
			kparams.AddIfNotNull("status", this._Status);
			kparams.AddIfNotNull("inheritancePolicy", this._InheritancePolicy);
			return kparams;
		}
		protected override string getPropertyName(string apiName)
		{
			switch(apiName)
			{
				case EXTERNAL_IDS:
					return "ExternalIds";
				case ENTRY_ID:
					return "EntryId";
				case DEVICE_RULE_ID:
					return "DeviceRuleId";
				case GEO_BLOCK_RULE_ID:
					return "GeoBlockRuleId";
				case STATUS:
					return "Status";
				case INHERITANCE_POLICY:
					return "InheritancePolicy";
				default:
					return base.getPropertyName(apiName);
			}
		}
		#endregion
	}
}

