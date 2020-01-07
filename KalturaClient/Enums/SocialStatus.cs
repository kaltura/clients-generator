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
	public sealed class SocialStatus : StringEnum
	{
		public static readonly SocialStatus ERROR = new SocialStatus("error");
		public static readonly SocialStatus OK = new SocialStatus("ok");
		public static readonly SocialStatus USER_DOES_NOT_EXIST = new SocialStatus("user_does_not_exist");
		public static readonly SocialStatus NO_USER_SOCIAL_SETTINGS_FOUND = new SocialStatus("no_user_social_settings_found");
		public static readonly SocialStatus ASSET_ALREADY_LIKED = new SocialStatus("asset_already_liked");
		public static readonly SocialStatus NOT_ALLOWED = new SocialStatus("not_allowed");
		public static readonly SocialStatus INVALID_PARAMETERS = new SocialStatus("invalid_parameters");
		public static readonly SocialStatus NO_FACEBOOK_ACTION = new SocialStatus("no_facebook_action");
		public static readonly SocialStatus ASSET_ALREADY_RATED = new SocialStatus("asset_already_rated");
		public static readonly SocialStatus ASSET_DOSE_NOT_EXISTS = new SocialStatus("asset_dose_not_exists");
		public static readonly SocialStatus INVALID_PLATFORM_REQUEST = new SocialStatus("invalid_platform_request");
		public static readonly SocialStatus INVALID_ACCESS_TOKEN = new SocialStatus("invalid_access_token");

		private SocialStatus(string name) : base(name) { }
	}
}
