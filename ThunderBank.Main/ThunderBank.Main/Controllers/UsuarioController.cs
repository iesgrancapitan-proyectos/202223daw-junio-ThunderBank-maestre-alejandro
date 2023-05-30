using Microsoft.AspNetCore.Mvc;
using ThunderBank.Services.Interfaces;
using ThunderBank.Models.DTO;
using ThunderBank.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using Microsoft.CSharp.RuntimeBinder;

namespace ThunderBank.Main.Controllers
{
    public class UsuarioController : Controller
    {
        private readonly UserManager<Usuario> _userManager;
        private readonly SignInManager<Usuario> _signInManager;
        private readonly IRepositorioCliente _repositorioCliente;

        public UsuarioController(UserManager<Usuario> userManager,SignInManager<Usuario> signInManager,IRepositorioCliente repositorioCliente)
        {
            this._userManager = userManager;
            this._signInManager = signInManager;
            this._repositorioCliente = repositorioCliente;
        }

        public IActionResult Registro()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Registro(RegistroViewModel modelo)
        {
            if (!ModelState.IsValid)
            {
                return View(modelo);
            }

            var usuario = new Usuario() { Nombre = modelo.Nombre };
            var resultado = await _userManager.CreateAsync(usuario,password:modelo.Pwd);
            if(resultado.Succeeded)
            {
                var cliente = new Cliente()
                {
                    Dni = modelo.Dni,
                    Nombre = modelo.Nombre,
                    Apellido = modelo.Apellido,
                    Correo = modelo.Correo,
                    Telefono = modelo.Telefono,
                    FechaDeNacimiento = modelo.FechaDeNacimiento,
                    FechaDeAlta = DateTime.Now,
                    IdUsuario = usuario.Id,
                    Activo = 1
                };
                try
                {
                    await _repositorioCliente.CrearCliente(cliente);

                }catch (RuntimeBinderException)
                {
                    await _signInManager.SignInAsync(usuario, isPersistent: true);
                    return RedirectToAction("Index", "Tarjeta");
                }

                await _signInManager.SignInAsync(usuario, isPersistent: true);
                return RedirectToAction("Index","Tarjeta");
            }
            else
            {
                foreach (var error in resultado.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
                return View(modelo);
            }
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel modelo)
        {
            if (!ModelState.IsValid)
            {
                return View(modelo); 
            }

            var resultado = await _signInManager.PasswordSignInAsync(modelo.Nombre, modelo.Pwd,
                            modelo.Recuerdame, lockoutOnFailure: false);

            if (resultado.Succeeded)
            {
                return RedirectToAction("Index", "Cuenta");
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Nombre de usuario o Contraseña incorrecto");
                return View(modelo);
            }

        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(IdentityConstants.ApplicationScheme);
            return RedirectToAction("Index","Home");
        }



        //    private readonly IRepositorioUsuario _repositorioUsuario;
        //    private readonly IRepositorioResponsable _repositorioResponsable;

        //    public UsuarioController(IRepositorioUsuario repositorioUsuario, IRepositorioResponsable repositorioResponsable)
        //    {
        //        _repositorioUsuario = repositorioUsuario;
        //        _repositorioResponsable = repositorioResponsable;
        //    }

        //    // CREAR USUARIOS
        //    [HttpGet]
        //    public IActionResult Crear()
        //    {
        //        return View();
        //    }

        //    [HttpPost]
        //    public async Task<IActionResult> Crear(DtoUsuario usuario)
        //    {
        //        if (!ModelState.IsValid)
        //        {
        //            return View(usuario);
        //        }


        //        DtoUsuario cuenta = await _repositorioUsuario.Existe(usuario.Dni);
        //        if (cuenta is not null)
        //        {
        //            return RedirectToAction("Error", "Shared");
        //        }

        //        usuario.NombreUsuario = usuario.Dni;
        //        usuario.ResponsableId = _repositorioResponsable.ObtenerResponsableId();

        //        await _repositorioUsuario.Crear(usuario);
        //        return RedirectToAction("Index", "Usuario");
        //    }

        //    [HttpGet]
        //    public IActionResult Editar()
        //    {
        //        return View();
        //    }
    }
}
