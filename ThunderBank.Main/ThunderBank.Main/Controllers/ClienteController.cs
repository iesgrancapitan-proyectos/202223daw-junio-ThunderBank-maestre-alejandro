using Microsoft.AspNetCore.Mvc;
using ThunderBank.Models;
using ThunderBank.Services.Interfaces;

namespace ThunderBank.Main.Controllers
{
    public class ClienteController : Controller
    {
        private readonly IRepositorioCliente _repositorioCliente;

        public ClienteController(IRepositorioCliente repositorioCliente)
        {
            this._repositorioCliente = repositorioCliente;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Crear()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Crear(Cliente cliente)
        {
            if (!ModelState.IsValid)
            {
                return View(cliente);
            }
            await _repositorioCliente.Crear(cliente);
            return RedirectToAction("Index");
        }
    }
}
