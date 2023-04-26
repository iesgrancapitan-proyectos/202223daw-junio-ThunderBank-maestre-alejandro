using Thunderbank.Models;

namespace ThunderBank.Services.Interfaces
{
    public interface IRepositorioCliente
    {
        Task Crear(Cliente cliente);
    }
}
