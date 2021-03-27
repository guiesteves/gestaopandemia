using CVC19.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CVC19.Data.Dao
{
    public class TipoVacinaDao
    {
        private readonly ApplicationDbContext _context;

        public TipoVacinaDao(ApplicationDbContext context)
        {
            _context = context;
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




        public void Incluir(TipoVacina TipoVacina)
        {
            using var transaction = _context.Database.BeginTransaction();

            try
            {
                _context.TipoVacina.Add(TipoVacina);


                _context.SaveChanges();

                transaction.Commit();
            }
            catch (Exception)
            {
                throw;
            }

        }

        public void Atualizar(TipoVacina tipoVacina)
        {
            using var transaction = _context.Database.BeginTransaction();

            try
            {
                _context.TipoVacina.Update(tipoVacina);
                _context.SaveChanges();

                transaction.Commit();
            }
            catch (Exception)
            {
                throw;
            }

        }

        public void Excluir(TipoVacina tipoVacina)
        {
            using var transaction = _context.Database.BeginTransaction();

            try
            {
                _context.TipoVacina.Remove(tipoVacina);
                _context.SaveChanges();

                transaction.Commit();
            }
            catch (Exception)
            {
                throw;
            }

        }

    }
}
