using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace mvault_api_app
{
    public class RestScaffolder
    {
        public enum Method { GET, POST, PUT, PATCH, DELETE };
        public String URL { get; set; }
        public Method HttpMethod { get; set; }

        public String Body { get; set; }
        public Dictionary <String, String> Headers { get; set; }

        public RestScaffolder(string URL, Method method)
        {
            this.URL = URL;
            this.HttpMethod = method;
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
    }
}
