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
	public class ImageTypeAddRequestBuilder : RequestBuilder<ImageType>
	{
		#region Constants
		public const string IMAGE_TYPE = "imageType";
		#endregion

		public ImageType ImageType
		{
			set;
			get;
		}

		public ImageTypeAddRequestBuilder()
			: base("imagetype", "add")
		{
		}

		public ImageTypeAddRequestBuilder(ImageType imageType)
			: this()
		{
			this.ImageType = imageType;
		}

		public override Params getParameters(bool includeServiceAndAction)
		{
			Params kparams = base.getParameters(includeServiceAndAction);
			if (!isMapped("imageType"))
				kparams.AddIfNotNull("imageType", ImageType);
			return kparams;
		}

		public override Files getFiles()
		{
			Files kfiles = base.getFiles();
			return kfiles;
		}

		public override object Deserialize(JToken result)
		{
			return ObjectFactory.Create<ImageType>(result);
		}
	}

	public class ImageTypeDeleteRequestBuilder : RequestBuilder<bool>
	{
		#region Constants
		public const string ID = "id";
		#endregion

		public long Id
		{
			set;
			get;
		}

		public ImageTypeDeleteRequestBuilder()
			: base("imagetype", "delete")
		{
		}

		public ImageTypeDeleteRequestBuilder(long id)
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

	public class ImageTypeListRequestBuilder : RequestBuilder<ListResponse<ImageType>>
	{
		#region Constants
		public const string FILTER = "filter";
		#endregion

		public ImageTypeFilter Filter
		{
			set;
			get;
		}

		public ImageTypeListRequestBuilder()
			: base("imagetype", "list")
		{
		}

		public ImageTypeListRequestBuilder(ImageTypeFilter filter)
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
			return ObjectFactory.Create<ListResponse<ImageType>>(result);
		}
	}

	public class ImageTypeUpdateRequestBuilder : RequestBuilder<ImageType>
	{
		#region Constants
		public const string ID = "id";
		public const string IMAGE_TYPE = "imageType";
		#endregion

		public long Id
		{
			set;
			get;
		}
		public ImageType ImageType
		{
			set;
			get;
		}

		public ImageTypeUpdateRequestBuilder()
			: base("imagetype", "update")
		{
		}

		public ImageTypeUpdateRequestBuilder(long id, ImageType imageType)
			: this()
		{
			this.Id = id;
			this.ImageType = imageType;
		}

		public override Params getParameters(bool includeServiceAndAction)
		{
			Params kparams = base.getParameters(includeServiceAndAction);
			if (!isMapped("id"))
				kparams.AddIfNotNull("id", Id);
			if (!isMapped("imageType"))
				kparams.AddIfNotNull("imageType", ImageType);
			return kparams;
		}

		public override Files getFiles()
		{
			Files kfiles = base.getFiles();
			return kfiles;
		}

		public override object Deserialize(JToken result)
		{
			return ObjectFactory.Create<ImageType>(result);
		}
	}


	public class ImageTypeService
	{
		private ImageTypeService()
		{
		}

		public static ImageTypeAddRequestBuilder Add(ImageType imageType)
		{
			return new ImageTypeAddRequestBuilder(imageType);
		}

		public static ImageTypeDeleteRequestBuilder Delete(long id)
		{
			return new ImageTypeDeleteRequestBuilder(id);
		}

		public static ImageTypeListRequestBuilder List(ImageTypeFilter filter = null)
		{
			return new ImageTypeListRequestBuilder(filter);
		}

		public static ImageTypeUpdateRequestBuilder Update(long id, ImageType imageType)
		{
			return new ImageTypeUpdateRequestBuilder(id, imageType);
		}
	}
}
