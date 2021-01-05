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
	public class EventNotification : CrudObject
	{
		#region Constants
		public const string ID = "id";
		public const string OBJECT_ID = "objectId";
		public const string EVENT_OBJECT_TYPE = "eventObjectType";
		public const string MESSAGE = "message";
		public const string STATUS = "status";
		public const string ACTION_TYPE = "actionType";
		public const string CREATE_DATE = "createDate";
		public const string UPDATE_DATE = "updateDate";
		#endregion

		#region Private Fields
		private string _Id = null;
		private long _ObjectId = long.MinValue;
		private string _EventObjectType = null;
		private string _Message = null;
		private EventNotificationStatus _Status = null;
		private string _ActionType = null;
		private long _CreateDate = long.MinValue;
		private long _UpdateDate = long.MinValue;
		#endregion

		#region Properties
		[JsonProperty]
		public string Id
		{
			get { return _Id; }
			set 
			{ 
				_Id = value;
				OnPropertyChanged("Id");
			}
		}
		[JsonProperty]
		public long ObjectId
		{
			get { return _ObjectId; }
			set 
			{ 
				_ObjectId = value;
				OnPropertyChanged("ObjectId");
			}
		}
		[JsonProperty]
		public string EventObjectType
		{
			get { return _EventObjectType; }
			set 
			{ 
				_EventObjectType = value;
				OnPropertyChanged("EventObjectType");
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
		public EventNotificationStatus Status
		{
			get { return _Status; }
			set 
			{ 
				_Status = value;
				OnPropertyChanged("Status");
			}
		}
		[JsonProperty]
		public string ActionType
		{
			get { return _ActionType; }
			set 
			{ 
				_ActionType = value;
				OnPropertyChanged("ActionType");
			}
		}
		[JsonProperty]
		public long CreateDate
		{
			get { return _CreateDate; }
			private set 
			{ 
				_CreateDate = value;
				OnPropertyChanged("CreateDate");
			}
		}
		[JsonProperty]
		public long UpdateDate
		{
			get { return _UpdateDate; }
			private set 
			{ 
				_UpdateDate = value;
				OnPropertyChanged("UpdateDate");
			}
		}
		#endregion

		#region CTor
		public EventNotification()
		{
		}

		public EventNotification(JToken node) : base(node)
		{
			if(node["id"] != null)
			{
				this._Id = node["id"].Value<string>();
			}
			if(node["objectId"] != null)
			{
				this._ObjectId = ParseLong(node["objectId"].Value<string>());
			}
			if(node["eventObjectType"] != null)
			{
				this._EventObjectType = node["eventObjectType"].Value<string>();
			}
			if(node["message"] != null)
			{
				this._Message = node["message"].Value<string>();
			}
			if(node["status"] != null)
			{
				this._Status = (EventNotificationStatus)StringEnum.Parse(typeof(EventNotificationStatus), node["status"].Value<string>());
			}
			if(node["actionType"] != null)
			{
				this._ActionType = node["actionType"].Value<string>();
			}
			if(node["createDate"] != null)
			{
				this._CreateDate = ParseLong(node["createDate"].Value<string>());
			}
			if(node["updateDate"] != null)
			{
				this._UpdateDate = ParseLong(node["updateDate"].Value<string>());
			}
		}
		#endregion

		#region Methods
		public override Params ToParams(bool includeObjectType = true)
		{
			Params kparams = base.ToParams(includeObjectType);
			if (includeObjectType)
				kparams.AddReplace("objectType", "KalturaEventNotification");
			kparams.AddIfNotNull("id", this._Id);
			kparams.AddIfNotNull("objectId", this._ObjectId);
			kparams.AddIfNotNull("eventObjectType", this._EventObjectType);
			kparams.AddIfNotNull("message", this._Message);
			kparams.AddIfNotNull("status", this._Status);
			kparams.AddIfNotNull("actionType", this._ActionType);
			kparams.AddIfNotNull("createDate", this._CreateDate);
			kparams.AddIfNotNull("updateDate", this._UpdateDate);
			return kparams;
		}
		protected override string getPropertyName(string apiName)
		{
			switch(apiName)
			{
				case ID:
					return "Id";
				case OBJECT_ID:
					return "ObjectId";
				case EVENT_OBJECT_TYPE:
					return "EventObjectType";
				case MESSAGE:
					return "Message";
				case STATUS:
					return "Status";
				case ACTION_TYPE:
					return "ActionType";
				case CREATE_DATE:
					return "CreateDate";
				case UPDATE_DATE:
					return "UpdateDate";
				default:
					return base.getPropertyName(apiName);
			}
		}
		#endregion
	}
}

