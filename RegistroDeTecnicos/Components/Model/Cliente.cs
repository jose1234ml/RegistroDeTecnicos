using System.ComponentModel.DataAnnotations;

namespace RegistroDeTecnicos.Components.Model;
public class Cliente
{
    [Key]
    public int ClienteId { get; set; }

    [Required(ErrorMessage = "La fecha de ingreso es obligatoria")]
    public DateTime FechaIngreso { get; set; } = DateTime.Today;

    [Required(ErrorMessage = "El nombre es obligatorio")]
    public string Nombres { get; set; } = string.Empty;

    [Required(ErrorMessage = "La dirección es obligatoria")]
    public string Direccion { get; set; } = string.Empty;

    [Required(ErrorMessage = "El RNC es obligatorio")]
    public string Rnc { get; set; } = string.Empty;

    [Required(ErrorMessage = "El límite de crédito es obligatorio")]
    [Range(0, double.MaxValue, ErrorMessage = "Debe ser un número válido")]
    public decimal LimiteCredito { get; set; }

    [Required(ErrorMessage = "Debe seleccionar un técnico")]
    public int TecnicoId { get; set; }

    public Tecnicos? Tecnico { get; set; }
}
