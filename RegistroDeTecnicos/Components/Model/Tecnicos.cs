using System.ComponentModel.DataAnnotations;

namespace RegistroDeTecnicos.Components.Model
{
    public class Tecnicos
    {
        [Key]
        public int TecnicoId { get; set; }

        // El nombre no debe contener números y es requerido
        [Required(ErrorMessage = "Debe ingresar el nombre del técnico")]
        [RegularExpression(@"^[A-Za-z\s]+$", ErrorMessage = "El nombre no puede contener números")]
        public string NombreTecnico { get; set; } = string.Empty;

        // El sueldo por hora debe ser mayor que 0 y puede tener hasta 2 decimales
        [Range(0.1, 50000, ErrorMessage = "El sueldo debe ser mayor que 0 y menor o igual a 50,000")]
        [RegularExpression(@"^\d+(\.\d{1,2})?$", ErrorMessage = "El formato debe ser numérico con hasta dos decimales")]
        public double SueldoHora { get; set; }
    }
}
