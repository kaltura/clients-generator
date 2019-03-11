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
		public const string FILE_OBJECT_NAME_EQUAL = "fileObjectNameEqual";
		public const string CREATE_DATE_GREATER_THAN_OR_EQUAL = "createDateGreaterThanOrEqual";
		public const string USER_ID_EQUAL_CURRENT = "userIdEqualCurrent";
		public const string SHOULD_GET_ON_GOING_BULK_UPLOADS = "shouldGetOnGoingBulkUploads";
		public new const string ORDER_BY = "orderBy";
		#endregion

		#region Private Fields
		private string _FileObjectNameEqual = null;
		private long _CreateDateGreaterThanOrEqual = long.MinValue;
		private bool? _UserIdEqualCurrent = null;
		private bool? _ShouldGetOnGoingBulkUploads = null;
		private BulkUploadOrderBy _OrderBy = null;
		#endregion

		#region Properties
		[JsonProperty]
		public string FileObjectNameEqual
		{
			get { return _FileObjectNameEqual; }
			set 
			{ 
				_FileObjectNameEqual = value;
				OnPropertyChanged("FileObjectNameEqual");
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
		public bool? ShouldGetOnGoingBulkUploads
		{
			get { return _ShouldGetOnGoingBulkUploads; }
			set 
			{ 
				_ShouldGetOnGoingBulkUploads = value;
				OnPropertyChanged("ShouldGetOnGoingBulkUploads");
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
			if(node["fileObjectNameEqual"] != null)
			{
				this._FileObjectNameEqual = node["fileObjectNameEqual"].Value<string>();
			}
			if(node["createDateGreaterThanOrEqual"] != null)
			{
				this._CreateDateGreaterThanOrEqual = ParseLong(node["createDateGreaterThanOrEqual"].Value<string>());
			}
			if(node["userIdEqualCurrent"] != null)
			{
				this._UserIdEqualCurrent = ParseBool(node["userIdEqualCurrent"].Value<string>());
			}
			if(node["shouldGetOnGoingBulkUploads"] != null)
			{
				this._ShouldGetOnGoingBulkUploads = ParseBool(node["shouldGetOnGoingBulkUploads"].Value<string>());
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
			kparams.AddIfNotNull("fileObjectNameEqual", this._FileObjectNameEqual);
			kparams.AddIfNotNull("createDateGreaterThanOrEqual", this._CreateDateGreaterThanOrEqual);
			kparams.AddIfNotNull("userIdEqualCurrent", this._UserIdEqualCurrent);
			kparams.AddIfNotNull("shouldGetOnGoingBulkUploads", this._ShouldGetOnGoingBulkUploads);
			kparams.AddIfNotNull("orderBy", this._OrderBy);
			return kparams;
		}
		protected override string getPropertyName(string apiName)
		{
			switch(apiName)
			{
				case FILE_OBJECT_NAME_EQUAL:
					return "FileObjectNameEqual";
				case CREATE_DATE_GREATER_THAN_OR_EQUAL:
					return "CreateDateGreaterThanOrEqual";
				case USER_ID_EQUAL_CURRENT:
					return "UserIdEqualCurrent";
				case SHOULD_GET_ON_GOING_BULK_UPLOADS:
					return "ShouldGetOnGoingBulkUploads";
				case ORDER_BY:
					return "OrderBy";
				default:
					return base.getPropertyName(apiName);
			}
		}
		#endregion
	}
}

