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
	public class SegmentRange : ObjectBase
	{
		#region Constants
		public const string NAME = "name";
		public const string MULTILINGUAL_NAME = "multilingualName";
		public const string GTE = "gte";
		public const string GT = "gt";
		public const string LTE = "lte";
		public const string LT = "lt";
		#endregion

		#region Private Fields
		private string _Name = null;
		private IList<TranslationToken> _MultilingualName;
		private float _Gte = Single.MinValue;
		private float _Gt = Single.MinValue;
		private float _Lte = Single.MinValue;
		private float _Lt = Single.MinValue;
		#endregion

		#region Properties
		public string Name
		{
			get { return _Name; }
		}
		public IList<TranslationToken> MultilingualName
		{
			get { return _MultilingualName; }
			set 
			{ 
				_MultilingualName = value;
				OnPropertyChanged("MultilingualName");
			}
		}
		public float Gte
		{
			get { return _Gte; }
			set 
			{ 
				_Gte = value;
				OnPropertyChanged("Gte");
			}
		}
		public float Gt
		{
			get { return _Gt; }
			set 
			{ 
				_Gt = value;
				OnPropertyChanged("Gt");
			}
		}
		public float Lte
		{
			get { return _Lte; }
			set 
			{ 
				_Lte = value;
				OnPropertyChanged("Lte");
			}
		}
		public float Lt
		{
			get { return _Lt; }
			set 
			{ 
				_Lt = value;
				OnPropertyChanged("Lt");
			}
		}
		#endregion

		#region CTor
		public SegmentRange()
		{
		}

		public SegmentRange(XmlElement node) : base(node)
		{
			foreach (XmlElement propertyNode in node.ChildNodes)
			{
				switch (propertyNode.Name)
				{
					case "name":
						this._Name = propertyNode.InnerText;
						continue;
					case "multilingualName":
						this._MultilingualName = new List<TranslationToken>();
						foreach(XmlElement arrayNode in propertyNode.ChildNodes)
						{
							this._MultilingualName.Add(ObjectFactory.Create<TranslationToken>(arrayNode));
						}
						continue;
					case "gte":
						this._Gte = ParseFloat(propertyNode.InnerText);
						continue;
					case "gt":
						this._Gt = ParseFloat(propertyNode.InnerText);
						continue;
					case "lte":
						this._Lte = ParseFloat(propertyNode.InnerText);
						continue;
					case "lt":
						this._Lt = ParseFloat(propertyNode.InnerText);
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
				kparams.AddReplace("objectType", "KalturaSegmentRange");
			kparams.AddIfNotNull("name", this._Name);
			kparams.AddIfNotNull("multilingualName", this._MultilingualName);
			kparams.AddIfNotNull("gte", this._Gte);
			kparams.AddIfNotNull("gt", this._Gt);
			kparams.AddIfNotNull("lte", this._Lte);
			kparams.AddIfNotNull("lt", this._Lt);
			return kparams;
		}
		protected override string getPropertyName(string apiName)
		{
			switch(apiName)
			{
				case NAME:
					return "Name";
				case MULTILINGUAL_NAME:
					return "MultilingualName";
				case GTE:
					return "Gte";
				case GT:
					return "Gt";
				case LTE:
					return "Lte";
				case LT:
					return "Lt";
				default:
					return base.getPropertyName(apiName);
			}
		}
		#endregion
	}
}

