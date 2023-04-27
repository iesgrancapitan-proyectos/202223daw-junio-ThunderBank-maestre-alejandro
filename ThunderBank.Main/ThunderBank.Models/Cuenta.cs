
using System.ComponentModel.DataAnnotations;

namespace ThunderBank.Models
{
    public class Cuenta
    {
        public string Numero { get; set; }
        [Required(ErrorMessage = "Con cuanto {0} quiere iniciar su cuenta")]
        public double Saldo { get; set; }
        public DateTime FechaApertura { get; set; }
        public string Tipo { get; set; }
        public int IdCliente { get; set; }
    }
}
