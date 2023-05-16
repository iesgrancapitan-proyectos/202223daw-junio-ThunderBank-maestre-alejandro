using Microsoft.AspNetCore.Mvc;
using ThunderBank.Models;
using ThunderBank.Services.Interfaces;

namespace ThunderBank.Main.Controllers
{
    public class MovimientoController: Controller
    {
        private readonly IRepositorioMovimiento _repositorioMovimiento;
        private readonly IRepositorioCuenta _repositorioCuenta;

        public MovimientoController(IRepositorioMovimiento repositorioMovimiento,IRepositorioCuenta repositorioCuenta)
        {
            this._repositorioMovimiento = repositorioMovimiento;
            this._repositorioCuenta = repositorioCuenta;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Crear()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Crear(Movimiento movimiento)
        {
            var cuentaOrigen = _repositorioCuenta.ObtenerNumeroDeCuenta();
            if (!ModelState.IsValid)
            {
                return View(movimiento);
            }
            movimiento.NumeroCuenta = cuentaOrigen;
            await _repositorioMovimiento.Crear(movimiento);
            return RedirectToAction("Index","Cuenta");
        }
    }
}
