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
using System.IO;
using Kaltura.Request;
using Kaltura.Types;
using Kaltura.Enums;
using Newtonsoft.Json.Linq;

namespace Kaltura.Services
{
	public class PricePlanAddRequestBuilder : RequestBuilder<PricePlan>
	{
		#region Constants
		public const string PRICE_PLAN = "pricePlan";
		#endregion

		public PricePlan PricePlan { get; set; }

		public PricePlanAddRequestBuilder()
			: base("priceplan", "add")
		{
		}

		public PricePlanAddRequestBuilder(PricePlan pricePlan)
			: this()
		{
			this.PricePlan = pricePlan;
		}

		public override Params getParameters(bool includeServiceAndAction)
		{
			Params kparams = base.getParameters(includeServiceAndAction);
			if (!isMapped("pricePlan"))
				kparams.AddIfNotNull("pricePlan", PricePlan);
			return kparams;
		}

		public override Files getFiles()
		{
			Files kfiles = base.getFiles();
			return kfiles;
		}

		public override object Deserialize(JToken result)
		{
			return ObjectFactory.Create<PricePlan>(result);
		}
	}

	public class PricePlanDeleteRequestBuilder : RequestBuilder<bool>
	{
		#region Constants
		public const string ID = "id";
		#endregion

		public long Id { get; set; }

		public PricePlanDeleteRequestBuilder()
			: base("priceplan", "delete")
		{
		}

		public PricePlanDeleteRequestBuilder(long id)
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

	public class PricePlanListRequestBuilder : RequestBuilder<ListResponse<PricePlan>>
	{
		#region Constants
		public const string FILTER = "filter";
		#endregion

		public PricePlanFilter Filter { get; set; }

		public PricePlanListRequestBuilder()
			: base("priceplan", "list")
		{
		}

		public PricePlanListRequestBuilder(PricePlanFilter filter)
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

		public override object Deserialize(JToken result)
		{
			return ObjectFactory.Create<ListResponse<PricePlan>>(result);
		}
	}

	public class PricePlanUpdateRequestBuilder : RequestBuilder<PricePlan>
	{
		#region Constants
		public const string ID = "id";
		public const string PRICE_PLAN = "pricePlan";
		#endregion

		public long Id { get; set; }
		public PricePlan PricePlan { get; set; }

		public PricePlanUpdateRequestBuilder()
			: base("priceplan", "update")
		{
		}

		public PricePlanUpdateRequestBuilder(long id, PricePlan pricePlan)
			: this()
		{
			this.Id = id;
			this.PricePlan = pricePlan;
		}

		public override Params getParameters(bool includeServiceAndAction)
		{
			Params kparams = base.getParameters(includeServiceAndAction);
			if (!isMapped("id"))
				kparams.AddIfNotNull("id", Id);
			if (!isMapped("pricePlan"))
				kparams.AddIfNotNull("pricePlan", PricePlan);
			return kparams;
		}

		public override Files getFiles()
		{
			Files kfiles = base.getFiles();
			return kfiles;
		}

		public override object Deserialize(JToken result)
		{
			return ObjectFactory.Create<PricePlan>(result);
		}
	}


	public class PricePlanService
	{
		private PricePlanService()
		{
		}

		public static PricePlanAddRequestBuilder Add(PricePlan pricePlan)
		{
			return new PricePlanAddRequestBuilder(pricePlan);
		}

		public static PricePlanDeleteRequestBuilder Delete(long id)
		{
			return new PricePlanDeleteRequestBuilder(id);
		}

		public static PricePlanListRequestBuilder List(PricePlanFilter filter = null)
		{
			return new PricePlanListRequestBuilder(filter);
		}

		public static PricePlanUpdateRequestBuilder Update(long id, PricePlan pricePlan)
		{
			return new PricePlanUpdateRequestBuilder(id, pricePlan);
		}
	}
}
