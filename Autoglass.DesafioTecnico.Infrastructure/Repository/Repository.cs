using Autoglass.DesafioTecnico.Domain.Core.Repository;
using Autoglass.DesafioTecnico.Domain.Model;
using Autoglass.DesafioTecnico.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Autoglass.DesafioTecnico.Infrastructure.Repository
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : BaseEntity
    {
        public readonly MainContext _dbContext;

        public Repository(MainContext context)
        {
            _dbContext = context;
        }

        public bool Delete(int codigo)
        {
            var fromDb = _dbContext.Set<TEntity>().FirstOrDefault(x => x.Codigo == codigo);

            if (fromDb != null)
            {
                fromDb.Situacao = false;
                _dbContext.SaveChanges();

                return true;
            }

            return false;
        }

        public IQueryable<TEntity> GetAll() =>
            _dbContext.Set<TEntity>().AsNoTracking().Where(x => x.Situacao == true);

        public TEntity GetById(int codigo) =>
            _dbContext.Set<TEntity>().AsNoTracking().FirstOrDefault(x => x.Codigo == codigo && x.Situacao == true);

        public void Patch(TEntity entity)
        {
            _dbContext.Update(entity);

            _dbContext.SaveChanges();
        }

        public int Post(TEntity entity)
        {
            var dbEntity = _dbContext.Set<TEntity>().Add(entity);
            _dbContext.SaveChanges();

            return dbEntity.Entity.Codigo;
        }
    }
}
