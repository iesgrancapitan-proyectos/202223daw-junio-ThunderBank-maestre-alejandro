﻿using Dapper;
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
            }
            else
            {
                throw new ApplicationException("El usuario no está autenticado");
            }
        }

        public async Task<int> ObtenerIdClientePorIdUsuario(string id)
        {
            using var db = DbConnection();
            return await db.QuerySingleOrDefaultAsync<int>(@"SELECT * FROM Cliente WHERE idUsuario = @Id", new { id });
        }

        public async Task CrearCliente(Cliente modelo)
        {
            using var db = DbConnection();
            await db.QuerySingle(@"INSERT INTO Cliente(dni,nombre,apellido,telefono,correo,fechaNacimiento,fechaAlta,idUsuario,activo) VALUES (@Dni,@Nombre,@Apellido,@Telefono,@Correo,@FechaDeNacimiento,@FechaDeAlta,@IdUsuario,@Activo) SELECT SCOPE_IDENTITY()", modelo);
        }
        public async Task<IEnumerable<DtoUsuario>> Listar()
        {
            using SqlConnection db = DbConnection();
            IEnumerable<DtoUsuario> listado = await db.QueryAsync<DtoUsuario>(
                @"SELECT Id As ClienteId,
                Dni,
                nombre,
                apellido,
                telefono,
                correo AS Email,
                direccion,
                fechaNacimiento AS FechaDeNacimiento,
                idResponsable AS ResponsableId
                FROM Cliente
                WHERE activo = 1;");
            return listado;
        }

        public async Task<IEnumerable<DtoUsuario>> ListarPorResponsable(int idResponsable)
        {
            using SqlConnection db = DbConnection();
            IEnumerable<DtoUsuario> listado = await db.QueryAsync<DtoUsuario>(
                @"SELECT Id As ClienteId,
                Dni,
                nombre,
                apellido,
                telefono,
                correo AS Email,
                direccion,
                fechaNacimiento AS FechaDeNacimiento,
                idResponsable AS ResponsableId
                FROM Cliente
                WHERE idResponsable = @IdResponsable
                AND activo = 1", new { idResponsable });
            return listado;
        }
        public async Task<Cliente> ObtenerDatosCliente(string dni)
        {
            using SqlConnection db = DbConnection();
            return await db.QueryFirstOrDefaultAsync<Cliente>(@"SELECT * FROM Cliente WHERE dni = @Dni", new { dni});
        }

        public async Task Editar(Cliente cliente)
        {
            using SqlConnection db = DbConnection();
            await db.ExecuteAsync(
                @"UPDATE Cliente
                SET nombre = @Nombre,
                apellido = @Apellido,
                telefono = @Telefono,
                correo = @Correo,
                direccion = @Direccion
                WHERE idUsuario = @IdUsuario",
                    cliente);
        }

        public async Task Soltar(int idCliente)
        {
            using var db = DbConnection();
            await db.ExecuteAsync(
                @"UPDATE Cliente
                  SET idResponsable = null
                  WHERE id = @idCliente;", new { idCliente });
        }

        public async Task Asignar(int idCliente, int idResponsable)
        {
            using var db = DbConnection();
            await db.ExecuteAsync(
                @"UPDATE Cliente
                  SET idResponsable = @idResponsable
                  WHERE id = @idCliente;", new { idCliente, idResponsable });
        }
    }
}

