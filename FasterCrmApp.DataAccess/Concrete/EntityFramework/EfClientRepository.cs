using FasterCrmApp.DataAccess.Abstract;
using FasterCrmApp.DataAccess.Context.EntityFramework.Context;
using FasterCrmApp.Entities.Concrete;

namespace FasterCrmApp.DataAccess.Concrete.EntityFramework.Base
{
    public class EfClientRepository :
                 EfBaseRepository<Client, DatabaseContext>,
                 IClientRepository
    {
        public EfClientRepository(DatabaseContext context) : base(context)
        { }
    }
}
