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
	public class BulkUploadProgramAssetResult : BulkUploadResult
	{
		#region Constants
		public const string PROGRAM_ID = "programId";
		public const string PROGRAM_EXTERNAL_ID = "programExternalId";
		public const string LIVE_ASSET_ID = "liveAssetId";
		#endregion

		#region Private Fields
		private int _ProgramId = Int32.MinValue;
		private string _ProgramExternalId = null;
		private int _LiveAssetId = Int32.MinValue;
		#endregion

		#region Properties
		[JsonProperty]
		public int ProgramId
		{
			get { return _ProgramId; }
			private set 
			{ 
				_ProgramId = value;
				OnPropertyChanged("ProgramId");
			}
		}
		[JsonProperty]
		public string ProgramExternalId
		{
			get { return _ProgramExternalId; }
			private set 
			{ 
				_ProgramExternalId = value;
				OnPropertyChanged("ProgramExternalId");
			}
		}
		[JsonProperty]
		public int LiveAssetId
		{
			get { return _LiveAssetId; }
			private set 
			{ 
				_LiveAssetId = value;
				OnPropertyChanged("LiveAssetId");
			}
		}
		#endregion

		#region CTor
		public BulkUploadProgramAssetResult()
		{
		}

		public BulkUploadProgramAssetResult(JToken node) : base(node)
		{
			if(node["programId"] != null)
			{
				this._ProgramId = ParseInt(node["programId"].Value<string>());
			}
			if(node["programExternalId"] != null)
			{
				this._ProgramExternalId = node["programExternalId"].Value<string>();
			}
			if(node["liveAssetId"] != null)
			{
				this._LiveAssetId = ParseInt(node["liveAssetId"].Value<string>());
			}
		}
		#endregion

		#region Methods
		public override Params ToParams(bool includeObjectType = true)
		{
			Params kparams = base.ToParams(includeObjectType);
			if (includeObjectType)
				kparams.AddReplace("objectType", "KalturaBulkUploadProgramAssetResult");
			kparams.AddIfNotNull("programId", this._ProgramId);
			kparams.AddIfNotNull("programExternalId", this._ProgramExternalId);
			kparams.AddIfNotNull("liveAssetId", this._LiveAssetId);
			return kparams;
		}
		protected override string getPropertyName(string apiName)
		{
			switch(apiName)
			{
				case PROGRAM_ID:
					return "ProgramId";
				case PROGRAM_EXTERNAL_ID:
					return "ProgramExternalId";
				case LIVE_ASSET_ID:
					return "LiveAssetId";
				default:
					return base.getPropertyName(apiName);
			}
		}
		#endregion
	}
}

