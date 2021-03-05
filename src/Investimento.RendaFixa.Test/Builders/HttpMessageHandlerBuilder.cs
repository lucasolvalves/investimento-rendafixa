using Moq;
using Moq.Protected;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Investimento.RendaFixa.Test.Builders
{
    public static class HttpMessageHandlerBuilder
    {
        public static HttpMessageHandler Create(HttpStatusCode statusCode, string json, HttpMethod method, string uri)
        {
            var httpMessageHandler = new Mock<HttpMessageHandler>();
            httpMessageHandler.Protected()
            .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(new HttpResponseMessage
            {
                StatusCode = statusCode,
                Content = new StringContent(json),
                RequestMessage = new HttpRequestMessage()
                {
                    Method = method,
                    RequestUri = new Uri(uri)
                }
            });

            return httpMessageHandler.Object;
        }
    }
}
