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
	public class UnifiedChannelInfo : UnifiedChannel
	{
		#region Constants
		public const string NAME = "name";
		public const string START_DATE_IN_SECONDS = "startDateInSeconds";
		public const string END_DATE_IN_SECONDS = "endDateInSeconds";
		#endregion

		#region Private Fields
		private string _Name = null;
		private long _StartDateInSeconds = long.MinValue;
		private long _EndDateInSeconds = long.MinValue;
		#endregion

		#region Properties
		[JsonProperty]
		public string Name
		{
			get { return _Name; }
			set 
			{ 
				_Name = value;
				OnPropertyChanged("Name");
			}
		}
		[JsonProperty]
		public long StartDateInSeconds
		{
			get { return _StartDateInSeconds; }
			set 
			{ 
				_StartDateInSeconds = value;
				OnPropertyChanged("StartDateInSeconds");
			}
		}
		[JsonProperty]
		public long EndDateInSeconds
		{
			get { return _EndDateInSeconds; }
			set 
			{ 
				_EndDateInSeconds = value;
				OnPropertyChanged("EndDateInSeconds");
			}
		}
		#endregion

		#region CTor
		public UnifiedChannelInfo()
		{
		}

		public UnifiedChannelInfo(JToken node) : base(node)
		{
			if(node["name"] != null)
			{
				this._Name = node["name"].Value<string>();
			}
			if(node["startDateInSeconds"] != null)
			{
				this._StartDateInSeconds = ParseLong(node["startDateInSeconds"].Value<string>());
			}
			if(node["endDateInSeconds"] != null)
			{
				this._EndDateInSeconds = ParseLong(node["endDateInSeconds"].Value<string>());
			}
		}
		#endregion

		#region Methods
		public override Params ToParams(bool includeObjectType = true)
		{
			Params kparams = base.ToParams(includeObjectType);
			if (includeObjectType)
				kparams.AddReplace("objectType", "KalturaUnifiedChannelInfo");
			kparams.AddIfNotNull("name", this._Name);
			kparams.AddIfNotNull("startDateInSeconds", this._StartDateInSeconds);
			kparams.AddIfNotNull("endDateInSeconds", this._EndDateInSeconds);
			return kparams;
		}
		protected override string getPropertyName(string apiName)
		{
			switch(apiName)
			{
				case NAME:
					return "Name";
				case START_DATE_IN_SECONDS:
					return "StartDateInSeconds";
				case END_DATE_IN_SECONDS:
					return "EndDateInSeconds";
				default:
					return base.getPropertyName(apiName);
			}
		}
		#endregion
	}
}

