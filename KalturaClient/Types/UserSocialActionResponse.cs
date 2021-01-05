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
	public class UserSocialActionResponse : ObjectBase
	{
		#region Constants
		public const string SOCIAL_ACTION = "socialAction";
		public const string FAIL_STATUS = "failStatus";
		#endregion

		#region Private Fields
		private SocialAction _SocialAction;
		private IList<NetworkActionStatus> _FailStatus;
		#endregion

		#region Properties
		[JsonProperty]
		public SocialAction SocialAction
		{
			get { return _SocialAction; }
			set 
			{ 
				_SocialAction = value;
				OnPropertyChanged("SocialAction");
			}
		}
		[JsonProperty]
		public IList<NetworkActionStatus> FailStatus
		{
			get { return _FailStatus; }
			set 
			{ 
				_FailStatus = value;
				OnPropertyChanged("FailStatus");
			}
		}
		#endregion

		#region CTor
		public UserSocialActionResponse()
		{
		}

		public UserSocialActionResponse(JToken node) : base(node)
		{
			if(node["socialAction"] != null)
			{
				this._SocialAction = ObjectFactory.Create<SocialAction>(node["socialAction"]);
			}
			if(node["failStatus"] != null)
			{
				this._FailStatus = new List<NetworkActionStatus>();
				foreach(var arrayNode in node["failStatus"].Children())
				{
					this._FailStatus.Add(ObjectFactory.Create<NetworkActionStatus>(arrayNode));
				}
			}
		}
		#endregion

		#region Methods
		public override Params ToParams(bool includeObjectType = true)
		{
			Params kparams = base.ToParams(includeObjectType);
			if (includeObjectType)
				kparams.AddReplace("objectType", "KalturaUserSocialActionResponse");
			kparams.AddIfNotNull("socialAction", this._SocialAction);
			kparams.AddIfNotNull("failStatus", this._FailStatus);
			return kparams;
		}
		protected override string getPropertyName(string apiName)
		{
			switch(apiName)
			{
				case SOCIAL_ACTION:
					return "SocialAction";
				case FAIL_STATUS:
					return "FailStatus";
				default:
					return base.getPropertyName(apiName);
			}
		}
		#endregion
	}
}

