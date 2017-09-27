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
	public class ClientConfiguration : ObjectBase
	{
		#region Constants
		public const string CLIENT_TAG = "clientTag";
		public const string API_VERSION = "apiVersion";
		#endregion

		#region Private Fields
		private string _ClientTag = null;
		private string _ApiVersion = null;
		#endregion

		#region Properties
		public string ClientTag
		{
			get { return _ClientTag; }
			set 
			{ 
				_ClientTag = value;
				OnPropertyChanged("ClientTag");
			}
		}
		public string ApiVersion
		{
			get { return _ApiVersion; }
			set 
			{ 
				_ApiVersion = value;
				OnPropertyChanged("ApiVersion");
			}
		}
		#endregion

		#region CTor
		public ClientConfiguration()
		{
		}

		public ClientConfiguration(XmlElement node) : base(node)
		{
			foreach (XmlElement propertyNode in node.ChildNodes)
			{
				switch (propertyNode.Name)
				{
					case "clientTag":
						this._ClientTag = propertyNode.InnerText;
						continue;
					case "apiVersion":
						this._ApiVersion = propertyNode.InnerText;
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
				kparams.AddReplace("objectType", "KalturaClientConfiguration");
			kparams.AddIfNotNull("clientTag", this._ClientTag);
			kparams.AddIfNotNull("apiVersion", this._ApiVersion);
			return kparams;
		}
		protected override string getPropertyName(string apiName)
		{
			switch(apiName)
			{
				case CLIENT_TAG:
					return "ClientTag";
				case API_VERSION:
					return "ApiVersion";
				default:
					return base.getPropertyName(apiName);
			}
		}
		#endregion
	}
}

