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
	public class AdsContext : ObjectBase
	{
		#region Constants
		public const string SOURCES = "sources";
		#endregion

		#region Private Fields
		private IList<AdsSource> _Sources;
		#endregion

		#region Properties
		public IList<AdsSource> Sources
		{
			get { return _Sources; }
			set 
			{ 
				_Sources = value;
				OnPropertyChanged("Sources");
			}
		}
		#endregion

		#region CTor
		public AdsContext()
		{
		}

		public AdsContext(XmlElement node) : base(node)
		{
			foreach (XmlElement propertyNode in node.ChildNodes)
			{
				switch (propertyNode.Name)
				{
					case "sources":
						this._Sources = new List<AdsSource>();
						foreach(XmlElement arrayNode in propertyNode.ChildNodes)
						{
							this._Sources.Add(ObjectFactory.Create<AdsSource>(arrayNode));
						}
						continue;
				}
			}
		}

		public AdsContext(IDictionary<string,object> data) : base(data)
		{
			    this._Sources = new List<AdsSource>();
			    foreach(var dataDictionary in data.TryGetValueSafe<IEnumerable<object>>("sources", new List<object>()))
			    {
			        if (dataDictionary == null) { continue; }
			        this._Sources.Add(ObjectFactory.Create<AdsSource>((IDictionary<string,object>)dataDictionary));
			    }
		}
		#endregion

		#region Methods
		public override Params ToParams(bool includeObjectType = true)
		{
			Params kparams = base.ToParams(includeObjectType);
			if (includeObjectType)
				kparams.AddReplace("objectType", "KalturaAdsContext");
			kparams.AddIfNotNull("sources", this._Sources);
			return kparams;
		}
		protected override string getPropertyName(string apiName)
		{
			switch(apiName)
			{
				case SOURCES:
					return "Sources";
				default:
					return base.getPropertyName(apiName);
			}
		}
		#endregion
	}
}

