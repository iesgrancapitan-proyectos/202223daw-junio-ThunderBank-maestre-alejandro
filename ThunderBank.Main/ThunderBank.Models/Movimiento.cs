using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThunderBank.Models.Validaciones;

namespace ThunderBank.Models
{
    public class Movimiento
    {
        public int Id { get; set; }
        public string Tipo { get; set; }

        [ImporteValidado]
        public double Importe { get; set; }
        public string Comentario { get; set; }
        [Display(Name ="Numero de cuenta de destino")]
        public string NumeroCuentaRelacionada { get; set; } // ESTE ES EL NUMERO DE LA OTRA CUENTA
        [Display(Name="Numero de Cuenta")]
        public string NumeroCuenta { get; set; }
    }
}
