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
	public class ConfigurationGroup : ObjectBase
	{
		#region Constants
		public const string ID = "id";
		public const string NAME = "name";
		public const string PARTNER_ID = "partnerId";
		public const string IS_DEFAULT = "isDefault";
		public const string TAGS = "tags";
		public const string NUMBER_OF_DEVICES = "numberOfDevices";
		public const string CONFIGURATION_IDENTIFIERS = "configurationIdentifiers";
		#endregion

		#region Private Fields
		private string _Id = null;
		private string _Name = null;
		private int _PartnerId = Int32.MinValue;
		private bool? _IsDefault = null;
		private IList<StringValue> _Tags;
		private long _NumberOfDevices = long.MinValue;
		private IList<ConfigurationIdentifier> _ConfigurationIdentifiers;
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
		public int PartnerId
		{
			get { return _PartnerId; }
			private set 
			{ 
				_PartnerId = value;
				OnPropertyChanged("PartnerId");
			}
		}
		[JsonProperty]
		public bool? IsDefault
		{
			get { return _IsDefault; }
			set 
			{ 
				_IsDefault = value;
				OnPropertyChanged("IsDefault");
			}
		}
		[JsonProperty]
		public IList<StringValue> Tags
		{
			get { return _Tags; }
			private set 
			{ 
				_Tags = value;
				OnPropertyChanged("Tags");
			}
		}
		[JsonProperty]
		public long NumberOfDevices
		{
			get { return _NumberOfDevices; }
			private set 
			{ 
				_NumberOfDevices = value;
				OnPropertyChanged("NumberOfDevices");
			}
		}
		[JsonProperty]
		public IList<ConfigurationIdentifier> ConfigurationIdentifiers
		{
			get { return _ConfigurationIdentifiers; }
			private set 
			{ 
				_ConfigurationIdentifiers = value;
				OnPropertyChanged("ConfigurationIdentifiers");
			}
		}
		#endregion

		#region CTor
		public ConfigurationGroup()
		{
		}

		public ConfigurationGroup(JToken node) : base(node)
		{
			if(node["id"] != null)
			{
				this._Id = node["id"].Value<string>();
			}
			if(node["name"] != null)
			{
				this._Name = node["name"].Value<string>();
			}
			if(node["partnerId"] != null)
			{
				this._PartnerId = ParseInt(node["partnerId"].Value<string>());
			}
			if(node["isDefault"] != null)
			{
				this._IsDefault = ParseBool(node["isDefault"].Value<string>());
			}
			if(node["tags"] != null)
			{
				this._Tags = new List<StringValue>();
				foreach(var arrayNode in node["tags"].Children())
				{
					this._Tags.Add(ObjectFactory.Create<StringValue>(arrayNode));
				}
			}
			if(node["numberOfDevices"] != null)
			{
				this._NumberOfDevices = ParseLong(node["numberOfDevices"].Value<string>());
			}
			if(node["configurationIdentifiers"] != null)
			{
				this._ConfigurationIdentifiers = new List<ConfigurationIdentifier>();
				foreach(var arrayNode in node["configurationIdentifiers"].Children())
				{
					this._ConfigurationIdentifiers.Add(ObjectFactory.Create<ConfigurationIdentifier>(arrayNode));
				}
			}
		}
		#endregion

		#region Methods
		public override Params ToParams(bool includeObjectType = true)
		{
			Params kparams = base.ToParams(includeObjectType);
			if (includeObjectType)
				kparams.AddReplace("objectType", "KalturaConfigurationGroup");
			kparams.AddIfNotNull("id", this._Id);
			kparams.AddIfNotNull("name", this._Name);
			kparams.AddIfNotNull("partnerId", this._PartnerId);
			kparams.AddIfNotNull("isDefault", this._IsDefault);
			kparams.AddIfNotNull("tags", this._Tags);
			kparams.AddIfNotNull("numberOfDevices", this._NumberOfDevices);
			kparams.AddIfNotNull("configurationIdentifiers", this._ConfigurationIdentifiers);
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
				case PARTNER_ID:
					return "PartnerId";
				case IS_DEFAULT:
					return "IsDefault";
				case TAGS:
					return "Tags";
				case NUMBER_OF_DEVICES:
					return "NumberOfDevices";
				case CONFIGURATION_IDENTIFIERS:
					return "ConfigurationIdentifiers";
				default:
					return base.getPropertyName(apiName);
			}
		}
		#endregion
	}
}

