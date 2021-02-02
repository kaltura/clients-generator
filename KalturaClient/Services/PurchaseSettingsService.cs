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
	public class PurchaseSettingsGetRequestBuilder : RequestBuilder<PurchaseSettings>
	{
		#region Constants
		public const string BY = "by";
		#endregion

		public EntityReferenceBy By { get; set; }

		public PurchaseSettingsGetRequestBuilder()
			: base("purchasesettings", "get")
		{
		}

		public PurchaseSettingsGetRequestBuilder(EntityReferenceBy by)
			: this()
		{
			this.By = by;
		}

		public override Params getParameters(bool includeServiceAndAction)
		{
			Params kparams = base.getParameters(includeServiceAndAction);
			if (!isMapped("by"))
				kparams.AddIfNotNull("by", By);
			return kparams;
		}

		public override Files getFiles()
		{
			Files kfiles = base.getFiles();
			return kfiles;
		}

		public override object Deserialize(JToken result)
		{
			return ObjectFactory.Create<PurchaseSettings>(result);
		}
	}

// BEO-9522 csharp2 before comment
// BEO-9522 csharp2 before comment
	public class PurchaseSettingsUpdateRequestBuilder : RequestBuilder<PurchaseSettings>
	{
		#region Constants
		public const string ENTITY_REFERENCE = "entityReference";
		public const string SETTINGS = "settings";
		#endregion

		public EntityReferenceBy EntityReference { get; set; }
		public PurchaseSettings Settings { get; set; }

		public PurchaseSettingsUpdateRequestBuilder()
			: base("purchasesettings", "update")
		{
		}

		public PurchaseSettingsUpdateRequestBuilder(EntityReferenceBy entityReference, PurchaseSettings settings)
			: this()
		{
			this.EntityReference = entityReference;
			this.Settings = settings;
		}

		public override Params getParameters(bool includeServiceAndAction)
		{
			Params kparams = base.getParameters(includeServiceAndAction);
			if (!isMapped("entityReference"))
				kparams.AddIfNotNull("entityReference", EntityReference);
			if (!isMapped("settings"))
				kparams.AddIfNotNull("settings", Settings);
			return kparams;
		}

		public override Files getFiles()
		{
			Files kfiles = base.getFiles();
			return kfiles;
		}

		public override object Deserialize(JToken result)
		{
			return ObjectFactory.Create<PurchaseSettings>(result);
		}
	}


	public class PurchaseSettingsService
	{
		private PurchaseSettingsService()
		{
		}
// BEO-9522 csharp2 writeAction
// BEO-9522 csharp2 before comment

		public static PurchaseSettingsGetRequestBuilder Get(EntityReferenceBy by)
		{
			return new PurchaseSettingsGetRequestBuilder(by);
		}
// BEO-9522 csharp2 writeAction
// BEO-9522 csharp2 before comment
// BEO-9522 csharp2 before comment

		public static PurchaseSettingsUpdateRequestBuilder Update(EntityReferenceBy entityReference, PurchaseSettings settings)
		{
			return new PurchaseSettingsUpdateRequestBuilder(entityReference, settings);
		}
	}
}
