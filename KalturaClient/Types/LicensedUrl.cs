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
	public class LicensedUrl : ObjectBase
	{
		#region Constants
		public const string MAIN_URL = "mainUrl";
		public const string ALT_URL = "altUrl";
		#endregion

		#region Private Fields
		private string _MainUrl = null;
		private string _AltUrl = null;
		#endregion

		#region Properties
		public string MainUrl
		{
			get { return _MainUrl; }
			set 
			{ 
				_MainUrl = value;
				OnPropertyChanged("MainUrl");
			}
		}
		public string AltUrl
		{
			get { return _AltUrl; }
			set 
			{ 
				_AltUrl = value;
				OnPropertyChanged("AltUrl");
			}
		}
		#endregion

		#region CTor
		public LicensedUrl()
		{
		}

		public LicensedUrl(XmlElement node) : base(node)
		{
			foreach (XmlElement propertyNode in node.ChildNodes)
			{
				switch (propertyNode.Name)
				{
					case "mainUrl":
						this._MainUrl = propertyNode.InnerText;
						continue;
					case "altUrl":
						this._AltUrl = propertyNode.InnerText;
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
				kparams.AddReplace("objectType", "KalturaLicensedUrl");
			kparams.AddIfNotNull("mainUrl", this._MainUrl);
			kparams.AddIfNotNull("altUrl", this._AltUrl);
			return kparams;
		}
		protected override string getPropertyName(string apiName)
		{
			switch(apiName)
			{
				case MAIN_URL:
					return "MainUrl";
				case ALT_URL:
					return "AltUrl";
				default:
					return base.getPropertyName(apiName);
			}
		}
		#endregion
	}
}

