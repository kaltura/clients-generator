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
	public class CDVRAdapterProfile : ObjectBase
	{
		#region Constants
		public const string ID = "id";
		public const string NAME = "name";
		public const string IS_ACTIVE = "isActive";
		public const string ADAPTER_URL = "adapterUrl";
		public const string SETTINGS = "settings";
		public const string EXTERNAL_IDENTIFIER = "externalIdentifier";
		public const string SHARED_SECRET = "sharedSecret";
		public const string DYNAMIC_LINKS_SUPPORT = "dynamicLinksSupport";
		#endregion

		#region Private Fields
		private int _Id = Int32.MinValue;
		private string _Name = null;
		private bool? _IsActive = null;
		private string _AdapterUrl = null;
		private IDictionary<string, StringValue> _Settings;
		private string _ExternalIdentifier = null;
		private string _SharedSecret = null;
		private bool? _DynamicLinksSupport = null;
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
		public IDictionary<string, StringValue> Settings
		{
			get { return _Settings; }
			set 
			{ 
				_Settings = value;
				OnPropertyChanged("Settings");
			}
		}
		public string ExternalIdentifier
		{
			get { return _ExternalIdentifier; }
			set 
			{ 
				_ExternalIdentifier = value;
				OnPropertyChanged("ExternalIdentifier");
			}
		}
		public string SharedSecret
		{
			get { return _SharedSecret; }
		}
		public bool? DynamicLinksSupport
		{
			get { return _DynamicLinksSupport; }
			set 
			{ 
				_DynamicLinksSupport = value;
				OnPropertyChanged("DynamicLinksSupport");
			}
		}
		#endregion

		#region CTor
		public CDVRAdapterProfile()
		{
		}

		public CDVRAdapterProfile(XmlElement node) : base(node)
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
						{
							string key;
							this._Settings = new Dictionary<string, StringValue>();
							foreach(XmlElement arrayNode in propertyNode.ChildNodes)
							{
								key = arrayNode["itemKey"].InnerText;;
								this._Settings[key] = ObjectFactory.Create<StringValue>(arrayNode);
							}
						}
						continue;
					case "externalIdentifier":
						this._ExternalIdentifier = propertyNode.InnerText;
						continue;
					case "sharedSecret":
						this._SharedSecret = propertyNode.InnerText;
						continue;
					case "dynamicLinksSupport":
						this._DynamicLinksSupport = ParseBool(propertyNode.InnerText);
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
				kparams.AddReplace("objectType", "KalturaCDVRAdapterProfile");
			kparams.AddIfNotNull("id", this._Id);
			kparams.AddIfNotNull("name", this._Name);
			kparams.AddIfNotNull("isActive", this._IsActive);
			kparams.AddIfNotNull("adapterUrl", this._AdapterUrl);
			kparams.AddIfNotNull("settings", this._Settings);
			kparams.AddIfNotNull("externalIdentifier", this._ExternalIdentifier);
			kparams.AddIfNotNull("sharedSecret", this._SharedSecret);
			kparams.AddIfNotNull("dynamicLinksSupport", this._DynamicLinksSupport);
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
				case EXTERNAL_IDENTIFIER:
					return "ExternalIdentifier";
				case SHARED_SECRET:
					return "SharedSecret";
				case DYNAMIC_LINKS_SUPPORT:
					return "DynamicLinksSupport";
				default:
					return base.getPropertyName(apiName);
			}
		}
		#endregion
	}
}

