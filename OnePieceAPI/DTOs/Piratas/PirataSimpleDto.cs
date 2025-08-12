using OnePieceAPI.DTOs.FrutasDelDiablo;

namespace OnePieceAPI.DTOs.Piratas
{
    public class PirataSimpleDto
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string? Descripcion { get; set; }
        public long Recompensa { get; set; }
    }
}
