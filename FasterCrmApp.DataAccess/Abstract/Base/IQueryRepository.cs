using FasterCrmApp.Entities.Abstract;
using System.Linq.Expressions;

namespace FasterCrmApp.DataAccess.Abstract.Base
{
    public interface IQueryRepository<TEntity>
                     where TEntity : EntityBase
    {
        TEntity GetById(int id);
        IEnumerable<TEntity> GetAll();
        IEnumerable<TEntity> GetAll(Expression<Func<TEntity, bool>> predicate);
    }
}
