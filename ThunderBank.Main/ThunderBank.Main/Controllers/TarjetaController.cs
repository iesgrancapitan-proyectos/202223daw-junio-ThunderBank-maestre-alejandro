using Microsoft.AspNetCore.Mvc;
using ThunderBank.Models;
using ThunderBank.Services.Interfaces;

namespace ThunderBank.Main.Controllers
{
    public class TarjetaController : Controller
    {
        private readonly IRepositorioTarjeta _repositorioTarjeta;
        private readonly IRepositorioCuenta _repositorioCuenta;
        private readonly IRepositorioCliente _repositorioCliente;

        public TarjetaController(IRepositorioTarjeta repositorioTarjeta, IRepositorioCuenta repositorioCuenta, IRepositorioCliente repositorioCliente)
        {
            _repositorioTarjeta = repositorioTarjeta;
            _repositorioCuenta = repositorioCuenta;
            _repositorioCliente = repositorioCliente;
        }
        public async Task<IActionResult> Index()
        {
            int clienteId = _repositorioCliente.ObtenerClienteId();
            IEnumerable<Tarjeta> tarjetasCliente = await _repositorioTarjeta.ObtenerTarjetas(clienteId);
            return View(tarjetasCliente);
        }

        /*
         CREAR TARJETA
         */
        [HttpGet]
        public IActionResult Crear()
        {
            Tarjeta model = new()
            {
                NumeroDeCuenta = _repositorioCuenta.ObtenerNumeroDeCuenta()
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Crear(Tarjeta model)
        {
            await _repositorioTarjeta.Crear(model);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Info(Tarjeta tarjeta)
        {
            return View(tarjeta);
        }
    }
}
