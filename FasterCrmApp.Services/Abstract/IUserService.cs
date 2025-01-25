using FasterCrmApp.Models;
using FasterCrmApp.Models.Results;

namespace FasterCrmApp.Services.Abstract
{
    public interface IUserService
    {
        Result<UserModel> Authenticate(AuthenticateModel authenticateModel);
        Result Add(CreateUserModel createUserModel);
        Result Update(UpdateUserModel updateUserModel);
        Result Delete(DeleteUserModel deleteUserModel);
        Result ChangeUsername(int id, ChangeUsernameModel changeUsernameModel);
        Result ChangePassword(int id, ChangePasswordModel changePasswordModel);
        Result<List<UserModel>> GetList(int id);
        Result<List<UserModel>> ListBySearch(string search);
    }
}
