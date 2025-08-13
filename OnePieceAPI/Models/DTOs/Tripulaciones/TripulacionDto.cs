using OnePieceAPI.Models;
using OnePieceAPI.Models.DTOs.Piratas;

namespace OnePieceAPI.Models.DTOs.Tripulaciones
{
    public class TripulacionDto
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string? Descripcion { get; set; }
        public long RecompensaTotal { get; set; }
        public PirataSimpleDto? Capitan { get; set; }

        public List<PirataSimpleDto?> Miembros { get; set; } = new List<PirataSimpleDto?>();
       
    }
}
