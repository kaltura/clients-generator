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
	public class ExternalRecording : Recording
	{
		#region Constants
		public const string EXTERNAL_ID = "externalId";
		public const string META_DATA = "metaData";
		public const string EXPIRY_DATE = "expiryDate";
		#endregion

		#region Private Fields
		private string _ExternalId = null;
		private IDictionary<string, StringValue> _MetaData;
		private long _ExpiryDate = long.MinValue;
		#endregion

		#region Properties
		[JsonProperty]
		public string ExternalId
		{
			get { return _ExternalId; }
			set 
			{ 
				_ExternalId = value;
				OnPropertyChanged("ExternalId");
			}
		}
		[JsonProperty]
		public IDictionary<string, StringValue> MetaData
		{
			get { return _MetaData; }
			set 
			{ 
				_MetaData = value;
				OnPropertyChanged("MetaData");
			}
		}
		[JsonProperty]
		public long ExpiryDate
		{
			get { return _ExpiryDate; }
			private set 
			{ 
				_ExpiryDate = value;
				OnPropertyChanged("ExpiryDate");
			}
		}
		#endregion

		#region CTor
		public ExternalRecording()
		{
		}

		public ExternalRecording(JToken node) : base(node)
		{
			if(node["externalId"] != null)
			{
				this._ExternalId = node["externalId"].Value<string>();
			}
			if(node["metaData"] != null)
			{
				{
					string key;
					this._MetaData = new Dictionary<string, StringValue>();
					foreach(var arrayNode in node["metaData"].Children<JProperty>())
					{
						key = arrayNode.Name;
						this._MetaData[key] = ObjectFactory.Create<StringValue>(arrayNode.Value);
					}
				}
			}
			if(node["expiryDate"] != null)
			{
				this._ExpiryDate = ParseLong(node["expiryDate"].Value<string>());
			}
		}
		#endregion

		#region Methods
		public override Params ToParams(bool includeObjectType = true)
		{
			Params kparams = base.ToParams(includeObjectType);
			if (includeObjectType)
				kparams.AddReplace("objectType", "KalturaExternalRecording");
			kparams.AddIfNotNull("externalId", this._ExternalId);
			kparams.AddIfNotNull("metaData", this._MetaData);
			kparams.AddIfNotNull("expiryDate", this._ExpiryDate);
			return kparams;
		}
		protected override string getPropertyName(string apiName)
		{
			switch(apiName)
			{
				case EXTERNAL_ID:
					return "ExternalId";
				case META_DATA:
					return "MetaData";
				case EXPIRY_DATE:
					return "ExpiryDate";
				default:
					return base.getPropertyName(apiName);
			}
		}
		#endregion
	}
}

