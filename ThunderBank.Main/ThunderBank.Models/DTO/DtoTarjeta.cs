using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThunderBank.Models.DTO
{
    public class DtoTarjeta
    {
        public string NumeroDeTarjeta { get; set; }
        public DateTime FechaDeCreacion { get; set; }
        public DateTime FechaDeCaducidad { get; set; }
        public string Cvc { get; set; }
        public string Pin { get; set; }
        public string Estado { get; set; }
        public string NumeroDeCuenta { get; set; }
        public string Titular { get; set; }
        public double SaldoCuenta { get; set; }
        public string Tipo { get; set; }
    }
}
