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
	public class IotClientConfiguration : ObjectBase
	{
		#region Constants
		public const string ANNOUNCEMENT_TOPIC = "announcementTopic";
		public const string CREDENTIALS_PROVIDER = "credentialsProvider";
		public const string COGNITO_USER_POOL = "cognitoUserPool";
		public const string JSON = "json";
		public const string TOPICS = "topics";
		#endregion

		#region Private Fields
		private string _AnnouncementTopic = null;
		private CredentialsProvider _CredentialsProvider;
		private CognitoUserPool _CognitoUserPool;
		private string _Json = null;
		private string _Topics = null;
		#endregion

		#region Properties
		[JsonProperty]
		public string AnnouncementTopic
		{
			get { return _AnnouncementTopic; }
			set 
			{ 
				_AnnouncementTopic = value;
				OnPropertyChanged("AnnouncementTopic");
			}
		}
		[JsonProperty]
		public CredentialsProvider CredentialsProvider
		{
			get { return _CredentialsProvider; }
			set 
			{ 
				_CredentialsProvider = value;
				OnPropertyChanged("CredentialsProvider");
			}
		}
		[JsonProperty]
		public CognitoUserPool CognitoUserPool
		{
			get { return _CognitoUserPool; }
			set 
			{ 
				_CognitoUserPool = value;
				OnPropertyChanged("CognitoUserPool");
			}
		}
		[JsonProperty]
		public string Json
		{
			get { return _Json; }
			set 
			{ 
				_Json = value;
				OnPropertyChanged("Json");
			}
		}
		[JsonProperty]
		public string Topics
		{
			get { return _Topics; }
			set 
			{ 
				_Topics = value;
				OnPropertyChanged("Topics");
			}
		}
		#endregion

		#region CTor
		public IotClientConfiguration()
		{
		}

		public IotClientConfiguration(JToken node) : base(node)
		{
			if(node["announcementTopic"] != null)
			{
				this._AnnouncementTopic = node["announcementTopic"].Value<string>();
			}
			if(node["credentialsProvider"] != null)
			{
				this._CredentialsProvider = ObjectFactory.Create<CredentialsProvider>(node["credentialsProvider"]);
			}
			if(node["cognitoUserPool"] != null)
			{
				this._CognitoUserPool = ObjectFactory.Create<CognitoUserPool>(node["cognitoUserPool"]);
			}
			if(node["json"] != null)
			{
				this._Json = node["json"].Value<string>();
			}
			if(node["topics"] != null)
			{
				this._Topics = node["topics"].Value<string>();
			}
		}
		#endregion

		#region Methods
		public override Params ToParams(bool includeObjectType = true)
		{
			Params kparams = base.ToParams(includeObjectType);
			if (includeObjectType)
				kparams.AddReplace("objectType", "KalturaIotClientConfiguration");
			kparams.AddIfNotNull("announcementTopic", this._AnnouncementTopic);
			kparams.AddIfNotNull("credentialsProvider", this._CredentialsProvider);
			kparams.AddIfNotNull("cognitoUserPool", this._CognitoUserPool);
			kparams.AddIfNotNull("json", this._Json);
			kparams.AddIfNotNull("topics", this._Topics);
			return kparams;
		}
		protected override string getPropertyName(string apiName)
		{
			switch(apiName)
			{
				case ANNOUNCEMENT_TOPIC:
					return "AnnouncementTopic";
				case CREDENTIALS_PROVIDER:
					return "CredentialsProvider";
				case COGNITO_USER_POOL:
					return "CognitoUserPool";
				case JSON:
					return "Json";
				case TOPICS:
					return "Topics";
				default:
					return base.getPropertyName(apiName);
			}
		}
		#endregion
	}
}

