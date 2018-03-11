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
	public class RecommendationProfileAddRequestBuilder : RequestBuilder<RecommendationProfile>
	{
		#region Constants
		public const string RECOMMENDATION_ENGINE = "recommendationEngine";
		#endregion

		public RecommendationProfile RecommendationEngine
		{
			set;
			get;
		}

		public RecommendationProfileAddRequestBuilder()
			: base("recommendationprofile", "add")
		{
		}

		public RecommendationProfileAddRequestBuilder(RecommendationProfile recommendationEngine)
			: this()
		{
			this.RecommendationEngine = recommendationEngine;
		}

		public override Params getParameters(bool includeServiceAndAction)
		{
			Params kparams = base.getParameters(includeServiceAndAction);
			if (!isMapped("recommendationEngine"))
				kparams.AddIfNotNull("recommendationEngine", RecommendationEngine);
			return kparams;
		}

		public override Files getFiles()
		{
			Files kfiles = base.getFiles();
			return kfiles;
		}

		public override object Deserialize(XmlElement result)
		{
			return ObjectFactory.Create<RecommendationProfile>(result);
		}
	}

	public class RecommendationProfileDeleteRequestBuilder : RequestBuilder<bool>
	{
		#region Constants
		public const string ID = "id";
		#endregion

		public int Id
		{
			set;
			get;
		}

		public RecommendationProfileDeleteRequestBuilder()
			: base("recommendationprofile", "delete")
		{
		}

		public RecommendationProfileDeleteRequestBuilder(int id)
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
			if (result.InnerText.Equals("1") || result.InnerText.ToLower().Equals("true"))
				return true;
			return false;
		}
	}

	public class RecommendationProfileGenerateSharedSecretRequestBuilder : RequestBuilder<RecommendationProfile>
	{
		#region Constants
		public const string RECOMMENDATION_ENGINE_ID = "recommendationEngineId";
		#endregion

		public int RecommendationEngineId
		{
			set;
			get;
		}

		public RecommendationProfileGenerateSharedSecretRequestBuilder()
			: base("recommendationprofile", "generateSharedSecret")
		{
		}

		public RecommendationProfileGenerateSharedSecretRequestBuilder(int recommendationEngineId)
			: this()
		{
			this.RecommendationEngineId = recommendationEngineId;
		}

		public override Params getParameters(bool includeServiceAndAction)
		{
			Params kparams = base.getParameters(includeServiceAndAction);
			if (!isMapped("recommendationEngineId"))
				kparams.AddIfNotNull("recommendationEngineId", RecommendationEngineId);
			return kparams;
		}

		public override Files getFiles()
		{
			Files kfiles = base.getFiles();
			return kfiles;
		}

		public override object Deserialize(XmlElement result)
		{
			return ObjectFactory.Create<RecommendationProfile>(result);
		}
	}

	public class RecommendationProfileListRequestBuilder : RequestBuilder<ListResponse<RecommendationProfile>>
	{
		#region Constants
		#endregion


		public RecommendationProfileListRequestBuilder()
			: base("recommendationprofile", "list")
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
			return ObjectFactory.Create<ListResponse<RecommendationProfile>>(result);
		}
	}

	public class RecommendationProfileUpdateRequestBuilder : RequestBuilder<RecommendationProfile>
	{
		#region Constants
		public const string RECOMMENDATION_ENGINE_ID = "recommendationEngineId";
		public const string RECOMMENDATION_ENGINE = "recommendationEngine";
		#endregion

		public int RecommendationEngineId
		{
			set;
			get;
		}
		public RecommendationProfile RecommendationEngine
		{
			set;
			get;
		}

		public RecommendationProfileUpdateRequestBuilder()
			: base("recommendationprofile", "update")
		{
		}

		public RecommendationProfileUpdateRequestBuilder(int recommendationEngineId, RecommendationProfile recommendationEngine)
			: this()
		{
			this.RecommendationEngineId = recommendationEngineId;
			this.RecommendationEngine = recommendationEngine;
		}

		public override Params getParameters(bool includeServiceAndAction)
		{
			Params kparams = base.getParameters(includeServiceAndAction);
			if (!isMapped("recommendationEngineId"))
				kparams.AddIfNotNull("recommendationEngineId", RecommendationEngineId);
			if (!isMapped("recommendationEngine"))
				kparams.AddIfNotNull("recommendationEngine", RecommendationEngine);
			return kparams;
		}

		public override Files getFiles()
		{
			Files kfiles = base.getFiles();
			return kfiles;
		}

		public override object Deserialize(XmlElement result)
		{
			return ObjectFactory.Create<RecommendationProfile>(result);
		}
	}


	public class RecommendationProfileService
	{
		private RecommendationProfileService()
		{
		}

		public static RecommendationProfileAddRequestBuilder Add(RecommendationProfile recommendationEngine)
		{
			return new RecommendationProfileAddRequestBuilder(recommendationEngine);
		}

		public static RecommendationProfileDeleteRequestBuilder Delete(int id)
		{
			return new RecommendationProfileDeleteRequestBuilder(id);
		}

		public static RecommendationProfileGenerateSharedSecretRequestBuilder GenerateSharedSecret(int recommendationEngineId)
		{
			return new RecommendationProfileGenerateSharedSecretRequestBuilder(recommendationEngineId);
		}

		public static RecommendationProfileListRequestBuilder List()
		{
			return new RecommendationProfileListRequestBuilder();
		}

		public static RecommendationProfileUpdateRequestBuilder Update(int recommendationEngineId, RecommendationProfile recommendationEngine)
		{
			return new RecommendationProfileUpdateRequestBuilder(recommendationEngineId, recommendationEngine);
		}
	}
}
