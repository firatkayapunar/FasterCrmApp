using FasterCrmApp.Models;
using FasterCrmApp.Models.Results;

namespace FasterCrmApp.Services.Abstract
{
    public interface IUserService
    {
        Result<UserModel> Authenticate(AuthenticateModel authenticateModel);
        Result<UserModel> GetById(int id);
        Result<List<UserModel>> GetList();
        Result<List<UserModel>> ListBySearch(string search);
        Result Create(CreateUserModel createUserModel);
        Result Edit(EditUserModel editUserModel);
        Result Delete(DeleteUserModel deleteUserModel);
        Result ChangeUsername(int id, ChangeUsernameModel changeUsernameModel);
        Result ChangePassword(int id, ChangePasswordModel changePasswordModel);
    }
}
