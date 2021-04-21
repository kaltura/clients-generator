// ===================================================================================================
//                           _  __     _ _
//                          | |/ /__ _| | |_ _  _ _ _ __ _
//                          | ' </ _` | |  _| || | '_/ _` |
//                          |_|\_\__,_|_|\__|\_,_|_| \__,_|
//
// This file is part of the Kaltura Collaborative Media Suite which allows users
// to do with audio, video, and animation what Wiki platforms allow them to do with
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
		[JsonProperty]
		public int IdEqual
		{
			get { return _IdEqual; }
			set 
			{ 
				_IdEqual = value;
				OnPropertyChanged("IdEqual");
			}
		}
		[JsonProperty]
		public long MediaIdEqual
		{
			get { return _MediaIdEqual; }
			set 
			{ 
				_MediaIdEqual = value;
				OnPropertyChanged("MediaIdEqual");
			}
		}
		[JsonProperty]
		public string NameEqual
		{
			get { return _NameEqual; }
			set 
			{ 
				_NameEqual = value;
				OnPropertyChanged("NameEqual");
			}
		}
		[JsonProperty]
		public string NameStartsWith
		{
			get { return _NameStartsWith; }
			set 
			{ 
				_NameStartsWith = value;
				OnPropertyChanged("NameStartsWith");
			}
		}
		[JsonProperty]
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

		public ChannelsFilter(JToken node) : base(node)
		{
			if(node["idEqual"] != null)
			{
				this._IdEqual = ParseInt(node["idEqual"].Value<string>());
			}
			if(node["mediaIdEqual"] != null)
			{
				this._MediaIdEqual = ParseLong(node["mediaIdEqual"].Value<string>());
			}
			if(node["nameEqual"] != null)
			{
				this._NameEqual = node["nameEqual"].Value<string>();
			}
			if(node["nameStartsWith"] != null)
			{
				this._NameStartsWith = node["nameStartsWith"].Value<string>();
			}
			if(node["orderBy"] != null)
			{
				this._OrderBy = (ChannelsOrderBy)StringEnum.Parse(typeof(ChannelsOrderBy), node["orderBy"].Value<string>());
			}
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

