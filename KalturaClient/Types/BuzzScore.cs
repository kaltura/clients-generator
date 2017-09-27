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
	public class BuzzScore : ObjectBase
	{
		#region Constants
		public const string NORMALIZED_AVG_SCORE = "normalizedAvgScore";
		public const string UPDATE_DATE = "updateDate";
		public const string AVG_SCORE = "avgScore";
		#endregion

		#region Private Fields
		private float _NormalizedAvgScore = Single.MinValue;
		private long _UpdateDate = long.MinValue;
		private float _AvgScore = Single.MinValue;
		#endregion

		#region Properties
		public float NormalizedAvgScore
		{
			get { return _NormalizedAvgScore; }
			set 
			{ 
				_NormalizedAvgScore = value;
				OnPropertyChanged("NormalizedAvgScore");
			}
		}
		public long UpdateDate
		{
			get { return _UpdateDate; }
			set 
			{ 
				_UpdateDate = value;
				OnPropertyChanged("UpdateDate");
			}
		}
		public float AvgScore
		{
			get { return _AvgScore; }
			set 
			{ 
				_AvgScore = value;
				OnPropertyChanged("AvgScore");
			}
		}
		#endregion

		#region CTor
		public BuzzScore()
		{
		}

		public BuzzScore(XmlElement node) : base(node)
		{
			foreach (XmlElement propertyNode in node.ChildNodes)
			{
				switch (propertyNode.Name)
				{
					case "normalizedAvgScore":
						this._NormalizedAvgScore = ParseFloat(propertyNode.InnerText);
						continue;
					case "updateDate":
						this._UpdateDate = ParseLong(propertyNode.InnerText);
						continue;
					case "avgScore":
						this._AvgScore = ParseFloat(propertyNode.InnerText);
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
				kparams.AddReplace("objectType", "KalturaBuzzScore");
			kparams.AddIfNotNull("normalizedAvgScore", this._NormalizedAvgScore);
			kparams.AddIfNotNull("updateDate", this._UpdateDate);
			kparams.AddIfNotNull("avgScore", this._AvgScore);
			return kparams;
		}
		protected override string getPropertyName(string apiName)
		{
			switch(apiName)
			{
				case NORMALIZED_AVG_SCORE:
					return "NormalizedAvgScore";
				case UPDATE_DATE:
					return "UpdateDate";
				case AVG_SCORE:
					return "AvgScore";
				default:
					return base.getPropertyName(apiName);
			}
		}
		#endregion
	}
}

