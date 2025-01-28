using FasterCrmApp.DataAccess.Abstract;
using FasterCrmApp.DataAccess.Context.EntityFramework.Context;
using FasterCrmApp.Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace FasterCrmApp.DataAccess.Concrete.EntityFramework.Base
{
    public class EfIssueRepository :
                 EfBaseRepository<Issue, DatabaseContext>,
                 IIssueRepository
    {
        public EfIssueRepository(DatabaseContext context) : base(context)
        { }

        public override Issue GetById(int id)
        {
            return _entity.Include(x => x.User).SingleOrDefault(x => x.ID == id);
        }

        public override IEnumerable<Issue> GetAll()
        {
            return _entity.Include(x => x.User).ToList();
        }

        public override IEnumerable<Issue> GetAll(Expression<Func<Issue, bool>> predicate)
        {
            return _entity.Include(x => x.User).Where(predicate).ToList();
        }
    }
}
