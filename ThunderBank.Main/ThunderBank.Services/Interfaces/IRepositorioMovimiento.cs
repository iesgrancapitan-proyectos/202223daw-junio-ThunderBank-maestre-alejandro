
using ThunderBank.Models;

namespace ThunderBank.Services.Interfaces
{
    public interface IRepositorioMovimiento
    {
        Task Crear(Movimiento movimiento);
    }
}
