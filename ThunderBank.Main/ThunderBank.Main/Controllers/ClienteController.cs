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
        [Authorize(Roles = "RESPONSABLE")]
        public async Task<IActionResult> ListarClientes()
        {
            ViewBag.Id = _repositorioResponsable.ObtenerResponsableId();
            IEnumerable<DtoUsuario> listadoClientes = await _repositorioCliente.Listar();
            return View(listadoClientes);
        }

        // Listar por responsable
        [Authorize(Roles = "RESPONSABLE")]
        public async Task<IActionResult> ListarClientesResponsable()
        {
            int idResponsable = await _repositorioResponsable.ObtenerResponsableId();
            ViewBag.Id = _repositorioResponsable.ObtenerResponsableId();
            IEnumerable<DtoUsuario> listadoClientes = await _repositorioCliente.ListarPorResponsable(idResponsable);

            return View(listadoClientes);
        }

        [Authorize(Roles = "RESPONSABLE")]
        [HttpGet]
        public async Task<IActionResult> Editar(string dni)
        {
            Cliente model = await _repositorioCliente.ObtenerDatosCliente(dni);
            return View(model);
        }
        [Authorize(Roles = "RESPONSABLE")]
        [HttpPost]
        public async Task<IActionResult> Editar(Cliente cliente)
        {
            await _repositorioCliente.Editar(cliente);
            return RedirectToAction("ListarClientes");
        }

        [Authorize(Roles = "RESPONSABLE")]
        public async Task<IActionResult> SoltarCliente(int idCliente)
        {
            try
            {
                await _repositorioCliente.Soltar(idCliente);

                // Si la desactivación es exitosa, devuelves un mensaje de éxito
                TempData["LiberarSuccess"] = "Cliente liberado";
            }
            catch (Exception ex)
            {
                // Si ocurre algún error durante la desactivación, devuelves un mensaje de error
                TempData["LiberarError"] = "Ha ocurrido un error al liberar al cliente: " + ex.Message;
            }

            // Rediriges a la acción "Index" para mostrar la vista correspondiente
            return RedirectToAction("ListarClientes");
        }

        [Authorize(Roles = "RESPONSABLE")]
        public async Task<IActionResult> AsignarCliente(int idCliente)
        {
            try
            {
                int idResponsable = await _repositorioResponsable.ObtenerResponsableId();
                await _repositorioCliente.Asignar(idCliente, idResponsable);

                // Si la desactivación es exitosa, devuelves un mensaje de éxito
                TempData["AsignarSuccess"] = "Cliente asignado";
            }
            catch (Exception ex)
            {
                // Si ocurre algún error durante la desactivación, devuelves un mensaje de error
                TempData["AsignarError"] = "Ha ocurrido un error al asignar al cliente: " + ex.Message;
            }

            // Rediriges a la acción "Index" para mostrar la vista correspondiente
            return RedirectToAction("ListarClientes");
        }

    }
}
