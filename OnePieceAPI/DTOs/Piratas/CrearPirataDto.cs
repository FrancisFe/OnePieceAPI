using System.ComponentModel.DataAnnotations;

namespace OnePieceAPI.DTOs.Piratas
{
    public class CrearPirataDto
    {
        [Required]
        [MaxLength(50)]
        public string Nombre { get; set; } = string.Empty;
        public string? Descripcion { get; set; }
        public int Recompensa { get; set; }
        public int? FrutaDelDiabloId { get; set; }
    }
}
