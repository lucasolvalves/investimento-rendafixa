using KissLog;
using KissLog.AspNetCore;
using KissLog.CloudListeners.Auth;
using KissLog.CloudListeners.RequestLogsListener;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Investimento.RendaFixa.Configurations
{
    public static class LoggerConfig
    {
        public static IServiceCollection AddLoggingConfig(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<ILogger>((context) =>
            {
                return Logger.Factory.Get();
            });

            return services;
        }

        public static IApplicationBuilder UseLoggingConfiguration(this IApplicationBuilder app, IConfiguration configuration)
        {
            app.UseKissLogMiddleware(options =>
            {
                options.Listeners.Add(new RequestLogsApiListener(new Application(
                    configuration.GetSection("AppSettings:KissLog:OrganizationId")?.Value,
                    configuration.GetSection("AppSettings:KissLog:ApplicationId")?.Value)
                )
                {
                    ApiUrl = "https://api.kisslog.net"
                });
            });

            return app;
        }
    }
}
