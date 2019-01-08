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
	public class SubscriptionSetAddRequestBuilder : RequestBuilder<SubscriptionSet>
	{
		#region Constants
		public const string SUBSCRIPTION_SET = "subscriptionSet";
		#endregion

		public SubscriptionSet SubscriptionSet
		{
			set;
			get;
		}

		public SubscriptionSetAddRequestBuilder()
			: base("subscriptionset", "add")
		{
		}

		public SubscriptionSetAddRequestBuilder(SubscriptionSet subscriptionSet)
			: this()
		{
			this.SubscriptionSet = subscriptionSet;
		}

		public override Params getParameters(bool includeServiceAndAction)
		{
			Params kparams = base.getParameters(includeServiceAndAction);
			if (!isMapped("subscriptionSet"))
				kparams.AddIfNotNull("subscriptionSet", SubscriptionSet);
			return kparams;
		}

		public override Files getFiles()
		{
			Files kfiles = base.getFiles();
			return kfiles;
		}

		public override object Deserialize(XmlElement result)
		{
			return ObjectFactory.Create<SubscriptionSet>(result);
		}
		public override object DeserializeObject(object result)
		{
			return ObjectFactory.Create<SubscriptionSet>((IDictionary<string,object>)result);
		}
	}

	public class SubscriptionSetDeleteRequestBuilder : RequestBuilder<bool>
	{
		#region Constants
		public const string ID = "id";
		#endregion

		public long Id
		{
			set;
			get;
		}

		public SubscriptionSetDeleteRequestBuilder()
			: base("subscriptionset", "delete")
		{
		}

		public SubscriptionSetDeleteRequestBuilder(long id)
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
		public override object DeserializeObject(object result)
		{
			var resultStr = (string)result;
			if (resultStr.Equals("1") || resultStr.ToLower().Equals("true"))
				return true;
			return false;
		}
	}

	public class SubscriptionSetGetRequestBuilder : RequestBuilder<SubscriptionSet>
	{
		#region Constants
		public const string ID = "id";
		#endregion

		public long Id
		{
			set;
			get;
		}

		public SubscriptionSetGetRequestBuilder()
			: base("subscriptionset", "get")
		{
		}

		public SubscriptionSetGetRequestBuilder(long id)
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
			return ObjectFactory.Create<SubscriptionSet>(result);
		}
		public override object DeserializeObject(object result)
		{
			return ObjectFactory.Create<SubscriptionSet>((IDictionary<string,object>)result);
		}
	}

	public class SubscriptionSetListRequestBuilder : RequestBuilder<ListResponse<SubscriptionSet>>
	{
		#region Constants
		public const string FILTER = "filter";
		#endregion

		public SubscriptionSetFilter Filter
		{
			set;
			get;
		}

		public SubscriptionSetListRequestBuilder()
			: base("subscriptionset", "list")
		{
		}

		public SubscriptionSetListRequestBuilder(SubscriptionSetFilter filter)
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
			return ObjectFactory.Create<ListResponse<SubscriptionSet>>(result);
		}
		public override object DeserializeObject(object result)
		{
			return ObjectFactory.Create<ListResponse<SubscriptionSet>>((IDictionary<string,object>)result);
		}
	}

	public class SubscriptionSetUpdateRequestBuilder : RequestBuilder<SubscriptionSet>
	{
		#region Constants
		public const string ID = "id";
		public const string SUBSCRIPTION_SET = "subscriptionSet";
		#endregion

		public long Id
		{
			set;
			get;
		}
		public SubscriptionSet SubscriptionSet
		{
			set;
			get;
		}

		public SubscriptionSetUpdateRequestBuilder()
			: base("subscriptionset", "update")
		{
		}

		public SubscriptionSetUpdateRequestBuilder(long id, SubscriptionSet subscriptionSet)
			: this()
		{
			this.Id = id;
			this.SubscriptionSet = subscriptionSet;
		}

		public override Params getParameters(bool includeServiceAndAction)
		{
			Params kparams = base.getParameters(includeServiceAndAction);
			if (!isMapped("id"))
				kparams.AddIfNotNull("id", Id);
			if (!isMapped("subscriptionSet"))
				kparams.AddIfNotNull("subscriptionSet", SubscriptionSet);
			return kparams;
		}

		public override Files getFiles()
		{
			Files kfiles = base.getFiles();
			return kfiles;
		}

		public override object Deserialize(XmlElement result)
		{
			return ObjectFactory.Create<SubscriptionSet>(result);
		}
		public override object DeserializeObject(object result)
		{
			return ObjectFactory.Create<SubscriptionSet>((IDictionary<string,object>)result);
		}
	}


	public class SubscriptionSetService
	{
		private SubscriptionSetService()
		{
		}

		public static SubscriptionSetAddRequestBuilder Add(SubscriptionSet subscriptionSet)
		{
			return new SubscriptionSetAddRequestBuilder(subscriptionSet);
		}

		public static SubscriptionSetDeleteRequestBuilder Delete(long id)
		{
			return new SubscriptionSetDeleteRequestBuilder(id);
		}

		public static SubscriptionSetGetRequestBuilder Get(long id)
		{
			return new SubscriptionSetGetRequestBuilder(id);
		}

		public static SubscriptionSetListRequestBuilder List(SubscriptionSetFilter filter = null)
		{
			return new SubscriptionSetListRequestBuilder(filter);
		}

		public static SubscriptionSetUpdateRequestBuilder Update(long id, SubscriptionSet subscriptionSet)
		{
			return new SubscriptionSetUpdateRequestBuilder(id, subscriptionSet);
		}
	}
}
