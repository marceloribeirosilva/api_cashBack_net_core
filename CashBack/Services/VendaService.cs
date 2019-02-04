using CashBack.Models;
using CashBack.Repositories.Interfaces;
using CashBack.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CashBack.Services
{
    public class VendaService : IVendaService
    {
        private IVendaRepository _vendaRepository;

        public VendaService(IVendaRepository vendaRepository)
        {
            _vendaRepository = vendaRepository;
        }

        public Resultado IncluirVenda(Venda venda)
        {
            return _vendaRepository.IncluirVenda(venda);
        }

        public IEnumerable<Venda> ObterTodasVendas(DateTime dataInicial, DateTime dataFinal, int offset, int limit)
        {
            return _vendaRepository.ObterTodasVendas(dataInicial, dataFinal, offset, limit);
        }

        public Venda ObterVendaPorId(int id)
        {
            return _vendaRepository.ObterVendaPorId(id);
        }        
    }
}
