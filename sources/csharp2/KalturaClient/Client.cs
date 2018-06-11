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
// Copyright (C) 2006-2016  Kaltura Inc.
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
using Kaltura.Types;
using Kaltura.Enums;

namespace Kaltura
{
	public class Client : ClientBase
	{
		public Client(Configuration config) : base(config)
		{
				ApiVersion = "@VERSION@";
				ClientTag = "dotnet:16-12-12";
		}
	
		#region Properties
			
 		public string ClientTag
 		{
 			get
 			{
 				return clientConfiguration.ClientTag;
 			}
 			set
 			{
 				clientConfiguration.ClientTag = value;
 			}
 		}
			
 		public string ApiVersion
 		{
 			get
 			{
 				return clientConfiguration.ApiVersion;
 			}
 			set
 			{
 				clientConfiguration.ApiVersion = value;
 			}
 		}
			
 		public int PartnerId
 		{
 			get
 			{
 				return requestConfiguration.PartnerId;
 			}
 			set
 			{
 				requestConfiguration.PartnerId = value;
 			}
 		}
			
 		public string KS
 		{
 			get
 			{
 				return requestConfiguration.Ks;
 			}
 			set
 			{
 				requestConfiguration.Ks = value;
 			}
 		}
			
 		public string SessionId
 		{
 			get
 			{
 				return requestConfiguration.Ks;
 			}
 			set
 			{
 				requestConfiguration.Ks = value;
 			}
 		}
			
 		public BaseResponseProfile ResponseProfile
 		{
 			get
 			{
 				return requestConfiguration.ResponseProfile;
 			}
 			set
 			{
 				requestConfiguration.ResponseProfile = value;
 			}
 		}
		#endregion
	}
}
