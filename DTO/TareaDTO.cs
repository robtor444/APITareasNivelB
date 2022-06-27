using System.ComponentModel.DataAnnotations;

namespace ApiTareasNivelB.DTO
{
    public class TareaDTO
    {
        public int Id { get; set; }


        [Required(ErrorMessage = "El campo {0} es obigatorio ")]
        [MinLength(4, ErrorMessage = "El campo {0} debe tener almenos {1} caracteres")]
        [MaxLength(50, ErrorMessage = "El campo {0} debe tener maximo {1} caracteres")]
        public string Titulo { get; set; }

        [MaxLength(250, ErrorMessage = "El campo {0} debe tener maximo {1} caracteres")]
        [MinLength(8, ErrorMessage = "El campo {0} debe tener almenos {1} caracteres")]
        public string? Descripcion { get; set; }

        [Required(ErrorMessage = "El campo {0} es obigatorio ")]
        public Boolean Finalizada { get; set; }
    }
}
