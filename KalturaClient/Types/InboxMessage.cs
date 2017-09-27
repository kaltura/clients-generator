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
	public class InboxMessage : ObjectBase
	{
		#region Constants
		public const string ID = "id";
		public const string MESSAGE = "message";
		public const string STATUS = "status";
		public const string TYPE = "type";
		public const string CREATED_AT = "createdAt";
		public const string URL = "url";
		#endregion

		#region Private Fields
		private string _Id = null;
		private string _Message = null;
		private InboxMessageStatus _Status = null;
		private InboxMessageType _Type = null;
		private long _CreatedAt = long.MinValue;
		private string _Url = null;
		#endregion

		#region Properties
		public string Id
		{
			get { return _Id; }
		}
		public string Message
		{
			get { return _Message; }
			set 
			{ 
				_Message = value;
				OnPropertyChanged("Message");
			}
		}
		public InboxMessageStatus Status
		{
			get { return _Status; }
		}
		public InboxMessageType Type
		{
			get { return _Type; }
			set 
			{ 
				_Type = value;
				OnPropertyChanged("Type");
			}
		}
		public long CreatedAt
		{
			get { return _CreatedAt; }
		}
		public string Url
		{
			get { return _Url; }
			set 
			{ 
				_Url = value;
				OnPropertyChanged("Url");
			}
		}
		#endregion

		#region CTor
		public InboxMessage()
		{
		}

		public InboxMessage(XmlElement node) : base(node)
		{
			foreach (XmlElement propertyNode in node.ChildNodes)
			{
				switch (propertyNode.Name)
				{
					case "id":
						this._Id = propertyNode.InnerText;
						continue;
					case "message":
						this._Message = propertyNode.InnerText;
						continue;
					case "status":
						this._Status = (InboxMessageStatus)StringEnum.Parse(typeof(InboxMessageStatus), propertyNode.InnerText);
						continue;
					case "type":
						this._Type = (InboxMessageType)StringEnum.Parse(typeof(InboxMessageType), propertyNode.InnerText);
						continue;
					case "createdAt":
						this._CreatedAt = ParseLong(propertyNode.InnerText);
						continue;
					case "url":
						this._Url = propertyNode.InnerText;
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
				kparams.AddReplace("objectType", "KalturaInboxMessage");
			kparams.AddIfNotNull("id", this._Id);
			kparams.AddIfNotNull("message", this._Message);
			kparams.AddIfNotNull("status", this._Status);
			kparams.AddIfNotNull("type", this._Type);
			kparams.AddIfNotNull("createdAt", this._CreatedAt);
			kparams.AddIfNotNull("url", this._Url);
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
				default:
					return base.getPropertyName(apiName);
			}
		}
		#endregion
	}
}

