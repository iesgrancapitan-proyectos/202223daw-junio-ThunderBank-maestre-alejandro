using ThunderBank.Models;

namespace ThunderBank.Services.Interfaces
{
    public interface IRepositorioCliente
    {
        Task Crear(Cliente cliente);
        int ObtenerClienteId();
    }
}
