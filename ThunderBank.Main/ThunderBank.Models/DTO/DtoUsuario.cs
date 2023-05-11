using System.ComponentModel.DataAnnotations;
using ThunderBank.Models.Validaciones;

namespace ThunderBank.Models.DTO
{
    public class DtoUsuario
    {
        public int ResponsableId { get; set; }

        [StringLength(maximumLength: 9, MinimumLength = 9, ErrorMessage = "Introduzca un DNI válido.")]
        [DniCorrecto]
        [Required]
        [Display(Name = "DNI")]
        public string Dni { get; set; }

        [StringLength(maximumLength: 50, MinimumLength = 1, ErrorMessage = "La longitud del campo debe estar entre {1} y {0}")]
        [Required]
        [Display(Name = "Nombre")]
        public string Nombre { get; set; }

        [StringLength(maximumLength: 100, MinimumLength = 1, ErrorMessage = "La longitud del campo debe estar entre {1} y {0}")]
        [Required]
        [Display(Name = "Apellido")]
        public string Apellido { get; set; }

        [Phone(ErrorMessage = "Introduce un número de teléfono válido.")]
        [Required]
        [Display(Name = "Teléfono")]
        public string Telefono { get; set; }

        [EmailAddress(ErrorMessage = "Introduce un correo electrónico válido")]
        [Required]
        [Display(Name = "Correo electrónico")]
        public string Email { get; set; }

        [StringLength(maximumLength: 150, MinimumLength = 1, ErrorMessage = "La longitud del campo debe estar entre {1} y {0}")]
        [Required]
        [Display(Name = "Dirección")]
        public string Direccion { get; set; }

        [DataType(DataType.Date)]
        [Required]
        [Display(Name = "Fecha de nacimiento")]
        public DateTime FechaDeNacimiento { get; set; }

        public string NombreUsuario { get; set; }

        [DataType(DataType.Password)]
        [Required]
        [Display(Name = "Contraseña")]
        public string Psw { get; set; }
        public DateTime FechaDeAlta { get; set; }

        [Required]
        [Display(Name = "Tipo de usuario")]
        public RolesUsuario Rol { get; set; }
    }
}

