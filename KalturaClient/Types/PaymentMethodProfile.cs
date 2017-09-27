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
	public class PaymentMethodProfile : ObjectBase
	{
		#region Constants
		public const string ID = "id";
		public const string PAYMENT_GATEWAY_ID = "paymentGatewayId";
		public const string NAME = "name";
		public const string ALLOW_MULTI_INSTANCE = "allowMultiInstance";
		#endregion

		#region Private Fields
		private int _Id = Int32.MinValue;
		private int _PaymentGatewayId = Int32.MinValue;
		private string _Name = null;
		private bool? _AllowMultiInstance = null;
		#endregion

		#region Properties
		public int Id
		{
			get { return _Id; }
		}
		public int PaymentGatewayId
		{
			get { return _PaymentGatewayId; }
			set 
			{ 
				_PaymentGatewayId = value;
				OnPropertyChanged("PaymentGatewayId");
			}
		}
		public string Name
		{
			get { return _Name; }
			set 
			{ 
				_Name = value;
				OnPropertyChanged("Name");
			}
		}
		public bool? AllowMultiInstance
		{
			get { return _AllowMultiInstance; }
			set 
			{ 
				_AllowMultiInstance = value;
				OnPropertyChanged("AllowMultiInstance");
			}
		}
		#endregion

		#region CTor
		public PaymentMethodProfile()
		{
		}

		public PaymentMethodProfile(XmlElement node) : base(node)
		{
			foreach (XmlElement propertyNode in node.ChildNodes)
			{
				switch (propertyNode.Name)
				{
					case "id":
						this._Id = ParseInt(propertyNode.InnerText);
						continue;
					case "paymentGatewayId":
						this._PaymentGatewayId = ParseInt(propertyNode.InnerText);
						continue;
					case "name":
						this._Name = propertyNode.InnerText;
						continue;
					case "allowMultiInstance":
						this._AllowMultiInstance = ParseBool(propertyNode.InnerText);
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
				kparams.AddReplace("objectType", "KalturaPaymentMethodProfile");
			kparams.AddIfNotNull("id", this._Id);
			kparams.AddIfNotNull("paymentGatewayId", this._PaymentGatewayId);
			kparams.AddIfNotNull("name", this._Name);
			kparams.AddIfNotNull("allowMultiInstance", this._AllowMultiInstance);
			return kparams;
		}
		protected override string getPropertyName(string apiName)
		{
			switch(apiName)
			{
				case ID:
					return "Id";
				case PAYMENT_GATEWAY_ID:
					return "PaymentGatewayId";
				case NAME:
					return "Name";
				case ALLOW_MULTI_INSTANCE:
					return "AllowMultiInstance";
				default:
					return base.getPropertyName(apiName);
			}
		}
		#endregion
	}
}

