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
// Copyright (C) 2006-2020  Kaltura Inc.
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
	public class MediaConcurrencyRule : ObjectBase
	{
		#region Constants
		public const string ID = "id";
		public const string NAME = "name";
		public const string CONCURRENCY_LIMITATION_TYPE = "concurrencyLimitationType";
		public const string LIMITATION = "limitation";
		#endregion

		#region Private Fields
		private string _Id = null;
		private string _Name = null;
		private ConcurrencyLimitationType _ConcurrencyLimitationType = null;
		private int _Limitation = Int32.MinValue;
		#endregion

		#region Properties
		[JsonProperty]
		public string Id
		{
			get { return _Id; }
			set 
			{ 
				_Id = value;
				OnPropertyChanged("Id");
			}
		}
		[JsonProperty]
		public string Name
		{
			get { return _Name; }
			set 
			{ 
				_Name = value;
				OnPropertyChanged("Name");
			}
		}
		[JsonProperty]
		public ConcurrencyLimitationType ConcurrencyLimitationType
		{
			get { return _ConcurrencyLimitationType; }
			set 
			{ 
				_ConcurrencyLimitationType = value;
				OnPropertyChanged("ConcurrencyLimitationType");
			}
		}
		[JsonProperty]
		public int Limitation
		{
			get { return _Limitation; }
			set 
			{ 
				_Limitation = value;
				OnPropertyChanged("Limitation");
			}
		}
		#endregion

		#region CTor
		public MediaConcurrencyRule()
		{
		}

		public MediaConcurrencyRule(JToken node) : base(node)
		{
			if(node["id"] != null)
			{
				this._Id = node["id"].Value<string>();
			}
			if(node["name"] != null)
			{
				this._Name = node["name"].Value<string>();
			}
			if(node["concurrencyLimitationType"] != null)
			{
				this._ConcurrencyLimitationType = (ConcurrencyLimitationType)StringEnum.Parse(typeof(ConcurrencyLimitationType), node["concurrencyLimitationType"].Value<string>());
			}
			if(node["limitation"] != null)
			{
				this._Limitation = ParseInt(node["limitation"].Value<string>());
			}
		}
		#endregion

		#region Methods
		public override Params ToParams(bool includeObjectType = true)
		{
			Params kparams = base.ToParams(includeObjectType);
			if (includeObjectType)
				kparams.AddReplace("objectType", "KalturaMediaConcurrencyRule");
			kparams.AddIfNotNull("id", this._Id);
			kparams.AddIfNotNull("name", this._Name);
			kparams.AddIfNotNull("concurrencyLimitationType", this._ConcurrencyLimitationType);
			kparams.AddIfNotNull("limitation", this._Limitation);
			return kparams;
		}
		protected override string getPropertyName(string apiName)
		{
			switch(apiName)
			{
				case ID:
					return "Id";
				case NAME:
					return "Name";
				case CONCURRENCY_LIMITATION_TYPE:
					return "ConcurrencyLimitationType";
				case LIMITATION:
					return "Limitation";
				default:
					return base.getPropertyName(apiName);
			}
		}
		#endregion
	}
}

