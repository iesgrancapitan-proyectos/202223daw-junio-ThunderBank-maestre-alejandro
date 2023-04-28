using Dapper;
using Microsoft.Data.SqlClient;
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
        public async Task Crear(Cuenta cuenta)
        {
            var db = DbConnection();
            var sql = @"INSERT INTO Cuenta(numero,saldo,fechaApertura,tipo,idCliente)
                VALUES (@Numero,@Saldo,@FechaApertura,@Tipo,1002) SELECT SCOPE_IDENTITY();";
            await db.QuerySingleAsync(sql, cuenta);
        }
    }
}
