using System;
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
            var responseObj = ClientBase.serializer.DeserializeObject(response);
            var ex = TryGetAPIError(responseObj);
            if (ex != null) { throw ex; }

            var retVal = (T) DeserializeObject(responseObj);
            return retVal;
        }

        private static APIException TryGetAPIError(object response)
        {
            var objectDictionary = response as Dictionary<string,object>;
            var objectType = objectDictionary.TryGetValueSafe<string>("objectType");
            
            if (objectType != null && objectType.ToString().Equals("KalturaAPIException",StringComparison.OrdinalIgnoreCase)) 
            {  
                return new APIException(objectDictionary["code"].ToString(), objectDictionary["message"].ToString());
            } 

            return null;
        }

        abstract public object Deserialize(XmlElement xmlElement);
        abstract public object DeserializeObject(object obj);
        
    }


}
