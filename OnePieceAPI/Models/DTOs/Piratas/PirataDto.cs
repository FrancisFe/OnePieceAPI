using OnePieceAPI.Models;
using OnePieceAPI.Models.DTOs.FrutasDelDiablo;
using OnePieceAPI.Models.DTOs.Tripulaciones;
using System.ComponentModel.DataAnnotations;

namespace OnePieceAPI.Models.DTOs.Piratas
{
    public class PirataDto
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string? Descripcion { get; set; }
        public long Recompensa { get; set; }
        public FrutaDelDiabloDto? FrutaDelDiablo { get; set; }

        public TripulacionSimpleDto? Tripulacion { get; set; }
    }
}