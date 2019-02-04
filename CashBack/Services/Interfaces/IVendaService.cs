using CashBack.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CashBack.Services.Interfaces
{
    public interface IVendaService
    {
        Venda ObterVendaPorId(int id);
        IEnumerable<Venda> ObterTodasVendas(DateTime dataInicial, DateTime dataFinal, int offset, int limit);
        Resultado IncluirVenda(Venda venda);
    }
}
