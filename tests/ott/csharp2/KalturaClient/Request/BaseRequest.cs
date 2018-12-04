using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using Kaltura.Types;

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
            var responseDictionary = (Dictionary<string, object>)ClientBase.serializer.DeserializeObject(response);
            var resultObjectDictionary = responseDictionary.TryGetValueSafe("result", responseDictionary);
            var ex = TryGetAPIError(resultObjectDictionary);
            if (ex != null) { throw ex; }

            var retVal = (T)DeserializeObject(resultObjectDictionary);
            return retVal;
        }

        private static APIException TryGetAPIError(object response)
        {
            var objectDictionary = response as Dictionary<string, object>;
            var errorObject = objectDictionary.TryGetValueSafe<IDictionary<string, object>>("error");
            var objectType = errorObject.TryGetValueSafe<string>("objectType");

            if (objectType != null && objectType.Equals("KalturaAPIException", StringComparison.OrdinalIgnoreCase))
            {
                return new APIException(errorObject["code"].ToString(), errorObject["message"].ToString());
            }

            return null;
        }

        abstract public object Deserialize(XmlElement xmlElement);
        abstract public object DeserializeObject(object obj);

    }

}