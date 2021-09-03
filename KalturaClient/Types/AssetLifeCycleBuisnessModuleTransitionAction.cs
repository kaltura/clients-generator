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
	public class AssetLifeCycleBuisnessModuleTransitionAction : AssetLifeCycleTransitionAction
	{
		#region Constants
		public const string FILE_TYPE_IDS = "fileTypeIds";
		public const string PPV_IDS = "ppvIds";
		#endregion

		#region Private Fields
		private string _FileTypeIds = null;
		private string _PpvIds = null;
		#endregion

		#region Properties
		[JsonProperty]
		public string FileTypeIds
		{
			get { return _FileTypeIds; }
			set 
			{ 
				_FileTypeIds = value;
				OnPropertyChanged("FileTypeIds");
			}
		}
		[JsonProperty]
		public string PpvIds
		{
			get { return _PpvIds; }
			set 
			{ 
				_PpvIds = value;
				OnPropertyChanged("PpvIds");
			}
		}
		#endregion

		#region CTor
		public AssetLifeCycleBuisnessModuleTransitionAction()
		{
		}

		public AssetLifeCycleBuisnessModuleTransitionAction(JToken node) : base(node)
		{
			if(node["fileTypeIds"] != null)
			{
				this._FileTypeIds = node["fileTypeIds"].Value<string>();
			}
			if(node["ppvIds"] != null)
			{
				this._PpvIds = node["ppvIds"].Value<string>();
			}
		}
		#endregion

		#region Methods
		public override Params ToParams(bool includeObjectType = true)
		{
			Params kparams = base.ToParams(includeObjectType);
			if (includeObjectType)
				kparams.AddReplace("objectType", "KalturaAssetLifeCycleBuisnessModuleTransitionAction");
			kparams.AddIfNotNull("fileTypeIds", this._FileTypeIds);
			kparams.AddIfNotNull("ppvIds", this._PpvIds);
			return kparams;
		}
		protected override string getPropertyName(string apiName)
		{
			switch(apiName)
			{
				case FILE_TYPE_IDS:
					return "FileTypeIds";
				case PPV_IDS:
					return "PpvIds";
				default:
					return base.getPropertyName(apiName);
			}
		}
		#endregion
	}
}

