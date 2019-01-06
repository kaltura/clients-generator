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
		public int Id
		{
			get { return _Id; }
		}
		public int TotalNumberOfRecipients
		{
			get { return _TotalNumberOfRecipients; }
		}
		public EngagementType Type
		{
			get { return _Type; }
			set 
			{ 
				_Type = value;
				OnPropertyChanged("Type");
			}
		}
		public int AdapterId
		{
			get { return _AdapterId; }
			set 
			{ 
				_AdapterId = value;
				OnPropertyChanged("AdapterId");
			}
		}
		public string AdapterDynamicData
		{
			get { return _AdapterDynamicData; }
			set 
			{ 
				_AdapterDynamicData = value;
				OnPropertyChanged("AdapterDynamicData");
			}
		}
		public int IntervalSeconds
		{
			get { return _IntervalSeconds; }
			set 
			{ 
				_IntervalSeconds = value;
				OnPropertyChanged("IntervalSeconds");
			}
		}
		public string UserList
		{
			get { return _UserList; }
			set 
			{ 
				_UserList = value;
				OnPropertyChanged("UserList");
			}
		}
		public long SendTimeInSeconds
		{
			get { return _SendTimeInSeconds; }
			set 
			{ 
				_SendTimeInSeconds = value;
				OnPropertyChanged("SendTimeInSeconds");
			}
		}
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

		public Engagement(XmlElement node) : base(node)
		{
			foreach (XmlElement propertyNode in node.ChildNodes)
			{
				switch (propertyNode.Name)
				{
					case "id":
						this._Id = ParseInt(propertyNode.InnerText);
						continue;
					case "totalNumberOfRecipients":
						this._TotalNumberOfRecipients = ParseInt(propertyNode.InnerText);
						continue;
					case "type":
						this._Type = (EngagementType)StringEnum.Parse(typeof(EngagementType), propertyNode.InnerText);
						continue;
					case "adapterId":
						this._AdapterId = ParseInt(propertyNode.InnerText);
						continue;
					case "adapterDynamicData":
						this._AdapterDynamicData = propertyNode.InnerText;
						continue;
					case "intervalSeconds":
						this._IntervalSeconds = ParseInt(propertyNode.InnerText);
						continue;
					case "userList":
						this._UserList = propertyNode.InnerText;
						continue;
					case "sendTimeInSeconds":
						this._SendTimeInSeconds = ParseLong(propertyNode.InnerText);
						continue;
					case "couponGroupId":
						this._CouponGroupId = ParseInt(propertyNode.InnerText);
						continue;
				}
			}
		}

		public Engagement(IDictionary<string,object> data) : base(data)
		{
			    this._Id = data.TryGetValueSafe<int>("id");
			    this._TotalNumberOfRecipients = data.TryGetValueSafe<int>("totalNumberOfRecipients");
			    this._Type = (EngagementType)StringEnum.Parse(typeof(EngagementType), data.TryGetValueSafe<string>("type"));
			    this._AdapterId = data.TryGetValueSafe<int>("adapterId");
			    this._AdapterDynamicData = data.TryGetValueSafe<string>("adapterDynamicData");
			    this._IntervalSeconds = data.TryGetValueSafe<int>("intervalSeconds");
			    this._UserList = data.TryGetValueSafe<string>("userList");
			    this._SendTimeInSeconds = data.TryGetValueSafe<long>("sendTimeInSeconds");
			    this._CouponGroupId = data.TryGetValueSafe<int>("couponGroupId");
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

