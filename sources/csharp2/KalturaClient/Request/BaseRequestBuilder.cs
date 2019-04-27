using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.Serialization;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Xml;
using Kaltura.Types;
using Newtonsoft.Json.Linq;
using String = System.String;

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
        private static readonly HttpClientHandler _HttpClientHandler = new HttpClientHandler() { AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate };
        private static readonly HttpClient _HttpClient = new HttpClient(_HttpClientHandler);

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

            Log(string.Format("url : [{0}]", url));

            var files = getFiles();
            var responseObject = await SendRequestAsync(url, files, client.Configuration.Timeout);
            sw.Stop();
            Log(string.Format("execution time for ([{0}]: [{1}]", getPath(), sw.Elapsed));

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

        private async Task<T> SendRequestAsync(string url, Files files, int timeout)
        {
            try
            {
                _HttpClientHandler.Proxy = CreateProxy();
                _HttpClient.Timeout = files.Count == 0 ? TimeSpan.FromMilliseconds(timeout) : Timeout.InfiniteTimeSpan;
                _HttpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var jsonBodyStr = GetRequestJsonBodyString();

                HttpResponseMessage response;
                using (var content = GetHttpRequestContent(files, jsonBodyStr))
                {
                    var additionalRequestHeaders = getHeaders();
                    foreach (var key in additionalRequestHeaders.AllKeys)
                    {
                        content.Headers.Add(key, additionalRequestHeaders[key]);
                    }
                    var requestHeadersStr = content.Headers.ToString().Replace("\r\n", ", ");
                    Log(string.Format("reqeust header: [{0}]", requestHeadersStr));
                    response = await _HttpClient.PostAsync(url, content);
                }

                var responseString = await response.Content.ReadAsStringAsync();
                var responseHeadersStr = response.Headers.ToString().Replace("\r\n", ", ");
                Log(string.Format("result : {0}", responseString));
                Log(string.Format("result headers : {0}", responseHeadersStr));

                if (response.IsSuccessStatusCode)
                {
                    var responseObject = ParseResponseString<T>(responseString);
                    return responseObject;
                }
                else
                {
                    throw new Exception(string.Format("Error while getting reponse for [{0}] status code:{1}", url, response.StatusCode));
                }
            }
            catch (Exception e)
            {
                this.Log(string.Format("Error while getting reponse for [{0}] excpetion:{1}", url, e));
                throw e;
            }
        }

        private static HttpContent GetHttpRequestContent(Files files, string jsonBodyStr)
        {
            if (files.Count == 0)
            {
                var content = new StringContent(jsonBodyStr);
                content.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json");
                return content;

            }
            else
            {
                var multipartContent = new MultipartFormDataContent();

                //Content-Disposition: form-data; name="json"
                var stringContent = new StringContent(jsonBodyStr);
                stringContent.Headers.Add("Content-Disposition", "form-data; name=\"json\"");
                multipartContent.Add(stringContent, "json");

                foreach (var fileEntry in files)
                {
                    var fileStream = fileEntry.Value;
                    var streamContent = new StreamContent(fileStream);
                    streamContent.Headers.Add("Content-Type", "application/octet-stream");
                    if (fileStream is FileStream)
                    {
                        var fs = (FileStream)fileStream;
                        streamContent.Headers.Add("Content-Disposition", "form-data; name=\"" + fileEntry.Key + "\"; filename=\"" + Path.GetFileName(fs.Name) + "\"");
                    }
                    else if (fileStream is MemoryStream)
                    {
                        streamContent.Headers.Add("Content-Disposition", "form-data; name=\"" + fileEntry.Key + "\"; filename=\"Memory-Stream-Upload\"");
                    }

                    multipartContent.Add(streamContent, "file", "Memory-Stream-Upload");
                }

                return multipartContent;
            }
        }

        private string GetRequestJsonBodyString()
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