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
	public class PlaybackProfileAddRequestBuilder : RequestBuilder<PlaybackProfile>
	{
		#region Constants
		public const string PLAYBACK_PROFILE = "playbackProfile";
		#endregion

		public PlaybackProfile PlaybackProfile { get; set; }

		public PlaybackProfileAddRequestBuilder()
			: base("playbackprofile", "add")
		{
		}

		public PlaybackProfileAddRequestBuilder(PlaybackProfile playbackProfile)
			: this()
		{
			this.PlaybackProfile = playbackProfile;
		}

		public override Params getParameters(bool includeServiceAndAction)
		{
			Params kparams = base.getParameters(includeServiceAndAction);
			if (!isMapped("playbackProfile"))
				kparams.AddIfNotNull("playbackProfile", PlaybackProfile);
			return kparams;
		}

		public override Files getFiles()
		{
			Files kfiles = base.getFiles();
			return kfiles;
		}

		public override object Deserialize(JToken result)
		{
			return ObjectFactory.Create<PlaybackProfile>(result);
		}
	}

	public class PlaybackProfileDeleteRequestBuilder : RequestBuilder<bool>
	{
		#region Constants
		public const string ID = "id";
		#endregion

		public int Id { get; set; }

		public PlaybackProfileDeleteRequestBuilder()
			: base("playbackprofile", "delete")
		{
		}

		public PlaybackProfileDeleteRequestBuilder(int id)
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

	public class PlaybackProfileGenerateSharedSecretRequestBuilder : RequestBuilder<PlaybackProfile>
	{
		#region Constants
		public const string ID = "id";
		#endregion

		public int Id { get; set; }

		public PlaybackProfileGenerateSharedSecretRequestBuilder()
			: base("playbackprofile", "generateSharedSecret")
		{
		}

		public PlaybackProfileGenerateSharedSecretRequestBuilder(int id)
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
			return ObjectFactory.Create<PlaybackProfile>(result);
		}
	}

	public class PlaybackProfileListRequestBuilder : RequestBuilder<ListResponse<PlaybackProfile>>
	{
		#region Constants
		public const string FILTER = "filter";
		#endregion

		public PlaybackProfileFilter Filter { get; set; }

		public PlaybackProfileListRequestBuilder()
			: base("playbackprofile", "list")
		{
		}

		public PlaybackProfileListRequestBuilder(PlaybackProfileFilter filter)
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
			return ObjectFactory.Create<ListResponse<PlaybackProfile>>(result);
		}
	}

	public class PlaybackProfileUpdateRequestBuilder : RequestBuilder<PlaybackProfile>
	{
		#region Constants
		public const string ID = "id";
		public const string PLAYBACK_PROFILE = "playbackProfile";
		#endregion

		public int Id { get; set; }
		public PlaybackProfile PlaybackProfile { get; set; }

		public PlaybackProfileUpdateRequestBuilder()
			: base("playbackprofile", "update")
		{
		}

		public PlaybackProfileUpdateRequestBuilder(int id, PlaybackProfile playbackProfile)
			: this()
		{
			this.Id = id;
			this.PlaybackProfile = playbackProfile;
		}

		public override Params getParameters(bool includeServiceAndAction)
		{
			Params kparams = base.getParameters(includeServiceAndAction);
			if (!isMapped("id"))
				kparams.AddIfNotNull("id", Id);
			if (!isMapped("playbackProfile"))
				kparams.AddIfNotNull("playbackProfile", PlaybackProfile);
			return kparams;
		}

		public override Files getFiles()
		{
			Files kfiles = base.getFiles();
			return kfiles;
		}

		public override object Deserialize(JToken result)
		{
			return ObjectFactory.Create<PlaybackProfile>(result);
		}
	}


	public class PlaybackProfileService
	{
		private PlaybackProfileService()
		{
		}

		public static PlaybackProfileAddRequestBuilder Add(PlaybackProfile playbackProfile)
		{
			return new PlaybackProfileAddRequestBuilder(playbackProfile);
		}

		public static PlaybackProfileDeleteRequestBuilder Delete(int id)
		{
			return new PlaybackProfileDeleteRequestBuilder(id);
		}

		public static PlaybackProfileGenerateSharedSecretRequestBuilder GenerateSharedSecret(int id)
		{
			return new PlaybackProfileGenerateSharedSecretRequestBuilder(id);
		}

		public static PlaybackProfileListRequestBuilder List(PlaybackProfileFilter filter = null)
		{
			return new PlaybackProfileListRequestBuilder(filter);
		}

		public static PlaybackProfileUpdateRequestBuilder Update(int id, PlaybackProfile playbackProfile)
		{
			return new PlaybackProfileUpdateRequestBuilder(id, playbackProfile);
		}
	}
}
