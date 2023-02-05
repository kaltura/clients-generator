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
	public class ActionPermissionItem : ObjectBase
	{
		#region Constants
		public const string NETWORK = "network";
		public const string ACTION_PRIVACY = "actionPrivacy";
		public const string PRIVACY = "privacy";
		public const string ACTION = "action";
		#endregion

		#region Private Fields
		private SocialNetwork _Network = null;
		private SocialActionPrivacy _ActionPrivacy = null;
		private SocialPrivacy _Privacy = null;
		private string _Action = null;
		#endregion

		#region Properties
		[JsonProperty]
		public SocialNetwork Network
		{
			get { return _Network; }
			set 
			{ 
				_Network = value;
				OnPropertyChanged("Network");
			}
		}
		[JsonProperty]
		public SocialActionPrivacy ActionPrivacy
		{
			get { return _ActionPrivacy; }
			set 
			{ 
				_ActionPrivacy = value;
				OnPropertyChanged("ActionPrivacy");
			}
		}
		[JsonProperty]
		public SocialPrivacy Privacy
		{
			get { return _Privacy; }
			set 
			{ 
				_Privacy = value;
				OnPropertyChanged("Privacy");
			}
		}
		[JsonProperty]
		public string Action
		{
			get { return _Action; }
			set 
			{ 
				_Action = value;
				OnPropertyChanged("Action");
			}
		}
		#endregion

		#region CTor
		public ActionPermissionItem()
		{
		}

		public ActionPermissionItem(JToken node) : base(node)
		{
			if(node["network"] != null)
			{
				this._Network = (SocialNetwork)StringEnum.Parse(typeof(SocialNetwork), node["network"].Value<string>());
			}
			if(node["actionPrivacy"] != null)
			{
				this._ActionPrivacy = (SocialActionPrivacy)StringEnum.Parse(typeof(SocialActionPrivacy), node["actionPrivacy"].Value<string>());
			}
			if(node["privacy"] != null)
			{
				this._Privacy = (SocialPrivacy)StringEnum.Parse(typeof(SocialPrivacy), node["privacy"].Value<string>());
			}
			if(node["action"] != null)
			{
				this._Action = node["action"].Value<string>();
			}
		}
		#endregion

		#region Methods
		public override Params ToParams(bool includeObjectType = true)
		{
			Params kparams = base.ToParams(includeObjectType);
			if (includeObjectType)
				kparams.AddReplace("objectType", "KalturaActionPermissionItem");
			kparams.AddIfNotNull("network", this._Network);
			kparams.AddIfNotNull("actionPrivacy", this._ActionPrivacy);
			kparams.AddIfNotNull("privacy", this._Privacy);
			kparams.AddIfNotNull("action", this._Action);
			return kparams;
		}
		protected override string getPropertyName(string apiName)
		{
			switch(apiName)
			{
				case NETWORK:
					return "Network";
				case ACTION_PRIVACY:
					return "ActionPrivacy";
				case PRIVACY:
					return "Privacy";
				case ACTION:
					return "Action";
				default:
					return base.getPropertyName(apiName);
			}
		}
		#endregion
	}
}

