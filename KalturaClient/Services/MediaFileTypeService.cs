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
	public class MediaFileTypeAddRequestBuilder : RequestBuilder<MediaFileType>
	{
		#region Constants
		public const string MEDIA_FILE_TYPE = "mediaFileType";
		#endregion

		public MediaFileType MediaFileType
		{
			set;
			get;
		}

		public MediaFileTypeAddRequestBuilder()
			: base("mediafiletype", "add")
		{
		}

		public MediaFileTypeAddRequestBuilder(MediaFileType mediaFileType)
			: this()
		{
			this.MediaFileType = mediaFileType;
		}

		public override Params getParameters(bool includeServiceAndAction)
		{
			Params kparams = base.getParameters(includeServiceAndAction);
			if (!isMapped("mediaFileType"))
				kparams.AddIfNotNull("mediaFileType", MediaFileType);
			return kparams;
		}

		public override Files getFiles()
		{
			Files kfiles = base.getFiles();
			return kfiles;
		}

		public override object Deserialize(JToken result)
		{
			return ObjectFactory.Create<MediaFileType>(result);
		}
	}

	public class MediaFileTypeDeleteRequestBuilder : RequestBuilder<bool>
	{
		#region Constants
		public const string ID = "id";
		#endregion

		public int Id
		{
			set;
			get;
		}

		public MediaFileTypeDeleteRequestBuilder()
			: base("mediafiletype", "delete")
		{
		}

		public MediaFileTypeDeleteRequestBuilder(int id)
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

	public class MediaFileTypeListRequestBuilder : RequestBuilder<ListResponse<MediaFileType>>
	{
		#region Constants
		#endregion


		public MediaFileTypeListRequestBuilder()
			: base("mediafiletype", "list")
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
			return ObjectFactory.Create<ListResponse<MediaFileType>>(result);
		}
	}

	public class MediaFileTypeUpdateRequestBuilder : RequestBuilder<MediaFileType>
	{
		#region Constants
		public const string ID = "id";
		public const string MEDIA_FILE_TYPE = "mediaFileType";
		#endregion

		public int Id
		{
			set;
			get;
		}
		public MediaFileType MediaFileType
		{
			set;
			get;
		}

		public MediaFileTypeUpdateRequestBuilder()
			: base("mediafiletype", "update")
		{
		}

		public MediaFileTypeUpdateRequestBuilder(int id, MediaFileType mediaFileType)
			: this()
		{
			this.Id = id;
			this.MediaFileType = mediaFileType;
		}

		public override Params getParameters(bool includeServiceAndAction)
		{
			Params kparams = base.getParameters(includeServiceAndAction);
			if (!isMapped("id"))
				kparams.AddIfNotNull("id", Id);
			if (!isMapped("mediaFileType"))
				kparams.AddIfNotNull("mediaFileType", MediaFileType);
			return kparams;
		}

		public override Files getFiles()
		{
			Files kfiles = base.getFiles();
			return kfiles;
		}

		public override object Deserialize(JToken result)
		{
			return ObjectFactory.Create<MediaFileType>(result);
		}
	}


	public class MediaFileTypeService
	{
		private MediaFileTypeService()
		{
		}

		public static MediaFileTypeAddRequestBuilder Add(MediaFileType mediaFileType)
		{
			return new MediaFileTypeAddRequestBuilder(mediaFileType);
		}

		public static MediaFileTypeDeleteRequestBuilder Delete(int id)
		{
			return new MediaFileTypeDeleteRequestBuilder(id);
		}

		public static MediaFileTypeListRequestBuilder List()
		{
			return new MediaFileTypeListRequestBuilder();
		}

		public static MediaFileTypeUpdateRequestBuilder Update(int id, MediaFileType mediaFileType)
		{
			return new MediaFileTypeUpdateRequestBuilder(id, mediaFileType);
		}
	}
}
