using FasterCrmApp.DataAccess.Abstract;
using FasterCrmApp.DataAccess.Context.EntityFramework.Context;
using FasterCrmApp.Entities;

namespace FasterCrmApp.DataAccess.Concrete.EntityFramework.Base
{
    public class EfNotificationRepository :
                 EfBaseRepository<Notification, DatabaseContext>,
                 INotificationRepository
    {
        public EfNotificationRepository(DatabaseContext context) : base(context)
        { }
    }
}
