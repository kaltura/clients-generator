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
// Copyright (C) 2006-2019  Kaltura Inc.
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
	public class HouseholdAddRequestBuilder : RequestBuilder<Household>
	{
		#region Constants
		public const string HOUSEHOLD = "household";
		#endregion

		public Household Household { get; set; }

		public HouseholdAddRequestBuilder()
			: base("household", "add")
		{
		}

		public HouseholdAddRequestBuilder(Household household)
			: this()
		{
			this.Household = household;
		}

		public override Params getParameters(bool includeServiceAndAction)
		{
			Params kparams = base.getParameters(includeServiceAndAction);
			if (!isMapped("household"))
				kparams.AddIfNotNull("household", Household);
			return kparams;
		}

		public override Files getFiles()
		{
			Files kfiles = base.getFiles();
			return kfiles;
		}

		public override object Deserialize(JToken result)
		{
			return ObjectFactory.Create<Household>(result);
		}
	}

	public class HouseholdDeleteRequestBuilder : RequestBuilder<bool>
	{
		#region Constants
		public const string ID = "id";
		#endregion

		public int Id { get; set; }

		public HouseholdDeleteRequestBuilder()
			: base("household", "delete")
		{
		}

		public HouseholdDeleteRequestBuilder(int id)
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

	public class HouseholdGetRequestBuilder : RequestBuilder<Household>
	{
		#region Constants
		public const string ID = "id";
		#endregion

		public int Id { get; set; }

		public HouseholdGetRequestBuilder()
			: base("household", "get")
		{
		}

		public HouseholdGetRequestBuilder(int id)
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
			return ObjectFactory.Create<Household>(result);
		}
	}

	public class HouseholdPurgeRequestBuilder : RequestBuilder<bool>
	{
		#region Constants
		public const string ID = "id";
		#endregion

		public int Id { get; set; }

		public HouseholdPurgeRequestBuilder()
			: base("household", "purge")
		{
		}

		public HouseholdPurgeRequestBuilder(int id)
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

	public class HouseholdResetFrequencyRequestBuilder : RequestBuilder<Household>
	{
		#region Constants
		public const string FREQUENCY_TYPE = "frequencyType";
		#endregion

		public HouseholdFrequencyType FrequencyType { get; set; }

		public HouseholdResetFrequencyRequestBuilder()
			: base("household", "resetFrequency")
		{
		}

		public HouseholdResetFrequencyRequestBuilder(HouseholdFrequencyType frequencyType)
			: this()
		{
			this.FrequencyType = frequencyType;
		}

		public override Params getParameters(bool includeServiceAndAction)
		{
			Params kparams = base.getParameters(includeServiceAndAction);
			if (!isMapped("frequencyType"))
				kparams.AddIfNotNull("frequencyType", FrequencyType);
			return kparams;
		}

		public override Files getFiles()
		{
			Files kfiles = base.getFiles();
			return kfiles;
		}

		public override object Deserialize(JToken result)
		{
			return ObjectFactory.Create<Household>(result);
		}
	}

	public class HouseholdResumeRequestBuilder : RequestBuilder<bool>
	{
		#region Constants
		#endregion


		public HouseholdResumeRequestBuilder()
			: base("household", "resume")
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
			if (result.Value<string>().Equals("1") || result.Value<string>().ToLower().Equals("true"))
				return true;
			return false;
		}
	}

	public class HouseholdSuspendRequestBuilder : RequestBuilder<bool>
	{
		#region Constants
		public const string ROLE_ID = "roleId";
		#endregion

		public int RoleId { get; set; }

		public HouseholdSuspendRequestBuilder()
			: base("household", "suspend")
		{
		}

		public HouseholdSuspendRequestBuilder(int roleId)
			: this()
		{
			this.RoleId = roleId;
		}

		public override Params getParameters(bool includeServiceAndAction)
		{
			Params kparams = base.getParameters(includeServiceAndAction);
			if (!isMapped("roleId"))
				kparams.AddIfNotNull("roleId", RoleId);
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

	public class HouseholdUpdateRequestBuilder : RequestBuilder<Household>
	{
		#region Constants
		public const string HOUSEHOLD = "household";
		#endregion

		public Household Household { get; set; }

		public HouseholdUpdateRequestBuilder()
			: base("household", "update")
		{
		}

		public HouseholdUpdateRequestBuilder(Household household)
			: this()
		{
			this.Household = household;
		}

		public override Params getParameters(bool includeServiceAndAction)
		{
			Params kparams = base.getParameters(includeServiceAndAction);
			if (!isMapped("household"))
				kparams.AddIfNotNull("household", Household);
			return kparams;
		}

		public override Files getFiles()
		{
			Files kfiles = base.getFiles();
			return kfiles;
		}

		public override object Deserialize(JToken result)
		{
			return ObjectFactory.Create<Household>(result);
		}
	}


	public class HouseholdService
	{
		private HouseholdService()
		{
		}

		public static HouseholdAddRequestBuilder Add(Household household)
		{
			return new HouseholdAddRequestBuilder(household);
		}

		public static HouseholdDeleteRequestBuilder Delete(int id = Int32.MinValue)
		{
			return new HouseholdDeleteRequestBuilder(id);
		}

		public static HouseholdGetRequestBuilder Get(int id = Int32.MinValue)
		{
			return new HouseholdGetRequestBuilder(id);
		}

		public static HouseholdPurgeRequestBuilder Purge(int id)
		{
			return new HouseholdPurgeRequestBuilder(id);
		}

		public static HouseholdResetFrequencyRequestBuilder ResetFrequency(HouseholdFrequencyType frequencyType)
		{
			return new HouseholdResetFrequencyRequestBuilder(frequencyType);
		}

		public static HouseholdResumeRequestBuilder Resume()
		{
			return new HouseholdResumeRequestBuilder();
		}

		public static HouseholdSuspendRequestBuilder Suspend(int roleId = Int32.MinValue)
		{
			return new HouseholdSuspendRequestBuilder(roleId);
		}

		public static HouseholdUpdateRequestBuilder Update(Household household)
		{
			return new HouseholdUpdateRequestBuilder(household);
		}
	}
}
