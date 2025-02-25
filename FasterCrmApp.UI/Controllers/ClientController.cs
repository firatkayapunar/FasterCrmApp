using FasterCrmApp.Models;
using FasterCrmApp.Models.Results;
using FasterCrmApp.Services.Abstract;
using FasterCrmApp.UI.Filter;
using Microsoft.AspNetCore.Mvc;

namespace FasterCrmApp.UI.Controllers
{
    [Auth]
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

        // GET: Client/Search?search=value
        [HttpGet("Client/Search")]
        public IActionResult Search(string search)
        {
            Result<List<ClientModel>> result;

            if (string.IsNullOrWhiteSpace(search))
                result = _clientService.GetList();
            else
                result = _clientService.ListBySearch(search);

            return ReturnResult(result);
        }

        // GET: Client/Details/{id}
        [HttpGet("Client/Details/{id:int}")]
        public IActionResult Details(int id)
        {
            var result = _clientService.GetById(id);
            return ReturnResult(result);
        }

        // POST: Client/Create
        [HttpPost("Client/Create")]
        public IActionResult Create(CreateClientModel createClientModel)
        {
            var result = _clientService.Create(createClientModel);
            return ReturnResult(result);
        }

        // POST: Client/Update
        [HttpPost("Client/Update")]
        public IActionResult Update(EditClientModel updateClientModel)
        {
            var result = _clientService.Edit(updateClientModel);
            return ReturnResult(result);
        }

        // POST: Client/Delete/{id}
        [HttpPost("Client/Delete/{clientId:int}")]
        public IActionResult Delete(int clientId)
        {
            var result = _clientService.Delete(new DeleteClientModel() { ID = clientId });
            return ReturnResult(result);
        }
    }
}
