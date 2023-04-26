using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Thunderbank.Models
{
    public class Cuenta
    {
        public string Numero => GenerarAleatorio();
        public Decimal Saldo { get; set; }
        public DateTime FechaApertura { get; set; }
        public string Tipo { get; set; }
        public int IdCliente { get; set; }
        public string TipoCuenta { get; set; }

        //Cada vez que se cree una cuenta queremos que se genere un numero aleatorio
        //Esto hay que mejorarlo ya que puede crear el mismo numero de cuenta varias veces
        private static string GenerarAleatorio()
        {
            var rnd = new Random();

        string numero = rnd.Next(1, 10).ToString();
            for (int i = 0; i <= 8; i++)
            {
                numero += rnd.Next(100, 1000).ToString();
    }
            return numero;
        }
    }
}
