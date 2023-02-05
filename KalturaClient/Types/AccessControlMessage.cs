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
	public class AccessControlMessage : ObjectBase
	{
		#region Constants
		public const string MESSAGE = "message";
		public const string CODE = "code";
		#endregion

		#region Private Fields
		private string _Message = null;
		private string _Code = null;
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
		public string Code
		{
			get { return _Code; }
			set 
			{ 
				_Code = value;
				OnPropertyChanged("Code");
			}
		}
		#endregion

		#region CTor
		public AccessControlMessage()
		{
		}

		public AccessControlMessage(JToken node) : base(node)
		{
			if(node["message"] != null)
			{
				this._Message = node["message"].Value<string>();
			}
			if(node["code"] != null)
			{
				this._Code = node["code"].Value<string>();
			}
		}
		#endregion

		#region Methods
		public override Params ToParams(bool includeObjectType = true)
		{
			Params kparams = base.ToParams(includeObjectType);
			if (includeObjectType)
				kparams.AddReplace("objectType", "KalturaAccessControlMessage");
			kparams.AddIfNotNull("message", this._Message);
			kparams.AddIfNotNull("code", this._Code);
			return kparams;
		}
		protected override string getPropertyName(string apiName)
		{
			switch(apiName)
			{
				case MESSAGE:
					return "Message";
				case CODE:
					return "Code";
				default:
					return base.getPropertyName(apiName);
			}
		}
		#endregion
	}
}

