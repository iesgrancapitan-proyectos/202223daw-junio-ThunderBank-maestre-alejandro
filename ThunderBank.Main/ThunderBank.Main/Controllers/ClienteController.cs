using Microsoft.AspNetCore.Mvc;
using ThunderBank.Models;
using ThunderBank.Models.DTO;
using ThunderBank.Services.Interfaces;

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

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Crear()
        {
            return View();
        }

        // LISTAR CLIENTES

        // Listar todos los clientes
        public async Task<IActionResult> ListarClientes()
        {
            IEnumerable<DtoUsuario> listadoClientes = await _repositorioCliente.Listar();

            return View(listadoClientes);
        }

        // Listar por responsable
        public async Task<IActionResult> ListarClientesResponsable()
        {
            int idResponsable = _repositorioResponsable.ObtenerResponsableId();
            IEnumerable<DtoUsuario> listadoClientes = await _repositorioCliente.ListarPorResponsable(idResponsable);

            return View(listadoClientes);
        }


    }
}
