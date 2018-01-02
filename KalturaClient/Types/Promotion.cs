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
	public class Promotion : ObjectBase
	{
		#region Constants
		public const string LINK = "link";
		public const string TEXT = "text";
		public const string START_TIME = "startTime";
		public const string END_TIME = "endTime";
		#endregion

		#region Private Fields
		private string _Link = null;
		private string _Text = null;
		private long _StartTime = long.MinValue;
		private long _EndTime = long.MinValue;
		#endregion

		#region Properties
		public string Link
		{
			get { return _Link; }
			set 
			{ 
				_Link = value;
				OnPropertyChanged("Link");
			}
		}
		public string Text
		{
			get { return _Text; }
			set 
			{ 
				_Text = value;
				OnPropertyChanged("Text");
			}
		}
		public long StartTime
		{
			get { return _StartTime; }
			set 
			{ 
				_StartTime = value;
				OnPropertyChanged("StartTime");
			}
		}
		public long EndTime
		{
			get { return _EndTime; }
			set 
			{ 
				_EndTime = value;
				OnPropertyChanged("EndTime");
			}
		}
		#endregion

		#region CTor
		public Promotion()
		{
		}

		public Promotion(XmlElement node) : base(node)
		{
			foreach (XmlElement propertyNode in node.ChildNodes)
			{
				switch (propertyNode.Name)
				{
					case "link":
						this._Link = propertyNode.InnerText;
						continue;
					case "text":
						this._Text = propertyNode.InnerText;
						continue;
					case "startTime":
						this._StartTime = ParseLong(propertyNode.InnerText);
						continue;
					case "endTime":
						this._EndTime = ParseLong(propertyNode.InnerText);
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
				kparams.AddReplace("objectType", "KalturaPromotion");
			kparams.AddIfNotNull("link", this._Link);
			kparams.AddIfNotNull("text", this._Text);
			kparams.AddIfNotNull("startTime", this._StartTime);
			kparams.AddIfNotNull("endTime", this._EndTime);
			return kparams;
		}
		protected override string getPropertyName(string apiName)
		{
			switch(apiName)
			{
				case LINK:
					return "Link";
				case TEXT:
					return "Text";
				case START_TIME:
					return "StartTime";
				case END_TIME:
					return "EndTime";
				default:
					return base.getPropertyName(apiName);
			}
		}
		#endregion
	}
}

