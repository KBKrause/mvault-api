using System;
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
        public enum Execution { SYNCHRONOUS, ASYNCHRONOUS };

        private string url;
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
        public Execution ExecutionStyle { get; set; }

        public String Body { get; set; }
        private Dictionary <String, String> Headers { get; set; }

        public RestScaffolder(string URL, Method method)
        {
            this.URL = URL;
            this.HttpMethod = method;
            this.Headers = new Dictionary<String, String>();
            this.ExecutionStyle = Execution.SYNCHRONOUS;
            this.Body = "";
        }

        public void AddHeader(string key, string value)
        {
            Headers.Add(key, value);
        }

        public async Task<string> ExecuteGet()
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

                foreach(KeyValuePair<string, string> header in this.Headers)
                {
                    _httpClient.DefaultRequestHeaders.TryAddWithoutValidation(header.Key, header.Value);
                }

                using (var result = await _httpClient.GetAsync(this.URL))
                {
                    string content = await result.Content.ReadAsStringAsync();
                    return content;
                }
            }
        }

        public async Task ExecuteStringPost()
        {
            if (this.HttpMethod != Method.POST)
            {
                Console.WriteLine("ERROR: POST not configured on class");
                return;
            }
            else 
            {
                if (this.Body.Equals(null))
                {
                    Console.WriteLine("WARNING: POST does not contain form data");
                }

                HttpClient _client = new HttpClient();
                var content = new StringContent(this.Body);

                var response = await _client.PostAsync(this.URL, content);

                var responseString = await response.Content.ReadAsStringAsync();
                Console.WriteLine("Resp: " + responseString);
            }
        }

        private String SynchronousGet()
        {
            String retVal = "";

            using (var client = new HttpClient())
            {
                var response = client.GetAsync("https://httpbin.org/get").Result;

                if (response.IsSuccessStatusCode)
                {
                    var responseContent = response.Content;

                    // by calling .Result you are synchronously reading the result
                    string responseString = responseContent.ReadAsStringAsync().Result;
                    retVal = responseString;
                    Console.WriteLine(responseString);
                }
            }

            return retVal;
        }

        static byte[] HmacSHA256(String data, byte[] key)
        {
            String algorithm = "HmacSHA256";
            KeyedHashAlgorithm kha = KeyedHashAlgorithm.Create(algorithm);
            kha.Key = key;

            return kha.ComputeHash(Encoding.UTF8.GetBytes(data));
        }

        static byte[] getSignatureKey(String key, String dateStamp, String regionName, String serviceName)
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
