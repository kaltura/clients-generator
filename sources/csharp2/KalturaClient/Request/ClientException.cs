using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Runtime.Serialization;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Web;
using System.Xml;

namespace Kaltura.Request
{
    public class ClientException : Exception
    {
        public ClientException(string message)
            : base(message)
        {
        }
    }
}
