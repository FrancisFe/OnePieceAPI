using OnePieceAPI.DTOs.Piratas;

namespace OnePieceAPI.Models.DTOs.Tripulaciones
{
    public class TripulacionSimpleDto
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string? Descripcion { get; set; }
        public long RecompensaTotal { get; set; }
    }
}
