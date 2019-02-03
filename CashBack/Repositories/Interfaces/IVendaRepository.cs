using CashBack.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CashBack.Repositories.Interfaces
{
    public interface IVendaRepository
    {
        Venda ObterVendaPorId(int id);
        IEnumerable<Venda> ObterTodasVendas(DateTime dataInicial, DateTime dataFinal, int offset, int limit); 
    }
}
