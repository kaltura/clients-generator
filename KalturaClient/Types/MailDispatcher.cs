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
	public class MailDispatcher : Dispatcher
	{
		#region Constants
		public const string BODY_TEMPLATE = "bodyTemplate";
		public const string SUBJECT_TEMPLATE = "subjectTemplate";
		#endregion

		#region Private Fields
		private string _BodyTemplate = null;
		private string _SubjectTemplate = null;
		#endregion

		#region Properties
		[JsonProperty]
		public string BodyTemplate
		{
			get { return _BodyTemplate; }
			set 
			{ 
				_BodyTemplate = value;
				OnPropertyChanged("BodyTemplate");
			}
		}
		[JsonProperty]
		public string SubjectTemplate
		{
			get { return _SubjectTemplate; }
			set 
			{ 
				_SubjectTemplate = value;
				OnPropertyChanged("SubjectTemplate");
			}
		}
		#endregion

		#region CTor
		public MailDispatcher()
		{
		}

		public MailDispatcher(JToken node) : base(node)
		{
			if(node["bodyTemplate"] != null)
			{
				this._BodyTemplate = node["bodyTemplate"].Value<string>();
			}
			if(node["subjectTemplate"] != null)
			{
				this._SubjectTemplate = node["subjectTemplate"].Value<string>();
			}
		}
		#endregion

		#region Methods
		public override Params ToParams(bool includeObjectType = true)
		{
			Params kparams = base.ToParams(includeObjectType);
			if (includeObjectType)
				kparams.AddReplace("objectType", "KalturaMailDispatcher");
			kparams.AddIfNotNull("bodyTemplate", this._BodyTemplate);
			kparams.AddIfNotNull("subjectTemplate", this._SubjectTemplate);
			return kparams;
		}
		protected override string getPropertyName(string apiName)
		{
			switch(apiName)
			{
				case BODY_TEMPLATE:
					return "BodyTemplate";
				case SUBJECT_TEMPLATE:
					return "SubjectTemplate";
				default:
					return base.getPropertyName(apiName);
			}
		}
		#endregion
	}
}

