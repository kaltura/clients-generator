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
	public class DeviceModelCondition : Condition
	{
		#region Constants
		public const string REGEX_EQUAL = "regexEqual";
		#endregion

		#region Private Fields
		private string _RegexEqual = null;
		#endregion

		#region Properties
		[JsonProperty]
		public string RegexEqual
		{
			get { return _RegexEqual; }
			set 
			{ 
				_RegexEqual = value;
				OnPropertyChanged("RegexEqual");
			}
		}
		#endregion

		#region CTor
		public DeviceModelCondition()
		{
		}

		public DeviceModelCondition(JToken node) : base(node)
		{
			if(node["regexEqual"] != null)
			{
				this._RegexEqual = node["regexEqual"].Value<string>();
			}
		}
		#endregion

		#region Methods
		public override Params ToParams(bool includeObjectType = true)
		{
			Params kparams = base.ToParams(includeObjectType);
			if (includeObjectType)
				kparams.AddReplace("objectType", "KalturaDeviceModelCondition");
			kparams.AddIfNotNull("regexEqual", this._RegexEqual);
			return kparams;
		}
		protected override string getPropertyName(string apiName)
		{
			switch(apiName)
			{
				case REGEX_EQUAL:
					return "RegexEqual";
				default:
					return base.getPropertyName(apiName);
			}
		}
		#endregion
	}
}

