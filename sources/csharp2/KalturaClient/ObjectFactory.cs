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
using System.Text;
using System.Xml;
using System.Runtime.Serialization;
using System.Text.RegularExpressions;

namespace Kaltura
{
	public static class ObjectFactory
	{
        private static Regex regex = new Regex("^Kaltura");

		public static object Create(XmlElement xmlElement, string fallbackClass)
		{
			if (xmlElement["objectType"] == null)
			{
				return null;
			}
			string className = xmlElement["objectType"].InnerText;
            className = regex.Replace(className, "");

			Type type = Type.GetType("Kaltura.Types." + className);
			if (type == null)
			{
				if (fallbackClass != null)
                    type = Type.GetType("Kaltura.Types." + fallbackClass);
			}
			
			if(type == null)
				throw new SerializationException("Invalid object type");
			return System.Activator.CreateInstance(type, xmlElement);
		}
	}
}
