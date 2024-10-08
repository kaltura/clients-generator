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
	public class SmsAdapterProfile : CrudObject
	{
		#region Constants
		public const string ID = "id";
		public const string ADAPTER_URL = "adapterUrl";
		public const string SHARED_SECRET = "sharedSecret";
		public const string IS_ACTIVE = "isActive";
		public const string SETTINGS = "settings";
		public const string EXTERNAL_IDENTIFIER = "externalIdentifier";
		public const string NAME = "name";
		#endregion

		#region Private Fields
		private long _Id = long.MinValue;
		private string _AdapterUrl = null;
		private string _SharedSecret = null;
		private int _IsActive = Int32.MinValue;
		private IDictionary<string, StringValue> _Settings;
		private string _ExternalIdentifier = null;
		private string _Name = null;
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
		public string AdapterUrl
		{
			get { return _AdapterUrl; }
			set 
			{ 
				_AdapterUrl = value;
				OnPropertyChanged("AdapterUrl");
			}
		}
		[JsonProperty]
		public string SharedSecret
		{
			get { return _SharedSecret; }
			set 
			{ 
				_SharedSecret = value;
				OnPropertyChanged("SharedSecret");
			}
		}
		[JsonProperty]
		public int IsActive
		{
			get { return _IsActive; }
			set 
			{ 
				_IsActive = value;
				OnPropertyChanged("IsActive");
			}
		}
		[JsonProperty]
		public IDictionary<string, StringValue> Settings
		{
			get { return _Settings; }
			set 
			{ 
				_Settings = value;
				OnPropertyChanged("Settings");
			}
		}
		[JsonProperty]
		public string ExternalIdentifier
		{
			get { return _ExternalIdentifier; }
			set 
			{ 
				_ExternalIdentifier = value;
				OnPropertyChanged("ExternalIdentifier");
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
		#endregion

		#region CTor
		public SmsAdapterProfile()
		{
		}

		public SmsAdapterProfile(JToken node) : base(node)
		{
			if(node["id"] != null)
			{
				this._Id = ParseLong(node["id"].Value<string>());
			}
			if(node["adapterUrl"] != null)
			{
				this._AdapterUrl = node["adapterUrl"].Value<string>();
			}
			if(node["sharedSecret"] != null)
			{
				this._SharedSecret = node["sharedSecret"].Value<string>();
			}
			if(node["isActive"] != null)
			{
				this._IsActive = ParseInt(node["isActive"].Value<string>());
			}
			if(node["settings"] != null)
			{
				{
					string key;
					this._Settings = new Dictionary<string, StringValue>();
					foreach(var arrayNode in node["settings"].Children<JProperty>())
					{
						key = arrayNode.Name;
						this._Settings[key] = ObjectFactory.Create<StringValue>(arrayNode.Value);
					}
				}
			}
			if(node["externalIdentifier"] != null)
			{
				this._ExternalIdentifier = node["externalIdentifier"].Value<string>();
			}
			if(node["name"] != null)
			{
				this._Name = node["name"].Value<string>();
			}
		}
		#endregion

		#region Methods
		public override Params ToParams(bool includeObjectType = true)
		{
			Params kparams = base.ToParams(includeObjectType);
			if (includeObjectType)
				kparams.AddReplace("objectType", "KalturaSmsAdapterProfile");
			kparams.AddIfNotNull("id", this._Id);
			kparams.AddIfNotNull("adapterUrl", this._AdapterUrl);
			kparams.AddIfNotNull("sharedSecret", this._SharedSecret);
			kparams.AddIfNotNull("isActive", this._IsActive);
			kparams.AddIfNotNull("settings", this._Settings);
			kparams.AddIfNotNull("externalIdentifier", this._ExternalIdentifier);
			kparams.AddIfNotNull("name", this._Name);
			return kparams;
		}
		protected override string getPropertyName(string apiName)
		{
			switch(apiName)
			{
				case ID:
					return "Id";
				case ADAPTER_URL:
					return "AdapterUrl";
				case SHARED_SECRET:
					return "SharedSecret";
				case IS_ACTIVE:
					return "IsActive";
				case SETTINGS:
					return "Settings";
				case EXTERNAL_IDENTIFIER:
					return "ExternalIdentifier";
				case NAME:
					return "Name";
				default:
					return base.getPropertyName(apiName);
			}
		}
		#endregion
	}
}

