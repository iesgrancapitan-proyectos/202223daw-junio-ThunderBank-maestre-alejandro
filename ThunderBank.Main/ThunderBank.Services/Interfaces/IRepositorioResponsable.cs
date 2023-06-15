using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThunderBank.Models;

namespace ThunderBank.Services.Interfaces
{
    public interface IRepositorioResponsable
    {
        Task CrearResponsable(Responsable modelo);
        Task<int> ObtenerIdResponsanlePorIdUsuario(string id);
        Task<int> ObtenerResponsableId();
    }
}
