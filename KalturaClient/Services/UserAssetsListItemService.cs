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
using System.IO;
using Kaltura.Request;
using Kaltura.Types;
using Kaltura.Enums;
using Newtonsoft.Json.Linq;

namespace Kaltura.Services
{
	public class UserAssetsListItemAddRequestBuilder : RequestBuilder<UserAssetsListItem>
	{
		#region Constants
		public const string USER_ASSETS_LIST_ITEM = "userAssetsListItem";
		#endregion

		public UserAssetsListItem UserAssetsListItem { get; set; }

		public UserAssetsListItemAddRequestBuilder()
			: base("userassetslistitem", "add")
		{
		}

		public UserAssetsListItemAddRequestBuilder(UserAssetsListItem userAssetsListItem)
			: this()
		{
			this.UserAssetsListItem = userAssetsListItem;
		}

		public override Params getParameters(bool includeServiceAndAction)
		{
			Params kparams = base.getParameters(includeServiceAndAction);
			if (!isMapped("userAssetsListItem"))
				kparams.AddIfNotNull("userAssetsListItem", UserAssetsListItem);
			return kparams;
		}

		public override Files getFiles()
		{
			Files kfiles = base.getFiles();
			return kfiles;
		}

		public override object Deserialize(JToken result)
		{
			return ObjectFactory.Create<UserAssetsListItem>(result);
		}
	}

	public class UserAssetsListItemDeleteRequestBuilder : RequestBuilder<bool>
	{
		#region Constants
		public const string ASSET_ID = "assetId";
		public const string LIST_TYPE = "listType";
		#endregion

		public string AssetId { get; set; }
		public UserAssetsListType ListType { get; set; }

		public UserAssetsListItemDeleteRequestBuilder()
			: base("userassetslistitem", "delete")
		{
		}

		public UserAssetsListItemDeleteRequestBuilder(string assetId, UserAssetsListType listType)
			: this()
		{
			this.AssetId = assetId;
			this.ListType = listType;
		}

		public override Params getParameters(bool includeServiceAndAction)
		{
			Params kparams = base.getParameters(includeServiceAndAction);
			if (!isMapped("assetId"))
				kparams.AddIfNotNull("assetId", AssetId);
			if (!isMapped("listType"))
				kparams.AddIfNotNull("listType", ListType);
			return kparams;
		}

		public override Files getFiles()
		{
			Files kfiles = base.getFiles();
			return kfiles;
		}

		public override object Deserialize(JToken result)
		{
			if (result.Value<string>().Equals("1") || result.Value<string>().ToLower().Equals("true"))
				return true;
			return false;
		}
	}

	public class UserAssetsListItemGetRequestBuilder : RequestBuilder<UserAssetsListItem>
	{
		#region Constants
		public const string ASSET_ID = "assetId";
		public const string LIST_TYPE = "listType";
		public const string ITEM_TYPE = "itemType";
		#endregion

		public string AssetId { get; set; }
		public UserAssetsListType ListType { get; set; }
		public UserAssetsListItemType ItemType { get; set; }

		public UserAssetsListItemGetRequestBuilder()
			: base("userassetslistitem", "get")
		{
		}

		public UserAssetsListItemGetRequestBuilder(string assetId, UserAssetsListType listType, UserAssetsListItemType itemType)
			: this()
		{
			this.AssetId = assetId;
			this.ListType = listType;
			this.ItemType = itemType;
		}

		public override Params getParameters(bool includeServiceAndAction)
		{
			Params kparams = base.getParameters(includeServiceAndAction);
			if (!isMapped("assetId"))
				kparams.AddIfNotNull("assetId", AssetId);
			if (!isMapped("listType"))
				kparams.AddIfNotNull("listType", ListType);
			if (!isMapped("itemType"))
				kparams.AddIfNotNull("itemType", ItemType);
			return kparams;
		}

		public override Files getFiles()
		{
			Files kfiles = base.getFiles();
			return kfiles;
		}

		public override object Deserialize(JToken result)
		{
			return ObjectFactory.Create<UserAssetsListItem>(result);
		}
	}


	public class UserAssetsListItemService
	{
		private UserAssetsListItemService()
		{
		}

		public static UserAssetsListItemAddRequestBuilder Add(UserAssetsListItem userAssetsListItem)
		{
			return new UserAssetsListItemAddRequestBuilder(userAssetsListItem);
		}

		public static UserAssetsListItemDeleteRequestBuilder Delete(string assetId, UserAssetsListType listType)
		{
			return new UserAssetsListItemDeleteRequestBuilder(assetId, listType);
		}

		public static UserAssetsListItemGetRequestBuilder Get(string assetId, UserAssetsListType listType, UserAssetsListItemType itemType)
		{
			return new UserAssetsListItemGetRequestBuilder(assetId, listType, itemType);
		}
	}
}
