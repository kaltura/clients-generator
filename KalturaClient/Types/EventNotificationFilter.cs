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
// Copyright (C) 2006-2019  Kaltura Inc.
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
	public class EventNotificationFilter : CrudFilter
	{
		#region Constants
		public const string ID_EQUAL = "idEqual";
		public const string OBJECT_ID_EQUAL = "objectIdEqual";
		public const string EVENT_OBJECT_TYPE_EQUAL = "eventObjectTypeEqual";
		public new const string ORDER_BY = "orderBy";
		#endregion

		#region Private Fields
		private string _IdEqual = null;
		private long _ObjectIdEqual = long.MinValue;
		private string _EventObjectTypeEqual = null;
		private EventNotificationOrderBy _OrderBy = null;
		#endregion

		#region Properties
		[JsonProperty]
		public string IdEqual
		{
			get { return _IdEqual; }
			set 
			{ 
				_IdEqual = value;
				OnPropertyChanged("IdEqual");
			}
		}
		[JsonProperty]
		public long ObjectIdEqual
		{
			get { return _ObjectIdEqual; }
			set 
			{ 
				_ObjectIdEqual = value;
				OnPropertyChanged("ObjectIdEqual");
			}
		}
		[JsonProperty]
		public string EventObjectTypeEqual
		{
			get { return _EventObjectTypeEqual; }
			set 
			{ 
				_EventObjectTypeEqual = value;
				OnPropertyChanged("EventObjectTypeEqual");
			}
		}
		[JsonProperty]
		public new EventNotificationOrderBy OrderBy
		{
			get { return _OrderBy; }
			set 
			{ 
				_OrderBy = value;
				OnPropertyChanged("OrderBy");
			}
		}
		#endregion

		#region CTor
		public EventNotificationFilter()
		{
		}

		public EventNotificationFilter(JToken node) : base(node)
		{
			if(node["idEqual"] != null)
			{
				this._IdEqual = node["idEqual"].Value<string>();
			}
			if(node["objectIdEqual"] != null)
			{
				this._ObjectIdEqual = ParseLong(node["objectIdEqual"].Value<string>());
			}
			if(node["eventObjectTypeEqual"] != null)
			{
				this._EventObjectTypeEqual = node["eventObjectTypeEqual"].Value<string>();
			}
			if(node["orderBy"] != null)
			{
				this._OrderBy = (EventNotificationOrderBy)StringEnum.Parse(typeof(EventNotificationOrderBy), node["orderBy"].Value<string>());
			}
		}
		#endregion

		#region Methods
		public override Params ToParams(bool includeObjectType = true)
		{
			Params kparams = base.ToParams(includeObjectType);
			if (includeObjectType)
				kparams.AddReplace("objectType", "KalturaEventNotificationFilter");
			kparams.AddIfNotNull("idEqual", this._IdEqual);
			kparams.AddIfNotNull("objectIdEqual", this._ObjectIdEqual);
			kparams.AddIfNotNull("eventObjectTypeEqual", this._EventObjectTypeEqual);
			kparams.AddIfNotNull("orderBy", this._OrderBy);
			return kparams;
		}
		protected override string getPropertyName(string apiName)
		{
			switch(apiName)
			{
				case ID_EQUAL:
					return "IdEqual";
				case OBJECT_ID_EQUAL:
					return "ObjectIdEqual";
				case EVENT_OBJECT_TYPE_EQUAL:
					return "EventObjectTypeEqual";
				case ORDER_BY:
					return "OrderBy";
				default:
					return base.getPropertyName(apiName);
			}
		}
		#endregion
	}
}

