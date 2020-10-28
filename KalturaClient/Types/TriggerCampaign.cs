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
	public class TriggerCampaign : Campaign
	{
		#region Constants
		public const string SERVICE = "service";
		public const string ACTION = "action";
		public const string TRIGGER_CONDITIONS = "triggerConditions";
		#endregion

		#region Private Fields
		private ApiService _Service = null;
		private ApiAction _Action = null;
		private IList<Condition> _TriggerConditions;
		#endregion

		#region Properties
		[JsonProperty]
		public ApiService Service
		{
			get { return _Service; }
			set 
			{ 
				_Service = value;
				OnPropertyChanged("Service");
			}
		}
		[JsonProperty]
		public ApiAction Action
		{
			get { return _Action; }
			set 
			{ 
				_Action = value;
				OnPropertyChanged("Action");
			}
		}
		[JsonProperty]
		public IList<Condition> TriggerConditions
		{
			get { return _TriggerConditions; }
			set 
			{ 
				_TriggerConditions = value;
				OnPropertyChanged("TriggerConditions");
			}
		}
		#endregion

		#region CTor
		public TriggerCampaign()
		{
		}

		public TriggerCampaign(JToken node) : base(node)
		{
			if(node["service"] != null)
			{
				this._Service = (ApiService)StringEnum.Parse(typeof(ApiService), node["service"].Value<string>());
			}
			if(node["action"] != null)
			{
				this._Action = (ApiAction)StringEnum.Parse(typeof(ApiAction), node["action"].Value<string>());
			}
			if(node["triggerConditions"] != null)
			{
				this._TriggerConditions = new List<Condition>();
				foreach(var arrayNode in node["triggerConditions"].Children())
				{
					this._TriggerConditions.Add(ObjectFactory.Create<Condition>(arrayNode));
				}
			}
		}
		#endregion

		#region Methods
		public override Params ToParams(bool includeObjectType = true)
		{
			Params kparams = base.ToParams(includeObjectType);
			if (includeObjectType)
				kparams.AddReplace("objectType", "KalturaTriggerCampaign");
			kparams.AddIfNotNull("service", this._Service);
			kparams.AddIfNotNull("action", this._Action);
			kparams.AddIfNotNull("triggerConditions", this._TriggerConditions);
			return kparams;
		}
		protected override string getPropertyName(string apiName)
		{
			switch(apiName)
			{
				case SERVICE:
					return "Service";
				case ACTION:
					return "Action";
				case TRIGGER_CONDITIONS:
					return "TriggerConditions";
				default:
					return base.getPropertyName(apiName);
			}
		}
		#endregion
	}
}

