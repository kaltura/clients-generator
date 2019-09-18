using System;
using System.Diagnostics;
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

        public int? ResponseLogLength { get; set; }
        public string Boundary { get; set; }

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
            var requestBodyStr = GetRequestBodyJsonString();

            var timeoutMs = files.Count == 0 ? client.Configuration.Timeout : Timeout.Infinite;
            var cts = new CancellationTokenSource();
            cts.CancelAfter(TimeSpan.FromMilliseconds(timeoutMs));

            try
            {
                var content = GetPostBodyHttpContent(files, requestBodyStr);
                SetRequestHeaders(content);

                var response = await client.HttpClient.PostAsync(url, content, cts.Token);
                var responseString = await response.Content.ReadAsStringAsync();
                var headersStr = GetResponseHeadersString(response);

                this.Log(string.Format("result : {0}", responseString));
                this.Log(string.Format("result headers : {0}", headersStr));

                response.EnsureSuccessStatusCode();
                var responseObject = ParseResponseString<T>(responseString);
                this.Log(string.Format("execution time for ([{0}]: [{1}]", getPath(), sw.Elapsed));
                return responseObject;
            }
            catch (Exception e)
            {
                this.Log(string.Format("Error General Exception occored during request, ex:{0}", e));
                throw;
            }
        }

        private static HttpContent GetPostBodyHttpContent(Files files, string requestBodyStr)
        {
            if (files.Count == 0)
            {
                var jsonContent = new StringContent(requestBodyStr);
                jsonContent.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json");
                return jsonContent;
            }
            else
            {
                var form = new MultipartFormDataContent();
                form.Add(new StringContent(requestBodyStr), "json");
                foreach (var fileEntry in files)
                {
                    var filename = fileEntry.Value.FileName ?? "Memory-Stream-Upload";
                    var fileContent = new StreamContent(fileEntry.Value.FileStream);

                    form.Add(fileContent, fileEntry.Key, filename);
                }

                return form;
            }
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

        private static string GetResponseHeadersString(HttpResponseMessage response)
        {
            var responseHeadersBuilder = new StringBuilder();
            foreach (var header in response.Headers)
            {
                responseHeadersBuilder.Append(header.Key + ":" + header.Value + " ; ");
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

        private void SetRequestHeaders(HttpContent reqContent)
        {
            var headers = getHeaders();
            foreach (var headerKey in headers.AllKeys)
            {
                reqContent.Headers.Add(headerKey, headers[headerKey]);
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
