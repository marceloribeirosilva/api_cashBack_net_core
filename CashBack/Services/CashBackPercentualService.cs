using CashBack.Repositories.Interfaces;
using CashBack.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CashBack.Services
{
    public class CashBackPercentualService : ICashBackPercentualService
    {
        private readonly ICashBackPercentualRepository _cashBackPercentualRepository;

        public CashBackPercentualService(ICashBackPercentualRepository cashBackPercentualRepository)
        {
            _cashBackPercentualRepository = cashBackPercentualRepository;
        }

        public decimal ObterCashBack(string genero)
        {
            return _cashBackPercentualRepository.ObterCashBack(genero);
        }
    }
}
