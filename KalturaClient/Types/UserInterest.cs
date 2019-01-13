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
// Copyright (C) 2006-2018  Kaltura Inc.
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
	public class UserInterest : ObjectBase
	{
		#region Constants
		public const string ID = "id";
		public const string TOPIC = "topic";
		#endregion

		#region Private Fields
		private string _Id = null;
		private UserInterestTopic _Topic;
		#endregion

		#region Properties
		public string Id
		{
			get { return _Id; }
		}
		public UserInterestTopic Topic
		{
			get { return _Topic; }
			set 
			{ 
				_Topic = value;
				OnPropertyChanged("Topic");
			}
		}
		#endregion

		#region CTor
		public UserInterest()
		{
		}

		public UserInterest(XmlElement node) : base(node)
		{
			foreach (XmlElement propertyNode in node.ChildNodes)
			{
				switch (propertyNode.Name)
				{
					case "id":
						this._Id = propertyNode.InnerText;
						continue;
					case "topic":
						this._Topic = ObjectFactory.Create<UserInterestTopic>(propertyNode);
						continue;
				}
			}
		}

		public UserInterest(IDictionary<string,object> data) : base(data)
		{
			    this._Id = data.TryGetValueSafe<string>("id");
			    this._Topic = ObjectFactory.Create<UserInterestTopic>(data.TryGetValueSafe<IDictionary<string,object>>("topic"));
		}
		#endregion

		#region Methods
		public override Params ToParams(bool includeObjectType = true)
		{
			Params kparams = base.ToParams(includeObjectType);
			if (includeObjectType)
				kparams.AddReplace("objectType", "KalturaUserInterest");
			kparams.AddIfNotNull("id", this._Id);
			kparams.AddIfNotNull("topic", this._Topic);
			return kparams;
		}
		protected override string getPropertyName(string apiName)
		{
			switch(apiName)
			{
				case ID:
					return "Id";
				case TOPIC:
					return "Topic";
				default:
					return base.getPropertyName(apiName);
			}
		}
		#endregion
	}
}

