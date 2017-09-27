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
	public class ApiActionPermissionItem : PermissionItem
	{
		#region Constants
		public const string SERVICE = "service";
		public const string ACTION = "action";
		#endregion

		#region Private Fields
		private string _Service = null;
		private string _Action = null;
		#endregion

		#region Properties
		public string Service
		{
			get { return _Service; }
			set 
			{ 
				_Service = value;
				OnPropertyChanged("Service");
			}
		}
		public string Action
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
		public ApiActionPermissionItem()
		{
		}

		public ApiActionPermissionItem(XmlElement node) : base(node)
		{
			foreach (XmlElement propertyNode in node.ChildNodes)
			{
				switch (propertyNode.Name)
				{
					case "service":
						this._Service = propertyNode.InnerText;
						continue;
					case "action":
						this._Action = propertyNode.InnerText;
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
				kparams.AddReplace("objectType", "KalturaApiActionPermissionItem");
			kparams.AddIfNotNull("service", this._Service);
			kparams.AddIfNotNull("action", this._Action);
			return kparams;
		}
		protected override string getPropertyName(string apiName)
		{
			switch(apiName)
			{
				case SERVICE:
					return "Service";
				case ACTION:
					return "Action";
				default:
					return base.getPropertyName(apiName);
			}
		}
		#endregion
	}
}

