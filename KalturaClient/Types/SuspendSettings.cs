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
using System;
using System.Xml;
using System.Collections.Generic;
using Kaltura.Enums;
using Kaltura.Request;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Kaltura.Types
{
	public class SuspendSettings : ObjectBase
	{
		#region Constants
		public const string REVOKE_ENTITLEMENTS = "revokeEntitlements";
		public const string STOP_RENEW = "stopRenew";
		#endregion

		#region Private Fields
		private bool? _RevokeEntitlements = null;
		private bool? _StopRenew = null;
		#endregion

		#region Properties
		[JsonProperty]
		public bool? RevokeEntitlements
		{
			get { return _RevokeEntitlements; }
			set 
			{ 
				_RevokeEntitlements = value;
				OnPropertyChanged("RevokeEntitlements");
			}
		}
		[JsonProperty]
		public bool? StopRenew
		{
			get { return _StopRenew; }
			set 
			{ 
				_StopRenew = value;
				OnPropertyChanged("StopRenew");
			}
		}
		#endregion

		#region CTor
		public SuspendSettings()
		{
		}

		public SuspendSettings(JToken node) : base(node)
		{
			if(node["revokeEntitlements"] != null)
			{
				this._RevokeEntitlements = ParseBool(node["revokeEntitlements"].Value<string>());
			}
			if(node["stopRenew"] != null)
			{
				this._StopRenew = ParseBool(node["stopRenew"].Value<string>());
			}
		}
		#endregion

		#region Methods
		public override Params ToParams(bool includeObjectType = true)
		{
			Params kparams = base.ToParams(includeObjectType);
			if (includeObjectType)
				kparams.AddReplace("objectType", "KalturaSuspendSettings");
			kparams.AddIfNotNull("revokeEntitlements", this._RevokeEntitlements);
			kparams.AddIfNotNull("stopRenew", this._StopRenew);
			return kparams;
		}
		protected override string getPropertyName(string apiName)
		{
			switch(apiName)
			{
				case REVOKE_ENTITLEMENTS:
					return "RevokeEntitlements";
				case STOP_RENEW:
					return "StopRenew";
				default:
					return base.getPropertyName(apiName);
			}
		}
		#endregion
	}
}

