using AutoMapper;
using FasterCrmApp.Entities.Concrete;
using FasterCrmApp.Models;

namespace FasterCrmApp.Services.Profiles
{
    public class IssueProfile : Profile
    {
        public IssueProfile()
        {
            CreateMap<CreateIssueModel, Issue>().ReverseMap();
            CreateMap<EditIssueModel, Issue>().ReverseMap();
            CreateMap<DeleteIssueModel, Issue>().ReverseMap();
            CreateMap<IssueModel, Issue>().ReverseMap();
        }
    }
}
