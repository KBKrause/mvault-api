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
            
            rest = new RestScaffolder("https://postman-echo.com/get?foo1=bar1&foo2=bar2", RestScaffolder.Method.GET);

            string result = rest.Get().Result;
            Console.WriteLine(result);
            /*
            // Test GET
            rest.Execute();

            /* Test POST
            rest.HttpMethod = RestScaffolder.Method.POST;
            rest.URL = "http://httpbin.org/post";
            */

        }

        
    }
}
