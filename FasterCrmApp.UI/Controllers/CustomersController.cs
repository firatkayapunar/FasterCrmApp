using FasterCrmApp.Models;
using FasterCrmApp.Services.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace FasterCrmApp.UI.Controllers
{
    public class CustomersController : Controller
    {
        private readonly IClientService _clientService;

        public CustomersController(IClientService clientService)
        {
            _clientService = clientService;
        }

        public IActionResult Index()
        {
            var result = _clientService.GetList();
            return View(result);
        }

        public ActionResult Details(int id)
        {
            var result = _clientService.Get(id);
            return Json(result);
        }

        [HttpPost]
        public ActionResult Create(CreateCustomerModel createCustomerModel)
        {
            var result = _clientService.Add(createCustomerModel);
            return Json(result);
        }

        [HttpPost]
        public ActionResult Update(UpdateCustomerModel updateCustomerModel)
        {
            var result = _clientService.Update(updateCustomerModel);
            return Json(result);
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            var result = _clientService.Delete(new DeleteCustomerModel() { ID = id });
            return Json(result);
        }
    }
}
