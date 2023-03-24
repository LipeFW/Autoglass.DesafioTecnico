using Autoglass.DesafioTecnico.Domain.Model;
using System.Linq;

namespace Autoglass.DesafioTecnico.Domain.Core.Repository
{
    public interface IRepository<TEntity>
        where TEntity : BaseEntity
    {
        IQueryable<TEntity> GetAll();
        TEntity GetById(int codigo);
        int Post(TEntity entity);
        void Patch(TEntity entity);
        void Delete(int codigo);

    }
}
