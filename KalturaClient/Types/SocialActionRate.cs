// ===================================================================================================
//                           _  __     _ _
//                          | |/ /__ _| | |_ _  _ _ _ __ _
//                          | ' </ _` | |  _| || | '_/ _` |
//                          |_|\_\__,_|_|\__|\_,_|_| \__,_|
//
// This file is part of the Kaltura Collaborative Media Suite which allows users
// to do with audio, video, and animation what Wiki platforms allow them to do with
// text.
//
// Copyright (C) 2006-2021  Kaltura Inc.
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
	public class SocialActionRate : SocialAction
	{
		#region Constants
		public const string RATE = "rate";
		#endregion

		#region Private Fields
		private int _Rate = Int32.MinValue;
		#endregion

		#region Properties
		[JsonProperty]
		public int Rate
		{
			get { return _Rate; }
			set 
			{ 
				_Rate = value;
				OnPropertyChanged("Rate");
			}
		}
		#endregion

		#region CTor
		public SocialActionRate()
		{
		}

		public SocialActionRate(JToken node) : base(node)
		{
			if(node["rate"] != null)
			{
				this._Rate = ParseInt(node["rate"].Value<string>());
			}
		}
		#endregion

		#region Methods
		public override Params ToParams(bool includeObjectType = true)
		{
			Params kparams = base.ToParams(includeObjectType);
			if (includeObjectType)
				kparams.AddReplace("objectType", "KalturaSocialActionRate");
			kparams.AddIfNotNull("rate", this._Rate);
			return kparams;
		}
		protected override string getPropertyName(string apiName)
		{
			switch(apiName)
			{
				case RATE:
					return "Rate";
				default:
					return base.getPropertyName(apiName);
			}
		}
		#endregion
	}
}

