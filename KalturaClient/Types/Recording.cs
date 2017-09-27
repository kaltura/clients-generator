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
// Copyright (C) 2006-2017  Kaltura Inc.
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

namespace Kaltura.Types
{
	public class Recording : ObjectBase
	{
		#region Constants
		public const string ID = "id";
		public const string STATUS = "status";
		public const string ASSET_ID = "assetId";
		public const string TYPE = "type";
		public const string VIEWABLE_UNTIL_DATE = "viewableUntilDate";
		public const string IS_PROTECTED = "isProtected";
		public const string CREATE_DATE = "createDate";
		public const string UPDATE_DATE = "updateDate";
		#endregion

		#region Private Fields
		private long _Id = long.MinValue;
		private RecordingStatus _Status = null;
		private long _AssetId = long.MinValue;
		private RecordingType _Type = null;
		private long _ViewableUntilDate = long.MinValue;
		private bool? _IsProtected = null;
		private long _CreateDate = long.MinValue;
		private long _UpdateDate = long.MinValue;
		#endregion

		#region Properties
		public long Id
		{
			get { return _Id; }
		}
		public RecordingStatus Status
		{
			get { return _Status; }
		}
		public long AssetId
		{
			get { return _AssetId; }
			set 
			{ 
				_AssetId = value;
				OnPropertyChanged("AssetId");
			}
		}
		public RecordingType Type
		{
			get { return _Type; }
		}
		public long ViewableUntilDate
		{
			get { return _ViewableUntilDate; }
		}
		public bool? IsProtected
		{
			get { return _IsProtected; }
		}
		public long CreateDate
		{
			get { return _CreateDate; }
		}
		public long UpdateDate
		{
			get { return _UpdateDate; }
		}
		#endregion

		#region CTor
		public Recording()
		{
		}

		public Recording(XmlElement node) : base(node)
		{
			foreach (XmlElement propertyNode in node.ChildNodes)
			{
				switch (propertyNode.Name)
				{
					case "id":
						this._Id = ParseLong(propertyNode.InnerText);
						continue;
					case "status":
						this._Status = (RecordingStatus)StringEnum.Parse(typeof(RecordingStatus), propertyNode.InnerText);
						continue;
					case "assetId":
						this._AssetId = ParseLong(propertyNode.InnerText);
						continue;
					case "type":
						this._Type = (RecordingType)StringEnum.Parse(typeof(RecordingType), propertyNode.InnerText);
						continue;
					case "viewableUntilDate":
						this._ViewableUntilDate = ParseLong(propertyNode.InnerText);
						continue;
					case "isProtected":
						this._IsProtected = ParseBool(propertyNode.InnerText);
						continue;
					case "createDate":
						this._CreateDate = ParseLong(propertyNode.InnerText);
						continue;
					case "updateDate":
						this._UpdateDate = ParseLong(propertyNode.InnerText);
						continue;
				}
			}
		}
		#endregion

		#region Methods
		public override Params ToParams(bool includeObjectType = true)
		{
			Params kparams = base.ToParams(includeObjectType);
			if (includeObjectType)
				kparams.AddReplace("objectType", "KalturaRecording");
			kparams.AddIfNotNull("id", this._Id);
			kparams.AddIfNotNull("status", this._Status);
			kparams.AddIfNotNull("assetId", this._AssetId);
			kparams.AddIfNotNull("type", this._Type);
			kparams.AddIfNotNull("viewableUntilDate", this._ViewableUntilDate);
			kparams.AddIfNotNull("isProtected", this._IsProtected);
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
				case STATUS:
					return "Status";
				case ASSET_ID:
					return "AssetId";
				case TYPE:
					return "Type";
				case VIEWABLE_UNTIL_DATE:
					return "ViewableUntilDate";
				case IS_PROTECTED:
					return "IsProtected";
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

