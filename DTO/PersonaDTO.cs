using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiTareasNivelB.DTO
{
    public class PersonaDTO:IValidatableObject
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El campo {0} es obigatorio ")]
        [MinLength(2, ErrorMessage = "El campo {0} debe tener almenos {1} caracteres")]
        [MaxLength(50, ErrorMessage = "El campo {0} debe tener maximo {1} caracteres")]
        public string Nombre { get; set; }


        [MinLength(5, ErrorMessage = "El campo {0} debe tener almenos {1} caracteres")]
        [MaxLength(30, ErrorMessage = "El campo {0} debe tener maximo {1} caracteres")]
        public string? Cargo { get; set; }


        public string? CI { get; set; }

        [Required(ErrorMessage = "El campo {0} es obigatorio ")]
        [MinLength(10, ErrorMessage = "El campo {0} debe tener almenos {1} caracteres")]
        [MaxLength(10, ErrorMessage = "El campo {0} debe tener maximo {1} caracteres")]
        public string Telefono { get; set; }

        [EmailAddress(ErrorMessage = "El campo {0} no es valido")]
        public string? Correo { get; set; }

        [NotMapped]
        public Guid IdEmpresa { get; set; } = new Guid();

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (CI.Length != 10)
            {
                yield return
                    new ValidationResult("La cedula debe tener 10 caracteres");
            }

        }
    }
}
