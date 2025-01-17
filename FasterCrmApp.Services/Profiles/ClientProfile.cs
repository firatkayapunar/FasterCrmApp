﻿using AutoMapper;
using FasterCrmApp.Entities.Concrete;
using FasterCrmApp.Models;

namespace FasterCrmApp.Services.Profiles
{
    public class ClientProfile : Profile
    {
        public ClientProfile()
        {
            // DTO to Entity
            CreateMap<CreateClientModel, Client>().ReverseMap();
            CreateMap<UpdateClientModel, Client>().ReverseMap();
            CreateMap<DeleteClientModel, Client>().ReverseMap();
            CreateMap<ClientModel, Client>().ReverseMap();
        }
    }
}
