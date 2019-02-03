using CashBack.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CashBack.Services.Interfaces
{
    public interface ICashBackPercentualService
    {
        decimal ObterCashBack(string genero);
    }
}
