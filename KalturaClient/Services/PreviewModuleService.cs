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
	public class PreviewModuleAddRequestBuilder : RequestBuilder<PreviewModule>
	{
		#region Constants
		public const string PREVIEW_MODULE = "previewModule";
		#endregion

		public PreviewModule PreviewModule { get; set; }

		public PreviewModuleAddRequestBuilder()
			: base("previewmodule", "add")
		{
		}

		public PreviewModuleAddRequestBuilder(PreviewModule previewModule)
			: this()
		{
			this.PreviewModule = previewModule;
		}

		public override Params getParameters(bool includeServiceAndAction)
		{
			Params kparams = base.getParameters(includeServiceAndAction);
			if (!isMapped("previewModule"))
				kparams.AddIfNotNull("previewModule", PreviewModule);
			return kparams;
		}

		public override Files getFiles()
		{
			Files kfiles = base.getFiles();
			return kfiles;
		}

		public override object Deserialize(JToken result)
		{
			return ObjectFactory.Create<PreviewModule>(result);
		}
	}

	public class PreviewModuleDeleteRequestBuilder : RequestBuilder<bool>
	{
		#region Constants
		public const string ID = "id";
		#endregion

		public long Id { get; set; }

		public PreviewModuleDeleteRequestBuilder()
			: base("previewmodule", "delete")
		{
		}

		public PreviewModuleDeleteRequestBuilder(long id)
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

	public class PreviewModuleListRequestBuilder : RequestBuilder<ListResponse<PreviewModule>>
	{
		#region Constants
		#endregion


		public PreviewModuleListRequestBuilder()
			: base("previewmodule", "list")
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
			return ObjectFactory.Create<ListResponse<PreviewModule>>(result);
		}
	}


	public class PreviewModuleService
	{
		private PreviewModuleService()
		{
		}

		public static PreviewModuleAddRequestBuilder Add(PreviewModule previewModule)
		{
			return new PreviewModuleAddRequestBuilder(previewModule);
		}

		public static PreviewModuleDeleteRequestBuilder Delete(long id)
		{
			return new PreviewModuleDeleteRequestBuilder(id);
		}

		public static PreviewModuleListRequestBuilder List()
		{
			return new PreviewModuleListRequestBuilder();
		}
	}
}
