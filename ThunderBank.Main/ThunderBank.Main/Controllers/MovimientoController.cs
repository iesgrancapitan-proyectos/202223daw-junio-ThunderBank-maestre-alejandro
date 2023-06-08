using ClosedXML.Excel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Data;
using ThunderBank.Models;
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
        
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Crear()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Crear(Movimiento movimiento)
        {
            var cuentaOrigen = await _repositorioCuenta.ObtenerNumeroDeCuenta();
            if (!ModelState.IsValid)
            {
                return View(movimiento);
            }
            movimiento.NumeroCuenta = cuentaOrigen;
            try
            {
                await _repositorioMovimiento.Crear(movimiento);

            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
                return RedirectToAction("ErrorView", "Home");
            }
            return RedirectToAction("ListadoMovimientos");
        }

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

        [HttpGet]
        public async Task<FileResult> ExportarExcel()
        {
            string numeroCuenta = await _repositorioCuenta.ObtenerNumeroDeCuenta();
            IEnumerable<Movimiento> movimientosCuenta = await _repositorioMovimiento.ObtenerMovimientos(numeroCuenta);
            var nombreArchivo = $"Movimientos - {numeroCuenta}.xlsx";
            return GenerarExcel(nombreArchivo, movimientosCuenta);
        }

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
