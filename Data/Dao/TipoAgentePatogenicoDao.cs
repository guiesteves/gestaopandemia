using CVC19.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CVC19.Data.Dao
{
    public class TipoAgentePatogenicoDao : BaseDao<TipoAgentePatogenico>
    {
        public TipoAgentePatogenicoDao(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<TipoAgentePatogenico> RecuperarPorIdAsync(int id)
        {
            return await _context.TipoAgentePatogenico.SingleOrDefaultAsync(a => a.TipoAgentePatogenicoId == id);
        }
        public async Task<List<TipoAgentePatogenico>> RecuperarTodosAsync()
        {
            return await _context.TipoAgentePatogenico.OrderBy(t => t.TipoAgentePatogenicoId).ToListAsync();
        }


        public bool ExistePorId(int id)
        {
            return _context.TipoAgentePatogenico.Any(a => a.TipoAgentePatogenicoId == id);
        }
    }
}
