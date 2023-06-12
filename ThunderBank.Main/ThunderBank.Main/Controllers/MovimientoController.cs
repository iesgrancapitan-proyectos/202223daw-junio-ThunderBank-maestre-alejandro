using ClosedXML.Excel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Data;
using ThunderBank.Models;
using ThunderBank.Models.DTO;
using ThunderBank.Services.Interfaces;
using ThunderBank.Services.Repositorios;

namespace ThunderBank.Main.Controllers
{
    public class MovimientoController: Controller
    {
        private readonly IRepositorioMovimiento _repositorioMovimiento;
        private readonly IRepositorioCuenta _repositorioCuenta;
        private readonly IRepositorioCliente _repositorioCliente;

        public MovimientoController(IRepositorioMovimiento repositorioMovimiento,IRepositorioCuenta repositorioCuenta,IRepositorioCliente repositorioCliente)
        {
            this._repositorioMovimiento = repositorioMovimiento;
            this._repositorioCuenta = repositorioCuenta;
            this._repositorioCliente = repositorioCliente;
        }
        [Authorize(Roles = "CLIENTE")]
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Crear()
        {
            try
            {
                var clienteId = await _repositorioCliente.ObtenerClienteId();
                DtoMovimiento model = new()
                {
                    Cuentas = await _repositorioCuenta.ObtenerCuentasPorIdCliente(clienteId)
                };
                return View(model);
            }catch(Exception ex)
            {
                TempData["Error"] = ex.Message;
                return RedirectToAction("ErrorView", "Home");
            }
        }

        [Authorize(Roles = "CLIENTE")]
        [HttpPost]
        public async Task<IActionResult> Crear(Movimiento movimiento)
        {
            if (!ModelState.IsValid)
            {
                return View(movimiento);
            }
            try
            {
                var cuentaOrigen = await _repositorioCuenta.ObtenerNumeroDeCuenta();
                movimiento.NumeroCuenta = cuentaOrigen;
                await _repositorioMovimiento.Crear(movimiento);

            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
                return RedirectToAction("ErrorView", "Home");
            }
            return RedirectToAction("ListadoMovimientos");
        }
        [Authorize(Roles = "CLIENTE")]
        public async Task<IActionResult> ListadoMovimientos()
        {
            var idCliente = await _repositorioCliente.ObtenerClienteId();
            var cuentas = await _repositorioCuenta.ObtenerCuentasPorIdCliente(idCliente);
            List<Movimiento> movimientosTotales = new();
            foreach (var cuenta in cuentas)
            {
                var movimientosCuentaActual = await _repositorioMovimiento.ObtenerMovimientos(cuenta.Numero);
                movimientosTotales.AddRange(movimientosCuentaActual);
            }
            return View(movimientosTotales);
        }
        [Authorize(Roles = "CLIENTE")]
        [HttpGet]
        public async Task<FileResult> ExportarExcel(IEnumerable<Movimiento> movimientos)
        {
            var idCliente = await _repositorioCliente.ObtenerClienteId();
            var cuentas = await _repositorioCuenta.ObtenerCuentasPorIdCliente(idCliente);
            List<Movimiento> movimientosTotales = new();
            foreach(var cuenta in cuentas)
            {
                var movimientosCuentaActual = await _repositorioMovimiento.ObtenerMovimientos(cuenta.Numero);
                movimientosTotales.AddRange(movimientosCuentaActual);
            }
            //string numeroCuenta = await _repositorioCuenta.ObtenerNumeroDeCuenta();
            //IEnumerable<Movimiento> movimientosCuenta = await _repositorioMovimiento.ObtenerMovimientos(numeroCuenta);
            var nombreArchivo = $"Movimientos - {idCliente}.xlsx";
            return GenerarExcel(nombreArchivo, movimientosTotales);
        }
        [Authorize(Roles = "CLIENTE")]
        private FileResult GenerarExcel(string numeroCuenta, IEnumerable<Movimiento> movimientos)
        {
            DataTable dataTable = new("Transacciones");
            dataTable.Columns.AddRange(new DataColumn[]
            {
                new DataColumn("ID"),
                new DataColumn("TIPO"),
                new DataColumn("IMPORTE"),
                new DataColumn("COMENTARIO"),
                new DataColumn("DESTINO"),
                new DataColumn("TU CUENTA")
            });

            foreach (var item in movimientos)
            {
                dataTable.Rows.Add(item.Id,
                    item.Tipo,
                    item.Importe,
                    item.Comentario,
                    item.NumeroCuentaRelacionada,
                    item.NumeroCuenta);
            }
            using XLWorkbook wb = new();
            wb.Worksheets.Add(dataTable);
            using MemoryStream stream = new();
            wb.SaveAs(stream);
            return File(stream.ToArray(),
                "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                numeroCuenta);
        }

        [Authorize(Roles = "CLIENTE")]
        public IActionResult Error()
        {
            if (TempData.ContainsKey("ErrorMessage"))
            {
                string mensajeError = TempData["ErrorMessage"].ToString();
                ViewBag.Error = mensajeError;
            }
            return View();
        }
    }
}
