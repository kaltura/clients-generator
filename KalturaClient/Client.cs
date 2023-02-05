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
using Kaltura.Types;
using Kaltura.Enums;

namespace Kaltura
{
	public class Client : ClientBase
	{
		public Client(Configuration config) : base(config)
		{
				ApiVersion = "6.1.0.28931";
				ClientTag = "dotnet:21-02-28";
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
			
 		public void setClientTag(string value)
 		{
 			ClientTag = value;
 		}
			
 		public string getClientTag()
 		{
 			return ClientTag;
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
			
 		public void setApiVersion(string value)
 		{
 			ApiVersion = value;
 		}
			
 		public string getApiVersion()
 		{
 			return ApiVersion;
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
			
 		public void setPartnerId(int value)
 		{
 			PartnerId = value;
 		}
			
 		public int getPartnerId()
 		{
 			return PartnerId;
 		}
			
 		public int UserId
 		{
 			get
 			{
 				return requestConfiguration.UserId;
 			}
 			set
 			{
 				requestConfiguration.UserId = value;
 			}
 		}
			
 		public void setUserId(int value)
 		{
 			UserId = value;
 		}
			
 		public int getUserId()
 		{
 			return UserId;
 		}
			
 		public string Language
 		{
 			get
 			{
 				return requestConfiguration.Language;
 			}
 			set
 			{
 				requestConfiguration.Language = value;
 			}
 		}
			
 		public void setLanguage(string value)
 		{
 			Language = value;
 		}
			
 		public string getLanguage()
 		{
 			return Language;
 		}
			
 		public string Currency
 		{
 			get
 			{
 				return requestConfiguration.Currency;
 			}
 			set
 			{
 				requestConfiguration.Currency = value;
 			}
 		}
			
 		public void setCurrency(string value)
 		{
 			Currency = value;
 		}
			
 		public string getCurrency()
 		{
 			return Currency;
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
			
 		public void setKS(string value)
 		{
 			KS = value;
 		}
			
 		public string getKS()
 		{
 			return KS;
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
			
 		public void setSessionId(string value)
 		{
 			SessionId = value;
 		}
			
 		public string getSessionId()
 		{
 			return SessionId;
 		}
			
 		public bool? AbortOnError
 		{
 			get
 			{
 				return requestConfiguration.AbortOnError;
 			}
 			set
 			{
 				requestConfiguration.AbortOnError = value;
 			}
 		}
			
 		public void setAbortOnError(bool? value)
 		{
 			AbortOnError = value;
 		}
			
 		public bool? getAbortOnError()
 		{
 			return AbortOnError;
 		}
			
 		public bool? AbortAllOnError
 		{
 			get
 			{
 				return requestConfiguration.AbortAllOnError;
 			}
 			set
 			{
 				requestConfiguration.AbortAllOnError = value;
 			}
 		}
			
 		public void setAbortAllOnError(bool? value)
 		{
 			AbortAllOnError = value;
 		}
			
 		public bool? getAbortAllOnError()
 		{
 			return AbortAllOnError;
 		}
			
 		public SkipCondition SkipCondition
 		{
 			get
 			{
 				return requestConfiguration.SkipCondition;
 			}
 			set
 			{
 				requestConfiguration.SkipCondition = value;
 			}
 		}
			
 		public void setSkipCondition(SkipCondition value)
 		{
 			SkipCondition = value;
 		}
			
 		public SkipCondition getSkipCondition()
 		{
 			return SkipCondition;
 		}
		#endregion
	}
}
