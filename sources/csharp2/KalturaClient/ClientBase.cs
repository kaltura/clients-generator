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
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Web;
using System.IO;
using System.Security.Cryptography;
using System.Xml;
using System.Xml.XPath;
using System.Runtime.Serialization;
using System.Threading;
using Kaltura.Types;
using Kaltura.Enums;

namespace Kaltura
{
    public class ClientBase
    {
        protected ClientConfiguration clientConfiguration = new ClientConfiguration();
        protected RequestConfiguration requestConfiguration = new RequestConfiguration();

        public Configuration Configuration
        {
            get;
            set;
        }

        public ClientConfiguration ClientConfiguration
        {
            get { return clientConfiguration; }
        }

        public RequestConfiguration RequestConfiguration
        {
            get { return requestConfiguration; }
        }

        public ClientBase(Configuration config)
        {
            Configuration = config;
        }

        public string GenerateSession(int partnerId, string adminSecretForSigning, string userId = "", SessionType type = SessionType.USER, int expiry = 86400, string privileges = "")
        {
            string ks = string.Format("{0};{0};{1};{2};{3};{4};{5};", partnerId, UnixTimeNow() + expiry, type.GetHashCode(), DateTime.Now.Ticks, userId, privileges);

            SHA1 sha = new SHA1CryptoServiceProvider();

            byte[] ksTextBytes = Encoding.ASCII.GetBytes(adminSecretForSigning + ks);

            byte[] sha1Bytes = sha.ComputeHash(ksTextBytes);

            string sha1Hex = "";
            foreach (char c in sha1Bytes)
                sha1Hex += string.Format("{0:x2}", (int)c);

            ks = sha1Hex.ToLower() + "|" + ks;

            return EncodeTo64(ks);
        }

        public long UnixTimeNow()
        {
            TimeSpan _TimeSpan = (DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0));
            return (long)_TimeSpan.TotalSeconds;
        }

        private string EncodeTo64(string toEncode)
        {
            byte[] toEncodeAsBytes = System.Text.ASCIIEncoding.ASCII.GetBytes(toEncode);
            string returnValue = System.Convert.ToBase64String(toEncodeAsBytes);
            return returnValue;
        }
    }
}
