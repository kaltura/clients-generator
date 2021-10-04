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
	public class RequestConfiguration : ObjectBase
	{
		#region Constants
		public const string PARTNER_ID = "partnerId";
		public const string USER_ID = "userId";
		public const string LANGUAGE = "language";
		public const string CURRENCY = "currency";
		public const string KS = "ks";
		public const string RESPONSE_PROFILE = "responseProfile";
		public const string ABORT_ON_ERROR = "abortOnError";
		public const string ABORT_ALL_ON_ERROR = "abortAllOnError";
		public const string SKIP_CONDITION = "skipCondition";
		#endregion

		#region Private Fields
		private int _PartnerId = Int32.MinValue;
		private int _UserId = Int32.MinValue;
		private string _Language = null;
		private string _Currency = null;
		private string _Ks = null;
		private BaseResponseProfile _ResponseProfile;
		private bool? _AbortOnError = null;
		private bool? _AbortAllOnError = null;
		private SkipCondition _SkipCondition;
		#endregion

		#region Properties
		[JsonProperty]
		public int PartnerId
		{
			get { return _PartnerId; }
			set 
			{ 
				_PartnerId = value;
				OnPropertyChanged("PartnerId");
			}
		}
		[JsonProperty]
		public int UserId
		{
			get { return _UserId; }
			set 
			{ 
				_UserId = value;
				OnPropertyChanged("UserId");
			}
		}
		[JsonProperty]
		public string Language
		{
			get { return _Language; }
			set 
			{ 
				_Language = value;
				OnPropertyChanged("Language");
			}
		}
		[JsonProperty]
		public string Currency
		{
			get { return _Currency; }
			set 
			{ 
				_Currency = value;
				OnPropertyChanged("Currency");
			}
		}
		[JsonProperty]
		public string Ks
		{
			get { return _Ks; }
			set 
			{ 
				_Ks = value;
				OnPropertyChanged("Ks");
			}
		}
		[JsonProperty]
		public BaseResponseProfile ResponseProfile
		{
			get { return _ResponseProfile; }
			set 
			{ 
				_ResponseProfile = value;
				OnPropertyChanged("ResponseProfile");
			}
		}
		[JsonProperty]
		public bool? AbortOnError
		{
			get { return _AbortOnError; }
			set 
			{ 
				_AbortOnError = value;
				OnPropertyChanged("AbortOnError");
			}
		}
		[JsonProperty]
		public bool? AbortAllOnError
		{
			get { return _AbortAllOnError; }
			set 
			{ 
				_AbortAllOnError = value;
				OnPropertyChanged("AbortAllOnError");
			}
		}
		[JsonProperty]
		public SkipCondition SkipCondition
		{
			get { return _SkipCondition; }
			set 
			{ 
				_SkipCondition = value;
				OnPropertyChanged("SkipCondition");
			}
		}
		#endregion

		#region CTor
		public RequestConfiguration()
		{
		}

		public RequestConfiguration(JToken node) : base(node)
		{
			if(node["partnerId"] != null)
			{
				this._PartnerId = ParseInt(node["partnerId"].Value<string>());
			}
			if(node["userId"] != null)
			{
				this._UserId = ParseInt(node["userId"].Value<string>());
			}
			if(node["language"] != null)
			{
				this._Language = node["language"].Value<string>();
			}
			if(node["currency"] != null)
			{
				this._Currency = node["currency"].Value<string>();
			}
			if(node["ks"] != null)
			{
				this._Ks = node["ks"].Value<string>();
			}
			if(node["responseProfile"] != null)
			{
				this._ResponseProfile = ObjectFactory.Create<BaseResponseProfile>(node["responseProfile"]);
			}
			if(node["abortOnError"] != null)
			{
				this._AbortOnError = ParseBool(node["abortOnError"].Value<string>());
			}
			if(node["abortAllOnError"] != null)
			{
				this._AbortAllOnError = ParseBool(node["abortAllOnError"].Value<string>());
			}
			if(node["skipCondition"] != null)
			{
				this._SkipCondition = ObjectFactory.Create<SkipCondition>(node["skipCondition"]);
			}
		}
		#endregion

		#region Methods
		public override Params ToParams(bool includeObjectType = true)
		{
			Params kparams = base.ToParams(includeObjectType);
			if (includeObjectType)
				kparams.AddReplace("objectType", "KalturaRequestConfiguration");
			kparams.AddIfNotNull("partnerId", this._PartnerId);
			kparams.AddIfNotNull("userId", this._UserId);
			kparams.AddIfNotNull("language", this._Language);
			kparams.AddIfNotNull("currency", this._Currency);
			kparams.AddIfNotNull("ks", this._Ks);
			kparams.AddIfNotNull("responseProfile", this._ResponseProfile);
			kparams.AddIfNotNull("abortOnError", this._AbortOnError);
			kparams.AddIfNotNull("abortAllOnError", this._AbortAllOnError);
			kparams.AddIfNotNull("skipCondition", this._SkipCondition);
			return kparams;
		}
		protected override string getPropertyName(string apiName)
		{
			switch(apiName)
			{
				case PARTNER_ID:
					return "PartnerId";
				case USER_ID:
					return "UserId";
				case LANGUAGE:
					return "Language";
				case CURRENCY:
					return "Currency";
				case KS:
					return "Ks";
				case RESPONSE_PROFILE:
					return "ResponseProfile";
				case ABORT_ON_ERROR:
					return "AbortOnError";
				case ABORT_ALL_ON_ERROR:
					return "AbortAllOnError";
				case SKIP_CONDITION:
					return "SkipCondition";
				default:
					return base.getPropertyName(apiName);
			}
		}
		#endregion
	}
}

