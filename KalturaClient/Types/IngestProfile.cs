// ===================================================================================================
//                           _  __     _ _
//                          | |/ /__ _| | |_ _  _ _ _ __ _
//                          | ' </ _` | |  _| || | '_/ _` |
//                          |_|\_\__,_|_|\__|\_,_|_| \__,_|
//
// This file is part of the Kaltura Collaborative Media Suite which allows users
// to do with audio, video, and animation what Wiki platforms allow them to do with
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
	public class IngestProfile : ObjectBase
	{
		#region Constants
		public const string ID = "id";
		public const string NAME = "name";
		public const string EXTERNAL_ID = "externalId";
		public const string ASSET_TYPE_ID = "assetTypeId";
		public const string TRANSFORMATION_ADAPTER_URL = "transformationAdapterUrl";
		public const string TRANSFORMATION_ADAPTER_SETTINGS = "transformationAdapterSettings";
		public const string TRANSFORMATION_ADAPTER_SHARED_SECRET = "transformationAdapterSharedSecret";
		public const string DEFAULT_AUTO_FILL_POLICY = "defaultAutoFillPolicy";
		public const string DEFAULT_OVERLAP_POLICY = "defaultOverlapPolicy";
		public const string OVERLAP_CHANNELS = "overlapChannels";
		#endregion

		#region Private Fields
		private int _Id = Int32.MinValue;
		private string _Name = null;
		private string _ExternalId = null;
		private int _AssetTypeId = Int32.MinValue;
		private string _TransformationAdapterUrl = null;
		private IDictionary<string, StringValue> _TransformationAdapterSettings;
		private string _TransformationAdapterSharedSecret = null;
		private IngestProfileAutofillPolicy _DefaultAutoFillPolicy = null;
		private IngestProfileOverlapPolicy _DefaultOverlapPolicy = null;
		private string _OverlapChannels = null;
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
		public string Name
		{
			get { return _Name; }
			set 
			{ 
				_Name = value;
				OnPropertyChanged("Name");
			}
		}
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
		public int AssetTypeId
		{
			get { return _AssetTypeId; }
			set 
			{ 
				_AssetTypeId = value;
				OnPropertyChanged("AssetTypeId");
			}
		}
		[JsonProperty]
		public string TransformationAdapterUrl
		{
			get { return _TransformationAdapterUrl; }
			set 
			{ 
				_TransformationAdapterUrl = value;
				OnPropertyChanged("TransformationAdapterUrl");
			}
		}
		[JsonProperty]
		public IDictionary<string, StringValue> TransformationAdapterSettings
		{
			get { return _TransformationAdapterSettings; }
			set 
			{ 
				_TransformationAdapterSettings = value;
				OnPropertyChanged("TransformationAdapterSettings");
			}
		}
		[JsonProperty]
		public string TransformationAdapterSharedSecret
		{
			get { return _TransformationAdapterSharedSecret; }
			set 
			{ 
				_TransformationAdapterSharedSecret = value;
				OnPropertyChanged("TransformationAdapterSharedSecret");
			}
		}
		[JsonProperty]
		public IngestProfileAutofillPolicy DefaultAutoFillPolicy
		{
			get { return _DefaultAutoFillPolicy; }
			set 
			{ 
				_DefaultAutoFillPolicy = value;
				OnPropertyChanged("DefaultAutoFillPolicy");
			}
		}
		[JsonProperty]
		public IngestProfileOverlapPolicy DefaultOverlapPolicy
		{
			get { return _DefaultOverlapPolicy; }
			set 
			{ 
				_DefaultOverlapPolicy = value;
				OnPropertyChanged("DefaultOverlapPolicy");
			}
		}
		[JsonProperty]
		public string OverlapChannels
		{
			get { return _OverlapChannels; }
			set 
			{ 
				_OverlapChannels = value;
				OnPropertyChanged("OverlapChannels");
			}
		}
		#endregion

		#region CTor
		public IngestProfile()
		{
		}

		public IngestProfile(JToken node) : base(node)
		{
			if(node["id"] != null)
			{
				this._Id = ParseInt(node["id"].Value<string>());
			}
			if(node["name"] != null)
			{
				this._Name = node["name"].Value<string>();
			}
			if(node["externalId"] != null)
			{
				this._ExternalId = node["externalId"].Value<string>();
			}
			if(node["assetTypeId"] != null)
			{
				this._AssetTypeId = ParseInt(node["assetTypeId"].Value<string>());
			}
			if(node["transformationAdapterUrl"] != null)
			{
				this._TransformationAdapterUrl = node["transformationAdapterUrl"].Value<string>();
			}
			if(node["transformationAdapterSettings"] != null)
			{
				{
					string key;
					this._TransformationAdapterSettings = new Dictionary<string, StringValue>();
					foreach(var arrayNode in node["transformationAdapterSettings"].Children<JProperty>())
					{
						key = arrayNode.Name;
						this._TransformationAdapterSettings[key] = ObjectFactory.Create<StringValue>(arrayNode.Value);
					}
				}
			}
			if(node["transformationAdapterSharedSecret"] != null)
			{
				this._TransformationAdapterSharedSecret = node["transformationAdapterSharedSecret"].Value<string>();
			}
			if(node["defaultAutoFillPolicy"] != null)
			{
				this._DefaultAutoFillPolicy = (IngestProfileAutofillPolicy)StringEnum.Parse(typeof(IngestProfileAutofillPolicy), node["defaultAutoFillPolicy"].Value<string>());
			}
			if(node["defaultOverlapPolicy"] != null)
			{
				this._DefaultOverlapPolicy = (IngestProfileOverlapPolicy)StringEnum.Parse(typeof(IngestProfileOverlapPolicy), node["defaultOverlapPolicy"].Value<string>());
			}
			if(node["overlapChannels"] != null)
			{
				this._OverlapChannels = node["overlapChannels"].Value<string>();
			}
		}
		#endregion

		#region Methods
		public override Params ToParams(bool includeObjectType = true)
		{
			Params kparams = base.ToParams(includeObjectType);
			if (includeObjectType)
				kparams.AddReplace("objectType", "KalturaIngestProfile");
			kparams.AddIfNotNull("id", this._Id);
			kparams.AddIfNotNull("name", this._Name);
			kparams.AddIfNotNull("externalId", this._ExternalId);
			kparams.AddIfNotNull("assetTypeId", this._AssetTypeId);
			kparams.AddIfNotNull("transformationAdapterUrl", this._TransformationAdapterUrl);
			kparams.AddIfNotNull("transformationAdapterSettings", this._TransformationAdapterSettings);
			kparams.AddIfNotNull("transformationAdapterSharedSecret", this._TransformationAdapterSharedSecret);
			kparams.AddIfNotNull("defaultAutoFillPolicy", this._DefaultAutoFillPolicy);
			kparams.AddIfNotNull("defaultOverlapPolicy", this._DefaultOverlapPolicy);
			kparams.AddIfNotNull("overlapChannels", this._OverlapChannels);
			return kparams;
		}
		protected override string getPropertyName(string apiName)
		{
			switch(apiName)
			{
				case ID:
					return "Id";
				case NAME:
					return "Name";
				case EXTERNAL_ID:
					return "ExternalId";
				case ASSET_TYPE_ID:
					return "AssetTypeId";
				case TRANSFORMATION_ADAPTER_URL:
					return "TransformationAdapterUrl";
				case TRANSFORMATION_ADAPTER_SETTINGS:
					return "TransformationAdapterSettings";
				case TRANSFORMATION_ADAPTER_SHARED_SECRET:
					return "TransformationAdapterSharedSecret";
				case DEFAULT_AUTO_FILL_POLICY:
					return "DefaultAutoFillPolicy";
				case DEFAULT_OVERLAP_POLICY:
					return "DefaultOverlapPolicy";
				case OVERLAP_CHANNELS:
					return "OverlapChannels";
				default:
					return base.getPropertyName(apiName);
			}
		}
		#endregion
	}
}

