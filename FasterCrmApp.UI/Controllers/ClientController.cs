using FasterCrmApp.Models;
using FasterCrmApp.Models.Results;
using FasterCrmApp.Services.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace FasterCrmApp.UI.Controllers
{
    public class ClientController : ControllerBase
    {
        private readonly IClientService _clientService;

        public ClientController(IClientService clientService)
        {
            _clientService = clientService;
        }

        public IActionResult Index()
        {
            var result = _clientService.GetList();
            return View(result);
        }

        // GET: Clients/Search?search=value
        [HttpGet("Search")]
        public IActionResult Search(string search)
        {
            Result<List<ClientModel>> result;

            if (string.IsNullOrWhiteSpace(search))
                result = _clientService.GetList();
            else
                result = _clientService.ListBySearch(search);

            return ReturnResult(result);
        }

        // GET: Clients/Details/{id}
        [HttpGet("Details/{id:int}")]
        public ActionResult Details(int id)
        {
            var result = _clientService.Get(id);
            return ReturnResult(result);
        }

        // POST: Clients/Create
        [HttpPost("Create")]
        public ActionResult Create(CreateClientModel createClientModel)
        {
            var result = _clientService.Add(createClientModel);
            return ReturnResult(result);
        }

        // POST: Clients/Update
        [HttpPost("Update")]
        public ActionResult Update(UpdateClientModel updateClientModel)
        {
            var result = _clientService.Update(updateClientModel);
            return ReturnResult(result);
        }

        // POST: Clients/Delete/{id}
        [HttpPost("Delete/{clientId:int}")]
        public ActionResult Delete(int clientId)
        {
            var result = _clientService.Delete(new DeleteClientModel() { ID = clientId });
            return ReturnResult(result);
        }
    }
}
