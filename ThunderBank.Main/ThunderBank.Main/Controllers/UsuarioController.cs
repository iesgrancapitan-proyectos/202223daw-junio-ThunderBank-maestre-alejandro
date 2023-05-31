using Microsoft.AspNetCore.Mvc;
using ThunderBank.Services.Interfaces;
using ThunderBank.Models.DTO;
using ThunderBank.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using Microsoft.CSharp.RuntimeBinder;
using Microsoft.AspNetCore.Authorization;
using System.Reflection;

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
        [AllowAnonymous]
        public IActionResult Registro()
        {
            return View();
        }
        [AllowAnonymous]
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
                    return RedirectToAction("Crear", "Cuenta");
                }

                await _signInManager.SignInAsync(usuario, isPersistent: true);
                return RedirectToAction("Crear", "Cuenta");
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
        [AllowAnonymous]
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [AllowAnonymous]
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
                var user = await _userManager.FindByNameAsync(modelo.Nombre);
                var roles = await _userManager.GetRolesAsync(user);
                if (roles.Contains("RESPONSABLE"))
                {
                    return RedirectToAction("ListarClientes","Cliente");
                }
                else if (roles.Contains("CLIENTE"))
                {
                    return RedirectToAction("Crear", "Cuenta");
                }
                return RedirectToAction("Index", "Home");
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

    }
}
