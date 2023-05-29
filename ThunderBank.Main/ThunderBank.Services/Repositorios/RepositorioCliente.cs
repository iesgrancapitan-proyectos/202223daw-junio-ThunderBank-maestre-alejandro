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
        private readonly IServicioUsuario _servicioUsuario;

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

        public async Task CrearCliente(Usuario modelo)
        {
            using var db = DbConnection();
            var actualDate = DateTime.Now.ToString("yyyy,MM,dd");
            await db.QuerySingle(@"INSERT INTO Cliente(dni,nombre,fechaAlta,idUsuario) VALUES (@Dni,@Nombre,@actualDate,@idUsuario)", modelo);
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

