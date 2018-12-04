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
	public class BusinessModuleRuleFilter : Filter
	{
		#region Constants
		public const string BUSINESS_MODULE_TYPE_APPLIED = "businessModuleTypeApplied";
		public const string BUSINESS_MODULE_ID_APPLIED = "businessModuleIdApplied";
		public const string SEGMENT_IDS_APPLIED = "segmentIdsApplied";
		#endregion

		#region Private Fields
		private TransactionType _BusinessModuleTypeApplied = null;
		private long _BusinessModuleIdApplied = long.MinValue;
		private string _SegmentIdsApplied = null;
		#endregion

		#region Properties
		public TransactionType BusinessModuleTypeApplied
		{
			get { return _BusinessModuleTypeApplied; }
			set 
			{ 
				_BusinessModuleTypeApplied = value;
				OnPropertyChanged("BusinessModuleTypeApplied");
			}
		}
		public long BusinessModuleIdApplied
		{
			get { return _BusinessModuleIdApplied; }
			set 
			{ 
				_BusinessModuleIdApplied = value;
				OnPropertyChanged("BusinessModuleIdApplied");
			}
		}
		public string SegmentIdsApplied
		{
			get { return _SegmentIdsApplied; }
			set 
			{ 
				_SegmentIdsApplied = value;
				OnPropertyChanged("SegmentIdsApplied");
			}
		}
		#endregion

		#region CTor
		public BusinessModuleRuleFilter()
		{
		}

		public BusinessModuleRuleFilter(XmlElement node) : base(node)
		{
			foreach (XmlElement propertyNode in node.ChildNodes)
			{
				switch (propertyNode.Name)
				{
					case "businessModuleTypeApplied":
						this._BusinessModuleTypeApplied = (TransactionType)StringEnum.Parse(typeof(TransactionType), propertyNode.InnerText);
						continue;
					case "businessModuleIdApplied":
						this._BusinessModuleIdApplied = ParseLong(propertyNode.InnerText);
						continue;
					case "segmentIdsApplied":
						this._SegmentIdsApplied = propertyNode.InnerText;
						continue;
				}
			}
		}

		public BusinessModuleRuleFilter(IDictionary<string,object> data) : base(data)
		{
			    this._BusinessModuleTypeApplied = (TransactionType)StringEnum.Parse(typeof(TransactionType), data.TryGetValueSafe<string>("businessModuleTypeApplied"));
			    this._BusinessModuleIdApplied = data.TryGetValueSafe<long>("businessModuleIdApplied");
			    this._SegmentIdsApplied = data.TryGetValueSafe<string>("segmentIdsApplied");
		}
		#endregion

		#region Methods
		public override Params ToParams(bool includeObjectType = true)
		{
			Params kparams = base.ToParams(includeObjectType);
			if (includeObjectType)
				kparams.AddReplace("objectType", "KalturaBusinessModuleRuleFilter");
			kparams.AddIfNotNull("businessModuleTypeApplied", this._BusinessModuleTypeApplied);
			kparams.AddIfNotNull("businessModuleIdApplied", this._BusinessModuleIdApplied);
			kparams.AddIfNotNull("segmentIdsApplied", this._SegmentIdsApplied);
			return kparams;
		}
		protected override string getPropertyName(string apiName)
		{
			switch(apiName)
			{
				case BUSINESS_MODULE_TYPE_APPLIED:
					return "BusinessModuleTypeApplied";
				case BUSINESS_MODULE_ID_APPLIED:
					return "BusinessModuleIdApplied";
				case SEGMENT_IDS_APPLIED:
					return "SegmentIdsApplied";
				default:
					return base.getPropertyName(apiName);
			}
		}
		#endregion
	}
}

