using Dapper;
using Microsoft.Data.SqlClient;
using ThunderBank.Models;
using ThunderBank.Models.DTO;
using ThunderBank.Services.Interfaces;
using ThunderBank.Services.SQL;

namespace ThunderBank.Services.Repositorios
{
    public class RepositorioUsuario: IRepositorioUsuario
    {

        private readonly SqlConfiguration _configuration;

        public RepositorioUsuario(SqlConfiguration configuration)
        {
            _configuration = configuration;
        }

        protected SqlConnection DbConnection()
        {
            return new SqlConnection(_configuration.ConnectionString);
        }

        public async Task<int> CrearUsuario(Usuario usuario)
        {
            using var db = DbConnection();
            usuario.Rol = "CLIENTE";
            var id = await db.QuerySingleAsync<int>(@"INSERT INTO Usuario(nombre,pwd,rol) VALUES (@Nombre,@Pwd,@Rol) SELECT SCOPE_IDENTITY()",usuario);
            return id;
        }

        public async Task<Usuario> BuscarUsuarioPorNombre(string nombre)
        {
            using var db = DbConnection();
            return await db.QuerySingleOrDefaultAsync<Usuario>(@"SELECT * FROM Usuario WHERE nombre = @Nombre", new { nombre });
        }

        public async Task<bool> Crear(DtoUsuario usuario)
        {
            var db = DbConnection();
            string rol = usuario.Rol.ToString();                // Se asigna una variable con el rol en tipo String para que lo pueda leer la base de datos
            return await db.QuerySingleAsync<bool>("Usuario_Insertar",
                new
                {
                    usuario.NombreUsuario,
                    usuario.Psw,
                    rol,
                    usuario.Dni,
                    usuario.Nombre,
                    usuario.Apellido,
                    usuario.Telefono,
                    usuario.Email,
                    usuario.Direccion,
                    usuario.FechaDeNacimiento,
                    usuario.FechaDeAlta,
                    usuario.ResponsableId
                },
                commandType: System.Data.CommandType.StoredProcedure);
        }

        public async Task<DtoUsuario> Existe(string usuario)
        {
            var db = DbConnection();
            return await db.QueryFirstOrDefaultAsync<DtoUsuario>(@"SELECT nombre FROM Usuario WHERE nombre = @usuario",
                new { usuario });
        }
    }
}
