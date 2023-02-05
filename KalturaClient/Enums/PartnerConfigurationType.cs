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
	public sealed class PartnerConfigurationType : StringEnum
	{
		public static readonly PartnerConfigurationType DEFAULTPAYMENTGATEWAY = new PartnerConfigurationType("DefaultPaymentGateway");
		public static readonly PartnerConfigurationType ENABLEPAYMENTGATEWAYSELECTION = new PartnerConfigurationType("EnablePaymentGatewaySelection");
		public static readonly PartnerConfigurationType OSSADAPTER = new PartnerConfigurationType("OSSAdapter");
		public static readonly PartnerConfigurationType CONCURRENCY = new PartnerConfigurationType("Concurrency");
		public static readonly PartnerConfigurationType GENERAL = new PartnerConfigurationType("General");
		public static readonly PartnerConfigurationType OBJECTVIRTUALASSET = new PartnerConfigurationType("ObjectVirtualAsset");
		public static readonly PartnerConfigurationType COMMERCE = new PartnerConfigurationType("Commerce");
		public static readonly PartnerConfigurationType PLAYBACK = new PartnerConfigurationType("Playback");
		public static readonly PartnerConfigurationType PAYMENT = new PartnerConfigurationType("Payment");
		public static readonly PartnerConfigurationType CATALOG = new PartnerConfigurationType("Catalog");
		public static readonly PartnerConfigurationType SECURITY = new PartnerConfigurationType("Security");
		public static readonly PartnerConfigurationType OPC = new PartnerConfigurationType("Opc");

		private PartnerConfigurationType(string name) : base(name) { }
	}
}
