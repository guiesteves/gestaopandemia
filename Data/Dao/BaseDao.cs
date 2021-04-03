using CVC19.Models;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CVC19.Data.Dao
{
    public abstract class BaseDao<TEntidade> where TEntidade : Entidade
    { 
        protected readonly ApplicationDbContext _context;

        protected BaseDao(ApplicationDbContext context) 
        {
            _context = context;
        }

        public IDbContextTransaction ObterNovaTransacao() 
        {
            return _context.Database.BeginTransaction();
        }

        public void Incluir(TEntidade entidade)
        {
            _context.Add(entidade);
        }

        public void Atualizar(TEntidade entidade)
        {
            _context.Update(entidade);
        }

        public void Excluir(TEntidade entidade)
        {
            _context.Remove(entidade);
        }
    }
}
