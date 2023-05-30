using ThunderBank.Models;
using ThunderBank.Models.DTO;

namespace ThunderBank.Services.Interfaces
{
    public interface IRepositorioTarjeta
    {
        Task ActivarTarjeta(string numeroTarjeta);
        Task CancelarTarjeta(string numeroTarjeta);
        Task CongelarTarjeta(string numeroTarjeta);
        Task Crear(Tarjeta tarjeta);
        Task<DtoTarjeta> ObtenerDatosTarjeta(string numeroTarjeta);
        Task<IEnumerable<Tarjeta>> ObtenerTarjetas(int clienteId);
        Task<IEnumerable<Tarjeta>> ObtenerTarjetasPorNumCuenta(string numCuenta);
    }
}
