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
namespace Kaltura.Enums
{
	public sealed class AssetReminderOrderBy : StringEnum
	{
		public static readonly AssetReminderOrderBy RELEVANCY_DESC = new AssetReminderOrderBy("RELEVANCY_DESC");
		public static readonly AssetReminderOrderBy NAME_ASC = new AssetReminderOrderBy("NAME_ASC");
		public static readonly AssetReminderOrderBy NAME_DESC = new AssetReminderOrderBy("NAME_DESC");
		public static readonly AssetReminderOrderBy VIEWS_DESC = new AssetReminderOrderBy("VIEWS_DESC");
		public static readonly AssetReminderOrderBy RATINGS_DESC = new AssetReminderOrderBy("RATINGS_DESC");
		public static readonly AssetReminderOrderBy VOTES_DESC = new AssetReminderOrderBy("VOTES_DESC");
		public static readonly AssetReminderOrderBy START_DATE_DESC = new AssetReminderOrderBy("START_DATE_DESC");
		public static readonly AssetReminderOrderBy START_DATE_ASC = new AssetReminderOrderBy("START_DATE_ASC");
		public static readonly AssetReminderOrderBy LIKES_DESC = new AssetReminderOrderBy("LIKES_DESC");

		private AssetReminderOrderBy(string name) : base(name) { }
	}
}
