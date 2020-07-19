using FrontEnd.Util.API.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Tropical.Infrastructure.Util;
using System.Net.Http;
using System.Net;
using System.IO;

namespace FrontEnd.Util.API
{
    public class BaseAPI
    {
        //SWAGER https://hacktonzenvia.azurewebsites.net/swagger/index.html
        private static HttpClient client = new HttpClient();
        private static string API_BasePathURL = ParameterCollection.GetParamValue("API_BasePathURL");

        protected string Get(string endpoint)
        {
            string objContent = null;

            try
            {
                var webRequest = WebRequest.CreateHttp(API_BasePathURL + endpoint);
                webRequest.Method = "GET";
                webRequest.KeepAlive = false;
                webRequest.ProtocolVersion = HttpVersion.Version10;
                webRequest.ServicePoint.ConnectionLimit = 1;
                
                //System.Net.ServicePointManager.Expect100Continue = false;

                using (var webResponse = webRequest.GetResponse())
                {
                    using (var streamDados = webResponse.GetResponseStream())
                    {
                        using (StreamReader reader = new StreamReader(streamDados))
                        {
                            objContent = reader.ReadToEnd();
                        }
                    }                        
                }
            }
            catch (Exception ex)
            {

                throw;
            }


            return objContent;
        }

        protected string Post(string endpoint, string jsonContent)
        {
            string objContent = null;

            try
            {
                var webRequest = WebRequest.CreateHttp(API_BasePathURL + endpoint);
                webRequest.Method = "POST";
                webRequest.KeepAlive = false;
                webRequest.ContentType = "application/json";
                webRequest.ProtocolVersion = HttpVersion.Version10;
                webRequest.ServicePoint.ConnectionLimit = 1;
                //System.Net.ServicePointManager.Expect100Continue = false;

                using (var streamWriter = new StreamWriter(webRequest.GetRequestStream()))
                {
                    streamWriter.Write(jsonContent);
                }

                using (var webResponse = webRequest.GetResponse())
                {
                    using (var streamDados = webResponse.GetResponseStream())
                    {
                        using (StreamReader reader = new StreamReader(streamDados))
                        {
                            objContent = reader.ReadToEnd();
                        }
                    }
                }
            }
            catch (Exception ex)
            {

                throw;
            }


            return objContent;
        }
        /*
        public class HttpClient
        {
            static readonly net.HttpClient _client;
            static HttpClient()
            {
                _client = new net.HttpClient();
                _client.DefaultRequestHeaders.Add("Accept", "application/json");
            }
            public async Task<Response> PostAsync<Request, Response>(
                string url,
                Request input,
                string token = null)
            {
                return await CreateRequest<Response>(url, net.HttpMethod.Post, input, token);
            }
            public async Task<Response> PutAsync<Request, Response>(
                string url,
                Request input,
                string token = null)
            {
                return await CreateRequest<Response>(url, net.HttpMethod.Put, input, token);
            }
            public async Task<Response> GetAsync<Response>(
                string url,
                string token = null)
            {
                return await CreateRequest<Response>(url, net.HttpMethod.Get, token);
            }
            public async Task<Response> DeleteAsync<Response>(
                string url,
                string token = null)
            {
                return await CreateRequest<Response>(url, net.HttpMethod.Delete, token);
            }
            #region [ -- Private helper methods -- ]
            async Task<Response> CreateRequest<Response>(
                string url,
                net.HttpMethod method,
                string token)
            {
                return await CreateRequestMessage(url, method, token, async (msg) =>
                {
                    return await GetResult<Response>(msg);
                });
            }
            async Task<Response> CreateRequest<Response>(
                string url,
                net.HttpMethod method,
                object input,
                string token)
            {
                return await CreateRequestMessage(url, method, token, async (msg) =>
                {
                    using (var content = new net.StringContent(JObject.FromObject(input).ToString()))
                    {
                        content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                        msg.Content = content;
                        return await GetResult<Response>(msg);
                    }
                });
            }
            async Task<Response> CreateRequestMessage<Response>(
                string url,
                net.HttpMethod method,
                string token,
                Func<net.HttpRequestMessage, Task<Response>> functor)
            {
                using (var msg = new net.HttpRequestMessage())
                {
                    msg.RequestUri = new Uri(url);
                    msg.Method = method;
                    if (token != null)
                        msg.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
                    return await functor(msg);
                }
            }
            async Task<Response> GetResult<Response>(net.HttpRequestMessage msg)
            {
                using (var response = await _client.SendAsync(msg))
                {
                    using (var content = response.Content)
                    {
                        var responseContent = await content.ReadAsStringAsync();
                        if (!response.IsSuccessStatusCode)
                            throw new Exception(responseContent);
                        if (typeof(IConvertible).IsAssignableFrom(typeof(Response)))
                            return (Response)Convert.ChangeType(responseContent, typeof(Response));
                        return JToken.Parse(responseContent).ToObject<Response>();
                    }
                }
            }
            #endregion
        }*/
    }
}