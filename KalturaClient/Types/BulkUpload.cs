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
	public class BulkUpload : ObjectBase
	{
		#region Constants
		public const string ID = "id";
		public const string STATUS = "status";
		public const string CREATE_DATE = "createDate";
		public const string UPDATE_DATE = "updateDate";
		public const string UPLOAD_TOKEN_ID = "uploadTokenId";
		public const string ACTION = "action";
		public const string RESULTS = "results";
		#endregion

		#region Private Fields
		private long _Id = long.MinValue;
		private BatchUploadJobStatus _Status = null;
		private long _CreateDate = long.MinValue;
		private long _UpdateDate = long.MinValue;
		private string _UploadTokenId = null;
		private BatchUploadJobAction _Action = null;
		private IList<BulkUploadResult> _Results;
		#endregion

		#region Properties
		[JsonProperty]
		public long Id
		{
			get { return _Id; }
			private set 
			{ 
				_Id = value;
				OnPropertyChanged("Id");
			}
		}
		[JsonProperty]
		public BatchUploadJobStatus Status
		{
			get { return _Status; }
			private set 
			{ 
				_Status = value;
				OnPropertyChanged("Status");
			}
		}
		[JsonProperty]
		public long CreateDate
		{
			get { return _CreateDate; }
			private set 
			{ 
				_CreateDate = value;
				OnPropertyChanged("CreateDate");
			}
		}
		[JsonProperty]
		public long UpdateDate
		{
			get { return _UpdateDate; }
			private set 
			{ 
				_UpdateDate = value;
				OnPropertyChanged("UpdateDate");
			}
		}
		[JsonProperty]
		public string UploadTokenId
		{
			get { return _UploadTokenId; }
			private set 
			{ 
				_UploadTokenId = value;
				OnPropertyChanged("UploadTokenId");
			}
		}
		[JsonProperty]
		public BatchUploadJobAction Action
		{
			get { return _Action; }
			private set 
			{ 
				_Action = value;
				OnPropertyChanged("Action");
			}
		}
		[JsonProperty]
		public IList<BulkUploadResult> Results
		{
			get { return _Results; }
			private set 
			{ 
				_Results = value;
				OnPropertyChanged("Results");
			}
		}
		#endregion

		#region CTor
		public BulkUpload()
		{
		}

		public BulkUpload(JToken node) : base(node)
		{
			if(node["id"] != null)
			{
				this._Id = ParseLong(node["id"].Value<string>());
			}
			if(node["status"] != null)
			{
				this._Status = (BatchUploadJobStatus)StringEnum.Parse(typeof(BatchUploadJobStatus), node["status"].Value<string>());
			}
			if(node["createDate"] != null)
			{
				this._CreateDate = ParseLong(node["createDate"].Value<string>());
			}
			if(node["updateDate"] != null)
			{
				this._UpdateDate = ParseLong(node["updateDate"].Value<string>());
			}
			if(node["uploadTokenId"] != null)
			{
				this._UploadTokenId = node["uploadTokenId"].Value<string>();
			}
			if(node["action"] != null)
			{
				this._Action = (BatchUploadJobAction)StringEnum.Parse(typeof(BatchUploadJobAction), node["action"].Value<string>());
			}
			if(node["results"] != null)
			{
				this._Results = new List<BulkUploadResult>();
				foreach(var arrayNode in node["results"].Children())
				{
					this._Results.Add(ObjectFactory.Create<BulkUploadResult>(arrayNode));
				}
			}
		}
		#endregion

		#region Methods
		public override Params ToParams(bool includeObjectType = true)
		{
			Params kparams = base.ToParams(includeObjectType);
			if (includeObjectType)
				kparams.AddReplace("objectType", "KalturaBulkUpload");
			kparams.AddIfNotNull("id", this._Id);
			kparams.AddIfNotNull("status", this._Status);
			kparams.AddIfNotNull("createDate", this._CreateDate);
			kparams.AddIfNotNull("updateDate", this._UpdateDate);
			kparams.AddIfNotNull("uploadTokenId", this._UploadTokenId);
			kparams.AddIfNotNull("action", this._Action);
			kparams.AddIfNotNull("results", this._Results);
			return kparams;
		}
		protected override string getPropertyName(string apiName)
		{
			switch(apiName)
			{
				case ID:
					return "Id";
				case STATUS:
					return "Status";
				case CREATE_DATE:
					return "CreateDate";
				case UPDATE_DATE:
					return "UpdateDate";
				case UPLOAD_TOKEN_ID:
					return "UploadTokenId";
				case ACTION:
					return "Action";
				case RESULTS:
					return "Results";
				default:
					return base.getPropertyName(apiName);
			}
		}
		#endregion
	}
}

