using System;
using System.IO;
using System.Net;

namespace mvault_api_app
{
    public class Class1
    {
        static void Main()
        {
            Console.WriteLine("Hello world");
            WebRequest request = WebRequest.Create("https://postman-echo.com/get?foo1=bar1&foo2=bar2");
            request.Credentials = CredentialCache.DefaultCredentials;
            WebResponse response = request.GetResponse();
            Console.WriteLine(((HttpWebResponse)response).StatusCode);
            using (Stream dataStream = response.GetResponseStream())
            {
                // Open the stream using a StreamReader for easy access.  
                StreamReader reader = new StreamReader(dataStream);
                // Read the content.  
                string responseFromServer = reader.ReadToEnd();
                // Display the content.  
                Console.WriteLine(responseFromServer);
            }

            // Close the response.  
            response.Close();
        }
    }
}
