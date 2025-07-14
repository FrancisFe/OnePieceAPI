using OnePieceAPI.Exceptions.Common;

namespace OnePieceAPI.Exceptions.Piratas
{
    public class PirataNoEncontradoException : BaseApiException
    {
        private const string DefaultCode = "PIRATA_NOT_FOUND";
        public PirataNoEncontradoException() : base(404, DefaultCode, "El pirata no fue encontrado.")
        {
        }
        public PirataNoEncontradoException(int id) : base(404, DefaultCode, $"El pirata con ID {id} no fue encontrado.")
        {

        }
    }
}
