using FasterCrmApp.Models;
using FasterCrmApp.Models.Results;

namespace FasterCrmApp.Services.Abstract
{
    public interface IClientService
    {
        Result Add(CreateClientModel createClientModel);
        Result Update(UpdateClientModel updateClientModel);
        Result Delete(DeleteClientModel deleteClientModel);
        Result<ClientModel> Get(int id);
        Result<List<ClientModel>> GetList();
        Result<List<ClientModel>> ListBySearch(string search);
    }
}
