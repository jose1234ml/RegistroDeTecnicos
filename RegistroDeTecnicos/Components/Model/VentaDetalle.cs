using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RegistroDeTecnicos.Components.Model;

public class VentaDetalle
{
    [Key]
    public int VentaDetalleId { get; set; }

    [Required(ErrorMessage = "La Venta es obligatoria.")]
    public int VentaId { get; set; } 
    [ForeignKey("VentaId")]
    public virtual Venta? Venta { get; set; } 

    [Required(ErrorMessage = "El Sistema es obligatorio.")]
    public int SistemaId { get; set; } 
    [ForeignKey("SistemaId")]
    public virtual Sistema? Sistema { get; set; } 

    [Required(ErrorMessage = "La Cantidad es obligatoria.")]
    [Range(1, int.MaxValue, ErrorMessage = "La Cantidad debe ser mayor que cero.")]
    public int Cantidad { get; set; }

    [Required(ErrorMessage = "El Precio es obligatorio.")]
    [Range(0.01, double.MaxValue, ErrorMessage = "El Precio debe ser mayor que cero.")]
    public decimal Precio { get; set; }

    [NotMapped]
    public decimal Subtotal => Cantidad * Precio;
}