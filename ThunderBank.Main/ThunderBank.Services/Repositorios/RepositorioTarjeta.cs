using Dapper;
using Microsoft.Data.SqlClient;
using ThunderBank.Models;
using ThunderBank.Models.DTO;
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

        public async Task<IEnumerable<Tarjeta>> ObtenerTarjetas(int clienteId,string fkNum)
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
                ON C.idCliente = @ClienteId WHERE T.numeroCuenta = @FkNum  ", new {clienteId,fkNum});
        }
        public async Task<IEnumerable<Tarjeta>> ObtenerTarjetasPorNumCuenta(string numCuenta)
        {
            using SqlConnection db = DbConnection();
            return await db.QueryAsync<Tarjeta>(@"SELECT * FROM Tarjeta WHERE numero = @NumCuenta", new { numCuenta});
        }

        public async Task<DtoTarjeta> ObtenerDatosTarjeta(string numeroTarjeta)
        {
            using SqlConnection db = DbConnection();
            return await db.QueryFirstOrDefaultAsync<DtoTarjeta>(
                @"SELECT 
                T.numero AS NumeroDeTarjeta,
                T.fechaCreacion AS FechaDeCreacion,
                T.fechaCaducidad AS FechaDeCaducidad,
                T.cvc AS Cvc,
                T.pin AS Pin,
                T.estado AS Estado,
                C.numero AS NumeroDeCuenta,
                CONCAT(Cl.nombre, ' ', Cl.apellido) AS Titular,
                C.saldo AS SaldoCuenta,
                C.tipo AS Tipo
                FROM Tarjeta T
                INNER JOIN Cuenta C
                ON T.numeroCuenta = C.numero
                INNER JOIN Cliente Cl
                ON C.idCliente = Cl.id
                WHERE T.numero = @numeroTarjeta",
                new { numeroTarjeta });
        }

        public async Task CongelarTarjeta(string numeroTarjeta)
        {
            using SqlConnection db = DbConnection();
            await db.QueryAsync(
                @"UPDATE Tarjeta 
                SET estado = 'CONGELADA' 
                WHERE numero = @numeroTarjeta
                SELECT SCOPE_IDENTITY();",
                new {numeroTarjeta});
        }

        public async Task ActivarTarjeta(string numeroTarjeta)
        {
            using SqlConnection db = DbConnection();
            await db.QueryAsync(
                @"UPDATE Tarjeta 
                SET estado = 'ACTIVA' 
                WHERE numero = @numeroTarjeta
                SELECT SCOPE_IDENTITY();",
                new { numeroTarjeta });
        }

        public async Task CancelarTarjeta(string numeroTarjeta)
        {
            using SqlConnection db = DbConnection();
            await db.QueryAsync(
                @"UPDATE Tarjeta 
                SET estado = 'CANCELADA' 
                WHERE numero = @numeroTarjeta
                SELECT SCOPE_IDENTITY();",
                new { numeroTarjeta });
        }
    }
}
