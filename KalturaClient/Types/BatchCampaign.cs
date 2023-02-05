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
	public class BatchCampaign : Campaign
	{
		#region Constants
		public const string POPULATION_CONDITIONS = "populationConditions";
		#endregion

		#region Private Fields
		private IList<Condition> _PopulationConditions;
		#endregion

		#region Properties
		[JsonProperty]
		public IList<Condition> PopulationConditions
		{
			get { return _PopulationConditions; }
			set 
			{ 
				_PopulationConditions = value;
				OnPropertyChanged("PopulationConditions");
			}
		}
		#endregion

		#region CTor
		public BatchCampaign()
		{
		}

		public BatchCampaign(JToken node) : base(node)
		{
			if(node["populationConditions"] != null)
			{
				this._PopulationConditions = new List<Condition>();
				foreach(var arrayNode in node["populationConditions"].Children())
				{
					this._PopulationConditions.Add(ObjectFactory.Create<Condition>(arrayNode));
				}
			}
		}
		#endregion

		#region Methods
		public override Params ToParams(bool includeObjectType = true)
		{
			Params kparams = base.ToParams(includeObjectType);
			if (includeObjectType)
				kparams.AddReplace("objectType", "KalturaBatchCampaign");
			kparams.AddIfNotNull("populationConditions", this._PopulationConditions);
			return kparams;
		}
		protected override string getPropertyName(string apiName)
		{
			switch(apiName)
			{
				case POPULATION_CONDITIONS:
					return "PopulationConditions";
				default:
					return base.getPropertyName(apiName);
			}
		}
		#endregion
	}
}

