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
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Kaltura.Types
{
	public class UsageModule : ObjectBase
	{
		#region Constants
		public const string ID = "id";
		public const string NAME = "name";
		public const string MAX_VIEWS_NUMBER = "maxViewsNumber";
		public const string VIEW_LIFE_CYCLE = "viewLifeCycle";
		public const string FULL_LIFE_CYCLE = "fullLifeCycle";
		public const string COUPON_ID = "couponId";
		public const string WAIVER_PERIOD = "waiverPeriod";
		public const string IS_WAIVER_ENABLED = "isWaiverEnabled";
		public const string IS_OFFLINE_PLAYBACK = "isOfflinePlayback";
		#endregion

		#region Private Fields
		private long _Id = long.MinValue;
		private string _Name = null;
		private int _MaxViewsNumber = Int32.MinValue;
		private int _ViewLifeCycle = Int32.MinValue;
		private int _FullLifeCycle = Int32.MinValue;
		private int _CouponId = Int32.MinValue;
		private int _WaiverPeriod = Int32.MinValue;
		private bool? _IsWaiverEnabled = null;
		private bool? _IsOfflinePlayback = null;
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
			private set 
			{ 
				_Name = value;
				OnPropertyChanged("Name");
			}
		}
		[JsonProperty]
		public int MaxViewsNumber
		{
			get { return _MaxViewsNumber; }
			private set 
			{ 
				_MaxViewsNumber = value;
				OnPropertyChanged("MaxViewsNumber");
			}
		}
		[JsonProperty]
		public int ViewLifeCycle
		{
			get { return _ViewLifeCycle; }
			private set 
			{ 
				_ViewLifeCycle = value;
				OnPropertyChanged("ViewLifeCycle");
			}
		}
		[JsonProperty]
		public int FullLifeCycle
		{
			get { return _FullLifeCycle; }
			private set 
			{ 
				_FullLifeCycle = value;
				OnPropertyChanged("FullLifeCycle");
			}
		}
		[JsonProperty]
		public int CouponId
		{
			get { return _CouponId; }
			private set 
			{ 
				_CouponId = value;
				OnPropertyChanged("CouponId");
			}
		}
		[JsonProperty]
		public int WaiverPeriod
		{
			get { return _WaiverPeriod; }
			private set 
			{ 
				_WaiverPeriod = value;
				OnPropertyChanged("WaiverPeriod");
			}
		}
		[JsonProperty]
		public bool? IsWaiverEnabled
		{
			get { return _IsWaiverEnabled; }
			private set 
			{ 
				_IsWaiverEnabled = value;
				OnPropertyChanged("IsWaiverEnabled");
			}
		}
		[JsonProperty]
		public bool? IsOfflinePlayback
		{
			get { return _IsOfflinePlayback; }
			private set 
			{ 
				_IsOfflinePlayback = value;
				OnPropertyChanged("IsOfflinePlayback");
			}
		}
		#endregion

		#region CTor
		public UsageModule()
		{
		}

		public UsageModule(JToken node) : base(node)
		{
			if(node["id"] != null)
			{
				this._Id = ParseLong(node["id"].Value<string>());
			}
			if(node["name"] != null)
			{
				this._Name = node["name"].Value<string>();
			}
			if(node["maxViewsNumber"] != null)
			{
				this._MaxViewsNumber = ParseInt(node["maxViewsNumber"].Value<string>());
			}
			if(node["viewLifeCycle"] != null)
			{
				this._ViewLifeCycle = ParseInt(node["viewLifeCycle"].Value<string>());
			}
			if(node["fullLifeCycle"] != null)
			{
				this._FullLifeCycle = ParseInt(node["fullLifeCycle"].Value<string>());
			}
			if(node["couponId"] != null)
			{
				this._CouponId = ParseInt(node["couponId"].Value<string>());
			}
			if(node["waiverPeriod"] != null)
			{
				this._WaiverPeriod = ParseInt(node["waiverPeriod"].Value<string>());
			}
			if(node["isWaiverEnabled"] != null)
			{
				this._IsWaiverEnabled = ParseBool(node["isWaiverEnabled"].Value<string>());
			}
			if(node["isOfflinePlayback"] != null)
			{
				this._IsOfflinePlayback = ParseBool(node["isOfflinePlayback"].Value<string>());
			}
		}
		#endregion

		#region Methods
		public override Params ToParams(bool includeObjectType = true)
		{
			Params kparams = base.ToParams(includeObjectType);
			if (includeObjectType)
				kparams.AddReplace("objectType", "KalturaUsageModule");
			kparams.AddIfNotNull("id", this._Id);
			kparams.AddIfNotNull("name", this._Name);
			kparams.AddIfNotNull("maxViewsNumber", this._MaxViewsNumber);
			kparams.AddIfNotNull("viewLifeCycle", this._ViewLifeCycle);
			kparams.AddIfNotNull("fullLifeCycle", this._FullLifeCycle);
			kparams.AddIfNotNull("couponId", this._CouponId);
			kparams.AddIfNotNull("waiverPeriod", this._WaiverPeriod);
			kparams.AddIfNotNull("isWaiverEnabled", this._IsWaiverEnabled);
			kparams.AddIfNotNull("isOfflinePlayback", this._IsOfflinePlayback);
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
				case MAX_VIEWS_NUMBER:
					return "MaxViewsNumber";
				case VIEW_LIFE_CYCLE:
					return "ViewLifeCycle";
				case FULL_LIFE_CYCLE:
					return "FullLifeCycle";
				case COUPON_ID:
					return "CouponId";
				case WAIVER_PERIOD:
					return "WaiverPeriod";
				case IS_WAIVER_ENABLED:
					return "IsWaiverEnabled";
				case IS_OFFLINE_PLAYBACK:
					return "IsOfflinePlayback";
				default:
					return base.getPropertyName(apiName);
			}
		}
		#endregion
	}
}

