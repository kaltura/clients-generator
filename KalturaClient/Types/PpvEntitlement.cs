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
	public class PpvEntitlement : Entitlement
	{
		#region Constants
		public const string MEDIA_FILE_ID = "mediaFileId";
		public const string MEDIA_ID = "mediaId";
		#endregion

		#region Private Fields
		private int _MediaFileId = Int32.MinValue;
		private int _MediaId = Int32.MinValue;
		#endregion

		#region Properties
		[JsonProperty]
		public int MediaFileId
		{
			get { return _MediaFileId; }
			private set 
			{ 
				_MediaFileId = value;
				OnPropertyChanged("MediaFileId");
			}
		}
		[JsonProperty]
		public int MediaId
		{
			get { return _MediaId; }
			private set 
			{ 
				_MediaId = value;
				OnPropertyChanged("MediaId");
			}
		}
		#endregion

		#region CTor
		public PpvEntitlement()
		{
		}

		public PpvEntitlement(JToken node) : base(node)
		{
			if(node["mediaFileId"] != null)
			{
				this._MediaFileId = ParseInt(node["mediaFileId"].Value<string>());
			}
			if(node["mediaId"] != null)
			{
				this._MediaId = ParseInt(node["mediaId"].Value<string>());
			}
		}
		#endregion

		#region Methods
		public override Params ToParams(bool includeObjectType = true)
		{
			Params kparams = base.ToParams(includeObjectType);
			if (includeObjectType)
				kparams.AddReplace("objectType", "KalturaPpvEntitlement");
			kparams.AddIfNotNull("mediaFileId", this._MediaFileId);
			kparams.AddIfNotNull("mediaId", this._MediaId);
			return kparams;
		}
		protected override string getPropertyName(string apiName)
		{
			switch(apiName)
			{
				case MEDIA_FILE_ID:
					return "MediaFileId";
				case MEDIA_ID:
					return "MediaId";
				default:
					return base.getPropertyName(apiName);
			}
		}
		#endregion
	}
}

