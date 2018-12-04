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
	public class Bulk : ObjectBase
	{
		#region Constants
		public const string ID = "id";
		public const string STATUS = "status";
		public const string CREATE_DATE = "createDate";
		public const string UPDATE_DATE = "updateDate";
		#endregion

		#region Private Fields
		private long _Id = long.MinValue;
		private BatchJobStatus _Status = null;
		private long _CreateDate = long.MinValue;
		private long _UpdateDate = long.MinValue;
		#endregion

		#region Properties
		public long Id
		{
			get { return _Id; }
		}
		public BatchJobStatus Status
		{
			get { return _Status; }
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
		public Bulk()
		{
		}

		public Bulk(XmlElement node) : base(node)
		{
			foreach (XmlElement propertyNode in node.ChildNodes)
			{
				switch (propertyNode.Name)
				{
					case "id":
						this._Id = ParseLong(propertyNode.InnerText);
						continue;
					case "status":
						this._Status = (BatchJobStatus)StringEnum.Parse(typeof(BatchJobStatus), propertyNode.InnerText);
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

		public Bulk(IDictionary<string,object> data) : base(data)
		{
			    this._Id = data.TryGetValueSafe<long>("id");
			    this._Status = (BatchJobStatus)StringEnum.Parse(typeof(BatchJobStatus), data.TryGetValueSafe<string>("status"));
			    this._CreateDate = data.TryGetValueSafe<long>("createDate");
			    this._UpdateDate = data.TryGetValueSafe<long>("updateDate");
		}
		#endregion

		#region Methods
		public override Params ToParams(bool includeObjectType = true)
		{
			Params kparams = base.ToParams(includeObjectType);
			if (includeObjectType)
				kparams.AddReplace("objectType", "KalturaBulk");
			kparams.AddIfNotNull("id", this._Id);
			kparams.AddIfNotNull("status", this._Status);
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

