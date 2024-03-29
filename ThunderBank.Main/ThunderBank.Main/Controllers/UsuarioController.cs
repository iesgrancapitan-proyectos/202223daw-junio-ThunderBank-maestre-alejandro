﻿using Microsoft.AspNetCore.Mvc;
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
        private readonly IRepositorioResponsable _repositorioResponsable;

        public UsuarioController(UserManager<Usuario> userManager, SignInManager<Usuario> signInManager,
            IRepositorioCliente repositorioCliente, IRepositorioResponsable repositorioResponsable)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _repositorioCliente = repositorioCliente;
            _repositorioResponsable = repositorioResponsable;
        }
        [AllowAnonymous]
        public IActionResult Registro()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Registro(RegistroViewModel modelo, bool esResponsable)
        {
            if (!ModelState.IsValid)
            {
                return View(modelo);
            }

            var rol = esResponsable ? "RESPONSABLE" : "CLIENTE";
            var usuario = new Usuario() { Nombre = modelo.Nombre, Rol = rol };

            var resultado = await _userManager.CreateAsync(usuario, password: modelo.Pwd);
            try
            {
                if (resultado.Succeeded)
                {
                    if (esResponsable)
                    {
                        var responsable = new Responsable()
                        {
                            Dni = modelo.Dni,
                            Nombre = modelo.Nombre,
                            Apellido = modelo.Apellido,
                            Telefono = modelo.Telefono,
                            Correo = modelo.Correo,
                            IdUsuario = usuario.Id,
                            Direccion = "-"
                        };
                        await _repositorioResponsable.CrearResponsable(responsable);
                        return View(responsable);
                    }
                    else
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
                        await _repositorioCliente.CrearCliente(cliente);
                        return View(cliente);
                    }
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
            catch (RuntimeBinderException)
            {
                if (esResponsable)
                {
                    return RedirectToAction("ListarClientes", "Cliente");
                }
                else
                {
                    await _signInManager.SignInAsync(usuario, isPersistent: false);
                    return RedirectToAction("Index", "Home");
                }
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
                    return RedirectToAction("ListarClientes", "Cliente");
                }
                else if (roles.Contains("CLIENTE"))
                {
                    return RedirectToAction("Index", "Cuenta");
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
            return RedirectToAction("Index", "Home");
        }

    }
}
