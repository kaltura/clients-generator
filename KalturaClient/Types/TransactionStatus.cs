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
	public class TransactionStatus : ObjectBase
	{
		#region Constants
		public const string ADAPTER_TRANSACTION_STATUS = "adapterTransactionStatus";
		public const string EXTERNAL_ID = "externalId";
		public const string EXTERNAL_STATUS = "externalStatus";
		public const string EXTERNAL_MESSAGE = "externalMessage";
		public const string FAIL_REASON = "failReason";
		#endregion

		#region Private Fields
		private TransactionAdapterStatus _AdapterTransactionStatus = null;
		private string _ExternalId = null;
		private string _ExternalStatus = null;
		private string _ExternalMessage = null;
		private int _FailReason = Int32.MinValue;
		#endregion

		#region Properties
		[JsonProperty]
		public TransactionAdapterStatus AdapterTransactionStatus
		{
			get { return _AdapterTransactionStatus; }
			set 
			{ 
				_AdapterTransactionStatus = value;
				OnPropertyChanged("AdapterTransactionStatus");
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
		public string ExternalStatus
		{
			get { return _ExternalStatus; }
			set 
			{ 
				_ExternalStatus = value;
				OnPropertyChanged("ExternalStatus");
			}
		}
		[JsonProperty]
		public string ExternalMessage
		{
			get { return _ExternalMessage; }
			set 
			{ 
				_ExternalMessage = value;
				OnPropertyChanged("ExternalMessage");
			}
		}
		[JsonProperty]
		public int FailReason
		{
			get { return _FailReason; }
			set 
			{ 
				_FailReason = value;
				OnPropertyChanged("FailReason");
			}
		}
		#endregion

		#region CTor
		public TransactionStatus()
		{
		}

		public TransactionStatus(JToken node) : base(node)
		{
			if(node["adapterTransactionStatus"] != null)
			{
				this._AdapterTransactionStatus = (TransactionAdapterStatus)StringEnum.Parse(typeof(TransactionAdapterStatus), node["adapterTransactionStatus"].Value<string>());
			}
			if(node["externalId"] != null)
			{
				this._ExternalId = node["externalId"].Value<string>();
			}
			if(node["externalStatus"] != null)
			{
				this._ExternalStatus = node["externalStatus"].Value<string>();
			}
			if(node["externalMessage"] != null)
			{
				this._ExternalMessage = node["externalMessage"].Value<string>();
			}
			if(node["failReason"] != null)
			{
				this._FailReason = ParseInt(node["failReason"].Value<string>());
			}
		}
		#endregion

		#region Methods
		public override Params ToParams(bool includeObjectType = true)
		{
			Params kparams = base.ToParams(includeObjectType);
			if (includeObjectType)
				kparams.AddReplace("objectType", "KalturaTransactionStatus");
			kparams.AddIfNotNull("adapterTransactionStatus", this._AdapterTransactionStatus);
			kparams.AddIfNotNull("externalId", this._ExternalId);
			kparams.AddIfNotNull("externalStatus", this._ExternalStatus);
			kparams.AddIfNotNull("externalMessage", this._ExternalMessage);
			kparams.AddIfNotNull("failReason", this._FailReason);
			return kparams;
		}
		protected override string getPropertyName(string apiName)
		{
			switch(apiName)
			{
				case ADAPTER_TRANSACTION_STATUS:
					return "AdapterTransactionStatus";
				case EXTERNAL_ID:
					return "ExternalId";
				case EXTERNAL_STATUS:
					return "ExternalStatus";
				case EXTERNAL_MESSAGE:
					return "ExternalMessage";
				case FAIL_REASON:
					return "FailReason";
				default:
					return base.getPropertyName(apiName);
			}
		}
		#endregion
	}
}

