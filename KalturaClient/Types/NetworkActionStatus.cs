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
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Kaltura.Types
{
	public class NetworkActionStatus : ObjectBase
	{
		#region Constants
		public const string STATUS = "status";
		public const string NETWORK = "network";
		#endregion

		#region Private Fields
		private SocialStatus _Status = null;
		private SocialNetwork _Network = null;
		#endregion

		#region Properties
		[JsonProperty]
		public SocialStatus Status
		{
			get { return _Status; }
			set 
			{ 
				_Status = value;
				OnPropertyChanged("Status");
			}
		}
		[JsonProperty]
		public SocialNetwork Network
		{
			get { return _Network; }
			set 
			{ 
				_Network = value;
				OnPropertyChanged("Network");
			}
		}
		#endregion

		#region CTor
		public NetworkActionStatus()
		{
		}

		public NetworkActionStatus(JToken node) : base(node)
		{
			if(node["status"] != null)
			{
				this._Status = (SocialStatus)StringEnum.Parse(typeof(SocialStatus), node["status"].Value<string>());
			}
			if(node["network"] != null)
			{
				this._Network = (SocialNetwork)StringEnum.Parse(typeof(SocialNetwork), node["network"].Value<string>());
			}
		}
		#endregion

		#region Methods
		public override Params ToParams(bool includeObjectType = true)
		{
			Params kparams = base.ToParams(includeObjectType);
			if (includeObjectType)
				kparams.AddReplace("objectType", "KalturaNetworkActionStatus");
			kparams.AddIfNotNull("status", this._Status);
			kparams.AddIfNotNull("network", this._Network);
			return kparams;
		}
		protected override string getPropertyName(string apiName)
		{
			switch(apiName)
			{
				case STATUS:
					return "Status";
				case NETWORK:
					return "Network";
				default:
					return base.getPropertyName(apiName);
			}
		}
		#endregion
	}
}

