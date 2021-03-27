using CVC19.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CVC19.Data.Dao
{
    public class LaboratorioDao
    {
        private readonly ApplicationDbContext _context;

        public LaboratorioDao(ApplicationDbContext context)
        {
            _context = context;
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

        public void Incluir(Laboratorio Laboratorio)
        {
            using var transaction = _context.Database.BeginTransaction();

            try
            {
                _context.Laboratorio.Add(Laboratorio);


                _context.SaveChanges();

                transaction.Commit();
            }
            catch (Exception)
            {
                throw;
            }

        }

        public void Atualizar(Laboratorio Laboratorio)
        {
            using var transaction = _context.Database.BeginTransaction();

            try
            {
                _context.Laboratorio.Update(Laboratorio);
                _context.SaveChanges();

                transaction.Commit();
            }
            catch (Exception)
            {
                throw;
            }

        }

        public void Excluir(Laboratorio Laboratorio)
        {
            using var transaction = _context.Database.BeginTransaction();

            try
            {
                _context.Laboratorio.Remove(Laboratorio);
                _context.SaveChanges();

                transaction.Commit();
            }
            catch (Exception)
            {
                throw;
            }

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
