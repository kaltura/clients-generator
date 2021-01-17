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
	public class BulkUploadResult : ObjectBase
	{
		#region Constants
		public const string OBJECT_ID = "objectId";
		public const string INDEX = "index";
		public const string BULK_UPLOAD_ID = "bulkUploadId";
		public const string STATUS = "status";
		public const string ERRORS = "errors";
		public const string WARNINGS = "warnings";
		#endregion

		#region Private Fields
		private long _ObjectId = long.MinValue;
		private int _Index = Int32.MinValue;
		private long _BulkUploadId = long.MinValue;
		private BulkUploadResultStatus _Status = null;
		private IList<Message> _Errors;
		private IList<Message> _Warnings;
		#endregion

		#region Properties
		[JsonProperty]
		public long ObjectId
		{
			get { return _ObjectId; }
			private set 
			{ 
				_ObjectId = value;
				OnPropertyChanged("ObjectId");
			}
		}
		[JsonProperty]
		public int Index
		{
			get { return _Index; }
			private set 
			{ 
				_Index = value;
				OnPropertyChanged("Index");
			}
		}
		[JsonProperty]
		public long BulkUploadId
		{
			get { return _BulkUploadId; }
			private set 
			{ 
				_BulkUploadId = value;
				OnPropertyChanged("BulkUploadId");
			}
		}
		[JsonProperty]
		public BulkUploadResultStatus Status
		{
			get { return _Status; }
			private set 
			{ 
				_Status = value;
				OnPropertyChanged("Status");
			}
		}
		[JsonProperty]
		public IList<Message> Errors
		{
			get { return _Errors; }
			private set 
			{ 
				_Errors = value;
				OnPropertyChanged("Errors");
			}
		}
		[JsonProperty]
		public IList<Message> Warnings
		{
			get { return _Warnings; }
			private set 
			{ 
				_Warnings = value;
				OnPropertyChanged("Warnings");
			}
		}
		#endregion

		#region CTor
		public BulkUploadResult()
		{
		}

		public BulkUploadResult(JToken node) : base(node)
		{
			if(node["objectId"] != null)
			{
				this._ObjectId = ParseLong(node["objectId"].Value<string>());
			}
			if(node["index"] != null)
			{
				this._Index = ParseInt(node["index"].Value<string>());
			}
			if(node["bulkUploadId"] != null)
			{
				this._BulkUploadId = ParseLong(node["bulkUploadId"].Value<string>());
			}
			if(node["status"] != null)
			{
				this._Status = (BulkUploadResultStatus)StringEnum.Parse(typeof(BulkUploadResultStatus), node["status"].Value<string>());
			}
			if(node["errors"] != null)
			{
				this._Errors = new List<Message>();
				foreach(var arrayNode in node["errors"].Children())
				{
					this._Errors.Add(ObjectFactory.Create<Message>(arrayNode));
				}
			}
			if(node["warnings"] != null)
			{
				this._Warnings = new List<Message>();
				foreach(var arrayNode in node["warnings"].Children())
				{
					this._Warnings.Add(ObjectFactory.Create<Message>(arrayNode));
				}
			}
		}
		#endregion

		#region Methods
		public override Params ToParams(bool includeObjectType = true)
		{
			Params kparams = base.ToParams(includeObjectType);
			if (includeObjectType)
				kparams.AddReplace("objectType", "KalturaBulkUploadResult");
			kparams.AddIfNotNull("objectId", this._ObjectId);
			kparams.AddIfNotNull("index", this._Index);
			kparams.AddIfNotNull("bulkUploadId", this._BulkUploadId);
			kparams.AddIfNotNull("status", this._Status);
			kparams.AddIfNotNull("errors", this._Errors);
			kparams.AddIfNotNull("warnings", this._Warnings);
			return kparams;
		}
		protected override string getPropertyName(string apiName)
		{
			switch(apiName)
			{
				case OBJECT_ID:
					return "ObjectId";
				case INDEX:
					return "Index";
				case BULK_UPLOAD_ID:
					return "BulkUploadId";
				case STATUS:
					return "Status";
				case ERRORS:
					return "Errors";
				case WARNINGS:
					return "Warnings";
				default:
					return base.getPropertyName(apiName);
			}
		}
		#endregion
	}
}

