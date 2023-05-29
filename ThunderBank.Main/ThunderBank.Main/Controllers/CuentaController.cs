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
        public async Task<IActionResult> Index()
        {
            var idCliente =  await _repositorioCliente.ObtenerClienteId();
            var cuentasAgrupadas = await _repositorioCuenta.Buscar(idCliente);
            var modelo = cuentasAgrupadas.GroupBy(x => x.Tipo).Select(grupo => new CuentaViewModel
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
            int idCliente = await _repositorioCliente.ObtenerClienteId();
            if (!ModelState.IsValid)
            {
                return View(cuenta);
            }
            cuenta.IdCliente = idCliente;
            await _repositorioCuenta.Crear(cuenta,idCliente);
            return RedirectToAction("Index");
        }
    }
}
