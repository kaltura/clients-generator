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
	public class SegmentValues : BaseSegmentValue
	{
		#region Constants
		public const string SOURCE = "source";
		public const string VALUES = "values";
		#endregion

		#region Private Fields
		private SegmentSource _Source;
		private IList<SegmentValue> _Values;
		#endregion

		#region Properties
		public SegmentSource Source
		{
			get { return _Source; }
			set 
			{ 
				_Source = value;
				OnPropertyChanged("Source");
			}
		}
		public IList<SegmentValue> Values
		{
			get { return _Values; }
			set 
			{ 
				_Values = value;
				OnPropertyChanged("Values");
			}
		}
		#endregion

		#region CTor
		public SegmentValues()
		{
		}

		public SegmentValues(XmlElement node) : base(node)
		{
			foreach (XmlElement propertyNode in node.ChildNodes)
			{
				switch (propertyNode.Name)
				{
					case "source":
						this._Source = ObjectFactory.Create<SegmentSource>(propertyNode);
						continue;
					case "values":
						this._Values = new List<SegmentValue>();
						foreach(XmlElement arrayNode in propertyNode.ChildNodes)
						{
							this._Values.Add(ObjectFactory.Create<SegmentValue>(arrayNode));
						}
						continue;
				}
			}
		}

		public SegmentValues(IDictionary<string,object> data) : base(data)
		{
			    this._Source = ObjectFactory.Create<SegmentSource>(data.TryGetValueSafe<IDictionary<string,object>>("source"));
			    this._Values = new List<SegmentValue>();
			    foreach(var dataDictionary in data.TryGetValueSafe<IEnumerable<object>>("values", new List<object>()))
			    {
			        if (dataDictionary == null) { continue; }
			        this._Values.Add(ObjectFactory.Create<SegmentValue>((IDictionary<string,object>)dataDictionary));
			    }
		}
		#endregion

		#region Methods
		public override Params ToParams(bool includeObjectType = true)
		{
			Params kparams = base.ToParams(includeObjectType);
			if (includeObjectType)
				kparams.AddReplace("objectType", "KalturaSegmentValues");
			kparams.AddIfNotNull("source", this._Source);
			kparams.AddIfNotNull("values", this._Values);
			return kparams;
		}
		protected override string getPropertyName(string apiName)
		{
			switch(apiName)
			{
				case SOURCE:
					return "Source";
				case VALUES:
					return "Values";
				default:
					return base.getPropertyName(apiName);
			}
		}
		#endregion
	}
}

