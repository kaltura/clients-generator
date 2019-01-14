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
namespace Kaltura.Enums
{
	public sealed class BatchJobStatus : StringEnum
	{
		public static readonly BatchJobStatus PENDING = new BatchJobStatus("PENDING");
		public static readonly BatchJobStatus QUEUED = new BatchJobStatus("QUEUED");
		public static readonly BatchJobStatus PROCESSING = new BatchJobStatus("PROCESSING");
		public static readonly BatchJobStatus PROCESSED = new BatchJobStatus("PROCESSED");
		public static readonly BatchJobStatus MOVEFILE = new BatchJobStatus("MOVEFILE");
		public static readonly BatchJobStatus FINISHED = new BatchJobStatus("FINISHED");
		public static readonly BatchJobStatus FAILED = new BatchJobStatus("FAILED");
		public static readonly BatchJobStatus ABORTED = new BatchJobStatus("ABORTED");
		public static readonly BatchJobStatus ALMOST_DONE = new BatchJobStatus("ALMOST_DONE");
		public static readonly BatchJobStatus RETRY = new BatchJobStatus("RETRY");
		public static readonly BatchJobStatus FATAL = new BatchJobStatus("FATAL");
		public static readonly BatchJobStatus DONT_PROCESS = new BatchJobStatus("DONT_PROCESS");
		public static readonly BatchJobStatus FINISHED_PARTIALLY = new BatchJobStatus("FINISHED_PARTIALLY");

		private BatchJobStatus(string name) : base(name) { }
	}
}
