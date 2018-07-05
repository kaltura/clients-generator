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
	public class RatioAddRequestBuilder : RequestBuilder<Ratio>
	{
		#region Constants
		public const string RATIO = "ratio";
		#endregion

		public Ratio Ratio
		{
			set;
			get;
		}

		public RatioAddRequestBuilder()
			: base("ratio", "add")
		{
		}

		public RatioAddRequestBuilder(Ratio ratio)
			: this()
		{
			this.Ratio = ratio;
		}

		public override Params getParameters(bool includeServiceAndAction)
		{
			Params kparams = base.getParameters(includeServiceAndAction);
			if (!isMapped("ratio"))
				kparams.AddIfNotNull("ratio", Ratio);
			return kparams;
		}

		public override Files getFiles()
		{
			Files kfiles = base.getFiles();
			return kfiles;
		}

		public override object Deserialize(XmlElement result)
		{
			return ObjectFactory.Create<Ratio>(result);
		}
	}

	public class RatioListRequestBuilder : RequestBuilder<ListResponse<Ratio>>
	{
		#region Constants
		#endregion


		public RatioListRequestBuilder()
			: base("ratio", "list")
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
			return ObjectFactory.Create<ListResponse<Ratio>>(result);
		}
	}

	public class RatioUpdateRequestBuilder : RequestBuilder<Ratio>
	{
		#region Constants
		public const string ID = "id";
		public const string RATIO = "ratio";
		#endregion

		public long Id
		{
			set;
			get;
		}
		public Ratio Ratio
		{
			set;
			get;
		}

		public RatioUpdateRequestBuilder()
			: base("ratio", "update")
		{
		}

		public RatioUpdateRequestBuilder(long id, Ratio ratio)
			: this()
		{
			this.Id = id;
			this.Ratio = ratio;
		}

		public override Params getParameters(bool includeServiceAndAction)
		{
			Params kparams = base.getParameters(includeServiceAndAction);
			if (!isMapped("id"))
				kparams.AddIfNotNull("id", Id);
			if (!isMapped("ratio"))
				kparams.AddIfNotNull("ratio", Ratio);
			return kparams;
		}

		public override Files getFiles()
		{
			Files kfiles = base.getFiles();
			return kfiles;
		}

		public override object Deserialize(XmlElement result)
		{
			return ObjectFactory.Create<Ratio>(result);
		}
	}


	public class RatioService
	{
		private RatioService()
		{
		}

		public static RatioAddRequestBuilder Add(Ratio ratio)
		{
			return new RatioAddRequestBuilder(ratio);
		}

		public static RatioListRequestBuilder List()
		{
			return new RatioListRequestBuilder();
		}

		public static RatioUpdateRequestBuilder Update(long id, Ratio ratio)
		{
			return new RatioUpdateRequestBuilder(id, ratio);
		}
	}
}
