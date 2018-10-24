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
// Copyright (C) 2006-2018  Kaltura Inc.
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

namespace Kaltura.Types
{
	public class MediaFile : AssetFile
	{
		#region Constants
		public const string ASSET_ID = "assetId";
		public const string ID = "id";
		public const string TYPE = "type";
		public const string TYPE_ID = "typeId";
		public const string DURATION = "duration";
		public const string EXTERNAL_ID = "externalId";
		public const string ALT_EXTERNAL_ID = "altExternalId";
		public const string FILE_SIZE = "fileSize";
		public const string ADDITIONAL_DATA = "additionalData";
		public const string ALT_STREAMING_CODE = "altStreamingCode";
		public const string ALTERNATIVE_CDN_ADAPATER_PROFILE_ID = "alternativeCdnAdapaterProfileId";
		public const string END_DATE = "endDate";
		public const string START_DATE = "startDate";
		public const string EXTERNAL_STORE_ID = "externalStoreId";
		public const string IS_DEFAULT_LANGUAGE = "isDefaultLanguage";
		public const string LANGUAGE = "language";
		public const string ORDER_NUM = "orderNum";
		public const string OUTPUT_PROTECATION_LEVEL = "outputProtecationLevel";
		public const string CDN_ADAPATER_PROFILE_ID = "cdnAdapaterProfileId";
		public const string STATUS = "status";
		public const string CATALOG_END_DATE = "catalogEndDate";
		#endregion

		#region Private Fields
		private int _AssetId = Int32.MinValue;
		private int _Id = Int32.MinValue;
		private string _Type = null;
		private int _TypeId = Int32.MinValue;
		private long _Duration = long.MinValue;
		private string _ExternalId = null;
		private string _AltExternalId = null;
		private long _FileSize = long.MinValue;
		private string _AdditionalData = null;
		private string _AltStreamingCode = null;
		private long _AlternativeCdnAdapaterProfileId = long.MinValue;
		private long _EndDate = long.MinValue;
		private long _StartDate = long.MinValue;
		private string _ExternalStoreId = null;
		private bool? _IsDefaultLanguage = null;
		private string _Language = null;
		private int _OrderNum = Int32.MinValue;
		private string _OutputProtecationLevel = null;
		private long _CdnAdapaterProfileId = long.MinValue;
		private bool? _Status = null;
		private long _CatalogEndDate = long.MinValue;
		#endregion

