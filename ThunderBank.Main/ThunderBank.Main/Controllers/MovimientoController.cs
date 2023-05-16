
using Microsoft.AspNetCore.Mvc;
using ThunderBank.Models;
using ThunderBank.Services.Interfaces;

namespace ThunderBank.Main.Controllers
{
    public class MovimientoController : Controller
    {
        private readonly IRepositorioCliente _repositorioCliente;
        private readonly IRepositorioCuenta _repositorioCuenta;
        private readonly IRepositorioMovimiento _repositorioMovimiento;

        public MovimientoController(IRepositorioCliente repositorioCliente,IRepositorioCuenta repositorioCuenta,IRepositorioMovimiento repositorioMovimiento)
        {
            this._repositorioCliente = repositorioCliente;
            this._repositorioCuenta = repositorioCuenta;
            this._repositorioMovimiento = repositorioMovimiento;
        }
        // GET: MovimientoController/Crear
        public ActionResult Crear()
        {

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Crear(Movimiento movimiento)
        {
            var numCuentaRelacionada = _repositorioCuenta.ObtenerNumeroDeCuenta();
            if(!ModelState.IsValid)
            {
                return View(movimiento);
            }
            movimiento.NumeroCuentaRelacionada = numCuentaRelacionada;
            await _repositorioMovimiento.Crear(movimiento);
            return RedirectToAction("Index");
        }

    }
}
