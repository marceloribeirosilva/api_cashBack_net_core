using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CashBack.Repositories.Interfaces
{
    public interface ICashBackPercentualRepository
    {
        decimal ObterCashBack(string genero);
        decimal ObterCashBack(string genero, DateTime dataVenda);
    }
}
