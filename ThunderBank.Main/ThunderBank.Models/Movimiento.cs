
namespace ThunderBank.Models
{
    public class Movimiento
    {
        public int Id { get; set; }
        public string Tipo { get; set; }
        public double Importe { get; set; }
        public string Comentario { get; set; }
        public string NumeroCuentaRelacionada { get; set; }
        public string NumeroCuenta { get; set; }
    }
}
