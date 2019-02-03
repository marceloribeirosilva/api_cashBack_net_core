using CashBack.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CashBack.Services.Interfaces
{
    public interface ICatalogoDiscoService
    {
        IEnumerable<Disco> ListarTodosPorGenero(string genero, int offset, int limit);
        Disco ObterDiscoPorID(int id);
    }
}
