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
	public class UserSegment : ObjectBase
	{
		#region Constants
		public const string SEGMENT_ID = "segmentId";
		public const string USER_ID = "userId";
		#endregion

		#region Private Fields
		private long _SegmentId = long.MinValue;
		private string _UserId = null;
		#endregion

		#region Properties
		public long SegmentId
		{
			get { return _SegmentId; }
			set 
			{ 
				_SegmentId = value;
				OnPropertyChanged("SegmentId");
			}
		}
		public string UserId
		{
			set 
			{ 
				_UserId = value;
				OnPropertyChanged("UserId");
			}
		}
		#endregion

		#region CTor
		public UserSegment()
		{
		}

		public UserSegment(XmlElement node) : base(node)
		{
			foreach (XmlElement propertyNode in node.ChildNodes)
			{
				switch (propertyNode.Name)
				{
					case "segmentId":
						this._SegmentId = ParseLong(propertyNode.InnerText);
						continue;
					case "userId":
						this._UserId = propertyNode.InnerText;
						continue;
				}
			}
		}

		public UserSegment(IDictionary<string,object> data) : base(data)
		{
			    this._SegmentId = data.TryGetValueSafe<long>("segmentId");
			    this._UserId = data.TryGetValueSafe<string>("userId");
		}
		#endregion

		#region Methods
		public override Params ToParams(bool includeObjectType = true)
		{
			Params kparams = base.ToParams(includeObjectType);
			if (includeObjectType)
				kparams.AddReplace("objectType", "KalturaUserSegment");
			kparams.AddIfNotNull("segmentId", this._SegmentId);
			kparams.AddIfNotNull("userId", this._UserId);
			return kparams;
		}
		protected override string getPropertyName(string apiName)
		{
			switch(apiName)
			{
				case SEGMENT_ID:
					return "SegmentId";
				case USER_ID:
					return "UserId";
				default:
					return base.getPropertyName(apiName);
			}
		}
		#endregion
	}
}

