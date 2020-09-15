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
	public class PromotionInfo : ObjectBase
	{
		#region Constants
		public const string CAMPAIGN_ID = "campaignId";
		#endregion

		#region Private Fields
		private long _CampaignId = long.MinValue;
		#endregion

		#region Properties
		[JsonProperty]
		public long CampaignId
		{
			get { return _CampaignId; }
			set 
			{ 
				_CampaignId = value;
				OnPropertyChanged("CampaignId");
			}
		}
		#endregion

		#region CTor
		public PromotionInfo()
		{
		}

		public PromotionInfo(JToken node) : base(node)
		{
			if(node["campaignId"] != null)
			{
				this._CampaignId = ParseLong(node["campaignId"].Value<string>());
			}
		}
		#endregion

		#region Methods
		public override Params ToParams(bool includeObjectType = true)
		{
			Params kparams = base.ToParams(includeObjectType);
			if (includeObjectType)
				kparams.AddReplace("objectType", "KalturaPromotionInfo");
			kparams.AddIfNotNull("campaignId", this._CampaignId);
			return kparams;
		}
		protected override string getPropertyName(string apiName)
		{
			switch(apiName)
			{
				case CAMPAIGN_ID:
					return "CampaignId";
				default:
					return base.getPropertyName(apiName);
			}
		}
		#endregion
	}
}

