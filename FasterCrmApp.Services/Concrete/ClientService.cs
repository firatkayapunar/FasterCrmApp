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

        public Result Add(CreateCustomerModel createCustomerModel)
        {
            try
            {
                var client = _mapper.Map<Client>(createCustomerModel);

                // Validasyonu çalıştırıyoruz
                ValidationTool.Validate(new ClientValidator(), client);

                client.CreatedAt = DateTime.Now;

                _clientRepository.Add(client);
                return Result.SuccessResult("Client successfully added.");
            }
            catch (CustomValidationException ex)
            {
                // CustomValidationException içindeki hataları direkt alıyoruz.
                return Result.FailureResult("Validation failed.", ex.Errors);
            }
            catch (Exception ex)
            {
                var errors = new Dictionary<string, IEnumerable<string>>
                {
                    { "General", new List<string> { ex.Message } }
                };
                return Result.FailureResult("An error occurred while adding the customer.", errors);
            }
        }

        public Result Update(UpdateCustomerModel updateCustomerModel)
        {
            try
            {
                var existingClient = _clientRepository.GetById(updateCustomerModel.ID);

                if (existingClient == null)
                {
                    var errors = new Dictionary<string, IEnumerable<string>>
                    {
                        { "ID", new List<string> { "The client with the given ID does not exist." } }
                    };
                    return Result.FailureResult("Client not found.", errors);
                }

                var client = _mapper.Map(updateCustomerModel, existingClient);

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

        public Result Delete(DeleteCustomerModel deleteCustomerModel)
        {
            try
            {
                var client = _clientRepository.GetById(deleteCustomerModel.ID);

                if (client == null)
                {
                    var errors = new Dictionary<string, IEnumerable<string>>
                    {
                        { "ID", new List<string> { "The customer with the given ID does not exist." } }
                    };
                    return Result.FailureResult("Client not found.", errors);
                }

                _clientRepository.Remove(client.ID);

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

        public Result<CustomerModel> Get(int id)
        {
            try
            {
                var client = _clientRepository.GetById(id);

                if (client == null)
                    return Result<CustomerModel>.FailureResult("Client not found.", new List<string> { "The client with the given ID does not exist." });

                var customerModel = _mapper.Map<CustomerModel>(client);

                return Result<CustomerModel>.SuccessResult(customerModel, "Client retrieved successfully.");
            }
            catch (Exception ex)
            {
                return Result<CustomerModel>.FailureResult("An error occurred.", new List<string> { ex.Message });
            }
        }

        public Result<List<CustomerModel>> GetList()
        {
            try
            {
                var clients = _clientRepository.GetAll();

                if (clients == null || !clients.Any())
                    return Result<List<CustomerModel>>.FailureResult("No clients found.", new List<string> { "The database contains no clients." });

                var customerModels = _mapper.Map<List<CustomerModel>>(clients);

                return Result<List<CustomerModel>>.SuccessResult(customerModels.OrderByDescending(x => x.CreatedAt).ToList(), "Clients retrieved successfully.");
            }
            catch (Exception ex)
            {
                return Result<List<CustomerModel>>.FailureResult("An error occurred.", new List<string> { ex.Message });
            }
        }
    }
}
