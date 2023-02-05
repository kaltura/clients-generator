// ===================================================================================================
//                           _  __     _ _
//                          | |/ /__ _| | |_ _  _ _ _ __ _
//                          | ' </ _` | |  _| || | '_/ _` |
//                          |_|\_\__,_|_|\__|\_,_|_| \__,_|
//
// This file is part of the Kaltura Collaborative Media Suite which allows users
// to do with audio, video, and animation what Wiki platforms allow them to do with
// text.
//
// Copyright (C) 2006-2021  Kaltura Inc.
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
	public class Pin : ObjectBase
	{
		#region Constants
		public const string PIN = "pin";
		public const string ORIGIN = "origin";
		public const string TYPE = "type";
		#endregion

		#region Private Fields
		private string _Pin = null;
		private RuleLevel _Origin = null;
		private PinType _Type = null;
		#endregion

		#region Properties
		[JsonProperty]
		public string PinValue
		{
			get { return _Pin; }
			set 
			{ 
				_Pin = value;
				OnPropertyChanged("Pin");
			}
		}
		[JsonProperty]
		public RuleLevel Origin
		{
			get { return _Origin; }
			set 
			{ 
				_Origin = value;
				OnPropertyChanged("Origin");
			}
		}
		[JsonProperty]
		public PinType Type
		{
			get { return _Type; }
			set 
			{ 
				_Type = value;
				OnPropertyChanged("Type");
			}
		}
		#endregion

		#region CTor
		public Pin()
		{
		}

		public Pin(JToken node) : base(node)
		{
			if(node["pin"] != null)
			{
				this._Pin = node["pin"].Value<string>();
			}
			if(node["origin"] != null)
			{
				this._Origin = (RuleLevel)StringEnum.Parse(typeof(RuleLevel), node["origin"].Value<string>());
			}
			if(node["type"] != null)
			{
				this._Type = (PinType)StringEnum.Parse(typeof(PinType), node["type"].Value<string>());
			}
		}
		#endregion

		#region Methods
		public override Params ToParams(bool includeObjectType = true)
		{
			Params kparams = base.ToParams(includeObjectType);
			if (includeObjectType)
				kparams.AddReplace("objectType", "KalturaPin");
			kparams.AddIfNotNull("pin", this._Pin);
			kparams.AddIfNotNull("origin", this._Origin);
			kparams.AddIfNotNull("type", this._Type);
			return kparams;
		}
		protected override string getPropertyName(string apiName)
		{
			switch(apiName)
			{
				case PIN:
					return "Pin";
				case ORIGIN:
					return "Origin";
				case TYPE:
					return "Type";
				default:
					return base.getPropertyName(apiName);
			}
		}
		#endregion
	}
}

