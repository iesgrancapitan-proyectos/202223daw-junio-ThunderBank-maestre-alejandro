using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

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

            var dni = value.ToString().ToUpper();

            if (!System.Text.RegularExpressions.Regex.IsMatch(dni, "^[0-9]{8}[A-Z]$"))
            {
                return new ValidationResult("El número de DNI no es válido.");
            }

            var dniNumero = int.Parse(dni.Substring(0, 8));
            var dniLetra = dni.Substring(8, 1);
            var letras = "TRWAGMYFPDXBNJZSQVHLCKE";
            var letraCalculada = letras[dniNumero % 23];

            if (letraCalculada.ToString() != dniLetra)
            {
                return new ValidationResult("El número de DNI no es válido.");
            }

            return ValidationResult.Success;
        }
    }
}