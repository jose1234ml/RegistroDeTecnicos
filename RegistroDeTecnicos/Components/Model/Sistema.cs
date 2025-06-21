using System.ComponentModel.DataAnnotations;

public class Sistema
{
    [Key]
    public int SistemaId { get; set; }

    public int Costo { get; set; } = 0; 

    [Required(ErrorMessage = "La descripción es obligatoria.")]
    [StringLength(200, ErrorMessage = "La descripción no puede exceder los 200 caracteres.")]
    public string Descripcion { get; set; } = string.Empty;

    [Required(ErrorMessage = "La complejidad es obligatoria.")]
    [StringLength(5, ErrorMessage = "La complejidad debe ser 'Baja', 'Media' o 'Alta'.")]
    public string Complejidad { get; set; } = string.Empty;

    [Range(0, int.MaxValue, ErrorMessage = "La existencia debe ser un número positivo.")]
    public int Existencia { get; set; } = 0;
}
