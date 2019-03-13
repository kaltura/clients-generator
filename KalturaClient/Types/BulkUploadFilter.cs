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
		public const string BULK_OBJECT_NAME_EQUAL = "bulkObjectNameEqual";
		public const string CREATE_DATE_GREATER_THAN_OR_EQUAL = "createDateGreaterThanOrEqual";
		public const string UPLOADED_BY_USER_ID_EQUAL_CURRENT = "uploadedByUserIdEqualCurrent";
		public const string STATUS_IN = "statusIn";
		public new const string ORDER_BY = "orderBy";
		#endregion

		#region Private Fields
		private string _BulkObjectNameEqual = null;
		private long _CreateDateGreaterThanOrEqual = long.MinValue;
		private bool? _UploadedByUserIdEqualCurrent = null;
		private string _StatusIn = null;
		private BulkUploadOrderBy _OrderBy = null;
		#endregion

		#region Properties
		[JsonProperty]
		public string BulkObjectNameEqual
		{
			get { return _BulkObjectNameEqual; }
			set 
			{ 
				_BulkObjectNameEqual = value;
				OnPropertyChanged("BulkObjectNameEqual");
			}
		}
		[JsonProperty]
		public long CreateDateGreaterThanOrEqual
		{
			get { return _CreateDateGreaterThanOrEqual; }
			set 
			{ 
				_CreateDateGreaterThanOrEqual = value;
				OnPropertyChanged("CreateDateGreaterThanOrEqual");
			}
		}
		[JsonProperty]
		public bool? UploadedByUserIdEqualCurrent
		{
			get { return _UploadedByUserIdEqualCurrent; }
			set 
			{ 
				_UploadedByUserIdEqualCurrent = value;
				OnPropertyChanged("UploadedByUserIdEqualCurrent");
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
			if(node["bulkObjectNameEqual"] != null)
			{
				this._BulkObjectNameEqual = node["bulkObjectNameEqual"].Value<string>();
			}
			if(node["createDateGreaterThanOrEqual"] != null)
			{
				this._CreateDateGreaterThanOrEqual = ParseLong(node["createDateGreaterThanOrEqual"].Value<string>());
			}
			if(node["uploadedByUserIdEqualCurrent"] != null)
			{
				this._UploadedByUserIdEqualCurrent = ParseBool(node["uploadedByUserIdEqualCurrent"].Value<string>());
			}
			if(node["statusIn"] != null)
			{
				this._StatusIn = node["statusIn"].Value<string>();
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
			kparams.AddIfNotNull("bulkObjectNameEqual", this._BulkObjectNameEqual);
			kparams.AddIfNotNull("createDateGreaterThanOrEqual", this._CreateDateGreaterThanOrEqual);
			kparams.AddIfNotNull("uploadedByUserIdEqualCurrent", this._UploadedByUserIdEqualCurrent);
			kparams.AddIfNotNull("statusIn", this._StatusIn);
			kparams.AddIfNotNull("orderBy", this._OrderBy);
			return kparams;
		}
		protected override string getPropertyName(string apiName)
		{
			switch(apiName)
			{
				case BULK_OBJECT_NAME_EQUAL:
					return "BulkObjectNameEqual";
				case CREATE_DATE_GREATER_THAN_OR_EQUAL:
					return "CreateDateGreaterThanOrEqual";
				case UPLOADED_BY_USER_ID_EQUAL_CURRENT:
					return "UploadedByUserIdEqualCurrent";
				case STATUS_IN:
					return "StatusIn";
				case ORDER_BY:
					return "OrderBy";
				default:
					return base.getPropertyName(apiName);
			}
		}
		#endregion
	}
}

