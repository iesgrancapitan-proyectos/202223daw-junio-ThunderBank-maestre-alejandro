using ThunderBank.Models;

namespace ThunderBank.Services.Interfaces
{
    public interface IRepositorioCuenta
    {
        Task<IEnumerable<Cuenta>> Buscar(int idCliente);
        Task Crear(Cuenta cuenta,int idCliente);
        Task<IEnumerable<Cuenta>> ObtenerCuentasPorIdCliente(int idCliente);
        Task<string> ObtenerNumeroDeCuenta();
        Task<string> ObtenerUltimaCuentaCliente(string id);
    }
}
