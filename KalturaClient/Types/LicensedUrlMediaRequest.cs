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
	public class LicensedUrlMediaRequest : LicensedUrlBaseRequest
	{
		#region Constants
		public const string CONTENT_ID = "contentId";
		public const string BASE_URL = "baseUrl";
		#endregion

		#region Private Fields
		private int _ContentId = Int32.MinValue;
		private string _BaseUrl = null;
		#endregion

		#region Properties
		public int ContentId
		{
			get { return _ContentId; }
			set 
			{ 
				_ContentId = value;
				OnPropertyChanged("ContentId");
			}
		}
		public string BaseUrl
		{
			get { return _BaseUrl; }
			set 
			{ 
				_BaseUrl = value;
				OnPropertyChanged("BaseUrl");
			}
		}
		#endregion

		#region CTor
		public LicensedUrlMediaRequest()
		{
		}

		public LicensedUrlMediaRequest(XmlElement node) : base(node)
		{
			foreach (XmlElement propertyNode in node.ChildNodes)
			{
				switch (propertyNode.Name)
				{
					case "contentId":
						this._ContentId = ParseInt(propertyNode.InnerText);
						continue;
					case "baseUrl":
						this._BaseUrl = propertyNode.InnerText;
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
				kparams.AddReplace("objectType", "KalturaLicensedUrlMediaRequest");
			kparams.AddIfNotNull("contentId", this._ContentId);
			kparams.AddIfNotNull("baseUrl", this._BaseUrl);
			return kparams;
		}
		protected override string getPropertyName(string apiName)
		{
			switch(apiName)
			{
				case CONTENT_ID:
					return "ContentId";
				case BASE_URL:
					return "BaseUrl";
				default:
					return base.getPropertyName(apiName);
			}
		}
		#endregion
	}
}

