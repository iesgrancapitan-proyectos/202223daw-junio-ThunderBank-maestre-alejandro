using ThunderBank.Models;
using ThunderBank.Models.DTO;

namespace ThunderBank.Services.Interfaces
{
    public interface IRepositorioCliente
    {
        Task CrearCliente(Cliente modelo);
        Task<IEnumerable<DtoUsuario>> Listar();
        Task<IEnumerable<DtoUsuario>> ListarPorResponsable(int idResponsable);
        Task<int> ObtenerClienteId();
    }
}
