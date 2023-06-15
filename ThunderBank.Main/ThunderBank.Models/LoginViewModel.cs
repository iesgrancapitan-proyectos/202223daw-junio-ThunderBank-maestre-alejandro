using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace ThunderBank.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public string Nombre { get; set; }
        [DisplayName(displayName: "Contraseña")]
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public string Pwd { get; set; }

        public bool Recuerdame { get; set; }
    }
}
