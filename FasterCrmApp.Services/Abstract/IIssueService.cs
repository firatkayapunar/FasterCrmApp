using FasterCrmApp.Models;
using FasterCrmApp.Models.Results;

namespace FasterCrmApp.Services.Abstract
{
    public interface IIssueService
    {
        Result<IssueModel> GetById(int id);
        Result<List<IssueModel>> GetList();
        Result<List<IssueModel>> GetListByUserId(int userId);
        Result<List<IssueModel>> ListBySearch(string search);
        Result<List<IssueModel>> ListBySearch(string search, int userId);
        Result Create(CreateIssueModel createIssueModel);
        Result Edit(EditIssueModel editIssueModel);
        Result Delete(DeleteIssueModel deleteIssueModel);
    }
}
