using CashBack.Models;
using CashBack.Repositories;
using CashBack.Repositories.Interfaces;
using CashBack.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CashBack.Services
{
    public class CatalogoDiscoService : ICatalogoDiscoService
    {
        private ICatalogoDiscoRepository _catalogoRepository;

        public CatalogoDiscoService(ICatalogoDiscoRepository catalogo)
        {
            _catalogoRepository = catalogo;
        }
        public IEnumerable<Disco> ListarTodosPorGenero(string genero, int offset, int limit)
        {
            return _catalogoRepository.ListarTodosPorGenero(genero, offset, limit);
        }

        public Disco ObterDiscoPorID(int id)
        {
            return _catalogoRepository.ObterDiscoPorID(id);
        }
    }
}
