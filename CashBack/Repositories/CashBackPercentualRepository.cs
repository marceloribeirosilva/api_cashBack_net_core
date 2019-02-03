using CashBack.Context;
using CashBack.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CashBack.Repositories
{
    public class CashBackPercentualRepository : ICashBackPercentualRepository
    {
        private ApplicationDbContext _context;

        public CashBackPercentualRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public decimal ObterCashBack(string genero)
        {
            decimal valor = 0;
            int diaDaSemana = (int)DateTime.Now.DayOfWeek;
            if (!string.IsNullOrWhiteSpace(genero))
            {
                var cash = _context.CashBackPercentuais.Where(c => c.Genero.Equals(genero)).FirstOrDefault();
                
                if (cash != null)
                {
                    switch(diaDaSemana)
                    {                        
                        case 1:
                            valor = cash.Segunda;
                            break;
                        case 2:
                            valor = cash.Terca;
                            break;
                        case 3:
                            valor = cash.Quarta;
                            break;
                        case 4:
                            valor = cash.Quinta;
                            break;
                        case 5:
                            valor = cash.Sexta;
                            break;
                        case 6:
                            valor = cash.Sabado;
                            break;
                        case 7:
                            valor = cash.Domingo;
                            break;
                    }
                }
            }
            return valor;
        }

        public decimal ObterCashBack(string genero, DateTime dataVenda)
        {
            decimal valor = 0;
            int diaDaSemana = (int)dataVenda.DayOfWeek;
            if (!string.IsNullOrWhiteSpace(genero))
            {
                var cash = _context.CashBackPercentuais.Where(c => c.Genero.Equals(genero)).FirstOrDefault();

                if (cash != null)
                {
                    switch (diaDaSemana)
                    {
                        case 0:
                            valor = cash.Domingo;
                            break;
                        case 1:
                            valor = cash.Segunda;
                            break;
                        case 2:
                            valor = cash.Terca;
                            break;
                        case 3:
                            valor = cash.Quarta;
                            break;
                        case 4:
                            valor = cash.Quinta;
                            break;
                        case 5:
                            valor = cash.Sexta;
                            break;
                        case 6:
                            valor = cash.Sabado;
                            break;                        
                    }
                }
            }
            return valor;
        }
    }
}
