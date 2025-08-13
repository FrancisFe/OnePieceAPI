using AutoMapper;
using OnePieceAPI.Models.DTOs.Tripulaciones;
using OnePieceAPI.Models.Entities;

namespace OnePieceAPI.Profiles
{
    public class TripulacionProfile : Profile
    {
        public TripulacionProfile()
        {
            CreateMap<CrearTripulacionDto, Tripulacion>();
            CreateMap<ActualizarTripulacionDto,Tripulacion>();
            CreateMap<Tripulacion, TripulacionDto>();
            CreateMap<Tripulacion, TripulacionSimpleDto>();
        }

    }
}
