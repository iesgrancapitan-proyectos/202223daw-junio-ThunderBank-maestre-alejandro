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
            SqlConnection db = DbConnection();
            await db.QuerySingleAsync(
                @"INSERT INTO Tarjeta(numero, fechaCaducidad, cvc, pin, fechaCreacion, estado, numeroCuenta)
                VALUES(@NumeroDeTarjeta, @FechaDeCaducidad, @Cvc, @Pin, @FechaDeCreacion, @Estado, @NumeroDeCuenta) SELECT SCOPE_IDENTITY();", 
                tarjeta);
        }
    }
}
