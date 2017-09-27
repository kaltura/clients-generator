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
	public class ParentalRuleDisableRequestBuilder : RequestBuilder<bool>
	{
		#region Constants
		public const string RULE_ID = "ruleId";
		public const string ENTITY_REFERENCE = "entityReference";
		#endregion

		public long RuleId
		{
			set;
			get;
		}
		public EntityReferenceBy EntityReference
		{
			set;
			get;
		}

		public ParentalRuleDisableRequestBuilder()
			: base("parentalrule", "disable")
		{
		}

		public ParentalRuleDisableRequestBuilder(long ruleId, EntityReferenceBy entityReference)
			: this()
		{
			this.RuleId = ruleId;
			this.EntityReference = entityReference;
		}

		public override Params getParameters(bool includeServiceAndAction)
		{
			Params kparams = base.getParameters(includeServiceAndAction);
			if (!isMapped("ruleId"))
				kparams.AddIfNotNull("ruleId", RuleId);
			if (!isMapped("entityReference"))
				kparams.AddIfNotNull("entityReference", EntityReference);
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

	public class ParentalRuleDisableDefaultRequestBuilder : RequestBuilder<bool>
	{
		#region Constants
		public const string ENTITY_REFERENCE = "entityReference";
		#endregion

		public EntityReferenceBy EntityReference
		{
			set;
			get;
		}

		public ParentalRuleDisableDefaultRequestBuilder()
			: base("parentalrule", "disableDefault")
		{
		}

		public ParentalRuleDisableDefaultRequestBuilder(EntityReferenceBy entityReference)
			: this()
		{
			this.EntityReference = entityReference;
		}

		public override Params getParameters(bool includeServiceAndAction)
		{
			Params kparams = base.getParameters(includeServiceAndAction);
			if (!isMapped("entityReference"))
				kparams.AddIfNotNull("entityReference", EntityReference);
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

	public class ParentalRuleEnableRequestBuilder : RequestBuilder<bool>
	{
		#region Constants
		public const string RULE_ID = "ruleId";
		public const string ENTITY_REFERENCE = "entityReference";
		#endregion

		public long RuleId
		{
			set;
			get;
		}
		public EntityReferenceBy EntityReference
		{
			set;
			get;
		}

		public ParentalRuleEnableRequestBuilder()
			: base("parentalrule", "enable")
		{
		}

		public ParentalRuleEnableRequestBuilder(long ruleId, EntityReferenceBy entityReference)
			: this()
		{
			this.RuleId = ruleId;
			this.EntityReference = entityReference;
		}

		public override Params getParameters(bool includeServiceAndAction)
		{
			Params kparams = base.getParameters(includeServiceAndAction);
			if (!isMapped("ruleId"))
				kparams.AddIfNotNull("ruleId", RuleId);
			if (!isMapped("entityReference"))
				kparams.AddIfNotNull("entityReference", EntityReference);
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

	public class ParentalRuleListRequestBuilder : RequestBuilder<ListResponse<ParentalRule>>
	{
		#region Constants
		public const string FILTER = "filter";
		#endregion

		public ParentalRuleFilter Filter
		{
			set;
			get;
		}

		public ParentalRuleListRequestBuilder()
			: base("parentalrule", "list")
		{
		}

		public ParentalRuleListRequestBuilder(ParentalRuleFilter filter)
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
			return ObjectFactory.Create<ListResponse<ParentalRule>>(result);
		}
	}


	public class ParentalRuleService
	{
		private ParentalRuleService()
		{
		}

		public static ParentalRuleDisableRequestBuilder Disable(long ruleId, EntityReferenceBy entityReference)
		{
			return new ParentalRuleDisableRequestBuilder(ruleId, entityReference);
		}

		public static ParentalRuleDisableDefaultRequestBuilder DisableDefault(EntityReferenceBy entityReference)
		{
			return new ParentalRuleDisableDefaultRequestBuilder(entityReference);
		}

		public static ParentalRuleEnableRequestBuilder Enable(long ruleId, EntityReferenceBy entityReference)
		{
			return new ParentalRuleEnableRequestBuilder(ruleId, entityReference);
		}

		public static ParentalRuleListRequestBuilder List(ParentalRuleFilter filter)
		{
			return new ParentalRuleListRequestBuilder(filter);
		}
	}
}
