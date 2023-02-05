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
	public class Engagement : ObjectBase
	{
		#region Constants
		public const string ID = "id";
		public const string TOTAL_NUMBER_OF_RECIPIENTS = "totalNumberOfRecipients";
		public const string TYPE = "type";
		public const string ADAPTER_ID = "adapterId";
		public const string ADAPTER_DYNAMIC_DATA = "adapterDynamicData";
		public const string INTERVAL_SECONDS = "intervalSeconds";
		public const string USER_LIST = "userList";
		public const string SEND_TIME_IN_SECONDS = "sendTimeInSeconds";
		public const string COUPON_GROUP_ID = "couponGroupId";
		#endregion

		#region Private Fields
		private int _Id = Int32.MinValue;
		private int _TotalNumberOfRecipients = Int32.MinValue;
		private EngagementType _Type = null;
		private int _AdapterId = Int32.MinValue;
		private string _AdapterDynamicData = null;
		private int _IntervalSeconds = Int32.MinValue;
		private string _UserList = null;
		private long _SendTimeInSeconds = long.MinValue;
		private int _CouponGroupId = Int32.MinValue;
		#endregion

		#region Properties
		[JsonProperty]
		public int Id
		{
			get { return _Id; }
			private set 
			{ 
				_Id = value;
				OnPropertyChanged("Id");
			}
		}
		[JsonProperty]
		public int TotalNumberOfRecipients
		{
			get { return _TotalNumberOfRecipients; }
			private set 
			{ 
				_TotalNumberOfRecipients = value;
				OnPropertyChanged("TotalNumberOfRecipients");
			}
		}
		[JsonProperty]
		public EngagementType Type
		{
			get { return _Type; }
			set 
			{ 
				_Type = value;
				OnPropertyChanged("Type");
			}
		}
		[JsonProperty]
		public int AdapterId
		{
			get { return _AdapterId; }
			set 
			{ 
				_AdapterId = value;
				OnPropertyChanged("AdapterId");
			}
		}
		[JsonProperty]
		public string AdapterDynamicData
		{
			get { return _AdapterDynamicData; }
			set 
			{ 
				_AdapterDynamicData = value;
				OnPropertyChanged("AdapterDynamicData");
			}
		}
		[JsonProperty]
		public int IntervalSeconds
		{
			get { return _IntervalSeconds; }
			set 
			{ 
				_IntervalSeconds = value;
				OnPropertyChanged("IntervalSeconds");
			}
		}
		[JsonProperty]
		public string UserList
		{
			get { return _UserList; }
			set 
			{ 
				_UserList = value;
				OnPropertyChanged("UserList");
			}
		}
		[JsonProperty]
		public long SendTimeInSeconds
		{
			get { return _SendTimeInSeconds; }
			set 
			{ 
				_SendTimeInSeconds = value;
				OnPropertyChanged("SendTimeInSeconds");
			}
		}
		[JsonProperty]
		public int CouponGroupId
		{
			get { return _CouponGroupId; }
			set 
			{ 
				_CouponGroupId = value;
				OnPropertyChanged("CouponGroupId");
			}
		}
		#endregion

		#region CTor
		public Engagement()
		{
		}

		public Engagement(JToken node) : base(node)
		{
			if(node["id"] != null)
			{
				this._Id = ParseInt(node["id"].Value<string>());
			}
			if(node["totalNumberOfRecipients"] != null)
			{
				this._TotalNumberOfRecipients = ParseInt(node["totalNumberOfRecipients"].Value<string>());
			}
			if(node["type"] != null)
			{
				this._Type = (EngagementType)StringEnum.Parse(typeof(EngagementType), node["type"].Value<string>());
			}
			if(node["adapterId"] != null)
			{
				this._AdapterId = ParseInt(node["adapterId"].Value<string>());
			}
			if(node["adapterDynamicData"] != null)
			{
				this._AdapterDynamicData = node["adapterDynamicData"].Value<string>();
			}
			if(node["intervalSeconds"] != null)
			{
				this._IntervalSeconds = ParseInt(node["intervalSeconds"].Value<string>());
			}
			if(node["userList"] != null)
			{
				this._UserList = node["userList"].Value<string>();
			}
			if(node["sendTimeInSeconds"] != null)
			{
				this._SendTimeInSeconds = ParseLong(node["sendTimeInSeconds"].Value<string>());
			}
			if(node["couponGroupId"] != null)
			{
				this._CouponGroupId = ParseInt(node["couponGroupId"].Value<string>());
			}
		}
		#endregion

		#region Methods
		public override Params ToParams(bool includeObjectType = true)
		{
			Params kparams = base.ToParams(includeObjectType);
			if (includeObjectType)
				kparams.AddReplace("objectType", "KalturaEngagement");
			kparams.AddIfNotNull("id", this._Id);
			kparams.AddIfNotNull("totalNumberOfRecipients", this._TotalNumberOfRecipients);
			kparams.AddIfNotNull("type", this._Type);
			kparams.AddIfNotNull("adapterId", this._AdapterId);
			kparams.AddIfNotNull("adapterDynamicData", this._AdapterDynamicData);
			kparams.AddIfNotNull("intervalSeconds", this._IntervalSeconds);
			kparams.AddIfNotNull("userList", this._UserList);
			kparams.AddIfNotNull("sendTimeInSeconds", this._SendTimeInSeconds);
			kparams.AddIfNotNull("couponGroupId", this._CouponGroupId);
			return kparams;
		}
		protected override string getPropertyName(string apiName)
		{
			switch(apiName)
			{
				case ID:
					return "Id";
				case TOTAL_NUMBER_OF_RECIPIENTS:
					return "TotalNumberOfRecipients";
				case TYPE:
					return "Type";
				case ADAPTER_ID:
					return "AdapterId";
				case ADAPTER_DYNAMIC_DATA:
					return "AdapterDynamicData";
				case INTERVAL_SECONDS:
					return "IntervalSeconds";
				case USER_LIST:
					return "UserList";
				case SEND_TIME_IN_SECONDS:
					return "SendTimeInSeconds";
				case COUPON_GROUP_ID:
					return "CouponGroupId";
				default:
					return base.getPropertyName(apiName);
			}
		}
		#endregion
	}
}

