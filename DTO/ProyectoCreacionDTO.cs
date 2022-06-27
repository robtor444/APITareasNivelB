using System.ComponentModel.DataAnnotations;

namespace ApiTareasNivelB.DTO
{
    public class ProyectoCreacionDTO
    {

        [Required(ErrorMessage = "El campo {0} es obigatorio ")]
        [MinLength(4, ErrorMessage = "El campo {0} debe tener almenos {1} caracteres")]
        [MaxLength(50, ErrorMessage = "El campo {0} debe tener maximo {1} caracteres")]
        public string Nombre { get; set; }

        public DateTime? FechaInicial { get; set; }

        public DateTime? FechaFinal { get; set; }
    }
}
