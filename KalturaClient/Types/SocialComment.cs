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
	public class SocialComment : ObjectBase
	{
		#region Constants
		public const string HEADER = "header";
		public const string TEXT = "text";
		public const string CREATE_DATE = "createDate";
		public const string WRITER = "writer";
		#endregion

		#region Private Fields
		private string _Header = null;
		private string _Text = null;
		private long _CreateDate = long.MinValue;
		private string _Writer = null;
		#endregion

		#region Properties
		public string Header
		{
			get { return _Header; }
			set 
			{ 
				_Header = value;
				OnPropertyChanged("Header");
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
		public long CreateDate
		{
			get { return _CreateDate; }
			set 
			{ 
				_CreateDate = value;
				OnPropertyChanged("CreateDate");
			}
		}
		public string Writer
		{
			get { return _Writer; }
			set 
			{ 
				_Writer = value;
				OnPropertyChanged("Writer");
			}
		}
		#endregion

		#region CTor
		public SocialComment()
		{
		}

		public SocialComment(XmlElement node) : base(node)
		{
			foreach (XmlElement propertyNode in node.ChildNodes)
			{
				switch (propertyNode.Name)
				{
					case "header":
						this._Header = propertyNode.InnerText;
						continue;
					case "text":
						this._Text = propertyNode.InnerText;
						continue;
					case "createDate":
						this._CreateDate = ParseLong(propertyNode.InnerText);
						continue;
					case "writer":
						this._Writer = propertyNode.InnerText;
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
				kparams.AddReplace("objectType", "KalturaSocialComment");
			kparams.AddIfNotNull("header", this._Header);
			kparams.AddIfNotNull("text", this._Text);
			kparams.AddIfNotNull("createDate", this._CreateDate);
			kparams.AddIfNotNull("writer", this._Writer);
			return kparams;
		}
		protected override string getPropertyName(string apiName)
		{
			switch(apiName)
			{
				case HEADER:
					return "Header";
				case TEXT:
					return "Text";
				case CREATE_DATE:
					return "CreateDate";
				case WRITER:
					return "Writer";
				default:
					return base.getPropertyName(apiName);
			}
		}
		#endregion
	}
}

