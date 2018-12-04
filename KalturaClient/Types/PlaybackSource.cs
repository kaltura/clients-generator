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
	public class PlaybackSource : MediaFile
	{
		#region Constants
		public const string FORMAT = "format";
		public const string PROTOCOLS = "protocols";
		public const string DRM = "drm";
		#endregion

		#region Private Fields
		private string _Format = null;
		private string _Protocols = null;
		private IList<DrmPlaybackPluginData> _Drm;
		#endregion

		#region Properties
		public string Format
		{
			get { return _Format; }
			set 
			{ 
				_Format = value;
				OnPropertyChanged("Format");
			}
		}
		public string Protocols
		{
			get { return _Protocols; }
			set 
			{ 
				_Protocols = value;
				OnPropertyChanged("Protocols");
			}
		}
		public IList<DrmPlaybackPluginData> Drm
		{
			get { return _Drm; }
			set 
			{ 
				_Drm = value;
				OnPropertyChanged("Drm");
			}
		}
		#endregion

		#region CTor
		public PlaybackSource()
		{
		}

		public PlaybackSource(XmlElement node) : base(node)
		{
			foreach (XmlElement propertyNode in node.ChildNodes)
			{
				switch (propertyNode.Name)
				{
					case "format":
						this._Format = propertyNode.InnerText;
						continue;
					case "protocols":
						this._Protocols = propertyNode.InnerText;
						continue;
					case "drm":
						this._Drm = new List<DrmPlaybackPluginData>();
						foreach(XmlElement arrayNode in propertyNode.ChildNodes)
						{
							this._Drm.Add(ObjectFactory.Create<DrmPlaybackPluginData>(arrayNode));
						}
						continue;
				}
			}
		}

		public PlaybackSource(IDictionary<string,object> data) : base(data)
		{
			    this._Format = data.TryGetValueSafe<string>("format");
			    this._Protocols = data.TryGetValueSafe<string>("protocols");
			    this._Drm = new List<DrmPlaybackPluginData>();
			    foreach(var dataDictionary in data.TryGetValueSafe<IEnumerable<object>>("drm", new List<object>()))
			    {
			        if (dataDictionary == null) { continue; }
			        this._Drm.Add(ObjectFactory.Create<DrmPlaybackPluginData>((IDictionary<string,object>)dataDictionary));
			    }
		}
		#endregion

		#region Methods
		public override Params ToParams(bool includeObjectType = true)
		{
			Params kparams = base.ToParams(includeObjectType);
			if (includeObjectType)
				kparams.AddReplace("objectType", "KalturaPlaybackSource");
			kparams.AddIfNotNull("format", this._Format);
			kparams.AddIfNotNull("protocols", this._Protocols);
			kparams.AddIfNotNull("drm", this._Drm);
			return kparams;
		}
		protected override string getPropertyName(string apiName)
		{
			switch(apiName)
			{
				case FORMAT:
					return "Format";
				case PROTOCOLS:
					return "Protocols";
				case DRM:
					return "Drm";
				default:
					return base.getPropertyName(apiName);
			}
		}
		#endregion
	}
}

