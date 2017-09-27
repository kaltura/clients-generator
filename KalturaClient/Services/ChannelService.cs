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
	public class ChannelAddRequestBuilder : RequestBuilder<Channel>
	{
		#region Constants
		public const string CHANNEL = "channel";
		#endregion

		public Channel Channel
		{
			set;
			get;
		}

		public ChannelAddRequestBuilder()
			: base("channel", "add")
		{
		}

		public ChannelAddRequestBuilder(Channel channel)
			: this()
		{
			this.Channel = channel;
		}

		public override Params getParameters(bool includeServiceAndAction)
		{
			Params kparams = base.getParameters(includeServiceAndAction);
			if (!isMapped("channel"))
				kparams.AddIfNotNull("channel", Channel);
			return kparams;
		}

		public override Files getFiles()
		{
			Files kfiles = base.getFiles();
			return kfiles;
		}

		public override object Deserialize(XmlElement result)
		{
			return ObjectFactory.Create<Channel>(result);
		}
	}

	public class ChannelDeleteRequestBuilder : RequestBuilder<bool>
	{
		#region Constants
		public const string CHANNEL_ID = "channelId";
		#endregion

		public int ChannelId
		{
			set;
			get;
		}

		public ChannelDeleteRequestBuilder()
			: base("channel", "delete")
		{
		}

		public ChannelDeleteRequestBuilder(int channelId)
			: this()
		{
			this.ChannelId = channelId;
		}

		public override Params getParameters(bool includeServiceAndAction)
		{
			Params kparams = base.getParameters(includeServiceAndAction);
			if (!isMapped("channelId"))
				kparams.AddIfNotNull("channelId", ChannelId);
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

	public class ChannelGetRequestBuilder : RequestBuilder<Channel>
	{
		#region Constants
		public const string ID = "id";
		#endregion

		public int Id
		{
			set;
			get;
		}

		public ChannelGetRequestBuilder()
			: base("channel", "get")
		{
		}

		public ChannelGetRequestBuilder(int id)
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
			return ObjectFactory.Create<Channel>(result);
		}
	}

	public class ChannelUpdateRequestBuilder : RequestBuilder<Channel>
	{
		#region Constants
		public const string CHANNEL_ID = "channelId";
		public const string CHANNEL = "channel";
		#endregion

		public int ChannelId
		{
			set;
			get;
		}
		public Channel Channel
		{
			set;
			get;
		}

		public ChannelUpdateRequestBuilder()
			: base("channel", "update")
		{
		}

		public ChannelUpdateRequestBuilder(int channelId, Channel channel)
			: this()
		{
			this.ChannelId = channelId;
			this.Channel = channel;
		}

		public override Params getParameters(bool includeServiceAndAction)
		{
			Params kparams = base.getParameters(includeServiceAndAction);
			if (!isMapped("channelId"))
				kparams.AddIfNotNull("channelId", ChannelId);
			if (!isMapped("channel"))
				kparams.AddIfNotNull("channel", Channel);
			return kparams;
		}

		public override Files getFiles()
		{
			Files kfiles = base.getFiles();
			return kfiles;
		}

		public override object Deserialize(XmlElement result)
		{
			return ObjectFactory.Create<Channel>(result);
		}
	}


	public class ChannelService
	{
		private ChannelService()
		{
		}

		public static ChannelAddRequestBuilder Add(Channel channel)
		{
			return new ChannelAddRequestBuilder(channel);
		}

		public static ChannelDeleteRequestBuilder Delete(int channelId)
		{
			return new ChannelDeleteRequestBuilder(channelId);
		}

		public static ChannelGetRequestBuilder Get(int id)
		{
			return new ChannelGetRequestBuilder(id);
		}

		public static ChannelUpdateRequestBuilder Update(int channelId, Channel channel)
		{
			return new ChannelUpdateRequestBuilder(channelId, channel);
		}
	}
}
