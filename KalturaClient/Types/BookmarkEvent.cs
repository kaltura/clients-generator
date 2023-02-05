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
	public class BookmarkEvent : EventObject
	{
		#region Constants
		public const string USER_ID = "userId";
		public const string HOUSEHOLD_ID = "householdId";
		public const string ASSET_ID = "assetId";
		public const string FILE_ID = "fileId";
		public const string POSITION = "position";
		public const string ACTION = "action";
		public const string PRODUCT_TYPE = "productType";
		public const string PRODUCT_ID = "productId";
		#endregion

		#region Private Fields
		private long _UserId = long.MinValue;
		private long _HouseholdId = long.MinValue;
		private long _AssetId = long.MinValue;
		private long _FileId = long.MinValue;
		private int _Position = Int32.MinValue;
		private BookmarkActionType _Action = null;
		private TransactionType _ProductType = null;
		private int _ProductId = Int32.MinValue;
		#endregion

		#region Properties
		[JsonProperty]
		public long UserId
		{
			get { return _UserId; }
			set 
			{ 
				_UserId = value;
				OnPropertyChanged("UserId");
			}
		}
		[JsonProperty]
		public long HouseholdId
		{
			get { return _HouseholdId; }
			set 
			{ 
				_HouseholdId = value;
				OnPropertyChanged("HouseholdId");
			}
		}
		[JsonProperty]
		public long AssetId
		{
			get { return _AssetId; }
			set 
			{ 
				_AssetId = value;
				OnPropertyChanged("AssetId");
			}
		}
		[JsonProperty]
		public long FileId
		{
			get { return _FileId; }
			set 
			{ 
				_FileId = value;
				OnPropertyChanged("FileId");
			}
		}
		[JsonProperty]
		public int Position
		{
			get { return _Position; }
			set 
			{ 
				_Position = value;
				OnPropertyChanged("Position");
			}
		}
		[JsonProperty]
		public BookmarkActionType Action
		{
			get { return _Action; }
			set 
			{ 
				_Action = value;
				OnPropertyChanged("Action");
			}
		}
		[JsonProperty]
		public TransactionType ProductType
		{
			get { return _ProductType; }
			set 
			{ 
				_ProductType = value;
				OnPropertyChanged("ProductType");
			}
		}
		[JsonProperty]
		public int ProductId
		{
			get { return _ProductId; }
			set 
			{ 
				_ProductId = value;
				OnPropertyChanged("ProductId");
			}
		}
		#endregion

		#region CTor
		public BookmarkEvent()
		{
		}

		public BookmarkEvent(JToken node) : base(node)
		{
			if(node["userId"] != null)
			{
				this._UserId = ParseLong(node["userId"].Value<string>());
			}
			if(node["householdId"] != null)
			{
				this._HouseholdId = ParseLong(node["householdId"].Value<string>());
			}
			if(node["assetId"] != null)
			{
				this._AssetId = ParseLong(node["assetId"].Value<string>());
			}
			if(node["fileId"] != null)
			{
				this._FileId = ParseLong(node["fileId"].Value<string>());
			}
			if(node["position"] != null)
			{
				this._Position = ParseInt(node["position"].Value<string>());
			}
			if(node["action"] != null)
			{
				this._Action = (BookmarkActionType)StringEnum.Parse(typeof(BookmarkActionType), node["action"].Value<string>());
			}
			if(node["productType"] != null)
			{
				this._ProductType = (TransactionType)StringEnum.Parse(typeof(TransactionType), node["productType"].Value<string>());
			}
			if(node["productId"] != null)
			{
				this._ProductId = ParseInt(node["productId"].Value<string>());
			}
		}
		#endregion

		#region Methods
		public override Params ToParams(bool includeObjectType = true)
		{
			Params kparams = base.ToParams(includeObjectType);
			if (includeObjectType)
				kparams.AddReplace("objectType", "KalturaBookmarkEvent");
			kparams.AddIfNotNull("userId", this._UserId);
			kparams.AddIfNotNull("householdId", this._HouseholdId);
			kparams.AddIfNotNull("assetId", this._AssetId);
			kparams.AddIfNotNull("fileId", this._FileId);
			kparams.AddIfNotNull("position", this._Position);
			kparams.AddIfNotNull("action", this._Action);
			kparams.AddIfNotNull("productType", this._ProductType);
			kparams.AddIfNotNull("productId", this._ProductId);
			return kparams;
		}
		protected override string getPropertyName(string apiName)
		{
			switch(apiName)
			{
				case USER_ID:
					return "UserId";
				case HOUSEHOLD_ID:
					return "HouseholdId";
				case ASSET_ID:
					return "AssetId";
				case FILE_ID:
					return "FileId";
				case POSITION:
					return "Position";
				case ACTION:
					return "Action";
				case PRODUCT_TYPE:
					return "ProductType";
				case PRODUCT_ID:
					return "ProductId";
				default:
					return base.getPropertyName(apiName);
			}
		}
		#endregion
	}
}

