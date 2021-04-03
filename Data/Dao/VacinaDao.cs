using CVC19.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CVC19.Data.Dao
{
    public class VacinaDao : BaseDao<Vacina>
    {
        public VacinaDao(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<Vacina> RecuperarPorIdAsync(int id)
        {
            return await _context.Vacina.Include(v => v.AgentePatogenico)
                .Include(v => v.Laboratorio)
                .Include(v => v.TipoVacina)
                .SingleOrDefaultAsync(v => v.VacinaId == id);
        }
        public async Task<List<Vacina>> RecuperarTodosAsync()
        {
            return await _context.Vacina.Include(v => v.AgentePatogenico)
                .Include(v => v.Laboratorio)
                .Include(v => v.TipoVacina)
                .OrderBy(v => v.VacinaId).ToListAsync();
        }

        public bool ExistePorId(int id)
        {
            return _context.Vacina.Any(a => a.VacinaId == id);
        }

        public bool ExistePorTipoVacinaId(int tipoVacinaId)
        {
            return _context.Vacina.Any(a => a.TipoVacinaId == tipoVacinaId);
        }


        public bool ExistePorLaboratorioId(int laboratorioId)
        {
            return _context.Vacina.Any(a => a.LaboratorioId == laboratorioId);
        }

        public bool ExistePorAgentePatogenicoId(int AgentePatogenicoId)
        {
            return _context.Vacina.Any(a => a.AgentePatogenicoId == AgentePatogenicoId);
        }

        public List<Tuple<string, int>> ObterQuantidadeVacinaPorTipoVacina()
        {
            var listaQuantidade = _context.Vacina
                .GroupBy(v => new { v.TipoVacinaId, v.TipoVacina.Nome })
                .Select(r => new Tuple<string, int>(r.Key.Nome, r.Count()));
            return listaQuantidade.ToList();
        }

        public List<Tuple<string, int>> ObterQuantidadeVacinaPorLaboratorio()
        {
            var listaQuantidade = _context.Vacina
                .GroupBy(v => new { v.LaboratorioId, v.Laboratorio.Nome })
                .Select(r => new Tuple<string, int>(r.Key.Nome, r.Count()));
            return listaQuantidade.ToList();
        }
    }
}
