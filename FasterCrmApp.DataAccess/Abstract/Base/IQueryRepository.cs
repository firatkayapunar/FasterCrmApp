using FasterCrmApp.Entities.Abstract;

namespace FasterCrmApp.DataAccess.Abstract.Base
{
    public interface IQueryRepository<TEntity>
                     where TEntity : BaseEntity
    {
        TEntity GetById(int id);
        IEnumerable<TEntity> GetAll();
    }
}
