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
	public class UnifiedBillingCycle : ObjectBase
	{
		#region Constants
		public const string NAME = "name";
		public const string DURATION = "duration";
		public const string PAYMENT_GATEWAY_ID = "paymentGatewayId";
		public const string IGNORE_PARTIAL_BILLING = "ignorePartialBilling";
		#endregion

		#region Private Fields
		private string _Name = null;
		private Duration _Duration;
		private int _PaymentGatewayId = Int32.MinValue;
		private bool? _IgnorePartialBilling = null;
		#endregion

		#region Properties
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
		public Duration Duration
		{
			get { return _Duration; }
			set 
			{ 
				_Duration = value;
				OnPropertyChanged("Duration");
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
		public bool? IgnorePartialBilling
		{
			get { return _IgnorePartialBilling; }
			set 
			{ 
				_IgnorePartialBilling = value;
				OnPropertyChanged("IgnorePartialBilling");
			}
		}
		#endregion

		#region CTor
		public UnifiedBillingCycle()
		{
		}

		public UnifiedBillingCycle(JToken node) : base(node)
		{
			if(node["name"] != null)
			{
				this._Name = node["name"].Value<string>();
			}
			if(node["duration"] != null)
			{
				this._Duration = ObjectFactory.Create<Duration>(node["duration"]);
			}
			if(node["paymentGatewayId"] != null)
			{
				this._PaymentGatewayId = ParseInt(node["paymentGatewayId"].Value<string>());
			}
			if(node["ignorePartialBilling"] != null)
			{
				this._IgnorePartialBilling = ParseBool(node["ignorePartialBilling"].Value<string>());
			}
		}
		#endregion

		#region Methods
		public override Params ToParams(bool includeObjectType = true)
		{
			Params kparams = base.ToParams(includeObjectType);
			if (includeObjectType)
				kparams.AddReplace("objectType", "KalturaUnifiedBillingCycle");
			kparams.AddIfNotNull("name", this._Name);
			kparams.AddIfNotNull("duration", this._Duration);
			kparams.AddIfNotNull("paymentGatewayId", this._PaymentGatewayId);
			kparams.AddIfNotNull("ignorePartialBilling", this._IgnorePartialBilling);
			return kparams;
		}
		protected override string getPropertyName(string apiName)
		{
			switch(apiName)
			{
				case NAME:
					return "Name";
				case DURATION:
					return "Duration";
				case PAYMENT_GATEWAY_ID:
					return "PaymentGatewayId";
				case IGNORE_PARTIAL_BILLING:
					return "IgnorePartialBilling";
				default:
					return base.getPropertyName(apiName);
			}
		}
		#endregion
	}
}

