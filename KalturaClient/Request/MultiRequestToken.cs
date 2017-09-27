using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace Kaltura.Request
{
    public class MultiRequestToken
    {
        private IRequestBuilder requestBuilder;
        private string path;

        public MultiRequestToken(IRequestBuilder requestBuilder, string path)
        {
            this.requestBuilder = requestBuilder;
            this.path = path;
        }

        public override string ToString()
        {
            string fixedPath = requestBuilder.MultiRequestIndex + ":result";
            if (path.Length > 0)
                fixedPath += ":" + path.Replace(".", ":");

            return "{" + fixedPath + "}";
        }
    }
}
