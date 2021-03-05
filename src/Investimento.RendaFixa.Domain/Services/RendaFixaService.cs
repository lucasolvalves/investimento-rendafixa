using Investimento.RendaFixa.Domain.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Investimento.RendaFixa.Domain.Services
{
    public class RendaFixaService : IRendaFixaService
    {
        private readonly IB3RendaFixaService _b3RendaFixaService;

        public RendaFixaService(IB3RendaFixaService b3RendaFixaService)
        {
            _b3RendaFixaService = b3RendaFixaService;
        }

        public async Task<List<Entities.RendaFixa>> GetAllByAccountIdAsync(long accountId)
        {
            return await _b3RendaFixaService.GetAllByAccountIdAsync(accountId);
        }
    }
}
