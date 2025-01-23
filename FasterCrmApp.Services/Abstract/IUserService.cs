using FasterCrmApp.Models;
using FasterCrmApp.Models.Results;

namespace FasterCrmApp.Services.Abstract
{
    public interface IUserService
    {
        Result<UserModel> Authenticate(AuthenticateModel authenticateModel);
        Result Add(CreateUserModel createUserModel);
    }
}
