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
namespace Kaltura.Enums
{
	public sealed class PersonalFeedOrderBy : StringEnum
	{
		public static readonly PersonalFeedOrderBy RELEVANCY_DESC = new PersonalFeedOrderBy("RELEVANCY_DESC");
		public static readonly PersonalFeedOrderBy NAME_ASC = new PersonalFeedOrderBy("NAME_ASC");
		public static readonly PersonalFeedOrderBy NAME_DESC = new PersonalFeedOrderBy("NAME_DESC");
		public static readonly PersonalFeedOrderBy VIEWS_DESC = new PersonalFeedOrderBy("VIEWS_DESC");
		public static readonly PersonalFeedOrderBy RATINGS_DESC = new PersonalFeedOrderBy("RATINGS_DESC");
		public static readonly PersonalFeedOrderBy VOTES_DESC = new PersonalFeedOrderBy("VOTES_DESC");
		public static readonly PersonalFeedOrderBy START_DATE_DESC = new PersonalFeedOrderBy("START_DATE_DESC");
		public static readonly PersonalFeedOrderBy START_DATE_ASC = new PersonalFeedOrderBy("START_DATE_ASC");

		private PersonalFeedOrderBy(string name) : base(name) { }
	}
}
