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
        public async Task<IActionResult> Index()
        {
            //Hay que obtener las cuentas por idCliente

            var cuentasAgrupadas = await _repositorioCuenta.Buscar(1002); //idCliente metido a pelo
            var modelo = cuentasAgrupadas.GroupBy(x => x.Tipo).Select(grupo => new CuentaIndex
            {
                Cuentas = grupo.AsEnumerable()
            }).ToList();
            return View(modelo);
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
            return RedirectToAction("Index","Home");
        }
    }
}
