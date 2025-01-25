using FasterCrmApp.Entities.Abstract;

namespace FasterCrmApp.DataAccess.Abstract.Base
{
    public interface IRepository<TEntity> :
                     IQueryRepository<TEntity>,
                     ICommandRepository<TEntity>
                     where TEntity : EntityBase
    { }
}
