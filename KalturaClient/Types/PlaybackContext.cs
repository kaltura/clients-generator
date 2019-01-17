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
	public class PlaybackContext : ObjectBase
	{
		#region Constants
		public const string SOURCES = "sources";
		public const string ACTIONS = "actions";
		public const string MESSAGES = "messages";
		public const string PLAYBACK_CAPTIONS = "playbackCaptions";
		public const string PLUGINS = "plugins";
		#endregion

		#region Private Fields
		private IList<PlaybackSource> _Sources;
		private IList<RuleAction> _Actions;
		private IList<AccessControlMessage> _Messages;
		private IList<CaptionPlaybackPluginData> _PlaybackCaptions;
		private IList<PlaybackPluginData> _Plugins;
		#endregion

		#region Properties
		public IList<PlaybackSource> Sources
		{
			get { return _Sources; }
			set 
			{ 
				_Sources = value;
				OnPropertyChanged("Sources");
			}
		}
		public IList<RuleAction> Actions
		{
			get { return _Actions; }
			set 
			{ 
				_Actions = value;
				OnPropertyChanged("Actions");
			}
		}
		public IList<AccessControlMessage> Messages
		{
			get { return _Messages; }
			set 
			{ 
				_Messages = value;
				OnPropertyChanged("Messages");
			}
		}
		public IList<CaptionPlaybackPluginData> PlaybackCaptions
		{
			get { return _PlaybackCaptions; }
			set 
			{ 
				_PlaybackCaptions = value;
				OnPropertyChanged("PlaybackCaptions");
			}
		}
		public IList<PlaybackPluginData> Plugins
		{
			get { return _Plugins; }
			set 
			{ 
				_Plugins = value;
				OnPropertyChanged("Plugins");
			}
		}
		#endregion

		#region CTor
		public PlaybackContext()
		{
		}

		public PlaybackContext(XmlElement node) : base(node)
		{
			foreach (XmlElement propertyNode in node.ChildNodes)
			{
				switch (propertyNode.Name)
				{
					case "sources":
						this._Sources = new List<PlaybackSource>();
						foreach(XmlElement arrayNode in propertyNode.ChildNodes)
						{
							this._Sources.Add(ObjectFactory.Create<PlaybackSource>(arrayNode));
						}
						continue;
					case "actions":
						this._Actions = new List<RuleAction>();
						foreach(XmlElement arrayNode in propertyNode.ChildNodes)
						{
							this._Actions.Add(ObjectFactory.Create<RuleAction>(arrayNode));
						}
						continue;
					case "messages":
						this._Messages = new List<AccessControlMessage>();
						foreach(XmlElement arrayNode in propertyNode.ChildNodes)
						{
							this._Messages.Add(ObjectFactory.Create<AccessControlMessage>(arrayNode));
						}
						continue;
					case "playbackCaptions":
						this._PlaybackCaptions = new List<CaptionPlaybackPluginData>();
						foreach(XmlElement arrayNode in propertyNode.ChildNodes)
						{
							this._PlaybackCaptions.Add(ObjectFactory.Create<CaptionPlaybackPluginData>(arrayNode));
						}
						continue;
					case "plugins":
						this._Plugins = new List<PlaybackPluginData>();
						foreach(XmlElement arrayNode in propertyNode.ChildNodes)
						{
							this._Plugins.Add(ObjectFactory.Create<PlaybackPluginData>(arrayNode));
						}
						continue;
				}
			}
		}

		public PlaybackContext(IDictionary<string,object> data) : base(data)
		{
			    this._Sources = new List<PlaybackSource>();
			    foreach(var dataDictionary in data.TryGetValueSafe<IEnumerable<object>>("sources", new List<object>()))
			    {
			        if (dataDictionary == null) { continue; }
			        this._Sources.Add(ObjectFactory.Create<PlaybackSource>((IDictionary<string,object>)dataDictionary));
			    }
			    this._Actions = new List<RuleAction>();
			    foreach(var dataDictionary in data.TryGetValueSafe<IEnumerable<object>>("actions", new List<object>()))
			    {
			        if (dataDictionary == null) { continue; }
			        this._Actions.Add(ObjectFactory.Create<RuleAction>((IDictionary<string,object>)dataDictionary));
			    }
			    this._Messages = new List<AccessControlMessage>();
			    foreach(var dataDictionary in data.TryGetValueSafe<IEnumerable<object>>("messages", new List<object>()))
			    {
			        if (dataDictionary == null) { continue; }
			        this._Messages.Add(ObjectFactory.Create<AccessControlMessage>((IDictionary<string,object>)dataDictionary));
			    }
			    this._PlaybackCaptions = new List<CaptionPlaybackPluginData>();
			    foreach(var dataDictionary in data.TryGetValueSafe<IEnumerable<object>>("playbackCaptions", new List<object>()))
			    {
			        if (dataDictionary == null) { continue; }
			        this._PlaybackCaptions.Add(ObjectFactory.Create<CaptionPlaybackPluginData>((IDictionary<string,object>)dataDictionary));
			    }
			    this._Plugins = new List<PlaybackPluginData>();
			    foreach(var dataDictionary in data.TryGetValueSafe<IEnumerable<object>>("plugins", new List<object>()))
			    {
			        if (dataDictionary == null) { continue; }
			        this._Plugins.Add(ObjectFactory.Create<PlaybackPluginData>((IDictionary<string,object>)dataDictionary));
			    }
		}
		#endregion

		#region Methods
		public override Params ToParams(bool includeObjectType = true)
		{
			Params kparams = base.ToParams(includeObjectType);
			if (includeObjectType)
				kparams.AddReplace("objectType", "KalturaPlaybackContext");
			kparams.AddIfNotNull("sources", this._Sources);
			kparams.AddIfNotNull("actions", this._Actions);
			kparams.AddIfNotNull("messages", this._Messages);
			kparams.AddIfNotNull("playbackCaptions", this._PlaybackCaptions);
			kparams.AddIfNotNull("plugins", this._Plugins);
			return kparams;
		}
		protected override string getPropertyName(string apiName)
		{
			switch(apiName)
			{
				case SOURCES:
					return "Sources";
				case ACTIONS:
					return "Actions";
				case MESSAGES:
					return "Messages";
				case PLAYBACK_CAPTIONS:
					return "PlaybackCaptions";
				case PLUGINS:
					return "Plugins";
				default:
					return base.getPropertyName(apiName);
			}
		}
		#endregion
	}
}

