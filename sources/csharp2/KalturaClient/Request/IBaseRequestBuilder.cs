using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using Newtonsoft.Json.Linq;

namespace Kaltura.Request
{
    public interface IBaseRequestBuilder
    {
        object Deserialize(JToken results);
    }
}
