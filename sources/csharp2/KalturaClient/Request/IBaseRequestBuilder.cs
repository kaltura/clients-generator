using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace Kaltura.Request
{
    public interface IBaseRequestBuilder
    {
        object Deserialize(XmlElement results);
    }
}
