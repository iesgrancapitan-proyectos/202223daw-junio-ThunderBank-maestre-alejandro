using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThunderBank.Models
{
    public class CuentaIndex
    {
        public IEnumerable<Cuenta> Cuentas { get; set; }
    }
}
