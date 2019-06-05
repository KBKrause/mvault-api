using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;

namespace mvault_api_app
{
    public class RestScaffolder
    {
        public enum Method { GET, POST, PUT, PATCH, DELETE };
        public String URL { get; set; }
        public Method HttpMethod { get; set; }

        public RestScaffolder(string URL, Method method)
        {
            this.URL = URL;
            this.HttpMethod = method;
        }

        public void Execute()
        {
            if (HttpMethod == Method.GET)
            {
                WebRequest request = WebRequest.Create(this.URL);
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
}
