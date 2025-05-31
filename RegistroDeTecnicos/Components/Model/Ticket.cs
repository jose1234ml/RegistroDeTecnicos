using System;
using System.ComponentModel.DataAnnotations;

namespace RegistroDeTecnicos.Components.Model;

public class Ticket
{
    [Key]
    public int TicketId { get; set; }

    [Required]
    public DateTime Fecha { get; set; }

    [Required]
    public string Prioridad { get; set; } = string.Empty;

    [Required]
    public int ClienteId { get; set; }

    [Required(ErrorMessage = "El asunto es obligatorio")]
    public string Asunto { get; set; } = string.Empty;

    [Required(ErrorMessage = "La descripción es obligatoria")]
    public string Descripcion { get; set; } = string.Empty;

    [Required]
    [Range(0.1, double.MaxValue, ErrorMessage = "El tiempo invertido debe ser mayor que cero")]
    public double TiempoInvertido { get; set; }

    [Required]
    public int TecnicoId { get; set; }
    public Cliente? Cliente { get; set; }
    public Tecnicos? Tecnico { get; set; }
}
