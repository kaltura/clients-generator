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
// Copyright (C) 2006-2017  Kaltura Inc.
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
		public string Id
		{
			get { return _Id; }
		}
		public string Name
		{
			get { return _Name; }
			set 
			{ 
				_Name = value;
				OnPropertyChanged("Name");
			}
		}
		public string SubscribersAmount
		{
			get { return _SubscribersAmount; }
			set 
			{ 
				_SubscribersAmount = value;
				OnPropertyChanged("SubscribersAmount");
			}
		}
		public TopicAutomaticIssueNotification AutomaticIssueNotification
		{
			get { return _AutomaticIssueNotification; }
			set 
			{ 
				_AutomaticIssueNotification = value;
				OnPropertyChanged("AutomaticIssueNotification");
			}
		}
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

		public Topic(XmlElement node) : base(node)
		{
			foreach (XmlElement propertyNode in node.ChildNodes)
			{
				switch (propertyNode.Name)
				{
					case "id":
						this._Id = propertyNode.InnerText;
						continue;
					case "name":
						this._Name = propertyNode.InnerText;
						continue;
					case "subscribersAmount":
						this._SubscribersAmount = propertyNode.InnerText;
						continue;
					case "automaticIssueNotification":
						this._AutomaticIssueNotification = (TopicAutomaticIssueNotification)StringEnum.Parse(typeof(TopicAutomaticIssueNotification), propertyNode.InnerText);
						continue;
					case "lastMessageSentDateSec":
						this._LastMessageSentDateSec = ParseLong(propertyNode.InnerText);
						continue;
				}
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

