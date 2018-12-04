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
	public class DrmProfile : ObjectBase
	{
		#region Constants
		public const string ID = "id";
		public const string NAME = "name";
		public const string IS_ACTIVE = "isActive";
		public const string ADAPTER_URL = "adapterUrl";
		public const string SETTINGS = "settings";
		public const string SYSTEM_NAME = "systemName";
		public const string SHARED_SECRET = "sharedSecret";
		#endregion

		#region Private Fields
		private int _Id = Int32.MinValue;
		private string _Name = null;
		private bool? _IsActive = null;
		private string _AdapterUrl = null;
		private string _Settings = null;
		private string _SystemName = null;
		private string _SharedSecret = null;
		#endregion

		#region Properties
		public int Id
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
		public bool? IsActive
		{
			get { return _IsActive; }
			set 
			{ 
				_IsActive = value;
				OnPropertyChanged("IsActive");
			}
		}
		public string AdapterUrl
		{
			get { return _AdapterUrl; }
			set 
			{ 
				_AdapterUrl = value;
				OnPropertyChanged("AdapterUrl");
			}
		}
		public string Settings
		{
			get { return _Settings; }
			set 
			{ 
				_Settings = value;
				OnPropertyChanged("Settings");
			}
		}
		public string SystemName
		{
			get { return _SystemName; }
			set 
			{ 
				_SystemName = value;
				OnPropertyChanged("SystemName");
			}
		}
		public string SharedSecret
		{
			get { return _SharedSecret; }
		}
		#endregion

		#region CTor
		public DrmProfile()
		{
		}

		public DrmProfile(XmlElement node) : base(node)
		{
			foreach (XmlElement propertyNode in node.ChildNodes)
			{
				switch (propertyNode.Name)
				{
					case "id":
						this._Id = ParseInt(propertyNode.InnerText);
						continue;
					case "name":
						this._Name = propertyNode.InnerText;
						continue;
					case "isActive":
						this._IsActive = ParseBool(propertyNode.InnerText);
						continue;
					case "adapterUrl":
						this._AdapterUrl = propertyNode.InnerText;
						continue;
					case "settings":
						this._Settings = propertyNode.InnerText;
						continue;
					case "systemName":
						this._SystemName = propertyNode.InnerText;
						continue;
					case "sharedSecret":
						this._SharedSecret = propertyNode.InnerText;
						continue;
				}
			}
		}

		public DrmProfile(IDictionary<string,object> data) : base(data)
		{
			    this._Id = data.TryGetValueSafe<int>("id");
			    this._Name = data.TryGetValueSafe<string>("name");
			    this._IsActive = data.TryGetValueSafe<bool>("isActive");
			    this._AdapterUrl = data.TryGetValueSafe<string>("adapterUrl");
			    this._Settings = data.TryGetValueSafe<string>("settings");
			    this._SystemName = data.TryGetValueSafe<string>("systemName");
			    this._SharedSecret = data.TryGetValueSafe<string>("sharedSecret");
		}
		#endregion

		#region Methods
		public override Params ToParams(bool includeObjectType = true)
		{
			Params kparams = base.ToParams(includeObjectType);
			if (includeObjectType)
				kparams.AddReplace("objectType", "KalturaDrmProfile");
			kparams.AddIfNotNull("id", this._Id);
			kparams.AddIfNotNull("name", this._Name);
			kparams.AddIfNotNull("isActive", this._IsActive);
			kparams.AddIfNotNull("adapterUrl", this._AdapterUrl);
			kparams.AddIfNotNull("settings", this._Settings);
			kparams.AddIfNotNull("systemName", this._SystemName);
			kparams.AddIfNotNull("sharedSecret", this._SharedSecret);
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
				case IS_ACTIVE:
					return "IsActive";
				case ADAPTER_URL:
					return "AdapterUrl";
				case SETTINGS:
					return "Settings";
				case SYSTEM_NAME:
					return "SystemName";
				case SHARED_SECRET:
					return "SharedSecret";
				default:
					return base.getPropertyName(apiName);
			}
		}
		#endregion
	}
}

