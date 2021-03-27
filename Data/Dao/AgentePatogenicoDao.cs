using CVC19.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CVC19.Data.Dao
{
    public class AgentePatogenicoDao
    {
        private readonly ApplicationDbContext _context;

        public AgentePatogenicoDao(ApplicationDbContext context)
        {
            _context = context;
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




        public void Incluir(AgentePatogenico agentePatogenico)
        {
            _context.AgentePatogenico.Add(agentePatogenico);
            _context.SaveChanges();
        }

        public void Atualizar(AgentePatogenico agentePatogenico)
        {
            _context.AgentePatogenico.Update(agentePatogenico);
            _context.SaveChanges();
        }

        public void Excluir(AgentePatogenico agentePatogenico)
        {
            _context.AgentePatogenico.Remove(agentePatogenico);
            _context.SaveChanges();
        }

    }
}
