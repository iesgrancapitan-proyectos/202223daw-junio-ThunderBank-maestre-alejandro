using System.ComponentModel.DataAnnotations;

namespace ThunderBank.Models
{
    public class Cliente
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [StringLength(9, MinimumLength = 1)]
        public string Dni { get; set; } //SERIA INTERESANTE HACER EL CALCULO DE LA LETRA
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Telefono { get; set; }
        public string Correo { get; set; }
        public string Direccion { get; set; }
        [Display(Name ="Fecha de Nacimiento")]
        public DateTime FechaDeNacimiento { get; set; }
        public DateTime FechaDeAlta { get; set; }
        public int IdUsuario { get; set; }
        public int? IdResponsable { get; set; }
        public int Activo { get; set; }
    }
}