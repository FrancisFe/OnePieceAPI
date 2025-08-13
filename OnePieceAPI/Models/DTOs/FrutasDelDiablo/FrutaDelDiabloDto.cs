using System.ComponentModel.DataAnnotations;

namespace OnePieceAPI.Models.DTOs.FrutasDelDiablo
{
    public class FrutaDelDiabloDto
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Tipo { get; set; } = string.Empty;


    }
}
