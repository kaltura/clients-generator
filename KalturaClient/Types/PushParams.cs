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
	public class PushParams : ObjectBase
	{
		#region Constants
		public const string TOKEN = "token";
		public const string EXTERNAL_TOKEN = "externalToken";
		#endregion

		#region Private Fields
		private string _Token = null;
		private string _ExternalToken = null;
		#endregion

		#region Properties
		public string Token
		{
			get { return _Token; }
			set 
			{ 
				_Token = value;
				OnPropertyChanged("Token");
			}
		}
		public string ExternalToken
		{
			get { return _ExternalToken; }
			set 
			{ 
				_ExternalToken = value;
				OnPropertyChanged("ExternalToken");
			}
		}
		#endregion

		#region CTor
		public PushParams()
		{
		}

		public PushParams(XmlElement node) : base(node)
		{
			foreach (XmlElement propertyNode in node.ChildNodes)
			{
				switch (propertyNode.Name)
				{
					case "token":
						this._Token = propertyNode.InnerText;
						continue;
					case "externalToken":
						this._ExternalToken = propertyNode.InnerText;
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
				kparams.AddReplace("objectType", "KalturaPushParams");
			kparams.AddIfNotNull("token", this._Token);
			kparams.AddIfNotNull("externalToken", this._ExternalToken);
			return kparams;
		}
		protected override string getPropertyName(string apiName)
		{
			switch(apiName)
			{
				case TOKEN:
					return "Token";
				case EXTERNAL_TOKEN:
					return "ExternalToken";
				default:
					return base.getPropertyName(apiName);
			}
		}
		#endregion
	}
}

