using Dapper;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThunderBank.Models;
using ThunderBank.Services.Interfaces;
using ThunderBank.Services.SQL;

namespace ThunderBank.Services.Repositorios
{
    public class RepositorioMovimiento: IRepositorioMovimiento
    {
        private readonly SqlConfiguration _configuration;

        public RepositorioMovimiento(SqlConfiguration configuration)
        {
            _configuration = configuration;
        }
        protected SqlConnection DbConnection()
        {
            return new SqlConnection(_configuration.ConnectionString);
        }

        public async Task<string> Crear(Movimiento movimiento)
        {
            using var db = DbConnection();
            return await db.QuerySingleAsync<string>(@"Movimiento_Crear", new
            {
                movimiento.Tipo,
                movimiento.Importe,
                movimiento.Comentario,
                movimiento.NumeroCuentaRelacionada,
                movimiento.NumeroCuenta
            }, commandType: CommandType.StoredProcedure);
        }
    }
}
