using FasterCrmApp.Entities.Concrete;

namespace FasterCrmApp.DataAccess.Abstract.Base
{
    public interface IRepository<TEntity> :
                     IQueryRepository<TEntity>,
                     ICommandRepository<TEntity>
                     where TEntity : BaseEntity
    { }
}
