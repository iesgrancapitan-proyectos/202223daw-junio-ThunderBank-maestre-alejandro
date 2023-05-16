using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThunderBank.Models;

namespace ThunderBank.Services.Interfaces
{
    public interface IRepositorioMovimiento
    {
        Task<string> Crear(Movimiento movimiento);
    }
}
