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
// Copyright (C) 2006-2019  Kaltura Inc.
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
		[JsonProperty]
		public IList<PlaybackSource> Sources
		{
			get { return _Sources; }
			set 
			{ 
				_Sources = value;
				OnPropertyChanged("Sources");
			}
		}
		[JsonProperty]
		public IList<RuleAction> Actions
		{
			get { return _Actions; }
			set 
			{ 
				_Actions = value;
				OnPropertyChanged("Actions");
			}
		}
		[JsonProperty]
		public IList<AccessControlMessage> Messages
		{
			get { return _Messages; }
			set 
			{ 
				_Messages = value;
				OnPropertyChanged("Messages");
			}
		}
		[JsonProperty]
		public IList<CaptionPlaybackPluginData> PlaybackCaptions
		{
			get { return _PlaybackCaptions; }
			set 
			{ 
				_PlaybackCaptions = value;
				OnPropertyChanged("PlaybackCaptions");
			}
		}
		[JsonProperty]
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

		public PlaybackContext(JToken node) : base(node)
		{
			if(node["sources"] != null)
			{
				this._Sources = new List<PlaybackSource>();
				foreach(var arrayNode in node["sources"].Children())
				{
					this._Sources.Add(ObjectFactory.Create<PlaybackSource>(arrayNode));
				}
			}
			if(node["actions"] != null)
			{
				this._Actions = new List<RuleAction>();
				foreach(var arrayNode in node["actions"].Children())
				{
					this._Actions.Add(ObjectFactory.Create<RuleAction>(arrayNode));
				}
			}
			if(node["messages"] != null)
			{
				this._Messages = new List<AccessControlMessage>();
				foreach(var arrayNode in node["messages"].Children())
				{
					this._Messages.Add(ObjectFactory.Create<AccessControlMessage>(arrayNode));
				}
			}
			if(node["playbackCaptions"] != null)
			{
				this._PlaybackCaptions = new List<CaptionPlaybackPluginData>();
				foreach(var arrayNode in node["playbackCaptions"].Children())
				{
					this._PlaybackCaptions.Add(ObjectFactory.Create<CaptionPlaybackPluginData>(arrayNode));
				}
			}
			if(node["plugins"] != null)
			{
				this._Plugins = new List<PlaybackPluginData>();
				foreach(var arrayNode in node["plugins"].Children())
				{
					this._Plugins.Add(ObjectFactory.Create<PlaybackPluginData>(arrayNode));
				}
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

