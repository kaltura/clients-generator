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
	public class IotProfileAddRequestBuilder : RequestBuilder<IotProfile>
	{
		#region Constants
		public const string OBJECT_TO_ADD = "objectToAdd";
		#endregion

		public IotProfile ObjectToAdd { get; set; }

		public IotProfileAddRequestBuilder()
			: base("iotprofile", "add")
		{
		}

		public IotProfileAddRequestBuilder(IotProfile objectToAdd)
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
			return ObjectFactory.Create<IotProfile>(result);
		}
	}

	public class IotProfileUpdateRequestBuilder : RequestBuilder<IotProfile>
	{
		#region Constants
		public const string ID = "id";
		public const string OBJECT_TO_UPDATE = "objectToUpdate";
		#endregion

		public long Id { get; set; }
		public IotProfile ObjectToUpdate { get; set; }

		public IotProfileUpdateRequestBuilder()
			: base("iotprofile", "update")
		{
		}

		public IotProfileUpdateRequestBuilder(long id, IotProfile objectToUpdate)
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
			return ObjectFactory.Create<IotProfile>(result);
		}
	}

	public class IotProfileGetRequestBuilder : RequestBuilder<IotProfile>
	{
		#region Constants
		public const string ID = "id";
		#endregion

		public long Id { get; set; }

		public IotProfileGetRequestBuilder()
			: base("iotprofile", "get")
		{
		}

		public IotProfileGetRequestBuilder(long id)
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
			return ObjectFactory.Create<IotProfile>(result);
		}
	}


	public class IotProfileService
	{
		private IotProfileService()
		{
		}

		public static IotProfileAddRequestBuilder Add(IotProfile objectToAdd)
		{
			return new IotProfileAddRequestBuilder(objectToAdd);
		}

		public static IotProfileUpdateRequestBuilder Update(long id, IotProfile objectToUpdate)
		{
			return new IotProfileUpdateRequestBuilder(id, objectToUpdate);
		}

		public static IotProfileGetRequestBuilder Get(long id)
		{
			return new IotProfileGetRequestBuilder(id);
		}
	}
}
