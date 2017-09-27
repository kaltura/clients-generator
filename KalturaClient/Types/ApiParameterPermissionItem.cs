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
	public class ApiParameterPermissionItem : PermissionItem
	{
		#region Constants
		public const string OBJECT = "object";
		public const string PARAMETER = "parameter";
		public const string ACTION = "action";
		#endregion

		#region Private Fields
		private string _Object = null;
		private string _Parameter = null;
		private ApiParameterPermissionItemAction _Action = null;
		#endregion

		#region Properties
		public string Object
		{
			get { return _Object; }
			set 
			{ 
				_Object = value;
				OnPropertyChanged("Object");
			}
		}
		public string Parameter
		{
			get { return _Parameter; }
			set 
			{ 
				_Parameter = value;
				OnPropertyChanged("Parameter");
			}
		}
		public ApiParameterPermissionItemAction Action
		{
			get { return _Action; }
			set 
			{ 
				_Action = value;
				OnPropertyChanged("Action");
			}
		}
		#endregion

		#region CTor
		public ApiParameterPermissionItem()
		{
		}

		public ApiParameterPermissionItem(XmlElement node) : base(node)
		{
			foreach (XmlElement propertyNode in node.ChildNodes)
			{
				switch (propertyNode.Name)
				{
					case "object":
						this._Object = propertyNode.InnerText;
						continue;
					case "parameter":
						this._Parameter = propertyNode.InnerText;
						continue;
					case "action":
						this._Action = (ApiParameterPermissionItemAction)StringEnum.Parse(typeof(ApiParameterPermissionItemAction), propertyNode.InnerText);
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
				kparams.AddReplace("objectType", "KalturaApiParameterPermissionItem");
			kparams.AddIfNotNull("object", this._Object);
			kparams.AddIfNotNull("parameter", this._Parameter);
			kparams.AddIfNotNull("action", this._Action);
			return kparams;
		}
		protected override string getPropertyName(string apiName)
		{
			switch(apiName)
			{
				case OBJECT:
					return "Object";
				case PARAMETER:
					return "Parameter";
				case ACTION:
					return "Action";
				default:
					return base.getPropertyName(apiName);
			}
		}
		#endregion
	}
}

