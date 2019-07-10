using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace mvault_api_app
{
    public class Class1
    {
        private static RestScaffolder rest;
        static void Main()
        {
            Console.WriteLine(DateTime.Now.ToString("yyyyMMDD'T'HHmmss'Z'"));
            rest = new RestScaffolder("https://httpbin.org/get", RestScaffolder.Method.GET);

            String retVal = "";

            using (var client = new HttpClient())
            {
                HttpContent httpContent = new StringContent("foo");

                var response = client.PostAsync("https://httpbin.org/post", httpContent).Result;

                if (response.IsSuccessStatusCode)
                {
                    var responseContent = response.Content;

                    // by calling .Result you are synchronously reading the result
                    string responseString = responseContent.ReadAsStringAsync().Result;
                    retVal = responseString;
                    Console.WriteLine(responseString);
                }
            }

            //TestGet();
            //rest = new RestScaffolder("http://www.google.com", RestScaffolder.Method.GET);
            //Console.WriteLine("The URL is: " + rest.URL);
        }

        static void TestGet()
        {
            rest = new RestScaffolder("https://0tmgxdej5k.execute-api.us-east-1.amazonaws.com/beta/foo", RestScaffolder.Method.GET);
            rest.AddHeader("Authorization", "");
            rest.AddHeader("X-Amz-Date", "");
            string result = rest.ExecuteGet().Result;
            Console.WriteLine(result);
        }

        static void TestStringPost()
        {
            rest = new RestScaffolder("http://httpbin.org/post", RestScaffolder.Method.POST);
            rest.Body = "{'cat' : 'toffee'}";
            rest.ExecuteStringPost().Wait();
        }
        
    }
}
