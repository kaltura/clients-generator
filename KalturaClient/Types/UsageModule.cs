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
		public long Id
		{
			get { return _Id; }
		}
		public string Name
		{
			get { return _Name; }
		}
		public int MaxViewsNumber
		{
			get { return _MaxViewsNumber; }
		}
		public int ViewLifeCycle
		{
			get { return _ViewLifeCycle; }
		}
		public int FullLifeCycle
		{
			get { return _FullLifeCycle; }
		}
		public int CouponId
		{
			get { return _CouponId; }
		}
		public int WaiverPeriod
		{
			get { return _WaiverPeriod; }
		}
		public bool? IsWaiverEnabled
		{
			get { return _IsWaiverEnabled; }
		}
		public bool? IsOfflinePlayback
		{
			get { return _IsOfflinePlayback; }
		}
		#endregion

		#region CTor
		public UsageModule()
		{
		}

		public UsageModule(XmlElement node) : base(node)
		{
			foreach (XmlElement propertyNode in node.ChildNodes)
			{
				switch (propertyNode.Name)
				{
					case "id":
						this._Id = ParseLong(propertyNode.InnerText);
						continue;
					case "name":
						this._Name = propertyNode.InnerText;
						continue;
					case "maxViewsNumber":
						this._MaxViewsNumber = ParseInt(propertyNode.InnerText);
						continue;
					case "viewLifeCycle":
						this._ViewLifeCycle = ParseInt(propertyNode.InnerText);
						continue;
					case "fullLifeCycle":
						this._FullLifeCycle = ParseInt(propertyNode.InnerText);
						continue;
					case "couponId":
						this._CouponId = ParseInt(propertyNode.InnerText);
						continue;
					case "waiverPeriod":
						this._WaiverPeriod = ParseInt(propertyNode.InnerText);
						continue;
					case "isWaiverEnabled":
						this._IsWaiverEnabled = ParseBool(propertyNode.InnerText);
						continue;
					case "isOfflinePlayback":
						this._IsOfflinePlayback = ParseBool(propertyNode.InnerText);
						continue;
				}
			}
		}

		public UsageModule(IDictionary<string,object> data) : base(data)
		{
			    this._Id = data.TryGetValueSafe<long>("id");
			    this._Name = data.TryGetValueSafe<string>("name");
			    this._MaxViewsNumber = data.TryGetValueSafe<int>("maxViewsNumber");
			    this._ViewLifeCycle = data.TryGetValueSafe<int>("viewLifeCycle");
			    this._FullLifeCycle = data.TryGetValueSafe<int>("fullLifeCycle");
			    this._CouponId = data.TryGetValueSafe<int>("couponId");
			    this._WaiverPeriod = data.TryGetValueSafe<int>("waiverPeriod");
			    this._IsWaiverEnabled = data.TryGetValueSafe<bool>("isWaiverEnabled");
			    this._IsOfflinePlayback = data.TryGetValueSafe<bool>("isOfflinePlayback");
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

