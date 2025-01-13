using FasterCrmApp.Entities.Results;
using FasterCrmApp.Models;

namespace FasterCrmApp.Services.Abstract
{
    public interface IClientService
    {
        Result Add(CreateCustomerModel createCustomerModel);
        Result Update(UpdateCustomerModel updateCustomerModel);
        Result Delete(DeleteCustomerModel deleteCustomerModel);
        Result<CustomerModel> Get(int id);
        Result<List<CustomerModel>> GetList();
    }
}
