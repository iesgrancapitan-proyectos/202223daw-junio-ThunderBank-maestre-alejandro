using ThunderBank.Models;
using ThunderBank.Models.DTO;

namespace ThunderBank.Services.Interfaces
{
    public interface IRepositorioCliente
    {
        Task CrearCliente(Cliente modelo);
        Task Editar(Cliente cliente);
        Task<IEnumerable<DtoUsuario>> Listar();
        Task<IEnumerable<DtoUsuario>> ListarPorResponsable(int idResponsable);
        Task<int> ObtenerClienteId();
        Task<Cliente> ObtenerDatosCliente(string id);
    }
}
