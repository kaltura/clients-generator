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
	public class ImageAddRequestBuilder : RequestBuilder<Image>
	{
		#region Constants
		public const string IMAGE = "image";
		#endregion

		public Image Image { get; set; }

		public ImageAddRequestBuilder()
			: base("image", "add")
		{
		}

		public ImageAddRequestBuilder(Image image)
			: this()
		{
			this.Image = image;
		}

		public override Params getParameters(bool includeServiceAndAction)
		{
			Params kparams = base.getParameters(includeServiceAndAction);
			if (!isMapped("image"))
				kparams.AddIfNotNull("image", Image);
			return kparams;
		}

		public override Files getFiles()
		{
			Files kfiles = base.getFiles();
			return kfiles;
		}

		public override object Deserialize(JToken result)
		{
			return ObjectFactory.Create<Image>(result);
		}
	}

	public class ImageDeleteRequestBuilder : RequestBuilder<bool>
	{
		#region Constants
		public const string ID = "id";
		#endregion

		public long Id { get; set; }

		public ImageDeleteRequestBuilder()
			: base("image", "delete")
		{
		}

		public ImageDeleteRequestBuilder(long id)
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

	public class ImageListRequestBuilder : RequestBuilder<ListResponse<Image>>
	{
		#region Constants
		public const string FILTER = "filter";
		#endregion

		public ImageFilter Filter { get; set; }

		public ImageListRequestBuilder()
			: base("image", "list")
		{
		}

		public ImageListRequestBuilder(ImageFilter filter)
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
			return ObjectFactory.Create<ListResponse<Image>>(result);
		}
	}

	public class ImageSetContentRequestBuilder : RequestBuilder<VoidResponse>
	{
		#region Constants
		public const string ID = "id";
		public const string CONTENT = "content";
		#endregion

		public long Id { get; set; }
		public ContentResource Content { get; set; }

		public ImageSetContentRequestBuilder()
			: base("image", "setContent")
		{
		}

		public ImageSetContentRequestBuilder(long id, ContentResource content)
			: this()
		{
			this.Id = id;
			this.Content = content;
		}

		public override Params getParameters(bool includeServiceAndAction)
		{
			Params kparams = base.getParameters(includeServiceAndAction);
			if (!isMapped("id"))
				kparams.AddIfNotNull("id", Id);
			if (!isMapped("content"))
				kparams.AddIfNotNull("content", Content);
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


	public class ImageService
	{
		private ImageService()
		{
		}

		public static ImageAddRequestBuilder Add(Image image)
		{
			return new ImageAddRequestBuilder(image);
		}

		public static ImageDeleteRequestBuilder Delete(long id)
		{
			return new ImageDeleteRequestBuilder(id);
		}

		public static ImageListRequestBuilder List(ImageFilter filter)
		{
			return new ImageListRequestBuilder(filter);
		}

		public static ImageSetContentRequestBuilder SetContent(long id, ContentResource content)
		{
			return new ImageSetContentRequestBuilder(id, content);
		}
	}
}
