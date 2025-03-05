using FasterCrmApp.DataAccess.Abstract.Base;
using FasterCrmApp.Entities.Concrete;

namespace FasterCrmApp.DataAccess.Abstract
{
    public interface ILeadRepository :
                     IRepository<Lead>
    { }
}
