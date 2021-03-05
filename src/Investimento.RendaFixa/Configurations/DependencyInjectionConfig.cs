using Investimento.RendaFixa.Domain.Interfaces.Services;
using Investimento.RendaFixa.Domain.Services;
using Investimento.RendaFixa.Infrastructure.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Investimento.RendaFixa.Configurations
{
    public static class DependencyInjectionConfig
    {
        public static IServiceCollection ResolveDependencies(this IServiceCollection services)
        {
            services.AddScoped<IRendaFixaService, RendaFixaService>();
            services.AddScoped<IB3RendaFixaService, B3RendaFixaService>();

            services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddHttpClient("mockinvestimento", options =>
            {
                options.DefaultRequestHeaders.Add("Accept", "application/json");
            })
            .AddPolicyHandler(PollyConfiguration.GetRetryPolicy());

            return services;
        }
    }
}
