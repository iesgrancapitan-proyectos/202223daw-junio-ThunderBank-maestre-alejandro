﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;
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
        [Authorize(Roles = "CLIENTE")]
        public async Task<IActionResult> Index()
        {
            int clienteId = await _repositorioCliente.ObtenerClienteId();
            IEnumerable<Tarjeta> tarjetasCliente = await _repositorioTarjeta.ObtenerTarjetas(clienteId);
            return View(tarjetasCliente);
        }

        /*
         CREAR TARJETA
         */
        [Authorize(Roles = "CLIENTE")]
        [HttpGet]
        public async Task<IActionResult> Crear()
        {
            try
            {
                var clienteId = await _repositorioCliente.ObtenerClienteId();
                DTOCrearTarjeta model = new()
                {
                    Cuentas = await _repositorioCuenta.ObtenerCuentasPorIdCliente(clienteId)
                };
                return View(model);
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
                return RedirectToAction("ErrorView", "Home");
            }
        }
        [Authorize(Roles = "CLIENTE")]
        [HttpPost]
        public async Task<IActionResult> Crear(DTOCrearTarjeta model)
        {
            try
            {
                await _repositorioTarjeta.Crear(model);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
                return RedirectToAction("ErrorView", "Home");
            }
        }

        /*
         INFO TARJETA
         */
        [Authorize(Roles = "CLIENTE")]
        [HttpGet]
        public async Task<IActionResult> Info(string numero)
        {
            DtoTarjeta model = await _repositorioTarjeta.ObtenerDatosTarjeta(numero);
            return View(model);
        }
        public async Task<RedirectToActionResult> Borrar(string numero)
        {
            _repositorioTarjeta.Borrar(numero);
            return RedirectToAction("Index");
        }

        /*
         ACCIONES TARJETA
         */
        [Authorize(Roles = "CLIENTE")]
        [HttpGet]
        public async Task<RedirectToActionResult> CongelarTarjeta(string numero)
        {
            await _repositorioTarjeta.CongelarTarjeta(numero);
            return RedirectToAction("Index", "Tarjeta");
        }

        [Authorize(Roles = "CLIENTE")]
        [HttpGet]
        public async Task<RedirectToActionResult> ActivarTarjeta(string numero)
        {
            await _repositorioTarjeta.ActivarTarjeta(numero);
            return RedirectToAction("Index", "Tarjeta");
        }

        [Authorize(Roles = "CLIENTE")]
        [HttpGet]
        public async Task<RedirectToActionResult> CancelarTarjeta(string numero)
        {
            await _repositorioTarjeta.CancelarTarjeta(numero);
            return RedirectToAction("Index", "Tarjeta");
        }

    }
}
