using OnePieceAPI.Exceptions.Common;

namespace OnePieceAPI.Exceptions.FrutasDelDiablo
{
    public class FrutaYaAsignadaException : BaseApiException
    {
        public FrutaYaAsignadaException(int id)
            : base(400, "FRUTA_YA_ASIGNADA", $"La fruta del diablo con ID {id} ya está asignada a otro pirata.")
        {
        }
    }
}
