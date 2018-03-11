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
	public class ConfigurationGroupTag : ObjectBase
	{
		#region Constants
		public const string CONFIGURATION_GROUP_ID = "configurationGroupId";
		public const string PARTNER_ID = "partnerId";
		public const string TAG = "tag";
		#endregion

		#region Private Fields
		private string _ConfigurationGroupId = null;
		private int _PartnerId = Int32.MinValue;
		private string _Tag = null;
		#endregion

		#region Properties
		public string ConfigurationGroupId
		{
			get { return _ConfigurationGroupId; }
			set 
			{ 
				_ConfigurationGroupId = value;
				OnPropertyChanged("ConfigurationGroupId");
			}
		}
		public int PartnerId
		{
			get { return _PartnerId; }
		}
		public string Tag
		{
			get { return _Tag; }
			set 
			{ 
				_Tag = value;
				OnPropertyChanged("Tag");
			}
		}
		#endregion

		#region CTor
		public ConfigurationGroupTag()
		{
		}

		public ConfigurationGroupTag(XmlElement node) : base(node)
		{
			foreach (XmlElement propertyNode in node.ChildNodes)
			{
				switch (propertyNode.Name)
				{
					case "configurationGroupId":
						this._ConfigurationGroupId = propertyNode.InnerText;
						continue;
					case "partnerId":
						this._PartnerId = ParseInt(propertyNode.InnerText);
						continue;
					case "tag":
						this._Tag = propertyNode.InnerText;
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
				kparams.AddReplace("objectType", "KalturaConfigurationGroupTag");
			kparams.AddIfNotNull("configurationGroupId", this._ConfigurationGroupId);
			kparams.AddIfNotNull("partnerId", this._PartnerId);
			kparams.AddIfNotNull("tag", this._Tag);
			return kparams;
		}
		protected override string getPropertyName(string apiName)
		{
			switch(apiName)
			{
				case CONFIGURATION_GROUP_ID:
					return "ConfigurationGroupId";
				case PARTNER_ID:
					return "PartnerId";
				case TAG:
					return "Tag";
				default:
					return base.getPropertyName(apiName);
			}
		}
		#endregion
	}
}

