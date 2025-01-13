using AutoMapper;
using FasterCrmApp.DataAccess.Abstract;
using FasterCrmApp.Entities.Concrete;
using FasterCrmApp.Entities.Results;
using FasterCrmApp.Models;
using FasterCrmApp.Services.Abstract;
using FasterCrmApp.Services.Validation;
using FasterCrmApp.Services.Validation.FluentValidation;
using System.ComponentModel.DataAnnotations;

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

                ValidationTool.Validate(new ClientValidator(), client);

                client.CreatedAt = DateTime.Now;

                _clientRepository.Add(client);

                return Result.SuccessResult("Customer successfully added.");
            }
            catch (ValidationException ex)
            {
                return Result.FailureResult("Validation failed.", new List<string> { ex.Message });
            }
            catch (Exception ex)
            {
                return Result.FailureResult("An error occurred while adding the customer.", new List<string> { ex.Message });
            }
        }

        public Result Update(UpdateCustomerModel updateCustomerModel)
        {
            try
            {
                var existingClient = _clientRepository.GetById(updateCustomerModel.ID);

                if (existingClient == null)
                    return Result.FailureResult("Client not found.", new List<string> { "The client with the given ID does not exist." });

                var client = _mapper.Map<Client>(updateCustomerModel);

                ValidationTool.Validate(new ClientValidator(), client);

                _clientRepository.Update(client);

                return Result.SuccessResult("Client successfully updated.");
            }
            catch (ValidationException ex)
            {
                return Result.FailureResult("Validation failed.", new List<string> { ex.Message });
            }
            catch (Exception ex)
            {
                return Result.FailureResult("An error occurred while updating the client.", new List<string> { ex.Message });
            }
        }
        public Result Delete(DeleteCustomerModel deleteCustomerModel)
        {
            try
            {
                var client = _clientRepository.GetById(deleteCustomerModel.ID);

                if (client == null)
                    return Result.FailureResult("Client not found.", new List<string> { "The customer with the given ID does not exist." });

                _clientRepository.Remove(client.ID);

                return Result.SuccessResult("Client successfully deleted.");
            }
            catch (Exception ex)
            {
                return Result.FailureResult("An error occurred while deleting the client.", new List<string> { ex.Message });
            }
        }
        public Result<CustomerModel> Get(int id)
        {
            try
            {
                var client = _clientRepository.GetById(id);

                if (client == null)
                    return Result<CustomerModel>.FailureResult("Client not found.", new List<string> { "The client with the given ID does not exist." });

                return Result<CustomerModel>.SuccessResult(_mapper.Map<CustomerModel>(client));
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

                return Result<List<CustomerModel>>.SuccessResult(_mapper.Map<List<CustomerModel>>(clients));
            }
            catch (Exception ex)
            {
                return Result<List<CustomerModel>>.FailureResult("An error occurred.", new List<string> { ex.Message });
            }
        }
    }
}
