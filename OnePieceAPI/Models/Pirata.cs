using System.ComponentModel.DataAnnotations;

namespace OnePieceAPI.Models
{
    public class Pirata
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(50)]
        public string Nombre { get; set; } = string.Empty;
        [MaxLength(400)]
        public string? Descripcion { get; set; }
        public double Recompensa { get; set; }
        public FrutaDelDiablo FrutaDelDiablo { get; set; } = new FrutaDelDiablo();
    }
}
