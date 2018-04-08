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
	public class RandomCouponGenerationOptions : CouponGenerationOptions
	{
		#region Constants
		public const string NUMBER_OF_COUPONS = "numberOfCoupons";
		public const string USE_LETTERS = "useLetters";
		public const string USE_NUMBERS = "useNumbers";
		public const string USE_SPECIAL_CHARACTERS = "useSpecialCharacters";
		#endregion

		#region Private Fields
		private int _NumberOfCoupons = Int32.MinValue;
		private bool? _UseLetters = null;
		private bool? _UseNumbers = null;
		private bool? _UseSpecialCharacters = null;
		#endregion

		#region Properties
		public int NumberOfCoupons
		{
			get { return _NumberOfCoupons; }
			set 
			{ 
				_NumberOfCoupons = value;
				OnPropertyChanged("NumberOfCoupons");
			}
		}
		public bool? UseLetters
		{
			get { return _UseLetters; }
			set 
			{ 
				_UseLetters = value;
				OnPropertyChanged("UseLetters");
			}
		}
		public bool? UseNumbers
		{
			get { return _UseNumbers; }
			set 
			{ 
				_UseNumbers = value;
				OnPropertyChanged("UseNumbers");
			}
		}
		public bool? UseSpecialCharacters
		{
			get { return _UseSpecialCharacters; }
			set 
			{ 
				_UseSpecialCharacters = value;
				OnPropertyChanged("UseSpecialCharacters");
			}
		}
		#endregion

		#region CTor
		public RandomCouponGenerationOptions()
		{
		}

		public RandomCouponGenerationOptions(XmlElement node) : base(node)
		{
			foreach (XmlElement propertyNode in node.ChildNodes)
			{
				switch (propertyNode.Name)
				{
					case "numberOfCoupons":
						this._NumberOfCoupons = ParseInt(propertyNode.InnerText);
						continue;
					case "useLetters":
						this._UseLetters = ParseBool(propertyNode.InnerText);
						continue;
					case "useNumbers":
						this._UseNumbers = ParseBool(propertyNode.InnerText);
						continue;
					case "useSpecialCharacters":
						this._UseSpecialCharacters = ParseBool(propertyNode.InnerText);
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
				kparams.AddReplace("objectType", "KalturaRandomCouponGenerationOptions");
			kparams.AddIfNotNull("numberOfCoupons", this._NumberOfCoupons);
			kparams.AddIfNotNull("useLetters", this._UseLetters);
			kparams.AddIfNotNull("useNumbers", this._UseNumbers);
			kparams.AddIfNotNull("useSpecialCharacters", this._UseSpecialCharacters);
			return kparams;
		}
		protected override string getPropertyName(string apiName)
		{
			switch(apiName)
			{
				case NUMBER_OF_COUPONS:
					return "NumberOfCoupons";
				case USE_LETTERS:
					return "UseLetters";
				case USE_NUMBERS:
					return "UseNumbers";
				case USE_SPECIAL_CHARACTERS:
					return "UseSpecialCharacters";
				default:
					return base.getPropertyName(apiName);
			}
		}
		#endregion
	}
}

