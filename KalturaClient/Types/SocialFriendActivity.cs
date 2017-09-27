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
using System;
using System.Xml;
using System.Collections.Generic;
using Kaltura.Enums;
using Kaltura.Request;

namespace Kaltura.Types
{
	public class SocialFriendActivity : ObjectBase
	{
		#region Constants
		public const string USER_FULL_NAME = "userFullName";
		public const string USER_PICTURE_URL = "userPictureUrl";
		public const string SOCIAL_ACTION = "socialAction";
		#endregion

		#region Private Fields
		private string _UserFullName = null;
		private string _UserPictureUrl = null;
		private SocialAction _SocialAction;
		#endregion

		#region Properties
		public string UserFullName
		{
			get { return _UserFullName; }
			set 
			{ 
				_UserFullName = value;
				OnPropertyChanged("UserFullName");
			}
		}
		public string UserPictureUrl
		{
			get { return _UserPictureUrl; }
			set 
			{ 
				_UserPictureUrl = value;
				OnPropertyChanged("UserPictureUrl");
			}
		}
		public SocialAction SocialAction
		{
			get { return _SocialAction; }
			set 
			{ 
				_SocialAction = value;
				OnPropertyChanged("SocialAction");
			}
		}
		#endregion

		#region CTor
		public SocialFriendActivity()
		{
		}

		public SocialFriendActivity(XmlElement node) : base(node)
		{
			foreach (XmlElement propertyNode in node.ChildNodes)
			{
				switch (propertyNode.Name)
				{
					case "userFullName":
						this._UserFullName = propertyNode.InnerText;
						continue;
					case "userPictureUrl":
						this._UserPictureUrl = propertyNode.InnerText;
						continue;
					case "socialAction":
						this._SocialAction = ObjectFactory.Create<SocialAction>(propertyNode);
						continue;
				}
			}
		}
		#endregion

		#region Methods
		public override Params ToParams(bool includeObjectType = true)
		{
			Params kparams = base.ToParams(includeObjectType);
			if (includeObjectType)
				kparams.AddReplace("objectType", "KalturaSocialFriendActivity");
			kparams.AddIfNotNull("userFullName", this._UserFullName);
			kparams.AddIfNotNull("userPictureUrl", this._UserPictureUrl);
			kparams.AddIfNotNull("socialAction", this._SocialAction);
			return kparams;
		}
		protected override string getPropertyName(string apiName)
		{
			switch(apiName)
			{
				case USER_FULL_NAME:
					return "UserFullName";
				case USER_PICTURE_URL:
					return "UserPictureUrl";
				case SOCIAL_ACTION:
					return "SocialAction";
				default:
					return base.getPropertyName(apiName);
			}
		}
		#endregion
	}
}

