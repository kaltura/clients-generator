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
	public class InboxMessage : ObjectBase
	{
		#region Constants
		public const string ID = "id";
		public const string MESSAGE = "message";
		public const string STATUS = "status";
		public const string TYPE = "type";
		public const string CREATED_AT = "createdAt";
		public const string URL = "url";
		public const string CAMPAIGN_ID = "campaignId";
		#endregion

		#region Private Fields
		private string _Id = null;
		private string _Message = null;
		private InboxMessageStatus _Status = null;
		private InboxMessageType _Type = null;
		private long _CreatedAt = long.MinValue;
		private string _Url = null;
		private long _CampaignId = long.MinValue;
		#endregion

		#region Properties
		[JsonProperty]
		public string Id
		{
			get { return _Id; }
			private set 
			{ 
				_Id = value;
				OnPropertyChanged("Id");
			}
		}
		[JsonProperty]
		public string Message
		{
			get { return _Message; }
			set 
			{ 
				_Message = value;
				OnPropertyChanged("Message");
			}
		}
		[JsonProperty]
		public InboxMessageStatus Status
		{
			get { return _Status; }
			private set 
			{ 
				_Status = value;
				OnPropertyChanged("Status");
			}
		}
		[JsonProperty]
		public InboxMessageType Type
		{
			get { return _Type; }
			set 
			{ 
				_Type = value;
				OnPropertyChanged("Type");
			}
		}
		[JsonProperty]
		public long CreatedAt
		{
			get { return _CreatedAt; }
			private set 
			{ 
				_CreatedAt = value;
				OnPropertyChanged("CreatedAt");
			}
		}
		[JsonProperty]
		public string Url
		{
			get { return _Url; }
			set 
			{ 
				_Url = value;
				OnPropertyChanged("Url");
			}
		}
		[JsonProperty]
		public long CampaignId
		{
			get { return _CampaignId; }
			private set 
			{ 
				_CampaignId = value;
				OnPropertyChanged("CampaignId");
			}
		}
		#endregion

		#region CTor
		public InboxMessage()
		{
		}

		public InboxMessage(JToken node) : base(node)
		{
			if(node["id"] != null)
			{
				this._Id = node["id"].Value<string>();
			}
			if(node["message"] != null)
			{
				this._Message = node["message"].Value<string>();
			}
			if(node["status"] != null)
			{
				this._Status = (InboxMessageStatus)StringEnum.Parse(typeof(InboxMessageStatus), node["status"].Value<string>());
			}
			if(node["type"] != null)
			{
				this._Type = (InboxMessageType)StringEnum.Parse(typeof(InboxMessageType), node["type"].Value<string>());
			}
			if(node["createdAt"] != null)
			{
				this._CreatedAt = ParseLong(node["createdAt"].Value<string>());
			}
			if(node["url"] != null)
			{
				this._Url = node["url"].Value<string>();
			}
			if(node["campaignId"] != null)
			{
				this._CampaignId = ParseLong(node["campaignId"].Value<string>());
			}
		}
		#endregion

		#region Methods
		public override Params ToParams(bool includeObjectType = true)
		{
			Params kparams = base.ToParams(includeObjectType);
			if (includeObjectType)
				kparams.AddReplace("objectType", "KalturaInboxMessage");
			kparams.AddIfNotNull("id", this._Id);
			kparams.AddIfNotNull("message", this._Message);
			kparams.AddIfNotNull("status", this._Status);
			kparams.AddIfNotNull("type", this._Type);
			kparams.AddIfNotNull("createdAt", this._CreatedAt);
			kparams.AddIfNotNull("url", this._Url);
			kparams.AddIfNotNull("campaignId", this._CampaignId);
			return kparams;
		}
		protected override string getPropertyName(string apiName)
		{
			switch(apiName)
			{
				case ID:
					return "Id";
				case MESSAGE:
					return "Message";
				case STATUS:
					return "Status";
				case TYPE:
					return "Type";
				case CREATED_AT:
					return "CreatedAt";
				case URL:
					return "Url";
				case CAMPAIGN_ID:
					return "CampaignId";
				default:
					return base.getPropertyName(apiName);
			}
		}
		#endregion
	}
}

