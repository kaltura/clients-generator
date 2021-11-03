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
	public class AssetCount : ObjectBase
	{
		#region Constants
		public const string VALUE = "value";
		public const string COUNT = "count";
		public const string SUBS = "subs";
		#endregion

		#region Private Fields
		private string _Value = null;
		private int _Count = Int32.MinValue;
		private IList<AssetsCount> _Subs;
		#endregion

		#region Properties
		[JsonProperty]
		public string Value
		{
			get { return _Value; }
			set 
			{ 
				_Value = value;
				OnPropertyChanged("Value");
			}
		}
		[JsonProperty]
		public int Count
		{
			get { return _Count; }
			set 
			{ 
				_Count = value;
				OnPropertyChanged("Count");
			}
		}
		[JsonProperty]
		public IList<AssetsCount> Subs
		{
			get { return _Subs; }
			set 
			{ 
				_Subs = value;
				OnPropertyChanged("Subs");
			}
		}
		#endregion

		#region CTor
		public AssetCount()
		{
		}

		public AssetCount(JToken node) : base(node)
		{
			if(node["value"] != null)
			{
				this._Value = node["value"].Value<string>();
			}
			if(node["count"] != null)
			{
				this._Count = ParseInt(node["count"].Value<string>());
			}
			if(node["subs"] != null)
			{
				this._Subs = new List<AssetsCount>();
				foreach(var arrayNode in node["subs"].Children())
				{
					this._Subs.Add(ObjectFactory.Create<AssetsCount>(arrayNode));
				}
			}
		}
		#endregion

		#region Methods
		public override Params ToParams(bool includeObjectType = true)
		{
			Params kparams = base.ToParams(includeObjectType);
			if (includeObjectType)
				kparams.AddReplace("objectType", "KalturaAssetCount");
			kparams.AddIfNotNull("value", this._Value);
			kparams.AddIfNotNull("count", this._Count);
			kparams.AddIfNotNull("subs", this._Subs);
			return kparams;
		}
		protected override string getPropertyName(string apiName)
		{
			switch(apiName)
			{
				case VALUE:
					return "Value";
				case COUNT:
					return "Count";
				case SUBS:
					return "Subs";
				default:
					return base.getPropertyName(apiName);
			}
		}
		#endregion
	}
}

