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
	public class TopicNotificationMessage : ObjectBase
	{
		#region Constants
		public const string ID = "id";
		public const string MESSAGE = "message";
		public const string IMAGE_URL = "imageUrl";
		public const string TOPIC_NOTIFICATION_ID = "topicNotificationId";
		public const string TRIGGER = "trigger";
		public const string DISPATCHERS = "dispatchers";
		public const string STATUS = "status";
		#endregion

		#region Private Fields
		private long _Id = long.MinValue;
		private string _Message = null;
		private string _ImageUrl = null;
		private long _TopicNotificationId = long.MinValue;
		private Trigger _Trigger;
		private IList<Dispatcher> _Dispatchers;
		private AnnouncementStatus _Status = null;
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
		public string ImageUrl
		{
			get { return _ImageUrl; }
			set 
			{ 
				_ImageUrl = value;
				OnPropertyChanged("ImageUrl");
			}
		}
		[JsonProperty]
		public long TopicNotificationId
		{
			get { return _TopicNotificationId; }
			set 
			{ 
				_TopicNotificationId = value;
				OnPropertyChanged("TopicNotificationId");
			}
		}
		[JsonProperty]
		public Trigger Trigger
		{
			get { return _Trigger; }
			set 
			{ 
				_Trigger = value;
				OnPropertyChanged("Trigger");
			}
		}
		[JsonProperty]
		public IList<Dispatcher> Dispatchers
		{
			get { return _Dispatchers; }
			set 
			{ 
				_Dispatchers = value;
				OnPropertyChanged("Dispatchers");
			}
		}
		[JsonProperty]
		public AnnouncementStatus Status
		{
			get { return _Status; }
			private set 
			{ 
				_Status = value;
				OnPropertyChanged("Status");
			}
		}
		#endregion

		#region CTor
		public TopicNotificationMessage()
		{
		}

		public TopicNotificationMessage(JToken node) : base(node)
		{
			if(node["id"] != null)
			{
				this._Id = ParseLong(node["id"].Value<string>());
			}
			if(node["message"] != null)
			{
				this._Message = node["message"].Value<string>();
			}
			if(node["imageUrl"] != null)
			{
				this._ImageUrl = node["imageUrl"].Value<string>();
			}
			if(node["topicNotificationId"] != null)
			{
				this._TopicNotificationId = ParseLong(node["topicNotificationId"].Value<string>());
			}
			if(node["trigger"] != null)
			{
				this._Trigger = ObjectFactory.Create<Trigger>(node["trigger"]);
			}
			if(node["dispatchers"] != null)
			{
				this._Dispatchers = new List<Dispatcher>();
				foreach(var arrayNode in node["dispatchers"].Children())
				{
					this._Dispatchers.Add(ObjectFactory.Create<Dispatcher>(arrayNode));
				}
			}
			if(node["status"] != null)
			{
				this._Status = (AnnouncementStatus)StringEnum.Parse(typeof(AnnouncementStatus), node["status"].Value<string>());
			}
		}
		#endregion

		#region Methods
		public override Params ToParams(bool includeObjectType = true)
		{
			Params kparams = base.ToParams(includeObjectType);
			if (includeObjectType)
				kparams.AddReplace("objectType", "KalturaTopicNotificationMessage");
			kparams.AddIfNotNull("id", this._Id);
			kparams.AddIfNotNull("message", this._Message);
			kparams.AddIfNotNull("imageUrl", this._ImageUrl);
			kparams.AddIfNotNull("topicNotificationId", this._TopicNotificationId);
			kparams.AddIfNotNull("trigger", this._Trigger);
			kparams.AddIfNotNull("dispatchers", this._Dispatchers);
			kparams.AddIfNotNull("status", this._Status);
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
				case IMAGE_URL:
					return "ImageUrl";
				case TOPIC_NOTIFICATION_ID:
					return "TopicNotificationId";
				case TRIGGER:
					return "Trigger";
				case DISPATCHERS:
					return "Dispatchers";
				case STATUS:
					return "Status";
				default:
					return base.getPropertyName(apiName);
			}
		}
		#endregion
	}
}

