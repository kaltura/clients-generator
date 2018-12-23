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
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Kaltura.Types
{
	public class SegmentRanges : BaseSegmentValue
	{
		#region Constants
		public const string SOURCE = "source";
		public const string RANGES = "ranges";
		#endregion

		#region Private Fields
		private SegmentSource _Source;
		private IList<SegmentRange> _Ranges;
		#endregion

		#region Properties
		[JsonProperty]
		public SegmentSource Source
		{
			get { return _Source; }
			set 
			{ 
				_Source = value;
				OnPropertyChanged("Source");
			}
		}
		[JsonProperty]
		public IList<SegmentRange> Ranges
		{
			get { return _Ranges; }
			set 
			{ 
				_Ranges = value;
				OnPropertyChanged("Ranges");
			}
		}
		#endregion

		#region CTor
		public SegmentRanges()
		{
		}

		public SegmentRanges(JToken node) : base(node)
		{
			if(node["source"] != null)
			{
				this._Source = ObjectFactory.Create<SegmentSource>(node["source"]);
			}
			if(node["ranges"] != null)
			{
				this._Ranges = new List<SegmentRange>();
				foreach(var arrayNode in node["ranges"].Children())
				{
					this._Ranges.Add(ObjectFactory.Create<SegmentRange>(arrayNode));
				}
			}
		}
		#endregion

		#region Methods
		public override Params ToParams(bool includeObjectType = true)
		{
			Params kparams = base.ToParams(includeObjectType);
			if (includeObjectType)
				kparams.AddReplace("objectType", "KalturaSegmentRanges");
			kparams.AddIfNotNull("source", this._Source);
			kparams.AddIfNotNull("ranges", this._Ranges);
			return kparams;
		}
		protected override string getPropertyName(string apiName)
		{
			switch(apiName)
			{
				case SOURCE:
					return "Source";
				case RANGES:
					return "Ranges";
				default:
					return base.getPropertyName(apiName);
			}
		}
		#endregion
	}
}

