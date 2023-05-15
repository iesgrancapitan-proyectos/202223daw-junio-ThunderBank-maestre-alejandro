using Dapper;
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

        public RepositorioCuenta(SqlConfiguration configuration)
        {
            _configuration = configuration;
        }
        protected SqlConnection DbConnection()
        {
            return new SqlConnection(_configuration.ConnectionString);
        }
        public string ObtenerNumeroDeCuenta()
        {
            return "5772281704013526134306522";
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
                @"SELECT numero,saldo,fechaApertura,tipo
                FROM Cuenta 
                WHERE idCliente = @idCliente;", new {idCliente});
        }
    }
}
