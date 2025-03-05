using AutoMapper;
using FasterCrmApp.Entities.Concrete;
using FasterCrmApp.Models;

namespace FasterCrmApp.Services.Profiles
{
    public class LogProfile : Profile
    {
        public LogProfile()
        {
            CreateMap<CreateLogModel, Log>().ReverseMap();
            CreateMap<EditLogModel, Log>().ReverseMap();
            CreateMap<DeleteLogModel, Log>().ReverseMap();
            CreateMap<LogModel, Log>().ReverseMap();
        }
    }
}
