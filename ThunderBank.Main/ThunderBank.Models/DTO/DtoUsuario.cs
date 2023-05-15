using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using ThunderBank.Models.Validaciones;

namespace ThunderBank.Models.DTO
{
    public class DtoUsuario
    {
        public int ResponsableId { get; set; }

        [StringLength(maximumLength: 9, MinimumLength = 9, ErrorMessage = "Introduzca un DNI válido.")]
        [DniCorrecto]
        [Required(ErrorMessage = "Campo obligatorio")]
        [Display(Name = "DNI")]
        public string Dni { get; set; }

        [StringLength(maximumLength: 50, MinimumLength = 1, ErrorMessage = "La longitud del campo debe estar entre {1} y {0}")]
        [Required(ErrorMessage = "Campo obligatorio")]
        [Display(Name = "Nombre")]
        public string Nombre { get; set; }

        [StringLength(maximumLength: 100, MinimumLength = 1, ErrorMessage = "La longitud del campo debe estar entre {1} y {0}")]
        [Required(ErrorMessage = "Campo obligatorio")]
        [Display(Name = "Apellido")]
        public string Apellido { get; set; }

        [Phone(ErrorMessage = "Introduce un número de teléfono válido.")]
        [Required(ErrorMessage = "Campo obligatorio")]
        [Display(Name = "Teléfono")]
        public string Telefono { get; set; }

        [EmailAddress(ErrorMessage = "Introduce un correo electrónico válido")]
        [Required(ErrorMessage = "Campo obligatorio")]
        [Display(Name = "Correo electrónico")]
        public string Email { get; set; }

        [StringLength(maximumLength: 150, MinimumLength = 1, ErrorMessage = "La longitud del campo debe estar entre {1} y {0}")]
        [Required(ErrorMessage = "Campo obligatorio")]
        [Display(Name = "Dirección")]
        public string Direccion { get; set; }

        [DataType(DataType.Date)]
        [Required(ErrorMessage = "Campo obligatorio")]
        [Display(Name = "Fecha de nacimiento")]
        public DateTime FechaDeNacimiento { get; set; }

        public string NombreUsuario { get; set; }

        [DataType(DataType.Password)]
        [Required(ErrorMessage = "La constraseña es obligatoria")]
        [Display(Name = "Contraseña")]
        [Compare("PswComprobada", ErrorMessage = "Las constraseñas no son iguales")]
        public string Psw { get; set; }
        public string PswComprobada { get; set; }
        public DateTime FechaDeAlta { get; set; }

        [Required(ErrorMessage = "Campo obligatorio")]
        [Display(Name = "Tipo de usuario")]
        public RolesUsuario Rol { get; set; }
    }
}

