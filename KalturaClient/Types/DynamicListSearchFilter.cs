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
	public class DynamicListSearchFilter : DynamicListFilter
	{
		#region Constants
		public const string VALUE_IN = "valueIn";
		#endregion

		#region Private Fields
		private string _ValueIn = null;
		#endregion

		#region Properties
		[JsonProperty]
		public string ValueIn
		{
			get { return _ValueIn; }
			set 
			{ 
				_ValueIn = value;
				OnPropertyChanged("ValueIn");
			}
		}
		#endregion

		#region CTor
		public DynamicListSearchFilter()
		{
		}

		public DynamicListSearchFilter(JToken node) : base(node)
		{
			if(node["valueIn"] != null)
			{
				this._ValueIn = node["valueIn"].Value<string>();
			}
		}
		#endregion

		#region Methods
		public override Params ToParams(bool includeObjectType = true)
		{
			Params kparams = base.ToParams(includeObjectType);
			if (includeObjectType)
				kparams.AddReplace("objectType", "KalturaDynamicListSearchFilter");
			kparams.AddIfNotNull("valueIn", this._ValueIn);
			return kparams;
		}
		protected override string getPropertyName(string apiName)
		{
			switch(apiName)
			{
				case VALUE_IN:
					return "ValueIn";
				default:
					return base.getPropertyName(apiName);
			}
		}
		#endregion
	}
}

