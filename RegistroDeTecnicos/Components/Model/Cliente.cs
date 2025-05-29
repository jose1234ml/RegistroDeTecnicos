using System;
using System.ComponentModel.DataAnnotations;

namespace RegistroDeTecnicos.Components.Model
{
    public class Cliente
    {
        [Key]
        public int ClienteId { get; set; }

        [Required(ErrorMessage = "La fecha de ingreso es obligatoria")]
        public DateTime FechaIngreso { get; set; } = DateTime.Today;

        [Required(ErrorMessage = "El nombre es obligatorio")]
        [RegularExpression(@"^[^\d]*$", ErrorMessage = "El nombre no debe contener números")]
        public string Nombres { get; set; } = string.Empty;

        [Required(ErrorMessage = "La dirección es obligatoria")]
        public string Direccion { get; set; } = string.Empty;

        [Required(ErrorMessage = "El RNC es obligatorio")]
        [RegularExpression(@"^\d{1,9}$", ErrorMessage = "El RNC debe tener hasta 9 dígitos numéricos")]
        public string Rnc { get; set; } = string.Empty;

        [Range(1, double.MaxValue, ErrorMessage = "El límite de crédito debe ser mayor que 0")]

        public decimal LimiteCredito { get; set; }

        [Required(ErrorMessage = "Debe seleccionar un técnico")]
        public int TecnicoId { get; set; }

        public Tecnicos? Tecnico { get; set; }
    }
}