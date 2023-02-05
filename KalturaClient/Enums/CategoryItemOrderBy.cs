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
	public sealed class CategoryItemOrderBy : StringEnum
	{
		public static readonly CategoryItemOrderBy NAME_ASC = new CategoryItemOrderBy("NAME_ASC");
		public static readonly CategoryItemOrderBy NAME_DESC = new CategoryItemOrderBy("NAME_DESC");
		public static readonly CategoryItemOrderBy CREATE_DATE_ASC = new CategoryItemOrderBy("CREATE_DATE_ASC");
		public static readonly CategoryItemOrderBy CREATE_DATE_DESC = new CategoryItemOrderBy("CREATE_DATE_DESC");
		public static readonly CategoryItemOrderBy NONE = new CategoryItemOrderBy("NONE");
		public static readonly CategoryItemOrderBy UPDATE_DATE_ASC = new CategoryItemOrderBy("UPDATE_DATE_ASC");
		public static readonly CategoryItemOrderBy UPDATE_DATE_DESC = new CategoryItemOrderBy("UPDATE_DATE_DESC");

		private CategoryItemOrderBy(string name) : base(name) { }
	}
}
