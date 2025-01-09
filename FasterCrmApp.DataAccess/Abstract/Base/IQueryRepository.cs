using FasterCrmApp.Entities.Concrete;

namespace FasterCrmApp.DataAccess.Abstract.Base
{
    public interface IQueryRepository<TEntity>
                     where TEntity : BaseEntity
    {
        IEnumerable<TEntity> GetAll();
        TEntity GetById(int id);
    }
}
