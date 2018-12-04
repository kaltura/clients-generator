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
	public class PushMessage : ObjectBase
	{
		#region Constants
		public const string MESSAGE = "message";
		public const string SOUND = "sound";
		public const string ACTION = "action";
		public const string URL = "url";
		#endregion

		#region Private Fields
		private string _Message = null;
		private string _Sound = null;
		private string _Action = null;
		private string _Url = null;
		#endregion

		#region Properties
		public string Message
		{
			get { return _Message; }
			set 
			{ 
				_Message = value;
				OnPropertyChanged("Message");
			}
		}
		public string Sound
		{
			get { return _Sound; }
			set 
			{ 
				_Sound = value;
				OnPropertyChanged("Sound");
			}
		}
		public string Action
		{
			get { return _Action; }
			set 
			{ 
				_Action = value;
				OnPropertyChanged("Action");
			}
		}
		public string Url
		{
			get { return _Url; }
			set 
			{ 
				_Url = value;
				OnPropertyChanged("Url");
			}
		}
		#endregion

		#region CTor
		public PushMessage()
		{
		}

		public PushMessage(XmlElement node) : base(node)
		{
			foreach (XmlElement propertyNode in node.ChildNodes)
			{
				switch (propertyNode.Name)
				{
					case "message":
						this._Message = propertyNode.InnerText;
						continue;
					case "sound":
						this._Sound = propertyNode.InnerText;
						continue;
					case "action":
						this._Action = propertyNode.InnerText;
						continue;
					case "url":
						this._Url = propertyNode.InnerText;
						continue;
				}
			}
		}

		public PushMessage(IDictionary<string,object> data) : base(data)
		{
			    this._Message = data.TryGetValueSafe<string>("message");
			    this._Sound = data.TryGetValueSafe<string>("sound");
			    this._Action = data.TryGetValueSafe<string>("action");
			    this._Url = data.TryGetValueSafe<string>("url");
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
				default:
					return base.getPropertyName(apiName);
			}
		}
		#endregion
	}
}

