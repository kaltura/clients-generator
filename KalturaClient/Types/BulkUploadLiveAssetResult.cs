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
	public class BulkUploadLiveAssetResult : BulkUploadResult
	{
		#region Constants
		public const string ID = "id";
		public const string EXTERNAL_EPG_INGEST_ID = "externalEpgIngestId";
		public const string PROGRAMS = "programs";
		#endregion

		#region Private Fields
		private int _Id = Int32.MinValue;
		private string _ExternalEpgIngestId = null;
		private IList<BulkUploadProgramAssetResult> _Programs;
		#endregion

		#region Properties
		[JsonProperty]
		public int Id
		{
			get { return _Id; }
			private set 
			{ 
				_Id = value;
				OnPropertyChanged("Id");
			}
		}
		[JsonProperty]
		public string ExternalEpgIngestId
		{
			get { return _ExternalEpgIngestId; }
			private set 
			{ 
				_ExternalEpgIngestId = value;
				OnPropertyChanged("ExternalEpgIngestId");
			}
		}
		[JsonProperty]
		public IList<BulkUploadProgramAssetResult> Programs
		{
			get { return _Programs; }
			private set 
			{ 
				_Programs = value;
				OnPropertyChanged("Programs");
			}
		}
		#endregion

		#region CTor
		public BulkUploadLiveAssetResult()
		{
		}

		public BulkUploadLiveAssetResult(JToken node) : base(node)
		{
			if(node["id"] != null)
			{
				this._Id = ParseInt(node["id"].Value<string>());
			}
			if(node["externalEpgIngestId"] != null)
			{
				this._ExternalEpgIngestId = node["externalEpgIngestId"].Value<string>();
			}
			if(node["programs"] != null)
			{
				this._Programs = new List<BulkUploadProgramAssetResult>();
				foreach(var arrayNode in node["programs"].Children())
				{
					this._Programs.Add(ObjectFactory.Create<BulkUploadProgramAssetResult>(arrayNode));
				}
			}
		}
		#endregion

		#region Methods
		public override Params ToParams(bool includeObjectType = true)
		{
			Params kparams = base.ToParams(includeObjectType);
			if (includeObjectType)
				kparams.AddReplace("objectType", "KalturaBulkUploadLiveAssetResult");
			kparams.AddIfNotNull("id", this._Id);
			kparams.AddIfNotNull("externalEpgIngestId", this._ExternalEpgIngestId);
			kparams.AddIfNotNull("programs", this._Programs);
			return kparams;
		}
		protected override string getPropertyName(string apiName)
		{
			switch(apiName)
			{
				case ID:
					return "Id";
				case EXTERNAL_EPG_INGEST_ID:
					return "ExternalEpgIngestId";
				case PROGRAMS:
					return "Programs";
				default:
					return base.getPropertyName(apiName);
			}
		}
		#endregion
	}
}

