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
	public class PermissionItemByArgumentFilter : PermissionItemByApiActionFilter
	{
		#region Constants
		public const string PARAMETER_EQUAL = "parameterEqual";
		#endregion

		#region Private Fields
		private string _ParameterEqual = null;
		#endregion

		#region Properties
		[JsonProperty]
		public string ParameterEqual
		{
			get { return _ParameterEqual; }
			set 
			{ 
				_ParameterEqual = value;
				OnPropertyChanged("ParameterEqual");
			}
		}
		#endregion

		#region CTor
		public PermissionItemByArgumentFilter()
		{
		}

		public PermissionItemByArgumentFilter(JToken node) : base(node)
		{
			if(node["parameterEqual"] != null)
			{
				this._ParameterEqual = node["parameterEqual"].Value<string>();
			}
		}
		#endregion

		#region Methods
		public override Params ToParams(bool includeObjectType = true)
		{
			Params kparams = base.ToParams(includeObjectType);
			if (includeObjectType)
				kparams.AddReplace("objectType", "KalturaPermissionItemByArgumentFilter");
			kparams.AddIfNotNull("parameterEqual", this._ParameterEqual);
			return kparams;
		}
		protected override string getPropertyName(string apiName)
		{
			switch(apiName)
			{
				case PARAMETER_EQUAL:
					return "ParameterEqual";
				default:
					return base.getPropertyName(apiName);
			}
		}
		#endregion
	}
}

