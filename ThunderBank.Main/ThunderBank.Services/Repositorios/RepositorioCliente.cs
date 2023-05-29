using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Data.SqlClient;
using System.Security.Claims;
using ThunderBank.Models;
using ThunderBank.Models.DTO;
using ThunderBank.Services.Interfaces;
using ThunderBank.Services.SQL;

namespace ThunderBank.Services.Repositorios
{
    public class RepositorioCliente : IRepositorioCliente
    {
        private readonly SqlConfiguration _configuration;
        private readonly HttpContext _httpContext;

        public RepositorioCliente(SqlConfiguration configuration, IHttpContextAccessor httpContextAccessor)
        {
            _httpContext = httpContextAccessor.HttpContext;
            _configuration = configuration;
        }

        protected SqlConnection DbConnection()
        {
            return new SqlConnection(_configuration.ConnectionString);
        }

        public async Task<int> ObtenerClienteId()
        {
            if (_httpContext.User.Identity.IsAuthenticated)
            {
                var idClaim = _httpContext.User.Claims
                    .Where(x => x.Type == ClaimTypes.NameIdentifier).FirstOrDefault();

                int id = await ObtenerIdClientePorIdUsuario(idClaim.Value);
                return id;

                //var id = int.Parse(idClaim.Value);
                //return id;
            }
            else
            {
                throw new ApplicationException("El usuario no está autenticado");
            }
            //return 1002;
        }

        public async Task<int> ObtenerIdClientePorIdUsuario(string id)
        {
            using var db = DbConnection();
            return await db.QuerySingleOrDefaultAsync<int>(@"SELECT * FROM Cliente WHERE idUsuario = @Id", new { id });
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

