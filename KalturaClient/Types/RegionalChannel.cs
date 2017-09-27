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
	public class RegionalChannel : ObjectBase
	{
		#region Constants
		public const string LINEAR_CHANNEL_ID = "linearChannelId";
		public const string CHANNEL_NUMBER = "channelNumber";
		#endregion

		#region Private Fields
		private int _LinearChannelId = Int32.MinValue;
		private int _ChannelNumber = Int32.MinValue;
		#endregion

		#region Properties
		public int LinearChannelId
		{
			get { return _LinearChannelId; }
			set 
			{ 
				_LinearChannelId = value;
				OnPropertyChanged("LinearChannelId");
			}
		}
		public int ChannelNumber
		{
			get { return _ChannelNumber; }
			set 
			{ 
				_ChannelNumber = value;
				OnPropertyChanged("ChannelNumber");
			}
		}
		#endregion

		#region CTor
		public RegionalChannel()
		{
		}

		public RegionalChannel(XmlElement node) : base(node)
		{
			foreach (XmlElement propertyNode in node.ChildNodes)
			{
				switch (propertyNode.Name)
				{
					case "linearChannelId":
						this._LinearChannelId = ParseInt(propertyNode.InnerText);
						continue;
					case "channelNumber":
						this._ChannelNumber = ParseInt(propertyNode.InnerText);
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
				kparams.AddReplace("objectType", "KalturaRegionalChannel");
			kparams.AddIfNotNull("linearChannelId", this._LinearChannelId);
			kparams.AddIfNotNull("channelNumber", this._ChannelNumber);
			return kparams;
		}
		protected override string getPropertyName(string apiName)
		{
			switch(apiName)
			{
				case LINEAR_CHANNEL_ID:
					return "LinearChannelId";
				case CHANNEL_NUMBER:
					return "ChannelNumber";
				default:
					return base.getPropertyName(apiName);
			}
		}
		#endregion
	}
}

