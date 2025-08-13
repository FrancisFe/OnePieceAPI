using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnePieceAPI.Models.Entities
{
    public class Tripulacion
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required(ErrorMessage = "El nombre es obligatorio")]
        [StringLength(100, ErrorMessage = "El nombre no puede exceder los 100 caracteres")]
        public string Nombre { get; set; } = string.Empty;
        [StringLength(1000, ErrorMessage = "La descripción no puede exceder los 1000 caracteres")]
        public string? Descripcion { get; set; }
        [Range(0, long.MaxValue, ErrorMessage = "La recompensa total debe ser un número positivo")]
        public long RecompensaTotal { get; set; }
        public int? CapitanId { get; set; }
        public Pirata? Capitan { get; set; }
        public ICollection<Pirata> Miembros { get; set; } = new List<Pirata>();

    }
}
