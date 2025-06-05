using System.ComponentModel.DataAnnotations;

namespace OnePieceAPI.DTOs.FrutasDelDiablo
{
    public class FrutaDelDiabloDTO
    {
        [Required(ErrorMessage = "El nombre es obligatorio")]
        [StringLength(100, ErrorMessage = "El nombre no puede tener mas de 100 caracteres")]
        public string Nombre { get; set; } = string.Empty;
        [Required(ErrorMessage = "El tipo es obligatorio")]
        [StringLength(30, ErrorMessage = "El tipo no puede tener mas de 30 caracteres")]
        public string Tipo { get; set; } = string.Empty;


    }
}
