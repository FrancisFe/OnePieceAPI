using OnePieceAPI.Exceptions.Common;

namespace OnePieceAPI.Exceptions.FrutasDelDiablo
{
    public class FrutaYaAsignadaException(int id) : BaseApiException(400, "FRUTA_YA_ASIGNADA",
        $"La fruta del diablo con ID {id} ya está asignada a otro pirata.");
}
