using Dapper;
using Microsoft.Data.SqlClient;
using ThunderBank.Models;
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

        public async Task Crear(Cliente cliente)
        {
            var db = DbConnection();
            var sql = @"INSERT INTO Cliente(dni,nombre,apellido,telefono,correo,direccion,fechaNacimiento,fechaAlta,idUsuario,idResponsable)
                VALUES (@dni,@nombre,@apellido,@telefono,@correo,@direccion,@fechaDeNacimiento,@fechaDeAlta,1,1) SELECT SCOPE_IDENTITY();";
            var id = await db.QuerySingleAsync<int>(sql, cliente);
            cliente.Id = id;
        }

    }
}

