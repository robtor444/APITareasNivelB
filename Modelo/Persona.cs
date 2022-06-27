using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiTareasNivelB.Modelo
{
    public class Persona
    {
        public int Id { get; set; }

        [Required(ErrorMessage ="El campo {0} es obigatorio ")]
        
        [MaxLength(50,ErrorMessage = "El campo {0} debe tener maximo {1} caracteres")]
        [MinLength(2, ErrorMessage = "El campo {0} debe tener almenos {1} caracteres")]
        public string Nombre { get; set; }

        
        [MaxLength(30, ErrorMessage = "El campo {0} debe tener maximo {1} caracteres")]
        [MinLength(5, ErrorMessage = "El campo {0} debe tener almenos {1} caracteres")]
        public string? Cargo { get; set; }


        public string? CI { get; set; }

        [Required(ErrorMessage = "El campo {0} es obigatorio ")]
        
        [MaxLength(10, ErrorMessage = "El campo {0} debe tener maximo {1} caracteres")]
        [MinLength(9, ErrorMessage = "El campo {0} debe tener almenos {1} caracteres")]
        public string Telefono { get; set; }

        [EmailAddress(ErrorMessage = "El campo {0} no es valido")]
        public string? Correo{ get; set; }

        [NotMapped]
        public Guid IdEmpresa { get; set; } = new Guid();

        
    }
}
