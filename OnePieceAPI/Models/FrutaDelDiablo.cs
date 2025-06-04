using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnePieceAPI.Models
{
    public class FrutaDelDiablo
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string Nombre { get; set; } = string.Empty;

        public string Tipo { get; set; } = string.Empty;
        [MaxLength(200)]
        public string? Descripcion { get; set; }
    }
}
