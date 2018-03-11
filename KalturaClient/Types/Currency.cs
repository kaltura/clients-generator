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
	public class Currency : ObjectBase
	{
		#region Constants
		public const string NAME = "name";
		public const string CODE = "code";
		public const string SIGN = "sign";
		public const string IS_DEFAULT = "isDefault";
		#endregion

		#region Private Fields
		private string _Name = null;
		private string _Code = null;
		private string _Sign = null;
		private bool? _IsDefault = null;
		#endregion

		#region Properties
		public string Name
		{
			get { return _Name; }
			set 
			{ 
				_Name = value;
				OnPropertyChanged("Name");
			}
		}
		public string Code
		{
			get { return _Code; }
			set 
			{ 
				_Code = value;
				OnPropertyChanged("Code");
			}
		}
		public string Sign
		{
			get { return _Sign; }
			set 
			{ 
				_Sign = value;
				OnPropertyChanged("Sign");
			}
		}
		public bool? IsDefault
		{
			get { return _IsDefault; }
			set 
			{ 
				_IsDefault = value;
				OnPropertyChanged("IsDefault");
			}
		}
		#endregion

		#region CTor
		public Currency()
		{
		}

		public Currency(XmlElement node) : base(node)
		{
			foreach (XmlElement propertyNode in node.ChildNodes)
			{
				switch (propertyNode.Name)
				{
					case "name":
						this._Name = propertyNode.InnerText;
						continue;
					case "code":
						this._Code = propertyNode.InnerText;
						continue;
					case "sign":
						this._Sign = propertyNode.InnerText;
						continue;
					case "isDefault":
						this._IsDefault = ParseBool(propertyNode.InnerText);
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
				kparams.AddReplace("objectType", "KalturaCurrency");
			kparams.AddIfNotNull("name", this._Name);
			kparams.AddIfNotNull("code", this._Code);
			kparams.AddIfNotNull("sign", this._Sign);
			kparams.AddIfNotNull("isDefault", this._IsDefault);
			return kparams;
		}
		protected override string getPropertyName(string apiName)
		{
			switch(apiName)
			{
				case NAME:
					return "Name";
				case CODE:
					return "Code";
				case SIGN:
					return "Sign";
				case IS_DEFAULT:
					return "IsDefault";
				default:
					return base.getPropertyName(apiName);
			}
		}
		#endregion
	}
}

