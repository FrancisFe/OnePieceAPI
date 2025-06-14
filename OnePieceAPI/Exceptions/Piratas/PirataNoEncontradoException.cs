﻿using OnePieceAPI.Exceptions.Common;

namespace OnePieceAPI.Exceptions.Piratas
{
    public class PirataNoEncontradoException : BaseApiException
    {
        public PirataNoEncontradoException() : base(404, "PIRATA_NOT_FOUND", "El pirata no fue encontrado.")
        {
        }
        public PirataNoEncontradoException(int id) : base(404, "PIRATA_NOT_FOUND", $"El pirata con ID {id} no fue encontrado.")
        {

        }
    }
}
