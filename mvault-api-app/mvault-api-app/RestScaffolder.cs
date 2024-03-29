﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace mvault_api_app
{
    public class RestScaffolder
    {
        public enum Method { GET, POST, PUT, PATCH, DELETE };
        //public enum Execution { SYNCHRONOUS, ASYNCHRONOUS };

        private string url;
        private Dictionary<String, String> Headers { get; set; }

        public String URL
        {
            get
            {
                return this.url;
            }
            set
            {
                if (value.StartsWith("http"))
                {
                    Console.WriteLine("WARNING: You are accessing a non-TLS secured resource");
                    this.url = value;
                }
            }
        }
        public Method HttpMethod { get; set; }
        //public Execution ExecutionStyle { get; set; }

        public String Body { get; set; }

        public RestScaffolder(string URL, Method method)
        {
            // TODO Timeout, header, method, endpoint, content type
            this.URL = URL;
            this.HttpMethod = method;
            this.Headers = new Dictionary<String, String>();
            this.Body = "";
        }

        public void AddHeader(string key, string value)
        {
            Headers.Add(key, value);
        }

        public Task<HttpResponseMessage> AsynchronousGet()
        {
            return this.executeGet();
        }

        public 

        public HttpResponseMessage SynchronousGet()
        {
            return this.executeGet().Result;

            // by calling .Result you are synchronously reading the result
            //string responseString = responseContent.ReadAsStringAsync().Result;
            //retVal = responseString;
            //Console.WriteLine(responseString);
        }

        public HttpResponseMessage SynchronousPost()
        {
            return this.executePost().Result;
        }

        public Task<HttpResponseMessage> AsynchronousPost()
        {
            return this.executePost();
        }

        private Task<HttpResponseMessage> executeGet()
        {
            if (this.Body.Length > 0)
            {
                Console.WriteLine("ERROR: GET does not support form content; set the Body length to 0");
                // TODO Return stmt
                return null;
            }
            else
            {
                HttpClient _httpClient = new HttpClient();

                foreach (KeyValuePair<string, string> header in this.Headers)
                {
                    _httpClient.DefaultRequestHeaders.TryAddWithoutValidation(header.Key, header.Value);
                }

                var result = _httpClient.GetAsync(this.URL);
                return result;
            }
        }

        private Task<HttpResponseMessage> executePost()
        {
            if (this.HttpMethod != Method.POST)
            {
                Console.WriteLine("ERROR: POST not configured on class");
                return null;
            }
            else
            {
                if (this.Body.Equals(null))
                {
                    Console.WriteLine("WARNING: POST does not contain form data");
                }

                HttpClient _client = new HttpClient();
                var content = new StringContent(this.Body);

                var response = _client.PostAsync(this.URL, content);

                return response;
            }
        }

        public static byte[] HmacSHA256(String data, byte[] key)
        {
            String algorithm = "HmacSHA256";
            KeyedHashAlgorithm kha = KeyedHashAlgorithm.Create(algorithm);
            kha.Key = key;

            return kha.ComputeHash(Encoding.UTF8.GetBytes(data));
        }

        public static byte[] getSignatureKey(String key, String dateStamp, String regionName, String serviceName)
        {
            byte[] kSecret = Encoding.UTF8.GetBytes(("AWS4" + key).ToCharArray());
            byte[] kDate = HmacSHA256(dateStamp, kSecret);
            byte[] kRegion = HmacSHA256(regionName, kDate);
            byte[] kService = HmacSHA256(serviceName, kRegion);
            byte[] kSigning = HmacSHA256("aws4_request", kService);

            return kSigning;
        }
    }
}
