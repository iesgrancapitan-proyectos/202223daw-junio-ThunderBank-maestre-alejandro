
using System.ComponentModel.DataAnnotations;

namespace ThunderBank.Models
{
    public class Cuenta
    {
        public string Numero { get; set; }
        [Required(ErrorMessage = "Con cuanto {0} quiere iniciar su cuenta")]
        public double Saldo { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}")]
        public DateTime FechaApertura { get; set; }
        public string Tipo { get; set; }
        public bool Activa { get; set; }
        public int IdCliente { get; set; }
    }
    
}
