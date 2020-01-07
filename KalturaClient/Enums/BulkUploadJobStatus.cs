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
// Copyright (C) 2006-2020  Kaltura Inc.
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
	public sealed class BulkUploadJobStatus : StringEnum
	{
		public static readonly BulkUploadJobStatus PENDING = new BulkUploadJobStatus("Pending");
		public static readonly BulkUploadJobStatus UPLOADED = new BulkUploadJobStatus("Uploaded");
		public static readonly BulkUploadJobStatus QUEUED = new BulkUploadJobStatus("Queued");
		public static readonly BulkUploadJobStatus PARSING = new BulkUploadJobStatus("Parsing");
		public static readonly BulkUploadJobStatus PROCESSING = new BulkUploadJobStatus("Processing");
		public static readonly BulkUploadJobStatus PROCESSED = new BulkUploadJobStatus("Processed");
		public static readonly BulkUploadJobStatus SUCCESS = new BulkUploadJobStatus("Success");
		public static readonly BulkUploadJobStatus PARTIAL = new BulkUploadJobStatus("Partial");
		public static readonly BulkUploadJobStatus FAILED = new BulkUploadJobStatus("Failed");
		public static readonly BulkUploadJobStatus FATAL = new BulkUploadJobStatus("Fatal");

		private BulkUploadJobStatus(string name) : base(name) { }
	}
}
