using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using Kaltura.Types;
using Newtonsoft.Json.Linq;

namespace Kaltura.Request
{
    /// <summary>
    /// This is an OTT Base Request
    /// Containing the method to parse the response object
    /// </summary>

    public abstract class BaseRequest : RequestConfiguration
    {
        protected virtual T ParseResponseString<T>(string response)
        {
            T responseObject = default(T);
            var jResult = JToken.Parse(response);

            // Validate 
            if (jResult.Type == JTokenType.Object)
            {
                var apiError = GetAPIError(jResult);
                if (apiError != null)
                {
                    throw apiError;
                }
            }

            // this cast should always work because the code is generated for every type and it returns its own object
            // instead of boxing and unboxing we should consider to use T as resposne and change the genrator code
            return (T)Deserialize(jResult);
        }

        protected APIException GetAPIError(JToken result)
        {
            // If the token is not an object this might be an array or primitive type so there is no error 
            if (result.Type != JTokenType.Object) { return null; }

            var objectType = result["objectType"];
            if (objectType != null)
            {
                var isError = objectType.Value<string>() == "KalturaAPIException";

                if (isError && result["code"] != null && result["message"] != null)
                {
                    return new APIException(result["code"].Value<string>(), result["message"].Value<string>());
                }
            }

            return null;
        }

        abstract public MultiRequestBuilder Add(IRequestBuilder requestBuilder);
        abstract public object Deserialize(JToken xmlElement);

    }

}