using System.ComponentModel.DataAnnotations;

namespace OnePieceAPI.DTOs.Piratas
{
    public class CrearPirataDto
    {
        [Required(ErrorMessage = "El nombre es obligatorio")]
        [StringLength(100, ErrorMessage = "El nombre no puede tener mas de 100 caracteres")]
        public string Nombre { get; set; } = string.Empty;

        [StringLength(500, ErrorMessage = "La descripción no puede exceder los 500 caracteres")]
        public string? Descripcion { get; set; }
        [Range(0, long.MaxValue, ErrorMessage = "La recompensa debe ser un número positivo")]
        public long Recompensa { get; set; }
        public int? FrutaDelDiabloId { get; set; }
    }
}
