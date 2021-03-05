using Investimento.RendaFixa.Domain.Extensions;
using Investimento.RendaFixa.Domain.Interfaces.Services;
using Investimento.RendaFixa.Infrastructure.ViewModels;
using KissLog;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Investimento.RendaFixa.Infrastructure.Services
{
    public class B3RendaFixaService : IB3RendaFixaService
    {
        private readonly IHttpClientFactory _httpFactory;
        private readonly ILogger _logger;
        private readonly IConfiguration _configuration;

        public B3RendaFixaService(IHttpClientFactory httpFactory, ILogger logger, IConfiguration configuration)
        {
            _httpFactory = httpFactory;
            _logger = logger;
            _configuration = configuration;
        }

        public async Task<List<Domain.Entities.RendaFixa>> GetAllByAccountIdAsync(long accountId)
        {
            try
            {
                var listRendasFixas = new List<Domain.Entities.RendaFixa>();
                var jsonString = await ConsumeEndpoint(accountId);
                var items = jsonString.JsonGetByName("lcis");

                if (string.IsNullOrEmpty(items))
                    return listRendasFixas;

                var rendasFixasViewModel = JsonConvert.DeserializeObject<List<RendaFixaViewModel>>(items);
                rendasFixasViewModel?.ForEach(x => listRendasFixas.Add(new Domain.Entities.RendaFixa(x.CapitalInvestido, x.CapitalAtual, x.Quantidade, x.Vencimento, x.Iof, x.OutrasTaxas, x.Taxas, x.Indice, 
                                                                                                     x.Tipo, x.Nome, x.GuarantidoFGC, x.DataOperacao, x.PrecoUnitario, x.Primario)));
                return listRendasFixas;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private async Task<string> ConsumeEndpoint(long accountId)
        {
            try
            {
                using (HttpClient httpclient = _httpFactory.CreateClient("mockinvestimento"))
                using (HttpResponseMessage httpResponse = await httpclient.GetAsync(_configuration.GetSection("AppSettings:Mockyio:RequestUri").Value))
                {
                    var body = await httpResponse.Content?.ReadAsStringAsync();

                    _logger.Trace("RequestUrl: " + httpResponse.RequestMessage.RequestUri?.ToString() +
                                "\nMethod: " + httpResponse.RequestMessage.Method?.ToString() +
                                "\nResponseStatusCode: " + httpResponse?.StatusCode +
                                "\nResponseBody: " + body, "ConsomeEndpoint", 20);

                    return !string.IsNullOrWhiteSpace(body) ? body : null;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
