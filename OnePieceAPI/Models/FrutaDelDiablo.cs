using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnePieceAPI.Models
{
    public class FrutaDelDiablo
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        [MaxLength(50)]
        public string Nombre { get; set; } = string.Empty;
        [Required]
        [MaxLength(50)]
        public string Tipo { get; set; } = string.Empty;
        [MaxLength(200)]
        public string? Descripcion { get; set; }

        [ForeignKey("PirataId")]
        public Pirata? Pirata { get; set; }
        public int PirataId { get; set; }
    }
}
