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
        [Required(ErrorMessage ="El campo {0} es obligatorio")]
        public string Nombre { get; set; }
        [DisplayName(displayName: "Contraseña")]
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public string Pwd { get; set; }
        [DniCorrecto]
        public string Dni { get; set; }
    }
}
