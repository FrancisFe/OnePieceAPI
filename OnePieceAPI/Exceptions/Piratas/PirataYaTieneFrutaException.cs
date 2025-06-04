using OnePieceAPI.Exceptions.Common;

namespace OnePieceAPI.Exceptions.Piratas
{
    public class PirataYaTieneFrutaException : BaseApiException
    {
        public PirataYaTieneFrutaException(int pirataId) : base (404,"PIRATA_YA_TIENE_FRUTA",$"El pirata con id {pirataId} ya tiene una fruta del diablo asignada.")
        {

        }
    }
}
