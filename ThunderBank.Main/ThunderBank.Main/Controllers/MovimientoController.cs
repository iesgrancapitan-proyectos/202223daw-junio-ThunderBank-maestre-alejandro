using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using ThunderBank.Models;
using ThunderBank.Services.Interfaces;
using ThunderBank.Services.Repositorios;

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
            var cuentaOrigen = await _repositorioCuenta.ObtenerNumeroDeCuenta();
            if (!ModelState.IsValid)
            {
                return View(movimiento);
            }
            movimiento.NumeroCuenta = cuentaOrigen;
            try
            {
                await _repositorioMovimiento.Crear(movimiento);

            }catch(SqlException e)
            {
                TempData["ErrorMessage"] = e.Message;
                return RedirectToAction("Error","Movimiento");
            }
            return RedirectToAction("ListadoMovimientos");
        }

        public async Task<IActionResult> ListadoMovimientos()
        {
            string numeroCuenta = await _repositorioCuenta.ObtenerNumeroDeCuenta();
            IEnumerable<Movimiento> movimientosCuenta = await _repositorioMovimiento.ObtenerMovimientos(numeroCuenta);
            return View(movimientosCuenta);
        }

        public IActionResult Error()
        {
            if (TempData.ContainsKey("ErrorMessage"))
            {
                string mensajeError = TempData["ErrorMessage"].ToString();
                ViewBag.Error = mensajeError;
            }
            return View();
        }
    }
}
