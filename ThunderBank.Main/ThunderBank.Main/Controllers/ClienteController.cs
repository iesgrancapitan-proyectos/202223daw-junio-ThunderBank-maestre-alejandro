using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ThunderBank.Models;
using ThunderBank.Models.DTO;
using ThunderBank.Services.Interfaces;
using ThunderBank.Services.Repositorios;

namespace ThunderBank.Main.Controllers
{
    public class ClienteController : Controller
    {
        private readonly IRepositorioCliente _repositorioCliente;
        private readonly IRepositorioResponsable _repositorioResponsable;

        public ClienteController(IRepositorioCliente repositorioCliente,
            IRepositorioResponsable repositorioResponsable)
        {
            _repositorioCliente = repositorioCliente;
            _repositorioResponsable = repositorioResponsable;
        }
        [Authorize]
        public IActionResult Index()
        {
            return View();
        }
        [Authorize]
        public IActionResult Crear()
        {
            return View();
        }

        // LISTAR CLIENTES

        // Listar todos los clientes
        [Authorize]
        public async Task<IActionResult> ListarClientes()
        {
            IEnumerable<DtoUsuario> listadoClientes = await _repositorioCliente.Listar();
            return View(listadoClientes);
        }

        // Listar por responsable
        [Authorize]
        public async Task<IActionResult> ListarClientesResponsable()
        {
            int idResponsable = _repositorioResponsable.ObtenerResponsableId();
            IEnumerable<DtoUsuario> listadoClientes = await _repositorioCliente.ListarPorResponsable(idResponsable);

            return View(listadoClientes);
        }

        [HttpGet]
        public async Task<IActionResult> Editar(string dni)
        {
            Cliente model = await _repositorioCliente.ObtenerDatosCliente(dni);
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Editar(Cliente cliente)
        {
            await _repositorioCliente.Editar(cliente);
            return RedirectToAction("ListarClientes");
        }
        


    }
}
