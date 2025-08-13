using AutoMapper;
using OnePieceAPI.Models.DTOs.FrutasDelDiablo;
using OnePieceAPI.Models.Entities;

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
