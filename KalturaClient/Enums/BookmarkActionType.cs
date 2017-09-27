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
	public sealed class BookmarkActionType : StringEnum
	{
		public static readonly BookmarkActionType HIT = new BookmarkActionType("HIT");
		public static readonly BookmarkActionType PLAY = new BookmarkActionType("PLAY");
		public static readonly BookmarkActionType STOP = new BookmarkActionType("STOP");
		public static readonly BookmarkActionType PAUSE = new BookmarkActionType("PAUSE");
		public static readonly BookmarkActionType FIRST_PLAY = new BookmarkActionType("FIRST_PLAY");
		public static readonly BookmarkActionType SWOOSH = new BookmarkActionType("SWOOSH");
		public static readonly BookmarkActionType FULL_SCREEN = new BookmarkActionType("FULL_SCREEN");
		public static readonly BookmarkActionType SEND_TO_FRIEND = new BookmarkActionType("SEND_TO_FRIEND");
		public static readonly BookmarkActionType LOAD = new BookmarkActionType("LOAD");
		public static readonly BookmarkActionType FULL_SCREEN_EXIT = new BookmarkActionType("FULL_SCREEN_EXIT");
		public static readonly BookmarkActionType FINISH = new BookmarkActionType("FINISH");
		public static readonly BookmarkActionType ERROR = new BookmarkActionType("ERROR");
		public static readonly BookmarkActionType BITRATE_CHANGE = new BookmarkActionType("BITRATE_CHANGE");
		public static readonly BookmarkActionType NONE = new BookmarkActionType("NONE");

		private BookmarkActionType(string name) : base(name) { }
	}
}
