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
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Kaltura.Types
{
	public class EngagementAdapter : EngagementAdapterBase
	{
		#region Constants
		public const string IS_ACTIVE = "isActive";
		public const string ADAPTER_URL = "adapterUrl";
		public const string PROVIDER_URL = "providerUrl";
		public const string ENGAGEMENT_ADAPTER_SETTINGS = "engagementAdapterSettings";
		public const string SHARED_SECRET = "sharedSecret";
		#endregion

		#region Private Fields
		private bool? _IsActive = null;
		private string _AdapterUrl = null;
		private string _ProviderUrl = null;
		private IDictionary<string, StringValue> _EngagementAdapterSettings;
		private string _SharedSecret = null;
		#endregion

		#region Properties
		[JsonProperty]
		public bool? IsActive
		{
			get { return _IsActive; }
			set 
			{ 
				_IsActive = value;
				OnPropertyChanged("IsActive");
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
		public string ProviderUrl
		{
			get { return _ProviderUrl; }
			set 
			{ 
				_ProviderUrl = value;
				OnPropertyChanged("ProviderUrl");
			}
		}
		[JsonProperty]
		public IDictionary<string, StringValue> EngagementAdapterSettings
		{
			get { return _EngagementAdapterSettings; }
			set 
			{ 
				_EngagementAdapterSettings = value;
				OnPropertyChanged("EngagementAdapterSettings");
			}
		}
		[JsonProperty]
		public string SharedSecret
		{
			get { return _SharedSecret; }
			private set 
			{ 
				_SharedSecret = value;
				OnPropertyChanged("SharedSecret");
			}
		}
		#endregion

		#region CTor
		public EngagementAdapter()
		{
		}

		public EngagementAdapter(JToken node) : base(node)
		{
			if(node["isActive"] != null)
			{
				this._IsActive = ParseBool(node["isActive"].Value<string>());
			}
			if(node["adapterUrl"] != null)
			{
				this._AdapterUrl = node["adapterUrl"].Value<string>();
			}
			if(node["providerUrl"] != null)
			{
				this._ProviderUrl = node["providerUrl"].Value<string>();
			}
			if(node["engagementAdapterSettings"] != null)
			{
				{
					string key;
					this._EngagementAdapterSettings = new Dictionary<string, StringValue>();
					foreach(var arrayNode in node["engagementAdapterSettings"].Children<JProperty>())
					{
						key = arrayNode.Name;
						this._EngagementAdapterSettings[key] = ObjectFactory.Create<StringValue>(arrayNode.Value);
					}
				}
			}
			if(node["sharedSecret"] != null)
			{
				this._SharedSecret = node["sharedSecret"].Value<string>();
			}
		}
		#endregion

		#region Methods
		public override Params ToParams(bool includeObjectType = true)
		{
			Params kparams = base.ToParams(includeObjectType);
			if (includeObjectType)
				kparams.AddReplace("objectType", "KalturaEngagementAdapter");
			kparams.AddIfNotNull("isActive", this._IsActive);
			kparams.AddIfNotNull("adapterUrl", this._AdapterUrl);
			kparams.AddIfNotNull("providerUrl", this._ProviderUrl);
			kparams.AddIfNotNull("engagementAdapterSettings", this._EngagementAdapterSettings);
			kparams.AddIfNotNull("sharedSecret", this._SharedSecret);
			return kparams;
		}
		protected override string getPropertyName(string apiName)
		{
			switch(apiName)
			{
				case IS_ACTIVE:
					return "IsActive";
				case ADAPTER_URL:
					return "AdapterUrl";
				case PROVIDER_URL:
					return "ProviderUrl";
				case ENGAGEMENT_ADAPTER_SETTINGS:
					return "EngagementAdapterSettings";
				case SHARED_SECRET:
					return "SharedSecret";
				default:
					return base.getPropertyName(apiName);
			}
		}
		#endregion
	}
}

