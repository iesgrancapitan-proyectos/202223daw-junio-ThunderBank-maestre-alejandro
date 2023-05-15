using Microsoft.AspNetCore.Mvc;
using ThunderBank.Services.Interfaces;
using ThunderBank.Models.DTO;

namespace ThunderBank.Main.Controllers
{
    public class UsuarioController : Controller
    {
        private readonly IRepositorioUsuario _repositorioUsuario;
        private readonly IRepositorioResponsable _repositorioResponsable;

        public UsuarioController(IRepositorioUsuario repositorioUsuario, IRepositorioResponsable repositorioResponsable)
        {
            _repositorioUsuario = repositorioUsuario;
            _repositorioResponsable = repositorioResponsable;
        }
        public IActionResult Index()
        {
            return View();
        }

        // CREAR USUARIOS
        [HttpGet]
        public IActionResult Crear()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Crear(DtoUsuario usuario)
        {
            if (!ModelState.IsValid)
            {
                return View(usuario);
            }

            
            DtoUsuario cuenta = await _repositorioUsuario.Existe(usuario.Dni);
            if (cuenta is not null)
            {
                return RedirectToAction("Error", "Shared");
            }

            usuario.NombreUsuario = usuario.Dni;
            usuario.ResponsableId = _repositorioResponsable.ObtenerResponsableId();

            await _repositorioUsuario.Crear(usuario);
            return RedirectToAction("Index", "Usuario");
        }
    }
}
