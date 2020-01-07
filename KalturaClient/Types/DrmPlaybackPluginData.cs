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
// Copyright (C) 2006-2020  Kaltura Inc.
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
	public class DrmPlaybackPluginData : PluginData
	{
		#region Constants
		public const string SCHEME = "scheme";
		public const string LICENSE_URL = "licenseURL";
		#endregion

		#region Private Fields
		private DrmSchemeName _Scheme = null;
		private string _LicenseURL = null;
		#endregion

		#region Properties
		[JsonProperty]
		public DrmSchemeName Scheme
		{
			get { return _Scheme; }
			set 
			{ 
				_Scheme = value;
				OnPropertyChanged("Scheme");
			}
		}
		[JsonProperty]
		public string LicenseURL
		{
			get { return _LicenseURL; }
			set 
			{ 
				_LicenseURL = value;
				OnPropertyChanged("LicenseURL");
			}
		}
		#endregion

		#region CTor
		public DrmPlaybackPluginData()
		{
		}

		public DrmPlaybackPluginData(JToken node) : base(node)
		{
			if(node["scheme"] != null)
			{
				this._Scheme = (DrmSchemeName)StringEnum.Parse(typeof(DrmSchemeName), node["scheme"].Value<string>());
			}
			if(node["licenseURL"] != null)
			{
				this._LicenseURL = node["licenseURL"].Value<string>();
			}
		}
		#endregion

		#region Methods
		public override Params ToParams(bool includeObjectType = true)
		{
			Params kparams = base.ToParams(includeObjectType);
			if (includeObjectType)
				kparams.AddReplace("objectType", "KalturaDrmPlaybackPluginData");
			kparams.AddIfNotNull("scheme", this._Scheme);
			kparams.AddIfNotNull("licenseURL", this._LicenseURL);
			return kparams;
		}
		protected override string getPropertyName(string apiName)
		{
			switch(apiName)
			{
				case SCHEME:
					return "Scheme";
				case LICENSE_URL:
					return "LicenseURL";
				default:
					return base.getPropertyName(apiName);
			}
		}
		#endregion
	}
}

