using Microsoft.AspNetCore.Mvc;
using ThunderBank.Models.DTO;
using ThunderBank.Models.Validaciones;
using ThunderBank.Services.Interfaces;

namespace ThunderBank.Main.Controllers
{
    public class ClienteController : Controller
    {
        private readonly IRepositorioCliente _repositorioCliente;

        public ClienteController(IRepositorioCliente repositorioCliente)
        {
            _repositorioCliente = repositorioCliente;
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
    }
}
