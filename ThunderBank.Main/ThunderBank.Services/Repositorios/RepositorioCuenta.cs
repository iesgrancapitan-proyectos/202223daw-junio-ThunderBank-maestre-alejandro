using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Data.SqlClient;
using System.Data;
using ThunderBank.Models;
using ThunderBank.Services.Interfaces;
using ThunderBank.Services.SQL;

namespace ThunderBank.Services.Repositorios
{
    public class RepositorioCuenta : IRepositorioCuenta
    {
        private readonly SqlConfiguration _configuration;
        private readonly IRepositorioCliente _repositorioCliente;
        private readonly HttpContext _httpContext;

        public RepositorioCuenta(SqlConfiguration configuration, IHttpContextAccessor httpContextAccessor,IRepositorioCliente repositorioCliente)
        {
            _httpContext = httpContextAccessor.HttpContext;
            _configuration = configuration;
            this._repositorioCliente = repositorioCliente;
        }
        protected SqlConnection DbConnection()
        {
            return new SqlConnection(_configuration.ConnectionString);
        }


        public async Task<string> ObtenerNumeroDeCuenta()
        {
            var clienteId = await _repositorioCliente.ObtenerClienteId();
            var ultimaCuenta = await ObtenerCuentasPorIdCliente(clienteId);
            ultimaCuenta = ultimaCuenta.Where(x => x.Activa == true);
            string ultimoNumero = ultimaCuenta.Select(x => (string)x.GetType().GetProperty("Numero").GetValue(x, null)).Last();
            //string ultimoNumero = await ObtenerUltimaCuentaCliente(clienteId.ToString());
            return ultimoNumero;
        }

        public async Task<IEnumerable<Cuenta>> ObtenerCuentasPorIdCliente(int idCliente)
        {
            using var db = DbConnection();
            var cuentas = await db.QueryAsync<Cuenta>(@"SELECT * FROM Cuenta WHERE idCliente = @IdCliente", new { IdCliente = idCliente });
            return cuentas;
        }

        public async Task<string> ObtenerUltimaCuentaCliente(string id)
        {
            using var db = DbConnection();
            return await db.QueryFirstOrDefaultAsync<string>(@"SELECT * FROM Cuenta WHERE idCliente = @Id ORDER BY idCliente numero DESC LIMIT 1", new { id });
        }

        public async Task Crear(Cuenta cuenta,int idCliente)
        {
            using var db = DbConnection();
            cuenta.IdCliente = idCliente;
            var numero = await db.QuerySingleAsync<string>(@"Cuenta_Insertar", new
            {
                cuenta.Saldo,
                cuenta.FechaApertura,
                cuenta.Tipo,
                cuenta.IdCliente,
            }, commandType: CommandType.StoredProcedure);
            cuenta.Numero = numero;
        }

        public async Task<IEnumerable<Cuenta>> Buscar(int idCliente)
        {
            using var db = DbConnection();
            return await db.QueryAsync<Cuenta>(
                @"SELECT numero,saldo,fechaApertura,tipo,activa
                FROM Cuenta 
                WHERE idCliente = @idCliente;", new {idCliente});
        }

        public async Task Desactivar(string numCuenta)
        {
            using var db = DbConnection();
            await db.ExecuteAsync(
                @"UPDATE Cuenta
                  SET activa = 0
                  WHERE numero = @numCuenta;", new { numCuenta });
        }
    }
}
