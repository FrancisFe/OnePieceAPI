using OnePieceAPI.Exceptions.Common;

namespace OnePieceAPI.Exceptions.FrutasDelDiablo
{
    public class FrutaYaExisteException : BaseApiException
    {
        public FrutaYaExisteException(string nombreFruta)
            : base(404, "FRUTA_EXIST", $"La fruta del diablo: {nombreFruta} ya existe")
        {
        }
        public FrutaYaExisteException()
            : base(404, "FRUTA_EXIST", "La fruta del diablo ya existe")
        {
        }
    }
}
