// ===================================================================================================
//                           _  __     _ _
//                          | |/ /__ _| | |_ _  _ _ _ __ _
//                          | ' </ _` | |  _| || | '_/ _` |
//                          |_|\_\__,_|_|\__|\_,_|_| \__,_|
//
// This file is part of the Kaltura Collaborative Media Suite which allows users
// to do with audio, video, and animation what Wiki platforms allow them to do with
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
namespace Kaltura.Enums
{
	public sealed class RuleConditionType : StringEnum
	{
		public static readonly RuleConditionType ASSET = new RuleConditionType("ASSET");
		public static readonly RuleConditionType COUNTRY = new RuleConditionType("COUNTRY");
		public static readonly RuleConditionType CONCURRENCY = new RuleConditionType("CONCURRENCY");
		public static readonly RuleConditionType IP_RANGE = new RuleConditionType("IP_RANGE");
		public static readonly RuleConditionType BUSINESS_MODULE = new RuleConditionType("BUSINESS_MODULE");
		public static readonly RuleConditionType SEGMENTS = new RuleConditionType("SEGMENTS");
		public static readonly RuleConditionType DATE = new RuleConditionType("DATE");
		public static readonly RuleConditionType OR = new RuleConditionType("OR");
		public static readonly RuleConditionType HEADER = new RuleConditionType("HEADER");
		public static readonly RuleConditionType USER_SUBSCRIPTION = new RuleConditionType("USER_SUBSCRIPTION");
		public static readonly RuleConditionType ASSET_SUBSCRIPTION = new RuleConditionType("ASSET_SUBSCRIPTION");
		public static readonly RuleConditionType USER_ROLE = new RuleConditionType("USER_ROLE");

		private RuleConditionType(string name) : base(name) { }
	}
}
