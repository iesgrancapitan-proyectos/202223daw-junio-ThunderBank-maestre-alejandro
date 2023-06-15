using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThunderBank.Models.Validaciones;

namespace ThunderBank.Models
{
    public class RegistroViewModel
    {
        [DniCorrecto]
        [Required]
        public string Dni { get; set; }
        [Required(ErrorMessage ="El campo {0} es obligatorio")]
        public string Nombre { get; set; }
        public string Apellido { get; set; }

        public string Telefono { get; set; }
        
        public string Correo { get; set; }
        [DisplayName(displayName: "Fecha de Nacimiento")]
        [Required]
        public DateTime FechaDeNacimiento { get; set; }
        [DisplayName(displayName: "Contraseña")]
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public string Pwd { get; set; }
    }
}
