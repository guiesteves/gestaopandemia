using CVC19.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CVC19.Data.Dao
{
    public class TipoVacinaDao : BaseDao<TipoVacina>
    {
        public TipoVacinaDao(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<TipoVacina> RecuperarPorIdAsync(int id)
        {
            return await _context.TipoVacina.SingleOrDefaultAsync(a => a.TipoVacinaId == id);
        }
        public async Task<List<TipoVacina>> RecuperarTodosAsync()
        {
            return await _context.TipoVacina.OrderBy(t => t.TipoVacinaId).ToListAsync();
        }

        public bool ExistePorId(int id)
        {
            return _context.TipoVacina.Any(a => a.TipoVacinaId == id);
        }
    }
}
