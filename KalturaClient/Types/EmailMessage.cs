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
		public string TemplateName
		{
			get { return _TemplateName; }
			set 
			{ 
				_TemplateName = value;
				OnPropertyChanged("TemplateName");
			}
		}
		public string Subject
		{
			get { return _Subject; }
			set 
			{ 
				_Subject = value;
				OnPropertyChanged("Subject");
			}
		}
		public string FirstName
		{
			get { return _FirstName; }
			set 
			{ 
				_FirstName = value;
				OnPropertyChanged("FirstName");
			}
		}
		public string LastName
		{
			get { return _LastName; }
			set 
			{ 
				_LastName = value;
				OnPropertyChanged("LastName");
			}
		}
		public string SenderName
		{
			get { return _SenderName; }
			set 
			{ 
				_SenderName = value;
				OnPropertyChanged("SenderName");
			}
		}
		public string SenderFrom
		{
			get { return _SenderFrom; }
			set 
			{ 
				_SenderFrom = value;
				OnPropertyChanged("SenderFrom");
			}
		}
		public string SenderTo
		{
			get { return _SenderTo; }
			set 
			{ 
				_SenderTo = value;
				OnPropertyChanged("SenderTo");
			}
		}
		public string BccAddress
		{
			get { return _BccAddress; }
			set 
			{ 
				_BccAddress = value;
				OnPropertyChanged("BccAddress");
			}
		}
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

		public EmailMessage(XmlElement node) : base(node)
		{
			foreach (XmlElement propertyNode in node.ChildNodes)
			{
				switch (propertyNode.Name)
				{
					case "templateName":
						this._TemplateName = propertyNode.InnerText;
						continue;
					case "subject":
						this._Subject = propertyNode.InnerText;
						continue;
					case "firstName":
						this._FirstName = propertyNode.InnerText;
						continue;
					case "lastName":
						this._LastName = propertyNode.InnerText;
						continue;
					case "senderName":
						this._SenderName = propertyNode.InnerText;
						continue;
					case "senderFrom":
						this._SenderFrom = propertyNode.InnerText;
						continue;
					case "senderTo":
						this._SenderTo = propertyNode.InnerText;
						continue;
					case "bccAddress":
						this._BccAddress = propertyNode.InnerText;
						continue;
					case "extraParameters":
						this._ExtraParameters = new List<KeyValue>();
						foreach(XmlElement arrayNode in propertyNode.ChildNodes)
						{
							this._ExtraParameters.Add(ObjectFactory.Create<KeyValue>(arrayNode));
						}
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

