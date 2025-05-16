using System.ComponentModel.DataAnnotations;

namespace RegistroDeTecnicos.Components.Model
{
    public class Tecnicos
    {
        [Key]
        public int TecnicoId { get; set; }

        [Required(ErrorMessage ="Este campo es obligatorio")] 
        public string NombreTecnico { get; set; } = null!;
        [Range(0.1, double.MaxValue, ErrorMessage = "Este campo es obligatorio y debe ser mayor que 0")]
        public double SueldoHora { get; set; }

    }
}
