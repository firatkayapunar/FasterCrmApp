using AutoMapper;
using FasterCrmApp.Entities.Concrete;
using FasterCrmApp.Models;

namespace FasterCrmApp.Services.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<CreateUserModel, User>().ReverseMap();
            CreateMap<EditUserModel, User>().ReverseMap();
            CreateMap<DeleteUserModel, User>().ReverseMap();
            CreateMap<UserModel, User>().ReverseMap();
        }
    }
}
