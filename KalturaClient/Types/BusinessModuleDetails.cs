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
	public class BusinessModuleDetails : ObjectBase
	{
		#region Constants
		public const string BUSINESS_MODULE_ID = "businessModuleId";
		public const string BUSINESS_MODULE_TYPE = "businessModuleType";
		#endregion

		#region Private Fields
		private int _BusinessModuleId = Int32.MinValue;
		private TransactionType _BusinessModuleType = null;
		#endregion

		#region Properties
		[JsonProperty]
		public int BusinessModuleId
		{
			get { return _BusinessModuleId; }
			set 
			{ 
				_BusinessModuleId = value;
				OnPropertyChanged("BusinessModuleId");
			}
		}
		[JsonProperty]
		public TransactionType BusinessModuleType
		{
			get { return _BusinessModuleType; }
			set 
			{ 
				_BusinessModuleType = value;
				OnPropertyChanged("BusinessModuleType");
			}
		}
		#endregion

		#region CTor
		public BusinessModuleDetails()
		{
		}

		public BusinessModuleDetails(JToken node) : base(node)
		{
			if(node["businessModuleId"] != null)
			{
				this._BusinessModuleId = ParseInt(node["businessModuleId"].Value<string>());
			}
			if(node["businessModuleType"] != null)
			{
				this._BusinessModuleType = (TransactionType)StringEnum.Parse(typeof(TransactionType), node["businessModuleType"].Value<string>());
			}
		}
		#endregion

		#region Methods
		public override Params ToParams(bool includeObjectType = true)
		{
			Params kparams = base.ToParams(includeObjectType);
			if (includeObjectType)
				kparams.AddReplace("objectType", "KalturaBusinessModuleDetails");
			kparams.AddIfNotNull("businessModuleId", this._BusinessModuleId);
			kparams.AddIfNotNull("businessModuleType", this._BusinessModuleType);
			return kparams;
		}
		protected override string getPropertyName(string apiName)
		{
			switch(apiName)
			{
				case BUSINESS_MODULE_ID:
					return "BusinessModuleId";
				case BUSINESS_MODULE_TYPE:
					return "BusinessModuleType";
				default:
					return base.getPropertyName(apiName);
			}
		}
		#endregion
	}
}

