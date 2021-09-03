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
	public class ContentActionCondition : ObjectBase
	{
		#region Constants
		public const string ACTION = "action";
		public const string LENGTH = "length";
		public const string LENGTH_TYPE = "lengthType";
		public const string MULTIPLIER = "multiplier";
		#endregion

		#region Private Fields
		private ContentAction _Action = null;
		private int _Length = Int32.MinValue;
		private ContentActionConditionLengthType _LengthType = null;
		private int _Multiplier = Int32.MinValue;
		#endregion

		#region Properties
		[JsonProperty]
		public ContentAction Action
		{
			get { return _Action; }
			set 
			{ 
				_Action = value;
				OnPropertyChanged("Action");
			}
		}
		[JsonProperty]
		public int Length
		{
			get { return _Length; }
			set 
			{ 
				_Length = value;
				OnPropertyChanged("Length");
			}
		}
		[JsonProperty]
		public ContentActionConditionLengthType LengthType
		{
			get { return _LengthType; }
			set 
			{ 
				_LengthType = value;
				OnPropertyChanged("LengthType");
			}
		}
		[JsonProperty]
		public int Multiplier
		{
			get { return _Multiplier; }
			set 
			{ 
				_Multiplier = value;
				OnPropertyChanged("Multiplier");
			}
		}
		#endregion

		#region CTor
		public ContentActionCondition()
		{
		}

		public ContentActionCondition(JToken node) : base(node)
		{
			if(node["action"] != null)
			{
				this._Action = (ContentAction)StringEnum.Parse(typeof(ContentAction), node["action"].Value<string>());
			}
			if(node["length"] != null)
			{
				this._Length = ParseInt(node["length"].Value<string>());
			}
			if(node["lengthType"] != null)
			{
				this._LengthType = (ContentActionConditionLengthType)StringEnum.Parse(typeof(ContentActionConditionLengthType), node["lengthType"].Value<string>());
			}
			if(node["multiplier"] != null)
			{
				this._Multiplier = ParseInt(node["multiplier"].Value<string>());
			}
		}
		#endregion

		#region Methods
		public override Params ToParams(bool includeObjectType = true)
		{
			Params kparams = base.ToParams(includeObjectType);
			if (includeObjectType)
				kparams.AddReplace("objectType", "KalturaContentActionCondition");
			kparams.AddIfNotNull("action", this._Action);
			kparams.AddIfNotNull("length", this._Length);
			kparams.AddIfNotNull("lengthType", this._LengthType);
			kparams.AddIfNotNull("multiplier", this._Multiplier);
			return kparams;
		}
		protected override string getPropertyName(string apiName)
		{
			switch(apiName)
			{
				case ACTION:
					return "Action";
				case LENGTH:
					return "Length";
				case LENGTH_TYPE:
					return "LengthType";
				case MULTIPLIER:
					return "Multiplier";
				default:
					return base.getPropertyName(apiName);
			}
		}
		#endregion
	}
}

