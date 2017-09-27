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
// Copyright (C) 2006-2017  Kaltura Inc.
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
		public TransactionAdapterStatus AdapterTransactionStatus
		{
			get { return _AdapterTransactionStatus; }
			set 
			{ 
				_AdapterTransactionStatus = value;
				OnPropertyChanged("AdapterTransactionStatus");
			}
		}
		public string ExternalId
		{
			get { return _ExternalId; }
			set 
			{ 
				_ExternalId = value;
				OnPropertyChanged("ExternalId");
			}
		}
		public string ExternalStatus
		{
			get { return _ExternalStatus; }
			set 
			{ 
				_ExternalStatus = value;
				OnPropertyChanged("ExternalStatus");
			}
		}
		public string ExternalMessage
		{
			get { return _ExternalMessage; }
			set 
			{ 
				_ExternalMessage = value;
				OnPropertyChanged("ExternalMessage");
			}
		}
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

		public TransactionStatus(XmlElement node) : base(node)
		{
			foreach (XmlElement propertyNode in node.ChildNodes)
			{
				switch (propertyNode.Name)
				{
					case "adapterTransactionStatus":
						this._AdapterTransactionStatus = (TransactionAdapterStatus)StringEnum.Parse(typeof(TransactionAdapterStatus), propertyNode.InnerText);
						continue;
					case "externalId":
						this._ExternalId = propertyNode.InnerText;
						continue;
					case "externalStatus":
						this._ExternalStatus = propertyNode.InnerText;
						continue;
					case "externalMessage":
						this._ExternalMessage = propertyNode.InnerText;
						continue;
					case "failReason":
						this._FailReason = ParseInt(propertyNode.InnerText);
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

