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

        public async Task<string> Get()
        {
            HttpClient _httpClient = new HttpClient();
            using (var result = await _httpClient.GetAsync("https://postman-echo.com/get?foo1=bar1&foo2=bar2"))
            {
                string content = await result.Content.ReadAsStringAsync();
                return content;
            }
        }
    }
}
