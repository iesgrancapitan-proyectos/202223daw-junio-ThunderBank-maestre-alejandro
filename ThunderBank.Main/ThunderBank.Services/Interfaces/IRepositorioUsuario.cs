using ThunderBank.Models;
using ThunderBank.Models.DTO;

namespace ThunderBank.Services.Interfaces
{
    public interface IRepositorioUsuario
    {
        Task<Usuario> BuscarUsuarioPorNombre(string nombre);
        Task<bool> Crear(DtoUsuario usuario);
        Task<int> CrearUsuario(Usuario usuario);
        Task<DtoUsuario> Existe(string usuario);
    }
}
