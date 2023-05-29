using ThunderBank.Models;
using ThunderBank.Models.DTO;

namespace ThunderBank.Services.Interfaces
{
    public interface IRepositorioCliente
    {
        Task CrearCliente(Usuario modelo);
        Task<IEnumerable<DtoUsuario>> Listar();
        Task<IEnumerable<DtoUsuario>> ListarPorResponsable(int idResponsable);
        int ObtenerClienteId();
    }
}
