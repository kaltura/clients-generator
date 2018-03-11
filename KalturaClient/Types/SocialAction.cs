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

namespace Kaltura.Types
{
	public class SocialAction : ObjectBase
	{
		#region Constants
		public const string ID = "id";
		public const string ACTION_TYPE = "actionType";
		public const string TIME = "time";
		public const string ASSET_ID = "assetId";
		public const string ASSET_TYPE = "assetType";
		public const string URL = "url";
		#endregion

		#region Private Fields
		private string _Id = null;
		private SocialActionType _ActionType = null;
		private long _Time = long.MinValue;
		private long _AssetId = long.MinValue;
		private AssetType _AssetType = null;
		private string _Url = null;
		#endregion

		#region Properties
		public string Id
		{
			get { return _Id; }
		}
		public SocialActionType ActionType
		{
			get { return _ActionType; }
			set 
			{ 
				_ActionType = value;
				OnPropertyChanged("ActionType");
			}
		}
		public long Time
		{
			get { return _Time; }
			set 
			{ 
				_Time = value;
				OnPropertyChanged("Time");
			}
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
		public AssetType AssetType
		{
			get { return _AssetType; }
			set 
			{ 
				_AssetType = value;
				OnPropertyChanged("AssetType");
			}
		}
		public string Url
		{
			set 
			{ 
				_Url = value;
				OnPropertyChanged("Url");
			}
		}
		#endregion

		#region CTor
		public SocialAction()
		{
		}

		public SocialAction(XmlElement node) : base(node)
		{
			foreach (XmlElement propertyNode in node.ChildNodes)
			{
				switch (propertyNode.Name)
				{
					case "id":
						this._Id = propertyNode.InnerText;
						continue;
					case "actionType":
						this._ActionType = (SocialActionType)StringEnum.Parse(typeof(SocialActionType), propertyNode.InnerText);
						continue;
					case "time":
						this._Time = ParseLong(propertyNode.InnerText);
						continue;
					case "assetId":
						this._AssetId = ParseLong(propertyNode.InnerText);
						continue;
					case "assetType":
						this._AssetType = (AssetType)StringEnum.Parse(typeof(AssetType), propertyNode.InnerText);
						continue;
					case "url":
						this._Url = propertyNode.InnerText;
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
				kparams.AddReplace("objectType", "KalturaSocialAction");
			kparams.AddIfNotNull("id", this._Id);
			kparams.AddIfNotNull("actionType", this._ActionType);
			kparams.AddIfNotNull("time", this._Time);
			kparams.AddIfNotNull("assetId", this._AssetId);
			kparams.AddIfNotNull("assetType", this._AssetType);
			kparams.AddIfNotNull("url", this._Url);
			return kparams;
		}
		protected override string getPropertyName(string apiName)
		{
			switch(apiName)
			{
				case ID:
					return "Id";
				case ACTION_TYPE:
					return "ActionType";
				case TIME:
					return "Time";
				case ASSET_ID:
					return "AssetId";
				case ASSET_TYPE:
					return "AssetType";
				case URL:
					return "Url";
				default:
					return base.getPropertyName(apiName);
			}
		}
		#endregion
	}
}

