using Investimento.RendaFixa.Domain.Interfaces.Services;
using KissLog;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Investimento.RendaFixa.Controllers.V1
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/rendas_fixas")]
    public class RendasFixasController : MainController
    {
        private readonly IRendaFixaService _rendaFixaService;

        public RendasFixasController(ILogger logger, IRendaFixaService rendaFixaService) : base(logger)
        {
            _rendaFixaService = rendaFixaService;
        }

        [HttpGet("{accountId:long}")]
        public async Task<IActionResult> GetInvestments(long accountId)
        {
            var Investments = await _rendaFixaService.GetAllByAccountIdAsync(accountId);

            if (Investments?.Count == 0)
                return NotFound();

            return Ok(new { data = Investments });
        }
    }
}
