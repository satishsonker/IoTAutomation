using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace IoT.BusinessLayer
{
   public class HttpClientWrapper
    {
        HttpClient httpClient;
        public HttpClientWrapper()
        {
            httpClient = new HttpClient();
        }

        public async Task<HttpResponseMessage> Post(string url,HttpContent httpContent)
        {
            HttpResponseMessage httpResponseMessage = await httpClient.PostAsync(url, httpContent);
            return httpResponseMessage;
        }
    }
}
