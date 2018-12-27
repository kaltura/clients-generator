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
	public class ChannelsFilter : Filter
	{
		#region Constants
		public const string ID_EQUAL = "idEqual";
		public const string MEDIA_ID_EQUAL = "mediaIdEqual";
		public const string NAME_EQUAL = "nameEqual";
		public const string NAME_STARTS_WITH = "nameStartsWith";
		public new const string ORDER_BY = "orderBy";
		#endregion

		#region Private Fields
		private int _IdEqual = Int32.MinValue;
		private long _MediaIdEqual = long.MinValue;
		private string _NameEqual = null;
		private string _NameStartsWith = null;
		private ChannelsOrderBy _OrderBy = null;
		#endregion

		#region Properties
		public int IdEqual
		{
			get { return _IdEqual; }
			set 
			{ 
				_IdEqual = value;
				OnPropertyChanged("IdEqual");
			}
		}
		public long MediaIdEqual
		{
			get { return _MediaIdEqual; }
			set 
			{ 
				_MediaIdEqual = value;
				OnPropertyChanged("MediaIdEqual");
			}
		}
		public string NameEqual
		{
			get { return _NameEqual; }
			set 
			{ 
				_NameEqual = value;
				OnPropertyChanged("NameEqual");
			}
		}
		public string NameStartsWith
		{
			get { return _NameStartsWith; }
			set 
			{ 
				_NameStartsWith = value;
				OnPropertyChanged("NameStartsWith");
			}
		}
		public new ChannelsOrderBy OrderBy
		{
			get { return _OrderBy; }
			set 
			{ 
				_OrderBy = value;
				OnPropertyChanged("OrderBy");
			}
		}
		#endregion

		#region CTor
		public ChannelsFilter()
		{
		}

		public ChannelsFilter(XmlElement node) : base(node)
		{
			foreach (XmlElement propertyNode in node.ChildNodes)
			{
				switch (propertyNode.Name)
				{
					case "idEqual":
						this._IdEqual = ParseInt(propertyNode.InnerText);
						continue;
					case "mediaIdEqual":
						this._MediaIdEqual = ParseLong(propertyNode.InnerText);
						continue;
					case "nameEqual":
						this._NameEqual = propertyNode.InnerText;
						continue;
					case "nameStartsWith":
						this._NameStartsWith = propertyNode.InnerText;
						continue;
					case "orderBy":
						this._OrderBy = (ChannelsOrderBy)StringEnum.Parse(typeof(ChannelsOrderBy), propertyNode.InnerText);
						continue;
				}
			}
		}

		public ChannelsFilter(IDictionary<string,object> data) : base(data)
		{
			    this._IdEqual = data.TryGetValueSafe<int>("idEqual");
			    this._MediaIdEqual = data.TryGetValueSafe<long>("mediaIdEqual");
			    this._NameEqual = data.TryGetValueSafe<string>("nameEqual");
			    this._NameStartsWith = data.TryGetValueSafe<string>("nameStartsWith");
			    this._OrderBy = (ChannelsOrderBy)StringEnum.Parse(typeof(ChannelsOrderBy), data.TryGetValueSafe<string>("orderBy"));
		}
		#endregion

		#region Methods
		public override Params ToParams(bool includeObjectType = true)
		{
			Params kparams = base.ToParams(includeObjectType);
			if (includeObjectType)
				kparams.AddReplace("objectType", "KalturaChannelsFilter");
			kparams.AddIfNotNull("idEqual", this._IdEqual);
			kparams.AddIfNotNull("mediaIdEqual", this._MediaIdEqual);
			kparams.AddIfNotNull("nameEqual", this._NameEqual);
			kparams.AddIfNotNull("nameStartsWith", this._NameStartsWith);
			kparams.AddIfNotNull("orderBy", this._OrderBy);
			return kparams;
		}
		protected override string getPropertyName(string apiName)
		{
			switch(apiName)
			{
				case ID_EQUAL:
					return "IdEqual";
				case MEDIA_ID_EQUAL:
					return "MediaIdEqual";
				case NAME_EQUAL:
					return "NameEqual";
				case NAME_STARTS_WITH:
					return "NameStartsWith";
				case ORDER_BY:
					return "OrderBy";
				default:
					return base.getPropertyName(apiName);
			}
		}
		#endregion
	}
}

