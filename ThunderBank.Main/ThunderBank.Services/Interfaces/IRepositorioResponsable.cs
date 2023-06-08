using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThunderBank.Services.Interfaces
{
    public interface IRepositorioResponsable
    {
        Task<int> ObtenerIdResponsanlePorIdUsuario(string id);
        Task<int> ObtenerResponsableId();
    }
}
