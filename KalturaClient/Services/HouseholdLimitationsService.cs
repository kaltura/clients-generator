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
using System.IO;
using Kaltura.Request;
using Kaltura.Types;
using Kaltura.Enums;
using Newtonsoft.Json.Linq;

namespace Kaltura.Services
{
	public class HouseholdLimitationsGetRequestBuilder : RequestBuilder<HouseholdLimitations>
	{
		#region Constants
		public const string ID = "id";
		#endregion

		public int Id { get; set; }

		public HouseholdLimitationsGetRequestBuilder()
			: base("householdlimitations", "get")
		{
		}

		public HouseholdLimitationsGetRequestBuilder(int id)
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
			return ObjectFactory.Create<HouseholdLimitations>(result);
		}
	}

	public class HouseholdLimitationsListRequestBuilder : RequestBuilder<ListResponse<HouseholdLimitations>>
	{
		#region Constants
		#endregion


		public HouseholdLimitationsListRequestBuilder()
			: base("householdlimitations", "list")
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
			return ObjectFactory.Create<ListResponse<HouseholdLimitations>>(result);
		}
	}


	public class HouseholdLimitationsService
	{
		private HouseholdLimitationsService()
		{
		}

		public static HouseholdLimitationsGetRequestBuilder Get(int id)
		{
			return new HouseholdLimitationsGetRequestBuilder(id);
		}

		public static HouseholdLimitationsListRequestBuilder List()
		{
			return new HouseholdLimitationsListRequestBuilder();
		}
	}
}
