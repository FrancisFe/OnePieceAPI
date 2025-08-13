using System.ComponentModel.DataAnnotations;

namespace OnePieceAPI.Models.DTOs.Tripulaciones
{
    public class ActualizarTripulacionDto
    {
        [Required(ErrorMessage = "El nombre es obligatorio")]
        [StringLength(100, ErrorMessage = "El nombre no puede exceder los 100 caracteres")]
        public string Nombre { get; set; } = string.Empty;

        [StringLength(1000, ErrorMessage = "La descripción no puede exceder los 1000 caracteres")]
        public string? Descripcion { get; set; }

        [Range(0, long.MaxValue, ErrorMessage = "La recompensa total debe ser un número positivo")]

        public int? CapitanId { get; set; }
    }
}
