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
	public class SocialFacebookConfig : SocialConfig
	{
		#region Constants
		public const string APP_ID = "appId";
		public const string PERMISSIONS = "permissions";
		#endregion

		#region Private Fields
		private string _AppId = null;
		private string _Permissions = null;
		#endregion

		#region Properties
		public string AppId
		{
			get { return _AppId; }
			set 
			{ 
				_AppId = value;
				OnPropertyChanged("AppId");
			}
		}
		public string Permissions
		{
			get { return _Permissions; }
			set 
			{ 
				_Permissions = value;
				OnPropertyChanged("Permissions");
			}
		}
		#endregion

		#region CTor
		public SocialFacebookConfig()
		{
		}

		public SocialFacebookConfig(XmlElement node) : base(node)
		{
			foreach (XmlElement propertyNode in node.ChildNodes)
			{
				switch (propertyNode.Name)
				{
					case "appId":
						this._AppId = propertyNode.InnerText;
						continue;
					case "permissions":
						this._Permissions = propertyNode.InnerText;
						continue;
				}
			}
		}

		public SocialFacebookConfig(IDictionary<string,object> data) : base(data)
		{
			    this._AppId = data.TryGetValueSafe<string>("appId");
			    this._Permissions = data.TryGetValueSafe<string>("permissions");
		}
		#endregion

		#region Methods
		public override Params ToParams(bool includeObjectType = true)
		{
			Params kparams = base.ToParams(includeObjectType);
			if (includeObjectType)
				kparams.AddReplace("objectType", "KalturaSocialFacebookConfig");
			kparams.AddIfNotNull("appId", this._AppId);
			kparams.AddIfNotNull("permissions", this._Permissions);
			return kparams;
		}
		protected override string getPropertyName(string apiName)
		{
			switch(apiName)
			{
				case APP_ID:
					return "AppId";
				case PERMISSIONS:
					return "Permissions";
				default:
					return base.getPropertyName(apiName);
			}
		}
		#endregion
	}
}

