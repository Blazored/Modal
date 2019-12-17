using RichardSzalay.MockHttp;
using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Text.Json;
using System.Net.Http.Headers;

namespace Microsoft.AspNetCore.Components.Testing
{
    public static class MockHttpExtensions
    {
        public static MockHttpMessageHandler AddMockHttp(this TestHost host)
        {
            var mockHttp = new MockHttpMessageHandler();
            var httpClient = mockHttp.ToHttpClient();
            httpClient.BaseAddress = new Uri("http://example.com");
            host.AddService(httpClient);
            return mockHttp;
        }

        public static TaskCompletionSource<object> Capture(this MockHttpMessageHandler handler, string url)
        {
            var tcs = new TaskCompletionSource<object>();

            handler.When(url).Respond(() =>
            {
                return tcs.Task.ContinueWith(task =>
                {
                    var response = new HttpResponseMessage(HttpStatusCode.OK);
                    response.Content = new StringContent(JsonSerializer.Serialize(task.Result));
                    response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                    return response;
                });
            });

            return tcs;
        }
    }
}