		#region Properties
		public int AssetId
		{
			get { return _AssetId; }
			set 
			{ 
				_AssetId = value;
				OnPropertyChanged("AssetId");
			}
		}
		public int Id
		{
			get { return _Id; }
		}
		public string Type
		{
			get { return _Type; }
		}
		public int TypeId
		{
			get { return _TypeId; }
			set 
			{ 
				_TypeId = value;
				OnPropertyChanged("TypeId");
			}
		}
		public long Duration
		{
			get { return _Duration; }
			set 
			{ 
				_Duration = value;
				OnPropertyChanged("Duration");
			}
		}
		public string ExternalId
		{
			get { return _ExternalId; }
			set 
			{ 
				_ExternalId = value;
				OnPropertyChanged("ExternalId");
			}
		}
		public string AltExternalId
		{
			get { return _AltExternalId; }
			set 
			{ 
				_AltExternalId = value;
				OnPropertyChanged("AltExternalId");
			}
		}
		public long FileSize
		{
			get { return _FileSize; }
			set 
			{ 
				_FileSize = value;
				OnPropertyChanged("FileSize");
			}
		}
		public string AdditionalData
		{
			get { return _AdditionalData; }
			set 
			{ 
				_AdditionalData = value;
				OnPropertyChanged("AdditionalData");
			}
		}
		public string AltStreamingCode
		{
			get { return _AltStreamingCode; }
			set 
			{ 
				_AltStreamingCode = value;
				OnPropertyChanged("AltStreamingCode");
			}
		}
		public long AlternativeCdnAdapaterProfileId
		{
			get { return _AlternativeCdnAdapaterProfileId; }
			set 
			{ 
				_AlternativeCdnAdapaterProfileId = value;
				OnPropertyChanged("AlternativeCdnAdapaterProfileId");
			}
		}
		public long EndDate
		{
			get { return _EndDate; }
			set 
			{ 
				_EndDate = value;
				OnPropertyChanged("EndDate");
			}
		}
		public long StartDate
		{
			get { return _StartDate; }
			set 
			{ 
				_StartDate = value;
				OnPropertyChanged("StartDate");
			}
		}
		public string ExternalStoreId
		{
			get { return _ExternalStoreId; }
			set 
			{ 
				_ExternalStoreId = value;
				OnPropertyChanged("ExternalStoreId");
			}
		}
		public bool? IsDefaultLanguage
		{
			get { return _IsDefaultLanguage; }
			set 
			{ 
				_IsDefaultLanguage = value;
				OnPropertyChanged("IsDefaultLanguage");
			}
		}
		public string Language
		{
			get { return _Language; }
			set 
			{ 
				_Language = value;
				OnPropertyChanged("Language");
			}
		}
		public int OrderNum
		{
			get { return _OrderNum; }
			set 
			{ 
				_OrderNum = value;
				OnPropertyChanged("OrderNum");
			}
		}
		public string OutputProtecationLevel
		{
			get { return _OutputProtecationLevel; }
			set 
			{ 
				_OutputProtecationLevel = value;
				OnPropertyChanged("OutputProtecationLevel");
			}
		}
		public long CdnAdapaterProfileId
		{
			get { return _CdnAdapaterProfileId; }
			set 
			{ 
				_CdnAdapaterProfileId = value;
				OnPropertyChanged("CdnAdapaterProfileId");
			}
		}
		public bool? Status
		{
			get { return _Status; }
			set 
			{ 
				_Status = value;
				OnPropertyChanged("Status");
			}
		}
		public long CatalogEndDate
		{
			get { return _CatalogEndDate; }
			set 
			{ 
				_CatalogEndDate = value;
				OnPropertyChanged("CatalogEndDate");
			}
		}
		#endregion

		#region CTor
		public MediaFile()
		{
		}

		public MediaFile(XmlElement node) : base(node)
		{
			foreach (XmlElement propertyNode in node.ChildNodes)
			{
				switch (propertyNode.Name)
				{
					case "assetId":
						this._AssetId = ParseInt(propertyNode.InnerText);
						continue;
					case "id":
						this._Id = ParseInt(propertyNode.InnerText);
						continue;
					case "type":
						this._Type = propertyNode.InnerText;
						continue;
					case "typeId":
						this._TypeId = ParseInt(propertyNode.InnerText);
						continue;
					case "duration":
						this._Duration = ParseLong(propertyNode.InnerText);
						continue;
					case "externalId":
						this._ExternalId = propertyNode.InnerText;
						continue;
					case "altExternalId":
						this._AltExternalId = propertyNode.InnerText;
						continue;
					case "fileSize":
						this._FileSize = ParseLong(propertyNode.InnerText);
						continue;
					case "additionalData":
						this._AdditionalData = propertyNode.InnerText;
						continue;
					case "altStreamingCode":
						this._AltStreamingCode = propertyNode.InnerText;
						continue;
					case "alternativeCdnAdapaterProfileId":
						this._AlternativeCdnAdapaterProfileId = ParseLong(propertyNode.InnerText);
						continue;
					case "endDate":
						this._EndDate = ParseLong(propertyNode.InnerText);
						continue;
					case "startDate":
						this._StartDate = ParseLong(propertyNode.InnerText);
						continue;
					case "externalStoreId":
						this._ExternalStoreId = propertyNode.InnerText;
						continue;
					case "isDefaultLanguage":
						this._IsDefaultLanguage = ParseBool(propertyNode.InnerText);
						continue;
					case "language":
						this._Language = propertyNode.InnerText;
						continue;
					case "orderNum":
						this._OrderNum = ParseInt(propertyNode.InnerText);
						continue;
					case "outputProtecationLevel":
						this._OutputProtecationLevel = propertyNode.InnerText;
						continue;
					case "cdnAdapaterProfileId":
						this._CdnAdapaterProfileId = ParseLong(propertyNode.InnerText);
						continue;
					case "status":
						this._Status = ParseBool(propertyNode.InnerText);
						continue;
					case "catalogEndDate":
						this._CatalogEndDate = ParseLong(propertyNode.InnerText);
						continue;
				}
			}
		}
		#endregion

