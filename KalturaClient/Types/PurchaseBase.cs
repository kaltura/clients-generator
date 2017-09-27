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
	public class PurchaseBase : ObjectBase
	{
		#region Constants
		public const string PRODUCT_ID = "productId";
		public const string CONTENT_ID = "contentId";
		public const string PRODUCT_TYPE = "productType";
		#endregion

		#region Private Fields
		private int _ProductId = Int32.MinValue;
		private int _ContentId = Int32.MinValue;
		private TransactionType _ProductType = null;
		#endregion

		#region Properties
		public int ProductId
		{
			get { return _ProductId; }
			set 
			{ 
				_ProductId = value;
				OnPropertyChanged("ProductId");
			}
		}
		public int ContentId
		{
			get { return _ContentId; }
			set 
			{ 
				_ContentId = value;
				OnPropertyChanged("ContentId");
			}
		}
		public TransactionType ProductType
		{
			get { return _ProductType; }
			set 
			{ 
				_ProductType = value;
				OnPropertyChanged("ProductType");
			}
		}
		#endregion

		#region CTor
		public PurchaseBase()
		{
		}

		public PurchaseBase(XmlElement node) : base(node)
		{
			foreach (XmlElement propertyNode in node.ChildNodes)
			{
				switch (propertyNode.Name)
				{
					case "productId":
						this._ProductId = ParseInt(propertyNode.InnerText);
						continue;
					case "contentId":
						this._ContentId = ParseInt(propertyNode.InnerText);
						continue;
					case "productType":
						this._ProductType = (TransactionType)StringEnum.Parse(typeof(TransactionType), propertyNode.InnerText);
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
				kparams.AddReplace("objectType", "KalturaPurchaseBase");
			kparams.AddIfNotNull("productId", this._ProductId);
			kparams.AddIfNotNull("contentId", this._ContentId);
			kparams.AddIfNotNull("productType", this._ProductType);
			return kparams;
		}
		protected override string getPropertyName(string apiName)
		{
			switch(apiName)
			{
				case PRODUCT_ID:
					return "ProductId";
				case CONTENT_ID:
					return "ContentId";
				case PRODUCT_TYPE:
					return "ProductType";
				default:
					return base.getPropertyName(apiName);
			}
		}
		#endregion
	}
}

