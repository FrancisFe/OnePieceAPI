using OnePieceAPI.Models.Common;

namespace OnePieceAPI.Models.DTOs.Piratas
{
    public class PirataFiltrosDto : IPageable, ISortable
    {
        public string? Nombre { get; set; }
        public long? RecompensaMin { get; set; }
        public long? RecompensaMax { get; set; }
        public string? TipoFrutaNombre { get; set; }

        public int? TipoFrutaId { get; set; }
        public bool? PiratasConFruta { get; set; } = null;
        public int? TripulacionId { get; set; }

        public int Pagina { get; set; } = 1;
        public int TamañoPagina { get; set; } = 5;
        public string OrdenarPor { get; set; } = "nombre";
        public bool Descendente { get; set; } = false;
    }
}
