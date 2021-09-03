using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace Kaltura.Request
{
    public interface IRequestBuilder : IBaseRequestBuilder
    {
        void OnComplete(object response, APIException error);
        Params getParameters(bool includeServiceAndAction);
        Files getFiles();

        MultiRequestToken Forward(string path = "");

        int MultiRequestIndex
        {
            set;
            get;
        }

        string Boundary
        {
            get;
            set;
        }
    }
}
