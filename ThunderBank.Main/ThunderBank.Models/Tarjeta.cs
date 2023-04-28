

namespace ThunderBank.Models
{
    public class Tarjeta
    {
        public string NumeroDeTarjeta { get; set; }
        public DateTime FechaDeCreacion { get; set; }
        public DateTime FechaDeCaducidad { get; set; }
        public string Cvc { get; set; } = GenerarCódigo(3);
        public string Pin { get; set; } = GenerarCódigo(4);

        public string Estado { get; set; } = "ACTIVA";
        public string NumeroDeCuenta { get; set; }

        // Método para generar códigos en string
        private static string GenerarCódigo(int longitud)
        {
            Random random = new();
            string codigo = string.Empty;
            while (codigo.Length < longitud)
            {
                int randomNumber = random.Next(10);
                codigo += randomNumber;
            }
            return codigo;
        }
    }
}
