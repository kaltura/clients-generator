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
	public class CampaignAddRequestBuilder : RequestBuilder<Campaign>
	{
		#region Constants
		public const string OBJECT_TO_ADD = "objectToAdd";
		#endregion

		public Campaign ObjectToAdd { get; set; }

		public CampaignAddRequestBuilder()
			: base("campaign", "add")
		{
		}

		public CampaignAddRequestBuilder(Campaign objectToAdd)
			: this()
		{
			this.ObjectToAdd = objectToAdd;
		}

		public override Params getParameters(bool includeServiceAndAction)
		{
			Params kparams = base.getParameters(includeServiceAndAction);
			if (!isMapped("objectToAdd"))
				kparams.AddIfNotNull("objectToAdd", ObjectToAdd);
			return kparams;
		}

		public override Files getFiles()
		{
			Files kfiles = base.getFiles();
			return kfiles;
		}

		public override object Deserialize(JToken result)
		{
			return ObjectFactory.Create<Campaign>(result);
		}
	}

	public class CampaignUpdateRequestBuilder : RequestBuilder<Campaign>
	{
		#region Constants
		public const string ID = "id";
		public const string OBJECT_TO_UPDATE = "objectToUpdate";
		#endregion

		public long Id { get; set; }
		public Campaign ObjectToUpdate { get; set; }

		public CampaignUpdateRequestBuilder()
			: base("campaign", "update")
		{
		}

		public CampaignUpdateRequestBuilder(long id, Campaign objectToUpdate)
			: this()
		{
			this.Id = id;
			this.ObjectToUpdate = objectToUpdate;
		}

		public override Params getParameters(bool includeServiceAndAction)
		{
			Params kparams = base.getParameters(includeServiceAndAction);
			if (!isMapped("id"))
				kparams.AddIfNotNull("id", Id);
			if (!isMapped("objectToUpdate"))
				kparams.AddIfNotNull("objectToUpdate", ObjectToUpdate);
			return kparams;
		}

		public override Files getFiles()
		{
			Files kfiles = base.getFiles();
			return kfiles;
		}

		public override object Deserialize(JToken result)
		{
			return ObjectFactory.Create<Campaign>(result);
		}
	}

	public class CampaignDeleteRequestBuilder : RequestBuilder<VoidResponse>
	{
		#region Constants
		public const string ID = "id";
		#endregion

		public long Id { get; set; }

		public CampaignDeleteRequestBuilder()
			: base("campaign", "delete")
		{
		}

		public CampaignDeleteRequestBuilder(long id)
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
			return null;
		}
	}

	public class CampaignListRequestBuilder : RequestBuilder<ListResponse<Campaign>>
	{
		#region Constants
		public const string FILTER = "filter";
		#endregion

		public CampaignFilter Filter { get; set; }

		public CampaignListRequestBuilder()
			: base("campaign", "list")
		{
		}

		public CampaignListRequestBuilder(CampaignFilter filter)
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
			return ObjectFactory.Create<ListResponse<Campaign>>(result);
		}
	}

	public class CampaignSetStateRequestBuilder : RequestBuilder<VoidResponse>
	{
		#region Constants
		public const string CAMPAIGN_ID = "campaignId";
		public const string NEW_STATE = "newState";
		#endregion

		public long CampaignId { get; set; }
		public ObjectState NewState { get; set; }

		public CampaignSetStateRequestBuilder()
			: base("campaign", "setState")
		{
		}

		public CampaignSetStateRequestBuilder(long campaignId, ObjectState newState)
			: this()
		{
			this.CampaignId = campaignId;
			this.NewState = newState;
		}

		public override Params getParameters(bool includeServiceAndAction)
		{
			Params kparams = base.getParameters(includeServiceAndAction);
			if (!isMapped("campaignId"))
				kparams.AddIfNotNull("campaignId", CampaignId);
			if (!isMapped("newState"))
				kparams.AddIfNotNull("newState", NewState);
			return kparams;
		}

		public override Files getFiles()
		{
			Files kfiles = base.getFiles();
			return kfiles;
		}

		public override object Deserialize(JToken result)
		{
			return null;
		}
	}


	public class CampaignService
	{
		private CampaignService()
		{
		}

		public static CampaignAddRequestBuilder Add(Campaign objectToAdd)
		{
			return new CampaignAddRequestBuilder(objectToAdd);
		}

		public static CampaignUpdateRequestBuilder Update(long id, Campaign objectToUpdate)
		{
			return new CampaignUpdateRequestBuilder(id, objectToUpdate);
		}

		public static CampaignDeleteRequestBuilder Delete(long id)
		{
			return new CampaignDeleteRequestBuilder(id);
		}

		public static CampaignListRequestBuilder List(CampaignFilter filter)
		{
			return new CampaignListRequestBuilder(filter);
		}

		public static CampaignSetStateRequestBuilder SetState(long campaignId, ObjectState newState)
		{
			return new CampaignSetStateRequestBuilder(campaignId, newState);
		}
	}
}
