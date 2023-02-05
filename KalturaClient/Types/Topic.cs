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
	public class Topic : ObjectBase
	{
		#region Constants
		public const string ID = "id";
		public const string NAME = "name";
		public const string SUBSCRIBERS_AMOUNT = "subscribersAmount";
		public const string AUTOMATIC_ISSUE_NOTIFICATION = "automaticIssueNotification";
		public const string LAST_MESSAGE_SENT_DATE_SEC = "lastMessageSentDateSec";
		#endregion

		#region Private Fields
		private string _Id = null;
		private string _Name = null;
		private string _SubscribersAmount = null;
		private TopicAutomaticIssueNotification _AutomaticIssueNotification = null;
		private long _LastMessageSentDateSec = long.MinValue;
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
		public string SubscribersAmount
		{
			get { return _SubscribersAmount; }
			set 
			{ 
				_SubscribersAmount = value;
				OnPropertyChanged("SubscribersAmount");
			}
		}
		[JsonProperty]
		public TopicAutomaticIssueNotification AutomaticIssueNotification
		{
			get { return _AutomaticIssueNotification; }
			set 
			{ 
				_AutomaticIssueNotification = value;
				OnPropertyChanged("AutomaticIssueNotification");
			}
		}
		[JsonProperty]
		public long LastMessageSentDateSec
		{
			get { return _LastMessageSentDateSec; }
			set 
			{ 
				_LastMessageSentDateSec = value;
				OnPropertyChanged("LastMessageSentDateSec");
			}
		}
		#endregion

		#region CTor
		public Topic()
		{
		}

		public Topic(JToken node) : base(node)
		{
			if(node["id"] != null)
			{
				this._Id = node["id"].Value<string>();
			}
			if(node["name"] != null)
			{
				this._Name = node["name"].Value<string>();
			}
			if(node["subscribersAmount"] != null)
			{
				this._SubscribersAmount = node["subscribersAmount"].Value<string>();
			}
			if(node["automaticIssueNotification"] != null)
			{
				this._AutomaticIssueNotification = (TopicAutomaticIssueNotification)StringEnum.Parse(typeof(TopicAutomaticIssueNotification), node["automaticIssueNotification"].Value<string>());
			}
			if(node["lastMessageSentDateSec"] != null)
			{
				this._LastMessageSentDateSec = ParseLong(node["lastMessageSentDateSec"].Value<string>());
			}
		}
		#endregion

		#region Methods
		public override Params ToParams(bool includeObjectType = true)
		{
			Params kparams = base.ToParams(includeObjectType);
			if (includeObjectType)
				kparams.AddReplace("objectType", "KalturaTopic");
			kparams.AddIfNotNull("id", this._Id);
			kparams.AddIfNotNull("name", this._Name);
			kparams.AddIfNotNull("subscribersAmount", this._SubscribersAmount);
			kparams.AddIfNotNull("automaticIssueNotification", this._AutomaticIssueNotification);
			kparams.AddIfNotNull("lastMessageSentDateSec", this._LastMessageSentDateSec);
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
				case SUBSCRIBERS_AMOUNT:
					return "SubscribersAmount";
				case AUTOMATIC_ISSUE_NOTIFICATION:
					return "AutomaticIssueNotification";
				case LAST_MESSAGE_SENT_DATE_SEC:
					return "LastMessageSentDateSec";
				default:
					return base.getPropertyName(apiName);
			}
		}
		#endregion
	}
}

