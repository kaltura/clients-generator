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
	public class BookmarkPlayerData : ObjectBase
	{
		#region Constants
		public const string ACTION = "action";
		public const string AVERAGE_BITRATE = "averageBitrate";
		public const string TOTAL_BITRATE = "totalBitrate";
		public const string CURRENT_BITRATE = "currentBitrate";
		public const string FILE_ID = "fileId";
		#endregion

		#region Private Fields
		private BookmarkActionType _Action = null;
		private int _AverageBitrate = Int32.MinValue;
		private int _TotalBitrate = Int32.MinValue;
		private int _CurrentBitrate = Int32.MinValue;
		private long _FileId = long.MinValue;
		#endregion

		#region Properties
		public BookmarkActionType Action
		{
			get { return _Action; }
			set 
			{ 
				_Action = value;
				OnPropertyChanged("Action");
			}
		}
		public int AverageBitrate
		{
			get { return _AverageBitrate; }
			set 
			{ 
				_AverageBitrate = value;
				OnPropertyChanged("AverageBitrate");
			}
		}
		public int TotalBitrate
		{
			get { return _TotalBitrate; }
			set 
			{ 
				_TotalBitrate = value;
				OnPropertyChanged("TotalBitrate");
			}
		}
		public int CurrentBitrate
		{
			get { return _CurrentBitrate; }
			set 
			{ 
				_CurrentBitrate = value;
				OnPropertyChanged("CurrentBitrate");
			}
		}
		public long FileId
		{
			get { return _FileId; }
			set 
			{ 
				_FileId = value;
				OnPropertyChanged("FileId");
			}
		}
		#endregion

		#region CTor
		public BookmarkPlayerData()
		{
		}

		public BookmarkPlayerData(XmlElement node) : base(node)
		{
			foreach (XmlElement propertyNode in node.ChildNodes)
			{
				switch (propertyNode.Name)
				{
					case "action":
						this._Action = (BookmarkActionType)StringEnum.Parse(typeof(BookmarkActionType), propertyNode.InnerText);
						continue;
					case "averageBitrate":
						this._AverageBitrate = ParseInt(propertyNode.InnerText);
						continue;
					case "totalBitrate":
						this._TotalBitrate = ParseInt(propertyNode.InnerText);
						continue;
					case "currentBitrate":
						this._CurrentBitrate = ParseInt(propertyNode.InnerText);
						continue;
					case "fileId":
						this._FileId = ParseLong(propertyNode.InnerText);
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
				kparams.AddReplace("objectType", "KalturaBookmarkPlayerData");
			kparams.AddIfNotNull("action", this._Action);
			kparams.AddIfNotNull("averageBitrate", this._AverageBitrate);
			kparams.AddIfNotNull("totalBitrate", this._TotalBitrate);
			kparams.AddIfNotNull("currentBitrate", this._CurrentBitrate);
			kparams.AddIfNotNull("fileId", this._FileId);
			return kparams;
		}
		protected override string getPropertyName(string apiName)
		{
			switch(apiName)
			{
				case ACTION:
					return "Action";
				case AVERAGE_BITRATE:
					return "AverageBitrate";
				case TOTAL_BITRATE:
					return "TotalBitrate";
				case CURRENT_BITRATE:
					return "CurrentBitrate";
				case FILE_ID:
					return "FileId";
				default:
					return base.getPropertyName(apiName);
			}
		}
		#endregion
	}
}

