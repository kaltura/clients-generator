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
// Copyright (C) 2006-2019  Kaltura Inc.
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
	public class SocialNetworkComment : SocialComment
	{
		#region Constants
		public const string LIKE_COUNTER = "likeCounter";
		public const string AUTHOR_IMAGE_URL = "authorImageUrl";
		#endregion

		#region Private Fields
		private string _LikeCounter = null;
		private string _AuthorImageUrl = null;
		#endregion

		#region Properties
		[JsonProperty]
		public string LikeCounter
		{
			get { return _LikeCounter; }
			set 
			{ 
				_LikeCounter = value;
				OnPropertyChanged("LikeCounter");
			}
		}
		[JsonProperty]
		public string AuthorImageUrl
		{
			get { return _AuthorImageUrl; }
			set 
			{ 
				_AuthorImageUrl = value;
				OnPropertyChanged("AuthorImageUrl");
			}
		}
		#endregion

		#region CTor
		public SocialNetworkComment()
		{
		}

		public SocialNetworkComment(JToken node) : base(node)
		{
			if(node["likeCounter"] != null)
			{
				this._LikeCounter = node["likeCounter"].Value<string>();
			}
			if(node["authorImageUrl"] != null)
			{
				this._AuthorImageUrl = node["authorImageUrl"].Value<string>();
			}
		}
		#endregion

		#region Methods
		public override Params ToParams(bool includeObjectType = true)
		{
			Params kparams = base.ToParams(includeObjectType);
			if (includeObjectType)
				kparams.AddReplace("objectType", "KalturaSocialNetworkComment");
			kparams.AddIfNotNull("likeCounter", this._LikeCounter);
			kparams.AddIfNotNull("authorImageUrl", this._AuthorImageUrl);
			return kparams;
		}
		protected override string getPropertyName(string apiName)
		{
			switch(apiName)
			{
				case LIKE_COUNTER:
					return "LikeCounter";
				case AUTHOR_IMAGE_URL:
					return "AuthorImageUrl";
				default:
					return base.getPropertyName(apiName);
			}
		}
		#endregion
	}
}

