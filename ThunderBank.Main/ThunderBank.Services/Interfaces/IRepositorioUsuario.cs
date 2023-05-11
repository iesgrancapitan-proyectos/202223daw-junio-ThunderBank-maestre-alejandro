using ThunderBank.Models.DTO;

namespace ThunderBank.Services.Interfaces
{
    public interface IRepositorioUsuario
    {
        Task<bool> Crear(DtoUsuario usuario);
        Task<DtoUsuario> Existe(string usuario);
    }
}
