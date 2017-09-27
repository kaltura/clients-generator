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
	public class SeriesRecordingAddRequestBuilder : RequestBuilder<SeriesRecording>
	{
		#region Constants
		public const string RECORDING = "recording";
		#endregion

		public SeriesRecording Recording
		{
			set;
			get;
		}

		public SeriesRecordingAddRequestBuilder()
			: base("seriesrecording", "add")
		{
		}

		public SeriesRecordingAddRequestBuilder(SeriesRecording recording)
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

		public override object Deserialize(XmlElement result)
		{
			return ObjectFactory.Create<SeriesRecording>(result);
		}
	}

	public class SeriesRecordingCancelRequestBuilder : RequestBuilder<SeriesRecording>
	{
		#region Constants
		public const string ID = "id";
		#endregion

		public long Id
		{
			set;
			get;
		}

		public SeriesRecordingCancelRequestBuilder()
			: base("seriesrecording", "cancel")
		{
		}

		public SeriesRecordingCancelRequestBuilder(long id)
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
			return ObjectFactory.Create<SeriesRecording>(result);
		}
	}

	public class SeriesRecordingCancelByEpgIdRequestBuilder : RequestBuilder<SeriesRecording>
	{
		#region Constants
		public const string ID = "id";
		public const string EPG_ID = "epgId";
		#endregion

		public long Id
		{
			set;
			get;
		}
		public long EpgId
		{
			set;
			get;
		}

		public SeriesRecordingCancelByEpgIdRequestBuilder()
			: base("seriesrecording", "cancelByEpgId")
		{
		}

		public SeriesRecordingCancelByEpgIdRequestBuilder(long id, long epgId)
			: this()
		{
			this.Id = id;
			this.EpgId = epgId;
		}

		public override Params getParameters(bool includeServiceAndAction)
		{
			Params kparams = base.getParameters(includeServiceAndAction);
			if (!isMapped("id"))
				kparams.AddIfNotNull("id", Id);
			if (!isMapped("epgId"))
				kparams.AddIfNotNull("epgId", EpgId);
			return kparams;
		}

		public override Files getFiles()
		{
			Files kfiles = base.getFiles();
			return kfiles;
		}

		public override object Deserialize(XmlElement result)
		{
			return ObjectFactory.Create<SeriesRecording>(result);
		}
	}

	public class SeriesRecordingCancelBySeasonNumberRequestBuilder : RequestBuilder<SeriesRecording>
	{
		#region Constants
		public const string ID = "id";
		public const string SEASON_NUMBER = "seasonNumber";
		#endregion

		public long Id
		{
			set;
			get;
		}
		public long SeasonNumber
		{
			set;
			get;
		}

		public SeriesRecordingCancelBySeasonNumberRequestBuilder()
			: base("seriesrecording", "cancelBySeasonNumber")
		{
		}

		public SeriesRecordingCancelBySeasonNumberRequestBuilder(long id, long seasonNumber)
			: this()
		{
			this.Id = id;
			this.SeasonNumber = seasonNumber;
		}

		public override Params getParameters(bool includeServiceAndAction)
		{
			Params kparams = base.getParameters(includeServiceAndAction);
			if (!isMapped("id"))
				kparams.AddIfNotNull("id", Id);
			if (!isMapped("seasonNumber"))
				kparams.AddIfNotNull("seasonNumber", SeasonNumber);
			return kparams;
		}

		public override Files getFiles()
		{
			Files kfiles = base.getFiles();
			return kfiles;
		}

		public override object Deserialize(XmlElement result)
		{
			return ObjectFactory.Create<SeriesRecording>(result);
		}
	}

	public class SeriesRecordingDeleteRequestBuilder : RequestBuilder<SeriesRecording>
	{
		#region Constants
		public const string ID = "id";
		#endregion

		public long Id
		{
			set;
			get;
		}

		public SeriesRecordingDeleteRequestBuilder()
			: base("seriesrecording", "delete")
		{
		}

		public SeriesRecordingDeleteRequestBuilder(long id)
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
			return ObjectFactory.Create<SeriesRecording>(result);
		}
	}

	public class SeriesRecordingDeleteBySeasonNumberRequestBuilder : RequestBuilder<SeriesRecording>
	{
		#region Constants
		public const string ID = "id";
		public const string SEASON_NUMBER = "seasonNumber";
		#endregion

		public long Id
		{
			set;
			get;
		}
		public int SeasonNumber
		{
			set;
			get;
		}

		public SeriesRecordingDeleteBySeasonNumberRequestBuilder()
			: base("seriesrecording", "deleteBySeasonNumber")
		{
		}

		public SeriesRecordingDeleteBySeasonNumberRequestBuilder(long id, int seasonNumber)
			: this()
		{
			this.Id = id;
			this.SeasonNumber = seasonNumber;
		}

		public override Params getParameters(bool includeServiceAndAction)
		{
			Params kparams = base.getParameters(includeServiceAndAction);
			if (!isMapped("id"))
				kparams.AddIfNotNull("id", Id);
			if (!isMapped("seasonNumber"))
				kparams.AddIfNotNull("seasonNumber", SeasonNumber);
			return kparams;
		}

		public override Files getFiles()
		{
			Files kfiles = base.getFiles();
			return kfiles;
		}

		public override object Deserialize(XmlElement result)
		{
			return ObjectFactory.Create<SeriesRecording>(result);
		}
	}

	public class SeriesRecordingListRequestBuilder : RequestBuilder<ListResponse<SeriesRecording>>
	{
		#region Constants
		public const string FILTER = "filter";
		#endregion

		public SeriesRecordingFilter Filter
		{
			set;
			get;
		}

		public SeriesRecordingListRequestBuilder()
			: base("seriesrecording", "list")
		{
		}

		public SeriesRecordingListRequestBuilder(SeriesRecordingFilter filter)
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

		public override object Deserialize(XmlElement result)
		{
			return ObjectFactory.Create<ListResponse<SeriesRecording>>(result);
		}
	}


	public class SeriesRecordingService
	{
		private SeriesRecordingService()
		{
		}

		public static SeriesRecordingAddRequestBuilder Add(SeriesRecording recording)
		{
			return new SeriesRecordingAddRequestBuilder(recording);
		}

		public static SeriesRecordingCancelRequestBuilder Cancel(long id)
		{
			return new SeriesRecordingCancelRequestBuilder(id);
		}

		public static SeriesRecordingCancelByEpgIdRequestBuilder CancelByEpgId(long id, long epgId)
		{
			return new SeriesRecordingCancelByEpgIdRequestBuilder(id, epgId);
		}

		public static SeriesRecordingCancelBySeasonNumberRequestBuilder CancelBySeasonNumber(long id, long seasonNumber)
		{
			return new SeriesRecordingCancelBySeasonNumberRequestBuilder(id, seasonNumber);
		}

		public static SeriesRecordingDeleteRequestBuilder Delete(long id)
		{
			return new SeriesRecordingDeleteRequestBuilder(id);
		}

		public static SeriesRecordingDeleteBySeasonNumberRequestBuilder DeleteBySeasonNumber(long id, int seasonNumber)
		{
			return new SeriesRecordingDeleteBySeasonNumberRequestBuilder(id, seasonNumber);
		}

		public static SeriesRecordingListRequestBuilder List(SeriesRecordingFilter filter = null)
		{
			return new SeriesRecordingListRequestBuilder(filter);
		}
	}
}
