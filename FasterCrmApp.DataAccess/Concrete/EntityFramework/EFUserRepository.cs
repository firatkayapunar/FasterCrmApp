using FasterCrmApp.DataAccess.Abstract;
using FasterCrmApp.DataAccess.Context.EntityFramework.Context;
using FasterCrmApp.Entities.Concrete;

namespace FasterCrmApp.DataAccess.Concrete.EntityFramework.Base
{
    public class EFUserRepository :
                 EfBaseRepository<User, DatabaseContext>,
                 IUserRepository
    {
        private readonly DatabaseContext _context;

        public EFUserRepository(DatabaseContext context) : base(context)
        {
            _context = context;
        }
    }
}
