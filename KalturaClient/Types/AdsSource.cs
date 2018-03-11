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
	public class AdsSource : ObjectBase
	{
		#region Constants
		public const string ID = "id";
		public const string TYPE = "type";
		public const string ADS_POLICY = "adsPolicy";
		public const string ADS_PARAM = "adsParam";
		#endregion

		#region Private Fields
		private int _Id = Int32.MinValue;
		private string _Type = null;
		private AdsPolicy _AdsPolicy = null;
		private string _AdsParam = null;
		#endregion

		#region Properties
		public int Id
		{
			get { return _Id; }
		}
		public string Type
		{
			get { return _Type; }
			set 
			{ 
				_Type = value;
				OnPropertyChanged("Type");
			}
		}
		public AdsPolicy AdsPolicy
		{
			get { return _AdsPolicy; }
			set 
			{ 
				_AdsPolicy = value;
				OnPropertyChanged("AdsPolicy");
			}
		}
		public string AdsParam
		{
			get { return _AdsParam; }
			set 
			{ 
				_AdsParam = value;
				OnPropertyChanged("AdsParam");
			}
		}
		#endregion

		#region CTor
		public AdsSource()
		{
		}

		public AdsSource(XmlElement node) : base(node)
		{
			foreach (XmlElement propertyNode in node.ChildNodes)
			{
				switch (propertyNode.Name)
				{
					case "id":
						this._Id = ParseInt(propertyNode.InnerText);
						continue;
					case "type":
						this._Type = propertyNode.InnerText;
						continue;
					case "adsPolicy":
						this._AdsPolicy = (AdsPolicy)StringEnum.Parse(typeof(AdsPolicy), propertyNode.InnerText);
						continue;
					case "adsParam":
						this._AdsParam = propertyNode.InnerText;
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
				kparams.AddReplace("objectType", "KalturaAdsSource");
			kparams.AddIfNotNull("id", this._Id);
			kparams.AddIfNotNull("type", this._Type);
			kparams.AddIfNotNull("adsPolicy", this._AdsPolicy);
			kparams.AddIfNotNull("adsParam", this._AdsParam);
			return kparams;
		}
		protected override string getPropertyName(string apiName)
		{
			switch(apiName)
			{
				case ID:
					return "Id";
				case TYPE:
					return "Type";
				case ADS_POLICY:
					return "AdsPolicy";
				case ADS_PARAM:
					return "AdsParam";
				default:
					return base.getPropertyName(apiName);
			}
		}
		#endregion
	}
}

