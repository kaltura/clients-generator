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
	public class UserInterestTopic : ObjectBase
	{
		#region Constants
		public const string META_ID = "metaId";
		public const string VALUE = "value";
		public const string PARENT_TOPIC = "parentTopic";
		#endregion

		#region Private Fields
		private string _MetaId = null;
		private string _Value = null;
		private UserInterestTopic _ParentTopic;
		#endregion

		#region Properties
		[JsonProperty]
		public string MetaId
		{
			get { return _MetaId; }
			set 
			{ 
				_MetaId = value;
				OnPropertyChanged("MetaId");
			}
		}
		[JsonProperty]
		public string Value
		{
			get { return _Value; }
			set 
			{ 
				_Value = value;
				OnPropertyChanged("Value");
			}
		}
		[JsonProperty]
		public UserInterestTopic ParentTopic
		{
			get { return _ParentTopic; }
			set 
			{ 
				_ParentTopic = value;
				OnPropertyChanged("ParentTopic");
			}
		}
		#endregion

		#region CTor
		public UserInterestTopic()
		{
		}

		public UserInterestTopic(JToken node) : base(node)
		{
			if(node["metaId"] != null)
			{
				this._MetaId = node["metaId"].Value<string>();
			}
			if(node["value"] != null)
			{
				this._Value = node["value"].Value<string>();
			}
			if(node["parentTopic"] != null)
			{
				this._ParentTopic = ObjectFactory.Create<UserInterestTopic>(node["parentTopic"]);
			}
		}
		#endregion

		#region Methods
		public override Params ToParams(bool includeObjectType = true)
		{
			Params kparams = base.ToParams(includeObjectType);
			if (includeObjectType)
				kparams.AddReplace("objectType", "KalturaUserInterestTopic");
			kparams.AddIfNotNull("metaId", this._MetaId);
			kparams.AddIfNotNull("value", this._Value);
			kparams.AddIfNotNull("parentTopic", this._ParentTopic);
			return kparams;
		}
		protected override string getPropertyName(string apiName)
		{
			switch(apiName)
			{
				case META_ID:
					return "MetaId";
				case VALUE:
					return "Value";
				case PARENT_TOPIC:
					return "ParentTopic";
				default:
					return base.getPropertyName(apiName);
			}
		}
		#endregion
	}
}

