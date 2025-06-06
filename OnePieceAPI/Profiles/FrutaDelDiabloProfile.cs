using AutoMapper;
using OnePieceAPI.DTOs.FrutasDelDiablo;
using OnePieceAPI.Models;

namespace OnePieceAPI.Profiles
{
    public class FrutaDelDiabloProfile : Profile
    {
        public FrutaDelDiabloProfile()
        {
            CreateMap<FrutaDelDiablo, FrutaDelDiabloDto>();
            CreateMap<ActualizarFrutaDelDiabloDto, FrutaDelDiablo>();
            CreateMap<CrearFrutaDelDiabloDto, FrutaDelDiablo>();
        }
    }
}
