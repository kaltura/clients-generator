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
	public class SearchHistory : ObjectBase
	{
		#region Constants
		public const string ID = "id";
		public const string NAME = "name";
		public const string FILTER = "filter";
		public const string LANGUAGE = "language";
		public const string CREATED_AT = "createdAt";
		public const string SERVICE = "service";
		public const string ACTION = "action";
		public const string DEVICE_ID = "deviceId";
		#endregion

		#region Private Fields
		private string _Id = null;
		private string _Name = null;
		private string _Filter = null;
		private string _Language = null;
		private long _CreatedAt = long.MinValue;
		private string _Service = null;
		private string _Action = null;
		private string _DeviceId = null;
		#endregion

		#region Properties
		public string Id
		{
			get { return _Id; }
		}
		public string Name
		{
			get { return _Name; }
		}
		public string Filter
		{
			get { return _Filter; }
		}
		public string Language
		{
			get { return _Language; }
		}
		public long CreatedAt
		{
			get { return _CreatedAt; }
		}
		public string Service
		{
			get { return _Service; }
		}
		public string Action
		{
			get { return _Action; }
		}
		public string DeviceId
		{
			get { return _DeviceId; }
		}
		#endregion

		#region CTor
		public SearchHistory()
		{
		}

		public SearchHistory(XmlElement node) : base(node)
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
					case "filter":
						this._Filter = propertyNode.InnerText;
						continue;
					case "language":
						this._Language = propertyNode.InnerText;
						continue;
					case "createdAt":
						this._CreatedAt = ParseLong(propertyNode.InnerText);
						continue;
					case "service":
						this._Service = propertyNode.InnerText;
						continue;
					case "action":
						this._Action = propertyNode.InnerText;
						continue;
					case "deviceId":
						this._DeviceId = propertyNode.InnerText;
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
				kparams.AddReplace("objectType", "KalturaSearchHistory");
			kparams.AddIfNotNull("id", this._Id);
			kparams.AddIfNotNull("name", this._Name);
			kparams.AddIfNotNull("filter", this._Filter);
			kparams.AddIfNotNull("language", this._Language);
			kparams.AddIfNotNull("createdAt", this._CreatedAt);
			kparams.AddIfNotNull("service", this._Service);
			kparams.AddIfNotNull("action", this._Action);
			kparams.AddIfNotNull("deviceId", this._DeviceId);
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
				case FILTER:
					return "Filter";
				case LANGUAGE:
					return "Language";
				case CREATED_AT:
					return "CreatedAt";
				case SERVICE:
					return "Service";
				case ACTION:
					return "Action";
				case DEVICE_ID:
					return "DeviceId";
				default:
					return base.getPropertyName(apiName);
			}
		}
		#endregion
	}
}

