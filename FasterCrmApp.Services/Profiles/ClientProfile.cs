using AutoMapper;
using FasterCrmApp.Entities.Concrete;
using FasterCrmApp.Models;

namespace FasterCrmApp.Services.Profiles
{
    public class ClientProfile : Profile
    {
        public ClientProfile()
        {
            // DTO to Entity
            CreateMap<CreateCustomerModel, Client>().ReverseMap();
            CreateMap<UpdateCustomerModel, Client>().ReverseMap();
            CreateMap<DeleteCustomerModel, Client>().ReverseMap();
            CreateMap<CustomerModel, Client>().ReverseMap();
        }
    }
}
