using FasterCrmApp.Models;
using FasterCrmApp.Models.Results;

namespace FasterCrmApp.Services.Abstract
{
    public interface ILogService
    {
        Result<LogModel> GetById(int id);
        Result<List<LogModel>> GetList();
        Result<List<LogModel>> GetListByUserId(int userId);
        Result Create(CreateLogModel createLogModel);
        Result Edit(EditLogModel editLogModel);
        Result Delete(DeleteLogModel deleteLogModel);
    }
}
