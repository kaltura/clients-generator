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
	public class ExternalChannelProfile : ObjectBase
	{
		#region Constants
		public const string ID = "id";
		public const string NAME = "name";
		public const string IS_ACTIVE = "isActive";
		public const string EXTERNAL_IDENTIFIER = "externalIdentifier";
		public const string FILTER_EXPRESSION = "filterExpression";
		public const string RECOMMENDATION_ENGINE_ID = "recommendationEngineId";
		public const string ENRICHMENTS = "enrichments";
		#endregion

		#region Private Fields
		private int _Id = Int32.MinValue;
		private string _Name = null;
		private bool? _IsActive = null;
		private string _ExternalIdentifier = null;
		private string _FilterExpression = null;
		private int _RecommendationEngineId = Int32.MinValue;
		private IList<ChannelEnrichmentHolder> _Enrichments;
		#endregion

		#region Properties
		[JsonProperty]
		public int Id
		{
			get { return _Id; }
			private set 
			{ 
				_Id = value;
				OnPropertyChanged("Id");
			}
		}
		[JsonProperty]
		public string Name
		{
			get { return _Name; }
			set 
			{ 
				_Name = value;
				OnPropertyChanged("Name");
			}
		}
		[JsonProperty]
		public bool? IsActive
		{
			get { return _IsActive; }
			set 
			{ 
				_IsActive = value;
				OnPropertyChanged("IsActive");
			}
		}
		[JsonProperty]
		public string ExternalIdentifier
		{
			get { return _ExternalIdentifier; }
			set 
			{ 
				_ExternalIdentifier = value;
				OnPropertyChanged("ExternalIdentifier");
			}
		}
		[JsonProperty]
		public string FilterExpression
		{
			get { return _FilterExpression; }
			set 
			{ 
				_FilterExpression = value;
				OnPropertyChanged("FilterExpression");
			}
		}
		[JsonProperty]
		public int RecommendationEngineId
		{
			get { return _RecommendationEngineId; }
			set 
			{ 
				_RecommendationEngineId = value;
				OnPropertyChanged("RecommendationEngineId");
			}
		}
		[JsonProperty]
		public IList<ChannelEnrichmentHolder> Enrichments
		{
			get { return _Enrichments; }
			set 
			{ 
				_Enrichments = value;
				OnPropertyChanged("Enrichments");
			}
		}
		#endregion

		#region CTor
		public ExternalChannelProfile()
		{
		}

		public ExternalChannelProfile(JToken node) : base(node)
		{
			if(node["id"] != null)
			{
				this._Id = ParseInt(node["id"].Value<string>());
			}
			if(node["name"] != null)
			{
				this._Name = node["name"].Value<string>();
			}
			if(node["isActive"] != null)
			{
				this._IsActive = ParseBool(node["isActive"].Value<string>());
			}
			if(node["externalIdentifier"] != null)
			{
				this._ExternalIdentifier = node["externalIdentifier"].Value<string>();
			}
			if(node["filterExpression"] != null)
			{
				this._FilterExpression = node["filterExpression"].Value<string>();
			}
			if(node["recommendationEngineId"] != null)
			{
				this._RecommendationEngineId = ParseInt(node["recommendationEngineId"].Value<string>());
			}
			if(node["enrichments"] != null)
			{
				this._Enrichments = new List<ChannelEnrichmentHolder>();
				foreach(var arrayNode in node["enrichments"].Children())
				{
					this._Enrichments.Add(ObjectFactory.Create<ChannelEnrichmentHolder>(arrayNode));
				}
			}
		}
		#endregion

		#region Methods
		public override Params ToParams(bool includeObjectType = true)
		{
			Params kparams = base.ToParams(includeObjectType);
			if (includeObjectType)
				kparams.AddReplace("objectType", "KalturaExternalChannelProfile");
			kparams.AddIfNotNull("id", this._Id);
			kparams.AddIfNotNull("name", this._Name);
			kparams.AddIfNotNull("isActive", this._IsActive);
			kparams.AddIfNotNull("externalIdentifier", this._ExternalIdentifier);
			kparams.AddIfNotNull("filterExpression", this._FilterExpression);
			kparams.AddIfNotNull("recommendationEngineId", this._RecommendationEngineId);
			kparams.AddIfNotNull("enrichments", this._Enrichments);
			return kparams;
		}
		protected override string getPropertyName(string apiName)
		{
			switch(apiName)
			{
				case ID:
					return "Id";
				case NAME:
					return "Name";
				case IS_ACTIVE:
					return "IsActive";
				case EXTERNAL_IDENTIFIER:
					return "ExternalIdentifier";
				case FILTER_EXPRESSION:
					return "FilterExpression";
				case RECOMMENDATION_ENGINE_ID:
					return "RecommendationEngineId";
				case ENRICHMENTS:
					return "Enrichments";
				default:
					return base.getPropertyName(apiName);
			}
		}
		#endregion
	}
}

