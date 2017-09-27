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
	public class AssetFileContext : ObjectBase
	{
		#region Constants
		public const string VIEW_LIFE_CYCLE = "viewLifeCycle";
		public const string FULL_LIFE_CYCLE = "fullLifeCycle";
		public const string IS_OFFLINE_PLAY_BACK = "isOfflinePlayBack";
		#endregion

		#region Private Fields
		private string _ViewLifeCycle = null;
		private string _FullLifeCycle = null;
		private bool? _IsOfflinePlayBack = null;
		#endregion

		#region Properties
		public string ViewLifeCycle
		{
			get { return _ViewLifeCycle; }
		}
		public string FullLifeCycle
		{
			get { return _FullLifeCycle; }
		}
		public bool? IsOfflinePlayBack
		{
			get { return _IsOfflinePlayBack; }
		}
		#endregion

		#region CTor
		public AssetFileContext()
		{
		}

		public AssetFileContext(XmlElement node) : base(node)
		{
			foreach (XmlElement propertyNode in node.ChildNodes)
			{
				switch (propertyNode.Name)
				{
					case "viewLifeCycle":
						this._ViewLifeCycle = propertyNode.InnerText;
						continue;
					case "fullLifeCycle":
						this._FullLifeCycle = propertyNode.InnerText;
						continue;
					case "isOfflinePlayBack":
						this._IsOfflinePlayBack = ParseBool(propertyNode.InnerText);
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
				kparams.AddReplace("objectType", "KalturaAssetFileContext");
			kparams.AddIfNotNull("viewLifeCycle", this._ViewLifeCycle);
			kparams.AddIfNotNull("fullLifeCycle", this._FullLifeCycle);
			kparams.AddIfNotNull("isOfflinePlayBack", this._IsOfflinePlayBack);
			return kparams;
		}
		protected override string getPropertyName(string apiName)
		{
			switch(apiName)
			{
				case VIEW_LIFE_CYCLE:
					return "ViewLifeCycle";
				case FULL_LIFE_CYCLE:
					return "FullLifeCycle";
				case IS_OFFLINE_PLAY_BACK:
					return "IsOfflinePlayBack";
				default:
					return base.getPropertyName(apiName);
			}
		}
		#endregion
	}
}

