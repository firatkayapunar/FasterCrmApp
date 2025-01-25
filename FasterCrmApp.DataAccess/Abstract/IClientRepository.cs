using FasterCrmApp.DataAccess.Abstract.Base;
using FasterCrmApp.Entities.Concrete;
using System.Linq.Expressions;

namespace FasterCrmApp.DataAccess.Abstract
{
    public interface IClientRepository :
                     IRepository<Client>
    { }
}
