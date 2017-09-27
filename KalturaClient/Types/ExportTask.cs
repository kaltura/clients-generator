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
		public long Id
		{
			get { return _Id; }
		}
		public string Alias
		{
			get { return _Alias; }
			set 
			{ 
				_Alias = value;
				OnPropertyChanged("Alias");
			}
		}
		public string Name
		{
			get { return _Name; }
			set 
			{ 
				_Name = value;
				OnPropertyChanged("Name");
			}
		}
		public ExportDataType DataType
		{
			get { return _DataType; }
			set 
			{ 
				_DataType = value;
				OnPropertyChanged("DataType");
			}
		}
		public string Filter
		{
			get { return _Filter; }
			set 
			{ 
				_Filter = value;
				OnPropertyChanged("Filter");
			}
		}
		public ExportType ExportType
		{
			get { return _ExportType; }
			set 
			{ 
				_ExportType = value;
				OnPropertyChanged("ExportType");
			}
		}
		public long Frequency
		{
			get { return _Frequency; }
			set 
			{ 
				_Frequency = value;
				OnPropertyChanged("Frequency");
			}
		}
		public string NotificationUrl
		{
			get { return _NotificationUrl; }
			set 
			{ 
				_NotificationUrl = value;
				OnPropertyChanged("NotificationUrl");
			}
		}
		public IList<IntegerValue> VodTypes
		{
			get { return _VodTypes; }
			set 
			{ 
				_VodTypes = value;
				OnPropertyChanged("VodTypes");
			}
		}
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

		public ExportTask(XmlElement node) : base(node)
		{
			foreach (XmlElement propertyNode in node.ChildNodes)
			{
				switch (propertyNode.Name)
				{
					case "id":
						this._Id = ParseLong(propertyNode.InnerText);
						continue;
					case "alias":
						this._Alias = propertyNode.InnerText;
						continue;
					case "name":
						this._Name = propertyNode.InnerText;
						continue;
					case "dataType":
						this._DataType = (ExportDataType)StringEnum.Parse(typeof(ExportDataType), propertyNode.InnerText);
						continue;
					case "filter":
						this._Filter = propertyNode.InnerText;
						continue;
					case "exportType":
						this._ExportType = (ExportType)StringEnum.Parse(typeof(ExportType), propertyNode.InnerText);
						continue;
					case "frequency":
						this._Frequency = ParseLong(propertyNode.InnerText);
						continue;
					case "notificationUrl":
						this._NotificationUrl = propertyNode.InnerText;
						continue;
					case "vodTypes":
						this._VodTypes = new List<IntegerValue>();
						foreach(XmlElement arrayNode in propertyNode.ChildNodes)
						{
							this._VodTypes.Add(ObjectFactory.Create<IntegerValue>(arrayNode));
						}
						continue;
					case "isActive":
						this._IsActive = ParseBool(propertyNode.InnerText);
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

