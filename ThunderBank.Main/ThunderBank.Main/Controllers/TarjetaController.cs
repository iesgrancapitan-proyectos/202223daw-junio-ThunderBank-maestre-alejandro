using Microsoft.AspNetCore.Mvc;
using System.Reflection;
using ThunderBank.Models;
using ThunderBank.Services.Interfaces;

namespace ThunderBank.Main.Controllers
{
    public class TarjetaController : Controller
    {
        private readonly IRepositorioTarjeta _repositorioTarjeta;

        public TarjetaController(IRepositorioTarjeta repositorioTarjeta)
        {
            _repositorioTarjeta = repositorioTarjeta;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Crear()
        {
            Tarjeta model = new()
            {
                NumeroDeCuenta = "123456789012345678925"
            };

            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Crear(Tarjeta model)
        {
            await _repositorioTarjeta.Crear(model);
            return RedirectToAction("Index");
        }
    }
}
