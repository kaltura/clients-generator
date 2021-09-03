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
using System.Linq;
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
using System.Net.Http;
using System.Net.Http.Headers;

namespace Kaltura
{
    public class ClientBase : ClientUtils
    {
        internal static int REQUEST_COUNTER = 0;

        protected ClientConfiguration clientConfiguration = new ClientConfiguration();
        protected RequestConfiguration requestConfiguration = new RequestConfiguration();

        public int? ResponseLogLength { get; set; }

        public Configuration Configuration { get; set; }

        public ClientConfiguration ClientConfiguration
        {
            get { return clientConfiguration; }
        }

        public RequestConfiguration RequestConfiguration
        {
            get { return requestConfiguration; }
        }
        public HttpClient HttpClient { get; internal set; }



        public ClientBase(Configuration config)
        {
            Configuration = config;

            var httpClientHandler = new HttpClientHandler
            {
                AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate,
                Proxy = CreateProxy(),
#if NETSTANDARD2_0
                MaxConnectionsPerServer = Configuration.MaxConnectionsPerServer,
#endif
            };

            ServicePointManager.DefaultConnectionLimit = Configuration.MaxConnectionsPerServer;
            HttpClient = new HttpClient(httpClientHandler);
            HttpClient.Timeout = Timeout.InfiniteTimeSpan;
            HttpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }



        private WebProxy CreateProxy()
        {
            WebProxy proxyToSet;
            if (string.IsNullOrEmpty(Configuration.ProxyAddress)) { return null; }

            if (!(string.IsNullOrEmpty(Configuration.ProxyUser) || string.IsNullOrEmpty(Configuration.ProxyPassword)))
            {
                ICredentials credentials = new NetworkCredential(Configuration.ProxyUser, Configuration.ProxyPassword);
                proxyToSet = new WebProxy(Configuration.ProxyAddress, false, null, credentials);
            }
            else
            {
                proxyToSet = new WebProxy(Configuration.ProxyAddress);
            }

            return proxyToSet;
        }

    }
}
