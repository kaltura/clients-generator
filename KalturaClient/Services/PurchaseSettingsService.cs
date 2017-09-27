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
	public class PurchaseSettingsGetRequestBuilder : RequestBuilder<PurchaseSettings>
	{
		#region Constants
		public const string BY = "by";
		#endregion

		public EntityReferenceBy By
		{
			set;
			get;
		}

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

		public override object Deserialize(XmlElement result)
		{
			return ObjectFactory.Create<PurchaseSettings>(result);
		}
	}

	public class PurchaseSettingsUpdateRequestBuilder : RequestBuilder<PurchaseSettings>
	{
		#region Constants
		public const string ENTITY_REFERENCE = "entityReference";
		public const string SETTINGS = "settings";
		#endregion

		public EntityReferenceBy EntityReference
		{
			set;
			get;
		}
		public PurchaseSettings Settings
		{
			set;
			get;
		}

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

		public override object Deserialize(XmlElement result)
		{
			return ObjectFactory.Create<PurchaseSettings>(result);
		}
	}


	public class PurchaseSettingsService
	{
		private PurchaseSettingsService()
		{
		}

		public static PurchaseSettingsGetRequestBuilder Get(EntityReferenceBy by)
		{
			return new PurchaseSettingsGetRequestBuilder(by);
		}

		public static PurchaseSettingsUpdateRequestBuilder Update(EntityReferenceBy entityReference, PurchaseSettings settings)
		{
			return new PurchaseSettingsUpdateRequestBuilder(entityReference, settings);
		}
	}
}
