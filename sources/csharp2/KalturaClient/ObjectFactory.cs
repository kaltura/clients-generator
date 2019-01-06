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
using System.Runtime.Serialization;
using System.Text.RegularExpressions;
using System.Xml;
using Kaltura.Types;
using Newtonsoft.Json.Linq;

namespace Kaltura
{
    public static class ObjectFactory
    {
        private static Regex prefixRegex = new Regex("^Kaltura");

        public static T Create<T>(JToken jToken)where T : ObjectBase
        {
            if (jToken["objectType"] == null)
            {
                return null;
            }

            string className = jToken["objectType"].Value<string>();
            className = prefixRegex.Replace(className, "");

            Type type = Type.GetType("Kaltura.Types." + className);
            if (type == null)
            {
                type = typeof(T);
            }

            if (type == null)
                throw new SerializationException("Invalid object type");

            return (T)System.Activator.CreateInstance(type, jToken);
        }

    }
}
