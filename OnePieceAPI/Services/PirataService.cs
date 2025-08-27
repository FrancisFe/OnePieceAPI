using AutoMapper;
using Microsoft.EntityFrameworkCore;
using OnePieceAPI.Models.Common;
using OnePieceAPI.Models.DTOs.Piratas;
using OnePieceAPI.Models.Entities;
using OnePieceAPI.Repositories.Interfaces;
using OnePieceAPI.Services.Interfaces;

namespace OnePieceAPI.Services
{
    public class PirataService : IPirataService
    {
        private readonly IPirataRepository _pirataRepository;
        private readonly IMapper _mapper;
        public PirataService(IPirataRepository pirataRepository, IMapper mapper)
        {
            _pirataRepository = pirataRepository ?? throw new ArgumentNullException(nameof(pirataRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }
        public async Task<PagedResult<PirataDto>> GetPirataConFiltrosAsync(PirataFiltrosDto filtros)
        {
            var query = _pirataRepository.GetQueryable();

            if (!string.IsNullOrWhiteSpace(filtros.Nombre))
            {
                query = query.Where(p => p.Nombre == filtros.Nombre);
            }

            if (filtros.TripulacionId.HasValue)
            {
                query = query.Where(p => p.TripulacionId == filtros.TripulacionId.Value);
            }

            if (filtros.PiratasConFruta.HasValue)
            {
                query = filtros.PiratasConFruta.Value ? query.Where(p => p.FrutaDelDiablo != null) : query.Where(p => p.FrutaDelDiablo == null);
            }
            if(!string.IsNullOrWhiteSpace(filtros.TipoFrutaNombre))
            {
                query = query.Where(p => p.FrutaDelDiablo != null && p.FrutaDelDiablo.Tipo == filtros.TipoFrutaNombre);
            }
            if (filtros.TipoFrutaId.HasValue)
            {
                query = query.Where(p => p.FrutaDelDiablo != null && p.FrutaDelDiablo.Id == filtros.TipoFrutaId.Value);    
            }
            if (filtros.RecompensaMin.HasValue)
            {
                query = query.Where(p => p.Recompensa >= filtros.RecompensaMin.Value);
            }

            if (filtros.RecompensaMax.HasValue)
            {
                query = query.Where(p => p.Recompensa <= filtros.RecompensaMax.Value);
            }

            var totalItems = await query.CountAsync();

            query = filtros.OrdenarPor.ToLower() switch
            {
                "nombre" => filtros.Descendente ? query.OrderByDescending(p => p.Nombre) : query.OrderBy(p => p.Nombre),
                "recompensa" => filtros.Descendente ? query.OrderByDescending(p => p.Recompensa) : query.OrderBy(p => p.Recompensa),
                _ => query.OrderBy(p => p.Nombre)
            };
            var items = await query
                .Skip((filtros.Pagina - 1) * filtros.TamañoPagina)
                .Take(filtros.TamañoPagina)
                .ToListAsync();

            var itemsDto = _mapper.Map<IEnumerable<PirataDto>>(items);

            return new PagedResult<PirataDto>
            {
                Items = itemsDto,
                TotalItems = totalItems,
                PageNumber = filtros.Pagina,
                PageSize = filtros.TamañoPagina
            };
        }
        public async Task<PirataDto?> GetByIdAsync(int id)
        {
            var pirata = await _pirataRepository.GetAsync(id);
            return _mapper.Map<PirataDto>(pirata);

        }
        public async Task<PirataDto?> CreateAsync(CrearPirataDto pirata)
        {
            var pirataNuevo = _mapper.Map<Pirata>(pirata);
            await _pirataRepository.CreateAsync(pirataNuevo);
            return _mapper.Map<PirataDto>(pirataNuevo);
        }
        public async Task<PirataDto?> UpdateAsync(int id, ActualizarPirataDto pirata)
        {
            var pirataEntrante = _mapper.Map<Pirata>(pirata);
            var pirataExistente =await _pirataRepository.UpdateAsync(id, pirataEntrante);
            var pirataConDto = _mapper.Map<PirataDto>(pirataExistente);
            return pirataConDto;
        }
        public async Task<bool> DeleteAsync(int id)
        {
            return await _pirataRepository.DeleteAsync(id);
        }

    }
}
