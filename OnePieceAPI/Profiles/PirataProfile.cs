using AutoMapper;
using OnePieceAPI.DTOs.Piratas;
using OnePieceAPI.Models;

namespace OnePieceAPI.Profiles
{
    public class PirataProfile : Profile
    {
        public PirataProfile()
        {
            CreateMap<CrearPirataDto, Pirata>();
            CreateMap<ActualizarPirataDto, Pirata>();
            CreateMap<Pirata, PirataDto>();
            CreateMap<Pirata, PirataConFrutaDto>();
        }
    }
}
