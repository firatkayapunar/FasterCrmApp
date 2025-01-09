using FasterCrmApp.DataAccess.Abstract;
using FasterCrmApp.DataAccess.Context.EntityFramework.Context;
using FasterCrmApp.Entities;

namespace FasterCrmApp.DataAccess.Concrete.EntityFramework.Base
{
    public class EfClientRepository :
                 EfBaseRepository<Client, DatabaseContext>,
                 IClientRepository
    {
        private readonly DatabaseContext _context;

        public EfClientRepository(DatabaseContext context) : base(context)
        {
            _context = context;
        }
    }
}
