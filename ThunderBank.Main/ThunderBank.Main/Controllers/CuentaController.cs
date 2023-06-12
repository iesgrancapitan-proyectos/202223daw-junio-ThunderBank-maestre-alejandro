using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ThunderBank.Models;
using ThunderBank.Services.Interfaces;

namespace ThunderBank.Main.Controllers
{
    public class CuentaController : Controller
    {
        private readonly IRepositorioCuenta _repositorioCuenta;
        private readonly IRepositorioCliente _repositorioCliente;

        public CuentaController(IRepositorioCuenta repositorioCuenta, IRepositorioCliente repositorioCliente)
        {
            this._repositorioCuenta = repositorioCuenta;
            this._repositorioCliente = repositorioCliente;
        }
        [Authorize(Roles = "CLIENTE")]
        public async Task<IActionResult> Index()
        {
            var idCliente =  await _repositorioCliente.ObtenerClienteId();
            var cuentasAgrupadas = await _repositorioCuenta.Buscar(idCliente);
            var modelo = cuentasAgrupadas.GroupBy(x => x.Tipo).Select(grupo => new CuentaViewModel
            {
                Cuentas = grupo.Where(c=>c.Activa == true).AsEnumerable()
            }).ToList();
            return View(modelo);
        }
        [Authorize(Roles = "CLIENTE")]
        [HttpGet]
        public IActionResult Crear()
        {
            return View();
        }
        [Authorize(Roles = "CLIENTE")]
        [HttpPost]
        public async Task<IActionResult> Crear(Cuenta cuenta)
        {
            int idCliente = await _repositorioCliente.ObtenerClienteId();
            if (!ModelState.IsValid)
            {
                return View(cuenta);
            }
            cuenta.IdCliente = idCliente;
            try
            {
            await _repositorioCuenta.Crear(cuenta,idCliente);
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
                return RedirectToAction("ErrorView","Home");
            }
            return RedirectToAction("Index");
        }
        [Authorize(Roles = "CLIENTE")]
        public async Task<IActionResult> Desactivar(string numCuenta)
        {
            try
            {
                await _repositorioCuenta.Desactivar(numCuenta);
                TempData["Success"] = "La cuenta " + numCuenta + " ha sido desactivada correctamente";
            }
            catch (Exception ex)
            {
                // Si ocurre algún error durante la desactivación, devuelves un mensaje de error
                TempData["Error"] = "Ha ocurrido un error al desactivar la cuenta: " + ex.Message;
            }

            // Rediriges a la acción "Index" para mostrar la vista correspondiente
            return RedirectToAction("Index");
        }

    }
}
