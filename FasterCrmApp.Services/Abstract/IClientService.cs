using FasterCrmApp.Models;
using FasterCrmApp.Models.Results;

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
