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
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Kaltura.Types
{
	public class PaymentGatewayConfiguration : ObjectBase
	{
		#region Constants
		public const string PAYMENT_GATEWAY_CONFIGURATION = "paymentGatewayConfiguration";
		#endregion

		#region Private Fields
		private IList<KeyValue> _PaymentGatewayConfiguration;
		#endregion

		#region Properties
		[JsonProperty]
		public IList<KeyValue> PaymentGatewayConfigurationValue
		{
			get { return _PaymentGatewayConfiguration; }
			set 
			{ 
				_PaymentGatewayConfiguration = value;
				OnPropertyChanged("PaymentGatewayConfiguration");
			}
		}
		#endregion

		#region CTor
		public PaymentGatewayConfiguration()
		{
		}

		public PaymentGatewayConfiguration(JToken node) : base(node)
		{
			if(node["paymentGatewayConfiguration"] != null)
			{
				this._PaymentGatewayConfiguration = new List<KeyValue>();
				foreach(var arrayNode in node["paymentGatewayConfiguration"].Children())
				{
					this._PaymentGatewayConfiguration.Add(ObjectFactory.Create<KeyValue>(arrayNode));
				}
			}
		}
		#endregion

		#region Methods
		public override Params ToParams(bool includeObjectType = true)
		{
			Params kparams = base.ToParams(includeObjectType);
			if (includeObjectType)
				kparams.AddReplace("objectType", "KalturaPaymentGatewayConfiguration");
			kparams.AddIfNotNull("paymentGatewayConfiguration", this._PaymentGatewayConfiguration);
			return kparams;
		}
		protected override string getPropertyName(string apiName)
		{
			switch(apiName)
			{
				case PAYMENT_GATEWAY_CONFIGURATION:
					return "PaymentGatewayConfiguration";
				default:
					return base.getPropertyName(apiName);
			}
		}
		#endregion
	}
}