		#region Methods
		public override Params ToParams(bool includeObjectType = true)
		{
			Params kparams = base.ToParams(includeObjectType);
			if (includeObjectType)
				kparams.AddReplace("objectType", "KalturaMediaFile");
			kparams.AddIfNotNull("assetId", this._AssetId);
			kparams.AddIfNotNull("id", this._Id);
			kparams.AddIfNotNull("type", this._Type);
			kparams.AddIfNotNull("typeId", this._TypeId);
			kparams.AddIfNotNull("duration", this._Duration);
			kparams.AddIfNotNull("externalId", this._ExternalId);
			kparams.AddIfNotNull("altExternalId", this._AltExternalId);
			kparams.AddIfNotNull("fileSize", this._FileSize);
			kparams.AddIfNotNull("additionalData", this._AdditionalData);
			kparams.AddIfNotNull("altStreamingCode", this._AltStreamingCode);
			kparams.AddIfNotNull("alternativeCdnAdapaterProfileId", this._AlternativeCdnAdapaterProfileId);
			kparams.AddIfNotNull("endDate", this._EndDate);
			kparams.AddIfNotNull("startDate", this._StartDate);
			kparams.AddIfNotNull("externalStoreId", this._ExternalStoreId);
			kparams.AddIfNotNull("isDefaultLanguage", this._IsDefaultLanguage);
			kparams.AddIfNotNull("language", this._Language);
			kparams.AddIfNotNull("orderNum", this._OrderNum);
			kparams.AddIfNotNull("outputProtecationLevel", this._OutputProtecationLevel);
			kparams.AddIfNotNull("cdnAdapaterProfileId", this._CdnAdapaterProfileId);
			kparams.AddIfNotNull("status", this._Status);
			kparams.AddIfNotNull("catalogEndDate", this._CatalogEndDate);
			return kparams;
		}
		protected override string getPropertyName(string apiName)
		{
			switch(apiName)
			{
				case ASSET_ID:
					return "AssetId";
				case ID:
					return "Id";
				case TYPE:
					return "Type";
				case TYPE_ID:
					return "TypeId";
				case DURATION:
					return "Duration";
				case EXTERNAL_ID:
					return "ExternalId";
				case ALT_EXTERNAL_ID:
					return "AltExternalId";
				case FILE_SIZE:
					return "FileSize";
				case ADDITIONAL_DATA:
					return "AdditionalData";
				case ALT_STREAMING_CODE:
					return "AltStreamingCode";
				case ALTERNATIVE_CDN_ADAPATER_PROFILE_ID:
					return "AlternativeCdnAdapaterProfileId";
				case END_DATE:
					return "EndDate";
				case START_DATE:
					return "StartDate";
				case EXTERNAL_STORE_ID:
					return "ExternalStoreId";
				case IS_DEFAULT_LANGUAGE:
					return "IsDefaultLanguage";
				case LANGUAGE:
					return "Language";
				case ORDER_NUM:
					return "OrderNum";
				case OUTPUT_PROTECATION_LEVEL:
					return "OutputProtecationLevel";
				case CDN_ADAPATER_PROFILE_ID:
					return "CdnAdapaterProfileId";
				case STATUS:
					return "Status";
				case CATALOG_END_DATE:
					return "CatalogEndDate";
				default:
					return base.getPropertyName(apiName);
			}
		}
		#endregion
	}
}

