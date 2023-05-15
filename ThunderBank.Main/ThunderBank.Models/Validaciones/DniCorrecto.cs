using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace ThunderBank.Models.Validaciones
{
    public class DniCorrecto : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null)
            {
                return new ValidationResult("Introduce un DNI.");
            }

            string dni = value.ToString().ToUpper();

            if (!Regex.IsMatch(dni, "^[0-9]{8}[A-Z]$"))
            {
                return new ValidationResult("El número de DNI no es válido.");
            }

            int dniNumero = int.Parse(dni.Substring(0, 8));
            string dniLetra = dni.Substring(8, 1);
            string letras = "TRWAGMYFPDXBNJZSQVHLCKE";
            char letraCalculada = letras[dniNumero % 23];

            if (letraCalculada.ToString() != dniLetra)
            {
                return new ValidationResult("El número de DNI no es válido.");
            }

            return ValidationResult.Success;
        }
    }
}