using Investimento.RendaFixa.Infrastructure.Services;
using Investimento.RendaFixa.Test.Builders;
using KissLog;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Investimento.RendaFixa.Test.Services
{
    [TestClass]
    public class B3RendaFixaServiceTest
    {
        private Mock<IHttpClientFactory> _httpFactory;
        private Mock<ILogger> _logger;
        private Mock<IConfigurationSection> _configurationSection;
        private const string JsonSucess = "{\r\n\"lcis\": [{\r\n\t\t\t\"capitalInvestido\": 2000.0,\r\n\t\t\t\"capitalAtual\": 2097.85,\r\n\t\t\t\"quantidade\": 2.0,\r\n\t\t\t\"vencimento\": \"2021-03-09T00:00:00\",\r\n\t\t\t\"iof\": 0.0,\r\n\t\t\t\"outrasTaxas\": 0.0,\r\n\t\t\t\"taxas\": 0.0,\r\n\t\t\t\"indice\": \"97% do CDI\",\r\n\t\t\t\"tipo\": \"LCI\",\r\n\t\t\t\"nome\": \"BANCO MAXIMA\",\r\n\t\t\t\"guarantidoFGC\": true,\r\n\t\t\t\"dataOperacao\": \"2019-03-14T00:00:00\",\r\n\t\t\t\"precoUnitario\": 1048.927450,\r\n\t\t\t\"primario\": false\r\n\t\t},\r\n\t\t{\r\n\t\t\t\"capitalInvestido\": 5000.0,\r\n\t\t\t\"capitalAtual\": 5509.76,\r\n\t\t\t\"quantidade\": 1.0,\r\n\t\t\t\"vencimento\": \"2021-03-09T00:00:00\",\r\n\t\t\t\"iof\": 0.0,\r\n\t\t\t\"outrasTaxas\": 0.0,\r\n\t\t\t\"taxas\": 0.0,\r\n\t\t\t\"indice\": \"97% do CDI\",\r\n\t\t\t\"tipo\": \"LCI\",\r\n\t\t\t\"nome\": \"BANCO BARI\",\r\n\t\t\t\"guarantidoFGC\": true,\r\n\t\t\t\"dataOperacao\": \"2019-03-14T00:00:00\",\r\n\t\t\t\"precoUnitario\": 2754.88,\r\n\t\t\t\"primario\": false\r\n\t\t}\r\n\t]\r\n}";

        [TestInitialize]
        public void Initialize()
        {
            _httpFactory = new Mock<IHttpClientFactory>();
            _logger = new Mock<ILogger>();
            _configurationSection = new Mock<IConfigurationSection>();
        }

        [TestMethod]
        public async Task ShouldReturnRendaFixaWhenB3ServiceIsUp()
        {
            _configurationSection.Setup(x => x.GetSection(It.IsAny<string>())).Returns(CreateFakeConfigurationSection());

            var client = new HttpClient(HttpMessageHandlerBuilder.Create(HttpStatusCode.OK, JsonSucess, HttpMethod.Get, "http://test.com.br/unitest"))
            {
                BaseAddress = new Uri("http://test.com.br/")
            };

            _httpFactory.Setup(_ => _.CreateClient(It.IsAny<string>())).Returns(client);

            var service = new B3RendaFixaService(_httpFactory.Object, _logger.Object, _configurationSection.Object);
            var result = await service.GetAllByAccountIdAsync(1234567899);

            Assert.IsTrue(result.Count == 2);
        }

        [TestMethod]
        public async Task ShouldNotReturnRendaFixaWhenB3ServiceReturnUnsuccessfully()
        {
            _configurationSection.Setup(x => x.GetSection(It.IsAny<string>())).Returns(CreateFakeConfigurationSection());

            var client = new HttpClient(HttpMessageHandlerBuilder.Create(HttpStatusCode.InternalServerError, "", HttpMethod.Get, "http://test.com.br/unitest"))
            {
                BaseAddress = new Uri("http://test.com.br/")
            };

            _httpFactory.Setup(_ => _.CreateClient(It.IsAny<string>())).Returns(client);

            var service = new B3RendaFixaService(_httpFactory.Object, _logger.Object, _configurationSection.Object);
            var result = await service.GetAllByAccountIdAsync(1234567899);

            Assert.IsTrue(result.Count == 0);
        }

        private IConfigurationSection CreateFakeConfigurationSection()
        {
            var configurationSection = new Mock<IConfigurationSection>();
            configurationSection.Setup(x => x.Path).Returns("AppSettings:Mockyio:RequestUri");
            configurationSection.Setup(x => x.Key).Returns("RequestUri");
            configurationSection.Setup(x => x.Value).Returns("http://test.com.br/configurationSection");
            return configurationSection.Object;
        }
    }
}
