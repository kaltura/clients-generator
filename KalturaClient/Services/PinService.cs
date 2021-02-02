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
// BEO-9522 csharp2 before comment
// BEO-9522 csharp2 before comment
	public class PinGetRequestBuilder : RequestBuilder<Pin>
	{
		#region Constants
		public const string BY = "by";
		public const string TYPE = "type";
		public const string RULE_ID = "ruleId";
		#endregion

		public EntityReferenceBy By { get; set; }
		public PinType Type { get; set; }
		public int RuleId { get; set; }

		public PinGetRequestBuilder()
			: base("pin", "get")
		{
		}

		public PinGetRequestBuilder(EntityReferenceBy by, PinType type, int ruleId)
			: this()
		{
			this.By = by;
			this.Type = type;
			this.RuleId = ruleId;
		}

		public override Params getParameters(bool includeServiceAndAction)
		{
			Params kparams = base.getParameters(includeServiceAndAction);
			if (!isMapped("by"))
				kparams.AddIfNotNull("by", By);
			if (!isMapped("type"))
				kparams.AddIfNotNull("type", Type);
			if (!isMapped("ruleId"))
				kparams.AddIfNotNull("ruleId", RuleId);
			return kparams;
		}

		public override Files getFiles()
		{
			Files kfiles = base.getFiles();
			return kfiles;
		}

		public override object Deserialize(JToken result)
		{
			return ObjectFactory.Create<Pin>(result);
		}
	}

// BEO-9522 csharp2 before comment
// BEO-9522 csharp2 before comment
// BEO-9522 csharp2 before comment
// BEO-9522 csharp2 before comment
	public class PinUpdateRequestBuilder : RequestBuilder<Pin>
	{
		#region Constants
		public const string BY = "by";
		public const string TYPE = "type";
		public const string PIN = "pin";
		public const string RULE_ID = "ruleId";
		#endregion

		public EntityReferenceBy By { get; set; }
		public PinType Type { get; set; }
		public Pin Pin { get; set; }
		public int RuleId { get; set; }

		public PinUpdateRequestBuilder()
			: base("pin", "update")
		{
		}

		public PinUpdateRequestBuilder(EntityReferenceBy by, PinType type, Pin pin, int ruleId)
			: this()
		{
			this.By = by;
			this.Type = type;
			this.Pin = pin;
			this.RuleId = ruleId;
		}

		public override Params getParameters(bool includeServiceAndAction)
		{
			Params kparams = base.getParameters(includeServiceAndAction);
			if (!isMapped("by"))
				kparams.AddIfNotNull("by", By);
			if (!isMapped("type"))
				kparams.AddIfNotNull("type", Type);
			if (!isMapped("pin"))
				kparams.AddIfNotNull("pin", Pin);
			if (!isMapped("ruleId"))
				kparams.AddIfNotNull("ruleId", RuleId);
			return kparams;
		}

		public override Files getFiles()
		{
			Files kfiles = base.getFiles();
			return kfiles;
		}

		public override object Deserialize(JToken result)
		{
			return ObjectFactory.Create<Pin>(result);
		}
	}

// BEO-9522 csharp2 before comment
// BEO-9522 csharp2 before comment
// BEO-9522 csharp2 before comment
	public class PinValidateRequestBuilder : RequestBuilder<bool>
	{
		#region Constants
		public const string PIN = "pin";
		public const string TYPE = "type";
		public const string RULE_ID = "ruleId";
		#endregion

		public string Pin { get; set; }
		public PinType Type { get; set; }
		public int RuleId { get; set; }

		public PinValidateRequestBuilder()
			: base("pin", "validate")
		{
		}

		public PinValidateRequestBuilder(string pin, PinType type, int ruleId)
			: this()
		{
			this.Pin = pin;
			this.Type = type;
			this.RuleId = ruleId;
		}

		public override Params getParameters(bool includeServiceAndAction)
		{
			Params kparams = base.getParameters(includeServiceAndAction);
			if (!isMapped("pin"))
				kparams.AddIfNotNull("pin", Pin);
			if (!isMapped("type"))
				kparams.AddIfNotNull("type", Type);
			if (!isMapped("ruleId"))
				kparams.AddIfNotNull("ruleId", RuleId);
			return kparams;
		}

		public override Files getFiles()
		{
			Files kfiles = base.getFiles();
			return kfiles;
		}

		public override object Deserialize(JToken result)
		{
			if (result.Value<string>().Equals("1") || result.Value<string>().ToLower().Equals("true"))
				return true;
			return false;
		}
	}


	public class PinService
	{
		private PinService()
		{
		}
// BEO-9522 csharp2 writeAction
// BEO-9522 csharp2 before comment
// BEO-9522 csharp2 before comment
// BEO-9522 csharp2 before comment

		public static PinGetRequestBuilder Get(EntityReferenceBy by, PinType type, int ruleId = Int32.MinValue)
		{
			return new PinGetRequestBuilder(by, type, ruleId);
		}
// BEO-9522 csharp2 writeAction
// BEO-9522 csharp2 before comment
// BEO-9522 csharp2 before comment
// BEO-9522 csharp2 before comment
// BEO-9522 csharp2 before comment

		public static PinUpdateRequestBuilder Update(EntityReferenceBy by, PinType type, Pin pin, int ruleId = Int32.MinValue)
		{
			return new PinUpdateRequestBuilder(by, type, pin, ruleId);
		}
// BEO-9522 csharp2 writeAction
// BEO-9522 csharp2 before comment
// BEO-9522 csharp2 before comment
// BEO-9522 csharp2 before comment

		public static PinValidateRequestBuilder Validate(string pin, PinType type, int ruleId = Int32.MinValue)
		{
			return new PinValidateRequestBuilder(pin, type, ruleId);
		}
	}
}
