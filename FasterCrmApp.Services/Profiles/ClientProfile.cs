using AutoMapper;
using FasterCrmApp.Entities.Concrete;
using FasterCrmApp.Models;

namespace FasterCrmApp.Services.Profiles
{
    public class ClientProfile : Profile
    {
        public ClientProfile()
        {
            CreateMap<CreateClientModel, Client>().ReverseMap();
            CreateMap<EditClientModel, Client>().ReverseMap();
            CreateMap<DeleteClientModel, Client>().ReverseMap();
            CreateMap<ClientModel, Client>().ReverseMap();
        }
    }
}
