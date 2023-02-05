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
	public class CommercePartnerConfig : PartnerConfiguration
	{
		#region Constants
		public const string BOOKMARK_EVENT_THRESHOLDS = "bookmarkEventThresholds";
		#endregion

		#region Private Fields
		private IList<BookmarkEventThreshold> _BookmarkEventThresholds;
		#endregion

		#region Properties
		[JsonProperty]
		public IList<BookmarkEventThreshold> BookmarkEventThresholds
		{
			get { return _BookmarkEventThresholds; }
			set 
			{ 
				_BookmarkEventThresholds = value;
				OnPropertyChanged("BookmarkEventThresholds");
			}
		}
		#endregion

		#region CTor
		public CommercePartnerConfig()
		{
		}

		public CommercePartnerConfig(JToken node) : base(node)
		{
			if(node["bookmarkEventThresholds"] != null)
			{
				this._BookmarkEventThresholds = new List<BookmarkEventThreshold>();
				foreach(var arrayNode in node["bookmarkEventThresholds"].Children())
				{
					this._BookmarkEventThresholds.Add(ObjectFactory.Create<BookmarkEventThreshold>(arrayNode));
				}
			}
		}
		#endregion

		#region Methods
		public override Params ToParams(bool includeObjectType = true)
		{
			Params kparams = base.ToParams(includeObjectType);
			if (includeObjectType)
				kparams.AddReplace("objectType", "KalturaCommercePartnerConfig");
			kparams.AddIfNotNull("bookmarkEventThresholds", this._BookmarkEventThresholds);
			return kparams;
		}
		protected override string getPropertyName(string apiName)
		{
			switch(apiName)
			{
				case BOOKMARK_EVENT_THRESHOLDS:
					return "BookmarkEventThresholds";
				default:
					return base.getPropertyName(apiName);
			}
		}
		#endregion
	}
}

