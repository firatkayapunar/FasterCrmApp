using AutoMapper;
using FasterCrmApp.Common;
using FasterCrmApp.DataAccess.Abstract;
using FasterCrmApp.Entities.Concrete;
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
        private readonly IMapper _mapper;

        public UserService(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public Result<UserModel> Authenticate(AuthenticateModel authenticateModel)
        {
            try
            {
                authenticateModel.Password = (Constants.PasswordSalt + authenticateModel.Password).MD5();

                var user = _userRepository.GetAll().Where(x => x.Username.Trim().ToLower() == authenticateModel.Username.ToLower() && x.Password == authenticateModel.Password).FirstOrDefault();

                if (user == null)
                    return Result<UserModel>.FailureResult("Client not found.", new List<string> { "The client with the given ID does not exist." });

                var userModel = _mapper.Map<UserModel>(user);

                return Result<UserModel>.SuccessResult(userModel, "Client retrieved successfully.");
            }
            catch (Exception ex)
            {
                return Result<UserModel>.FailureResult("An error occurred.", new List<string> { ex.Message });
            }
        }

        public Result Add(CreateUserModel createUserModel)
        {
            try
            {
                if (createUserModel.Password != createUserModel.RePassword)
                {
                    var errors = new Dictionary<string, IEnumerable<string>>
                    {
                        { "General", new List<string> { "Passwords do not match." } }
                    };
                    return Result.FailureResult("An error occurred while adding the user.", errors);
                }

                var user = _mapper.Map<User>(createUserModel);
                user.Password = (Constants.PasswordSalt + user.Password).MD5();
                user.CreatedAt = DateTime.Now;

                ValidationTool.Validate(new UserValidator(), user);

                _userRepository.Add(user);
                return Result.SuccessResult("User successfully added.");
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
                return Result.FailureResult("An error occurred while adding the user.", errors);
            }
        }
    }
}
