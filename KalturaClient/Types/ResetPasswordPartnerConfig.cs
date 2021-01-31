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
	public class ResetPasswordPartnerConfig : ObjectBase
	{
		#region Constants
		public const string TEMPLATE_LIST_LABEL = "templateListLabel";
		public const string TEMPLATES = "templates";
		#endregion

		#region Private Fields
		private string _TemplateListLabel = null;
		private IList<ResetPasswordPartnerConfigTemplate> _Templates;
		#endregion

		#region Properties
		[JsonProperty]
		public string TemplateListLabel
		{
			get { return _TemplateListLabel; }
			set 
			{ 
				_TemplateListLabel = value;
				OnPropertyChanged("TemplateListLabel");
			}
		}
		[JsonProperty]
		public IList<ResetPasswordPartnerConfigTemplate> Templates
		{
			get { return _Templates; }
			set 
			{ 
				_Templates = value;
				OnPropertyChanged("Templates");
			}
		}
		#endregion

		#region CTor
		public ResetPasswordPartnerConfig()
		{
		}

		public ResetPasswordPartnerConfig(JToken node) : base(node)
		{
			if(node["templateListLabel"] != null)
			{
				this._TemplateListLabel = node["templateListLabel"].Value<string>();
			}
			if(node["templates"] != null)
			{
				this._Templates = new List<ResetPasswordPartnerConfigTemplate>();
				foreach(var arrayNode in node["templates"].Children())
				{
					this._Templates.Add(ObjectFactory.Create<ResetPasswordPartnerConfigTemplate>(arrayNode));
				}
			}
		}
		#endregion

		#region Methods
		public override Params ToParams(bool includeObjectType = true)
		{
			Params kparams = base.ToParams(includeObjectType);
			if (includeObjectType)
				kparams.AddReplace("objectType", "KalturaResetPasswordPartnerConfig");
			kparams.AddIfNotNull("templateListLabel", this._TemplateListLabel);
			kparams.AddIfNotNull("templates", this._Templates);
			return kparams;
		}
		protected override string getPropertyName(string apiName)
		{
			switch(apiName)
			{
				case TEMPLATE_LIST_LABEL:
					return "TemplateListLabel";
				case TEMPLATES:
					return "Templates";
				default:
					return base.getPropertyName(apiName);
			}
		}
		#endregion
	}
}

