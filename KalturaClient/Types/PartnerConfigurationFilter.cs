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
// Copyright (C) 2006-2018  Kaltura Inc.
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
	public class PartnerConfigurationFilter : Filter
	{
		#region Constants
		public const string PARTNER_CONFIGURATION_TYPE_EQUAL = "partnerConfigurationTypeEqual";
		public new const string ORDER_BY = "orderBy";
		#endregion

		#region Private Fields
		private PartnerConfigurationType _PartnerConfigurationTypeEqual = null;
		private PartnerConfigurationOrderBy _OrderBy = null;
		#endregion

		#region Properties
		[JsonProperty]
		public PartnerConfigurationType PartnerConfigurationTypeEqual
		{
			get { return _PartnerConfigurationTypeEqual; }
			set 
			{ 
				_PartnerConfigurationTypeEqual = value;
				OnPropertyChanged("PartnerConfigurationTypeEqual");
			}
		}
		[JsonProperty]
		public new PartnerConfigurationOrderBy OrderBy
		{
			get { return _OrderBy; }
			set 
			{ 
				_OrderBy = value;
				OnPropertyChanged("OrderBy");
			}
		}
		#endregion

		#region CTor
		public PartnerConfigurationFilter()
		{
		}

		public PartnerConfigurationFilter(JToken node) : base(node)
		{
			if(node["partnerConfigurationTypeEqual"] != null)
			{
				this._PartnerConfigurationTypeEqual = (PartnerConfigurationType)StringEnum.Parse(typeof(PartnerConfigurationType), node["partnerConfigurationTypeEqual"].Value<string>());
			}
			if(node["orderBy"] != null)
			{
				this._OrderBy = (PartnerConfigurationOrderBy)StringEnum.Parse(typeof(PartnerConfigurationOrderBy), node["orderBy"].Value<string>());
			}
		}
		#endregion

		#region Methods
		public override Params ToParams(bool includeObjectType = true)
		{
			Params kparams = base.ToParams(includeObjectType);
			if (includeObjectType)
				kparams.AddReplace("objectType", "KalturaPartnerConfigurationFilter");
			kparams.AddIfNotNull("partnerConfigurationTypeEqual", this._PartnerConfigurationTypeEqual);
			kparams.AddIfNotNull("orderBy", this._OrderBy);
			return kparams;
		}
		protected override string getPropertyName(string apiName)
		{
			switch(apiName)
			{
				case PARTNER_CONFIGURATION_TYPE_EQUAL:
					return "PartnerConfigurationTypeEqual";
				case ORDER_BY:
					return "OrderBy";
				default:
					return base.getPropertyName(apiName);
			}
		}
		#endregion
	}
}

