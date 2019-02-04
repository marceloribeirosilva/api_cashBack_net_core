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
        private ICashBackPercentualRepository _cashBackRepository;
        private ICatalogoDiscoRepository _catalogo;

        public VendaRepository(ApplicationDbContext contexto, ICashBackPercentualRepository cashBackRepository, ICatalogoDiscoRepository catalogo)
        {
            _context = contexto;
            _cashBackRepository = cashBackRepository;
            _catalogo = catalogo;
        }

        public Resultado IncluirVenda(Venda venda)
        {
            Resultado resultado = DadosValidos(venda);
            resultado.Acao = "Inclusão de Venda";

            if (resultado.Inconsistencias.Count == 0)
            {
                venda.DataVenda = DateTime.Now;

                _context.Vendas.Add(venda);
                
                

                foreach (var item in venda.Itens)
                {
                    item.VendaID = venda.VendaID;
                    
                    // Busca valor do CashBack
                    decimal valorPercentualCash = _cashBackRepository.ObterCashBack(item.Disco.Genero);
                    item.ValorCashBack = decimal.Round(item.Disco.PrecoVenda * valorPercentualCash, 2);
                    item.Disco = _catalogo.ObterDiscoPorID(item.Disco.ID);

                    if (item.Disco != null)
                    {
                        _context.ItensVendas.Add(item);
                    }                    
                }

                if (venda.Itens != null && venda.Itens.Any())
                {
                    venda.CashBackTotalVenda = venda.Itens.Sum(x => x.ValorCashBack);
                    venda.ValorTotalItens = venda.Itens.Sum(x => x.Disco.PrecoVenda);
                    _context.SaveChanges();
                }

                
            }


            return resultado;
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

        private Resultado DadosValidos(Venda venda)
        {
            Resultado resultado = new Resultado();
            if (venda == null)
            {
                resultado.Inconsistencias.Add("Dados de venda inválido");
            }
            else
            {
                if (venda.Itens == null || !venda.Itens.Any())
                {
                    resultado.Inconsistencias.Add("Não é permitido inserir uma venda sem nenhum disco selecionado");
                }
            }

            return resultado;
        }
    }
}
