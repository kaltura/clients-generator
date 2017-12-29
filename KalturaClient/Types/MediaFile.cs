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
// Copyright (C) 2006-2017  Kaltura Inc.
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
	public class MediaFile : ObjectBase
	{
		#region Constants
		public const string ASSET_ID = "assetId";
		public const string ID = "id";
		public const string TYPE = "type";
		public const string URL = "url";
		public const string DURATION = "duration";
		public const string EXTERNAL_ID = "externalId";
		public const string BILLING_TYPE = "billingType";
		public const string QUALITY = "quality";
		public const string HANDLING_TYPE = "handlingType";
		public const string CDN_NAME = "cdnName";
		public const string CDN_CODE = "cdnCode";
		public const string ALT_CDN_CODE = "altCdnCode";
		public const string PPV_MODULES = "ppvModules";
		public const string PRODUCT_CODE = "productCode";
		#endregion

		#region Private Fields
		private int _AssetId = Int32.MinValue;
		private int _Id = Int32.MinValue;
		private string _Type = null;
		private string _Url = null;
		private long _Duration = long.MinValue;
		private string _ExternalId = null;
		private string _BillingType = null;
		private string _Quality = null;
		private string _HandlingType = null;
		private string _CdnName = null;
		private string _CdnCode = null;
		private string _AltCdnCode = null;
		private StringValueArray _PpvModules;
		private string _ProductCode = null;
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
			set 
			{ 
				_Type = value;
				OnPropertyChanged("Type");
			}
		}
		public string Url
		{
			get { return _Url; }
			set 
			{ 
				_Url = value;
				OnPropertyChanged("Url");
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
		public string BillingType
		{
			get { return _BillingType; }
			set 
			{ 
				_BillingType = value;
				OnPropertyChanged("BillingType");
			}
		}
		public string Quality
		{
			get { return _Quality; }
			set 
			{ 
				_Quality = value;
				OnPropertyChanged("Quality");
			}
		}
		public string HandlingType
		{
			get { return _HandlingType; }
			set 
			{ 
				_HandlingType = value;
				OnPropertyChanged("HandlingType");
			}
		}
		public string CdnName
		{
			get { return _CdnName; }
			set 
			{ 
				_CdnName = value;
				OnPropertyChanged("CdnName");
			}
		}
		public string CdnCode
		{
			get { return _CdnCode; }
			set 
			{ 
				_CdnCode = value;
				OnPropertyChanged("CdnCode");
			}
		}
		public string AltCdnCode
		{
			get { return _AltCdnCode; }
			set 
			{ 
				_AltCdnCode = value;
				OnPropertyChanged("AltCdnCode");
			}
		}
		public StringValueArray PpvModules
		{
			get { return _PpvModules; }
			set 
			{ 
				_PpvModules = value;
				OnPropertyChanged("PpvModules");
			}
		}
		public string ProductCode
		{
			get { return _ProductCode; }
			set 
			{ 
				_ProductCode = value;
				OnPropertyChanged("ProductCode");
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
					case "url":
						this._Url = propertyNode.InnerText;
						continue;
					case "duration":
						this._Duration = ParseLong(propertyNode.InnerText);
						continue;
					case "externalId":
						this._ExternalId = propertyNode.InnerText;
						continue;
					case "billingType":
						this._BillingType = propertyNode.InnerText;
						continue;
					case "quality":
						this._Quality = propertyNode.InnerText;
						continue;
					case "handlingType":
						this._HandlingType = propertyNode.InnerText;
						continue;
					case "cdnName":
						this._CdnName = propertyNode.InnerText;
						continue;
					case "cdnCode":
						this._CdnCode = propertyNode.InnerText;
						continue;
					case "altCdnCode":
						this._AltCdnCode = propertyNode.InnerText;
						continue;
					case "ppvModules":
						this._PpvModules = ObjectFactory.Create<StringValueArray>(propertyNode);
						continue;
					case "productCode":
						this._ProductCode = propertyNode.InnerText;
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
			kparams.AddIfNotNull("url", this._Url);
			kparams.AddIfNotNull("duration", this._Duration);
			kparams.AddIfNotNull("externalId", this._ExternalId);
			kparams.AddIfNotNull("billingType", this._BillingType);
			kparams.AddIfNotNull("quality", this._Quality);
			kparams.AddIfNotNull("handlingType", this._HandlingType);
			kparams.AddIfNotNull("cdnName", this._CdnName);
			kparams.AddIfNotNull("cdnCode", this._CdnCode);
			kparams.AddIfNotNull("altCdnCode", this._AltCdnCode);
			kparams.AddIfNotNull("ppvModules", this._PpvModules);
			kparams.AddIfNotNull("productCode", this._ProductCode);
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
				case URL:
					return "Url";
				case DURATION:
					return "Duration";
				case EXTERNAL_ID:
					return "ExternalId";
				case BILLING_TYPE:
					return "BillingType";
				case QUALITY:
					return "Quality";
				case HANDLING_TYPE:
					return "HandlingType";
				case CDN_NAME:
					return "CdnName";
				case CDN_CODE:
					return "CdnCode";
				case ALT_CDN_CODE:
					return "AltCdnCode";
				case PPV_MODULES:
					return "PpvModules";
				case PRODUCT_CODE:
					return "ProductCode";
				default:
					return base.getPropertyName(apiName);
			}
		}
		#endregion
	}
}

