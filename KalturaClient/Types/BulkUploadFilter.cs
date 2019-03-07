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
// Copyright (C) 2006-2019  Kaltura Inc.
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
	public class BulkUploadFilter : Filter
	{
		#region Constants
		public const string UPLOADED_ON_EQUAL = "uploadedOnEqual";
		public const string DATE_COMPARISON_TYPE = "dateComparisonType";
		public const string STATUS_IN = "statusIn";
		public const string USER_ID_EQUAL_CURRENT = "userIdEqualCurrent";
		public new const string ORDER_BY = "orderBy";
		#endregion

		#region Private Fields
		private long _UploadedOnEqual = long.MinValue;
		private DateComparisonType _DateComparisonType = null;
		private string _StatusIn = null;
		private bool? _UserIdEqualCurrent = null;
		private BulkUploadOrderBy _OrderBy = null;
		#endregion

		#region Properties
		[JsonProperty]
		public long UploadedOnEqual
		{
			get { return _UploadedOnEqual; }
			set 
			{ 
				_UploadedOnEqual = value;
				OnPropertyChanged("UploadedOnEqual");
			}
		}
		[JsonProperty]
		public DateComparisonType DateComparisonType
		{
			get { return _DateComparisonType; }
			set 
			{ 
				_DateComparisonType = value;
				OnPropertyChanged("DateComparisonType");
			}
		}
		[JsonProperty]
		public string StatusIn
		{
			get { return _StatusIn; }
			set 
			{ 
				_StatusIn = value;
				OnPropertyChanged("StatusIn");
			}
		}
		[JsonProperty]
		public bool? UserIdEqualCurrent
		{
			get { return _UserIdEqualCurrent; }
			set 
			{ 
				_UserIdEqualCurrent = value;
				OnPropertyChanged("UserIdEqualCurrent");
			}
		}
		[JsonProperty]
		public new BulkUploadOrderBy OrderBy
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
		public BulkUploadFilter()
		{
		}

		public BulkUploadFilter(JToken node) : base(node)
		{
			if(node["uploadedOnEqual"] != null)
			{
				this._UploadedOnEqual = ParseLong(node["uploadedOnEqual"].Value<string>());
			}
			if(node["dateComparisonType"] != null)
			{
				this._DateComparisonType = (DateComparisonType)StringEnum.Parse(typeof(DateComparisonType), node["dateComparisonType"].Value<string>());
			}
			if(node["statusIn"] != null)
			{
				this._StatusIn = node["statusIn"].Value<string>();
			}
			if(node["userIdEqualCurrent"] != null)
			{
				this._UserIdEqualCurrent = ParseBool(node["userIdEqualCurrent"].Value<string>());
			}
			if(node["orderBy"] != null)
			{
				this._OrderBy = (BulkUploadOrderBy)StringEnum.Parse(typeof(BulkUploadOrderBy), node["orderBy"].Value<string>());
			}
		}
		#endregion

		#region Methods
		public override Params ToParams(bool includeObjectType = true)
		{
			Params kparams = base.ToParams(includeObjectType);
			if (includeObjectType)
				kparams.AddReplace("objectType", "KalturaBulkUploadFilter");
			kparams.AddIfNotNull("uploadedOnEqual", this._UploadedOnEqual);
			kparams.AddIfNotNull("dateComparisonType", this._DateComparisonType);
			kparams.AddIfNotNull("statusIn", this._StatusIn);
			kparams.AddIfNotNull("userIdEqualCurrent", this._UserIdEqualCurrent);
			kparams.AddIfNotNull("orderBy", this._OrderBy);
			return kparams;
		}
		protected override string getPropertyName(string apiName)
		{
			switch(apiName)
			{
				case UPLOADED_ON_EQUAL:
					return "UploadedOnEqual";
				case DATE_COMPARISON_TYPE:
					return "DateComparisonType";
				case STATUS_IN:
					return "StatusIn";
				case USER_ID_EQUAL_CURRENT:
					return "UserIdEqualCurrent";
				case ORDER_BY:
					return "OrderBy";
				default:
					return base.getPropertyName(apiName);
			}
		}
		#endregion
	}
}

