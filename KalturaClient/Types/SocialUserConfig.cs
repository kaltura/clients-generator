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
	public class SocialUserConfig : SocialConfig
	{
		#region Constants
		public const string ACTION_PERMISSION_ITEMS = "actionPermissionItems";
		#endregion

		#region Private Fields
		private IList<ActionPermissionItem> _ActionPermissionItems;
		#endregion

		#region Properties
		[JsonProperty]
		public IList<ActionPermissionItem> ActionPermissionItems
		{
			get { return _ActionPermissionItems; }
			set 
			{ 
				_ActionPermissionItems = value;
				OnPropertyChanged("ActionPermissionItems");
			}
		}
		#endregion

		#region CTor
		public SocialUserConfig()
		{
		}

		public SocialUserConfig(JToken node) : base(node)
		{
			if(node["actionPermissionItems"] != null)
			{
				this._ActionPermissionItems = new List<ActionPermissionItem>();
				foreach(var arrayNode in node["actionPermissionItems"].Children())
				{
					this._ActionPermissionItems.Add(ObjectFactory.Create<ActionPermissionItem>(arrayNode));
				}
			}
		}
		#endregion

		#region Methods
		public override Params ToParams(bool includeObjectType = true)
		{
			Params kparams = base.ToParams(includeObjectType);
			if (includeObjectType)
				kparams.AddReplace("objectType", "KalturaSocialUserConfig");
			kparams.AddIfNotNull("actionPermissionItems", this._ActionPermissionItems);
			return kparams;
		}
		protected override string getPropertyName(string apiName)
		{
			switch(apiName)
			{
				case ACTION_PERMISSION_ITEMS:
					return "ActionPermissionItems";
				default:
					return base.getPropertyName(apiName);
			}
		}
		#endregion
	}
}

