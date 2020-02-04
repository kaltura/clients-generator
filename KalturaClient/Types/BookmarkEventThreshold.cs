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
	public class BookmarkEventThreshold : ObjectBase
	{
		#region Constants
		public const string TRANSACTION_TYPE = "transactionType";
		public const string THRESHOLD = "threshold";
		#endregion

		#region Private Fields
		private TransactionType _TransactionType = null;
		private int _Threshold = Int32.MinValue;
		#endregion

		#region Properties
		[JsonProperty]
		public TransactionType TransactionType
		{
			get { return _TransactionType; }
			set 
			{ 
				_TransactionType = value;
				OnPropertyChanged("TransactionType");
			}
		}
		[JsonProperty]
		public int Threshold
		{
			get { return _Threshold; }
			set 
			{ 
				_Threshold = value;
				OnPropertyChanged("Threshold");
			}
		}
		#endregion

		#region CTor
		public BookmarkEventThreshold()
		{
		}

		public BookmarkEventThreshold(JToken node) : base(node)
		{
			if(node["transactionType"] != null)
			{
				this._TransactionType = (TransactionType)StringEnum.Parse(typeof(TransactionType), node["transactionType"].Value<string>());
			}
			if(node["threshold"] != null)
			{
				this._Threshold = ParseInt(node["threshold"].Value<string>());
			}
		}
		#endregion

		#region Methods
		public override Params ToParams(bool includeObjectType = true)
		{
			Params kparams = base.ToParams(includeObjectType);
			if (includeObjectType)
				kparams.AddReplace("objectType", "KalturaBookmarkEventThreshold");
			kparams.AddIfNotNull("transactionType", this._TransactionType);
			kparams.AddIfNotNull("threshold", this._Threshold);
			return kparams;
		}
		protected override string getPropertyName(string apiName)
		{
			switch(apiName)
			{
				case TRANSACTION_TYPE:
					return "TransactionType";
				case THRESHOLD:
					return "Threshold";
				default:
					return base.getPropertyName(apiName);
			}
		}
		#endregion
	}
}

