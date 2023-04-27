using ThunderBank.Models;

namespace ThunderBank.Services.Interfaces
{
    public interface IRepositorioCuenta
    {
        Task Crear(Cuenta cuenta);
    }
}
