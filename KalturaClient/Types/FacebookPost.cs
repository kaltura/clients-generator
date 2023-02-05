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
	public class FacebookPost : SocialNetworkComment
	{
		#region Constants
		public const string COMMENTS = "comments";
		public const string LINK = "link";
		#endregion

		#region Private Fields
		private IList<SocialNetworkComment> _Comments;
		private string _Link = null;
		#endregion

		#region Properties
		[JsonProperty]
		public IList<SocialNetworkComment> Comments
		{
			get { return _Comments; }
			set 
			{ 
				_Comments = value;
				OnPropertyChanged("Comments");
			}
		}
		[JsonProperty]
		public string Link
		{
			get { return _Link; }
			set 
			{ 
				_Link = value;
				OnPropertyChanged("Link");
			}
		}
		#endregion

		#region CTor
		public FacebookPost()
		{
		}

		public FacebookPost(JToken node) : base(node)
		{
			if(node["comments"] != null)
			{
				this._Comments = new List<SocialNetworkComment>();
				foreach(var arrayNode in node["comments"].Children())
				{
					this._Comments.Add(ObjectFactory.Create<SocialNetworkComment>(arrayNode));
				}
			}
			if(node["link"] != null)
			{
				this._Link = node["link"].Value<string>();
			}
		}
		#endregion

		#region Methods
		public override Params ToParams(bool includeObjectType = true)
		{
			Params kparams = base.ToParams(includeObjectType);
			if (includeObjectType)
				kparams.AddReplace("objectType", "KalturaFacebookPost");
			kparams.AddIfNotNull("comments", this._Comments);
			kparams.AddIfNotNull("link", this._Link);
			return kparams;
		}
		protected override string getPropertyName(string apiName)
		{
			switch(apiName)
			{
				case COMMENTS:
					return "Comments";
				case LINK:
					return "Link";
				default:
					return base.getPropertyName(apiName);
			}
		}
		#endregion
	}
}

