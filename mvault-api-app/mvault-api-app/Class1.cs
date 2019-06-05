using System;
using System.IO;
using System.Net;

namespace mvault_api_app
{
    public class Class1
    {
        static void Main()
        {
            RestScaffolder rest = new RestScaffolder("https://postman-echo.com/get?foo1=bar1&foo2=bar2", RestScaffolder.Method.GET);

            rest.Execute();
        }
    }
}
