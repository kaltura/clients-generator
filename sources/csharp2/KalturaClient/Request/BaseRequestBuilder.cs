using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Runtime.Serialization;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Xml;

namespace Kaltura.Request
{
    public delegate void OnCompletedHandler<T>(T response, Exception error);
    public delegate void OnErrorHandler(Exception error);

    public abstract class BaseRequestBuilder<T> : BaseRequest, IBaseRequestBuilder
    {
        private string service;
        private OnCompletedHandler<T> onCompletion;
        private OnErrorHandler onError;
        private Client client = null;
        private readonly string requestId;

        public string Boundary
        {
            get;
            set;
        }

        public BaseRequestBuilder(string service)
        {
            this.service = service;

            // Generate a unique task id to group logs
            requestId = (Interlocked.Increment(ref Client.REQUEST_COUNTER)).ToString("X5");
        }

        private void Log(string msg)
        {
            if (client != null && client.Configuration.Logger != null)
            {
                var msgtoLog = string.Format("[{0}] > {1}", requestId, msg);
                client.Configuration.Logger.Log(msgtoLog);
            }
        }

        public Type getReturnType()
        {
            return typeof(T);
        }

        public BaseRequestBuilder<T> SetCompletion(OnCompletedHandler<T> completion)
        {
            onCompletion = completion;
            return this;
        }

        public BaseRequestBuilder<T> SetCompletion(OnErrorHandler errorHandler)
        {
            onError = errorHandler;
            return this;
        }

        public virtual Params getParameters(bool includeServiceAndAction)
        {
            Params kparams = new Params();

            if (client != null)
                kparams.Add(client.RequestConfiguration.ToParams(false));

            kparams.Add(base.ToParams(false));

            if (includeServiceAndAction)
                kparams.Add("service", service);

            return kparams;
        }

        public virtual Files getFiles()
        {
            return new Files();
        }

        public virtual BaseRequestBuilder<T> Build(Client client)
        {
            this.client = client;
            Boundary = "---------------------------" + DateTime.Now.Ticks.ToString("x");
            return this;
        }

        public virtual void OnComplete(object response, Exception error)
        {
            if (onCompletion != null)
            {
                onCompletion((T)response, error);
            }
            if (onError != null && error != null)
            {
                onError(error);
            }
        }

        public async Task<T> ExecuteAsync(Client client = null)
        {
            var sw = new Stopwatch();
            sw.Start();

            if (this.client == null) { this.Build(client); }
            if (this.client == null) { OnComplete(null, new ClientException("No client instance defined")); }

            var url = client.Configuration.ServiceUrl + "/api_v3" + getPath();

            this.Log(string.Format("url : [{0}]", url));

            var files = getFiles();
            var request = BuildRequest(url, files, client.Configuration.Timeout);
            await WriteRequestBodyAsync(files, request);
            var responseObject = await GetResponseAsync(request);
            this.Log(string.Format("execution time for ([{0}]: [{1}]", getPath(), sw.Elapsed));

            return responseObject;
        }

        public void Execute(Client client = null)
        {
            var task = this.ExecuteAsync(client).ContinueWith(t =>
            {
                if (t.Status == TaskStatus.Faulted)
                {
                    var ex = t.Exception.InnerException == null ? t.Exception : t.Exception.InnerException;

                    OnComplete(null, ex); return;
                }

                var result = t.GetAwaiter().GetResult();
                OnComplete(result, null);
            }).ConfigureAwait(false);
        }

        public T ExecuteAndWaitForResponse(Client client = null)
        {
            // Wrapping the async method in task run to avoid deadlock of the syncronization context in some cases.
            // https://stackoverflow.com/questions/17248680/await-works-but-calling-task-result-hangs-deadlocks
            var result = Task.Run(() => ExecuteAsync(client)).GetAwaiter().GetResult();

            return result;
        }



        private async Task<T> GetResponseAsync(HttpWebRequest request)
        {
            T responseObject = default(T);
            try
            {
                using (var response = await request.GetResponseAsync())
                using (var responseStream = response.GetResponseStream())
                using (var responseReader = new StreamReader(responseStream))
                {
                    var responseString = await responseReader.ReadToEndAsync();
                    var headersStr = GetResponseHeadersString(response);
                    this.Log(string.Format("result : {0}", responseString));
                    this.Log(string.Format("result headers : {0}", headersStr));

                    responseObject = ParseResponseString<T>(responseString);
                }
            }
            catch (WebException wex)
            {
                using (var errorResponse = wex.Response)
                {
                    var httpResponse = (HttpWebResponse)errorResponse;
                    this.Log(string.Format("Error code : {0}", httpResponse.StatusCode));
                    using (var responseDataStream = errorResponse.GetResponseStream())
                    using (var reader = new StreamReader(responseDataStream))
                    {
                        var text = reader.ReadToEnd();
                        this.Log(string.Format("ErrorResponse : {0}", text));
                    }
                }

                throw wex;
            }
            catch (Exception e)
            {
                this.Log(string.Format("Error while getting reponse for [{0}] excpetion:{1}", request.RequestUri, e));
                throw e;
            }

            return responseObject;
        }

