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
using Newtonsoft.Json.Linq;

namespace Kaltura.Services
{
	public class MediaFileAddRequestBuilder : RequestBuilder<MediaFile>
	{
		#region Constants
		public const string MEDIA_FILE = "mediaFile";
		#endregion

		public MediaFile MediaFile
		{
			set;
			get;
		}

		public MediaFileAddRequestBuilder()
			: base("mediafile", "add")
		{
		}

		public MediaFileAddRequestBuilder(MediaFile mediaFile)
			: this()
		{
			this.MediaFile = mediaFile;
		}

		public override Params getParameters(bool includeServiceAndAction)
		{
			Params kparams = base.getParameters(includeServiceAndAction);
			if (!isMapped("mediaFile"))
				kparams.AddIfNotNull("mediaFile", MediaFile);
			return kparams;
		}

		public override Files getFiles()
		{
			Files kfiles = base.getFiles();
			return kfiles;
		}

		public override object Deserialize(JToken result)
		{
			return ObjectFactory.Create<MediaFile>(result);
		}
	}

	public class MediaFileDeleteRequestBuilder : RequestBuilder<bool>
	{
		#region Constants
		public const string ID = "id";
		#endregion

		public long Id
		{
			set;
			get;
		}

		public MediaFileDeleteRequestBuilder()
			: base("mediafile", "delete")
		{
		}

		public MediaFileDeleteRequestBuilder(long id)
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

	public class MediaFileListRequestBuilder : RequestBuilder<ListResponse<MediaFile>>
	{
		#region Constants
		public const string FILTER = "filter";
		#endregion

		public MediaFileFilter Filter
		{
			set;
			get;
		}

		public MediaFileListRequestBuilder()
			: base("mediafile", "list")
		{
		}

		public MediaFileListRequestBuilder(MediaFileFilter filter)
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
			return ObjectFactory.Create<ListResponse<MediaFile>>(result);
		}
	}

	public class MediaFileUpdateRequestBuilder : RequestBuilder<MediaFile>
	{
		#region Constants
		public const string ID = "id";
		public const string MEDIA_FILE = "mediaFile";
		#endregion

		public long Id
		{
			set;
			get;
		}
		public MediaFile MediaFile
		{
			set;
			get;
		}

		public MediaFileUpdateRequestBuilder()
			: base("mediafile", "update")
		{
		}

		public MediaFileUpdateRequestBuilder(long id, MediaFile mediaFile)
			: this()
		{
			this.Id = id;
			this.MediaFile = mediaFile;
		}

		public override Params getParameters(bool includeServiceAndAction)
		{
			Params kparams = base.getParameters(includeServiceAndAction);
			if (!isMapped("id"))
				kparams.AddIfNotNull("id", Id);
			if (!isMapped("mediaFile"))
				kparams.AddIfNotNull("mediaFile", MediaFile);
			return kparams;
		}

		public override Files getFiles()
		{
			Files kfiles = base.getFiles();
			return kfiles;
		}

		public override object Deserialize(JToken result)
		{
			return ObjectFactory.Create<MediaFile>(result);
		}
	}


	public class MediaFileService
	{
		private MediaFileService()
		{
		}

		public static MediaFileAddRequestBuilder Add(MediaFile mediaFile)
		{
			return new MediaFileAddRequestBuilder(mediaFile);
		}

		public static MediaFileDeleteRequestBuilder Delete(long id)
		{
			return new MediaFileDeleteRequestBuilder(id);
		}

		public static MediaFileListRequestBuilder List(MediaFileFilter filter = null)
		{
			return new MediaFileListRequestBuilder(filter);
		}

		public static MediaFileUpdateRequestBuilder Update(long id, MediaFile mediaFile)
		{
			return new MediaFileUpdateRequestBuilder(id, mediaFile);
		}
	}
}
