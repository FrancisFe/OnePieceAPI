using OnePieceAPI.Models;

namespace OnePieceAPI.DTOs.Piratas
{
    public class PirataConFrutaDto
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string? Descripcion { get; set; }
        public long Recompensa { get; set; }
        public FrutaDelDiablo? FrutaDelDiablo { get; set; } = new FrutaDelDiablo();
    }
}
