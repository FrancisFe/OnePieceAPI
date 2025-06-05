using OnePieceAPI.Exceptions.Common;

namespace OnePieceAPI.Exceptions.FrutasDelDiablo
{
    public class FrutaNoEncontradaException : BaseApiException
    {
        public FrutaNoEncontradaException() : base(404,"FRUTA_NOT_FOUND", "La fruta del diablo no fue encontrada.")
        {
        }
        public FrutaNoEncontradaException(int id)
            : base(404, "FRUTA_NOT_FOUND", $"La fruta del diablo con ID {id} no fue encontrada.")
        {
        }
    }
}
