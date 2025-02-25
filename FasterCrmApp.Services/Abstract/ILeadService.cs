using FasterCrmApp.Models;
using FasterCrmApp.Models.Results;

namespace FasterCrmApp.Services.Abstract
{
    public interface ILeadService
    {
        Result<LeadModel> GetById(int id);
        Result<List<LeadModel>> GetList();
        Result<List<LeadModel>> GetListByUserId(int userId);
        Result<List<LeadModel>> ListBySearch(string search);
        Result<List<LeadModel>> ListBySearch(string search, int userId);
        Result Create(CreateLeadModel createLeadModel);
        Result Edit(EditLeadModel editLeadModel);
        Result Delete(DeleteLeadModel deleteLeadModel);
    }
}
