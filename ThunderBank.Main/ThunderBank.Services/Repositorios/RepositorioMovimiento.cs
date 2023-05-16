using Dapper;
using Microsoft.Data.SqlClient;
using System.Data;
using ThunderBank.Models;
using ThunderBank.Services.Interfaces;
using ThunderBank.Services.SQL;

namespace ThunderBank.Services.Repositorios
{
    public class RepositorioMovimiento : IRepositorioMovimiento
    {
        private readonly SqlConfiguration _configuration;

        public RepositorioMovimiento(SqlConfiguration configuration)
        {
            _configuration = configuration;
        }
        protected SqlConnection DbConnection()
        {
            return new SqlConnection(_configuration.ConnectionString);
        }

        public async Task Crear(Movimiento movimiento)
        {
            using SqlConnection db = DbConnection();
            await db.ExecuteAsync(
                @"Movimiento_Insertar",
                new
                {
                    movimiento.Tipo,
                    movimiento.Importe,
                    movimiento.Comentario,
                    movimiento.NumeroCuentaRelacionada,
                    movimiento.NumeroCuenta
                }, commandType: CommandType.StoredProcedure);

        }
    }
}
