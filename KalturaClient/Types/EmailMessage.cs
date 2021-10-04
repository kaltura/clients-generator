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
	public class EmailMessage : ObjectBase
	{
		#region Constants
		public const string TEMPLATE_NAME = "templateName";
		public const string SUBJECT = "subject";
		public const string FIRST_NAME = "firstName";
		public const string LAST_NAME = "lastName";
		public const string SENDER_NAME = "senderName";
		public const string SENDER_FROM = "senderFrom";
		public const string SENDER_TO = "senderTo";
		public const string BCC_ADDRESS = "bccAddress";
		public const string EXTRA_PARAMETERS = "extraParameters";
		#endregion

		#region Private Fields
		private string _TemplateName = null;
		private string _Subject = null;
		private string _FirstName = null;
		private string _LastName = null;
		private string _SenderName = null;
		private string _SenderFrom = null;
		private string _SenderTo = null;
		private string _BccAddress = null;
		private IList<KeyValue> _ExtraParameters;
		#endregion

		#region Properties
		[JsonProperty]
		public string TemplateName
		{
			get { return _TemplateName; }
			set 
			{ 
				_TemplateName = value;
				OnPropertyChanged("TemplateName");
			}
		}
		[JsonProperty]
		public string Subject
		{
			get { return _Subject; }
			set 
			{ 
				_Subject = value;
				OnPropertyChanged("Subject");
			}
		}
		[JsonProperty]
		public string FirstName
		{
			get { return _FirstName; }
			set 
			{ 
				_FirstName = value;
				OnPropertyChanged("FirstName");
			}
		}
		[JsonProperty]
		public string LastName
		{
			get { return _LastName; }
			set 
			{ 
				_LastName = value;
				OnPropertyChanged("LastName");
			}
		}
		[JsonProperty]
		public string SenderName
		{
			get { return _SenderName; }
			set 
			{ 
				_SenderName = value;
				OnPropertyChanged("SenderName");
			}
		}
		[JsonProperty]
		public string SenderFrom
		{
			get { return _SenderFrom; }
			set 
			{ 
				_SenderFrom = value;
				OnPropertyChanged("SenderFrom");
			}
		}
		[JsonProperty]
		public string SenderTo
		{
			get { return _SenderTo; }
			set 
			{ 
				_SenderTo = value;
				OnPropertyChanged("SenderTo");
			}
		}
		[JsonProperty]
		public string BccAddress
		{
			get { return _BccAddress; }
			set 
			{ 
				_BccAddress = value;
				OnPropertyChanged("BccAddress");
			}
		}
		[JsonProperty]
		public IList<KeyValue> ExtraParameters
		{
			get { return _ExtraParameters; }
			set 
			{ 
				_ExtraParameters = value;
				OnPropertyChanged("ExtraParameters");
			}
		}
		#endregion

		#region CTor
		public EmailMessage()
		{
		}

		public EmailMessage(JToken node) : base(node)
		{
			if(node["templateName"] != null)
			{
				this._TemplateName = node["templateName"].Value<string>();
			}
			if(node["subject"] != null)
			{
				this._Subject = node["subject"].Value<string>();
			}
			if(node["firstName"] != null)
			{
				this._FirstName = node["firstName"].Value<string>();
			}
			if(node["lastName"] != null)
			{
				this._LastName = node["lastName"].Value<string>();
			}
			if(node["senderName"] != null)
			{
				this._SenderName = node["senderName"].Value<string>();
			}
			if(node["senderFrom"] != null)
			{
				this._SenderFrom = node["senderFrom"].Value<string>();
			}
			if(node["senderTo"] != null)
			{
				this._SenderTo = node["senderTo"].Value<string>();
			}
			if(node["bccAddress"] != null)
			{
				this._BccAddress = node["bccAddress"].Value<string>();
			}
			if(node["extraParameters"] != null)
			{
				this._ExtraParameters = new List<KeyValue>();
				foreach(var arrayNode in node["extraParameters"].Children())
				{
					this._ExtraParameters.Add(ObjectFactory.Create<KeyValue>(arrayNode));
				}
			}
		}
		#endregion

		#region Methods
		public override Params ToParams(bool includeObjectType = true)
		{
			Params kparams = base.ToParams(includeObjectType);
			if (includeObjectType)
				kparams.AddReplace("objectType", "KalturaEmailMessage");
			kparams.AddIfNotNull("templateName", this._TemplateName);
			kparams.AddIfNotNull("subject", this._Subject);
			kparams.AddIfNotNull("firstName", this._FirstName);
			kparams.AddIfNotNull("lastName", this._LastName);
			kparams.AddIfNotNull("senderName", this._SenderName);
			kparams.AddIfNotNull("senderFrom", this._SenderFrom);
			kparams.AddIfNotNull("senderTo", this._SenderTo);
			kparams.AddIfNotNull("bccAddress", this._BccAddress);
			kparams.AddIfNotNull("extraParameters", this._ExtraParameters);
			return kparams;
		}
		protected override string getPropertyName(string apiName)
		{
			switch(apiName)
			{
				case TEMPLATE_NAME:
					return "TemplateName";
				case SUBJECT:
					return "Subject";
				case FIRST_NAME:
					return "FirstName";
				case LAST_NAME:
					return "LastName";
				case SENDER_NAME:
					return "SenderName";
				case SENDER_FROM:
					return "SenderFrom";
				case SENDER_TO:
					return "SenderTo";
				case BCC_ADDRESS:
					return "BccAddress";
				case EXTRA_PARAMETERS:
					return "ExtraParameters";
				default:
					return base.getPropertyName(apiName);
			}
		}
		#endregion
	}
}

