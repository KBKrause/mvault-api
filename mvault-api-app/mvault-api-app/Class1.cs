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
            rest = new RestScaffolder("http://www.google.com", RestScaffolder.Method.GET);
            Console.WriteLine("The URL is: " + rest.URL);
        }

        static void TestGet()
        {
            rest = new RestScaffolder("https://postman-echo.com/get?foo1=bar1&foo2=bar2", RestScaffolder.Method.GET);
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
