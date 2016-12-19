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
// Copyright (C) 2006-2011  Kaltura Inc.
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
using System.Text;
using System.ComponentModel;
using System.Collections.Generic;
using Kaltura.Request;

namespace Kaltura.Types
{
    public class ObjectBase : INotifyPropertyChanged
    {
        #region Private Fields

        private IDictionary<string, IListResponse> _RelatedObjects;

        protected Dictionary<string, MultiRequestToken> multiRequestMappings = null;

        #endregion

        #region Properties

        public IDictionary<string, IListResponse> RelatedObjects
        {
            get { return _RelatedObjects; }
            set
            {
                _RelatedObjects = value;
                OnPropertyChanged("RelatedObjects");
            }
        }

        #endregion

        #region CTor

		public ObjectBase()
		{
		}

        public ObjectBase(XmlElement node)
        {
            foreach (XmlElement propertyNode in node.ChildNodes)
            {
                switch (propertyNode.Name)
                {
                    case "relatedObjects":
                        {
                            string key;
                            this.RelatedObjects = new Dictionary<string, IListResponse>();
                            foreach (XmlElement arrayNode in propertyNode.ChildNodes)
                            {
                                key = arrayNode["itemKey"].InnerText;
                                this.RelatedObjects[key] = ObjectFactory.Create(arrayNode);
                            }
                        }
                        continue;
                }
            }
		}

        #endregion

        #region Methods

        public ObjectBase Map(string key, MultiRequestToken token)
        {
            if (multiRequestMappings == null)
            {
                multiRequestMappings = new Dictionary<string, MultiRequestToken>();
            }

            multiRequestMappings.Add(key, token);
            string propertyName = getPropertyName(key);
            if (propertyName != null)
                OnPropertyChanged(propertyName);

            return this;
        }

        public virtual Params ToParams(bool includeObjectType = true)
        {
            Params kparams = new Params();

            if (multiRequestMappings != null)
            {
                foreach (KeyValuePair<string, MultiRequestToken> keyValue in multiRequestMappings)
                {
                    kparams.Add(keyValue.Key, keyValue.Value.ToString());
                }
            }

            return kparams;
        }

        protected virtual string getPropertyName(string apiName)
        {
            return null;
        }

        protected int ParseInt(string s)
        {
            int i = int.MinValue;
            int.TryParse(s, out i);
            return i;
        }

        protected Single ParseFloat(string s)
        {
            Single i = Single.MinValue;
            Single.TryParse(s, out i);
            return i;
        }
        
        protected long ParseLong(string s)
        {
            long l = long.MinValue;
            long.TryParse(s, out l);
            return l;
        }

        protected Enum ParseEnum(Type type, string s)
        {
            int i = this.ParseInt(s);
            return (Enum)Enum.Parse(type, i.ToString());
        }

        protected bool ParseBool(string s)
        {
            return s.Equals("1") || s.ToLower().Equals("true");
        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion

        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion
    }
}