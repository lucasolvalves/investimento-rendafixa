using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Investimento.RendaFixa.Domain.Interfaces.Services
{
    public interface IRendaFixaService
    {
        Task<List<Entities.RendaFixa>> GetAllByAccountIdAsync(long accountId);
    }
}
