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
using System;
using System.Xml;
using System.Collections.Generic;
using Kaltura.Enums;
using Kaltura.Request;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Kaltura.Types
{
	public class AssetUserRuleFilterAction : AssetUserRuleAction
	{
		#region Constants
		public const string APPLY_ON_CHANNEL = "applyOnChannel";
		#endregion

		#region Private Fields
		private bool? _ApplyOnChannel = null;
		#endregion

		#region Properties
		[JsonProperty]
		public bool? ApplyOnChannel
		{
			get { return _ApplyOnChannel; }
			set 
			{ 
				_ApplyOnChannel = value;
				OnPropertyChanged("ApplyOnChannel");
			}
		}
		#endregion

		#region CTor
		public AssetUserRuleFilterAction()
		{
		}

		public AssetUserRuleFilterAction(JToken node) : base(node)
		{
			if(node["applyOnChannel"] != null)
			{
				this._ApplyOnChannel = ParseBool(node["applyOnChannel"].Value<string>());
			}
		}
		#endregion

		#region Methods
		public override Params ToParams(bool includeObjectType = true)
		{
			Params kparams = base.ToParams(includeObjectType);
			if (includeObjectType)
				kparams.AddReplace("objectType", "KalturaAssetUserRuleFilterAction");
			kparams.AddIfNotNull("applyOnChannel", this._ApplyOnChannel);
			return kparams;
		}
		protected override string getPropertyName(string apiName)
		{
			switch(apiName)
			{
				case APPLY_ON_CHANNEL:
					return "ApplyOnChannel";
				default:
					return base.getPropertyName(apiName);
			}
		}
		#endregion
	}
}

