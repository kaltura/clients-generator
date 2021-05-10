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
	public class PriceDetailsAddRequestBuilder : RequestBuilder<PriceDetails>
	{
		#region Constants
		public const string PRICE_DETAILS = "priceDetails";
		#endregion

		public PriceDetails PriceDetails { get; set; }

		public PriceDetailsAddRequestBuilder()
			: base("pricedetails", "add")
		{
		}

		public PriceDetailsAddRequestBuilder(PriceDetails priceDetails)
			: this()
		{
			this.PriceDetails = priceDetails;
		}

		public override Params getParameters(bool includeServiceAndAction)
		{
			Params kparams = base.getParameters(includeServiceAndAction);
			if (!isMapped("priceDetails"))
				kparams.AddIfNotNull("priceDetails", PriceDetails);
			return kparams;
		}

		public override Files getFiles()
		{
			Files kfiles = base.getFiles();
			return kfiles;
		}

		public override object Deserialize(JToken result)
		{
			return ObjectFactory.Create<PriceDetails>(result);
		}
	}

	public class PriceDetailsDeleteRequestBuilder : RequestBuilder<bool>
	{
		#region Constants
		public const string ID = "id";
		#endregion

		public long Id { get; set; }

		public PriceDetailsDeleteRequestBuilder()
			: base("pricedetails", "delete")
		{
		}

		public PriceDetailsDeleteRequestBuilder(long id)
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

	public class PriceDetailsListRequestBuilder : RequestBuilder<ListResponse<PriceDetails>>
	{
		#region Constants
		public const string FILTER = "filter";
		#endregion

		public PriceDetailsFilter Filter { get; set; }

		public PriceDetailsListRequestBuilder()
			: base("pricedetails", "list")
		{
		}

		public PriceDetailsListRequestBuilder(PriceDetailsFilter filter)
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
			return ObjectFactory.Create<ListResponse<PriceDetails>>(result);
		}
	}


	public class PriceDetailsService
	{
		private PriceDetailsService()
		{
		}

		public static PriceDetailsAddRequestBuilder Add(PriceDetails priceDetails)
		{
			return new PriceDetailsAddRequestBuilder(priceDetails);
		}

		public static PriceDetailsDeleteRequestBuilder Delete(long id)
		{
			return new PriceDetailsDeleteRequestBuilder(id);
		}

		public static PriceDetailsListRequestBuilder List(PriceDetailsFilter filter = null)
		{
			return new PriceDetailsListRequestBuilder(filter);
		}
	}
}
