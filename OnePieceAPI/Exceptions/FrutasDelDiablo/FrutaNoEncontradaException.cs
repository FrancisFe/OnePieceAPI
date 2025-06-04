using OnePieceAPI.Exceptions.Common;

namespace OnePieceAPI.Exceptions.FrutasDelDiablo
{
    public class FrutaNoEncontradaException : BaseApiException
    {
        public FrutaNoEncontradaException(int id)
            : base(404, "FRUTA_NOT_FOUND", $"La fruta del diablo con ID {id} no fue encontrada.")
        {
        }
    }
}
