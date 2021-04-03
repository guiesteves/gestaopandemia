using CVC19.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CVC19.Data.Dao
{
    public class AgentePatogenicoDao : BaseDao<AgentePatogenico>
    {
        public AgentePatogenicoDao(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<AgentePatogenico> RecuperarPorIdAsync(int id)
        {
            return await _context.AgentePatogenico.Include(a => a.TipoAgentePatogenico)
                                                  .Include(a => a.ListaVarianteAgentePatogenico)
                                                  .ThenInclude(v => v.Pais)
                                                  .SingleOrDefaultAsync(a => a.AgentePatogenicoId == id);
        }
        public async Task<List<AgentePatogenico>> RecuperarTodosAsync()
        {
            return await _context.AgentePatogenico.OrderBy(a => a.AgentePatogenicoId).Include(a => a.TipoAgentePatogenico)
                                                  .ToListAsync();
        }


        public bool ExistePorId(int id)
        {
            return _context.AgentePatogenico.Any(a => a.AgentePatogenicoId == id);
        }
    }
}
