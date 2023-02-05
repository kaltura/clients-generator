// ===================================================================================================
//                           _  __     _ _
//                          | |/ /__ _| | |_ _  _ _ _ __ _
//                          | ' </ _` | |  _| || | '_/ _` |
//                          |_|\_\__,_|_|\__|\_,_|_| \__,_|
//
// This file is part of the Kaltura Collaborative Media Suite which allows users
// to do with audio, video, and animation what Wiki platforms allow them to do with
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
	public class PreviewModule : ObjectBase
	{
		#region Constants
		public const string ID = "id";
		public const string NAME = "name";
		public const string LIFE_CYCLE = "lifeCycle";
		public const string NON_RENEWABLE_PERIOD = "nonRenewablePeriod";
		#endregion

		#region Private Fields
		private long _Id = long.MinValue;
		private string _Name = null;
		private int _LifeCycle = Int32.MinValue;
		private int _NonRenewablePeriod = Int32.MinValue;
		#endregion

		#region Properties
		[JsonProperty]
		public long Id
		{
			get { return _Id; }
			private set 
			{ 
				_Id = value;
				OnPropertyChanged("Id");
			}
		}
		[JsonProperty]
		public string Name
		{
			get { return _Name; }
			set 
			{ 
				_Name = value;
				OnPropertyChanged("Name");
			}
		}
		[JsonProperty]
		public int LifeCycle
		{
			get { return _LifeCycle; }
			set 
			{ 
				_LifeCycle = value;
				OnPropertyChanged("LifeCycle");
			}
		}
		[JsonProperty]
		public int NonRenewablePeriod
		{
			get { return _NonRenewablePeriod; }
			set 
			{ 
				_NonRenewablePeriod = value;
				OnPropertyChanged("NonRenewablePeriod");
			}
		}
		#endregion

		#region CTor
		public PreviewModule()
		{
		}

		public PreviewModule(JToken node) : base(node)
		{
			if(node["id"] != null)
			{
				this._Id = ParseLong(node["id"].Value<string>());
			}
			if(node["name"] != null)
			{
				this._Name = node["name"].Value<string>();
			}
			if(node["lifeCycle"] != null)
			{
				this._LifeCycle = ParseInt(node["lifeCycle"].Value<string>());
			}
			if(node["nonRenewablePeriod"] != null)
			{
				this._NonRenewablePeriod = ParseInt(node["nonRenewablePeriod"].Value<string>());
			}
		}
		#endregion

		#region Methods
		public override Params ToParams(bool includeObjectType = true)
		{
			Params kparams = base.ToParams(includeObjectType);
			if (includeObjectType)
				kparams.AddReplace("objectType", "KalturaPreviewModule");
			kparams.AddIfNotNull("id", this._Id);
			kparams.AddIfNotNull("name", this._Name);
			kparams.AddIfNotNull("lifeCycle", this._LifeCycle);
			kparams.AddIfNotNull("nonRenewablePeriod", this._NonRenewablePeriod);
			return kparams;
		}
		protected override string getPropertyName(string apiName)
		{
			switch(apiName)
			{
				case ID:
					return "Id";
				case NAME:
					return "Name";
				case LIFE_CYCLE:
					return "LifeCycle";
				case NON_RENEWABLE_PERIOD:
					return "NonRenewablePeriod";
				default:
					return base.getPropertyName(apiName);
			}
		}
		#endregion
	}
}

