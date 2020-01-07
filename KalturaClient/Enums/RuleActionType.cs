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
	public sealed class RuleActionType : StringEnum
	{
		public static readonly RuleActionType BLOCK = new RuleActionType("BLOCK");
		public static readonly RuleActionType START_DATE_OFFSET = new RuleActionType("START_DATE_OFFSET");
		public static readonly RuleActionType END_DATE_OFFSET = new RuleActionType("END_DATE_OFFSET");
		public static readonly RuleActionType USER_BLOCK = new RuleActionType("USER_BLOCK");
		public static readonly RuleActionType ALLOW_PLAYBACK = new RuleActionType("ALLOW_PLAYBACK");
		public static readonly RuleActionType BLOCK_PLAYBACK = new RuleActionType("BLOCK_PLAYBACK");
		public static readonly RuleActionType APPLY_DISCOUNT_MODULE = new RuleActionType("APPLY_DISCOUNT_MODULE");
		public static readonly RuleActionType APPLY_PLAYBACK_ADAPTER = new RuleActionType("APPLY_PLAYBACK_ADAPTER");
		public static readonly RuleActionType FILTER = new RuleActionType("FILTER");
		public static readonly RuleActionType ASSET_LIFE_CYCLE_TRANSITION = new RuleActionType("ASSET_LIFE_CYCLE_TRANSITION");
		public static readonly RuleActionType APPLY_FREE_PLAYBACK = new RuleActionType("APPLY_FREE_PLAYBACK");

		private RuleActionType(string name) : base(name) { }
	}
}
