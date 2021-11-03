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
	public class SegmentationType : ObjectBase
	{
		#region Constants
		public const string ID = "id";
		public const string NAME = "name";
		public const string DESCRIPTION = "description";
		public const string CONDITIONS = "conditions";
		public const string ACTIONS = "actions";
		public const string VALUE = "value";
		public const string CREATE_DATE = "createDate";
		public const string VERSION = "version";
		#endregion

		#region Private Fields
		private long _Id = long.MinValue;
		private string _Name = null;
		private string _Description = null;
		private IList<BaseSegmentCondition> _Conditions;
		private IList<BaseSegmentAction> _Actions;
		private BaseSegmentValue _Value;
		private long _CreateDate = long.MinValue;
		private long _Version = long.MinValue;
		#endregion

		#region Properties
		[JsonProperty]
		public long Id
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
		public string Description
		{
			get { return _Description; }
			set 
			{ 
				_Description = value;
				OnPropertyChanged("Description");
			}
		}
		[JsonProperty]
		public IList<BaseSegmentCondition> Conditions
		{
			get { return _Conditions; }
			set 
			{ 
				_Conditions = value;
				OnPropertyChanged("Conditions");
			}
		}
		[JsonProperty]
		public IList<BaseSegmentAction> Actions
		{
			get { return _Actions; }
			set 
			{ 
				_Actions = value;
				OnPropertyChanged("Actions");
			}
		}
		[JsonProperty]
		public BaseSegmentValue Value
		{
			get { return _Value; }
			set 
			{ 
				_Value = value;
				OnPropertyChanged("Value");
			}
		}
		[JsonProperty]
		public long CreateDate
		{
			get { return _CreateDate; }
			private set 
			{ 
				_CreateDate = value;
				OnPropertyChanged("CreateDate");
			}
		}
		[JsonProperty]
		public long Version
		{
			get { return _Version; }
			private set 
			{ 
				_Version = value;
				OnPropertyChanged("Version");
			}
		}
		#endregion

		#region CTor
		public SegmentationType()
		{
		}

		public SegmentationType(JToken node) : base(node)
		{
			if(node["id"] != null)
			{
				this._Id = ParseLong(node["id"].Value<string>());
			}
			if(node["name"] != null)
			{
				this._Name = node["name"].Value<string>();
			}
			if(node["description"] != null)
			{
				this._Description = node["description"].Value<string>();
			}
			if(node["conditions"] != null)
			{
				this._Conditions = new List<BaseSegmentCondition>();
				foreach(var arrayNode in node["conditions"].Children())
				{
					this._Conditions.Add(ObjectFactory.Create<BaseSegmentCondition>(arrayNode));
				}
			}
			if(node["actions"] != null)
			{
				this._Actions = new List<BaseSegmentAction>();
				foreach(var arrayNode in node["actions"].Children())
				{
					this._Actions.Add(ObjectFactory.Create<BaseSegmentAction>(arrayNode));
				}
			}
			if(node["value"] != null)
			{
				this._Value = ObjectFactory.Create<BaseSegmentValue>(node["value"]);
			}
			if(node["createDate"] != null)
			{
				this._CreateDate = ParseLong(node["createDate"].Value<string>());
			}
			if(node["version"] != null)
			{
				this._Version = ParseLong(node["version"].Value<string>());
			}
		}
		#endregion

		#region Methods
		public override Params ToParams(bool includeObjectType = true)
		{
			Params kparams = base.ToParams(includeObjectType);
			if (includeObjectType)
				kparams.AddReplace("objectType", "KalturaSegmentationType");
			kparams.AddIfNotNull("id", this._Id);
			kparams.AddIfNotNull("name", this._Name);
			kparams.AddIfNotNull("description", this._Description);
			kparams.AddIfNotNull("conditions", this._Conditions);
			kparams.AddIfNotNull("actions", this._Actions);
			kparams.AddIfNotNull("value", this._Value);
			kparams.AddIfNotNull("createDate", this._CreateDate);
			kparams.AddIfNotNull("version", this._Version);
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
				case DESCRIPTION:
					return "Description";
				case CONDITIONS:
					return "Conditions";
				case ACTIONS:
					return "Actions";
				case VALUE:
					return "Value";
				case CREATE_DATE:
					return "CreateDate";
				case VERSION:
					return "Version";
				default:
					return base.getPropertyName(apiName);
			}
		}
		#endregion
	}
}

