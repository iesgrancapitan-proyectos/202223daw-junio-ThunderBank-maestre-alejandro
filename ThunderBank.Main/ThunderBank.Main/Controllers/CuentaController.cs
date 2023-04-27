using Microsoft.AspNetCore.Mvc;
using ThunderBank.Models;
using ThunderBank.Services.Interfaces;

namespace ThunderBank.Main.Controllers
{
    public class CuentaController : Controller
    {
        private readonly IRepositorioCuenta _repositorioCuenta;

        public CuentaController(IRepositorioCuenta repositorioCuenta)
        {
            this._repositorioCuenta = repositorioCuenta;
        }

        [HttpGet]
        public IActionResult Crear()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Crear(Cuenta cuenta)
        {
            if (!ModelState.IsValid)
            {
                return View(cuenta);
            }
            await _repositorioCuenta.Crear(cuenta);
            return RedirectToAction("Index");
        }
    }
}
