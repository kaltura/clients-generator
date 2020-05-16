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
	public class SSOAdapterProfileInvoke : ObjectBase
	{
		#region Constants
		public const string ADAPTER_DATA = "adapterData";
		public const string CODE = "code";
		public const string MESSAGE = "message";
		#endregion

		#region Private Fields
		private IDictionary<string, StringValue> _AdapterData;
		private string _Code = null;
		private string _Message = null;
		#endregion

		#region Properties
		[JsonProperty]
		public IDictionary<string, StringValue> AdapterData
		{
			get { return _AdapterData; }
			set 
			{ 
				_AdapterData = value;
				OnPropertyChanged("AdapterData");
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
		#endregion

		#region CTor
		public SSOAdapterProfileInvoke()
		{
		}

		public SSOAdapterProfileInvoke(JToken node) : base(node)
		{
			if(node["adapterData"] != null)
			{
				{
					string key;
					this._AdapterData = new Dictionary<string, StringValue>();
					foreach(var arrayNode in node["adapterData"].Children<JProperty>())
					{
						key = arrayNode.Name;
						this._AdapterData[key] = ObjectFactory.Create<StringValue>(arrayNode.Value);
					}
				}
			}
			if(node["code"] != null)
			{
				this._Code = node["code"].Value<string>();
			}
			if(node["message"] != null)
			{
				this._Message = node["message"].Value<string>();
			}
		}
		#endregion

		#region Methods
		public override Params ToParams(bool includeObjectType = true)
		{
			Params kparams = base.ToParams(includeObjectType);
			if (includeObjectType)
				kparams.AddReplace("objectType", "KalturaSSOAdapterProfileInvoke");
			kparams.AddIfNotNull("adapterData", this._AdapterData);
			kparams.AddIfNotNull("code", this._Code);
			kparams.AddIfNotNull("message", this._Message);
			return kparams;
		}
		protected override string getPropertyName(string apiName)
		{
			switch(apiName)
			{
				case ADAPTER_DATA:
					return "AdapterData";
				case CODE:
					return "Code";
				case MESSAGE:
					return "Message";
				default:
					return base.getPropertyName(apiName);
			}
		}
		#endregion
	}
}

