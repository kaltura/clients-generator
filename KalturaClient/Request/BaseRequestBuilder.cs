using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Runtime.Serialization;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Xml;
using Kaltura.Types;

namespace Kaltura.Request
{
    public delegate void OnCompletedHandler<T>(T response, Exception error);
    public delegate void OnErrorHandler(Exception error);

    public abstract class BaseRequestBuilder<T> : RequestConfiguration, IBaseRequestBuilder
    {
        const int BUFFER_SIZE = 1024;

        private string service;
        private OnCompletedHandler<T> onCompletion;
        private OnErrorHandler onError;
        private Client client = null;

        private DateTime startTime;
        private StringBuilder requestData;
        private byte[] bufferRead;
        private HttpWebRequest request;
        private Stream responseStream;
        // Create Decoder for appropriate enconding type.
        private Decoder streamDecode = Encoding.UTF8.GetDecoder();

        public string Boundary
        {
            get;
            set;
        }

        public BaseRequestBuilder(string service)
        {
            this.service = service;
            bufferRead = new byte[BUFFER_SIZE];
            requestData = new StringBuilder(System.String.Empty);
        }

        abstract public MultiRequestBuilder Add(IRequestBuilder requestBuilder);
        abstract public object Deserialize(XmlElement xmlElement);

        private void Log(string msg)
        {
            if (client != null && client.Configuration.Logger != null)
            {
                client.Configuration.Logger.Log(msg);
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

        public virtual BaseRequestBuilder<T> Build(Client client)
        {
            this.client = client;
            Boundary = "---------------------------" + DateTime.Now.Ticks.ToString("x");
            return this;
        }

        public void Execute(Client client = null)
        {
            startTime = DateTime.Now;

            if (this.client == null)
            {
                this.Build(client);
            }

            if (this.client == null)
            {
                OnComplete(null, new ClientException("No client instance defined"));
            }

            string url = client.Configuration.ServiceUrl + "/api_v3" + getPath();
            this.Log("url: [" + url + "]");

            // build request
            request = (HttpWebRequest)HttpWebRequest.Create(url);
            if (getFiles().Count == 0)
            {
                request.Timeout = client.Configuration.Timeout;
            }
            else
            {
                request.Timeout = Timeout.Infinite;
            }
            request.Method = "POST";

            request.Headers = getHeaders();
            request.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;
            request.Accept = "application/xml";
            request.ContentType = getContentType();

            // Add proxy information if required
            CreateProxy(request);

            request.BeginGetRequestStream(new AsyncCallback(RequestCallback), this);
        }

        public T ExecuteAndWaitForResponse(Client client = null)
        {
            var taskWrapper = Task.Run(() =>
            {
                var taskResult = new TaskCompletionSource<T>();
                this.SetCompletion((result, err) =>
                    {
                        if (err != null)
                        {
                            taskResult.TrySetException(err);
                        }
                        else
                        {
                            taskResult.TrySetResult(result);
                        }
                    })
                    .Execute(client);
                return taskResult.Task;
            });

            taskWrapper.Wait();
            return taskWrapper.Result;
        }

        private void RequestCallback(IAsyncResult result)
        {
            Params parameters = getParameters(false);
            parameters.Add(client.ClientConfiguration.ToParams(false));
            parameters.Add("format", EServiceFormat.RESPONSE_TYPE_XML.GetHashCode());
            parameters.Add("kalsig", Signature(parameters));

            string json = parameters.ToJson();
            this.Log("full reqeust data: [" + json + "]");

            Stream postStream = request.EndGetRequestStream(result);

            if (getFiles().Count == 0)
            {
                byte[] byteArray = Encoding.UTF8.GetBytes(json);
                postStream.Write(byteArray, 0, json.Length);
            }
            else
            {
                byte[] paramsBuffer = BuildMultiPartParamsBuffer(json);
                postStream.Write(paramsBuffer, 0, paramsBuffer.Length);

                SortedList<string, MultiPartFileDescriptor> filesDescriptions = new SortedList<string, MultiPartFileDescriptor>();
                foreach (KeyValuePair<string, Stream> file in getFiles())
                    filesDescriptions.Add(file.Key, BuildMultiPartFileDescriptor(file));

                foreach (KeyValuePair<string, MultiPartFileDescriptor> fileDesc in filesDescriptions)
                {
                    postStream.Write(fileDesc.Value._Header, 0, fileDesc.Value._Header.Length);

                    byte[] buffer = new Byte[checked(Math.Min((uint)1048576, fileDesc.Value._Stream.Length))];
                    int bytesRead = 0;
                    while ((bytesRead = fileDesc.Value._Stream.Read(buffer, 0, buffer.Length)) != 0)
                        postStream.Write(buffer, 0, bytesRead);

                    postStream.Write(fileDesc.Value._Footer, 0, fileDesc.Value._Footer.Length);
                }
            }
            postStream.Close();

            request.BeginGetResponse(new AsyncCallback(ResponseCallback), this);
        }

        private byte[] BuildMultiPartParamsBuffer(string json)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("--" + Boundary + "\r\n");
            sb.Append("Content-Disposition: form-data; name=\"json\"\r\n");
            sb.Append("\r\n");
            sb.Append(HttpUtility.UrlDecode(json));
            sb.Append("\r\n--" + Boundary + "\r\n");

            return Encoding.UTF8.GetBytes(sb.ToString());
        }

        private MultiPartFileDescriptor BuildMultiPartFileDescriptor(KeyValuePair<string, Stream> fileEntry)
        {
            MultiPartFileDescriptor result = new MultiPartFileDescriptor();
            result._Stream = fileEntry.Value;

            // Build header
            StringBuilder sb = new StringBuilder();
            Stream fileStream = fileEntry.Value;
            if (fileStream is FileStream)
            {
                FileStream fs = (FileStream)fileStream;
                sb.Append("Content-Disposition: form-data; name=\"" + fileEntry.Key + "\"; filename=\"" + Path.GetFileName(fs.Name) + "\"" + "\r\n");
            }
            else if (fileStream is MemoryStream)
            {
                sb.Append("Content-Disposition: form-data; name=\"" + fileEntry.Key + "\"; filename=\"Memory-Stream-Upload\"" + "\r\n");
            }
            sb.Append("Content-Type: application/octet-stream" + "\r\n");
            sb.Append("\r\n");
            result._Header = Encoding.UTF8.GetBytes(sb.ToString());

            result._Footer = Encoding.UTF8.GetBytes("\r\n--" + Boundary + "\r\n");

            return result;
        }

        private void ResponseCallback(IAsyncResult result)
        {
            // Call EndGetResponse, which produces the WebResponse object
            //  that came from the request issued above.
            WebResponse response;
            try
            {
                response = request.EndGetResponse(result);
            }
            catch (WebException e)
            {
                OnComplete(null, e);
                return;
            }

            //  Start reading data from the response stream.
            responseStream = response.GetResponseStream();

            //  Pass rs.BufferRead to BeginRead. Read data into rs.BufferRead
            responseStream.BeginRead(bufferRead, 0, BUFFER_SIZE, new AsyncCallback(ReadCallBack), this);
        }

        private void ReadCallBack(IAsyncResult result)
        {
            // Read rs.BufferRead to verify that it contains data. 
            int read = responseStream.EndRead(result);
            if (read > 0)
            {
                // Prepare a Char array buffer for converting to Unicode.
                Char[] charBuffer = new Char[BUFFER_SIZE];

                // Convert byte stream to Char array and then to String.
                // len contains the number of characters converted to Unicode.
                int len = streamDecode.GetChars(bufferRead, 0, read, charBuffer, 0);
                
                // Append the recently read data to the RequestData stringbuilder
                // object contained in RequestState.
                requestData.Append(Encoding.UTF8.GetString(bufferRead, 0, read));

                // Continue reading data until 
                // responseStream.EndRead returns –1.
                responseStream.BeginRead(bufferRead, 0, BUFFER_SIZE, new AsyncCallback(ReadCallBack), this);
            }
            else
            {
                if (requestData.Length > 0)
                {
                    string responseString = requestData.ToString();
                    this.Log("result (serialized): " + responseString);

                    DateTime endTime = DateTime.Now;

                    this.Log("execution time for [" + getPath() + "]: [" + (endTime - startTime).ToString() + "]");

                    XmlDocument xml = new XmlDocument();
                    xml.LoadXml(responseString);

                    ValidateXmlResult(xml);
                    XmlElement resultXml = xml["xml"]["result"];

                    OnComplete(Deserialize(resultXml), GetAPIError(resultXml));
                }
                // Close down the response stream.
                responseStream.Close();
            }
            return;
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

        protected APIException GetAPIError(XmlElement result)
        {
            XmlElement error = result["error"];
            if (error != null && error["code"] != null && error["message"] != null)
            {
                return new APIException(error["code"].InnerText, error["message"].InnerText);
            }

            return null;
        }

        private void CreateProxy(HttpWebRequest request)
        {
            if (string.IsNullOrEmpty(client.Configuration.ProxyAddress))
                return;
            Console.WriteLine("Create proxy");
            if (!(string.IsNullOrEmpty(client.Configuration.ProxyUser) || string.IsNullOrEmpty(client.Configuration.ProxyPassword)))
            {
                ICredentials credentials = new NetworkCredential(client.Configuration.ProxyUser, client.Configuration.ProxyPassword);
                request.Proxy = new WebProxy(client.Configuration.ProxyAddress, false, null, credentials);
            }
            else
            {
                request.Proxy = new WebProxy(client.Configuration.ProxyAddress);
            }
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

        #region Support Types

        private struct MultiPartFileDescriptor
        {
            public Stream _Stream;
            public byte[] _Header;
            public byte[] _Footer;

            public long GetTotalLength()
            {
                return _Stream.Length + _Header.LongLength + _Footer.LongLength;
            }
        }

        #endregion
    }
}
