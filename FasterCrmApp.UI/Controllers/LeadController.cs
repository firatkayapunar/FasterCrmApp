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
    public class LeadController : ControllerBase
    {
        private readonly ILeadService _leadService;
        private readonly IUserService _userService;
        private readonly IClientService _clientService;

        public LeadController(ILeadService leadService, IUserService userService, IClientService clientService)
        {
            _leadService = leadService;
            _userService = userService;
            _clientService = clientService;
        }

        public IActionResult Index()
        {
            Result<List<LeadModel>> result;

            var role = HttpContext.Session.GetInt32(Constants.Session_RoleValue);
            var userId = HttpContext.Session.GetInt32(Constants.Session_ID);

            if (role == (int)Role.Admin)
                result = _leadService.GetList();
            else
                result = _leadService.GetListByUserId(userId ?? 0);

            return View(result);
        }

        // GET: Lead/Search?search=value
        [HttpGet("Lead/Search")]
        public IActionResult Search(string search)
        {
            Result<List<LeadModel>> result;

            var role = HttpContext.Session.GetInt32(Constants.Session_RoleValue);
            var userId = HttpContext.Session.GetInt32(Constants.Session_ID);

            if (role == (int)Role.Admin)
            {
                if (string.IsNullOrWhiteSpace(search))
                    result = _leadService.GetList();
                else
                    result = _leadService.ListBySearch(search);
            }
            else
            {
                if (string.IsNullOrWhiteSpace(search))
                    result = _leadService.GetListByUserId(userId ?? 0);
                else
                    result = _leadService.ListBySearch(search, userId ?? 0);
            }

            return ReturnResult(result);
        }

        // GET: Lead/Details/{id}
        [HttpGet("Lead/Details/{id:int}")]
        public IActionResult Details(int id)
        {
            var result = _leadService.GetById(id);
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

        public ActionResult GetClientList()
        {
            var clients = _clientService.GetList();
            return Json(clients);
        }


        // POST: Lead/Create
        [HttpPost("Lead/Create")]
        public IActionResult Create(CreateLeadModel createLeadModel)
        {
            var result = _leadService.Create(createLeadModel);
            return ReturnResult(result);
        }

        // POST: Lead/Update
        [HttpPost("Lead/Update")]
        public IActionResult Update(EditLeadModel editLeadModel)
        {
            var result = _leadService.Edit(editLeadModel);
            return ReturnResult(result);
        }

        // POST: Lead/Delete/{id}
        [HttpPost("Lead/Delete/{leadId:int}")]
        public IActionResult Delete(int leadID)
        {
            var result = _leadService.Delete(new DeleteLeadModel() { ID = leadID });
            return ReturnResult(result);
        }
    }
}
