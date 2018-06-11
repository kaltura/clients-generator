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
using System.IO;
using Kaltura.Request;
using Kaltura.Types;
using Kaltura.Enums;

namespace Kaltura.Services
{
	public class AssetRuleAddRequestBuilder : RequestBuilder<AssetRule>
	{
		#region Constants
		public const string ASSET_RULE = "assetRule";
		#endregion

		public AssetRule AssetRule
		{
			set;
			get;
		}

		public AssetRuleAddRequestBuilder()
			: base("assetrule", "add")
		{
		}

		public AssetRuleAddRequestBuilder(AssetRule assetRule)
			: this()
		{
			this.AssetRule = assetRule;
		}

		public override Params getParameters(bool includeServiceAndAction)
		{
			Params kparams = base.getParameters(includeServiceAndAction);
			if (!isMapped("assetRule"))
				kparams.AddIfNotNull("assetRule", AssetRule);
			return kparams;
		}

		public override Files getFiles()
		{
			Files kfiles = base.getFiles();
			return kfiles;
		}

		public override object Deserialize(XmlElement result)
		{
			return ObjectFactory.Create<AssetRule>(result);
		}
	}

	public class AssetRuleDeleteRequestBuilder : RequestBuilder<bool>
	{
		#region Constants
		public const string ID = "id";
		#endregion

		public long Id
		{
			set;
			get;
		}

		public AssetRuleDeleteRequestBuilder()
			: base("assetrule", "delete")
		{
		}

		public AssetRuleDeleteRequestBuilder(long id)
			: this()
		{
			this.Id = id;
		}

		public override Params getParameters(bool includeServiceAndAction)
		{
			Params kparams = base.getParameters(includeServiceAndAction);
			if (!isMapped("id"))
				kparams.AddIfNotNull("id", Id);
			return kparams;
		}

		public override Files getFiles()
		{
			Files kfiles = base.getFiles();
			return kfiles;
		}

		public override object Deserialize(XmlElement result)
		{
			if (result.InnerText.Equals("1") || result.InnerText.ToLower().Equals("true"))
				return true;
			return false;
		}
	}

	public class AssetRuleListRequestBuilder : RequestBuilder<ListResponse<AssetRule>>
	{
		#region Constants
		public const string FILTER = "filter";
		#endregion

		public AssetRuleFilter Filter
		{
			set;
			get;
		}

		public AssetRuleListRequestBuilder()
			: base("assetrule", "list")
		{
		}

		public AssetRuleListRequestBuilder(AssetRuleFilter filter)
			: this()
		{
			this.Filter = filter;
		}

		public override Params getParameters(bool includeServiceAndAction)
		{
			Params kparams = base.getParameters(includeServiceAndAction);
			if (!isMapped("filter"))
				kparams.AddIfNotNull("filter", Filter);
			return kparams;
		}

		public override Files getFiles()
		{
			Files kfiles = base.getFiles();
			return kfiles;
		}

		public override object Deserialize(XmlElement result)
		{
			return ObjectFactory.Create<ListResponse<AssetRule>>(result);
		}
	}

	public class AssetRuleUpdateRequestBuilder : RequestBuilder<AssetRule>
	{
		#region Constants
		public const string ID = "id";
		public const string ASSET_RULE = "assetRule";
		#endregion

		public long Id
		{
			set;
			get;
		}
		public AssetRule AssetRule
		{
			set;
			get;
		}

		public AssetRuleUpdateRequestBuilder()
			: base("assetrule", "update")
		{
		}

		public AssetRuleUpdateRequestBuilder(long id, AssetRule assetRule)
			: this()
		{
			this.Id = id;
			this.AssetRule = assetRule;
		}

		public override Params getParameters(bool includeServiceAndAction)
		{
			Params kparams = base.getParameters(includeServiceAndAction);
			if (!isMapped("id"))
				kparams.AddIfNotNull("id", Id);
			if (!isMapped("assetRule"))
				kparams.AddIfNotNull("assetRule", AssetRule);
			return kparams;
		}

		public override Files getFiles()
		{
			Files kfiles = base.getFiles();
			return kfiles;
		}

		public override object Deserialize(XmlElement result)
		{
			return ObjectFactory.Create<AssetRule>(result);
		}
	}


	public class AssetRuleService
	{
		private AssetRuleService()
		{
		}

		public static AssetRuleAddRequestBuilder Add(AssetRule assetRule)
		{
			return new AssetRuleAddRequestBuilder(assetRule);
		}

		public static AssetRuleDeleteRequestBuilder Delete(long id)
		{
			return new AssetRuleDeleteRequestBuilder(id);
		}

		public static AssetRuleListRequestBuilder List(AssetRuleFilter filter = null)
		{
			return new AssetRuleListRequestBuilder(filter);
		}

		public static AssetRuleUpdateRequestBuilder Update(long id, AssetRule assetRule)
		{
			return new AssetRuleUpdateRequestBuilder(id, assetRule);
		}
	}
}