        private async Task WriteRequestBodyAsync(Files files, HttpWebRequest request)
        {
            var requestBodyStr = GetRequestBodyJsonString();
            var requestBody = Encoding.UTF8.GetBytes(requestBodyStr);
            using (var postStream = await request.GetRequestStreamAsync())
            {

                if (files.Count == 0)
                {
                    await postStream.WriteAsync(requestBody, 0, requestBody.Length);

                }
                else
                {
                    await WriteMultipartRequestBody(postStream, files, requestBodyStr);
                }
            }
        }

        private async Task WriteMultipartRequestBody(Stream postStream, Files files, string jsonBody)
        {
            var paramsBuffer = BuildMultiPartParamsBuffer(jsonBody);
            var paramsBufferBytes = Encoding.UTF8.GetBytes(paramsBuffer);

            await postStream.WriteAsync(paramsBufferBytes, 0, paramsBuffer.Length);

            var bundryBytes = Encoding.UTF8.GetBytes(Boundary);
            var headerTemplate = "Content-Disposition: form-data; name=\"{0}\"; filename=\"{1}\"\r\nContent-Type: {2}\r\n\r\n";

            foreach (var fileEntry in files)
            {
                var fileStream = fileEntry.Value.FileStream;
                var filename = fileEntry.Value.FileName != null ? fileEntry.Value.FileName : "Memory-Stream-Upload";

                var header = string.Format(headerTemplate, fileEntry.Key, filename, "application/octet-stream");
                var headerbytes = Encoding.UTF8.GetBytes(header);
                await postStream.WriteAsync(headerbytes, 0, headerbytes.Length);

                byte[] buffer = new byte[4096];
                int bytesRead = 0;
                while ((bytesRead = fileStream.Read(buffer, 0, buffer.Length)) != 0)
                {
                    await postStream.WriteAsync(buffer, 0, bytesRead);
                }


                var trailer = Encoding.UTF8.GetBytes("\r\n--" + Boundary + "--\r\n");
                await postStream.WriteAsync(trailer, 0, trailer.Length);
            }
        }


        private HttpWebRequest BuildRequest(string url, Files files, int timeout)
        {
            Client client;
            var request = (HttpWebRequest)HttpWebRequest.Create(url);
            request.Timeout = files.Count == 0 ? timeout : Timeout.Infinite;
            request.Method = "POST";

            request.Headers = getHeaders();
            request.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;
            request.Accept = "application/json";
            request.ContentType = getContentType();
            request.Proxy = CreateProxy();
            return request;
        }

        private static string GetResponseHeadersString(WebResponse response)
        {
            var responseHeadersBuilder = new StringBuilder();
            foreach (var key in response.Headers.AllKeys)
            {
                responseHeadersBuilder.Append(key + ":" + response.Headers[key] + " ; ");
            }
            return responseHeadersBuilder.ToString();
        }

        private string GetRequestBodyJsonString()
        {
            var requestBody = "";
            var parameters = getParameters(false);
            parameters.Add(client.ClientConfiguration.ToParams(false));
            parameters.Add("format", EServiceFormat.RESPONSE_TYPE_JSON.GetHashCode());
            parameters.Add("kalsig", Signature(parameters));

            var json = parameters.ToJson();
            this.Log(string.Format("full reqeust data: [{0}]", json));

            requestBody = json;

            return requestBody;
        }

        private string BuildMultiPartParamsBuffer(string json)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("--" + Boundary + "\r\n");
            sb.Append("Content-Disposition: form-data; name=\"json\"\r\n");
            sb.Append("\r\n");
            sb.Append(HttpUtility.UrlDecode(json));
            sb.Append("\r\n--" + Boundary + "\r\n");

            return sb.ToString();
        }

        private void ValidateXmlResult(XmlDocument doc)
        {
            XmlElement xml = doc["xml"];
            if (xml != null)
            {
                XmlElement result = xml["result"];
                if (result != null)
                {
                    return;
                }
            }

            throw new SerializationException("Invalid result");
        }

        private WebProxy CreateProxy()
        {
            var proxyToSet = new WebProxy();
            if (string.IsNullOrEmpty(client.Configuration.ProxyAddress))
                return null;
            Console.WriteLine("Create proxy");
            if (!(string.IsNullOrEmpty(client.Configuration.ProxyUser) || string.IsNullOrEmpty(client.Configuration.ProxyPassword)))
            {
                ICredentials credentials = new NetworkCredential(client.Configuration.ProxyUser, client.Configuration.ProxyPassword);
                proxyToSet = new WebProxy(client.Configuration.ProxyAddress, false, null, credentials);
            }
            else
            {
                proxyToSet = new WebProxy(client.Configuration.ProxyAddress);
            }

            return proxyToSet;
        }

        private string Signature(Params kparams)
        {
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            byte[] data = Encoding.ASCII.GetBytes(kparams.ToJson());
            data = md5.ComputeHash(data);
            StringBuilder sBuilder = new StringBuilder();
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }
            return sBuilder.ToString();
        }

        protected string getContentType()
        {
            if (getFiles().Count > 0)
            {
                return "multipart/form-data; boundary=" + Boundary;
            }
            else
            {
                return "application/json";
            }
        }

        protected WebHeaderCollection getHeaders()
        {
            return client.Configuration.RequestHeaders;
        }

        protected virtual string getPath()
        {
            return "/service/" + service;
        }


    }
}
