using FasterCrmApp.Common;
using FasterCrmApp.Models;
using FasterCrmApp.Models.Results;
using FasterCrmApp.Services.Abstract;
using FasterCrmApp.UI.Filter;
using Microsoft.AspNetCore.Mvc;

namespace FasterCrmApp.UI.Controllers
{
    [Auth(Roles = Constants.Role_Admin)]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        public IActionResult Index()
        {
            var result = _userService.GetList();
            return View(result);
        }

        // GET: User/Search?search=value
        [HttpGet("User/Search")]
        public IActionResult Search(string search)
        {
            Result<List<UserModel>> result;

            if (string.IsNullOrWhiteSpace(search))
                result = _userService.GetList();
            else
                result = _userService.ListBySearch(search);

            return ReturnResult(result);
        }

        // GET: User/Details/{id}
        [HttpGet("User/Details/{id:int}")]
        public IActionResult Details(int id)
        {
            var result = _userService.GetById(id);
            return ReturnResult(result);
        }

        // POST: User/Create
        [HttpPost("User/Create")]
        public IActionResult Create(CreateUserModel createUserModel)
        {
            var result = _userService.Create(createUserModel);
            return ReturnResult(result);
        }

        // POST: User/Update
        [HttpPost("User/Update")]
        public IActionResult Update(EditUserModel updateUserModel)
        {
            var result = _userService.Edit(updateUserModel);
            return ReturnResult(result);
        }

        // GET: User/ChangeUsername/{id}
        [HttpPost("User/ChangeUsername/{id:int}")]
        public IActionResult ChangeUsername(int id, ChangeUsernameModel changeUsernameModel)
        {
            var result = _userService.ChangeUsername(id, changeUsernameModel);
            return ReturnResult(result);
        }

        // GET: User/ChangePassword/{id}
        [HttpPost("User/ChangePassword/{id:int}")]
        public IActionResult ChangePassword(int id, ChangePasswordModel changePasswordModel)
        {
            var result = _userService.ChangePassword(id, changePasswordModel);
            return ReturnResult(result);
        }

        // POST: User/Delete/{id}
        [HttpPost("User/Delete/{userId:int}")]
        public IActionResult Delete(int userId)
        {
            var result = _userService.Delete(new DeleteUserModel() { ID = userId });
            return ReturnResult(result);
        }
    }
}
