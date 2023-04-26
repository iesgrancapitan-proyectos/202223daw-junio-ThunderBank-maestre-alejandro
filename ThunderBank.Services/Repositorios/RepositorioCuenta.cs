using Microsoft.Data.SqlClient;
using ThunderBank.Services.Sql;

namespace ThunderBank.Services.Repositorios
{
    public class RepositorioCuenta
    {
        private readonly SqlConfiguration _configuration;

        public RepositorioCuenta(SqlConfiguration configuration)
        {
            _configuration = configuration;
        }
        protected SqlConnection dbConnection()
        {
            return new SqlConnection(_configuration.ConnectionString);
        }

    }
}