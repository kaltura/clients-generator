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
	public class AssetsCount : ObjectBase
	{
		#region Constants
		public const string FIELD = "field";
		public const string OBJECTS = "objects";
		#endregion

		#region Private Fields
		private string _Field = null;
		private IList<AssetCount> _Objects;
		#endregion

		#region Properties
		public string Field
		{
			get { return _Field; }
			set 
			{ 
				_Field = value;
				OnPropertyChanged("Field");
			}
		}
		public IList<AssetCount> Objects
		{
			get { return _Objects; }
			set 
			{ 
				_Objects = value;
				OnPropertyChanged("Objects");
			}
		}
		#endregion

		#region CTor
		public AssetsCount()
		{
		}

		public AssetsCount(XmlElement node) : base(node)
		{
			foreach (XmlElement propertyNode in node.ChildNodes)
			{
				switch (propertyNode.Name)
				{
					case "field":
						this._Field = propertyNode.InnerText;
						continue;
					case "objects":
						this._Objects = new List<AssetCount>();
						foreach(XmlElement arrayNode in propertyNode.ChildNodes)
						{
							this._Objects.Add(ObjectFactory.Create<AssetCount>(arrayNode));
						}
						continue;
				}
			}
		}

		public AssetsCount(IDictionary<string,object> data) : base(data)
		{
			    this._Field = data.TryGetValueSafe<string>("field");
			    this._Objects = new List<AssetCount>();
			    foreach(var dataDictionary in data.TryGetValueSafe<IEnumerable<object>>("objects", new List<object>()))
			    {
			        if (dataDictionary == null) { continue; }
			        this._Objects.Add(ObjectFactory.Create<AssetCount>((IDictionary<string,object>)dataDictionary));
			    }
		}
		#endregion

		#region Methods
		public override Params ToParams(bool includeObjectType = true)
		{
			Params kparams = base.ToParams(includeObjectType);
			if (includeObjectType)
				kparams.AddReplace("objectType", "KalturaAssetsCount");
			kparams.AddIfNotNull("field", this._Field);
			kparams.AddIfNotNull("objects", this._Objects);
			return kparams;
		}
		protected override string getPropertyName(string apiName)
		{
			switch(apiName)
			{
				case FIELD:
					return "Field";
				case OBJECTS:
					return "Objects";
				default:
					return base.getPropertyName(apiName);
			}
		}
		#endregion
	}
}

