using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Thunderbank.Models.DTO
{
    public class DtoCliente
    {
        public string Dni { get; set; } //SERIA INTERESANTE HACER EL CALCULO DE LA LETRA
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Telefono { get; set; }
        public string Email { get; set; }
        public string Direccion { get; set; }
        public DateTime FechaDeNacimiento { get; set; }
    }
}
