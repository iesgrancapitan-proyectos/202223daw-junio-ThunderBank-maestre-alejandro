using ThunderBank.Models;

namespace ThunderBank.Services.Interfaces
{
    public interface IRepositorioCuenta
    {
        Task<IEnumerable<Cuenta>> Buscar(int idCliente);
        Task Crear(Cuenta cuenta,int idCliente);
        string ObtenerNumeroDeCuenta();
    }
}
