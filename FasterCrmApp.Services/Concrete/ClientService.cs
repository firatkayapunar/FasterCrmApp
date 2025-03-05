using AutoMapper;
using FasterCrmApp.DataAccess.Abstract;
using FasterCrmApp.Entities.Concrete;
using FasterCrmApp.Models;
using FasterCrmApp.Models.Results;
using FasterCrmApp.Services.Abstract;
using FasterCrmApp.Services.Validation;
using FasterCrmApp.Services.Validation.FluentValidation;

namespace FasterCrmApp.Services.Concrete
{
    public class ClientService : IClientService
    {
        private readonly IClientRepository _clientRepository;
        private readonly IMapper _mapper;

        public ClientService(IClientRepository clientRepository, IMapper mapper)
        {
            _clientRepository = clientRepository;
            _mapper = mapper;
        }

        public Result<ClientModel> GetById(int id)
        {
            try
            {
                var existingClient = _clientRepository.GetById(id);

                if (existingClient == null)
                    return Result<ClientModel>.FailureResult("Client not found.", new List<string> { "The client with the given ID does not exist." });

                var clientModel = _mapper.Map<ClientModel>(existingClient);

                return Result<ClientModel>.SuccessResult(clientModel, "Client retrieved successfully.");
            }
            catch (Exception ex)
            {
                return Result<ClientModel>.FailureResult("An error occurred.", new List<string> { ex.Message });
            }
        }

        public Result<List<ClientModel>> GetList()
        {
            try
            {
                var clients = _clientRepository.GetAll();

                if (clients == null || !clients.Any())
                    return Result<List<ClientModel>>.SuccessResult(new List<ClientModel>(), "No clients found.");

                var clientModels = _mapper.Map<List<ClientModel>>(clients);

                return Result<List<ClientModel>>.SuccessResult(clientModels.OrderByDescending(x => x.CreatedAt).ToList(), "Clients retrieved successfully.");
            }
            catch (Exception ex)
            {
                return Result<List<ClientModel>>.FailureResult("An error occurred.", new List<string> { ex.Message });
            }
        }

        public Result<List<ClientModel>> ListBySearch(string search)
        {
            try
            {
                var clients = _clientRepository.GetAll(x =>
                              x.Name.Contains(search) ||
                              x.Email.Contains(search) ||
                              x.Phone.Contains(search) ||
                              x.Description.Contains(search)
                );

                if (clients == null || !clients.Any())
                    return Result<List<ClientModel>>.SuccessResult(new List<ClientModel>(), "No clients found.");

                var clientModels = _mapper.Map<List<ClientModel>>(clients);

                return Result<List<ClientModel>>.SuccessResult(clientModels.OrderByDescending(x => x.CreatedAt).ToList(), "Clients retrieved successfully.");
            }
            catch (Exception ex)
            {
                return Result<List<ClientModel>>.FailureResult("An error occurred.", new List<string> { ex.Message });
            }
        }

        public Result Create(CreateClientModel createClientModel)
        {
            try
            {
                var client = _mapper.Map<Client>(createClientModel);

                ValidationTool.Validate(new ClientValidator(), client);

                _clientRepository.Add(client);

                return Result.SuccessResult("Client successfully added.");
            }
            catch (CustomValidationException ex)
            {
                return Result.FailureResult("Validation failed.", ex.Errors);
            }
            catch (Exception ex)
            {
                var errors = new Dictionary<string, IEnumerable<string>>
                {
                    { "General", new List<string> { ex.Message } }
                };

                return Result.FailureResult("An error occurred while adding the client.", errors);
            }
        }

        public Result Edit(EditClientModel editClientModel)
        {
            try
            {
                var existingClient = _clientRepository.GetById(editClientModel.ID);

                if (existingClient == null)
                {
                    var errors = new Dictionary<string, IEnumerable<string>>
                    {
                        { "ID", new List<string> { "The client with the given ID does not exist." } }
                    };

                    return Result.FailureResult("Client not found.", errors);
                }

                var client = _mapper.Map(editClientModel, existingClient);
                client.CreatedAt = existingClient.CreatedAt;

                ValidationTool.Validate(new ClientValidator(), client);

                _clientRepository.Update(client);

                return Result.SuccessResult("Client successfully updated.");
            }
            catch (CustomValidationException ex)
            {
                return Result.FailureResult("Validation failed.", ex.Errors);
            }
            catch (Exception ex)
            {
                var errors = new Dictionary<string, IEnumerable<string>>
                {
                    { "General", new List<string> { ex.Message } }
                };

                return Result.FailureResult("An error occurred while updating the client.", errors);
            }
        }

        public Result Delete(DeleteClientModel deleteClientModel)
        {
            try
            {
                var existingClient = _clientRepository.GetById(deleteClientModel.ID);

                if (existingClient == null)
                {
                    var errors = new Dictionary<string, IEnumerable<string>>
                    {
                        { "ID", new List<string> { "The client with the given ID does not exist." } }
                    };

                    return Result.FailureResult("Client not found.", errors);
                }

                _clientRepository.Remove(existingClient.ID);

                return Result.SuccessResult("Client successfully deleted.");
            }
            catch (Exception ex)
            {
                var errors = new Dictionary<string, IEnumerable<string>>
                {
                    { "General", new List<string> { ex.Message } }
                };

                return Result.FailureResult("An error occurred while deleting the client.", errors);
            }
        }
    }
}
