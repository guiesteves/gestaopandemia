using CVC19.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CVC19.Data.Dao
{
    public class PaisDao : BaseDao<Pais>
    {
        public PaisDao(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<Pais> RecuperarPorIdAsync(int id)
        {
            return await _context.Pais.SingleOrDefaultAsync(a => a.PaisId == id);
        }

        public Pais RecuperarPorId(int id)
        {
            return _context.Pais.SingleOrDefault(a => a.PaisId == id);
        }

        public async Task<List<Pais>> RecuperarTodosAsync()
        {
            return await _context.Pais.OrderBy(p => p.Nome).ToListAsync();
        }

        public bool ExistePorId(int id)
        {
            return _context.Pais.Any(a => a.PaisId == id);
        }
    }
}
