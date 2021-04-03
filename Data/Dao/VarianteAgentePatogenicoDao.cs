using CVC19.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CVC19.Data.Dao
{
    public class VarianteAgentePatogenicoDao : BaseDao<VarianteAgentePatogenico>
    {
        public VarianteAgentePatogenicoDao(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<VarianteAgentePatogenico> RecuperarPorIdAsync(int id)
        {
            return await _context.VarianteAgentePatogenico.Include(v => v.Pais).SingleOrDefaultAsync(v => v.VarianteAgentePatogenicoId == id);
        }
        public async Task<List<VarianteAgentePatogenico>> RecuperarTodosAsync()
        {
            return await _context.VarianteAgentePatogenico.OrderBy(v => v.VarianteAgentePatogenicoId).Include(v => v.Pais)
                                                  .ToListAsync();
        }

        public void ExcluirPorAgentePatogenicoNaoExistentesLista(int agentePatogenicoId, List<int> listaIdVarianteAgentePatogenico)
        {
            var listaExclusao = _context.VarianteAgentePatogenico
                      .Where(vp => vp.AgentePatogenicoId == agentePatogenicoId && !listaIdVarianteAgentePatogenico.Any( i => i == vp.VarianteAgentePatogenicoId));

            if (listaExclusao.Any()) 
            {
                _context.VarianteAgentePatogenico.RemoveRange(listaExclusao);
            }
        }

        public bool ExistePorId(int id)
        {
            return _context.VarianteAgentePatogenico.Any(v => v.VarianteAgentePatogenicoId == id);
        }

        public List<Tuple<string, int>> ObterQuantidadeVariantesPorAgentePatogenico()
        {
            var listaQuantidade = _context.VarianteAgentePatogenico
                .GroupBy(v => new { v.AgentePatogenicoId, v.AgentePatogenico.Nome })
                .Select(r => new Tuple<string, int>(r.Key.Nome, r.Count()));
            return listaQuantidade.ToList();
        }

        public List<Tuple<string, int>> ObterQuantidadeVariantesPorPais()
        {
            var listaQuantidade = _context.VarianteAgentePatogenico
                .GroupBy(v => new { v.PaisId, v.Pais.Nome })
                .Select(r => new Tuple<string, int>(r.Key.Nome, r.Count()));
            return listaQuantidade.ToList();
        }
    }
}
