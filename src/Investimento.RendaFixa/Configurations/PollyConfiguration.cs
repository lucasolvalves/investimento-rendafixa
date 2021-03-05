using KissLog;
using Polly;
using Polly.Extensions.Http;
using System;
using System.Net.Http;

namespace Investimento.RendaFixa.Configurations
{
    public static class PollyConfiguration
    {
        private static ILogger _logger = Logger.Factory.Get();

        public static IAsyncPolicy<HttpResponseMessage> GetRetryPolicy()
        {
            return HttpPolicyExtensions
                    .HandleTransientHttpError()
                    .OrResult(msg => !msg.IsSuccessStatusCode)
                    .WaitAndRetryAsync(3, retryAttempt => TimeSpan.FromSeconds(retryAttempt * 5),
                    onRetry: (exception, timeSpan, retryCount, context) =>
                    {
                        _logger.Warn($"Polly: Retentativa: {retryCount}");
                    });
        }
    }
}
