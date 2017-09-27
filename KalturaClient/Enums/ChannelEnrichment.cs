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
	public sealed class ChannelEnrichment : StringEnum
	{
		public static readonly ChannelEnrichment CLIENTLOCATION = new ChannelEnrichment("ClientLocation");
		public static readonly ChannelEnrichment USERID = new ChannelEnrichment("UserId");
		public static readonly ChannelEnrichment HOUSEHOLDID = new ChannelEnrichment("HouseholdId");
		public static readonly ChannelEnrichment DEVICEID = new ChannelEnrichment("DeviceId");
		public static readonly ChannelEnrichment DEVICETYPE = new ChannelEnrichment("DeviceType");
		public static readonly ChannelEnrichment UTCOFFSET = new ChannelEnrichment("UTCOffset");
		public static readonly ChannelEnrichment LANGUAGE = new ChannelEnrichment("Language");
		public static readonly ChannelEnrichment NPVRSUPPORT = new ChannelEnrichment("NPVRSupport");
		public static readonly ChannelEnrichment CATCHUP = new ChannelEnrichment("Catchup");
		public static readonly ChannelEnrichment PARENTAL = new ChannelEnrichment("Parental");
		public static readonly ChannelEnrichment DTTREGION = new ChannelEnrichment("DTTRegion");
		public static readonly ChannelEnrichment ATHOME = new ChannelEnrichment("AtHome");

		private ChannelEnrichment(string name) : base(name) { }
	}
}
