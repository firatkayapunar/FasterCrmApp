using AutoMapper;
using FasterCrmApp.Common;
using FasterCrmApp.DataAccess.Abstract;
using FasterCrmApp.Entities.Concrete;
using FasterCrmApp.Entities.Enum;
using FasterCrmApp.Models;
using FasterCrmApp.Models.Results;
using FasterCrmApp.Services.Abstract;
using FasterCrmApp.Services.Validation;
using FasterCrmApp.Services.Validation.FluentValidation;
using NETCore.Encrypt.Extensions;

namespace FasterCrmApp.Services.Concrete
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly ILogService _logService;
        private readonly IMapper _mapper;

        public UserService(IUserRepository userRepository, ILogService logService, IMapper mapper)
        {
            _userRepository = userRepository;
            _logService = logService;
            _mapper = mapper;
        }

        public Result<UserModel> Authenticate(AuthenticateModel authenticateModel)
        {
            try
            {
                authenticateModel.Password = (Constants.PasswordSalt + authenticateModel.Password).MD5();

                var user = _userRepository.GetAll(x => x.Username.Trim().ToLower() == authenticateModel.Username.ToLower() && x.Password == authenticateModel.Password).FirstOrDefault();

                if (user == null)
                    return Result<UserModel>.FailureResult("User not found.", new List<string> { "The user with the specified credentials does not exist." });

                var userModel = _mapper.Map<UserModel>(user);

                // Aslında burada Unit of Work pattern kullanarak, User authenticate işlemi sırasında herhangi bir sorun yaşanmazken, Log eklenirken bir hata oluştuğu durumu kontrol altına almamız ve işlemi transaction ile yönetmemiz gerekirdi. Böylece, herhangi bir aşamada hata meydana geldiğinde tüm işlemler geri alınarak (rollback) veri bütünlüğü korunmuş olur.

                _logService.Create(new CreateLogModel
                {
                    Text = "Sistem girişi yapıldı.",
                    LogType = (int)LogType.SystemLogin,
                    UserID = userModel.ID
                });

                return Result<UserModel>.SuccessResult(userModel, "You have been successfully logged in.");
            }
            catch (Exception ex)
            {
                return Result<UserModel>.FailureResult("An error occurred.", new List<string> { ex.Message });
            }
        }
       
        public Result<UserModel> GetById(int id)
        {
            try
            {
                var existingUser = _userRepository.GetById(id);

                if (existingUser == null)
                    return Result<UserModel>.FailureResult("User not found.", new List<string> { "The user with the given ID does not exist." });

                var userModel = _mapper.Map<UserModel>(existingUser);

                return Result<UserModel>.SuccessResult(userModel, "User retrieved successfully.");
            }
            catch (Exception ex)
            {
                return Result<UserModel>.FailureResult("An error occurred.", new List<string> { ex.Message });
            }
        }

        public Result<List<UserModel>> GetList()
        {
            try
            {
                var users = _userRepository.GetAll();

                if (users == null || !users.Any())
                    return Result<List<UserModel>>.SuccessResult(new List<UserModel>(), "No users found.");

                var userModels = _mapper.Map<List<UserModel>>(users);

                foreach (var userModel in userModels)
                {
                    userModel.RoleName = RoleHelper.GetRoleName((Role)userModel.Role);
                }

                return Result<List<UserModel>>.SuccessResult(userModels.OrderByDescending(x => x.CreatedAt).ToList(), "Users retrieved successfully.");
            }
            catch (Exception ex)
            {
                return Result<List<UserModel>>.FailureResult("An error occurred.", new List<string> { ex.Message });
            }
        }

        public Result<List<UserModel>> ListBySearch(string search)
        {
            try
            {
                var users = _userRepository
                    .GetAll(x => x.Name.Contains(search)
                                 || x.Username.Contains(search)
                                 || x.Email.Contains(search))
                    .ToList();

                if (!users.Any())
                {
                    users = _userRepository
                        .GetAll()
                        .ToList()
                        .Where(x => RoleHelper.GetRoleName(x.Role).Contains(search))
                        .ToList();
                }

                if (users == null || !users.Any())
                    return Result<List<UserModel>>.SuccessResult(new List<UserModel>(), "No users found.");

                var userModels = _mapper.Map<List<UserModel>>(users)
                    .Select(userModel =>
                    {
                        userModel.RoleName = RoleHelper.GetRoleName((Role)userModel.Role);
                        return userModel;
                    })
                    .OrderByDescending(x => x.CreatedAt)
                    .ToList();

                return Result<List<UserModel>>.SuccessResult(userModels, "Users retrieved successfully.");
            }
            catch (Exception ex)
            {
                return Result<List<UserModel>>.FailureResult("An error occurred while retrieving users.", new List<string> { ex.Message });
            }
        }

        public Result Create(CreateUserModel createUserModel)
        {
            try
            {
                if (createUserModel.Password != createUserModel.RePassword)
                {
                    var errors = new Dictionary<string, IEnumerable<string>>
                    {
                        { "General", new List<string> { "The passwords provided do not match." } }
                    };
                    return Result.FailureResult("Failed to add the user.", errors);
                }

                // Note : Role gelen modelde int olarak ayarlandı. Ancak User modelin içinde Role enumı olarak tutuluyor. Bu dönüşümü AutoMapper kendisi yapabiliyor.

                var user = _mapper.Map<User>(createUserModel);
                user.Password = (Constants.PasswordSalt + user.Password).MD5();

                ValidationTool.Validate(new UserValidator(), user);

                _userRepository.Add(user);
                return Result.SuccessResult("User successfully added.");
            }
            catch (CustomValidationException ex)
            {
                return Result.FailureResult("Validation error.", ex.Errors);
            }
            catch (Exception ex)
            {
                var errors = new Dictionary<string, IEnumerable<string>>
                {
                    { "General", new List<string> { ex.Message } }
                };
                return Result.FailureResult("An error occurred while adding the user.", errors);
            }
        }

        public Result Edit(EditUserModel editUserModel)
        {
            try
            {
                var existingUser = _userRepository.GetById(editUserModel.ID);
                var username = existingUser.Username;

                if (existingUser == null)
                {
                    var errors = new Dictionary<string, IEnumerable<string>>
                    {
                        { "ID", new List<string> { "The user with the given ID does not exist." } }
                    };
                    return Result.FailureResult("User not found.", errors);
                }

                var user = _mapper.Map(editUserModel, existingUser);
                user.CreatedAt = existingUser.CreatedAt;
                user.Username = username;

                ValidationTool.Validate(new UserValidator(), user);

                _userRepository.Update(user);

                return Result.SuccessResult("User successfully updated.");
            }
            catch (CustomValidationException ex)
            {
                return Result.FailureResult("Validation failed.", ex.Errors);
            }
            catch (Exception ex)
            {
                var errors = new Dictionary<string, IEnumerable<string>>
                {
                    { "General", new List<string> { ex.Message } }
                };
                return Result.FailureResult("An error occurred while updating the user.", errors);
            }
        }

        public Result Delete(DeleteUserModel deleteUserModel)
        {
            try
            {
                var user = _userRepository.GetById(deleteUserModel.ID);

                if (user == null)
                {
                    var errors = new Dictionary<string, IEnumerable<string>>
                    {
                        { "ID", new List<string> { "The user with the given ID does not exist." } }
                    };
                    return Result.FailureResult("User not found.", errors);
                }

                _userRepository.Remove(user.ID);

                return Result.SuccessResult("User successfully deleted.");
            }
            catch (Exception ex)
            {
                var errors = new Dictionary<string, IEnumerable<string>>
                {
                    { "General", new List<string> { ex.Message } }
                };
                return Result.FailureResult("An error occurred while deleting the user.", errors);
            }
        }

        public Result ChangePassword(int id, ChangePasswordModel changePasswordModel)
        {
            try
            {
                var user = _userRepository.GetById(id);

                if (user == null)
                {
                    var errors = new Dictionary<string, IEnumerable<string>>
                    {
                        { "ID", new List<string> { "The user with the given ID does not exist." } }
                    };
                    return Result.FailureResult("User not found.", errors);
                }

                if (changePasswordModel.Password != changePasswordModel.RePassword)
                {
                    var errors = new Dictionary<string, IEnumerable<string>>
                    {
                        { "Password", new List<string> { "Passwords do not match." } }
                    };
                    return Result.FailureResult("Password change failed.", errors);
                }

                user.Password = (Constants.PasswordSalt + changePasswordModel.Password).MD5();

                _userRepository.Update(user);

                return Result.SuccessResult("Password successfully changed.");
            }
            catch (Exception ex)
            {
                var errors = new Dictionary<string, IEnumerable<string>>
                {
                    { "General", new List<string> { ex.Message } }
                };
                return Result.FailureResult("An error occurred while changing the password.", errors);
            }
        }

        public Result ChangeUsername(int id, ChangeUsernameModel changeUsernameModel)
        {
            try
            {
                var user = _userRepository.GetById(id);

                if (user == null)
                {
                    var errors = new Dictionary<string, IEnumerable<string>>
                    {
                        { "ID", new List<string> { "The user with the given ID does not exist." } }
                    };

                    return Result.FailureResult("User not found.", errors);
                }

                var usernameExists = _userRepository.GetAll(x => x.Username.Trim().ToLower() == changeUsernameModel.Username.Trim().ToLower() && x.ID != id).Any();

                if (usernameExists)
                {
                    var errors = new Dictionary<string, IEnumerable<string>>
                    {
                        { "Username", new List<string> { "The provided username is already in use." } }
                    };

                    return Result.FailureResult("Username change failed.", errors);
                }

                user.Username = changeUsernameModel.Username.Trim();

                _userRepository.Update(user);

                return Result.SuccessResult("Username successfully changed.");
            }
            catch (Exception ex)
            {
                var errors = new Dictionary<string, IEnumerable<string>>
                {
                    { "General", new List<string> { ex.Message } }
                };
                return Result.FailureResult("An error occurred while changing the username.", errors);
            }
        }
    }
}
