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
	public class PushMessage : ObjectBase
	{
		#region Constants
		public const string MESSAGE = "message";
		public const string SOUND = "sound";
		public const string ACTION = "action";
		public const string URL = "url";
		public const string UDID = "udid";
		public const string PUSH_CHANNELS = "pushChannels";
		#endregion

		#region Private Fields
		private string _Message = null;
		private string _Sound = null;
		private string _Action = null;
		private string _Url = null;
		private string _Udid = null;
		private string _PushChannels = null;
		#endregion

		#region Properties
		[JsonProperty]
		public string Message
		{
			get { return _Message; }
			set 
			{ 
				_Message = value;
				OnPropertyChanged("Message");
			}
		}
		[JsonProperty]
		public string Sound
		{
			get { return _Sound; }
			set 
			{ 
				_Sound = value;
				OnPropertyChanged("Sound");
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
		[JsonProperty]
		public string Url
		{
			get { return _Url; }
			set 
			{ 
				_Url = value;
				OnPropertyChanged("Url");
			}
		}
		[JsonProperty]
		public string Udid
		{
			get { return _Udid; }
			set 
			{ 
				_Udid = value;
				OnPropertyChanged("Udid");
			}
		}
		[JsonProperty]
		public string PushChannels
		{
			get { return _PushChannels; }
			set 
			{ 
				_PushChannels = value;
				OnPropertyChanged("PushChannels");
			}
		}
		#endregion

		#region CTor
		public PushMessage()
		{
		}

		public PushMessage(JToken node) : base(node)
		{
			if(node["message"] != null)
			{
				this._Message = node["message"].Value<string>();
			}
			if(node["sound"] != null)
			{
				this._Sound = node["sound"].Value<string>();
			}
			if(node["action"] != null)
			{
				this._Action = node["action"].Value<string>();
			}
			if(node["url"] != null)
			{
				this._Url = node["url"].Value<string>();
			}
			if(node["udid"] != null)
			{
				this._Udid = node["udid"].Value<string>();
			}
			if(node["pushChannels"] != null)
			{
				this._PushChannels = node["pushChannels"].Value<string>();
			}
		}
		#endregion

		#region Methods
		public override Params ToParams(bool includeObjectType = true)
		{
			Params kparams = base.ToParams(includeObjectType);
			if (includeObjectType)
				kparams.AddReplace("objectType", "KalturaPushMessage");
			kparams.AddIfNotNull("message", this._Message);
			kparams.AddIfNotNull("sound", this._Sound);
			kparams.AddIfNotNull("action", this._Action);
			kparams.AddIfNotNull("url", this._Url);
			kparams.AddIfNotNull("udid", this._Udid);
			kparams.AddIfNotNull("pushChannels", this._PushChannels);
			return kparams;
		}
		protected override string getPropertyName(string apiName)
		{
			switch(apiName)
			{
				case MESSAGE:
					return "Message";
				case SOUND:
					return "Sound";
				case ACTION:
					return "Action";
				case URL:
					return "Url";
				case UDID:
					return "Udid";
				case PUSH_CHANNELS:
					return "PushChannels";
				default:
					return base.getPropertyName(apiName);
			}
		}
		#endregion
	}
}

