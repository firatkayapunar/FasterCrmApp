using FasterCrmApp.Models;
using FasterCrmApp.Models.Results;

namespace FasterCrmApp.Services.Abstract
{
    public interface IClientService
    {
        Result<ClientModel> GetById(int id);
        Result<List<ClientModel>> GetList();
        Result<List<ClientModel>> ListBySearch(string search);
        Result Create(CreateClientModel createClientModel);
        Result Edit(EditClientModel editClientModel);
        Result Delete(DeleteClientModel deleteClientModel);
    }
}
