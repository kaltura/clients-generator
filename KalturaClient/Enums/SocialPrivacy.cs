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
	public sealed class SocialPrivacy : StringEnum
	{
		public static readonly SocialPrivacy UNKNOWN = new SocialPrivacy("UNKNOWN");
		public static readonly SocialPrivacy EVERYONE = new SocialPrivacy("EVERYONE");
		public static readonly SocialPrivacy ALL_FRIENDS = new SocialPrivacy("ALL_FRIENDS");
		public static readonly SocialPrivacy FRIENDS_OF_FRIENDS = new SocialPrivacy("FRIENDS_OF_FRIENDS");
		public static readonly SocialPrivacy SELF = new SocialPrivacy("SELF");
		public static readonly SocialPrivacy CUSTOM = new SocialPrivacy("CUSTOM");

		private SocialPrivacy(string name) : base(name) { }
	}
}
