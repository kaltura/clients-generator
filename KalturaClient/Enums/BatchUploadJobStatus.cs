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
	public sealed class BatchUploadJobStatus : StringEnum
	{
		public static readonly BatchUploadJobStatus PENDING = new BatchUploadJobStatus("PENDING");
		public static readonly BatchUploadJobStatus QUEUED = new BatchUploadJobStatus("QUEUED");
		public static readonly BatchUploadJobStatus PROCESSING = new BatchUploadJobStatus("PROCESSING");
		public static readonly BatchUploadJobStatus PROCESSED = new BatchUploadJobStatus("PROCESSED");
		public static readonly BatchUploadJobStatus MOVEFILE = new BatchUploadJobStatus("MOVEFILE");
		public static readonly BatchUploadJobStatus FINISHED = new BatchUploadJobStatus("FINISHED");
		public static readonly BatchUploadJobStatus FAILED = new BatchUploadJobStatus("FAILED");
		public static readonly BatchUploadJobStatus ABORTED = new BatchUploadJobStatus("ABORTED");
		public static readonly BatchUploadJobStatus RETRY = new BatchUploadJobStatus("RETRY");
		public static readonly BatchUploadJobStatus FATAL = new BatchUploadJobStatus("FATAL");
		public static readonly BatchUploadJobStatus DONT_PROCESS = new BatchUploadJobStatus("DONT_PROCESS");
		public static readonly BatchUploadJobStatus FINISHED_PARTIALLY = new BatchUploadJobStatus("FINISHED_PARTIALLY");

		private BatchUploadJobStatus(string name) : base(name) { }
	}
}
