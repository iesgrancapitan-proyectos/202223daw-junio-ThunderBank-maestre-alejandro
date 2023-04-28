using Dapper;
using Microsoft.Data.SqlClient;
using ThunderBank.Models;
using ThunderBank.Services.Interfaces;
using ThunderBank.Services.SQL;

namespace ThunderBank.Services.Repositorios
{
    public class RepositorioTarjeta: IRepositorioTarjeta
    {
        private readonly SqlConfiguration _configuration;

        public RepositorioTarjeta(SqlConfiguration configuration)
        {
            _configuration = configuration;
        }

        protected SqlConnection DbConnection()
        {
            return new SqlConnection(_configuration.ConnectionString);
        }

        public async Task Crear(Tarjeta tarjeta)
        {
            using SqlConnection db = DbConnection();
            await db.QuerySingleAsync(
                @"Tarjeta_Insertar",
                new{
                    FechaDeCreacion = tarjeta.FechaDeCreacion,
                    FechaDeCaducidad = tarjeta.FechaDeCaducidad,
                    Cvc = tarjeta.Cvc,
                    Pin = tarjeta.Pin,
                    Estado = tarjeta.Estado,
                    NumeroDeCuenta = tarjeta.NumeroDeCuenta
                }, commandType: System.Data.CommandType.StoredProcedure);
        }

        public async Task<IEnumerable<Tarjeta>> ObtenerTarjetas(int clienteId)
        {
            using SqlConnection db = DbConnection();
            return await db.QueryAsync<Tarjeta>(
                @"SELECT DISTINCT T.numero AS NumeroDeTarjeta,
                T.fechaCreacion AS FechaDeCreacion,
                T.fechaCaducidad AS FechaDeCaducidad,
                T.cvc, T.pin, T.estado, 
                T.numeroCuenta AS NumeroDeCuenta
                FROM Tarjeta T
                INNER JOIN Cuenta C
                ON C.idCliente = @ClienteId", new {clienteId});
        }
    }
}
