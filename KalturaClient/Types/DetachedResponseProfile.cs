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
	public class DetachedResponseProfile : BaseResponseProfile
	{
		#region Constants
		public const string NAME = "name";
		public const string FILTER = "filter";
		public const string RELATED_PROFILES = "relatedProfiles";
		#endregion

		#region Private Fields
		private string _Name = null;
		private RelatedObjectFilter _Filter;
		private IList<DetachedResponseProfile> _RelatedProfiles;
		#endregion

		#region Properties
		public string Name
		{
			get { return _Name; }
			set 
			{ 
				_Name = value;
				OnPropertyChanged("Name");
			}
		}
		public RelatedObjectFilter Filter
		{
			get { return _Filter; }
			set 
			{ 
				_Filter = value;
				OnPropertyChanged("Filter");
			}
		}
		public IList<DetachedResponseProfile> RelatedProfiles
		{
			get { return _RelatedProfiles; }
			set 
			{ 
				_RelatedProfiles = value;
				OnPropertyChanged("RelatedProfiles");
			}
		}
		#endregion

		#region CTor
		public DetachedResponseProfile()
		{
		}

		public DetachedResponseProfile(XmlElement node) : base(node)
		{
			foreach (XmlElement propertyNode in node.ChildNodes)
			{
				switch (propertyNode.Name)
				{
					case "name":
						this._Name = propertyNode.InnerText;
						continue;
					case "filter":
						this._Filter = ObjectFactory.Create<RelatedObjectFilter>(propertyNode);
						continue;
					case "relatedProfiles":
						this._RelatedProfiles = new List<DetachedResponseProfile>();
						foreach(XmlElement arrayNode in propertyNode.ChildNodes)
						{
							this._RelatedProfiles.Add(ObjectFactory.Create<DetachedResponseProfile>(arrayNode));
						}
						continue;
				}
			}
		}

		public DetachedResponseProfile(IDictionary<string,object> data) : base(data)
		{
			    this._Name = data.TryGetValueSafe<string>("name");
			    this._Filter = ObjectFactory.Create<RelatedObjectFilter>(data.TryGetValueSafe<IDictionary<string,object>>("filter"));
			    this._RelatedProfiles = new List<DetachedResponseProfile>();
			    foreach(var dataDictionary in data.TryGetValueSafe<IEnumerable<object>>("relatedProfiles", new List<object>()))
			    {
			        if (dataDictionary == null) { continue; }
			        this._RelatedProfiles.Add(ObjectFactory.Create<DetachedResponseProfile>((IDictionary<string,object>)dataDictionary));
			    }
		}
		#endregion

		#region Methods
		public override Params ToParams(bool includeObjectType = true)
		{
			Params kparams = base.ToParams(includeObjectType);
			if (includeObjectType)
				kparams.AddReplace("objectType", "KalturaDetachedResponseProfile");
			kparams.AddIfNotNull("name", this._Name);
			kparams.AddIfNotNull("filter", this._Filter);
			kparams.AddIfNotNull("relatedProfiles", this._RelatedProfiles);
			return kparams;
		}
		protected override string getPropertyName(string apiName)
		{
			switch(apiName)
			{
				case NAME:
					return "Name";
				case FILTER:
					return "Filter";
				case RELATED_PROFILES:
					return "RelatedProfiles";
				default:
					return base.getPropertyName(apiName);
			}
		}
		#endregion
	}
}

