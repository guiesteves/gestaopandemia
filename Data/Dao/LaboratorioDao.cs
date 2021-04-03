using CVC19.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CVC19.Data.Dao
{
    public class LaboratorioDao : BaseDao<Laboratorio>
    {
        public LaboratorioDao(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<Laboratorio> RecuperarPorIdAsync(int id)
        {
            return await _context.Laboratorio.Include(p => p.Pais)
                .SingleOrDefaultAsync(a => a.LaboratorioId == id);
        }
        public async Task<List<Laboratorio>> RecuperarTodosAsync()
        {
            return await _context.Laboratorio.OrderBy(l => l.LaboratorioId).Include(p => p.Pais)
                .ToListAsync();
        }


        public bool ExistePorId(int id)
        {
            return _context.Laboratorio.Any(a => a.LaboratorioId == id);
        }

        public List<Tuple<string, int>> ObterQuantidadeLaboratoriosPorPais()
        {
            var listaQuantidade = _context.Laboratorio
                .GroupBy(l => new { l.PaisId, l.Pais.Nome })
                .Select(r => new Tuple<string, int>(r.Key.Nome, r.Count()));
            return listaQuantidade.ToList();
        }

    }
}
