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
// Copyright (C) 2006-2017  Kaltura Inc.
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
	public class ExternalChannelProfileAddRequestBuilder : RequestBuilder<ExternalChannelProfile>
	{
		#region Constants
		public const string EXTERNAL_CHANNEL = "externalChannel";
		#endregion

		public ExternalChannelProfile ExternalChannel
		{
			set;
			get;
		}

		public ExternalChannelProfileAddRequestBuilder()
			: base("externalchannelprofile", "add")
		{
		}

		public ExternalChannelProfileAddRequestBuilder(ExternalChannelProfile externalChannel)
			: this()
		{
			this.ExternalChannel = externalChannel;
		}

		public override Params getParameters(bool includeServiceAndAction)
		{
			Params kparams = base.getParameters(includeServiceAndAction);
			if (!isMapped("externalChannel"))
				kparams.AddIfNotNull("externalChannel", ExternalChannel);
			return kparams;
		}

		public override Files getFiles()
		{
			Files kfiles = base.getFiles();
			return kfiles;
		}

		public override object Deserialize(XmlElement result)
		{
			return ObjectFactory.Create<ExternalChannelProfile>(result);
		}
	}

	public class ExternalChannelProfileDeleteRequestBuilder : RequestBuilder<bool>
	{
		#region Constants
		public const string EXTERNAL_CHANNEL_ID = "externalChannelId";
		#endregion

		public int ExternalChannelId
		{
			set;
			get;
		}

		public ExternalChannelProfileDeleteRequestBuilder()
			: base("externalchannelprofile", "delete")
		{
		}

		public ExternalChannelProfileDeleteRequestBuilder(int externalChannelId)
			: this()
		{
			this.ExternalChannelId = externalChannelId;
		}

		public override Params getParameters(bool includeServiceAndAction)
		{
			Params kparams = base.getParameters(includeServiceAndAction);
			if (!isMapped("externalChannelId"))
				kparams.AddIfNotNull("externalChannelId", ExternalChannelId);
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

	public class ExternalChannelProfileListRequestBuilder : RequestBuilder<ListResponse<ExternalChannelProfile>>
	{
		#region Constants
		#endregion


		public ExternalChannelProfileListRequestBuilder()
			: base("externalchannelprofile", "list")
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

		public override object Deserialize(XmlElement result)
		{
			return ObjectFactory.Create<ListResponse<ExternalChannelProfile>>(result);
		}
	}

	public class ExternalChannelProfileUpdateRequestBuilder : RequestBuilder<ExternalChannelProfile>
	{
		#region Constants
		public const string EXTERNAL_CHANNEL_ID = "externalChannelId";
		public const string EXTERNAL_CHANNEL = "externalChannel";
		#endregion

		public int ExternalChannelId
		{
			set;
			get;
		}
		public ExternalChannelProfile ExternalChannel
		{
			set;
			get;
		}

		public ExternalChannelProfileUpdateRequestBuilder()
			: base("externalchannelprofile", "update")
		{
		}

		public ExternalChannelProfileUpdateRequestBuilder(int externalChannelId, ExternalChannelProfile externalChannel)
			: this()
		{
			this.ExternalChannelId = externalChannelId;
			this.ExternalChannel = externalChannel;
		}

		public override Params getParameters(bool includeServiceAndAction)
		{
			Params kparams = base.getParameters(includeServiceAndAction);
			if (!isMapped("externalChannelId"))
				kparams.AddIfNotNull("externalChannelId", ExternalChannelId);
			if (!isMapped("externalChannel"))
				kparams.AddIfNotNull("externalChannel", ExternalChannel);
			return kparams;
		}

		public override Files getFiles()
		{
			Files kfiles = base.getFiles();
			return kfiles;
		}

		public override object Deserialize(XmlElement result)
		{
			return ObjectFactory.Create<ExternalChannelProfile>(result);
		}
	}


	public class ExternalChannelProfileService
	{
		private ExternalChannelProfileService()
		{
		}

		public static ExternalChannelProfileAddRequestBuilder Add(ExternalChannelProfile externalChannel)
		{
			return new ExternalChannelProfileAddRequestBuilder(externalChannel);
		}

		public static ExternalChannelProfileDeleteRequestBuilder Delete(int externalChannelId)
		{
			return new ExternalChannelProfileDeleteRequestBuilder(externalChannelId);
		}

		public static ExternalChannelProfileListRequestBuilder List()
		{
			return new ExternalChannelProfileListRequestBuilder();
		}

		public static ExternalChannelProfileUpdateRequestBuilder Update(int externalChannelId, ExternalChannelProfile externalChannel)
		{
			return new ExternalChannelProfileUpdateRequestBuilder(externalChannelId, externalChannel);
		}
	}
}
