using FasterCrmApp.DataAccess.Abstract;
using FasterCrmApp.DataAccess.Context.EntityFramework.Context;
using FasterCrmApp.Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace FasterCrmApp.DataAccess.Concrete.EntityFramework.Base
{
    public class EfLeadRepository :
                 EfBaseRepository<Lead, DatabaseContext>,
                 ILeadRepository
    {
        public EfLeadRepository(DatabaseContext context) : base(context)
        { }

        public override Lead GetById(int id)
        {
            return _entity.Include(x => x.User).Include(x => x.Client).SingleOrDefault(x => x.ID == id);
        }

        public override IEnumerable<Lead> GetAll()
        {
            return _entity.Include(x => x.User).Include(x => x.Client).ToList();
        }

        public override IEnumerable<Lead> GetAll(Expression<Func<Lead, bool>> predicate)
        {
            return _entity.Include(x => x.User).Include(x => x.Client).Where(predicate).ToList();
        }
    }
}
