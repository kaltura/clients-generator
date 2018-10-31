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
	public class RequestConfiguration : ObjectBase
	{
		#region Constants
		public const string PARTNER_ID = "partnerId";
		public const string USER_ID = "userId";
		public const string LANGUAGE = "language";
		public const string CURRENCY = "currency";
		public const string KS = "ks";
		public const string RESPONSE_PROFILE = "responseProfile";
		public const string ABORT_ALL_ON_ERROR = "abortAllOnError";
		public const string SKIP_ON_ORROR = "skipOnOrror";
		#endregion

		#region Private Fields
		private int _PartnerId = Int32.MinValue;
		private int _UserId = Int32.MinValue;
		private string _Language = null;
		private string _Currency = null;
		private string _Ks = null;
		private BaseResponseProfile _ResponseProfile;
		private bool? _AbortAllOnError = null;
		private SkipOptions _SkipOnOrror = null;
		#endregion

		#region Properties
		public int PartnerId
		{
			get { return _PartnerId; }
			set 
			{ 
				_PartnerId = value;
				OnPropertyChanged("PartnerId");
			}
		}
		public int UserId
		{
			get { return _UserId; }
			set 
			{ 
				_UserId = value;
				OnPropertyChanged("UserId");
			}
		}
		public string Language
		{
			get { return _Language; }
			set 
			{ 
				_Language = value;
				OnPropertyChanged("Language");
			}
		}
		public string Currency
		{
			get { return _Currency; }
			set 
			{ 
				_Currency = value;
				OnPropertyChanged("Currency");
			}
		}
		public string Ks
		{
			get { return _Ks; }
			set 
			{ 
				_Ks = value;
				OnPropertyChanged("Ks");
			}
		}
		public BaseResponseProfile ResponseProfile
		{
			get { return _ResponseProfile; }
			set 
			{ 
				_ResponseProfile = value;
				OnPropertyChanged("ResponseProfile");
			}
		}
		public bool? AbortAllOnError
		{
			get { return _AbortAllOnError; }
			set 
			{ 
				_AbortAllOnError = value;
				OnPropertyChanged("AbortAllOnError");
			}
		}
		public SkipOptions SkipOnOrror
		{
			get { return _SkipOnOrror; }
			set 
			{ 
				_SkipOnOrror = value;
				OnPropertyChanged("SkipOnOrror");
			}
		}
		#endregion

		#region CTor
		public RequestConfiguration()
		{
		}

		public RequestConfiguration(XmlElement node) : base(node)
		{
			foreach (XmlElement propertyNode in node.ChildNodes)
			{
				switch (propertyNode.Name)
				{
					case "partnerId":
						this._PartnerId = ParseInt(propertyNode.InnerText);
						continue;
					case "userId":
						this._UserId = ParseInt(propertyNode.InnerText);
						continue;
					case "language":
						this._Language = propertyNode.InnerText;
						continue;
					case "currency":
						this._Currency = propertyNode.InnerText;
						continue;
					case "ks":
						this._Ks = propertyNode.InnerText;
						continue;
					case "responseProfile":
						this._ResponseProfile = ObjectFactory.Create<BaseResponseProfile>(propertyNode);
						continue;
					case "abortAllOnError":
						this._AbortAllOnError = ParseBool(propertyNode.InnerText);
						continue;
					case "skipOnOrror":
						this._SkipOnOrror = (SkipOptions)StringEnum.Parse(typeof(SkipOptions), propertyNode.InnerText);
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
				kparams.AddReplace("objectType", "KalturaRequestConfiguration");
			kparams.AddIfNotNull("partnerId", this._PartnerId);
			kparams.AddIfNotNull("userId", this._UserId);
			kparams.AddIfNotNull("language", this._Language);
			kparams.AddIfNotNull("currency", this._Currency);
			kparams.AddIfNotNull("ks", this._Ks);
			kparams.AddIfNotNull("responseProfile", this._ResponseProfile);
			kparams.AddIfNotNull("abortAllOnError", this._AbortAllOnError);
			kparams.AddIfNotNull("skipOnOrror", this._SkipOnOrror);
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
				case ABORT_ALL_ON_ERROR:
					return "AbortAllOnError";
				case SKIP_ON_ORROR:
					return "SkipOnOrror";
				default:
					return base.getPropertyName(apiName);
			}
		}
		#endregion
	}
}

