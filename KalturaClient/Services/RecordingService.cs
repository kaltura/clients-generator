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
// BEO-9522 csharp2 before comment
	public class RecordingAddRequestBuilder : RequestBuilder<Recording>
	{
		#region Constants
		public const string RECORDING = "recording";
		#endregion

		public Recording Recording { get; set; }

		public RecordingAddRequestBuilder()
			: base("recording", "add")
		{
		}

		public RecordingAddRequestBuilder(Recording recording)
			: this()
		{
			this.Recording = recording;
		}

		public override Params getParameters(bool includeServiceAndAction)
		{
			Params kparams = base.getParameters(includeServiceAndAction);
			if (!isMapped("recording"))
				kparams.AddIfNotNull("recording", Recording);
			return kparams;
		}

		public override Files getFiles()
		{
			Files kfiles = base.getFiles();
			return kfiles;
		}

		public override object Deserialize(JToken result)
		{
			return ObjectFactory.Create<Recording>(result);
		}
	}

// BEO-9522 csharp2 before comment
	public class RecordingCancelRequestBuilder : RequestBuilder<Recording>
	{
		#region Constants
		public const string ID = "id";
		#endregion

		public long Id { get; set; }

		public RecordingCancelRequestBuilder()
			: base("recording", "cancel")
		{
		}

		public RecordingCancelRequestBuilder(long id)
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
			return ObjectFactory.Create<Recording>(result);
		}
	}

// BEO-9522 csharp2 before comment
	public class RecordingDeleteRequestBuilder : RequestBuilder<Recording>
	{
		#region Constants
		public const string ID = "id";
		#endregion

		public long Id { get; set; }

		public RecordingDeleteRequestBuilder()
			: base("recording", "delete")
		{
		}

		public RecordingDeleteRequestBuilder(long id)
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
			return ObjectFactory.Create<Recording>(result);
		}
	}

// BEO-9522 csharp2 before comment
	public class RecordingGetRequestBuilder : RequestBuilder<Recording>
	{
		#region Constants
		public const string ID = "id";
		#endregion

		public long Id { get; set; }

		public RecordingGetRequestBuilder()
			: base("recording", "get")
		{
		}

		public RecordingGetRequestBuilder(long id)
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
			return ObjectFactory.Create<Recording>(result);
		}
	}

// BEO-9522 csharp2 before comment
// BEO-9522 csharp2 before comment
	public class RecordingListRequestBuilder : RequestBuilder<ListResponse<Recording>>
	{
		#region Constants
		public const string FILTER = "filter";
		public const string PAGER = "pager";
		#endregion

		public RecordingFilter Filter { get; set; }
		public FilterPager Pager { get; set; }

		public RecordingListRequestBuilder()
			: base("recording", "list")
		{
		}

		public RecordingListRequestBuilder(RecordingFilter filter, FilterPager pager)
			: this()
		{
			this.Filter = filter;
			this.Pager = pager;
		}

		public override Params getParameters(bool includeServiceAndAction)
		{
			Params kparams = base.getParameters(includeServiceAndAction);
			if (!isMapped("filter"))
				kparams.AddIfNotNull("filter", Filter);
			if (!isMapped("pager"))
				kparams.AddIfNotNull("pager", Pager);
			return kparams;
		}

		public override Files getFiles()
		{
			Files kfiles = base.getFiles();
			return kfiles;
		}

		public override object Deserialize(JToken result)
		{
			return ObjectFactory.Create<ListResponse<Recording>>(result);
		}
	}

// BEO-9522 csharp2 before comment
	public class RecordingProtectRequestBuilder : RequestBuilder<Recording>
	{
		#region Constants
		public const string ID = "id";
		#endregion

		public long Id { get; set; }

		public RecordingProtectRequestBuilder()
			: base("recording", "protect")
		{
		}

		public RecordingProtectRequestBuilder(long id)
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
			return ObjectFactory.Create<Recording>(result);
		}
	}

// BEO-9522 csharp2 before comment
// BEO-9522 csharp2 before comment
	public class RecordingUpdateRequestBuilder : RequestBuilder<Recording>
	{
		#region Constants
		public const string ID = "id";
		public const string RECORDING = "recording";
		#endregion

		public long Id { get; set; }
		public Recording Recording { get; set; }

		public RecordingUpdateRequestBuilder()
			: base("recording", "update")
		{
		}

		public RecordingUpdateRequestBuilder(long id, Recording recording)
			: this()
		{
			this.Id = id;
			this.Recording = recording;
		}

		public override Params getParameters(bool includeServiceAndAction)
		{
			Params kparams = base.getParameters(includeServiceAndAction);
			if (!isMapped("id"))
				kparams.AddIfNotNull("id", Id);
			if (!isMapped("recording"))
				kparams.AddIfNotNull("recording", Recording);
			return kparams;
		}

		public override Files getFiles()
		{
			Files kfiles = base.getFiles();
			return kfiles;
		}

		public override object Deserialize(JToken result)
		{
			return ObjectFactory.Create<Recording>(result);
		}
	}


	public class RecordingService
	{
		private RecordingService()
		{
		}
// BEO-9522 csharp2 writeAction
// BEO-9522 csharp2 before comment

		public static RecordingAddRequestBuilder Add(Recording recording)
		{
			return new RecordingAddRequestBuilder(recording);
		}
// BEO-9522 csharp2 writeAction
// BEO-9522 csharp2 before comment

		public static RecordingCancelRequestBuilder Cancel(long id)
		{
			return new RecordingCancelRequestBuilder(id);
		}
// BEO-9522 csharp2 writeAction
// BEO-9522 csharp2 before comment

		public static RecordingDeleteRequestBuilder Delete(long id)
		{
			return new RecordingDeleteRequestBuilder(id);
		}
// BEO-9522 csharp2 writeAction
// BEO-9522 csharp2 before comment

		public static RecordingGetRequestBuilder Get(long id)
		{
			return new RecordingGetRequestBuilder(id);
		}
// BEO-9522 csharp2 writeAction
// BEO-9522 csharp2 before comment
// BEO-9522 csharp2 before comment

		public static RecordingListRequestBuilder List(RecordingFilter filter = null, FilterPager pager = null)
		{
			return new RecordingListRequestBuilder(filter, pager);
		}
// BEO-9522 csharp2 writeAction
// BEO-9522 csharp2 before comment

		public static RecordingProtectRequestBuilder Protect(long id)
		{
			return new RecordingProtectRequestBuilder(id);
		}
// BEO-9522 csharp2 writeAction
// BEO-9522 csharp2 before comment
// BEO-9522 csharp2 before comment

		public static RecordingUpdateRequestBuilder Update(long id, Recording recording)
		{
			return new RecordingUpdateRequestBuilder(id, recording);
		}
	}
}
