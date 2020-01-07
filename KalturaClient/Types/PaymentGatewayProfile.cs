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
	public class PaymentGatewayProfile : PaymentGatewayBaseProfile
	{
		#region Constants
		public const string IS_ACTIVE = "isActive";
		public const string ADAPTER_URL = "adapterUrl";
		public const string TRANSACT_URL = "transactUrl";
		public const string STATUS_URL = "statusUrl";
		public const string RENEW_URL = "renewUrl";
		public const string PAYMENT_GATEWAY_SETTINGS = "paymentGatewaySettings";
		public const string EXTERNAL_IDENTIFIER = "externalIdentifier";
		public const string PENDING_INTERVAL = "pendingInterval";
		public const string PENDING_RETRIES = "pendingRetries";
		public const string SHARED_SECRET = "sharedSecret";
		public const string RENEW_INTERVAL_MINUTES = "renewIntervalMinutes";
		public const string RENEW_START_MINUTES = "renewStartMinutes";
		public const string EXTERNAL_VERIFICATION = "externalVerification";
		#endregion

		#region Private Fields
		private int _IsActive = Int32.MinValue;
		private string _AdapterUrl = null;
		private string _TransactUrl = null;
		private string _StatusUrl = null;
		private string _RenewUrl = null;
		private IDictionary<string, StringValue> _PaymentGatewaySettings;
		private string _ExternalIdentifier = null;
		private int _PendingInterval = Int32.MinValue;
		private int _PendingRetries = Int32.MinValue;
		private string _SharedSecret = null;
		private int _RenewIntervalMinutes = Int32.MinValue;
		private int _RenewStartMinutes = Int32.MinValue;
		private bool? _ExternalVerification = null;
		#endregion

		#region Properties
		[JsonProperty]
		public int IsActive
		{
			get { return _IsActive; }
			set 
			{ 
				_IsActive = value;
				OnPropertyChanged("IsActive");
			}
		}
		[JsonProperty]
		public string AdapterUrl
		{
			get { return _AdapterUrl; }
			set 
			{ 
				_AdapterUrl = value;
				OnPropertyChanged("AdapterUrl");
			}
		}
		[JsonProperty]
		public string TransactUrl
		{
			get { return _TransactUrl; }
			set 
			{ 
				_TransactUrl = value;
				OnPropertyChanged("TransactUrl");
			}
		}
		[JsonProperty]
		public string StatusUrl
		{
			get { return _StatusUrl; }
			set 
			{ 
				_StatusUrl = value;
				OnPropertyChanged("StatusUrl");
			}
		}
		[JsonProperty]
		public string RenewUrl
		{
			get { return _RenewUrl; }
			set 
			{ 
				_RenewUrl = value;
				OnPropertyChanged("RenewUrl");
			}
		}
		[JsonProperty]
		public IDictionary<string, StringValue> PaymentGatewaySettings
		{
			get { return _PaymentGatewaySettings; }
			set 
			{ 
				_PaymentGatewaySettings = value;
				OnPropertyChanged("PaymentGatewaySettings");
			}
		}
		[JsonProperty]
		public string ExternalIdentifier
		{
			get { return _ExternalIdentifier; }
			set 
			{ 
				_ExternalIdentifier = value;
				OnPropertyChanged("ExternalIdentifier");
			}
		}
		[JsonProperty]
		public int PendingInterval
		{
			get { return _PendingInterval; }
			set 
			{ 
				_PendingInterval = value;
				OnPropertyChanged("PendingInterval");
			}
		}
		[JsonProperty]
		public int PendingRetries
		{
			get { return _PendingRetries; }
			set 
			{ 
				_PendingRetries = value;
				OnPropertyChanged("PendingRetries");
			}
		}
		[JsonProperty]
		public string SharedSecret
		{
			get { return _SharedSecret; }
			set 
			{ 
				_SharedSecret = value;
				OnPropertyChanged("SharedSecret");
			}
		}
		[JsonProperty]
		public int RenewIntervalMinutes
		{
			get { return _RenewIntervalMinutes; }
			set 
			{ 
				_RenewIntervalMinutes = value;
				OnPropertyChanged("RenewIntervalMinutes");
			}
		}
		[JsonProperty]
		public int RenewStartMinutes
		{
			get { return _RenewStartMinutes; }
			set 
			{ 
				_RenewStartMinutes = value;
				OnPropertyChanged("RenewStartMinutes");
			}
		}
		[JsonProperty]
		public bool? ExternalVerification
		{
			get { return _ExternalVerification; }
			set 
			{ 
				_ExternalVerification = value;
				OnPropertyChanged("ExternalVerification");
			}
		}
		#endregion

		#region CTor
		public PaymentGatewayProfile()
		{
		}

		public PaymentGatewayProfile(JToken node) : base(node)
		{
			if(node["isActive"] != null)
			{
				this._IsActive = ParseInt(node["isActive"].Value<string>());
			}
			if(node["adapterUrl"] != null)
			{
				this._AdapterUrl = node["adapterUrl"].Value<string>();
			}
			if(node["transactUrl"] != null)
			{
				this._TransactUrl = node["transactUrl"].Value<string>();
			}
			if(node["statusUrl"] != null)
			{
				this._StatusUrl = node["statusUrl"].Value<string>();
			}
			if(node["renewUrl"] != null)
			{
				this._RenewUrl = node["renewUrl"].Value<string>();
			}
			if(node["paymentGatewaySettings"] != null)
			{
				{
					string key;
					this._PaymentGatewaySettings = new Dictionary<string, StringValue>();
					foreach(var arrayNode in node["paymentGatewaySettings"].Children<JProperty>())
					{
						key = arrayNode.Name;
						this._PaymentGatewaySettings[key] = ObjectFactory.Create<StringValue>(arrayNode.Value);
					}
				}
			}
			if(node["externalIdentifier"] != null)
			{
				this._ExternalIdentifier = node["externalIdentifier"].Value<string>();
			}
			if(node["pendingInterval"] != null)
			{
				this._PendingInterval = ParseInt(node["pendingInterval"].Value<string>());
			}
			if(node["pendingRetries"] != null)
			{
				this._PendingRetries = ParseInt(node["pendingRetries"].Value<string>());
			}
			if(node["sharedSecret"] != null)
			{
				this._SharedSecret = node["sharedSecret"].Value<string>();
			}
			if(node["renewIntervalMinutes"] != null)
			{
				this._RenewIntervalMinutes = ParseInt(node["renewIntervalMinutes"].Value<string>());
			}
			if(node["renewStartMinutes"] != null)
			{
				this._RenewStartMinutes = ParseInt(node["renewStartMinutes"].Value<string>());
			}
			if(node["externalVerification"] != null)
			{
				this._ExternalVerification = ParseBool(node["externalVerification"].Value<string>());
			}
		}
		#endregion

		#region Methods
		public override Params ToParams(bool includeObjectType = true)
		{
			Params kparams = base.ToParams(includeObjectType);
			if (includeObjectType)
				kparams.AddReplace("objectType", "KalturaPaymentGatewayProfile");
			kparams.AddIfNotNull("isActive", this._IsActive);
			kparams.AddIfNotNull("adapterUrl", this._AdapterUrl);
			kparams.AddIfNotNull("transactUrl", this._TransactUrl);
			kparams.AddIfNotNull("statusUrl", this._StatusUrl);
			kparams.AddIfNotNull("renewUrl", this._RenewUrl);
			kparams.AddIfNotNull("paymentGatewaySettings", this._PaymentGatewaySettings);
			kparams.AddIfNotNull("externalIdentifier", this._ExternalIdentifier);
			kparams.AddIfNotNull("pendingInterval", this._PendingInterval);
			kparams.AddIfNotNull("pendingRetries", this._PendingRetries);
			kparams.AddIfNotNull("sharedSecret", this._SharedSecret);
			kparams.AddIfNotNull("renewIntervalMinutes", this._RenewIntervalMinutes);
			kparams.AddIfNotNull("renewStartMinutes", this._RenewStartMinutes);
			kparams.AddIfNotNull("externalVerification", this._ExternalVerification);
			return kparams;
		}
		protected override string getPropertyName(string apiName)
		{
			switch(apiName)
			{
				case IS_ACTIVE:
					return "IsActive";
				case ADAPTER_URL:
					return "AdapterUrl";
				case TRANSACT_URL:
					return "TransactUrl";
				case STATUS_URL:
					return "StatusUrl";
				case RENEW_URL:
					return "RenewUrl";
				case PAYMENT_GATEWAY_SETTINGS:
					return "PaymentGatewaySettings";
				case EXTERNAL_IDENTIFIER:
					return "ExternalIdentifier";
				case PENDING_INTERVAL:
					return "PendingInterval";
				case PENDING_RETRIES:
					return "PendingRetries";
				case SHARED_SECRET:
					return "SharedSecret";
				case RENEW_INTERVAL_MINUTES:
					return "RenewIntervalMinutes";
				case RENEW_START_MINUTES:
					return "RenewStartMinutes";
				case EXTERNAL_VERIFICATION:
					return "ExternalVerification";
				default:
					return base.getPropertyName(apiName);
			}
		}
		#endregion
	}
}

