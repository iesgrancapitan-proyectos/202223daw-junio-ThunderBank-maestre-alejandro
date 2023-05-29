using Microsoft.AspNetCore.Mvc;
using ThunderBank.Models;
using ThunderBank.Models.DTO;
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
            int clienteId = await _repositorioCliente.ObtenerClienteId();
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

        /*
         INFO TARJETA
         */
        [HttpGet]
        public async Task<IActionResult> Info(string numero)
        {
            DtoTarjeta model = await _repositorioTarjeta.ObtenerDatosTarjeta(numero);
            return View(model);
        }

        /*
         ACCIONES TARJETA
         */
        [HttpGet]
        public async Task<RedirectToActionResult> CongelarTarjeta(string numero)
        {
            await _repositorioTarjeta.CongelarTarjeta(numero);
            return RedirectToAction("Index", "Tarjeta");
        }

        [HttpGet]
        public async Task<RedirectToActionResult> ActivarTarjeta(string numero)
        {
            await _repositorioTarjeta.ActivarTarjeta(numero);
            return RedirectToAction("Index", "Tarjeta");
        }

        [HttpGet]
        public async Task<RedirectToActionResult> CancelarTarjeta(string numero)
        {
            await _repositorioTarjeta.CancelarTarjeta(numero);
            return RedirectToAction("Index", "Tarjeta");
        }
    }
}
