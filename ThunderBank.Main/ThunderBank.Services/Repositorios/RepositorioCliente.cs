using Dapper;
using Microsoft.Data.SqlClient;
using ThunderBank.Models;
using ThunderBank.Models.DTO;
using ThunderBank.Services.Interfaces;
using ThunderBank.Services.SQL;

namespace ThunderBank.Services.Repositorios
{
    public class RepositorioCliente : IRepositorioCliente
    {
        private readonly SqlConfiguration _configuration;

        public RepositorioCliente(SqlConfiguration configuration)
        {
            _configuration = configuration;
        }
        protected SqlConnection DbConnection()
        {
            return new SqlConnection(_configuration.ConnectionString);
        }

        public int ObtenerClienteId()
        {
            return 1002;
        }

        public async Task CrearCliente(Cliente modelo)
        {
            using var db = DbConnection();
            await db.QuerySingle(@"INSERT INTO Cliente(nombre,fechaAlta,idUsuario,activo) VALUES (@Nombre,@FechaDeAlta,@IdUsuario,@Activo) SELECT SCOPE_IDENTITY()", modelo);
        }
        public async Task<IEnumerable<DtoUsuario>> Listar()
        {
            using SqlConnection db = DbConnection();
            IEnumerable<DtoUsuario> listado = await db.QueryAsync<DtoUsuario>(
                @"SELECT Dni,
                nombre,
                apellido,
                telefono,
                correo AS Email,
                direccion,
                fechaNacimiento AS FechaDeNacimiento
                FROM Cliente
                WHERE activo = 1;");
            return listado;
        }

        public async Task<IEnumerable<DtoUsuario>> ListarPorResponsable(int idResponsable)
        {
            using SqlConnection db = DbConnection();
            IEnumerable<DtoUsuario> listado = await db.QueryAsync<DtoUsuario>(
                @"SELECT Dni,
                nombre,
                apellido,
                telefono,
                correo AS Email,
                direccion,
                fechaNacimiento AS FechaDeNacimiento
                FROM Cliente
                WHERE idResponsable = @IdResponsable
                AND activo = 1", new { idResponsable });
            return listado;
        }
    }
}

