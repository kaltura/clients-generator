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
	public class UploadTokenAddRequestBuilder : RequestBuilder<UploadToken>
	{
		#region Constants
		public const string UPLOAD_TOKEN = "uploadToken";
		#endregion

		public UploadToken UploadToken { get; set; }

		public UploadTokenAddRequestBuilder()
			: base("uploadtoken", "add")
		{
		}

		public UploadTokenAddRequestBuilder(UploadToken uploadToken)
			: this()
		{
			this.UploadToken = uploadToken;
		}

		public override Params getParameters(bool includeServiceAndAction)
		{
			Params kparams = base.getParameters(includeServiceAndAction);
			if (!isMapped("uploadToken"))
				kparams.AddIfNotNull("uploadToken", UploadToken);
			return kparams;
		}

		public override Files getFiles()
		{
			Files kfiles = base.getFiles();
			return kfiles;
		}

		public override object Deserialize(JToken result)
		{
			return ObjectFactory.Create<UploadToken>(result);
		}
	}

	public class UploadTokenUploadRequestBuilder : RequestBuilder<UploadToken>
	{
		#region Constants
		public const string UPLOAD_TOKEN_ID = "uploadTokenId";
		public const string FILE_DATA = "fileData";
		#endregion

		public string UploadTokenId { get; set; }
		public Stream FileData { get; set; }
		public string FileData_FileName { get; set; }

		public UploadTokenUploadRequestBuilder()
			: base("uploadtoken", "upload")
		{
		}

		public UploadTokenUploadRequestBuilder(string uploadTokenId, Stream fileData)
			: this()
		{
			this.UploadTokenId = uploadTokenId;
			this.FileData = fileData;
		}

		public override Params getParameters(bool includeServiceAndAction)
		{
			Params kparams = base.getParameters(includeServiceAndAction);
			if (!isMapped("uploadTokenId"))
				kparams.AddIfNotNull("uploadTokenId", UploadTokenId);
			return kparams;
		}

		public override Files getFiles()
		{
			Files kfiles = base.getFiles();
			kfiles.Add("fileData", new FileData(FileData, FileData_FileName));
			return kfiles;
		}

		public override object Deserialize(JToken result)
		{
			return ObjectFactory.Create<UploadToken>(result);
		}
	}


	public class UploadTokenService
	{
		private UploadTokenService()
		{
		}

		public static UploadTokenAddRequestBuilder Add(UploadToken uploadToken = null)
		{
			return new UploadTokenAddRequestBuilder(uploadToken);
		}

		public static UploadTokenUploadRequestBuilder Upload(string uploadTokenId, Stream fileData)
		{
			return new UploadTokenUploadRequestBuilder(uploadTokenId, fileData);
		}
	}
}
