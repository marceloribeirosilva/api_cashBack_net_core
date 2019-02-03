using CashBack.Context;
using CashBack.Models;
using CashBack.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CashBack.Repositories
{
    public class VendaRepository : IVendaRepository
    {
        private ApplicationDbContext _context;

        public VendaRepository(ApplicationDbContext contexto)
        {
            _context = contexto;
        }

        public IEnumerable<Venda> ObterTodasVendas(DateTime dataInicial, DateTime dataFinal, int offset, int limit)
        {
            IEnumerable<Venda> vendas = _context.Vendas
                .Where(x=>x.DataVenda >= dataInicial && x.DataVenda <= dataFinal)
                .Skip(offset)
                .Take(limit)
                .OrderByDescending(x=>x.DataVenda).ToList();                        
            return vendas;
        }

        public Venda ObterVendaPorId(int id)
        {
            return _context.Vendas.Where(x => x.VendaID.Equals(id)).FirstOrDefault();
        }
    }
}
