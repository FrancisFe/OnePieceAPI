using System.ComponentModel.DataAnnotations;

namespace OnePieceAPI.DTOs.Piratas
{
    public class ActualizarPirataDto
    {
        [Required]
        [MaxLength(50)]
        public string Nombre { get; set; } = string.Empty;

        [MaxLength(400)]
        public string? Descripcion { get; set; }

        [Range(0, int.MaxValue)]
        public int Recompensa { get; set; }

        public int? FrutaDelDiabloId { get; set; }
    }
}
