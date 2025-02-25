using AutoMapper;
using FasterCrmApp.Entities.Concrete;
using FasterCrmApp.Models;

namespace FasterCrmApp.Services.Profiles
{
    public class LeadProfile : Profile
    {
        public LeadProfile()
        {
            CreateMap<CreateLeadModel, Lead>().ReverseMap();
            CreateMap<EditLeadModel, Lead>().ReverseMap();
            CreateMap<DeleteLeadModel, Lead>().ReverseMap();
            CreateMap<LeadModel, Lead>().ReverseMap();
        }
    }
}
