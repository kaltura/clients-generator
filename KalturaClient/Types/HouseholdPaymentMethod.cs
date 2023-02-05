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
	public class HouseholdPaymentMethod : ObjectBase
	{
		#region Constants
		public const string ID = "id";
		public const string EXTERNAL_ID = "externalId";
		public const string PAYMENT_GATEWAY_ID = "paymentGatewayId";
		public const string DETAILS = "details";
		public const string IS_DEFAULT = "isDefault";
		public const string PAYMENT_METHOD_PROFILE_ID = "paymentMethodProfileId";
		#endregion

		#region Private Fields
		private int _Id = Int32.MinValue;
		private string _ExternalId = null;
		private int _PaymentGatewayId = Int32.MinValue;
		private string _Details = null;
		private bool? _IsDefault = null;
		private int _PaymentMethodProfileId = Int32.MinValue;
		#endregion

		#region Properties
		[JsonProperty]
		public int Id
		{
			get { return _Id; }
			private set 
			{ 
				_Id = value;
				OnPropertyChanged("Id");
			}
		}
		[JsonProperty]
		public string ExternalId
		{
			get { return _ExternalId; }
			set 
			{ 
				_ExternalId = value;
				OnPropertyChanged("ExternalId");
			}
		}
		[JsonProperty]
		public int PaymentGatewayId
		{
			get { return _PaymentGatewayId; }
			set 
			{ 
				_PaymentGatewayId = value;
				OnPropertyChanged("PaymentGatewayId");
			}
		}
		[JsonProperty]
		public string Details
		{
			get { return _Details; }
			set 
			{ 
				_Details = value;
				OnPropertyChanged("Details");
			}
		}
		[JsonProperty]
		public bool? IsDefault
		{
			get { return _IsDefault; }
			private set 
			{ 
				_IsDefault = value;
				OnPropertyChanged("IsDefault");
			}
		}
		[JsonProperty]
		public int PaymentMethodProfileId
		{
			get { return _PaymentMethodProfileId; }
			set 
			{ 
				_PaymentMethodProfileId = value;
				OnPropertyChanged("PaymentMethodProfileId");
			}
		}
		#endregion

		#region CTor
		public HouseholdPaymentMethod()
		{
		}

		public HouseholdPaymentMethod(JToken node) : base(node)
		{
			if(node["id"] != null)
			{
				this._Id = ParseInt(node["id"].Value<string>());
			}
			if(node["externalId"] != null)
			{
				this._ExternalId = node["externalId"].Value<string>();
			}
			if(node["paymentGatewayId"] != null)
			{
				this._PaymentGatewayId = ParseInt(node["paymentGatewayId"].Value<string>());
			}
			if(node["details"] != null)
			{
				this._Details = node["details"].Value<string>();
			}
			if(node["isDefault"] != null)
			{
				this._IsDefault = ParseBool(node["isDefault"].Value<string>());
			}
			if(node["paymentMethodProfileId"] != null)
			{
				this._PaymentMethodProfileId = ParseInt(node["paymentMethodProfileId"].Value<string>());
			}
		}
		#endregion

		#region Methods
		public override Params ToParams(bool includeObjectType = true)
		{
			Params kparams = base.ToParams(includeObjectType);
			if (includeObjectType)
				kparams.AddReplace("objectType", "KalturaHouseholdPaymentMethod");
			kparams.AddIfNotNull("id", this._Id);
			kparams.AddIfNotNull("externalId", this._ExternalId);
			kparams.AddIfNotNull("paymentGatewayId", this._PaymentGatewayId);
			kparams.AddIfNotNull("details", this._Details);
			kparams.AddIfNotNull("isDefault", this._IsDefault);
			kparams.AddIfNotNull("paymentMethodProfileId", this._PaymentMethodProfileId);
			return kparams;
		}
		protected override string getPropertyName(string apiName)
		{
			switch(apiName)
			{
				case ID:
					return "Id";
				case EXTERNAL_ID:
					return "ExternalId";
				case PAYMENT_GATEWAY_ID:
					return "PaymentGatewayId";
				case DETAILS:
					return "Details";
				case IS_DEFAULT:
					return "IsDefault";
				case PAYMENT_METHOD_PROFILE_ID:
					return "PaymentMethodProfileId";
				default:
					return base.getPropertyName(apiName);
			}
		}
		#endregion
	}
}

