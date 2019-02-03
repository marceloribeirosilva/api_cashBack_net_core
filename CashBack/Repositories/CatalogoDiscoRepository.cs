using CashBack.Context;
using CashBack.Models;
using CashBack.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CashBack.Repositories
{
    public class CatalogoDiscoRepository : ICatalogoDiscoRepository
    {
        private ApplicationDbContext _context;

        public CatalogoDiscoRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Disco> ListarTodosPorGenero(string genero, int offset, int limit)
        {
            return _context.CatalogoDiscos.Where(x => x.Genero.Equals(genero)).OrderBy(o => o.Nome).Skip(offset).Take(limit).ToList();
        }

        public Disco ObterDiscoPorID(int id)
        {
            return _context.CatalogoDiscos.Where(x => x.ID.Equals(id)).FirstOrDefault();
        }
    }
}
