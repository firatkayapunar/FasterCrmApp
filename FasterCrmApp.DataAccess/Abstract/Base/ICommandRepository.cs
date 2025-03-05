using FasterCrmApp.Entities.Abstract;

namespace FasterCrmApp.DataAccess.Abstract.Base
{
    public interface ICommandRepository<TEntity>
                     where TEntity : EntityBase
    {
        void Add(TEntity entity);
        void Update(TEntity entity);
        void Remove(int id);
    }
}
