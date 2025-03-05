using FasterCrmApp.DataAccess.Abstract;
using FasterCrmApp.DataAccess.Context.EntityFramework.Context;
using FasterCrmApp.Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace FasterCrmApp.DataAccess.Concrete.EntityFramework.Base
{
    public class EfLogRepository :
                 EfBaseRepository<Log, DatabaseContext>,
                 ILogRepository
    {
        public EfLogRepository(DatabaseContext context) : base(context)
        { }

        public override Log GetById(int id)
        {
            return _entity.Include(x => x.User).SingleOrDefault(x => x.ID == id);
        }

        public override IEnumerable<Log> GetAll()
        {
            return _entity.Include(x => x.User).ToList();
        }

        public override IEnumerable<Log> GetAll(Expression<Func<Log, bool>> predicate)
        {
            return _entity.Include(x => x.User).Where(predicate).ToList();
        }
    }
}
