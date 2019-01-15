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
	public class RecordingFilter : Filter
	{
		#region Constants
		public const string STATUS_IN = "statusIn";
		public const string EXTERNAL_RECORDING_ID_IN = "externalRecordingIdIn";
		public const string KSQL = "kSql";
		public new const string ORDER_BY = "orderBy";
		#endregion

		#region Private Fields
		private string _StatusIn = null;
		private string _ExternalRecordingIdIn = null;
		private string _KSql = null;
		private RecordingOrderBy _OrderBy = null;
		#endregion

		#region Properties
		public string StatusIn
		{
			get { return _StatusIn; }
			set 
			{ 
				_StatusIn = value;
				OnPropertyChanged("StatusIn");
			}
		}
		public string ExternalRecordingIdIn
		{
			get { return _ExternalRecordingIdIn; }
			set 
			{ 
				_ExternalRecordingIdIn = value;
				OnPropertyChanged("ExternalRecordingIdIn");
			}
		}
		public string KSql
		{
			get { return _KSql; }
			set 
			{ 
				_KSql = value;
				OnPropertyChanged("KSql");
			}
		}
		public new RecordingOrderBy OrderBy
		{
			get { return _OrderBy; }
			set 
			{ 
				_OrderBy = value;
				OnPropertyChanged("OrderBy");
			}
		}
		#endregion

		#region CTor
		public RecordingFilter()
		{
		}

		public RecordingFilter(XmlElement node) : base(node)
		{
			foreach (XmlElement propertyNode in node.ChildNodes)
			{
				switch (propertyNode.Name)
				{
					case "statusIn":
						this._StatusIn = propertyNode.InnerText;
						continue;
					case "externalRecordingIdIn":
						this._ExternalRecordingIdIn = propertyNode.InnerText;
						continue;
					case "kSql":
						this._KSql = propertyNode.InnerText;
						continue;
					case "orderBy":
						this._OrderBy = (RecordingOrderBy)StringEnum.Parse(typeof(RecordingOrderBy), propertyNode.InnerText);
						continue;
				}
			}
		}

		public RecordingFilter(IDictionary<string,object> data) : base(data)
		{
			    this._StatusIn = data.TryGetValueSafe<string>("statusIn");
			    this._ExternalRecordingIdIn = data.TryGetValueSafe<string>("externalRecordingIdIn");
			    this._KSql = data.TryGetValueSafe<string>("kSql");
			    this._OrderBy = (RecordingOrderBy)StringEnum.Parse(typeof(RecordingOrderBy), data.TryGetValueSafe<string>("orderBy"));
		}
		#endregion

		#region Methods
		public override Params ToParams(bool includeObjectType = true)
		{
			Params kparams = base.ToParams(includeObjectType);
			if (includeObjectType)
				kparams.AddReplace("objectType", "KalturaRecordingFilter");
			kparams.AddIfNotNull("statusIn", this._StatusIn);
			kparams.AddIfNotNull("externalRecordingIdIn", this._ExternalRecordingIdIn);
			kparams.AddIfNotNull("kSql", this._KSql);
			kparams.AddIfNotNull("orderBy", this._OrderBy);
			return kparams;
		}
		protected override string getPropertyName(string apiName)
		{
			switch(apiName)
			{
				case STATUS_IN:
					return "StatusIn";
				case EXTERNAL_RECORDING_ID_IN:
					return "ExternalRecordingIdIn";
				case KSQL:
					return "KSql";
				case ORDER_BY:
					return "OrderBy";
				default:
					return base.getPropertyName(apiName);
			}
		}
		#endregion
	}
}

