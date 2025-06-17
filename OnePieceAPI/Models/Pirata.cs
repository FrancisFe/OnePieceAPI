using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnePieceAPI.Models
{
    public class Pirata
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required(ErrorMessage = "El nombre es obligatorio")]
        [StringLength(100, ErrorMessage = "El nombre no puede tener mas de 100 caracteres")]
        public string Nombre { get; set; } = string.Empty;
        [StringLength(500, ErrorMessage = "La descripción no puede exceder los 500 caracteres")]
        public string? Descripcion { get; set; }
        [Range(0, long.MaxValue, ErrorMessage = "La recompensa debe ser un número positivo")]
        public long Recompensa { get; set; }

        public int? FrutaDelDiabloId { get; set; }
        public FrutaDelDiablo? FrutaDelDiablo { get; set; }
    }
}
