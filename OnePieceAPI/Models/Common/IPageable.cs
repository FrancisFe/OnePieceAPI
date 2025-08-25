namespace OnePieceAPI.Models.Common
{
    public interface IPageable
    {
        int Pagina { get; set; }
        int TamañoPagina { get; set; }
    }
}
