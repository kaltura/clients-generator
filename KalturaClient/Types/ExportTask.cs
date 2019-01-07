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
	public class ExportTask : ObjectBase
	{
		#region Constants
		public const string ID = "id";
		public const string ALIAS = "alias";
		public const string NAME = "name";
		public const string DATA_TYPE = "dataType";
		public const string FILTER = "filter";
		public const string EXPORT_TYPE = "exportType";
		public const string FREQUENCY = "frequency";
		public const string NOTIFICATION_URL = "notificationUrl";
		public const string VOD_TYPES = "vodTypes";
		public const string IS_ACTIVE = "isActive";
		#endregion

		#region Private Fields
		private long _Id = long.MinValue;
		private string _Alias = null;
		private string _Name = null;
		private ExportDataType _DataType = null;
		private string _Filter = null;
		private ExportType _ExportType = null;
		private long _Frequency = long.MinValue;
		private string _NotificationUrl = null;
		private IList<IntegerValue> _VodTypes;
		private bool? _IsActive = null;
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
		public string Alias
		{
			get { return _Alias; }
			set 
			{ 
				_Alias = value;
				OnPropertyChanged("Alias");
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
		public ExportDataType DataType
		{
			get { return _DataType; }
			set 
			{ 
				_DataType = value;
				OnPropertyChanged("DataType");
			}
		}
		[JsonProperty]
		public string Filter
		{
			get { return _Filter; }
			set 
			{ 
				_Filter = value;
				OnPropertyChanged("Filter");
			}
		}
		[JsonProperty]
		public ExportType ExportType
		{
			get { return _ExportType; }
			set 
			{ 
				_ExportType = value;
				OnPropertyChanged("ExportType");
			}
		}
		[JsonProperty]
		public long Frequency
		{
			get { return _Frequency; }
			set 
			{ 
				_Frequency = value;
				OnPropertyChanged("Frequency");
			}
		}
		[JsonProperty]
		public string NotificationUrl
		{
			get { return _NotificationUrl; }
			set 
			{ 
				_NotificationUrl = value;
				OnPropertyChanged("NotificationUrl");
			}
		}
		[JsonProperty]
		public IList<IntegerValue> VodTypes
		{
			get { return _VodTypes; }
			set 
			{ 
				_VodTypes = value;
				OnPropertyChanged("VodTypes");
			}
		}
		[JsonProperty]
		public bool? IsActive
		{
			get { return _IsActive; }
			set 
			{ 
				_IsActive = value;
				OnPropertyChanged("IsActive");
			}
		}
		#endregion

		#region CTor
		public ExportTask()
		{
		}

		public ExportTask(JToken node) : base(node)
		{
			if(node["id"] != null)
			{
				this._Id = ParseLong(node["id"].Value<string>());
			}
			if(node["alias"] != null)
			{
				this._Alias = node["alias"].Value<string>();
			}
			if(node["name"] != null)
			{
				this._Name = node["name"].Value<string>();
			}
			if(node["dataType"] != null)
			{
				this._DataType = (ExportDataType)StringEnum.Parse(typeof(ExportDataType), node["dataType"].Value<string>());
			}
			if(node["filter"] != null)
			{
				this._Filter = node["filter"].Value<string>();
			}
			if(node["exportType"] != null)
			{
				this._ExportType = (ExportType)StringEnum.Parse(typeof(ExportType), node["exportType"].Value<string>());
			}
			if(node["frequency"] != null)
			{
				this._Frequency = ParseLong(node["frequency"].Value<string>());
			}
			if(node["notificationUrl"] != null)
			{
				this._NotificationUrl = node["notificationUrl"].Value<string>();
			}
			if(node["vodTypes"] != null)
			{
				this._VodTypes = new List<IntegerValue>();
				foreach(var arrayNode in node["vodTypes"].Children())
				{
					this._VodTypes.Add(ObjectFactory.Create<IntegerValue>(arrayNode));
				}
			}
			if(node["isActive"] != null)
			{
				this._IsActive = ParseBool(node["isActive"].Value<string>());
			}
		}
		#endregion

		#region Methods
		public override Params ToParams(bool includeObjectType = true)
		{
			Params kparams = base.ToParams(includeObjectType);
			if (includeObjectType)
				kparams.AddReplace("objectType", "KalturaExportTask");
			kparams.AddIfNotNull("id", this._Id);
			kparams.AddIfNotNull("alias", this._Alias);
			kparams.AddIfNotNull("name", this._Name);
			kparams.AddIfNotNull("dataType", this._DataType);
			kparams.AddIfNotNull("filter", this._Filter);
			kparams.AddIfNotNull("exportType", this._ExportType);
			kparams.AddIfNotNull("frequency", this._Frequency);
			kparams.AddIfNotNull("notificationUrl", this._NotificationUrl);
			kparams.AddIfNotNull("vodTypes", this._VodTypes);
			kparams.AddIfNotNull("isActive", this._IsActive);
			return kparams;
		}
		protected override string getPropertyName(string apiName)
		{
			switch(apiName)
			{
				case ID:
					return "Id";
				case ALIAS:
					return "Alias";
				case NAME:
					return "Name";
				case DATA_TYPE:
					return "DataType";
				case FILTER:
					return "Filter";
				case EXPORT_TYPE:
					return "ExportType";
				case FREQUENCY:
					return "Frequency";
				case NOTIFICATION_URL:
					return "NotificationUrl";
				case VOD_TYPES:
					return "VodTypes";
				case IS_ACTIVE:
					return "IsActive";
				default:
					return base.getPropertyName(apiName);
			}
		}
		#endregion
	}
}

