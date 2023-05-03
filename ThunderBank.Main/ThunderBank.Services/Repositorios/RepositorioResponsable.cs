using Microsoft.Data.SqlClient;
using ThunderBank.Services.Interfaces;
using ThunderBank.Services.SQL;

namespace ThunderBank.Services.Repositorios
{
    public class RepositorioResponsable : IRepositorioResponsable
    {
        private readonly SqlConfiguration _configuration;

        public RepositorioResponsable(SqlConfiguration configuration)
        {
            _configuration = configuration;
        }
        protected SqlConnection DbConnection()
        {
            return new SqlConnection(_configuration.ConnectionString);
        }
        public int ObtenerResponsableId()
        {
            return 4;
        }
    }
}
