using CVC19.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CVC19.Data.Dao
{
    public class TipoAgentePatogenicoDao
    {
        private readonly ApplicationDbContext _context;

        public TipoAgentePatogenicoDao(ApplicationDbContext context)
        {
            _context = context;
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




        public void Incluir(TipoAgentePatogenico tipoAgentePatogenico)
        {
            using var transaction = _context.Database.BeginTransaction();

            try
            {
                _context.TipoAgentePatogenico.Add(tipoAgentePatogenico);


                _context.SaveChanges();

                transaction.Commit();
            }
            catch (Exception)
            {
                throw;
            }

        }

        public void Atualizar(TipoAgentePatogenico tipoAgentePatogenico)
        {
            using var transaction = _context.Database.BeginTransaction();

            try
            {
                _context.TipoAgentePatogenico.Update(tipoAgentePatogenico);
                _context.SaveChanges();

                transaction.Commit();
            }
            catch (Exception)
            {
                throw;
            }

        }

        public void Excluir(TipoAgentePatogenico tipoAgentePatogenico)
        {
            using var transaction = _context.Database.BeginTransaction();

            try
            {
                _context.TipoAgentePatogenico.Remove(tipoAgentePatogenico);
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
