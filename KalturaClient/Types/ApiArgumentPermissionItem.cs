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
	public class ApiArgumentPermissionItem : PermissionItem
	{
		#region Constants
		public const string SERVICE = "service";
		public const string ACTION = "action";
		public const string PARAMETER = "parameter";
		#endregion

		#region Private Fields
		private string _Service = null;
		private string _Action = null;
		private string _Parameter = null;
		#endregion

		#region Properties
		[JsonProperty]
		public string Service
		{
			get { return _Service; }
			set 
			{ 
				_Service = value;
				OnPropertyChanged("Service");
			}
		}
		[JsonProperty]
		public string Action
		{
			get { return _Action; }
			set 
			{ 
				_Action = value;
				OnPropertyChanged("Action");
			}
		}
		[JsonProperty]
		public string Parameter
		{
			get { return _Parameter; }
			set 
			{ 
				_Parameter = value;
				OnPropertyChanged("Parameter");
			}
		}
		#endregion

		#region CTor
		public ApiArgumentPermissionItem()
		{
		}

		public ApiArgumentPermissionItem(JToken node) : base(node)
		{
			if(node["service"] != null)
			{
				this._Service = node["service"].Value<string>();
			}
			if(node["action"] != null)
			{
				this._Action = node["action"].Value<string>();
			}
			if(node["parameter"] != null)
			{
				this._Parameter = node["parameter"].Value<string>();
			}
		}
		#endregion

		#region Methods
		public override Params ToParams(bool includeObjectType = true)
		{
			Params kparams = base.ToParams(includeObjectType);
			if (includeObjectType)
				kparams.AddReplace("objectType", "KalturaApiArgumentPermissionItem");
			kparams.AddIfNotNull("service", this._Service);
			kparams.AddIfNotNull("action", this._Action);
			kparams.AddIfNotNull("parameter", this._Parameter);
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
				case PARAMETER:
					return "Parameter";
				default:
					return base.getPropertyName(apiName);
			}
		}
		#endregion
	}
}

