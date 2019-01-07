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
	public class SocialGetRequestBuilder : RequestBuilder<Social>
	{
		#region Constants
		public const string TYPE = "type";
		#endregion

		public SocialNetwork Type
		{
			set;
			get;
		}

		public SocialGetRequestBuilder()
			: base("social", "get")
		{
		}

		public SocialGetRequestBuilder(SocialNetwork type)
			: this()
		{
			this.Type = type;
		}

		public override Params getParameters(bool includeServiceAndAction)
		{
			Params kparams = base.getParameters(includeServiceAndAction);
			if (!isMapped("type"))
				kparams.AddIfNotNull("type", Type);
			return kparams;
		}

		public override Files getFiles()
		{
			Files kfiles = base.getFiles();
			return kfiles;
		}

		public override object Deserialize(JToken result)
		{
			return ObjectFactory.Create<Social>(result);
		}
	}

	public class SocialGetByTokenRequestBuilder : RequestBuilder<Social>
	{
		#region Constants
		public new const string PARTNER_ID = "partnerId";
		public const string TOKEN = "token";
		public const string TYPE = "type";
		#endregion

		public new int PartnerId
		{
			set;
			get;
		}
		public string Token
		{
			set;
			get;
		}
		public SocialNetwork Type
		{
			set;
			get;
		}

		public SocialGetByTokenRequestBuilder()
			: base("social", "getByToken")
		{
		}

		public SocialGetByTokenRequestBuilder(int partnerId, string token, SocialNetwork type)
			: this()
		{
			this.PartnerId = partnerId;
			this.Token = token;
			this.Type = type;
		}

		public override Params getParameters(bool includeServiceAndAction)
		{
			Params kparams = base.getParameters(includeServiceAndAction);
			if (!isMapped("partnerId"))
				kparams.AddIfNotNull("partnerId", PartnerId);
			if (!isMapped("token"))
				kparams.AddIfNotNull("token", Token);
			if (!isMapped("type"))
				kparams.AddIfNotNull("type", Type);
			return kparams;
		}

		public override Files getFiles()
		{
			Files kfiles = base.getFiles();
			return kfiles;
		}

		public override object Deserialize(JToken result)
		{
			return ObjectFactory.Create<Social>(result);
		}
	}

	public class SocialGetConfigurationRequestBuilder : RequestBuilder<SocialConfig>
	{
		#region Constants
		public const string TYPE = "type";
		public new const string PARTNER_ID = "partnerId";
		#endregion

		public SocialNetwork Type
		{
			set;
			get;
		}
		public new int PartnerId
		{
			set;
			get;
		}

		public SocialGetConfigurationRequestBuilder()
			: base("social", "getConfiguration")
		{
		}

		public SocialGetConfigurationRequestBuilder(SocialNetwork type, int partnerId)
			: this()
		{
			this.Type = type;
			this.PartnerId = partnerId;
		}

		public override Params getParameters(bool includeServiceAndAction)
		{
			Params kparams = base.getParameters(includeServiceAndAction);
			if (!isMapped("type"))
				kparams.AddIfNotNull("type", Type);
			if (!isMapped("partnerId"))
				kparams.AddIfNotNull("partnerId", PartnerId);
			return kparams;
		}

		public override Files getFiles()
		{
			Files kfiles = base.getFiles();
			return kfiles;
		}

		public override object Deserialize(JToken result)
		{
			return ObjectFactory.Create<SocialConfig>(result);
		}
	}

	public class SocialLoginRequestBuilder : RequestBuilder<LoginResponse>
	{
		#region Constants
		public new const string PARTNER_ID = "partnerId";
		public const string TOKEN = "token";
		public const string TYPE = "type";
		public const string UDID = "udid";
		#endregion

		public new int PartnerId
		{
			set;
			get;
		}
		public string Token
		{
			set;
			get;
		}
		public SocialNetwork Type
		{
			set;
			get;
		}
		public string Udid
		{
			set;
			get;
		}

		public SocialLoginRequestBuilder()
			: base("social", "login")
		{
		}

		public SocialLoginRequestBuilder(int partnerId, string token, SocialNetwork type, string udid)
			: this()
		{
			this.PartnerId = partnerId;
			this.Token = token;
			this.Type = type;
			this.Udid = udid;
		}

		public override Params getParameters(bool includeServiceAndAction)
		{
			Params kparams = base.getParameters(includeServiceAndAction);
			if (!isMapped("partnerId"))
				kparams.AddIfNotNull("partnerId", PartnerId);
			if (!isMapped("token"))
				kparams.AddIfNotNull("token", Token);
			if (!isMapped("type"))
				kparams.AddIfNotNull("type", Type);
			if (!isMapped("udid"))
				kparams.AddIfNotNull("udid", Udid);
			return kparams;
		}

		public override Files getFiles()
		{
			Files kfiles = base.getFiles();
			return kfiles;
		}

		public override object Deserialize(JToken result)
		{
			return ObjectFactory.Create<LoginResponse>(result);
		}
	}

	public class SocialMergeRequestBuilder : RequestBuilder<Social>
	{
		#region Constants
		public const string TOKEN = "token";
		public const string TYPE = "type";
		#endregion

		public string Token
		{
			set;
			get;
		}
		public SocialNetwork Type
		{
			set;
			get;
		}

		public SocialMergeRequestBuilder()
			: base("social", "merge")
		{
		}

		public SocialMergeRequestBuilder(string token, SocialNetwork type)
			: this()
		{
			this.Token = token;
			this.Type = type;
		}

		public override Params getParameters(bool includeServiceAndAction)
		{
			Params kparams = base.getParameters(includeServiceAndAction);
			if (!isMapped("token"))
				kparams.AddIfNotNull("token", Token);
			if (!isMapped("type"))
				kparams.AddIfNotNull("type", Type);
			return kparams;
		}

		public override Files getFiles()
		{
			Files kfiles = base.getFiles();
			return kfiles;
		}

		public override object Deserialize(JToken result)
		{
			return ObjectFactory.Create<Social>(result);
		}
	}

	public class SocialRegisterRequestBuilder : RequestBuilder<Social>
	{
		#region Constants
		public new const string PARTNER_ID = "partnerId";
		public const string TOKEN = "token";
		public const string TYPE = "type";
		public const string EMAIL = "email";
		#endregion

		public new int PartnerId
		{
			set;
			get;
		}
		public string Token
		{
			set;
			get;
		}
		public SocialNetwork Type
		{
			set;
			get;
		}
		public string Email
		{
			set;
			get;
		}

		public SocialRegisterRequestBuilder()
			: base("social", "register")
		{
		}

		public SocialRegisterRequestBuilder(int partnerId, string token, SocialNetwork type, string email)
			: this()
		{
			this.PartnerId = partnerId;
			this.Token = token;
			this.Type = type;
			this.Email = email;
		}

		public override Params getParameters(bool includeServiceAndAction)
		{
			Params kparams = base.getParameters(includeServiceAndAction);
			if (!isMapped("partnerId"))
				kparams.AddIfNotNull("partnerId", PartnerId);
			if (!isMapped("token"))
				kparams.AddIfNotNull("token", Token);
			if (!isMapped("type"))
				kparams.AddIfNotNull("type", Type);
			if (!isMapped("email"))
				kparams.AddIfNotNull("email", Email);
			return kparams;
		}

		public override Files getFiles()
		{
			Files kfiles = base.getFiles();
			return kfiles;
		}

		public override object Deserialize(JToken result)
		{
			return ObjectFactory.Create<Social>(result);
		}
	}

	public class SocialUnmergeRequestBuilder : RequestBuilder<Social>
	{
		#region Constants
		public const string TYPE = "type";
		#endregion

		public SocialNetwork Type
		{
			set;
			get;
		}

		public SocialUnmergeRequestBuilder()
			: base("social", "unmerge")
		{
		}

		public SocialUnmergeRequestBuilder(SocialNetwork type)
			: this()
		{
			this.Type = type;
		}

		public override Params getParameters(bool includeServiceAndAction)
		{
			Params kparams = base.getParameters(includeServiceAndAction);
			if (!isMapped("type"))
				kparams.AddIfNotNull("type", Type);
			return kparams;
		}

		public override Files getFiles()
		{
			Files kfiles = base.getFiles();
			return kfiles;
		}

		public override object Deserialize(JToken result)
		{
			return ObjectFactory.Create<Social>(result);
		}
	}

	public class SocialUpdateConfigurationRequestBuilder : RequestBuilder<SocialConfig>
	{
		#region Constants
		public const string CONFIGURATION = "configuration";
		#endregion

		public SocialConfig Configuration
		{
			set;
			get;
		}

		public SocialUpdateConfigurationRequestBuilder()
			: base("social", "UpdateConfiguration")
		{
		}

		public SocialUpdateConfigurationRequestBuilder(SocialConfig configuration)
			: this()
		{
			this.Configuration = configuration;
		}

		public override Params getParameters(bool includeServiceAndAction)
		{
			Params kparams = base.getParameters(includeServiceAndAction);
			if (!isMapped("configuration"))
				kparams.AddIfNotNull("configuration", Configuration);
			return kparams;
		}

		public override Files getFiles()
		{
			Files kfiles = base.getFiles();
			return kfiles;
		}

		public override object Deserialize(JToken result)
		{
			return ObjectFactory.Create<SocialConfig>(result);
		}
	}


	public class SocialService
	{
		private SocialService()
		{
		}

		public static SocialGetRequestBuilder Get(SocialNetwork type)
		{
			return new SocialGetRequestBuilder(type);
		}

		public static SocialGetByTokenRequestBuilder GetByToken(int partnerId, string token, SocialNetwork type)
		{
			return new SocialGetByTokenRequestBuilder(partnerId, token, type);
		}

		public static SocialGetConfigurationRequestBuilder GetConfiguration(SocialNetwork type, int partnerId = Int32.MinValue)
		{
			return new SocialGetConfigurationRequestBuilder(type, partnerId);
		}

		public static SocialLoginRequestBuilder Login(int partnerId, string token, SocialNetwork type, string udid = null)
		{
			return new SocialLoginRequestBuilder(partnerId, token, type, udid);
		}

		public static SocialMergeRequestBuilder Merge(string token, SocialNetwork type)
		{
			return new SocialMergeRequestBuilder(token, type);
		}

		public static SocialRegisterRequestBuilder Register(int partnerId, string token, SocialNetwork type, string email = null)
		{
			return new SocialRegisterRequestBuilder(partnerId, token, type, email);
		}

		public static SocialUnmergeRequestBuilder Unmerge(SocialNetwork type)
		{
			return new SocialUnmergeRequestBuilder(type);
		}

		public static SocialUpdateConfigurationRequestBuilder UpdateConfiguration(SocialConfig configuration)
		{
			return new SocialUpdateConfigurationRequestBuilder(configuration);
		}
	}
}
