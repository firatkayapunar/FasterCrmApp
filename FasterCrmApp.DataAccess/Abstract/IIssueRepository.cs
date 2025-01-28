using FasterCrmApp.DataAccess.Abstract.Base;
using FasterCrmApp.Entities.Concrete;

namespace FasterCrmApp.DataAccess.Abstract
{
    public interface IIssueRepository :
                     IRepository<Issue>
    { }
}
