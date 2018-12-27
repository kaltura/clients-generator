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
using Newtonsoft.Json.Linq;

namespace Kaltura.Services
{
	public class CouponsGroupAddRequestBuilder : RequestBuilder<CouponsGroup>
	{
		#region Constants
		public const string COUPONS_GROUP = "couponsGroup";
		#endregion

		public CouponsGroup CouponsGroup
		{
			set;
			get;
		}

		public CouponsGroupAddRequestBuilder()
			: base("couponsgroup", "add")
		{
		}

		public CouponsGroupAddRequestBuilder(CouponsGroup couponsGroup)
			: this()
		{
			this.CouponsGroup = couponsGroup;
		}

		public override Params getParameters(bool includeServiceAndAction)
		{
			Params kparams = base.getParameters(includeServiceAndAction);
			if (!isMapped("couponsGroup"))
				kparams.AddIfNotNull("couponsGroup", CouponsGroup);
			return kparams;
		}

		public override Files getFiles()
		{
			Files kfiles = base.getFiles();
			return kfiles;
		}

		public override object Deserialize(JToken result)
		{
			return ObjectFactory.Create<CouponsGroup>(result);
		}
	}

	public class CouponsGroupDeleteRequestBuilder : RequestBuilder<bool>
	{
		#region Constants
		public const string ID = "id";
		#endregion

		public long Id
		{
			set;
			get;
		}

		public CouponsGroupDeleteRequestBuilder()
			: base("couponsgroup", "delete")
		{
		}

		public CouponsGroupDeleteRequestBuilder(long id)
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

		public override object Deserialize(JToken result)
		{
			if (result.Value<string>().Equals("1") || result.Value<string>().ToLower().Equals("true"))
				return true;
			return false;
		}
	}

	public class CouponsGroupGenerateRequestBuilder : RequestBuilder<StringValueArray>
	{
		#region Constants
		public const string ID = "id";
		public const string COUPON_GENERATION_OPTIONS = "couponGenerationOptions";
		#endregion

		public long Id
		{
			set;
			get;
		}
		public CouponGenerationOptions CouponGenerationOptions
		{
			set;
			get;
		}

		public CouponsGroupGenerateRequestBuilder()
			: base("couponsgroup", "generate")
		{
		}

		public CouponsGroupGenerateRequestBuilder(long id, CouponGenerationOptions couponGenerationOptions)
			: this()
		{
			this.Id = id;
			this.CouponGenerationOptions = couponGenerationOptions;
		}

		public override Params getParameters(bool includeServiceAndAction)
		{
			Params kparams = base.getParameters(includeServiceAndAction);
			if (!isMapped("id"))
				kparams.AddIfNotNull("id", Id);
			if (!isMapped("couponGenerationOptions"))
				kparams.AddIfNotNull("couponGenerationOptions", CouponGenerationOptions);
			return kparams;
		}

		public override Files getFiles()
		{
			Files kfiles = base.getFiles();
			return kfiles;
		}

		public override object Deserialize(JToken result)
		{
			return ObjectFactory.Create<StringValueArray>(result);
		}
	}

	public class CouponsGroupGetRequestBuilder : RequestBuilder<CouponsGroup>
	{
		#region Constants
		public const string ID = "id";
		#endregion

		public long Id
		{
			set;
			get;
		}

		public CouponsGroupGetRequestBuilder()
			: base("couponsgroup", "get")
		{
		}

		public CouponsGroupGetRequestBuilder(long id)
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

		public override object Deserialize(JToken result)
		{
			return ObjectFactory.Create<CouponsGroup>(result);
		}
	}

	public class CouponsGroupListRequestBuilder : RequestBuilder<ListResponse<CouponsGroup>>
	{
		#region Constants
		#endregion


		public CouponsGroupListRequestBuilder()
			: base("couponsgroup", "list")
		{
		}

		public override Params getParameters(bool includeServiceAndAction)
		{
			Params kparams = base.getParameters(includeServiceAndAction);
			return kparams;
		}

		public override Files getFiles()
		{
			Files kfiles = base.getFiles();
			return kfiles;
		}

		public override object Deserialize(JToken result)
		{
			return ObjectFactory.Create<ListResponse<CouponsGroup>>(result);
		}
	}

	public class CouponsGroupUpdateRequestBuilder : RequestBuilder<CouponsGroup>
	{
		#region Constants
		public const string ID = "id";
		public const string COUPONS_GROUP = "couponsGroup";
		#endregion

		public long Id
		{
			set;
			get;
		}
		public CouponsGroup CouponsGroup
		{
			set;
			get;
		}

		public CouponsGroupUpdateRequestBuilder()
			: base("couponsgroup", "update")
		{
		}

		public CouponsGroupUpdateRequestBuilder(long id, CouponsGroup couponsGroup)
			: this()
		{
			this.Id = id;
			this.CouponsGroup = couponsGroup;
		}

		public override Params getParameters(bool includeServiceAndAction)
		{
			Params kparams = base.getParameters(includeServiceAndAction);
			if (!isMapped("id"))
				kparams.AddIfNotNull("id", Id);
			if (!isMapped("couponsGroup"))
				kparams.AddIfNotNull("couponsGroup", CouponsGroup);
			return kparams;
		}

		public override Files getFiles()
		{
			Files kfiles = base.getFiles();
			return kfiles;
		}

		public override object Deserialize(JToken result)
		{
			return ObjectFactory.Create<CouponsGroup>(result);
		}
	}


	public class CouponsGroupService
	{
		private CouponsGroupService()
		{
		}

		public static CouponsGroupAddRequestBuilder Add(CouponsGroup couponsGroup)
		{
			return new CouponsGroupAddRequestBuilder(couponsGroup);
		}

		public static CouponsGroupDeleteRequestBuilder Delete(long id)
		{
			return new CouponsGroupDeleteRequestBuilder(id);
		}

		public static CouponsGroupGenerateRequestBuilder Generate(long id, CouponGenerationOptions couponGenerationOptions)
		{
			return new CouponsGroupGenerateRequestBuilder(id, couponGenerationOptions);
		}

		public static CouponsGroupGetRequestBuilder Get(long id)
		{
			return new CouponsGroupGetRequestBuilder(id);
		}

		public static CouponsGroupListRequestBuilder List()
		{
			return new CouponsGroupListRequestBuilder();
		}

		public static CouponsGroupUpdateRequestBuilder Update(long id, CouponsGroup couponsGroup)
		{
			return new CouponsGroupUpdateRequestBuilder(id, couponsGroup);
		}
	}
}
