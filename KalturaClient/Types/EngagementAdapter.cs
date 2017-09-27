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
		public string ProviderUrl
		{
			get { return _ProviderUrl; }
			set 
			{ 
				_ProviderUrl = value;
				OnPropertyChanged("ProviderUrl");
			}
		}
		public IDictionary<string, StringValue> EngagementAdapterSettings
		{
			get { return _EngagementAdapterSettings; }
			set 
			{ 
				_EngagementAdapterSettings = value;
				OnPropertyChanged("EngagementAdapterSettings");
			}
		}
		public string SharedSecret
		{
			get { return _SharedSecret; }
		}
		#endregion

		#region CTor
		public EngagementAdapter()
		{
		}

		public EngagementAdapter(XmlElement node) : base(node)
		{
			foreach (XmlElement propertyNode in node.ChildNodes)
			{
				switch (propertyNode.Name)
				{
					case "isActive":
						this._IsActive = ParseBool(propertyNode.InnerText);
						continue;
					case "adapterUrl":
						this._AdapterUrl = propertyNode.InnerText;
						continue;
					case "providerUrl":
						this._ProviderUrl = propertyNode.InnerText;
						continue;
					case "engagementAdapterSettings":
						{
							string key;
							this._EngagementAdapterSettings = new Dictionary<string, StringValue>();
							foreach(XmlElement arrayNode in propertyNode.ChildNodes)
							{
								key = arrayNode["itemKey"].InnerText;;
								this._EngagementAdapterSettings[key] = ObjectFactory.Create<StringValue>(arrayNode);
							}
						}
						continue;
					case "sharedSecret":
						this._SharedSecret = propertyNode.InnerText;
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

