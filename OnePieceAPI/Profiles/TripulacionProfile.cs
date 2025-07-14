using AutoMapper;
using OnePieceAPI.DTOs.Tripulaciones;
using OnePieceAPI.Models;

namespace OnePieceAPI.Profiles
{
    public class TripulacionProfile : Profile
    {
        public TripulacionProfile()
        {
            CreateMap<CrearTripulacionDto, Tripulacion>();
            CreateMap<ActualizarTripulacionDto,Tripulacion>();
            CreateMap<Tripulacion, TripulacionDto>();
        }

    }
}
