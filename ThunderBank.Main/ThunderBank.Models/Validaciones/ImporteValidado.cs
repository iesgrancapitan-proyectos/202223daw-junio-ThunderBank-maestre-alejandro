using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ThunderBank.Models.Validaciones
{
    public class ImporteValidado : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if(value == null)
            {
                return new ValidationResult("Introduzca una cuantía");
            }

            string importe = value.ToString();
            if(!Regex.IsMatch(importe, "^\\d+(\\,\\d+)?$"))
            {
                return new ValidationResult("El importe no es valido.");
            }

            return ValidationResult.Success;

        }
    }
}
