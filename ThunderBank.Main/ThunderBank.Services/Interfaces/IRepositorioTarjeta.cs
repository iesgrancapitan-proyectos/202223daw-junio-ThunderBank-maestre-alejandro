using ThunderBank.Models;

namespace ThunderBank.Services.Interfaces
{
    public interface IRepositorioTarjeta
    {
        Task Crear(Tarjeta tarjeta);
        Task<IEnumerable<Tarjeta>> ObtenerTarjetas(int clienteId);
    }
}
