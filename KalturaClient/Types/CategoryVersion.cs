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
	public class CategoryVersion : CrudObject
	{
		#region Constants
		public const string ID = "id";
		public const string NAME = "name";
		public const string TREE_ID = "treeId";
		public const string STATE = "state";
		public const string BASE_VERSION_ID = "baseVersionId";
		public const string CATEGORY_ROOT_ID = "categoryRootId";
		public const string DEFAULT_DATE = "defaultDate";
		public const string UPDATER_ID = "updaterId";
		public const string COMMENT = "comment";
		public const string CREATE_DATE = "createDate";
		public const string UPDATE_DATE = "updateDate";
		#endregion

		#region Private Fields
		private long _Id = long.MinValue;
		private string _Name = null;
		private long _TreeId = long.MinValue;
		private CategoryVersionState _State = null;
		private long _BaseVersionId = long.MinValue;
		private long _CategoryRootId = long.MinValue;
		private long _DefaultDate = long.MinValue;
		private long _UpdaterId = long.MinValue;
		private string _Comment = null;
		private long _CreateDate = long.MinValue;
		private long _UpdateDate = long.MinValue;
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
		public long TreeId
		{
			get { return _TreeId; }
			private set 
			{ 
				_TreeId = value;
				OnPropertyChanged("TreeId");
			}
		}
		[JsonProperty]
		public CategoryVersionState State
		{
			get { return _State; }
			private set 
			{ 
				_State = value;
				OnPropertyChanged("State");
			}
		}
		[JsonProperty]
		public long BaseVersionId
		{
			get { return _BaseVersionId; }
			set 
			{ 
				_BaseVersionId = value;
				OnPropertyChanged("BaseVersionId");
			}
		}
		[JsonProperty]
		public long CategoryRootId
		{
			get { return _CategoryRootId; }
			private set 
			{ 
				_CategoryRootId = value;
				OnPropertyChanged("CategoryRootId");
			}
		}
		[JsonProperty]
		public long DefaultDate
		{
			get { return _DefaultDate; }
			private set 
			{ 
				_DefaultDate = value;
				OnPropertyChanged("DefaultDate");
			}
		}
		[JsonProperty]
		public long UpdaterId
		{
			get { return _UpdaterId; }
			private set 
			{ 
				_UpdaterId = value;
				OnPropertyChanged("UpdaterId");
			}
		}
		[JsonProperty]
		public string Comment
		{
			get { return _Comment; }
			set 
			{ 
				_Comment = value;
				OnPropertyChanged("Comment");
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
		public long UpdateDate
		{
			get { return _UpdateDate; }
			private set 
			{ 
				_UpdateDate = value;
				OnPropertyChanged("UpdateDate");
			}
		}
		#endregion

		#region CTor
		public CategoryVersion()
		{
		}

		public CategoryVersion(JToken node) : base(node)
		{
			if(node["id"] != null)
			{
				this._Id = ParseLong(node["id"].Value<string>());
			}
			if(node["name"] != null)
			{
				this._Name = node["name"].Value<string>();
			}
			if(node["treeId"] != null)
			{
				this._TreeId = ParseLong(node["treeId"].Value<string>());
			}
			if(node["state"] != null)
			{
				this._State = (CategoryVersionState)StringEnum.Parse(typeof(CategoryVersionState), node["state"].Value<string>());
			}
			if(node["baseVersionId"] != null)
			{
				this._BaseVersionId = ParseLong(node["baseVersionId"].Value<string>());
			}
			if(node["categoryRootId"] != null)
			{
				this._CategoryRootId = ParseLong(node["categoryRootId"].Value<string>());
			}
			if(node["defaultDate"] != null)
			{
				this._DefaultDate = ParseLong(node["defaultDate"].Value<string>());
			}
			if(node["updaterId"] != null)
			{
				this._UpdaterId = ParseLong(node["updaterId"].Value<string>());
			}
			if(node["comment"] != null)
			{
				this._Comment = node["comment"].Value<string>();
			}
			if(node["createDate"] != null)
			{
				this._CreateDate = ParseLong(node["createDate"].Value<string>());
			}
			if(node["updateDate"] != null)
			{
				this._UpdateDate = ParseLong(node["updateDate"].Value<string>());
			}
		}
		#endregion

		#region Methods
		public override Params ToParams(bool includeObjectType = true)
		{
			Params kparams = base.ToParams(includeObjectType);
			if (includeObjectType)
				kparams.AddReplace("objectType", "KalturaCategoryVersion");
			kparams.AddIfNotNull("id", this._Id);
			kparams.AddIfNotNull("name", this._Name);
			kparams.AddIfNotNull("treeId", this._TreeId);
			kparams.AddIfNotNull("state", this._State);
			kparams.AddIfNotNull("baseVersionId", this._BaseVersionId);
			kparams.AddIfNotNull("categoryRootId", this._CategoryRootId);
			kparams.AddIfNotNull("defaultDate", this._DefaultDate);
			kparams.AddIfNotNull("updaterId", this._UpdaterId);
			kparams.AddIfNotNull("comment", this._Comment);
			kparams.AddIfNotNull("createDate", this._CreateDate);
			kparams.AddIfNotNull("updateDate", this._UpdateDate);
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
				case TREE_ID:
					return "TreeId";
				case STATE:
					return "State";
				case BASE_VERSION_ID:
					return "BaseVersionId";
				case CATEGORY_ROOT_ID:
					return "CategoryRootId";
				case DEFAULT_DATE:
					return "DefaultDate";
				case UPDATER_ID:
					return "UpdaterId";
				case COMMENT:
					return "Comment";
				case CREATE_DATE:
					return "CreateDate";
				case UPDATE_DATE:
					return "UpdateDate";
				default:
					return base.getPropertyName(apiName);
			}
		}
		#endregion
	}
}

