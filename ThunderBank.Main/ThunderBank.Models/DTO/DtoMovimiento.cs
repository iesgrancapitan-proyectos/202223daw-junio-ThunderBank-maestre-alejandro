using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;
using ThunderBank.Models.Validaciones;

namespace ThunderBank.Models.DTO
{
    //TODO
    public class DtoMovimiento
    {
        public int Id { get; set; }
        public string Tipo { get; set; }

        [ImporteValidado]
        [Required]
        public double Importe { get; set; }
        public string Comentario { get; set; }
        [Display(Name = "Numero de cuenta de destino")]
        [Required]
        public string NumeroCuentaRelacionada { get; set; } // ESTE ES EL NUMERO DE LA OTRA CUENTA
        [Display(Name = "Numero de Cuenta")]
        public IEnumerable<Cuenta> Cuentas { get; set;}
        public string NumeroCuenta { get; set; }
    }
}
