using FasterCrmApp.Common;
using FasterCrmApp.Entities.Enum;
using FasterCrmApp.Models;
using FasterCrmApp.Models.Results;
using FasterCrmApp.Services.Abstract;
using FasterCrmApp.UI.Filter;
using Microsoft.AspNetCore.Mvc;

namespace FasterCrmApp.UI.Controllers
{
    [Auth]
    public class IssueController : ControllerBase
    {
        private readonly IIssueService _issueService;
        private readonly IUserService _userService;

        public IssueController(IIssueService issueService, IUserService userService)
        {
            _issueService = issueService;
            _userService = userService;
        }

        public IActionResult Index()
        {
            Result<List<IssueModel>> result;

            var role = HttpContext.Session.GetInt32(Constants.Session_RoleValue);
            var userId = HttpContext.Session.GetInt32(Constants.Session_ID);

            if (role == (int)Role.Admin)
                result = _issueService.GetList();
            else
                result = _issueService.GetListByUserId(userId ?? 0);

            return View(result);
        }

        // GET: Issue/Search?search=value
        [HttpGet("Issue/Search")]
        public IActionResult Search(string search)
        {
            Result<List<IssueModel>> result;

            var role = HttpContext.Session.GetInt32(Constants.Session_RoleValue);
            var userId = HttpContext.Session.GetInt32(Constants.Session_ID);

            if (role == (int)Role.Admin)
            {
                if (string.IsNullOrWhiteSpace(search))
                    result = _issueService.GetList();
                else
                    result = _issueService.ListBySearch(search);
            }
            else
            {
                if (string.IsNullOrWhiteSpace(search))
                    result = _issueService.GetListByUserId(userId ?? 0);
                else
                    result = _issueService.ListBySearch(search, userId ?? 0);
            }

            return ReturnResult(result);
        }

        // GET: Issue/Details/{id}
        [HttpGet("Issue/Details/{id:int}")]
        public IActionResult Details(int id)
        {
            var result = _issueService.GetById(id);
            return ReturnResult(result);
        }

        public ActionResult GetUserList()
        {
            var role = HttpContext.Session.GetInt32(Constants.Session_RoleValue);
            var userId = HttpContext.Session.GetInt32(Constants.Session_ID);

            if (role == (int)Role.Admin)
            {
                var users = _userService.GetList();
                return Json(users);
            }
            else
            {
                var user = _userService.GetById(userId ?? 0);
                return Json(user);
            }
        }

        // POST: Issue/Create
        [HttpPost("Issue/Create")]
        public IActionResult Create(CreateIssueModel createIssueModel)
        {
            var result = _issueService.Create(createIssueModel);
            return ReturnResult(result);
        }

        // POST: Issue/Update
        [HttpPost("Issue/Update")]
        public IActionResult Update(EditIssueModel editIssueModel)
        {
            var result = _issueService.Edit(editIssueModel);
            return ReturnResult(result);
        }

        // POST: Issue/Delete/{id}
        [HttpPost("Issue/Delete/{issueID:int}")]
        public IActionResult Delete(int issueID)
        {
            var result = _issueService.Delete(new DeleteIssueModel() { ID = issueID });
            return ReturnResult(result);
        }
    }
}
