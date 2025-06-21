using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RegistroDeTecnicos.Components.Model;

public class Venta
{
    [Key]
    public int VentaId { get; set; }

    [Required(ErrorMessage = "La fecha es obligatoria.")]
    public DateTime Fecha { get; set; } = DateTime.Today;

    [Required(ErrorMessage = "El Cliente es obligatorio.")]
    public int ClienteId { get; set; } 
    [ForeignKey("ClienteId")]
    public virtual Cliente? Cliente { get; set; } 

    [Required(ErrorMessage = "El Monto es obligatorio.")]
   
    public decimal Monto { get; set; }

    public virtual ICollection<VentaDetalle> Detalles { get; set; } = new List<VentaDetalle>();
}