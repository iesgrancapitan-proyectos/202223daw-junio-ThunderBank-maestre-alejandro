using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Data.SqlClient;
using System.Security.Claims;
using ThunderBank.Models;
using ThunderBank.Services.Interfaces;
using ThunderBank.Services.SQL;

namespace ThunderBank.Services.Repositorios
{
    public class RepositorioResponsable : IRepositorioResponsable
    {
        private readonly SqlConfiguration _configuration;
        private readonly HttpContext _httpContext;

        public RepositorioResponsable(SqlConfiguration configuration, IHttpContextAccessor httpContextAccessor)
        {
            _configuration = configuration;
            _httpContext = httpContextAccessor.HttpContext;
        }
        protected SqlConnection DbConnection()
        {
            return new SqlConnection(_configuration.ConnectionString);
        }

        public async Task<int> ObtenerResponsableId()
        {
            if (_httpContext.User.Identity.IsAuthenticated)
            {
                var idClaim = _httpContext.User.Claims
                    .Where(x => x.Type == ClaimTypes.NameIdentifier).FirstOrDefault();

                int id = await ObtenerIdResponsanlePorIdUsuario(idClaim.Value);
                return id;
            }
            else
            {
                throw new ApplicationException("El usuario no está autenticado");
            }
        }

        public async Task<int> ObtenerIdResponsanlePorIdUsuario(string id)
        {
            using var db = DbConnection();
            return await db.QuerySingleOrDefaultAsync<int>(@"SELECT * FROM Responsable WHERE idUsuario = @Id", new { id });
        }

        public async Task CrearResponsable(Responsable modelo)
        {
            using var db = DbConnection();
            await db.QuerySingle(@"INSERT INTO Responsable(dni,nombre,apellido,telefono,correo,direccion,idUsuario) 
                VALUES (@Dni,@Nombre,@Apellido,@Telefono,@Correo,@Direccion,@IdUsuario);
                UPDATE Usuario SET rol = 'RESPONSABLE' WHERE id = @IdUsuario
                SELECT SCOPE_IDENTITY()", modelo);
            
        }
    }
}
