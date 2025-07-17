using OnePieceAPI.Models;

namespace OnePieceAPI.DTOs.Tripulaciones
{
    public class TripulacionDto
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string? Descripcion { get; set; }
        public long RecompensaTotal { get; set; }
        public string Capitan { get; set; } = string.Empty;

        public List<string> Miembros { get; set; } = new List<string>();
       
    }
}
