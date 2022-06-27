using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace ApiTareasNivelB.Validaciones
{
    public class SoloNumerosAttribute:ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value,ValidationContext validationContext)
        {
            if (value==null)
            {
                return ValidationResult.Success;
            }

            var regex = "^[0-9\b]+$";
            Match match =Regex.Match(value.ToString(), regex, RegexOptions.IgnoreCase);

            if (match.Success)
            {
                return ValidationResult.Success;
            }
            else
            {
                return new ValidationResult($"El campo {validationContext.DisplayName} solo acepta numeros");
            }

        }

    }
}
